namespace App.Scripts.Systems.PopupSystem.Interfaces {
    /// <summary>
    /// Call right after popup close animation ended, in case dont have close animation, this will be call right after <see cref="IOnClosePopup"/>
    /// </summary>
    public interface IOnClosedPopup {
        void OnClosedPopup();
    }
}