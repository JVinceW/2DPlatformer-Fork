namespace App.Scripts.Systems.PopupSystem.Interfaces {
    /// <summary>
    /// Call right after popup open animation end, in case of popup dont have animation, this will be call right after <see cref="IOnOpenPopup"/>
    /// </summary>
    public interface IOnOpenedPopup {
        void OnOpenedPopup();
    }
}