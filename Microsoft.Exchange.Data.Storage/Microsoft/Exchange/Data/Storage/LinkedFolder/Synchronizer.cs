using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x02000967 RID: 2407
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class Synchronizer : DisposableObject
	{
		// Token: 0x170018B2 RID: 6322
		// (get) Token: 0x06005922 RID: 22818 RVA: 0x0016E134 File Offset: 0x0016C334
		// (set) Token: 0x06005923 RID: 22819 RVA: 0x0016E13C File Offset: 0x0016C33C
		public object SyncResult { get; protected set; }

		// Token: 0x170018B3 RID: 6323
		// (get) Token: 0x06005924 RID: 22820 RVA: 0x0016E145 File Offset: 0x0016C345
		// (set) Token: 0x06005925 RID: 22821 RVA: 0x0016E14D File Offset: 0x0016C34D
		public Exception LastError { get; protected set; }

		// Token: 0x170018B4 RID: 6324
		// (get) Token: 0x06005926 RID: 22822 RVA: 0x0016E156 File Offset: 0x0016C356
		public SyncOption SyncOption
		{
			get
			{
				if (this.Job == null)
				{
					return SyncOption.Default;
				}
				return this.Job.SyncOption;
			}
		}

		// Token: 0x06005927 RID: 22823 RVA: 0x0016E170 File Offset: 0x0016C370
		public Synchronizer(TeamMailboxSyncJob job, MailboxSession mailboxSession, IResourceMonitor resourceMonitor, string siteUrl, ICredentials credential, bool isOAuthCredential, bool enableHttpDebugProxy, Stream syncCycleLogStream)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			if (resourceMonitor == null)
			{
				throw new ArgumentNullException("resourceMonitor");
			}
			if (string.IsNullOrEmpty(siteUrl))
			{
				throw new ArgumentNullException("siteUrl");
			}
			try
			{
				this.siteUri = new Uri(siteUrl);
			}
			catch (UriFormatException innerException)
			{
				throw new ArgumentException(string.Format("Invalid format for siteUrl: {0}", siteUrl), innerException);
			}
			if (!this.siteUri.IsAbsoluteUri)
			{
				throw new ArgumentException(string.Format("Expect siteUrl: {0} to be absolute Uri", siteUrl));
			}
			this.mailboxSession = mailboxSession;
			this.Job = job;
			this.credential = credential;
			this.isOAuthCredential = isOAuthCredential;
			this.resourceMonitor = resourceMonitor;
			this.enableHttpDebugProxy = enableHttpDebugProxy;
			this.loggingContext = new LoggingContext(this.mailboxSession.MailboxGuid, this.siteUri.ToString(), (job != null) ? job.ClientString : string.Empty, syncCycleLogStream);
		}

		// Token: 0x06005928 RID: 22824 RVA: 0x0016E284 File Offset: 0x0016C484
		protected AsyncCallback WrapCallbackWithUnhandledExceptionAndSendReport(AsyncCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return delegate(IAsyncResult asyncResult)
			{
				this.ProtectedExecution(callback, asyncResult);
			};
		}

		// Token: 0x06005929 RID: 22825 RVA: 0x0016E2D8 File Offset: 0x0016C4D8
		protected CancelableAsyncCallback WrapCallbackWithUnhandledExceptionAndSendReportEx(AsyncCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return delegate(ICancelableAsyncResult asyncResult)
			{
				this.ProtectedExecution(callback, asyncResult);
			};
		}

		// Token: 0x0600592A RID: 22826
		protected abstract LocalizedString GetSyncIssueEmailErrorString(string error, out LocalizedString body);

		// Token: 0x0600592B RID: 22827
		protected abstract void InitializeSyncMetadata();

		// Token: 0x0600592C RID: 22828 RVA: 0x0016E310 File Offset: 0x0016C510
		protected void InitializeHttpClient(string method)
		{
			if (this.httpClient == null)
			{
				this.httpClient = new HttpClient();
				this.httpSessionConfig = new HttpSessionConfig();
				this.httpSessionConfig.Method = method;
				this.httpSessionConfig.Credentials = this.credential;
				this.httpSessionConfig.UserAgent = Utils.GetUserAgentStringForSiteMailboxRequests();
				if (this.enableHttpDebugProxy)
				{
					this.httpSessionConfig.Proxy = new WebProxy("127.0.0.1", 8888);
				}
			}
		}

		// Token: 0x0600592D RID: 22829 RVA: 0x0016E38C File Offset: 0x0016C58C
		protected void SetCommonOauthRequestHeaders()
		{
			this.httpSessionConfig.PreAuthenticate = true;
			this.httpSessionConfig.Headers = new WebHeaderCollection();
			this.httpSessionConfig.Headers["Authorization"] = "Bearer";
			this.httpSessionConfig.Headers["X-RequestForceAuthentication"] = "true";
			this.httpSessionConfig.Headers["client-request-id"] = Guid.NewGuid().ToString();
			this.httpSessionConfig.Headers["return-client-request-id"] = "true";
		}

		// Token: 0x0600592E RID: 22830 RVA: 0x0016E42C File Offset: 0x0016C62C
		protected void PublishMonitoringResult()
		{
			string name = ExchangeComponent.SiteMailbox.Name;
			string name2 = base.GetType().Name;
			string notificationReason = this.siteUri.AbsoluteUri.Replace('/', '\\');
			ResultSeverityLevel severity = ResultSeverityLevel.Informational;
			string message = string.Empty;
			if (this.LastError != null)
			{
				severity = ResultSeverityLevel.Error;
				message = this.LastError.Message;
			}
			EventNotificationItem eventNotificationItem = new EventNotificationItem(name, name2, notificationReason, severity);
			eventNotificationItem.Message = message;
			try
			{
				eventNotificationItem.Publish(false);
			}
			catch (UnauthorizedAccessException exception)
			{
				ProtocolLog.LogError(ProtocolLog.Component.Monitor, this.loggingContext, "PublishMonitoringResult failed with UnauthorizedAccessException", exception);
			}
			catch (EventLogNotFoundException exception2)
			{
				ProtocolLog.LogError(ProtocolLog.Component.Monitor, this.loggingContext, "PublishMonitoringResult failed with EventLogNotFoundException", exception2);
			}
		}

		// Token: 0x0600592F RID: 22831 RVA: 0x0016E4F0 File Offset: 0x0016C6F0
		protected void SaveSyncMetadata()
		{
			if (this.syncMetadata != null)
			{
				try
				{
					if (this.LastError != null)
					{
						this.UpdateSyncMetadataOnSyncFailure();
					}
					else
					{
						this.UpdateSyncMetadataOnSuccessfulSync();
					}
					this.syncMetadata.Save();
				}
				catch (StorageTransientException exception)
				{
					ProtocolLog.LogError(this.loggingComponent, this.loggingContext, "SaveSyncMetadata: Failed with StorageTransientException", exception);
				}
				catch (StoragePermanentException exception2)
				{
					ProtocolLog.LogError(this.loggingComponent, this.loggingContext, "SaveSyncMetadata: Failed with StoragePermanentException", exception2);
				}
			}
		}

		// Token: 0x06005930 RID: 22832 RVA: 0x0016E578 File Offset: 0x0016C778
		protected void SetSyncMetadataValue(string name, object value)
		{
			if (this.syncMetadata != null)
			{
				IDictionary dictionary = this.syncMetadata.GetDictionary();
				dictionary[name] = value;
			}
		}

		// Token: 0x06005931 RID: 22833 RVA: 0x0016E5A4 File Offset: 0x0016C7A4
		protected object GetSyncMetadataValue(string name)
		{
			if (this.syncMetadata == null)
			{
				return null;
			}
			IDictionary dictionary = this.syncMetadata.GetDictionary();
			if (dictionary.Contains(name))
			{
				return dictionary[name];
			}
			return null;
		}

		// Token: 0x06005932 RID: 22834 RVA: 0x0016E5D9 File Offset: 0x0016C7D9
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.syncMetadata != null)
			{
				this.syncMetadata.Dispose();
				this.syncMetadata = null;
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x06005933 RID: 22835 RVA: 0x0016E600 File Offset: 0x0016C800
		protected void UpdateSyncMetadataOnBeginSync()
		{
			if (!(this.GetSyncMetadataValue("FirstAttemptedSyncTime") is ExDateTime?))
			{
				this.SetSyncMetadataValue("FirstAttemptedSyncTime", ExDateTime.UtcNow);
			}
			this.lastAttemptedSyncTime = ExDateTime.UtcNow;
			this.SetSyncMetadataValue("LastAttemptedSyncTime", this.lastAttemptedSyncTime);
		}

		// Token: 0x06005934 RID: 22836 RVA: 0x0016E662 File Offset: 0x0016C862
		private void UpdateSyncMetadataOnSuccessfulSync()
		{
			this.SetSyncMetadataValue("LastSuccessfulSyncTime", ExDateTime.UtcNow);
			this.SetSyncMetadataValue("LastSyncFailure", null);
			this.SetSyncMetadataValue("LastFailedSyncTime", null);
			this.SetSyncMetadataValue("LastFailedSyncEmailTime", null);
		}

		// Token: 0x06005935 RID: 22837 RVA: 0x0016E6A0 File Offset: 0x0016C8A0
		private void UpdateSyncMetadataOnSyncFailure()
		{
			try
			{
				ExDateTime? exDateTime = this.GetSyncMetadataValue("LastSuccessfulSyncTime") as ExDateTime?;
				ExDateTime? exDateTime2 = this.GetSyncMetadataValue("FirstAttemptedSyncTime") as ExDateTime?;
				ExDateTime? exDateTime3 = this.GetSyncMetadataValue("LastFailedSyncEmailTime") as ExDateTime?;
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine(ProtocolLog.GetExceptionLogString(this.LastError));
				if (this.LastError is SharePointException)
				{
					stringBuilder.AppendLine("SharePointException Diagnostic Info:");
					stringBuilder.AppendLine(((SharePointException)this.LastError).DiagnosticInfo);
				}
				this.SetSyncMetadataValue("LastSyncFailure", stringBuilder.ToString());
				this.SetSyncMetadataValue("LastFailedSyncTime", ExDateTime.UtcNow);
				ExDateTime? exDateTime4 = exDateTime ?? exDateTime2;
				if ((exDateTime3 == null || ExDateTime.UtcNow - exDateTime3 > TimeSpan.FromHours(24.0)) && exDateTime4 != null && ExDateTime.UtcNow - exDateTime4 > TimeSpan.FromHours(12.0))
				{
					StoreObjectId destFolderId = Utils.EnsureSyncIssueFolder(this.mailboxSession);
					using (MessageItem messageItem = MessageItem.Create(this.mailboxSession, destFolderId))
					{
						LocalizedString empty = LocalizedString.Empty;
						messageItem.From = new Participant(this.Job.SyncInfoEntry.MailboxPrincipal);
						messageItem.Subject = this.GetSyncIssueEmailErrorString(this.LastError.Message, out empty);
						using (TextWriter textWriter = messageItem.Body.OpenTextWriter(BodyFormat.TextHtml))
						{
							messageItem.Body.Reset();
							using (HtmlWriter htmlWriter = new HtmlWriter(textWriter))
							{
								htmlWriter.WriteStartTag(HtmlTagId.P);
								htmlWriter.WriteText(empty);
								htmlWriter.WriteEndTag(HtmlTagId.P);
							}
						}
						messageItem.IsDraft = false;
						messageItem.MarkAsUnread(true);
						messageItem.Save(SaveMode.NoConflictResolutionForceSave);
						this.SetSyncMetadataValue("LastFailedSyncEmailTime", ExDateTime.UtcNow);
					}
				}
			}
			catch (StorageTransientException exception)
			{
				ProtocolLog.LogError(this.loggingComponent, this.loggingContext, "UpdateSyncMetadataOnSyncFailure: Failed with StorageTransientException", exception);
			}
			catch (StoragePermanentException exception2)
			{
				ProtocolLog.LogError(this.loggingComponent, this.loggingContext, "UpdateSyncMetadataOnSyncFailure: Failed with StoragePermanentException", exception2);
			}
		}

		// Token: 0x06005936 RID: 22838 RVA: 0x0016EA24 File Offset: 0x0016CC24
		private void ProtectedExecution(AsyncCallback callback, IAsyncResult asyncResult)
		{
			try
			{
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					callback(asyncResult);
				});
			}
			catch (GrayException ex)
			{
				ProtocolLog.LogError(this.loggingComponent, this.loggingContext, "WrapCallbackWithUnhandledExceptionAndSendReport: Failed with unexpected exception", ex);
				this.executionAsyncResult.InvokeCallback(ex);
			}
		}

		// Token: 0x06005937 RID: 22839
		public abstract IAsyncResult BeginExecute(AsyncCallback executeCallback, object state);

		// Token: 0x06005938 RID: 22840
		public abstract void EndExecute(IAsyncResult asyncResult);

		// Token: 0x06005939 RID: 22841 RVA: 0x0016EA98 File Offset: 0x0016CC98
		protected bool HandleShutDown()
		{
			if (this.Job != null && this.Job.IsShuttingdown)
			{
				this.resourceMonitor.ResetBudget();
				ProtocolLog.LogInformation(this.loggingComponent, this.loggingContext, "HandleShutDown: Shutdown the job by invoke callback");
				this.executionAsyncResult.InvokeCallback(null);
				return true;
			}
			return false;
		}

		// Token: 0x040030EC RID: 12524
		protected readonly TeamMailboxSyncJob Job;

		// Token: 0x040030ED RID: 12525
		protected MailboxSession mailboxSession;

		// Token: 0x040030EE RID: 12526
		protected Uri siteUri;

		// Token: 0x040030EF RID: 12527
		protected LoggingContext loggingContext;

		// Token: 0x040030F0 RID: 12528
		protected bool enableHttpDebugProxy;

		// Token: 0x040030F1 RID: 12529
		protected Stopwatch executeStopwatch;

		// Token: 0x040030F2 RID: 12530
		protected LazyAsyncResult executionAsyncResult;

		// Token: 0x040030F3 RID: 12531
		protected LazyAsyncResult exchangeOperationsAsyncResult;

		// Token: 0x040030F4 RID: 12532
		protected ICredentials credential;

		// Token: 0x040030F5 RID: 12533
		protected bool isOAuthCredential;

		// Token: 0x040030F6 RID: 12534
		protected IResourceMonitor resourceMonitor;

		// Token: 0x040030F7 RID: 12535
		protected UserConfiguration syncMetadata;

		// Token: 0x040030F8 RID: 12536
		protected ExDateTime lastAttemptedSyncTime;

		// Token: 0x040030F9 RID: 12537
		protected HttpClient httpClient;

		// Token: 0x040030FA RID: 12538
		protected ProtocolLog.Component loggingComponent;

		// Token: 0x040030FB RID: 12539
		protected HttpSessionConfig httpSessionConfig;
	}
}
