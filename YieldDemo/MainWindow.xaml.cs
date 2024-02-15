using System.IO;
using System.Windows;

namespace YieldDemo;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void ReadFileAsync_Click(object sender, RoutedEventArgs e)
    {
        _listBox.Items.Clear();

        string path = Path.Combine(Environment.CurrentDirectory, "../../../Data.txt");

        var lines = GetLinesAsync(path);
        await foreach (string line in lines)
        {
            _listBox.Items.Add(line);
        }
    }
    private void ReadFile_Click(object sender, RoutedEventArgs e)
    {
        _listBox.Items.Clear(); //this doesn't fluidly clear the UI

        string path = Path.Combine(Environment.CurrentDirectory, "../../../Data.txt");

        var lines = GetLines(path);
        foreach (string line in lines)
        {
            _listBox.Items.Add(line);
        }
    }

    private void ClearList_Click(object sender, RoutedEventArgs e)
    {
        _listBox.Items.Clear();
    }

    private IEnumerable<string> GetLines(string path)
    {
        using StreamReader file = new(path);

        while (file.ReadLine() is { } line)
        {
            Thread.Sleep(300);
            yield return line;
        }
    }

    private async IAsyncEnumerable<string> GetLinesAsync(string path)
    {
        using StreamReader file = new(path);
        
        while (await file.ReadLineAsync() is { } line)
        {
            await Task.Delay(300);
            yield return line;
        }
    }

}