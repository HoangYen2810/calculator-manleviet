using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        bool isTypingNumber = false;
        bool isCalculated = true;
        bool isFloatingPoint = false;

        enum PhepToan { None, Cong, Tru, Nhan, Chia, PhanTram,
                        Can, DoiDau};
        PhepToan pheptoan = PhepToan.None;
        double nho = 0.0;

        private void NhapSo(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            NhapSo(btn.Text);
        }

        private void NhapSo(string so)
        {
            if (isTypingNumber)
            {
                string text = lblDisplay.Text;
                if (!isFloatingPoint)
                    text = LaySoTuDisplayText();
                lblDisplay.Text = text + so;
            }
            else
            {
                lblDisplay.Text = so;
                if (so != "0")
                    isTypingNumber = true;
            }

            
        }

        private void NhapPhepToan(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            NhapPhepToan(btn.Text);
        }

        private void NhapPhepToan(string pt)
        {
            if (!isCalculated)
                TinhKetQua();

            pheptoan = XacDinhPhepToan(pt);

            nho = double.Parse(lblDisplay.Text);

            isTypingNumber = false;
            isCalculated = false;
            isFloatingPoint = false;
        }

        private void NhapPhepToanMotNgoi(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            NhapPhepToanMotNgoi(btn.Text);
        }

        private void NhapPhepToanMotNgoi(string pt)
        {
            pheptoan = XacDinhPhepToan(pt);

            TinhKetQua();

            isTypingNumber = false;
            isFloatingPoint = false;
        }

        private PhepToan XacDinhPhepToan(string pt)
        {
            switch (pt)
            {
                case "+": return PhepToan.Cong;
                case "-": return PhepToan.Tru;
                case "*": return PhepToan.Nhan;
                case "/": return PhepToan.Chia;
                case "%": return PhepToan.PhanTram;
                case "√": return PhepToan.Can;
                case "-/+": return PhepToan.DoiDau;
                default: return PhepToan.None;
            }
        }

        private void TinhKetQua()
        {
            // tinh toan dua tren: nho, pheptoan, lblDisplay.Text
            double tam = double.Parse(LaySoTuDisplayText());
            double ketqua = 0.0;
            switch (pheptoan)
            {
                case PhepToan.Cong: ketqua = nho + tam; break;
                case PhepToan.Tru: ketqua = nho - tam; break;
                case PhepToan.Nhan: ketqua = nho * tam; break;
                case PhepToan.Chia: ketqua = nho / tam; break;
                case PhepToan.PhanTram: ketqua = tam / 100; break;
                case PhepToan.Can: ketqua = Math.Sqrt(tam); break;
                case PhepToan.DoiDau: ketqua = -1 * tam; break;
            }

            // gan ket qua tinh duoc len lblDisplay
            lblDisplay.Text = ketqua.ToString();
            ThemDauCham();
        }

        private void btnBang_Click(object sender, EventArgs e)
        {
            if (!isCalculated)
                TinhKetQua();
            Clear();
        }

        private void Clear()
        {
            isTypingNumber = false;
            nho = 0;
            pheptoan = PhepToan.None;
            isCalculated = true;
            isFloatingPoint = false;
        }

        private void frmMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    NhapSo("" + e.KeyChar);
                    break;
                case '+':
                case '-':
                case '*':
                case '/':
                    NhapPhepToan("" + e.KeyChar);
                    break;
                case '%':
                    NhapPhepToanMotNgoi("" + e.KeyChar);
                    break;
                case '=':
                    btnBang.PerformClick();
                    break;
                default:
                    break;
            }
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                btnBang.PerformClick();
        }

        private void btnNho_Click(object sender, EventArgs e)
        {
            lblDisplay.Text = "0.";
            Clear();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string text = lblDisplay.Text;

            if (text.Length == 1)
                lblDisplay.Text = "0";
            else
                lblDisplay.Text = text.Remove(text.Length - 1);
        }

        private void btnThapPhan_Click(object sender, EventArgs e)
        {
            isFloatingPoint = true;
        }

        private void ThemDauCham()
        {
            if (!isFloatingPoint || !lblDisplay.Text.Contains("."))
                lblDisplay.Text = lblDisplay.Text + ".";
        }

        private string LaySoTuDisplayText()
        {
            string text = lblDisplay.Text;
            int lastIndex = text.Length - 1;

            if (text[lastIndex] == '.')
                text = text.Remove(lastIndex);

            return text;
        }
    }
}
