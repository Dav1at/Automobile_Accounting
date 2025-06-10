using Automobile_Accounting.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Diagnostics; 
using PdfSharp.Fonts; 
using System.IO; 
using System.Reflection; 
using PdfSharp.UniversalAccessibility.Drawing;

namespace Automobile_Accounting.Pages
{
    public partial class PartnerPage : Page
    {
        private Random random = new Random();
        private List<Car> carList;
        private decimal carPrice = 0;

        public PartnerPage()
        {
            InitializeComponent();
        }

        private void PartnerPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Инициализация списка автомобилей
            carList = new List<Car>()
            {
                new Car { name_brend = "Stellardyne", name_model = "Nebula X", name_configuration = "Base", sum_sale = 2500000 },
                new Car { name_brend = "Stellardyne", name_model = "Zenith LX", name_configuration = "Base", sum_sale = 3000000 },
                new Car { name_brend = "Stellardyne", name_model = "Terra", name_configuration = "Luxury", sum_sale = 3300000 },
                new Car { name_brend = "Voltus Motors", name_model = "Aethera", name_configuration = "Luxury", sum_sale = 3200000 },
                new Car { name_brend = "Voltus Motors", name_model = "Aethera GT", name_configuration = "Sport", sum_sale = 4400000 }
            };

            // Генерация случайного номера заказа
            int randomNumber = random.Next(0, 1000);
            TBlockNumOrder.Text = randomNumber.ToString();

            // Заполнение ComboBox для брендов
            List<string> brendList = new List<string>() { "Stellardyne", "Voltus Motors" };
            ComboBoxBrendAvto.ItemsSource = brendList;

            // Заполнение ComboBox для цветов
            List<string> colorList = new List<string>() { "Белый", "Черный", "Серый", "Красный", "Синий", "Серебряный" };
            ComboBoxColorOrder.ItemsSource = colorList;

            TBoxQuantityOrder.Text = " ";

            // Заполнение ComboBox для партнеров
            List<partners> partners = App.Context.partners.ToList();
            ComboBoxPartner.ItemsSource = partners;

            UpdateData();
        }

