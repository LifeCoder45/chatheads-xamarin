using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamarinChatHeads.Services
{
    [Service(Label = "Chat Head Service")]
    public class ChatHeadService : Service
    {
        private ImageView _chatHead;

        private float _initialTouchX;
        private float _initialTouchY;

        private int _initialX;
        private int _initialY;

        private WindowManagerLayoutParams _layoutParams;

        private IWindowManager _windowManager;

        public override void OnCreate()
        {
            base.OnCreate();

            _windowManager = GetSystemService(WindowService).JavaCast<IWindowManager>();

            _chatHead = new ImageView(this);
            _chatHead.Touch += ChatHeadOnTouch;
            _chatHead.SetImageResource(Resource.Drawable.Face);

            _layoutParams = new WindowManagerLayoutParams(
                ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent,
                WindowManagerTypes.Phone,
                WindowManagerFlags.NotFocusable,
                Format.Translucent)
            {
                Gravity = GravityFlags.Top | GravityFlags.Left,
                X = 0,
                Y = 100
            };

            _windowManager.AddView(_chatHead, _layoutParams);
        }

        private void ChatHeadOnTouch(object sender, View.TouchEventArgs touchEventArgs)
        {
            var e = touchEventArgs.Event;

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    _initialX = _layoutParams.X;
                    _initialY = _layoutParams.Y;
                    _initialTouchX = e.RawX;
                    _initialTouchY = e.RawY;
                    break;
                case MotionEventActions.Up:
                    break;
                case MotionEventActions.Move:
                    _layoutParams.X = _initialX + (int) (e.RawX - _initialTouchX);
                    _layoutParams.Y = _initialY + (int) (e.RawY - _initialTouchY);
                    _windowManager.UpdateViewLayout(_chatHead, _layoutParams);
                    break;
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            if (_chatHead != null)
                _windowManager.RemoveView(_chatHead);
        }

        public override IBinder OnBind(Intent intent) => null;
    }
}