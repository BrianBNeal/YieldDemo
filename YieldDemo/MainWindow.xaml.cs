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

    private async void ReadFile_Click(object sender, RoutedEventArgs e)
    {
        string path = Path.Combine("C:\\Users\\brian\\source\\repos\\YieldDemo\\YieldDemo\\Data.txt");

        var lines = GetLinesAsync(path);
        await foreach (string line in lines)
        {
            _listBox.Items.Add(line);
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