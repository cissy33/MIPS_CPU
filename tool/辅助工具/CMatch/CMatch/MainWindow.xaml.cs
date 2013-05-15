using Cal_Note.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace CMatch
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private string lText;
        public string Text
        {
            set 
            {
                if (!string.IsNullOrEmpty(value))
                {
                    lText = value;
                    setTitleColumn();
                    if (flag)
                        return;
                    getTexts();
                    showText();
                }
            }
        }

        private List<string> lTexts;
        private List<int> lCheckNums = new List<int>();
        private Dictionary<string, int> lTitles;
        private Dictionary<int, int> lCheckLists;
        private int lIndex;
        private Dictionary<int, bool> lLineStates ;
        private bool flag = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void getTexts()
        {
            lTexts = new List<string>();
            string pattern = @"(\w)+";
            string input = lText;
            MatchCollection matches = Regex.Matches(input.Substring(lIndex + 1), pattern);
            if (matches != null)
            {
                foreach (Match match in matches)
                {
                    lTexts.Add(match.ToString());
                }
            }
            else
                MessageBox.Show("输入错误，请输入正确的输入text");

        }

        private void getColumn()
        {
            int lCount = 0;
            lTitles = new Dictionary<string, int>();
            lCheckLists = new Dictionary<int, int>();
            string pattern = @"(\w)+";// @"^#([a-z, _]+\t+){1,}";
            string input = lText;
            lIndex = input.IndexOf('\r');
            if (lIndex == -1)
            {
                MessageBox.Show("输入错误，请输入正确的输入1");
                flag = true;
            }
            else
            {
                flag = false;
                MatchCollection matches = Regex.Matches(input.Substring(0, lIndex), pattern);
                if (matches != null)
                {
                    foreach (Match match in matches)
                    {
                        lTitles.Add(match.ToString(), lCount);
                        lCheckLists.Add(lCount, 0);
                        lCount++;
                    }
                }
                else
                    MessageBox.Show("输入错误，请输入正确的输入cloumn");
            }
        }

        private void setTitleColumn()
        {
            titleGrd.Children.Clear();
            getColumn();
            if (flag)
                return;
            CInitGrd.showGrid(1, lTitles.Count, titleGrd);
            foreach (var item in lTitles)
            {
                CheckBox cb = new CheckBox();
                cb.Content = item.Key;
                cb.SetValue(Grid.ColumnProperty, item.Value);
                cb.HorizontalAlignment = HorizontalAlignment.Center;
                cb.VerticalAlignment =  VerticalAlignment.Center;
                cb.Click += cb_Click;
                titleGrd.Children.Add(cb);
            }
        }

        void cb_Click(object sender, RoutedEventArgs e)
        {
            var name = (sender as CheckBox).Content.ToString();
            if ((sender as CheckBox).IsChecked == true)
            {
                lCheckLists[lTitles[name]] = 1;
                lCheckNums.Add(lTitles[name]);
            }
            else if ((sender as CheckBox).IsChecked == false)
            {
                lCheckLists[lTitles[name]] = 0;
                lCheckNums.Remove(lTitles[name]);
            }
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text = (sender as TextBox).Text;
            if (flag)
            {
                //(sender as TextBox).Text = "";
                flag = false;
            }
        }

        private void click_match(object sender, RoutedEventArgs e)
        {
            match();
            showMatchState();
        }

        private void match()
        {
            lLineStates = new Dictionary<int,bool>();
            if (lCheckNums.Count == 0 && lCheckNums.Count == 1)
                MessageBox.Show("选择数目不正确，请重新选择");
            else
            {
                for (int j = 0; j < lTexts.Count / lTitles.Count; j++)
                {
                    bool flag = false;
                    for (int i = 1; i < lCheckNums.Count; i++)
                    {
                        if(lTexts[j * lTitles.Count + lCheckNums[0]] == lTexts[j * lTitles.Count + lCheckNums[i]])
                            flag = true;
                        else
                        {
                            flag = false;
                            break;
                        }
                    }
                    lLineStates.Add(j, flag);
                }
                    
            }
         }

        private void showText()
        {
            CInitGrd.showGrid(lTexts.Count / lTitles.Count, lTitles.Count, textGrd);
           
            for (int i = 0; i < lTexts.Count; i++)
			{
                TextBlock tb = new TextBlock();
                tb.TextWrapping = TextWrapping.Wrap;
                tb.TextAlignment = TextAlignment.Center;
                if ((i % lTitles.Count) % 2 == 1)
                {
                    tb.Background = new SolidColorBrush(Colors.LightGray);
                }
                tb.Text = lTexts[i];
                tb.SetValue(Grid.RowProperty, i / lTitles.Count);
                tb.SetValue(Grid.ColumnProperty, i % lTitles.Count);
                textGrd.Children.Add(tb);
			}
                
           
        }

        private void showMatchState()
        {
            stateGrd.Children.Clear();
            CInitGrd.showGrid(lTexts.Count / lTitles.Count, 1, stateGrd);
            foreach (var item in lLineStates)
            {
                TextBlock tb = new TextBlock();
                tb.Text = item.Value.ToString();
                if (tb.Text.Equals("False"))
                    tb.Foreground = new SolidColorBrush(Colors.Red);
                tb.TextAlignment = TextAlignment.Right;
                tb.SetValue(Grid.RowProperty, item.Key);
                stateGrd.Children.Add(tb);
            }
        }

        private void click_clear(object sender, RoutedEventArgs e)
        {
            inputbox.Text = "";
            titleGrd.Children.Clear();
            textGrd.Children.Clear();
            stateGrd.Children.Clear();
            lTitles.Clear();
            lCheckLists.Clear();
            lLineStates.Clear();
            lCheckNums.Clear();
            //flag = false;
        }
            
        
    }
}
