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
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(EventToCommandBehavior));
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        #endregion

        #region CommandParameter BindableProperty
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(EventToCommandBehavior));
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        #endregion

        #region Converter BindableProperty
        public static readonly BindableProperty ConverterProperty = BindableProperty.Create(nameof(Converter), typeof(IValueConverter), typeof(EventToCommandBehavior));
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
        private EventInfo _eventInfo;
        /// <summary>
        /// アタッチしているオブジェクトの対象のイベントを購読するイベントハンドラ
        /// </summary>
        private Delegate _eventHandler;

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
            _eventInfo = AssociatedObject.GetType().GetRuntimeEvent(EventName);
            if (_eventInfo == null)
                throw new ArgumentException($"EventToCommandBehavior: Can't register the '{EventName}' event.");

            // OnEventメソッドでイベントを購読するため、MethodInfoからデリゲートを作成しイベントへ追加する
            MethodInfo methodInfo = typeof(EventToCommandBehavior).GetTypeInfo().GetDeclaredMethod("OnEvent");
            _eventHandler = methodInfo.CreateDelegate(_eventInfo.EventHandlerType, this);
            _eventInfo.AddEventHandler(AssociatedObject, _eventHandler);
        }

        /// <summary>
        /// デタッチ時に、イベントの購読を解除する
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnDetachingFrom(VisualElement bindable)
        {
            if (_eventInfo != null && _eventHandler != null)
                _eventInfo.RemoveEventHandler(AssociatedObject, _eventHandler);

            base.OnDetachingFrom(bindable);
        }

        /// <summary>
        /// イベントを購読する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        // ReSharper disable once UnusedParameter.Local
        // ReSharper disable once UnusedMember.Local
        private void OnEvent(object sender, object eventArgs)
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
