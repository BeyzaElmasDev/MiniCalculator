using System;
using System.Globalization;

namespace MiniCalculator;

public partial class MainPage : ContentPage
{
    double firstNumber = 0;
    string selectedOperator = "";

    public MainPage()
    {
        InitializeComponent();
    }

   
    private bool TryParseInput(string text, out double number)
    {
        number = 0;

        if (string.IsNullOrWhiteSpace(text))
            return false;

        
        text = text.Replace(',', '.');

        return double.TryParse(
            text,
            NumberStyles.Any,
            CultureInfo.InvariantCulture,
            out number
        );
    }

    
    private void OnAddClicked(object sender, EventArgs e)      => StoreFirstNumber("+");
    private void OnSubtractClicked(object sender, EventArgs e) => StoreFirstNumber("-");
    private void OnMultiplyClicked(object sender, EventArgs e) => StoreFirstNumber("*");
    private void OnDivideClicked(object sender, EventArgs e)   => StoreFirstNumber("/");

    
    private void StoreFirstNumber(string op)
    {
        if (TryParseInput(NumberEntry.Text, out firstNumber))
        {
            selectedOperator = op;
            NumberEntry.Text = "";
            ResultLabel.Text = $"İşlem: {firstNumber} {op}";
        }
        else
        {
            ResultLabel.Text = "Geçerli bir sayı giriniz.";
        }
    }

    
    private void OnEqualsClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(selectedOperator))
        {
            ResultLabel.Text = "Lütfen bir işlem seçiniz.";
            return;
        }

        if (!TryParseInput(NumberEntry.Text, out double secondNumber))
        {
            ResultLabel.Text = "Geçerli bir sayı giriniz.";
            return;
        }

        double result = 0;

        switch (selectedOperator)
        {
            case "+":
                result = firstNumber + secondNumber;
                break;
            case "-":
                result = firstNumber - secondNumber;
                break;
            case "*":
                result = firstNumber * secondNumber;
                break;
            case "/":
                if (secondNumber == 0)
                {
                    ResultLabel.Text = "Sıfıra bölünemez!";
                    return;
                }
                result = firstNumber / secondNumber;
                break;
            default:
                ResultLabel.Text = "Lütfen bir işlem seçiniz.";
                return;
        }

        ResultLabel.Text = $"Sonuç: {result}";
        firstNumber = result;
        NumberEntry.Text = result.ToString(CultureInfo.InvariantCulture);
        selectedOperator = "";
    }

    
    private void OnClearEntryClicked(object sender, EventArgs e)
    {
        NumberEntry.Text = "";
        ResultLabel.Text = "Giriş temizlendi.";
    }

    
    private void OnClearAllClicked(object sender, EventArgs e)
    {
        NumberEntry.Text = "";
        ResultLabel.Text = "Tüm veriler temizlendi.";
        firstNumber = 0;
        selectedOperator = "";
    }

    
    private void OnDecimalClicked(object sender, EventArgs e)
    {
        var text = NumberEntry.Text ?? "";

        
        if (!text.Contains(".") && !text.Contains(","))
        {
            if (string.IsNullOrEmpty(text))
                NumberEntry.Text = "0.";
            else
                NumberEntry.Text = text + ".";
        }
    }

   
    private void OnSquareClicked(object sender, EventArgs e)
    {
        if (!TryParseInput(NumberEntry.Text, out double number))
        {
            ResultLabel.Text = "Geçerli bir sayı giriniz.";
            return;
        }

        double result = number * number;
        ResultLabel.Text = $"Sonuç: {result}";
        NumberEntry.Text = result.ToString(CultureInfo.InvariantCulture);

        firstNumber = result;
        selectedOperator = "";
    }

    
    private void OnSqrtClicked(object sender, EventArgs e)
    {
        if (!TryParseInput(NumberEntry.Text, out double number))
        {
            ResultLabel.Text = "Geçerli bir sayı giriniz.";
            return;
        }

        if (number < 0)
        {
            ResultLabel.Text = "Negatif sayının karekökü alınamaz.";
            return;
        }

        double result = Math.Sqrt(number);
        ResultLabel.Text = $"Sonuç: {result}";
        NumberEntry.Text = result.ToString(CultureInfo.InvariantCulture);

        firstNumber = result;
        selectedOperator = "";
    }
}
