using Core.ApplicationState;
using UI.Components;

namespace UI.MainWindow;

public class FileList : Gtk.ListBox
{
    public delegate void FileSelectedCallback(string file);
    public event FileSelectedCallback? OnFileSelected;

    public FileList(ICollection<string> files)
    {
        SetActivateOnSingleClick(true);
        AddCssClass("boxed-list");
        UpdateRecentFiles(files);
    }

    public void UpdateRecentFiles(ICollection<string> files)
    {
        for (Gtk.Widget? child; (child = GetLastChild()) != null;)
        {
            Remove(child);
        }

        OnRowActivated += (sender, args) =>
        {
            DeleteRow row = (DeleteRow)args.Row;
            OnFileSelected?.Invoke(row.GetTitle());
        };

        foreach (string file in files)
        {
            DeleteRow row = new(file);
            row.OnDelete += () => StateManager.State.RecentFiles.Remove(file);
            row.SetActivatable(true);
            Prepend(row);
        }
    }
}
