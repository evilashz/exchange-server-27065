using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.SharePointSignalStore;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.SharePointSignalStore;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.SharePointSignalStore
{
	// Token: 0x02000229 RID: 553
	internal sealed class SharePointSignalStoreAssistant : TimeBasedAssistant, ITimeBasedAssistant, IAssistantBase
	{
		// Token: 0x060014E2 RID: 5346 RVA: 0x00077DD4 File Offset: 0x00075FD4
		public SharePointSignalStoreAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
			DiagnosticsLogConfig.LogDefaults logDefaults = new DiagnosticsLogConfig.LogDefaults(Guid.Parse("{9e16ddf9-44a5-4ae1-9a08-ca80382935cf}"), "SharePointSignalStoreAssistant", "SharePoint SignalStore Diagnostics Logs", Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\SharePointSignalStore"), "SharePointSignalStore_", "SharePointSignalStoreLogs");
			this.diagnosticsSession = new DiagnosticsSession(base.NonLocalizedName, null, ExTraceGlobals.GeneralTracer, (long)this.GetHashCode(), null, logDefaults, null);
			this.IsDesignatedEnvironment = this.IsExchangeDatacenter();
			this.SharePointUrlFactory = new SharePointUrlFactory();
			this.LoggerFactory = new UserSessionLoggerFactory();
			this.GetCredentials = new Func<ADUser, ICredentials>(SharePointSignalStoreAssistant.GetOAuthCredentials);
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x00077E72 File Offset: 0x00076072
		// (set) Token: 0x060014E4 RID: 5348 RVA: 0x00077E7A File Offset: 0x0007607A
		public ISharePointUrlFactory SharePointUrlFactory { get; set; }

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x00077E83 File Offset: 0x00076083
		// (set) Token: 0x060014E6 RID: 5350 RVA: 0x00077E8B File Offset: 0x0007608B
		public ILoggerFactory LoggerFactory { get; set; }

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x00077E94 File Offset: 0x00076094
		// (set) Token: 0x060014E8 RID: 5352 RVA: 0x00077E9C File Offset: 0x0007609C
		public bool IsDesignatedEnvironment { get; set; }

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x060014E9 RID: 5353 RVA: 0x00077EA5 File Offset: 0x000760A5
		public IDiagnosticsSession DiagnosticsSession
		{
			get
			{
				return this.diagnosticsSession;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x00077EAD File Offset: 0x000760AD
		// (set) Token: 0x060014EB RID: 5355 RVA: 0x00077EB5 File Offset: 0x000760B5
		public Func<ADUser, ICredentials> GetCredentials { get; set; }

		// Token: 0x060014EC RID: 5356 RVA: 0x00077EBE File Offset: 0x000760BE
		public static ICredentials GetOAuthCredentials(ADUser user)
		{
			return OAuthCredentials.GetOAuthCredentialsForAppActAsToken(user.OrganizationId, user, null);
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x00077ECD File Offset: 0x000760CD
		public void OnWorkCycleCheckpoint()
		{
			this.diagnosticsSession.TraceDebug("OnWorkCycleCheckpoint()", new object[0]);
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x00077EE5 File Offset: 0x000760E5
		protected override void OnShutdownInternal()
		{
			this.diagnosticsSession.TraceDebug("ShutDown is called on the service. Sending Abort processing signal to the pipeline", new object[0]);
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x00077F00 File Offset: 0x00076100
		protected override void InvokeInternal(InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog)
		{
			if (!this.IsDesignatedEnvironment)
			{
				return;
			}
			MailboxSession mailboxSession = invokeArgs.StoreSession as MailboxSession;
			if (mailboxSession == null)
			{
				return;
			}
			if (mailboxSession.MailboxOwner.RecipientTypeDetails != RecipientTypeDetails.UserMailbox && mailboxSession.MailboxOwner.RecipientTypeDetails != RecipientTypeDetails.LinkedMailbox)
			{
				this.diagnosticsSession.TraceDebug<Guid, string, RecipientTypeDetails>("Skipping mailbox with guid {0} and display name {1} since this is a {2} and not a UserMailbox or a LinkedMailbox", mailboxSession.MailboxGuid, mailboxSession.MailboxOwner.MailboxInfo.DisplayName, mailboxSession.MailboxOwner.RecipientTypeDetails);
				return;
			}
			ILogger logger = this.LoggerFactory.CreateLogger(mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(), this.diagnosticsSession);
			logger.LogInfo("Begin process mailbox", new object[0]);
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			try
			{
				this.SendSignalsForMailbox(mailboxSession, logger);
			}
			catch (Exception ex)
			{
				if (ex is OutOfMemoryException || ex is StackOverflowException || ex is ThreadAbortException)
				{
					stopwatch.Stop();
					logger.LogWarning("Aborted sending signals (used {0} seconds), caught exception {1}", new object[]
					{
						stopwatch.Elapsed.TotalSeconds,
						ex
					});
					throw;
				}
				if (ex is SharePointSignalStoreException)
				{
					logger.LogInfo("Failed to send the signals: {0}", new object[]
					{
						ex.InnerException.Message
					});
					throw new SkipException(ex);
				}
				logger.LogInfo("Failed to send the signals: {0}", new object[]
				{
					ex
				});
			}
			finally
			{
				stopwatch.Stop();
				logger.LogInfo("End process mailbox (used {0} seconds)", new object[]
				{
					stopwatch.Elapsed.TotalSeconds
				});
			}
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x000780C8 File Offset: 0x000762C8
		private bool IsExchangeDatacenter()
		{
			bool result;
			try
			{
				result = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).MailboxAssistants.SharePointSignalStoreInDatacenter.Enabled;
			}
			catch (Exception ex)
			{
				if (this.diagnosticsSession != null)
				{
					string operation = string.Format(CultureInfo.InvariantCulture, "Failed to determine Exchange DataCenter status: {0}", new object[]
					{
						ex
					});
					this.diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Warnings, operation, new object[0]);
				}
				result = false;
			}
			return result;
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x00078148 File Offset: 0x00076348
		private void SendSignalsForMailbox(MailboxSession session, ILogger logger)
		{
			SharePointFlighted sharePointFlighted = new SharePointFlighted(logger);
			ISharePointUrl sharePointUrl = this.SharePointUrlFactory.CreateADWithDictFallbackSharePointUrl(new ADSharePointUrl(), new DictSharePointUrl(), logger);
			ADUser aduser = (ADUser)DirectoryHelper.ReadADRecipient(session.MailboxOwner.MailboxInfo.MailboxGuid, session.MailboxOwner.MailboxInfo.IsArchive, session.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid));
			ICredentials credentials = this.GetCredentials(aduser);
			if (sharePointFlighted.IsUserFlighted(aduser))
			{
				string url = sharePointUrl.GetUrl(session.MailboxOwner, session.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid));
				if (string.IsNullOrEmpty(url))
				{
					return;
				}
				using (Folder folder = Folder.Bind(session, DefaultFolderType.Inbox, new PropertyDefinition[]
				{
					FolderSchema.OfficeGraphLocation
				}))
				{
					string value = folder.TryGetProperty(FolderSchema.OfficeGraphLocation) as string;
					if (string.IsNullOrEmpty(value))
					{
						folder[FolderSchema.OfficeGraphLocation] = url;
						folder.Save();
					}
				}
				SharePointSignalRestSender sharePointSignalRestSender = new SharePointSignalRestSender(credentials, url, logger);
				SharePointSignalRestDataProvider sharePointSignalRestDataProvider = new SharePointSignalRestDataProvider();
				sharePointSignalRestDataProvider.AddAnalyticsSignalSource(new RecipientCacheSignalSource(session));
				sharePointSignalRestDataProvider.ProvideDataFor(sharePointSignalRestSender);
				if (sharePointSignalRestDataProvider.AnyDataProvided())
				{
					sharePointSignalRestDataProvider.PrintProviderReport(logger);
					try
					{
						sharePointSignalRestSender.Send();
					}
					catch (WebException ex)
					{
						if (SharePointSignalStoreAssistant.IsWebExceptionUnauthorized(ex))
						{
							throw;
						}
						throw new SharePointSignalStoreException("Failed to send signals to SharePoint", ex);
					}
					logger.LogInfo("Successfully sent signals", new object[0]);
					return;
				}
				logger.LogInfo("No Recipient Cache retrieved for mailbox.", new object[0]);
			}
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x000782D8 File Offset: 0x000764D8
		internal static bool IsWebExceptionUnauthorized(WebException exception)
		{
			if (exception.Status == WebExceptionStatus.ProtocolError)
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)exception.Response;
				if (httpWebResponse != null && httpWebResponse.StatusCode == HttpStatusCode.Unauthorized)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0007830D File Offset: 0x0007650D
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x00078315 File Offset: 0x00076515
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x0007831D File Offset: 0x0007651D
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x04000C9C RID: 3228
		private readonly IDiagnosticsSession diagnosticsSession;
	}
}
