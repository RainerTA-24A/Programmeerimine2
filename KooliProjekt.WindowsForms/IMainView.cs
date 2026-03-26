using System.Collections.Generic;
using KooliProjekt.WindowsForms.Api;

namespace KooliProjekt.WindowsForms
{
    public interface IMainView
    {
        IList<Toode>? DataSource { get; set; }
        Toode? SelectedItem { get; set; }
        void SetPresenter(MainViewPresenter presenter);
        void ShowError(string message, OperationResult result);
        int CurrentId { get; set; }
        string CurrentTitle { get; set; }
    }
}