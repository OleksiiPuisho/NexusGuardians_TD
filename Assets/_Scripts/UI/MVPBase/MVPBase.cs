namespace MVP
{
    public interface IView
    {
        void Show();
        void Hide();
    }

    public interface IView<TPresenter> : IView where TPresenter : IPresenter
    {
        TPresenter Presenter { get; }
        void InitPresenter(TPresenter presenter);
    }

    public interface IPresenter
    {
        void Initialize();
    }

    public interface IPresenter<TView> : IPresenter where TView : IView
    {
        TView View { get; }
    }
    
    public interface IPresenter<TView, TModel> : IPresenter where TView : IView where TModel : IModel
    {
        TView View { get; }
        TModel Model { get; }
    }

    public interface IModel
    {
    }
}
