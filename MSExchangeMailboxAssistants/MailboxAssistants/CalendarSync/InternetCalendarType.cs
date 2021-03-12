using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.HolidayCalendars.Configuration;
using Microsoft.Exchange.Entities.HolidayCalendars.Configuration.Exceptions;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.Common.Sharing;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MailboxAssistants.CalendarSync
{
	// Token: 0x020000C7 RID: 199
	internal class InternetCalendarType : SynchronizableFolderType
	{
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x0003A04A File Offset: 0x0003824A
		public override string FolderTypeName
		{
			get
			{
				return "InternetCalendar";
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x0003A051 File Offset: 0x00038251
		protected override Guid ProviderGuid
		{
			get
			{
				return PublishingFolderManager.InternetCalendarProviderGuid;
			}
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0003A058 File Offset: 0x00038258
		protected override bool MatchesFolder(Folder folder)
		{
			if (!((CalendarFolder)folder).IsInternetCalendarFolder)
			{
				SynchronizableFolderType.Tracer.TraceDebug<object, string, string>((long)this.GetHashCode(), "{0}: InternetCalendarType.MatchesFolder: folder {1} from user {2} isn't an internet calendar.", TraceContext.Get(), folder.DisplayName, ((MailboxSession)folder.Session).DisplayName);
				return false;
			}
			return true;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0003A0A6 File Offset: 0x000382A6
		protected override bool MatchesExtendedFolderFlags(int extendedFolderFlags)
		{
			return CalendarFolder.IsInternetCalendar(extendedFolderFlags);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0003A0B0 File Offset: 0x000382B0
		protected override bool HasSubscriptionInternal(MailboxSession mailboxSession, StoreObjectId folderId)
		{
			bool result;
			using (PublishingSubscriptionManager publishingSubscriptionManager = new PublishingSubscriptionManager(mailboxSession))
			{
				result = (publishingSubscriptionManager.GetByLocalFolderId(folderId) != null);
			}
			return result;
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x0003A0F0 File Offset: 0x000382F0
		public override PropertyDefinition CounterProperty
		{
			get
			{
				return MailboxSchema.InternetCalendarSubscriptionCount;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x0003A0F7 File Offset: 0x000382F7
		public override StoreObjectType StoreObjectType
		{
			get
			{
				return StoreObjectType.CalendarFolder;
			}
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0003A0FA File Offset: 0x000382FA
		protected override bool MatchesContainerClass(string containerClass)
		{
			return ObjectClass.IsCalendarFolder(containerClass);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0003A104 File Offset: 0x00038304
		public override bool Synchronize(MailboxSession mailboxSession, FolderRow folderRow, Deadline deadline, CalendarSyncPerformanceCountersInstance counters, CalendarSyncFolderOperationLogEntry folderOpLogEntry)
		{
			SynchronizableFolderType.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: InternetCalendarType.Synchronize will try to sync folder {1} with id {2} for mailbox {3}.", new object[]
			{
				TraceContext.Get(),
				folderRow.DisplayName,
				folderRow.FolderId,
				mailboxSession.DisplayName
			});
			bool flag = true;
			PublishingSubscriptionData subscriptionData = this.GetSubscriptionData(mailboxSession, folderRow);
			if (subscriptionData == null)
			{
				folderOpLogEntry.AddErrorToLog("NoSubscriptionData", null);
				return true;
			}
			folderOpLogEntry.SubscriptionData = subscriptionData;
			folderOpLogEntry.FolderUrl = subscriptionData.PublishingUrl.ToString();
			SynchronizableFolderType.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: InternetCalendarType.Synchronize will try to open URL {1} to sync folder {2} with id {3} for mailbox {4}.", new object[]
			{
				TraceContext.Get(),
				subscriptionData.PublishingUrl,
				folderRow.DisplayName,
				folderRow.FolderId,
				mailboxSession.DisplayName
			});
			try
			{
				flag = this.SyncFolder(mailboxSession, folderRow, subscriptionData, deadline, counters, folderOpLogEntry);
			}
			catch (ObjectNotFoundException ex)
			{
				SynchronizableFolderType.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: InternetCalendarType.Synchronize for subscription {1} folder {2} with id {3} for mailbox {4} hit exception {5}.", new object[]
				{
					TraceContext.Get(),
					subscriptionData,
					folderRow.DisplayName,
					folderRow.FolderId,
					mailboxSession.DisplayName,
					ex
				});
				folderOpLogEntry.AddExceptionToLog(ex);
			}
			SynchronizableFolderType.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: InternetCalendarType.Synchronize for folder {1} with id {2} for mailbox {3} will return {4}.", new object[]
			{
				TraceContext.Get(),
				folderRow.DisplayName,
				folderRow.FolderId,
				mailboxSession.DisplayName,
				flag
			});
			return flag;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0003A2AC File Offset: 0x000384AC
		private bool SyncFolder(MailboxSession mailboxSession, FolderRow folderRow, PublishingSubscriptionData subscriptionData, Deadline deadline, CalendarSyncPerformanceCountersInstance counters, CalendarSyncFolderOperationLogEntry folderOpLogEntry)
		{
			List<LocalizedString> list = new List<LocalizedString>();
			bool result = true;
			bool flag = false;
			ExDateTime utcNow = ExDateTime.UtcNow;
			using (CalendarFolder calendarFolder = CalendarFolder.Bind(mailboxSession, folderRow.FolderId))
			{
				ImportCalendarResults importCalendarResults = null;
				try
				{
					TimeSpan timeLeft = deadline.TimeLeft;
					if (timeLeft > TimeSpan.Zero)
					{
						calendarFolder.LastAttemptedSyncTime = utcNow;
						TimeSpan timeout = (timeLeft > SynchronizableFolderType.MaxSyncTimePerFolder) ? SynchronizableFolderType.MaxSyncTimePerFolder : timeLeft;
						HttpWebRequest httpWebRequest = this.CreateWebRequest(subscriptionData, timeout, mailboxSession, folderOpLogEntry);
						if (httpWebRequest == null)
						{
							SynchronizableFolderType.Tracer.TraceWarning<Uri>((long)this.GetHashCode(), "Unable to get web request for subscription. {0}", subscriptionData.PublishingUrl);
							return true;
						}
						using (ImportCalendarStream importCalendarStream = new ImportCalendarStream())
						{
							int num = -1;
							using (WebResponse response = httpWebRequest.GetResponse())
							{
								using (Stream responseStream = response.GetResponseStream())
								{
									num = importCalendarStream.CopyFrom(responseStream);
									SynchronizableFolderType.Tracer.TraceDebug<object, int, PublishingSubscriptionData>((long)this.GetHashCode(), "{0}: Read data ({1} bytes ) into memory stream from subscription source {2}.", TraceContext.Get(), num, subscriptionData);
									responseStream.Close();
								}
								response.Close();
							}
							if (num < 0)
							{
								folderOpLogEntry.AddErrorToLog("SubscriptionOverSizeLimit", null);
							}
							else
							{
								ExDateRange importWindow = InternetCalendarType.GetImportWindow(subscriptionData.PublishingUrl);
								importCalendarResults = ICalSharingHelper.ImportCalendar(importCalendarStream, "utf-8", new InboundConversionOptions(mailboxSession.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid), mailboxSession.ServerFullyQualifiedDomainName), mailboxSession, calendarFolder, deadline, importWindow.Start, importWindow.End);
								if (importCalendarResults.Result != ImportCalendarResultType.Success)
								{
									list.AddRange(importCalendarResults.Errors);
									foreach (LocalizedString value in importCalendarResults.Errors)
									{
										folderOpLogEntry.AddErrorToLog("ImportCalendarException", value);
									}
								}
								flag = (importCalendarResults.Result != ImportCalendarResultType.Failed);
							}
						}
						SynchronizableFolderType.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: iCal to folder {1} with id {2} for mailbox {3} with subscription {4}. Results: {5}", new object[]
						{
							TraceContext.Get(),
							folderRow.DisplayName,
							folderRow.FolderId,
							mailboxSession.DisplayName,
							subscriptionData,
							(importCalendarResults != null) ? importCalendarResults.ToString() : "iCal over the size limit"
						});
						if (importCalendarResults == null)
						{
							folderOpLogEntry.AddErrorToLog("iCalOverSizeLimit", null);
						}
					}
					result = (importCalendarResults != null && !importCalendarResults.TimedOut);
				}
				catch (WebException ex)
				{
					list.Add(new LocalizedString(ex.ToString()));
					result = (ex.Status != WebExceptionStatus.Timeout);
					folderOpLogEntry.AddExceptionToLog(ex);
				}
				catch (SystemException ex2)
				{
					list.Add(new LocalizedString(ex2.ToString()));
					folderOpLogEntry.AddExceptionToLog(ex2);
				}
				catch (Exception ex3)
				{
					SynchronizableFolderType.Tracer.TraceError((long)this.GetHashCode(), "{0}: InternetCalendarType.SyncFolder for folder {1} with id {2} for mailbox {3} had the following exception: {4}. Subscription data: {5}", new object[]
					{
						TraceContext.Get(),
						folderRow.DisplayName,
						folderRow.FolderId,
						mailboxSession.DisplayName,
						ex3,
						subscriptionData
					});
					this.SaveLocalFolder(mailboxSession, calendarFolder, subscriptionData, folderRow);
					list.Add(new LocalizedString(ex3.ToString()));
					folderOpLogEntry.AddExceptionToLog(ex3);
					throw;
				}
				if (flag)
				{
					calendarFolder.LastSuccessfulSyncTime = utcNow;
				}
				this.SaveLocalFolder(mailboxSession, calendarFolder, subscriptionData, folderRow);
			}
			if (list != null)
			{
				foreach (LocalizedString localizedString in list)
				{
					SynchronizableFolderType.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: InternetCalendarType.SyncFolder for folder {1} with id {2} for mailbox {3} had the following error: {4}. Subscription data: {5}", new object[]
					{
						TraceContext.Get(),
						folderRow.DisplayName,
						folderRow.FolderId,
						mailboxSession.DisplayName,
						localizedString,
						subscriptionData
					});
				}
			}
			return result;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0003A754 File Offset: 0x00038954
		private void SaveLocalFolder(MailboxSession mailboxSession, Folder folder, PublishingSubscriptionData subscriptionData, FolderRow folderRow)
		{
			if (folder.Save().OperationResult != OperationResult.Succeeded)
			{
				InternetCalendarLog.LogEntry(mailboxSession, string.Format("InternetCalendarType.SyncFolder couldn't update LastAttemptedSyncTime of URL {0} on folder {1} with id {2}.", subscriptionData.PublishingUrl, folderRow.DisplayName, folderRow.FolderId));
				SynchronizableFolderType.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: InternetCalendarType.SyncFolder couldn't update LastAttemptedSyncTime of URL {1} on folder {2} with id {3} for mailbox {4}.", new object[]
				{
					TraceContext.Get(),
					subscriptionData.PublishingUrl,
					folderRow.DisplayName,
					folderRow.FolderId,
					mailboxSession.DisplayName
				});
			}
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0003A7E0 File Offset: 0x000389E0
		private HttpWebRequest CreateWebRequest(PublishingSubscriptionData subscriptionData, TimeSpan timeout, MailboxSession session, CalendarSyncFolderOperationLogEntry folderOpLogEntry)
		{
			Uri uri = subscriptionData.PublishingUrl;
			if (subscriptionData.UrlNeedsExpansion)
			{
				VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(session.MailboxOwner.GetContext(null), null, null);
				if (snapshot.OwaClientServer.XOWAHolidayCalendars.Enabled)
				{
					uri = this.ExpandSubscriptionUrl(uri, snapshot, session.PreferedCulture, folderOpLogEntry);
				}
			}
			if (uri == null)
			{
				return null;
			}
			HttpWebRequest httpWebRequest = WebRequest.Create(uri) as HttpWebRequest;
			if (httpWebRequest == null)
			{
				throw new UriFormatException(string.Format("The following URI isn't supported: {0}", uri));
			}
			httpWebRequest.Timeout = (int)timeout.TotalMilliseconds;
			httpWebRequest.Method = "GET";
			httpWebRequest.Accept = "*/*";
			httpWebRequest.Credentials = null;
			httpWebRequest.AllowAutoRedirect = true;
			httpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)";
			Server localServer = LocalServerCache.LocalServer;
			if (localServer != null && localServer.InternetWebProxy != null)
			{
				httpWebRequest.Proxy = new WebProxy(localServer.InternetWebProxy, true);
			}
			return httpWebRequest;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0003A8CC File Offset: 0x00038ACC
		private static ExDateRange GetImportWindow(Uri publishingUri)
		{
			ExDateTime utcNow = ExDateTime.UtcNow;
			ExDateTime start;
			ExDateTime end;
			if (publishingUri.Scheme == "holidays")
			{
				start = utcNow.IncrementYears(-5);
				end = utcNow.IncrementYears(10);
			}
			else
			{
				start = utcNow.IncrementYears(-1);
				end = utcNow.IncrementYears(1);
			}
			return new ExDateRange(start, end);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0003A920 File Offset: 0x00038B20
		private Uri ExpandSubscriptionUrl(Uri subscriptionUri, VariantConfigurationSnapshot config, CultureInfo culture, CalendarSyncFolderOperationLogEntry folderOpLogEntry)
		{
			Uri result = null;
			UrlResolver urlResolver = null;
			try
			{
				urlResolver = ConfigurationCache.Instance.GetUrlResolver(config);
				result = urlResolver.ResolveUrl(subscriptionUri, culture);
			}
			catch (HolidayCalendarException ex)
			{
				SynchronizableFolderType.Tracer.TraceError((long)this.GetHashCode(), "Exception encountered while expanding holiday calendar URL:'{0}' Endpoint: '{1}' Resolver: '{2}' Exception: {3}", new object[]
				{
					subscriptionUri,
					(config.HolidayCalendars.HostConfiguration != null) ? config.HolidayCalendars.HostConfiguration.Endpoint : "<null>",
					(urlResolver != null) ? "Retrieved" : "<null>",
					ex
				});
				folderOpLogEntry.AddExceptionToLog(ex);
			}
			return result;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0003A9CC File Offset: 0x00038BCC
		private PublishingSubscriptionData GetSubscriptionData(MailboxSession mailboxSession, FolderRow folderRow)
		{
			PublishingSubscriptionData byLocalFolderId;
			using (PublishingSubscriptionManager publishingSubscriptionManager = new PublishingSubscriptionManager(mailboxSession))
			{
				byLocalFolderId = publishingSubscriptionManager.GetByLocalFolderId(folderRow.FolderId);
			}
			return byLocalFolderId;
		}

		// Token: 0x040005D4 RID: 1492
		private const string noSubscriptionDataStr = "NoSubscriptionData";

		// Token: 0x040005D5 RID: 1493
		private const string subscriptionOverSizeLimitStr = "SubscriptionOverSizeLimit";

		// Token: 0x040005D6 RID: 1494
		private const string importCalendarExceptionStr = "ImportCalendarException";

		// Token: 0x040005D7 RID: 1495
		private const string iCalOverSizeLimitStr = "iCalOverSizeLimit";
	}
}
