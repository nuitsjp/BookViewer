using System;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookViewer.Views.Interactive
{
    /// <summary>
    /// アタッチしているオブジェクトで特定のイベントを購読し、イベント発行時にバインドされたコマンドを実行するビヘイビア
    /// </summary>
    public class EventToCommandBehavior : BindableBehavior<VisualElement>
    {
        #region Command BindableProperty
        public static readonly BindableProperty CommandProperty = BindableProperty.Create<EventToCommandBehavior, ICommand>(p => p.Command, null);
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        #endregion

        #region CommandParameter BindableProperty
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create<EventToCommandBehavior, object>(p => p.CommandParameter, null);
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        #endregion

        #region Converter BindableProperty
        public static readonly BindableProperty ConverterProperty = BindableProperty.Create<EventToCommandBehavior, IValueConverter>(p => p.Converter, null);
        public IValueConverter Converter
        {
            get { return (IValueConverter)GetValue(ConverterProperty); }
            set { SetValue(ConverterProperty, value); }
        }
        #endregion

        /// <summary>
        /// 監視対象のイベント名
        /// </summary>
        public string EventName { get; set; }
        /// <summary>
        /// アタッチしているオブジェクトのEventNameと一致する名称のEventInfo
        /// </summary>
        EventInfo eventInfo;
        /// <summary>
        /// アタッチしているオブジェクトの対象のイベントを購読するイベントハンドラ
        /// </summary>
        Delegate eventHandler;

        /// <summary>
        /// アタッチ時に、対象のイベントの購読設定を行う
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnAttachedTo(VisualElement bindable)
        {
            base.OnAttachedTo(bindable);

            if (string.IsNullOrWhiteSpace(EventName))
            {
                return;
            }

            // 指定された名称のイベントが存在しない場合、例外をスローする
            eventInfo = AssociatedObject.GetType().GetRuntimeEvent(EventName);
            if (eventInfo == null)
                throw new ArgumentException($"EventToCommandBehavior: Can't register the '{EventName}' event.");

            // OnEventメソッドでイベントを購読するため、MethodInfoからデリゲートを作成しイベントへ追加する
            MethodInfo methodInfo = typeof(EventToCommandBehavior).GetTypeInfo().GetDeclaredMethod("OnEvent");
            eventHandler = methodInfo.CreateDelegate(eventInfo.EventHandlerType, this);
            eventInfo.AddEventHandler(AssociatedObject, eventHandler);
        }

        /// <summary>
        /// デタッチ時に、イベントの購読を解除する
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnDetachingFrom(VisualElement bindable)
        {
            if (eventInfo != null && eventHandler != null)
                eventInfo.RemoveEventHandler(AssociatedObject, eventHandler);

            base.OnDetachingFrom(bindable);
        }

        /// <summary>
        /// イベントを購読する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        void OnEvent(object sender, object eventArgs)
        {
            // コマンドがバインドされていない場合、何もしない
            if (Command == null)
            {
                return;
            }

            // コマンド実行時のパラメータを決定する
            object resolvedParameter;
            if (CommandParameter != null)
            {
                // Commandプロパティに値が指定されていた場合、対象の値を利用する
                resolvedParameter = CommandParameter;
            }
            else if (Converter != null)
            {
                // Converterが指定されていた場合、イベントパラメータをコンバータで変換した結果を利用する
                resolvedParameter = Converter.Convert(eventArgs, typeof(object), null, null);
            }
            else
            {
                // それ以外の場合、イベント引数を利用する
                resolvedParameter = eventArgs;
            }

            // コマンドが実行可能であれば、コマンドを実行する
            if (Command.CanExecute(resolvedParameter))
            {
                Command.Execute(resolvedParameter);
            }
        }
    }
}
