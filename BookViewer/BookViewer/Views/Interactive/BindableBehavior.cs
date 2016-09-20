using System;
using Xamarin.Forms;

namespace BookViewer.Views.Interactive
{
    /// <summary>
    /// BindablePropertyを持つBehaviorの基底クラス
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BindableBehavior<T> : Behavior<T> where T : BindableObject
    {
        /// <summary>
        /// ビヘイビアがアタッチされるオブジェクト
        /// </summary>
        protected T AssociatedObject { get; private set; }
        /// <summary>
        /// アタッチ処理
        /// </summary>
        /// <param name="bindableObject">アタッチ対象のオブジェクト</param>
        protected override void OnAttachedTo(T bindableObject)
        {
            base.OnAttachedTo(bindableObject);

            AssociatedObject = bindableObject;
            // アタッチ対象のオブジェクトにBindingContextが設定されていた場合、ビヘイビアの
            // BindingContextにも同じ値を設定する
            // これをしておかないと、BindablePropertyにXAML上でBinding指定しても実際には
            // 設定されない
            if (bindableObject.BindingContext != null)
                BindingContext = bindableObject.BindingContext;

            // アタッチ対象のBindablePropertyの変更イベントにハンドラを設定する
            bindableObject.BindingContextChanged += OnBindingContextChanged;
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        /// <summary>
        /// デタッチ時にBindingContextのイベントからハンドラを除去する
        /// </summary>
        /// <param name="bindableObject"></param>
        protected override void OnDetachingFrom(T bindableObject)
        {
            bindableObject.BindingContextChanged -= OnBindingContextChanged;
        }

        /// <summary>
        /// アタッチ先のBindingContext、または自分自身のBindingContextが変更された場合
        /// 自分自身のBindingContextにアタッチ先のBindingContextを設定する
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }
    }
}
