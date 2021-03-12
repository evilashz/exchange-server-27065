using System;
using System.ComponentModel;
using System.IO;
using System.Management.Automation;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.UpdatableHelp
{
	// Token: 0x02000BFC RID: 3068
	internal class HelpDownloader
	{
		// Token: 0x060073C2 RID: 29634 RVA: 0x001D6EF3 File Offset: 0x001D50F3
		internal HelpDownloader(HelpUpdater updater)
		{
			this.helpUpdater = updater;
		}

		// Token: 0x060073C3 RID: 29635 RVA: 0x001D6F04 File Offset: 0x001D5104
		internal void DownloadManifest()
		{
			string downloadUrl = this.ResolveUri(this.helpUpdater.ManifestUrl);
			if (!this.helpUpdater.Cmdlet.Abort)
			{
				this.AsyncDownloadFile(UpdatableHelpStrings.UpdateComponentManifest, downloadUrl, this.helpUpdater.LocalManifestPath, 30000, new DownloadProgressChangedEventHandler(this.OnManifestProgressChanged), new AsyncCompletedEventHandler(this.OnManifestDownloadCompleted));
			}
		}

		// Token: 0x060073C4 RID: 29636 RVA: 0x001D6F6E File Offset: 0x001D516E
		internal void DownloadPackage(string packageUrl)
		{
			this.AsyncDownloadFile(UpdatableHelpStrings.UpdateComponentCabinet, packageUrl, this.helpUpdater.LocalCabinetPath, 120000, new DownloadProgressChangedEventHandler(this.OnCabinetProgressChanged), new AsyncCompletedEventHandler(this.OnCabinetDownloadCompleted));
		}

		// Token: 0x060073C5 RID: 29637 RVA: 0x001D6FAC File Offset: 0x001D51AC
		internal UpdatableHelpVersionRange SearchManifestForApplicableUpdates(UpdatableHelpVersion currentVersion, int currentRevision)
		{
			StreamReader streamReader = new StreamReader(this.helpUpdater.LocalManifestPath);
			string xml = streamReader.ReadToEnd();
			streamReader.Close();
			HelpSchema helpSchema = new HelpSchema();
			return helpSchema.ParseManifestForApplicableUpdates(xml, currentVersion, currentRevision);
		}

		// Token: 0x060073C6 RID: 29638 RVA: 0x001D6FE8 File Offset: 0x001D51E8
		private void AsyncDownloadFile(string description, string downloadUrl, string localFilePath, int timeoutMilliseconds, DownloadProgressChangedEventHandler progressHandler, AsyncCompletedEventHandler completionHandler)
		{
			LocalizedString value = UpdatableHelpStrings.UpdateDownloadComplete;
			using (WebClient webClient = new WebClient())
			{
				AutoResetEvent autoResetEvent = new AutoResetEvent(false);
				webClient.DownloadProgressChanged += progressHandler;
				webClient.DownloadFileCompleted += completionHandler;
				this.downloadException = null;
				webClient.DownloadFileAsync(new Uri(downloadUrl), localFilePath, autoResetEvent);
				DateTime utcNow = DateTime.UtcNow;
				int num = 0;
				while (!this.helpUpdater.Cmdlet.Abort && num <= timeoutMilliseconds)
				{
					num += 100;
					if (autoResetEvent.WaitOne(100))
					{
						IL_78:
						if (num > timeoutMilliseconds)
						{
							value = UpdatableHelpStrings.UpdateDownloadTimeout;
						}
						TimeSpan timeSpan = DateTime.UtcNow - utcNow;
						string elapsedTime = string.Format("{0}.{1} seconds", timeSpan.TotalSeconds, timeSpan.TotalMilliseconds.ToString().PadLeft(3, '0'));
						this.helpUpdater.Cmdlet.WriteVerbose(UpdatableHelpStrings.UpdateDownloadTimeElapsed(description, value, elapsedTime));
						goto IL_EB;
					}
				}
				value = UpdatableHelpStrings.UpdateDownloadCancelled;
				webClient.CancelAsync();
				goto IL_78;
			}
			IL_EB:
			if (this.downloadException != null)
			{
				throw this.downloadException;
			}
		}

		// Token: 0x060073C7 RID: 29639 RVA: 0x001D7100 File Offset: 0x001D5300
		private void OnManifestProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			this.OnProgressChanged(sender, e, UpdatePhase.Checking, UpdatableHelpStrings.UpdateSubtaskCheckingManifest);
		}

		// Token: 0x060073C8 RID: 29640 RVA: 0x001D7110 File Offset: 0x001D5310
		private void OnCabinetProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			this.OnProgressChanged(sender, e, UpdatePhase.Downloading, LocalizedString.Empty);
		}

		// Token: 0x060073C9 RID: 29641 RVA: 0x001D7120 File Offset: 0x001D5320
		private void OnProgressChanged(object sender, DownloadProgressChangedEventArgs e, UpdatePhase phase, LocalizedString subTask)
		{
		}

		// Token: 0x060073CA RID: 29642 RVA: 0x001D7122 File Offset: 0x001D5322
		private void OnManifestDownloadCompleted(object sender, AsyncCompletedEventArgs e)
		{
			this.OnDownloadCompleted(sender, e, UpdatePhase.Checking, UpdatableHelpStrings.UpdateDownloadManifestFailureErrorID, UpdatableHelpStrings.UpdateDownloadManifestFailure);
		}

		// Token: 0x060073CB RID: 29643 RVA: 0x001D7137 File Offset: 0x001D5337
		private void OnCabinetDownloadCompleted(object sender, AsyncCompletedEventArgs e)
		{
			this.OnDownloadCompleted(sender, e, UpdatePhase.Downloading, UpdatableHelpStrings.UpdateDownloadCabinetFailureErrorID, UpdatableHelpStrings.UpdateDownloadCabinetFailure);
		}

		// Token: 0x060073CC RID: 29644 RVA: 0x001D714C File Offset: 0x001D534C
		private void OnDownloadCompleted(object sender, AsyncCompletedEventArgs e, UpdatePhase phase, LocalizedString errorId, LocalizedString errorMessage)
		{
			if (e.Cancelled)
			{
				this.downloadException = new UpdatableExchangeHelpSystemException(errorId, errorMessage, ErrorCategory.OperationStopped, this, null);
			}
			else if (e.Error != null)
			{
				this.downloadException = new UpdatableExchangeHelpSystemException(errorId, errorMessage, ErrorCategory.ResourceUnavailable, this, e.Error);
			}
			else
			{
				this.downloadException = null;
			}
			AutoResetEvent autoResetEvent = (AutoResetEvent)e.UserState;
			autoResetEvent.Set();
		}

		// Token: 0x060073CD RID: 29645 RVA: 0x001D71B4 File Offset: 0x001D53B4
		private string ResolveUri(string baseUri)
		{
			string text = baseUri;
			try
			{
				for (int i = 0; i < 5; i++)
				{
					if (this.helpUpdater.Cmdlet.Abort)
					{
						return text;
					}
					string text2 = text;
					HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(text2);
					httpWebRequest.AllowAutoRedirect = false;
					httpWebRequest.Timeout = 30000;
					HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
					WebHeaderCollection headers = httpWebResponse.Headers;
					try
					{
						if (httpWebResponse.StatusCode == HttpStatusCode.Found || httpWebResponse.StatusCode == HttpStatusCode.Found || httpWebResponse.StatusCode == HttpStatusCode.MovedPermanently || httpWebResponse.StatusCode == HttpStatusCode.MovedPermanently)
						{
							Uri uri = new Uri(headers["Location"], UriKind.RelativeOrAbsolute);
							if (uri.IsAbsoluteUri)
							{
								text = uri.ToString();
							}
							else
							{
								text = text.Replace(httpWebRequest.Address.AbsolutePath, uri.ToString());
							}
							text = text.Trim();
							this.helpUpdater.Cmdlet.WriteVerbose(UpdatableHelpStrings.UpdateRedirectingToHost(text2, text));
						}
						else if (httpWebResponse.StatusCode == HttpStatusCode.OK)
						{
							return text;
						}
					}
					finally
					{
						httpWebResponse.Close();
					}
				}
			}
			catch (UriFormatException ex)
			{
				throw new UpdatableExchangeHelpSystemException(UpdatableHelpStrings.UpdateInvalidHelpInfoUriErrorID, new LocalizedString(ex.Message), ErrorCategory.InvalidData, null, ex);
			}
			throw new UpdatableExchangeHelpSystemException(UpdatableHelpStrings.UpdateTooManyUriRedirectionsErrorID, UpdatableHelpStrings.UpdateTooManyUriRedirections(5), ErrorCategory.InvalidOperation, null, null);
		}

		// Token: 0x04003ADD RID: 15069
		private const int ThreadWaitMilliseconds = 100;

		// Token: 0x04003ADE RID: 15070
		private const int ManifestTimeoutMilliseconds = 30000;

		// Token: 0x04003ADF RID: 15071
		private const int CabinetTimeoutMilliseconds = 120000;

		// Token: 0x04003AE0 RID: 15072
		private const int MaxUrlRedirections = 5;

		// Token: 0x04003AE1 RID: 15073
		private HelpUpdater helpUpdater;

		// Token: 0x04003AE2 RID: 15074
		private UpdatableExchangeHelpSystemException downloadException;
	}
}
