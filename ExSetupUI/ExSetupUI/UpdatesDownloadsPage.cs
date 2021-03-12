using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Exchange.CabUtility;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Setup.AcquireLanguagePack;
using Microsoft.Exchange.Setup.Common;
using Microsoft.Exchange.Setup.CommonBase;
using Microsoft.Win32;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x02000029 RID: 41
	internal class UpdatesDownloadsPage : ProgressPageBase
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x0000B0D8 File Offset: 0x000092D8
		public UpdatesDownloadsPage(SetupBase theApp)
		{
			this.webFileDownloader = new WebFileDownloader();
			this.webFileDownloader.DownloadProgressEvent += this.WebFileDownloader_ProgressChangedEvent;
			this.webFileDownloader.DownloadCompletedEvent += this.WebFileDownloader_DownloadCompletedEvent;
			this.webFileDownloader.DownloaderErrorEvent += new DownloaderErrorHandler(this.UpdateDownloadUpdatesStatusBox);
			this.webFileDownloader.DownloadCancelEvent += this.WebFileDownloader_DownloadCancelEvent;
			this.InitializeComponent();
			base.Name = "UpdatesDownloadsPage";
			base.SetActive += this.UpdatesDownloadsPage_SetActive;
			base.CheckLoaded += this.UpdatesDownloadsPage_CheckLoaded;
			base.WizardCancel += this.UpdatesDownloadsPage_WizardCancel;
			this.UnableContactServerTextBox.LinkClicked += this.UnableContactServerTextBox_LinkClicked;
			base.Visible = base.PageVisible;
			this.UnableContactServerTextBox.Visible = false;
			this.sourceDir = theApp.ParsedArguments["sourcedir"].ToString();
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000B23B File Offset: 0x0000943B
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x0000B243 File Offset: 0x00009443
		internal bool UsePreviousDownloadedUpdates { get; set; }

		// Token: 0x060001E4 RID: 484 RVA: 0x0000B24C File Offset: 0x0000944C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000B26C File Offset: 0x0000946C
		private void UpdatesDownloadsPage_SetActive(object sender, CancelEventArgs e)
		{
			if (base.PageVisible)
			{
				base.SetWizardButtons(WizardButtons.None);
				base.SetVisibleWizardButtons(WizardButtons.Next);
				this.customProgressBarWithTitle.TitleOnly = false;
				this.customProgressBarWithTitle.Title = string.Empty;
				this.UnableContactServerTextBox.Visible = false;
				if (this.UsePreviousDownloadedUpdates)
				{
					base.PageTitle = Strings.ApplyingUpdatesPageTitle;
					this.customProgressBarWithTitle.Visible = false;
				}
				else
				{
					base.PageTitle = Strings.UpdatesDownloadsPageTitle;
					this.customProgressBarWithTitle.Visible = true;
				}
				base.SetPageTitle(base.PageTitle);
				base.EnableCancelButton(true);
				if (!this.UsePreviousDownloadedUpdates)
				{
					this.webThread = new Thread(new ThreadStart(this.StartDownloading));
					this.webThread.Start();
				}
				else
				{
					this.alreadyDownloadedThread = new Thread(new ParameterizedThreadStart(this.VerifyAndSetRegistry));
					this.alreadyDownloadedThread.Start(this.saveToDirectory);
				}
				base.EnableCheckLoadedTimer(200);
				return;
			}
			if (base.ParentPageName == "CheckForUpdatesPage")
			{
				base.PressButton(WizardButtons.Next);
				return;
			}
			base.PressButton(WizardButtons.Previous);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000B390 File Offset: 0x00009590
		private void UpdatesDownloadsPage_CheckLoaded(object sender, CancelEventArgs e)
		{
			Control[] array = base.Controls.Find(this.customProgressBarWithTitle.Name, true);
			if (array.Length > 0)
			{
				this.OnSetLoaded(new CancelEventArgs());
				SetupLogger.Log(Strings.PageLoaded(base.Name));
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000B3D6 File Offset: 0x000095D6
		private void UpdatesDownloadsPage_WizardCancel(object sender, CancelEventArgs e)
		{
			this.CancelDownload();
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000B3DE File Offset: 0x000095DE
		private void UnableContactServerTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			SetupFormBase.ShowHelpFromUrl(e.LinkText);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000B3EC File Offset: 0x000095EC
		private void InitializeComponent()
		{
			this.components = new Container();
			this.UnableContactServerTextBox = new RichTextBox();
			base.SuspendLayout();
			this.customProgressBarWithTitle.Location = new Point(0, 20);
			this.customProgressBarWithTitle.Size = new Size(721, 76);
			this.UnableContactServerTextBox.BackColor = SystemColors.Window;
			this.UnableContactServerTextBox.BorderStyle = BorderStyle.None;
			this.UnableContactServerTextBox.ForeColor = Color.FromArgb(51, 51, 51);
			this.UnableContactServerTextBox.Location = new Point(0, 20);
			this.UnableContactServerTextBox.Name = "UnableContactServerTextBox";
			this.UnableContactServerTextBox.ReadOnly = true;
			this.UnableContactServerTextBox.ScrollBars = RichTextBoxScrollBars.None;
			this.UnableContactServerTextBox.Size = new Size(721, 44);
			this.UnableContactServerTextBox.TabIndex = 30;
			this.UnableContactServerTextBox.Text = "[UnableContactServerText]";
			base.Controls.Add(this.UnableContactServerTextBox);
			base.Name = "UpdatesDownloadsPage";
			base.Controls.SetChildIndex(this.UnableContactServerTextBox, 0);
			base.Controls.SetChildIndex(this.customProgressBarWithTitle, 0);
			base.ResumeLayout(false);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000B528 File Offset: 0x00009728
		public void StartDownloading()
		{
			try
			{
				using (DiskSpaceValidator diskSpaceValidator = new DiskSpaceValidator(500000000L, Environment.GetEnvironmentVariable("windir"), new Action<object>(this.UpdateDownloadUpdatesStatusBox)))
				{
					if (!diskSpaceValidator.Validate())
					{
						throw new LanguagePackBundleLoadException(Strings.NotEnoughDiskSpace);
					}
				}
				List<DownloadFileInfo> downloadFileInfoFromXml = LanguagePackXmlHelper.GetDownloadFileInfoFromXml(this.localXMLVersioningPath, false);
				if (downloadFileInfoFromXml != null && downloadFileInfoFromXml.Count > 0)
				{
					if (Directory.Exists(this.downloadToDirectory))
					{
						Directory.Delete(this.downloadToDirectory, true);
					}
					Directory.CreateDirectory(this.downloadToDirectory);
					this.webFileDownloader.StartDownloading(downloadFileInfoFromXml, this.downloadToDirectory);
				}
				else
				{
					this.UpdateDownloadUpdatesStatusBox(new LanguagePackBundleLoadException(Strings.InvalidLocalLPVersioningXML(this.localXMLVersioningPath)));
				}
			}
			catch (LanguagePackBundleLoadException message)
			{
				this.UpdateDownloadUpdatesStatusBox(message);
			}
			catch (LPVersioningValueException message2)
			{
				this.UpdateDownloadUpdatesStatusBox(message2);
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000B628 File Offset: 0x00009828
		internal void UpdateDownloadUpdatesStatusBox(object message)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new UpdatesDownloadsPage.UpdateStatusBox(this.UpdateDownloadUpdatesStatusBox), new object[]
				{
					message
				});
				return;
			}
			if (!this.userCancelled)
			{
				if (message is ILocalizedString)
				{
					string text = message.ToString();
					Logger.LoggerMessage(text);
					this.customProgressBarWithTitle.Title = text;
					return;
				}
				if (message is CabUtilityWrapperException)
				{
					CabUtilityWrapperException ex = (CabUtilityWrapperException)message;
					string text = string.Concat(new object[]
					{
						ex.Message,
						" Code: ",
						ex.errorCode,
						" Type: ",
						ex.errorType,
						" Cause: ",
						ex.errorMessage,
						" Source: ",
						ex.fileName,
						" at Line Number ",
						ex.lineNumber
					});
					Logger.LoggerMessage(text);
					text = Strings.InvalidLanguageBundle.ToString();
					this.customProgressBarWithTitle.Title = text;
					base.SetWizardButtons(WizardButtons.Next);
					base.SetVisibleWizardButtons(WizardButtons.Next);
					return;
				}
				if (message is Exception)
				{
					Exception ex2 = (Exception)message;
					if (ex2 is WebException)
					{
						this.customProgressBarWithTitle.Visible = false;
						this.UnableContactServerTextBox.Visible = true;
						string text = Strings.NotFound.ToString();
						Logger.LoggerMessage(text);
						this.UnableContactServerTextBox.Text = text;
						this.ChangeRichTextBoxHeight();
						if (this.webFileDownloader != null)
						{
							this.webFileDownloader.StopDownloading();
						}
						base.SetWizardButtons(WizardButtons.Next);
						base.SetVisibleWizardButtons(WizardButtons.Next);
					}
					else
					{
						this.customProgressBarWithTitle.Visible = true;
						this.UnableContactServerTextBox.Visible = false;
						string text = ex2.Message;
						this.customProgressBarWithTitle.Title = text;
						base.SetVisibleWizardButtons(WizardButtons.Previous | WizardButtons.Next);
						base.SetWizardButtons(WizardButtons.Previous);
					}
					Logger.LogError(ex2);
				}
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000B820 File Offset: 0x00009A20
		private void ChangeRichTextBoxHeight()
		{
			Graphics graphics = this.UnableContactServerTextBox.CreateGraphics();
			SizeF sizeF = graphics.MeasureString(this.UnableContactServerTextBox.Text, this.UnableContactServerTextBox.Font, this.UnableContactServerTextBox.ClientSize.Width);
			this.UnableContactServerTextBox.Height = Convert.ToInt32(sizeF.Height + 1f);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000B886 File Offset: 0x00009A86
		private void CancelDownload()
		{
			this.userCancelled = true;
			this.UpdateDownloadUpdatesStatusBox(Strings.CancellingDownload);
			if (this.webFileDownloader != null)
			{
				this.webFileDownloader.StopDownloading();
			}
			ExSetupUI.ExitApplication(ExitCode.Success);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000B8B8 File Offset: 0x00009AB8
		private void WebFileDownloader_ProgressChangedEvent()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new DownloadProgressChangeHandler(this.WebFileDownloader_ProgressChangedEvent));
				return;
			}
			if (!this.downloadControlsSet)
			{
				this.UpdateDownloadUpdatesStatusBox(Strings.DownloadStarted);
				this.downloadControlsSet = true;
				if (!this.UsePreviousDownloadedUpdates)
				{
					this.customProgressBarWithTitle.Visible = true;
				}
				base.SetWizardButtons(WizardButtons.None);
			}
			if (this.webFileDownloader.PercentageDownloaded == 100)
			{
				this.UpdateDownloadUpdatesStatusBox(Strings.ClosingHTTPConnection);
			}
			if (!this.UsePreviousDownloadedUpdates)
			{
				this.customProgressBarWithTitle.Value = this.webFileDownloader.PercentageDownloaded;
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000B958 File Offset: 0x00009B58
		private void WebFileDownloader_DownloadCancelEvent()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new DownloadCanceledHandler(this.WebFileDownloader_DownloadCancelEvent));
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000B978 File Offset: 0x00009B78
		private void WebFileDownloader_DownloadCompletedEvent(object validDownloads)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new DownloadCompletedHandler(this.WebFileDownloader_DownloadCompletedEvent), new object[]
				{
					validDownloads
				});
				return;
			}
			if ((int)validDownloads == 0)
			{
				this.customProgressBarWithTitle.TitleOnly = true;
				this.UpdateDownloadUpdatesStatusBox(Strings.DownloadNoUpdatesFound);
				base.SetWizardButtons(WizardButtons.Next);
				base.SetVisibleWizardButtons(WizardButtons.Next);
				return;
			}
			this.customProgressBarWithTitle.Value = 100;
			this.UpdateDownloadUpdatesStatusBox(Strings.DownloadCompleted);
			this.VerifyAndSetRegistry(this.downloadToDirectory);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000BA34 File Offset: 0x00009C34
		private void VerifyAndSetRegistry(object checkPath)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new UpdatesDownloadsPage.VerifyAndSetRegistryDelegate(this.VerifyAndSetRegistry), new object[]
				{
					checkPath
				});
				return;
			}
			string text = (string)checkPath;
			string languagePackBundleFileName = Path.Combine(text, "LanguagePackBundle.exe");
			bool flag = true;
			string value = string.Empty;
			if (File.Exists(languagePackBundleFileName))
			{
				using (LanguagePackValidator languagePackValidator = new LanguagePackValidator(languagePackBundleFileName, this.localXMLVersioningPath, new Action<object>(this.UpdateDownloadUpdatesStatusBox)))
				{
					flag = languagePackValidator.Validate();
					if (flag)
					{
						value = languagePackValidator.ValidatedFiles.FirstOrDefault((string x) => x.Equals(languagePackBundleFileName, StringComparison.InvariantCultureIgnoreCase));
					}
					goto IL_C2;
				}
			}
			languagePackBundleFileName = null;
			IL_C2:
			bool flag2 = true;
			string text2 = string.Empty;
			if (Directory.Exists(text))
			{
				string[] files = Directory.GetFiles(text, "*.msp", SearchOption.TopDirectoryOnly);
				if (files.Length != 0)
				{
					using (MspValidator mspValidator = new MspValidator(files, Path.Combine(this.sourceDir, "EXCHANGESERVER.msi"), languagePackBundleFileName, File.Exists(this.localXMLVersioningPath) ? this.localXMLVersioningPath : null, new Action<object>(this.UpdateDownloadUpdatesStatusBox)))
					{
						flag2 = mspValidator.Validate();
						if (flag2)
						{
							text2 = mspValidator.ValidatedFiles.LastOrDefault((string x) => !string.IsNullOrEmpty(x) && MsiHelper.IsMspFileExtension(x));
						}
					}
				}
			}
			bool flag3 = !string.IsNullOrEmpty(value);
			bool flag4 = !string.IsNullOrEmpty(text2);
			bool flag5 = false;
			if (flag && flag2)
			{
				flag5 = (text.Equals(this.saveToDirectory, StringComparison.InvariantCultureIgnoreCase) || this.MoveToSaveToPath());
			}
			bool flag6 = false;
			if (flag5)
			{
				flag6 = this.SetRegistryKeyForBundle();
			}
			if (flag6)
			{
				bool flag7 = false;
				string text3 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Temp\\ExchangeSetup\\MspTemp");
				if (flag4)
				{
					MspUtility.UnpackMspCabs(Path.Combine(this.saveToDirectory, Path.GetFileName(text2)), text3);
					flag7 = SetupLauncherHelper.SetupRequiredFilesUpdated(SetupChecksFileConstant.GetSetupRequiredFiles(), SetupHelper.GetSetupRequiredFilesFromAssembly(text3), text3);
				}
				if (flag7 || flag3)
				{
					ExSetupUI.ExitApplication(ExitCode.Restart);
				}
				else if (flag4)
				{
					SetupLauncherHelper.CopyMspFiles(text3, Path.Combine(SetupHelper.WindowsDir, "Temp\\ExchangeSetup"));
				}
				this.UpdateDownloadUpdatesStatusBox(Strings.DownloadInstallationCompleted);
				base.SetWizardButtons(WizardButtons.Next);
				base.SetVisibleWizardButtons(WizardButtons.Next);
				return;
			}
			this.customProgressBarWithTitle.TitleOnly = true;
			this.UpdateDownloadUpdatesStatusBox(Strings.FinishedWithError(new LocalizedString(Logger.PathToFileLog)));
			base.SetVisibleWizardButtons(WizardButtons.Previous | WizardButtons.Next);
			base.SetWizardButtons(WizardButtons.Previous);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000BCF0 File Offset: 0x00009EF0
		private bool SetRegistryKeyForBundle()
		{
			bool result;
			try
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(SetupChecksRegistryConstant.RegistryPathForLanguagePack, true);
				if (registryKey == null)
				{
					registryKey = Registry.LocalMachine.CreateSubKey(SetupChecksRegistryConstant.RegistryPathForLanguagePack);
				}
				if (registryKey != null)
				{
					registryKey.SetValue("LanguagePackBundlePath", this.saveToDirectory);
					registryKey.SetValue("LPForExSetupUI", this.saveToDirectory);
					registryKey.Close();
					result = true;
				}
				else
				{
					this.UpdateDownloadUpdatesStatusBox(new LanguagePackBundleLoadException(Strings.ErrorCreatingRegKey));
					result = false;
				}
			}
			catch (UnauthorizedAccessException message)
			{
				this.UpdateDownloadUpdatesStatusBox(message);
				result = false;
			}
			catch (IOException message2)
			{
				this.UpdateDownloadUpdatesStatusBox(message2);
				result = false;
			}
			return result;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000BDA0 File Offset: 0x00009FA0
		private bool MoveToSaveToPath()
		{
			if (!Directory.Exists(this.downloadToDirectory))
			{
				this.UpdateDownloadUpdatesStatusBox(Strings.CantFindSourceDir(this.downloadToDirectory));
				return false;
			}
			try
			{
				if (Directory.Exists(this.saveToDirectory))
				{
					Directory.Delete(this.saveToDirectory, true);
				}
				Directory.Move(this.downloadToDirectory, this.saveToDirectory);
			}
			catch (Exception message)
			{
				this.UpdateDownloadUpdatesStatusBox(message);
				return false;
			}
			return true;
		}

		// Token: 0x0400010A RID: 266
		private const string CheckForUpdatesPageName = "CheckForUpdatesPage";

		// Token: 0x0400010B RID: 267
		private const string RegistryKeyForLanguagePack = "LanguagePackBundlePath";

		// Token: 0x0400010C RID: 268
		private const string RegistryKeyLangPackForExSetupUI = "LPForExSetupUI";

		// Token: 0x0400010D RID: 269
		private const string MsiFileName = "EXCHANGESERVER.msi";

		// Token: 0x0400010E RID: 270
		private readonly string sourceDir;

		// Token: 0x0400010F RID: 271
		internal RichTextBox UnableContactServerTextBox;

		// Token: 0x04000110 RID: 272
		private IContainer components;

		// Token: 0x04000111 RID: 273
		private WebFileDownloader webFileDownloader;

		// Token: 0x04000112 RID: 274
		private Thread webThread;

		// Token: 0x04000113 RID: 275
		private Thread alreadyDownloadedThread;

		// Token: 0x04000114 RID: 276
		private readonly string downloadToDirectory = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "ExchangeLanguagePack\\");

		// Token: 0x04000115 RID: 277
		private readonly string saveToDirectory = Path.Combine(Path.GetPathRoot(Environment.GetEnvironmentVariable("windir")), "ExchangeSetupLogs\\ExchangeLanguagePack\\");

		// Token: 0x04000116 RID: 278
		private readonly string localXMLVersioningPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "LPVersioning.xml");

		// Token: 0x04000117 RID: 279
		private bool downloadControlsSet;

		// Token: 0x04000118 RID: 280
		private bool userCancelled;

		// Token: 0x0200002A RID: 42
		// (Invoke) Token: 0x060001F6 RID: 502
		private delegate void UpdateStatusBox(object msg);

		// Token: 0x0200002B RID: 43
		// (Invoke) Token: 0x060001FA RID: 506
		private delegate void LogErrorDelegate(Exception exceptionMessage);

		// Token: 0x0200002C RID: 44
		// (Invoke) Token: 0x060001FE RID: 510
		private delegate void VerifyAndSetRegistryDelegate(string checkPath);
	}
}
