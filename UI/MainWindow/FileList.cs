using Core.ApplicationState;
using UI.Components;

namespace UI.MainWindow;

public class FileList : Gtk.ListBox
{
    public delegate void FileCallback(string file);
    public event FileCallback? OnFileSelected;
    public event FileCallback? OnFileDelete;

    public FileList(ICollection<string> files)
    {
        SetActivateOnSingleClick(true);
        AddCssClass("boxed-list");

        OnRowActivated += (sender, args) =>
        {
            DeleteRow row = (DeleteRow)args.Row;
            OnFileSelected?.Invoke(row.GetTitle());
        };

        UpdateRecentFiles(files);
    }

    public void UpdateRecentFiles(ICollection<string> files)
    {
        for (Gtk.Widget? child; (child = GetLastChild()) != null;)
        {
            Remove(child);
        }

        foreach (string file in files)
        {
            DeleteRow row = new(file);
            row.OnDelete += () => OnFileDelete?.Invoke(file);
            row.SetActivatable(true);
            Prepend(row);
        }
    }
}