        private void ComboBoxBrendAvto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TBoxQuantityOrder.Text = "";
            UpdateModelOptions();
            ComboBoxConfigOrder.ItemsSource = null; // Clear config combobox
            CalculateTotal();
        }

        private void ComboBoxModelOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TBoxQuantityOrder.Text = "";
            UpdateConfigurationOptions();
            CalculateTotal();
        }

        private void ComboBoxConfigOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TBoxQuantityOrder.Text = "";
            CalculateTotal();
        }

        private void ComboBoxPartner_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (ComboBoxPartner.SelectedItem is partners selectedPartner)
            {
                TBlockFIOParnerOrder.Text = selectedPartner.fio_partner;
            }
            else
            {
                TBlockFIOParnerOrder.Text = "";
            }
            CalculateTotal();
        }

        private void TBoxQuantityOrder_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateTotal();
        }

        private void BtnSendOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TBoxQuantityOrder.Text))
                {
                    MessageBox.Show("Пожалуйста, введите количество автомобилей.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!int.TryParse(TBoxQuantityOrder.Text, out int quantity))
                {
                    MessageBox.Show("Пожалуйста, введите корректное число автомобилей.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (ComboBoxBrendAvto.SelectedItem == null || ComboBoxModelOrder.SelectedItem == null || ComboBoxConfigOrder.SelectedItem == null
                    || ComboBoxColorOrder.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, выберите все параметры автомобиля.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int availableStock = 1500;
                if (quantity > availableStock)
                {
                    MessageBox.Show($"На складе недостаточно автомобилей. Доступно: {availableStock}", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                decimal discount = CalculateDiscount(quantity);

                var newOrder = new order
                {
                    num_order = int.Parse(TBlockNumOrder.Text),
                    name_brend = ComboBoxBrendAvto.SelectedItem?.ToString(),
                    name_model = ComboBoxModelOrder.SelectedItem?.ToString(),
                    name_configuration = ComboBoxConfigOrder.SelectedItem?.ToString(),
                    name_color = ComboBoxColorOrder.SelectedItem?.ToString(),
                    quantity_avto = TBoxQuantityOrder.Text,
                    name_partner = (ComboBoxPartner.SelectedItem as partners)?.name_partner,
                    fio_partner = TBlockFIOParnerOrder.Text,
                    contract = CheckBoxContract.IsChecked ?? false,
                    sum_sale = carPrice * quantity,
                    discount = discount,
                    sum_sale_discount = (carPrice * quantity) * (1 - discount),
                    date_order = DateTime.Now,
                };

                App.Context.order.Add(newOrder);
                App.Context.SaveChanges();
                MessageBox.Show("Заявка успешно добавлена!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);

                CreateOrderPdfSimple(newOrder);
                NavigationService.Navigate(new ManagerPage());

                ComboBoxBrendAvto.SelectedItem = null;
                ComboBoxModelOrder.SelectedItem = null;
                ComboBoxConfigOrder.SelectedItem = null;
                ComboBoxColorOrder.SelectedItem = null;
                TBoxQuantityOrder.Text = " ";
                CheckBoxContract.IsChecked = false;
                CalculateTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении заявки: {ex.Message}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnUpdateOrder_Click(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        public void UpdateData()
        {
            List<partners> partners = App.Context.partners.ToList();
            ComboBoxPartner.ItemsSource = partners;
        }

        private decimal CalculateDiscount(int quantity)
        {
            if (quantity > 30) return 0.20m;
            if (quantity > 25) return 0.15m;
            if (quantity > 15) return 0.10m;
            if (quantity > 5) return 0.05m;
            return 0;
        }

        private void CalculateTotal()
        {
            if (string.IsNullOrEmpty(TBoxQuantityOrder.Text) ||
                ComboBoxBrendAvto.SelectedItem == null ||
                ComboBoxModelOrder.SelectedItem == null ||
                ComboBoxConfigOrder.SelectedItem == null ||
                ComboBoxColorOrder.SelectedItem == null)
            {
                TBlockTotalSum.Text = "Общая сумма: 0";
                TBlockDiscount.Text = "Скидка: 0%";
                TBlockTotalSumDiscount.Text = "Сумма со скидкой: 0";
                return;
            }

            if (!int.TryParse(TBoxQuantityOrder.Text, out int quantity))
            {
                TBlockTotalSum.Text = "Общая сумма: 0";
                TBlockDiscount.Text = "Скидка: 0%";
                TBlockTotalSumDiscount.Text = "Сумма со скидкой: 0";
                return;
            }

            string selectedBrend = ComboBoxBrendAvto.SelectedItem?.ToString();
            string selectedModel = ComboBoxModelOrder.SelectedItem?.ToString();
            string selectedConfig = ComboBoxConfigOrder.SelectedItem?.ToString();

            Car selectedCar = carList.FirstOrDefault(c => c.name_brend == selectedBrend &&
                                                        c.name_model == selectedModel &&
                                                        c.name_configuration == selectedConfig);

            if (selectedCar != null)
            {
                carPrice = selectedCar.sum_sale;
            }
            else
            {
                carPrice = 0;
            }

            decimal discount = CalculateDiscount(quantity);
            decimal totalSum = carPrice * quantity;
            decimal totalSumDiscount = totalSum * (1 - discount);

            TBlockTotalSum.Text = $"Общая сумма: {totalSum:C}";
            TBlockDiscount.Text = $"Скидка: {discount:P0}";
            TBlockTotalSumDiscount.Text = $"Сумма со скидкой: {totalSumDiscount:C}";
        }

        private void UpdateModelOptions()
        {
            string selectedBrend = ComboBoxBrendAvto.SelectedItem?.ToString();
            if (selectedBrend == null)
            {
                ComboBoxModelOrder.ItemsSource = null;
                return;
            }

            List<string> modelList = carList.Where(c => c.name_brend == selectedBrend)
                                          .Select(c => c.name_model)
                                          .Distinct()
                                          .ToList();
            ComboBoxModelOrder.ItemsSource = modelList;
        }

        private void UpdateConfigurationOptions()
        {
            string selectedBrend = ComboBoxBrendAvto.SelectedItem?.ToString();
            string selectedModel = ComboBoxModelOrder.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedBrend) || string.IsNullOrEmpty(selectedModel))
            {
                ComboBoxConfigOrder.ItemsSource = null;
                return;
            }

            List<string> configList = carList
                .Where(c => c.name_brend == selectedBrend && c.name_model == selectedModel)
                .Select(c => c.name_configuration)
                .Distinct()
                .ToList();

            ComboBoxConfigOrder.ItemsSource = configList;
        }

        private void CreateOrderPdfSimple(order orderData)
        {
            try
            {
                PdfDocument document = new PdfDocument();
                document.Info.Title = "Акт продажи автомобилей";

                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                XFont titleFont = new XFont("Arial", 18);
                XFont headerFont = new XFont("Arial", 14);
                XFont font = new XFont("Arial", 12);
                XFont boldFont = new XFont("Arial", 12);
                XBrush brush = XBrushes.Black;
                double lineHeight = 18;
                double x = 50;

                double y = 50;
                gfx.DrawString("АКТ ПРОДАЖИ", titleFont, brush, new XPoint(x, y));

                y += 30;
                gfx.DrawString($"№: {orderData.num_order}", font, brush, new XPoint(x, y));

                double pageWidth = page.Width.Point;
                double dateTextWidth = gfx.MeasureString($"Дата: {orderData.date_order:dd.MM.yyyy}", font).Width;
                x = pageWidth - dateTextWidth - 50;
                gfx.DrawString($"Дата: {orderData.date_order:dd.MM.yyyy}", font, brush, new XPoint(x, y));
                x = 50;

                y += lineHeight;
                gfx.DrawLine(XPens.LightGray, 50, y, pageWidth - 50, y);
                y += 20;

                gfx.DrawString("Информация об автомобиле:", headerFont, brush, new XPoint(x, y));
                y += lineHeight;

                double column1X = x;
                double column2X = x + 150;

                y += lineHeight;
                gfx.DrawLine(XPens.LightGray, column1X, y, pageWidth - 50, y);

                y += lineHeight;
                gfx.DrawString("Бренд:", font, brush, new XPoint(column1X, y));
                gfx.DrawString(orderData.name_brend, font, brush, new XPoint(column2X, y));

                y += lineHeight;
                gfx.DrawString("Модель:", font, brush, new XPoint(column1X, y));
                gfx.DrawString(orderData.name_model, font, brush, new XPoint(column2X, y));

                y += lineHeight;
                gfx.DrawString("Конфигурация:", font, brush, new XPoint(column1X, y));
                gfx.DrawString(orderData.name_configuration, font, brush, new XPoint(column2X, y));

                y += lineHeight;
                gfx.DrawString("Цвет:", font, brush, new XPoint(column1X, y));
                gfx.DrawString(orderData.name_color, font, brush, new XPoint(column2X, y));
                y += lineHeight;
                gfx.DrawLine(XPens.LightGray, column1X, y, pageWidth - 50, y);

                y += 20;
                gfx.DrawString("Информация о заказе:", headerFont, brush, new XPoint(x, y));
                y += lineHeight;

                y += lineHeight;
                gfx.DrawLine(XPens.LightGray, column1X, y, pageWidth - 50, y);

                y += lineHeight;
                gfx.DrawString("Количество:", font, brush, new XPoint(column1X, y));
                gfx.DrawString(orderData.quantity_avto, font, brush, new XPoint(column2X, y));
                y += lineHeight;
                gfx.DrawString("Общая сумма:", font, brush, new XPoint(column1X, y));
                gfx.DrawString($"{orderData.sum_sale:C}", font, brush, new XPoint(column2X, y));
                y += lineHeight;
                gfx.DrawString("Скидка:", font, brush, new XPoint(column1X, y));
                gfx.DrawString($"{orderData.discount:P0}", font, brush, new XPoint(column2X, y));
                y += lineHeight;
                gfx.DrawString("Сумма со скидкой:", font, brush, new XPoint(column1X, y));
                gfx.DrawString($"{orderData.sum_sale_discount:C}", font, brush, new XPoint(column2X, y));
                y += lineHeight;

                gfx.DrawString("Партнер:", font, brush, new XPoint(column1X, y));
                gfx.DrawString(orderData.name_partner, font, brush, new XPoint(column2X, y));
                y += lineHeight;
                gfx.DrawString("ФИО Партнера:", font, brush, new XPoint(column1X, y));
                gfx.DrawString(orderData.fio_partner, font, brush, new XPoint(column2X, y));
                y += lineHeight;
                gfx.DrawString("Наличие договора:", font, brush, new XPoint(column1X, y));
                gfx.DrawString((orderData.contract ?? false) ? "Да" : "Нет", font, brush, new XPoint(column2X, y));
                y += lineHeight;

                y += 20;
                gfx.DrawLine(XPens.LightGray, 50, y, pageWidth - 50, y);

                y += 20;
                gfx.DrawString("Менеджер: ", boldFont, brush, new XPoint(x, y));
                string managerFio = App.Currentrole?.fio ?? "Неизвестен";
                gfx.DrawString(managerFio, font, brush, new XPoint(x + gfx.MeasureString("Менеджер: ", boldFont).Width, y));
                y += lineHeight;

                y += 20;
                double signatureWidth = 100;
                double signatureLineHeight = 7;
                double signatureTextOffset = 1;

                x = 50;
                gfx.DrawString("Менеджер:", boldFont, brush, new XPoint(x, y));
                double managerLabelWidth = gfx.MeasureString("Менеджер:", boldFont).Width;
                gfx.DrawLine(XPens.Black, x + managerLabelWidth, y + signatureLineHeight, x + managerLabelWidth + signatureWidth, y + signatureLineHeight);
                gfx.DrawString("(Подпись)", font, brush, new XPoint(x + managerLabelWidth, y + lineHeight + signatureTextOffset)); // Под линией

                x += 300;
                gfx.DrawString("Партнер:", boldFont, brush, new XPoint(x, y));
                gfx.DrawLine(XPens.Black, x + gfx.MeasureString("Партнер:", boldFont).Width, y + signatureLineHeight, x + gfx.MeasureString("Партнер:", boldFont).Width + signatureWidth, y + signatureLineHeight);
                gfx.DrawString("(Подпись)", font, brush, new XPoint(x + gfx.MeasureString("Партнер:", boldFont).Width, y + lineHeight + signatureTextOffset)); // Под линией

                string filename = $"Order_{orderData.num_order}_Simple_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), filename);
                document.Save(filePath);

                Process.Start(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании PDF: {ex.Message}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerPage());
        }
    }

    public class Car
    {
        public string name_brend { get; set; }
        public string name_model { get; set; }
        public string name_configuration { get; set; }
        public decimal sum_sale { get; set; }
    }
}