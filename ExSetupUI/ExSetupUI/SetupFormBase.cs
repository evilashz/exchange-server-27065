using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x02000024 RID: 36
	internal partial class SetupFormBase : Form
	{
		// Token: 0x0600018E RID: 398
		[DllImport("user32.dll")]
		public static extern bool ReleaseCapture();

		// Token: 0x0600018F RID: 399
		[DllImport("user32.dll")]
		public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00008D60 File Offset: 0x00006F60
		internal static Image SubstituteImage
		{
			get
			{
				if (SetupFormBase.substituteImage == null)
				{
					SetupFormBase.substituteImage = new Bitmap(32, 32);
					Graphics graphics = Graphics.FromImage(SetupFormBase.substituteImage);
					graphics.FillRectangle(Brushes.Black, new Rectangle(0, 0, 32, 32));
					graphics.Dispose();
				}
				return SetupFormBase.substituteImage;
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00008DB0 File Offset: 0x00006FB0
		static SetupFormBase()
		{
			SetupFormBase.Images = new List<Image>(SetupFormBase.ImagesToLoad.Count);
			string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			foreach (string path in SetupFormBase.ImagesToLoad)
			{
				string text = Path.Combine(directoryName, path);
				if (File.Exists(text))
				{
					SetupFormBase.Images.Add(Image.FromFile(text));
				}
				else
				{
					SetupFormBase.Images.Add(SetupFormBase.SubstituteImage);
				}
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00008EA8 File Offset: 0x000070A8
		protected SetupFormBase()
		{
			this.InitializeComponent();
			for (int i = 0; i < 4; i++)
			{
				this.setupFormBaseImageList.Images.Add(SetupFormBase.Images[i]);
			}
			this.setupFormBaseImageList.Images.SetKeyName(0, "SetupClose.png");
			this.setupFormBaseImageList.Images.SetKeyName(1, "SetupHelp.png");
			this.setupFormBaseImageList.Images.SetKeyName(2, "SetupPrint.png");
			this.setupFormBaseImageList.Images.SetKeyName(3, "SetupPrint_h.png");
			this.btnCancel.ImageIndex = 0;
			this.btnHelp.ImageIndex = 1;
			this.btnPrint.ImageIndex = 2;
			base.Icon = Icons.EXCHANGE;
			this.btnPrint.Click += this.BtnPrint_Click;
			this.printFont = Control.DefaultFont;
			this.Retry = false;
			this.Exit = false;
			SetupFormBase.LaunchECPUrl = null;
			this.nextButtonTimer.Enabled = false;
			this.checkLoadedTimer.Enabled = false;
			this.EnablePrintButton(false);
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00008FDA File Offset: 0x000071DA
		public IList<SetupWizardPage> Pages
		{
			get
			{
				return this.pages;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00008FE2 File Offset: 0x000071E2
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00008FEA File Offset: 0x000071EA
		public IHelpUrlGenerator HelpUrlGenerator { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00008FF3 File Offset: 0x000071F3
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00008FFB File Offset: 0x000071FB
		public string DocumentToPrint
		{
			get
			{
				return this.documentToPrint;
			}
			set
			{
				this.documentToPrint = value;
				if (!string.IsNullOrEmpty(value))
				{
					this.streamToPrint = new StreamReader(this.documentToPrint);
				}
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000901D File Offset: 0x0000721D
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00009024 File Offset: 0x00007224
		internal static string LaunchECPUrl { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0000902C File Offset: 0x0000722C
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00009034 File Offset: 0x00007234
		internal bool Retry { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000903D File Offset: 0x0000723D
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00009045 File Offset: 0x00007245
		internal bool Exit { get; set; }

		// Token: 0x0600019E RID: 414 RVA: 0x00009050 File Offset: 0x00007250
		public void SetActivePage(int pageIndex)
		{
			if (pageIndex < 0 || pageIndex >= this.pages.Count)
			{
				throw new ArgumentOutOfRangeException("pageIndex");
			}
			SetupWizardPage setupWizardPage = this.pages[pageIndex];
			this.SetActivePage(setupWizardPage);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00009090 File Offset: 0x00007290
		internal static void ShowHelpFromUrl(string helpUrl)
		{
			if (!string.IsNullOrEmpty(helpUrl))
			{
				bool flag = false;
				try
				{
					Process.Start(helpUrl);
				}
				catch (Win32Exception)
				{
					flag = true;
				}
				catch (FileNotFoundException)
				{
					flag = true;
				}
				if (flag)
				{
					MessageBoxHelper.ShowError(SetupFormBase.browserLaunchErrorMessage);
				}
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000090E4 File Offset: 0x000072E4
		internal void SetWizardButtons(WizardButtons buttons)
		{
			this.btnPrevious.Enabled = ((buttons & WizardButtons.Previous) != WizardButtons.None);
			this.btnNext.Enabled = ((buttons & WizardButtons.Next) != WizardButtons.None);
			base.AcceptButton = this.btnNext;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000911A File Offset: 0x0000731A
		internal void SetVisibleWizardButtons(WizardButtons buttons)
		{
			this.btnPrevious.Visible = ((buttons & WizardButtons.Previous) != WizardButtons.None);
			this.btnNext.Visible = ((buttons & WizardButtons.Next) != WizardButtons.None);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00009144 File Offset: 0x00007344
		internal void SetTitle(string title)
		{
			this.pageTitle.Text = title;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00009152 File Offset: 0x00007352
		internal void SetExchangeServerLabel(string text)
		{
			this.exchangeServerLabel.Text = text.ToUpper();
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00009165 File Offset: 0x00007365
		internal void SetCancelMessageBoxMessage(string text)
		{
			this.cancelMessageBoxMessage = text;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000916E File Offset: 0x0000736E
		internal void EnableCancelButton(bool enableCancelButton)
		{
			this.btnCancel.Enabled = enableCancelButton;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000917C File Offset: 0x0000737C
		internal void SetPrintErrorMessageBoxMessage(string text)
		{
			this.printErrorMessageBoxMessage = text;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00009185 File Offset: 0x00007385
		internal void SetBrowserLaunchErrorMessage(string text)
		{
			SetupFormBase.browserLaunchErrorMessage = text;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000918D File Offset: 0x0000738D
		internal void EnablePrintButton(bool enablePrintButton)
		{
			this.btnPrint.Visible = enablePrintButton;
			this.btnPrint.Enabled = enablePrintButton;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000091A7 File Offset: 0x000073A7
		internal void PressButton(WizardButtons buttons)
		{
			if ((buttons & WizardButtons.Next) == WizardButtons.Next)
			{
				this.btnNext.PerformClick();
				return;
			}
			if ((buttons & WizardButtons.Previous) == WizardButtons.Previous)
			{
				this.btnPrevious.PerformClick();
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000091CC File Offset: 0x000073CC
		internal void EnableNextButtonTimer(int interval)
		{
			this.nextButtonTimer.Interval = interval;
			this.nextButtonTimer.Enabled = true;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000091E6 File Offset: 0x000073E6
		internal void DisableNextButtonTimer()
		{
			if (this.nextButtonTimer.Enabled)
			{
				this.nextButtonTimer.Enabled = false;
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00009201 File Offset: 0x00007401
		internal void EnableCheckLoadedTimer(int interval)
		{
			this.checkLoadedTimer.Interval = interval;
			this.checkLoadedTimer.Enabled = true;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000921B File Offset: 0x0000741B
		internal void DisableCheckLoadedTimer()
		{
			if (this.checkLoadedTimer.Enabled)
			{
				this.checkLoadedTimer.Enabled = false;
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00009238 File Offset: 0x00007438
		internal SetupWizardPage FindPage(string pageName)
		{
			foreach (SetupWizardPage setupWizardPage in this.pages)
			{
				if (setupWizardPage.Name == pageName)
				{
					return setupWizardPage;
				}
			}
			return null;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00009294 File Offset: 0x00007494
		internal void CheckLoadedTimer_Tick(object sender, EventArgs e)
		{
			this.activePage.OnCheckLoaded(new CancelEventArgs());
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000092A8 File Offset: 0x000074A8
		internal void SetActivePage(SetupWizardPage newPage)
		{
			if (!this.pagePanel.Controls.Contains(newPage))
			{
				this.pagePanel.Controls.Add(newPage);
			}
			newPage.Visible = true;
			newPage.Dock = DockStyle.Fill;
			this.activePage = newPage;
			CancelEventArgs e = new CancelEventArgs();
			newPage.OnSetActive(e);
			foreach (SetupWizardPage setupWizardPage in this.pages)
			{
				if (setupWizardPage != this.activePage)
				{
					setupWizardPage.Visible = false;
				}
			}
			if (!string.IsNullOrEmpty(this.activePage.Caption))
			{
				this.Text = this.activePage.Caption;
				return;
			}
			this.Text = this.wizardCaption;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00009374 File Offset: 0x00007574
		internal string GetHelpId(string pageName)
		{
			HelpId helpId = (HelpId)Enum.Parse(typeof(HelpId), pageName, true);
			return helpId.ToString();
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000093A4 File Offset: 0x000075A4
		internal void InsertPage(SetupWizardPage pageToInsert, SetupWizardPage pageInsertBefore)
		{
			int num = this.pages.IndexOf(pageInsertBefore);
			if (num < 0)
			{
				throw new Exception(string.Format("Page {0} does not exist.", pageInsertBefore.Name));
			}
			this.pages.Insert(num, pageToInsert);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000093E8 File Offset: 0x000075E8
		internal void RemovePage(SetupWizardPage pageToRemove)
		{
			int num = this.pages.IndexOf(pageToRemove);
			if (num >= 0)
			{
				this.pages.RemoveAt(num);
				if (this.pagePanel.Controls.Contains(pageToRemove))
				{
					this.pagePanel.Controls.Remove(pageToRemove);
				}
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00009455 File Offset: 0x00007655
		private void SetupFormBase_Load(object sender, EventArgs e)
		{
			this.wizardCaption = this.Text;
			if (this.pages.Count != 0)
			{
				this.SetActivePage(0);
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00009478 File Offset: 0x00007678
		private void SetActivePage(string newPageName)
		{
			SetupWizardPage setupWizardPage = this.FindPage(newPageName);
			if (setupWizardPage == null)
			{
				throw new Exception(string.Format("Can't find page named {0}", newPageName));
			}
			this.SetActivePage(setupWizardPage);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000094A8 File Offset: 0x000076A8
		private int GetActiveIndex()
		{
			for (int i = 0; i < this.pages.Count; i++)
			{
				if (this.activePage == this.pages[i])
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000094E4 File Offset: 0x000076E4
		private WizardPageEventArgs PreChangePage(int delta)
		{
			int activeIndex = this.GetActiveIndex();
			int num = activeIndex + delta;
			if (num < 0 || num >= this.pages.Count)
			{
				num = activeIndex;
			}
			SetupWizardPage setupWizardPage = this.pages[num];
			return new WizardPageEventArgs
			{
				Page = setupWizardPage.Name,
				Cancel = false
			};
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000953D File Offset: 0x0000773D
		private void PostChangePage(WizardPageEventArgs e)
		{
			if (!e.Cancel)
			{
				this.SetActivePage(e.Page);
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00009553 File Offset: 0x00007753
		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.DisableNextButtonTimer();
			this.DisableCheckLoadedTimer();
			this.ValidateUserEntry();
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00009568 File Offset: 0x00007768
		private void ValidateUserEntry()
		{
			DialogResult dialogResult = MessageBoxHelper.ShowCancel(this, this.cancelMessageBoxMessage);
			if (dialogResult == DialogResult.Yes)
			{
				CancelEventArgs e = new CancelEventArgs();
				this.activePage.OnWizardCancel(e);
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00009598 File Offset: 0x00007798
		internal void SetBtnNextText(string btnNextText)
		{
			this.btnNext.Text = btnNextText.ToLower();
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000095AB File Offset: 0x000077AB
		internal void SetBtnPreviousText(string btnPreviousText)
		{
			this.btnPrevious.Text = btnPreviousText.ToLower();
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000095C0 File Offset: 0x000077C0
		private void BtnNext_Click(object sender, EventArgs e)
		{
			if (this.Exit)
			{
				CancelEventArgs e2 = new CancelEventArgs();
				this.activePage.OnWizardFailed(e2);
				return;
			}
			this.DoBtnNextClick(this.Retry);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000095F4 File Offset: 0x000077F4
		internal void DoBtnNextClick(bool isRetry)
		{
			this.DisableNextButtonTimer();
			this.DisableCheckLoadedTimer();
			this.EnablePrintButton(false);
			int activeIndex = this.GetActiveIndex();
			if (activeIndex == this.pages.Count - 1)
			{
				base.Close();
				return;
			}
			if (isRetry)
			{
				CancelEventArgs e = new CancelEventArgs();
				this.activePage.OnWizardRetry(e);
				return;
			}
			if (this.ValidateChildren())
			{
				WizardPageEventArgs e2 = this.PreChangePage(1);
				this.activePage.OnWizardNext(e2);
				this.PostChangePage(e2);
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000966C File Offset: 0x0000786C
		private void BtnPrevious_Click(object sender, EventArgs e)
		{
			this.DisableNextButtonTimer();
			this.DisableCheckLoadedTimer();
			this.EnablePrintButton(false);
			this.Retry = false;
			this.Exit = false;
			if (this.ValidateChildren())
			{
				WizardPageEventArgs e2 = this.PreChangePage(-1);
				this.activePage.OnWizardPrevious(e2);
				this.PostChangePage(e2);
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000096BD File Offset: 0x000078BD
		private void BtnHelp_Click(object sender, EventArgs e)
		{
			this.ShowHelp();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000096C8 File Offset: 0x000078C8
		private void ShowHelp()
		{
			if (this.HelpUrlGenerator == null)
			{
				throw new Exception("HelpUrlGenerator object is not initialized.");
			}
			try
			{
				string helpUrl = this.HelpUrlGenerator.GetHelpUrl(this.GetHelpId(this.activePage.Name));
				SetupFormBase.ShowHelpFromUrl(helpUrl);
			}
			catch (ArgumentException)
			{
				MessageBox.Show(string.Format("{0} is not an value of the HelpId enumeration.", this.activePage.Name));
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000973C File Offset: 0x0000793C
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (keyData == Keys.Escape)
			{
				this.ValidateUserEntry();
				return true;
			}
			if (keyData == Keys.F1)
			{
				this.ShowHelp();
				return true;
			}
			return base.ProcessDialogKey(keyData);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000976C File Offset: 0x0000796C
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (this.btnPrint.Visible && keyData == (Keys)262224)
			{
				this.DoPrint();
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00009794 File Offset: 0x00007994
		private void SetupFormBase_Closing(object sender, CancelEventArgs e)
		{
			int activeIndex = this.GetActiveIndex();
			if (!this.btnCancel.Enabled && activeIndex != this.pages.Count - 1)
			{
				e.Cancel = true;
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000097CC File Offset: 0x000079CC
		private void SetupFormBase_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (!string.IsNullOrEmpty(SetupFormBase.LaunchECPUrl))
			{
				SetupFormBase.ShowHelpFromUrl(SetupFormBase.LaunchECPUrl);
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000097E4 File Offset: 0x000079E4
		private void NextButtonTimer_Tick(object sender, EventArgs e)
		{
			this.nextButtonTimer.Enabled = false;
			this.PressButton(WizardButtons.Next);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000097F9 File Offset: 0x000079F9
		private void SetupFormBase_MouseDown(object sender, MouseEventArgs e)
		{
			SetupFormBase.ReleaseCapture();
			SetupFormBase.SendMessage(base.Handle, 274, 61458, 0);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00009818 File Offset: 0x00007A18
		private void BtnPrint_Click(object sender, EventArgs e)
		{
			this.DoPrint();
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00009820 File Offset: 0x00007A20
		private void DoPrint()
		{
			try
			{
				this.printDialog.Document = this.printDocument;
				this.printDocument.PrintPage += this.PrintDocument_PrintPage;
				DialogResult dialogResult = this.printDialog.ShowDialog();
				if (dialogResult == DialogResult.OK)
				{
					this.printDocument.Print();
				}
			}
			catch (Exception)
			{
				MessageBoxHelper.ShowError(this.printErrorMessageBoxMessage);
			}
			finally
			{
				if (this.streamToPrint != null)
				{
					this.streamToPrint.Close();
				}
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000098B4 File Offset: 0x00007AB4
		private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
		{
			int num = 0;
			float x = (float)e.MarginBounds.Left;
			float num2 = (float)e.MarginBounds.Top;
			int num3 = (int)this.printFont.GetHeight(e.Graphics);
			float num4 = (float)(e.MarginBounds.Height / num3);
			while ((float)num < num4 && !this.streamToPrint.EndOfStream)
			{
				string s = this.streamToPrint.ReadLine();
				float y = num2 + (float)(num * num3);
				e.Graphics.DrawString(s, this.printFont, Brushes.Black, x, y, new StringFormat());
				num++;
			}
			if (!this.streamToPrint.EndOfStream)
			{
				e.HasMorePages = true;
				return;
			}
			e.HasMorePages = false;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00009988 File Offset: 0x00007B88
		private void PreviousButtonEnabledChanged(object sender, EventArgs e)
		{
			this.btnPrevious.ForeColor = (this.btnPrevious.Enabled ? Color.FromArgb(102, 102, 102) : Color.FromArgb(221, 221, 221));
			this.btnPrevious.FlatAppearance.BorderColor = this.btnPrevious.ForeColor;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000099EC File Offset: 0x00007BEC
		private void NextButtonEnabledChanged(object sender, EventArgs e)
		{
			this.btnNext.ForeColor = (this.btnNext.Enabled ? Color.FromArgb(102, 102, 102) : Color.FromArgb(221, 221, 221));
			this.btnNext.FlatAppearance.BorderColor = this.btnNext.ForeColor;
		}

		// Token: 0x040000D5 RID: 213
		private const int WM_SYSCOMMAND = 274;

		// Token: 0x040000D6 RID: 214
		private const int SC_MOVE = 61456;

		// Token: 0x040000D7 RID: 215
		private const int HTCAPTION = 2;

		// Token: 0x040000D8 RID: 216
		private readonly IList<SetupWizardPage> pages = new List<SetupWizardPage>();

		// Token: 0x040000D9 RID: 217
		private static string browserLaunchErrorMessage;

		// Token: 0x040000DA RID: 218
		private static Image substituteImage;

		// Token: 0x040000E6 RID: 230
		private SetupWizardPage activePage;

		// Token: 0x040000E7 RID: 231
		private string wizardCaption = string.Empty;

		// Token: 0x040000E8 RID: 232
		private string cancelMessageBoxMessage;

		// Token: 0x040000E9 RID: 233
		private string printErrorMessageBoxMessage;

		// Token: 0x040000F0 RID: 240
		private Font printFont;

		// Token: 0x040000F2 RID: 242
		private StreamReader streamToPrint;

		// Token: 0x040000F3 RID: 243
		private string documentToPrint;

		// Token: 0x040000F8 RID: 248
		private static readonly IList<string> ImagesToLoad = new List<string>
		{
			"res\\SetupClose.png",
			"res\\SetupHelp.png",
			"res\\SetupPrint.png",
			"res\\SetupPrint_h.png",
			"res\\SetupError.png",
			"res\\SetupWarning.png",
			"res\\ExchangeLogo.png"
		};
	}
}
