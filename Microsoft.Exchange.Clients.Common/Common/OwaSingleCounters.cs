using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Common
{
	// Token: 0x02000006 RID: 6
	internal static class OwaSingleCounters
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002F60 File Offset: 0x00001160
		public static void GetPerfCounterInfo(XElement element)
		{
			if (OwaSingleCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in OwaSingleCounters.AllCounters)
			{
				try
				{
					element.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					element.Add(content);
				}
			}
		}

		// Token: 0x0400013F RID: 319
		public const string CategoryName = "MSExchange OWA";

		// Token: 0x04000140 RID: 320
		public static readonly ExPerformanceCounter CurrentUsers = new ExPerformanceCounter("MSExchange OWA", "Current Users", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000141 RID: 321
		public static readonly ExPerformanceCounter CurrentUniqueUsers = new ExPerformanceCounter("MSExchange OWA", "Current Unique Users", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000142 RID: 322
		private static readonly ExPerformanceCounter LogonsPerSecond = new ExPerformanceCounter("MSExchange OWA", "Logons/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000143 RID: 323
		public static readonly ExPerformanceCounter TotalUsers = new ExPerformanceCounter("MSExchange OWA", "Total Users", string.Empty, null, new ExPerformanceCounter[]
		{
			OwaSingleCounters.LogonsPerSecond
		});

		// Token: 0x04000144 RID: 324
		public static readonly ExPerformanceCounter TotalUniqueUsers = new ExPerformanceCounter("MSExchange OWA", "Total Unique Users", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000145 RID: 325
		public static readonly ExPerformanceCounter PeakUserCount = new ExPerformanceCounter("MSExchange OWA", "Peak User Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000146 RID: 326
		public static readonly ExPerformanceCounter CurrentUsersLight = new ExPerformanceCounter("MSExchange OWA", "Current Users Light", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000147 RID: 327
		public static readonly ExPerformanceCounter CurrentUniqueUsersLight = new ExPerformanceCounter("MSExchange OWA", "Current Unique Users Light", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000148 RID: 328
		private static readonly ExPerformanceCounter LogonsPerSecondLight = new ExPerformanceCounter("MSExchange OWA", "Logons/sec Light", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000149 RID: 329
		public static readonly ExPerformanceCounter TotalUsersLight = new ExPerformanceCounter("MSExchange OWA", "Total Users Light", string.Empty, null, new ExPerformanceCounter[]
		{
			OwaSingleCounters.LogonsPerSecondLight
		});

		// Token: 0x0400014A RID: 330
		public static readonly ExPerformanceCounter TotalUniqueUsersLight = new ExPerformanceCounter("MSExchange OWA", "Total Unique Users Light", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400014B RID: 331
		public static readonly ExPerformanceCounter PeakUserCountLight = new ExPerformanceCounter("MSExchange OWA", "Peak User Count Light", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400014C RID: 332
		public static readonly ExPerformanceCounter CurrentUsersPremium = new ExPerformanceCounter("MSExchange OWA", "Current Users Premium", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400014D RID: 333
		public static readonly ExPerformanceCounter CurrentUniqueUsersPremium = new ExPerformanceCounter("MSExchange OWA", "Current Unique Users Premium", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400014E RID: 334
		private static readonly ExPerformanceCounter LogonsPerSecondPremium = new ExPerformanceCounter("MSExchange OWA", "Logons/sec Premium", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400014F RID: 335
		public static readonly ExPerformanceCounter TotalUsersPremium = new ExPerformanceCounter("MSExchange OWA", "Total Users Premium", string.Empty, null, new ExPerformanceCounter[]
		{
			OwaSingleCounters.LogonsPerSecondPremium
		});

		// Token: 0x04000150 RID: 336
		public static readonly ExPerformanceCounter TotalUniqueUsersPremium = new ExPerformanceCounter("MSExchange OWA", "Total Unique Users Premium", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000151 RID: 337
		public static readonly ExPerformanceCounter PeakUserCountPremium = new ExPerformanceCounter("MSExchange OWA", "Peak User Count Premium", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000152 RID: 338
		public static readonly ExPerformanceCounter AverageResponseTime = new ExPerformanceCounter("MSExchange OWA", "Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000153 RID: 339
		public static readonly ExPerformanceCounter TotalSessionsEndedByLogoff = new ExPerformanceCounter("MSExchange OWA", "Sessions Ended by Logoff", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000154 RID: 340
		public static readonly ExPerformanceCounter TotalSessionsEndedByTimeout = new ExPerformanceCounter("MSExchange OWA", "Sessions Ended by Time-out", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000155 RID: 341
		public static readonly ExPerformanceCounter CalendarViewsLoaded = new ExPerformanceCounter("MSExchange OWA", "Calendar Views Loaded", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000156 RID: 342
		public static readonly ExPerformanceCounter CalendarViewsRefreshed = new ExPerformanceCounter("MSExchange OWA", "Calendar View Refreshed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000157 RID: 343
		public static readonly ExPerformanceCounter ItemsCreated = new ExPerformanceCounter("MSExchange OWA", "Items Created Since OWA Start", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000158 RID: 344
		public static readonly ExPerformanceCounter ItemsUpdated = new ExPerformanceCounter("MSExchange OWA", "Items Updated Since OWA Start", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000159 RID: 345
		public static readonly ExPerformanceCounter ItemsDeleted = new ExPerformanceCounter("MSExchange OWA", "Items Deleted Since OWA Start", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400015A RID: 346
		public static readonly ExPerformanceCounter AttachmentsUploaded = new ExPerformanceCounter("MSExchange OWA", "Attachments Uploaded Since OWA Start", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400015B RID: 347
		public static readonly ExPerformanceCounter MailViewsLoaded = new ExPerformanceCounter("MSExchange OWA", "Mail Views Loaded", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400015C RID: 348
		public static readonly ExPerformanceCounter MailViewRefreshes = new ExPerformanceCounter("MSExchange OWA", "Mail View Refreshes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400015D RID: 349
		public static readonly ExPerformanceCounter MessagesSent = new ExPerformanceCounter("MSExchange OWA", "Messages Sent", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400015E RID: 350
		public static readonly ExPerformanceCounter IRMMessagesSent = new ExPerformanceCounter("MSExchange OWA", "IRM-protected Messages Sent", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400015F RID: 351
		public static readonly ExPerformanceCounter AverageSearchTime = new ExPerformanceCounter("MSExchange OWA", "Average Search Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000160 RID: 352
		public static readonly ExPerformanceCounter TotalSearches = new ExPerformanceCounter("MSExchange OWA", "Searches", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000161 RID: 353
		public static readonly ExPerformanceCounter SearchesTimedOut = new ExPerformanceCounter("MSExchange OWA", "Searches Timed Out", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000162 RID: 354
		private static readonly ExPerformanceCounter RequestsPerSecond = new ExPerformanceCounter("MSExchange OWA", "Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000163 RID: 355
		public static readonly ExPerformanceCounter TotalRequests = new ExPerformanceCounter("MSExchange OWA", "Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			OwaSingleCounters.RequestsPerSecond
		});

		// Token: 0x04000164 RID: 356
		private static readonly ExPerformanceCounter RequestsFailedPerSecond = new ExPerformanceCounter("MSExchange OWA", "Failed Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000165 RID: 357
		public static readonly ExPerformanceCounter TotalRequestsFailed = new ExPerformanceCounter("MSExchange OWA", "Requests Failed", string.Empty, null, new ExPerformanceCounter[]
		{
			OwaSingleCounters.RequestsFailedPerSecond
		});

		// Token: 0x04000166 RID: 358
		public static readonly ExPerformanceCounter PID = new ExPerformanceCounter("MSExchange OWA", "PID", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000167 RID: 359
		public static readonly ExPerformanceCounter AverageSpellcheckTime = new ExPerformanceCounter("MSExchange OWA", "Average Check Spelling Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000168 RID: 360
		public static readonly ExPerformanceCounter TotalSpellchecks = new ExPerformanceCounter("MSExchange OWA", "Spelling Checks", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000169 RID: 361
		public static readonly ExPerformanceCounter TotalConversions = new ExPerformanceCounter("MSExchange OWA", "Conversions", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400016A RID: 362
		public static readonly ExPerformanceCounter ActiveConversions = new ExPerformanceCounter("MSExchange OWA", "Active Conversions", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400016B RID: 363
		public static readonly ExPerformanceCounter TotalRejectedConversions = new ExPerformanceCounter("MSExchange OWA", "Rejected Conversions", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400016C RID: 364
		public static readonly ExPerformanceCounter QueuedConversionRequests = new ExPerformanceCounter("MSExchange OWA", "Queued Conversion Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400016D RID: 365
		public static readonly ExPerformanceCounter TotalTimeoutConversions = new ExPerformanceCounter("MSExchange OWA", "Conversions Ended by Time-out", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400016E RID: 366
		public static readonly ExPerformanceCounter TotalErrorConversions = new ExPerformanceCounter("MSExchange OWA", "Conversions Ended with Errors", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400016F RID: 367
		public static readonly ExPerformanceCounter TotalConvertingRequestsRate = new ExPerformanceCounter("MSExchange OWA", "Conversion Requests KB/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000170 RID: 368
		public static readonly ExPerformanceCounter SuccessfulConversionRequestRate = new ExPerformanceCounter("MSExchange OWA", "Successful Conversion Requests KB/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000171 RID: 369
		public static readonly ExPerformanceCounter TotalConvertingResponseRate = new ExPerformanceCounter("MSExchange OWA", "Conversion Responses KB/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000172 RID: 370
		public static readonly ExPerformanceCounter AverageConvertingTime = new ExPerformanceCounter("MSExchange OWA", "Average Conversion Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000173 RID: 371
		public static readonly ExPerformanceCounter AverageConversionQueuingTime = new ExPerformanceCounter("MSExchange OWA", "Average Conversion Queuing Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000174 RID: 372
		public static readonly ExPerformanceCounter InvalidCanaryRequests = new ExPerformanceCounter("MSExchange OWA", "Invalid Canary Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000175 RID: 373
		public static readonly ExPerformanceCounter NamesChecked = new ExPerformanceCounter("MSExchange OWA", "Names Checked", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000176 RID: 374
		public static readonly ExPerformanceCounter PasswordChanges = new ExPerformanceCounter("MSExchange OWA", "Password Changes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000177 RID: 375
		public static readonly ExPerformanceCounter CurrentProxiedUsers = new ExPerformanceCounter("MSExchange OWA", "Current Proxy Users", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000178 RID: 376
		private static readonly ExPerformanceCounter ProxiedUserRequestsPerSecond = new ExPerformanceCounter("MSExchange OWA", "Proxy User Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000179 RID: 377
		public static readonly ExPerformanceCounter ProxiedUserRequests = new ExPerformanceCounter("MSExchange OWA", "Proxy User Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			OwaSingleCounters.ProxiedUserRequestsPerSecond
		});

		// Token: 0x0400017A RID: 378
		public static readonly ExPerformanceCounter ProxiedResponseTimeAverage = new ExPerformanceCounter("MSExchange OWA", "Proxy Response Time Average", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400017B RID: 379
		public static readonly ExPerformanceCounter ProxyRequestBytes = new ExPerformanceCounter("MSExchange OWA", "Proxy Request Bytes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400017C RID: 380
		public static readonly ExPerformanceCounter ProxyResponseBytes = new ExPerformanceCounter("MSExchange OWA", "Proxy Response Bytes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400017D RID: 381
		private static readonly ExPerformanceCounter WssBytesPerSecond = new ExPerformanceCounter("MSExchange OWA", "WSS Response Bytes/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400017E RID: 382
		public static readonly ExPerformanceCounter WssBytes = new ExPerformanceCounter("MSExchange OWA", "WSS Response Bytes", string.Empty, null, new ExPerformanceCounter[]
		{
			OwaSingleCounters.WssBytesPerSecond
		});

		// Token: 0x0400017F RID: 383
		private static readonly ExPerformanceCounter UncBytesPerSecond = new ExPerformanceCounter("MSExchange OWA", "UNC Response Bytes/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000180 RID: 384
		public static readonly ExPerformanceCounter UncBytes = new ExPerformanceCounter("MSExchange OWA", "UNC Response Bytes", string.Empty, null, new ExPerformanceCounter[]
		{
			OwaSingleCounters.UncBytesPerSecond
		});

		// Token: 0x04000181 RID: 385
		public static readonly ExPerformanceCounter WssRequests = new ExPerformanceCounter("MSExchange OWA", "WSS Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000182 RID: 386
		public static readonly ExPerformanceCounter UncRequests = new ExPerformanceCounter("MSExchange OWA", "UNC Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000183 RID: 387
		public static readonly ExPerformanceCounter ASQueries = new ExPerformanceCounter("MSExchange OWA", "AS Queries", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000184 RID: 388
		public static readonly ExPerformanceCounter ASQueriesFailurePercent = new ExPerformanceCounter("MSExchange OWA", "AS Queries Failure %", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000185 RID: 389
		public static readonly ExPerformanceCounter StoreLogonFailurePercent = new ExPerformanceCounter("MSExchange OWA", "Store Logon Failure %", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000186 RID: 390
		public static readonly ExPerformanceCounter CASIntraSiteRedirectionLatertoEarlierVersion = new ExPerformanceCounter("MSExchange OWA", "CAS Intra-Site Redirection Later to Earlier Version", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000187 RID: 391
		public static readonly ExPerformanceCounter CASIntraSiteRedirectionEarliertoLaterVersion = new ExPerformanceCounter("MSExchange OWA", "CAS Intra-Site Redirection Earlier to Later Version", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000188 RID: 392
		public static readonly ExPerformanceCounter CASCrossSiteRedirectionLatertoEarlierVersion = new ExPerformanceCounter("MSExchange OWA", "CAS Cross-Site Redirection Later to Earlier Version", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000189 RID: 393
		public static readonly ExPerformanceCounter CASCrossSiteRedirectionEarliertoLaterVersion = new ExPerformanceCounter("MSExchange OWA", "CAS Cross-Site Redirection Earlier to Later Version", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400018A RID: 394
		public static readonly ExPerformanceCounter ActiveMailboxSubscriptions = new ExPerformanceCounter("MSExchange OWA", "Active Mailbox Subscriptions", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400018B RID: 395
		private static readonly ExPerformanceCounter MailboxNotificationsPerSecond = new ExPerformanceCounter("MSExchange OWA", "Mailbox Notifications/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400018C RID: 396
		public static readonly ExPerformanceCounter TotalMailboxNotifications = new ExPerformanceCounter("MSExchange OWA", "Total Mailbox Notifications", string.Empty, null, new ExPerformanceCounter[]
		{
			OwaSingleCounters.MailboxNotificationsPerSecond
		});

		// Token: 0x0400018D RID: 397
		public static readonly ExPerformanceCounter TotalUserContextReInitializationRequests = new ExPerformanceCounter("MSExchange OWA", "Total Usercontext ReInitialization requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400018E RID: 398
		public static readonly ExPerformanceCounter MailboxOfflineExceptionFailuresPercent = new ExPerformanceCounter("MSExchange OWA", "Mailbox Offline Exception Failure %", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400018F RID: 399
		public static readonly ExPerformanceCounter ConnectionFailedTransientExceptionPercent = new ExPerformanceCounter("MSExchange OWA", "Connection Failed Transient Exception %", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000190 RID: 400
		public static readonly ExPerformanceCounter StorageTransientExceptionPercent = new ExPerformanceCounter("MSExchange OWA", "Storage Transient Exception Failure %", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000191 RID: 401
		public static readonly ExPerformanceCounter StoragePermanentExceptionPercent = new ExPerformanceCounter("MSExchange OWA", "Storage Permanent Exception Failure %", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000192 RID: 402
		private static readonly ExPerformanceCounter IMInstantMessagesSentPerSecond = new ExPerformanceCounter("MSExchange OWA", "IM - Messages Sent/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000193 RID: 403
		public static readonly ExPerformanceCounter IMTotalInstantMessagesSent = new ExPerformanceCounter("MSExchange OWA", "IM - Total Messages Sent", string.Empty, null, new ExPerformanceCounter[]
		{
			OwaSingleCounters.IMInstantMessagesSentPerSecond
		});

		// Token: 0x04000194 RID: 404
		private static readonly ExPerformanceCounter IMInstantMessagesReceivedPerSecond = new ExPerformanceCounter("MSExchange OWA", "IM - Messages Received/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000195 RID: 405
		public static readonly ExPerformanceCounter IMTotalInstantMessagesReceived = new ExPerformanceCounter("MSExchange OWA", "IM - Total Messages Received", string.Empty, null, new ExPerformanceCounter[]
		{
			OwaSingleCounters.IMInstantMessagesReceivedPerSecond
		});

		// Token: 0x04000196 RID: 406
		private static readonly ExPerformanceCounter IMPresenceQueriesPerSecond = new ExPerformanceCounter("MSExchange OWA", "IM - Presence Queries/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000197 RID: 407
		public static readonly ExPerformanceCounter IMTotalPresenceQueries = new ExPerformanceCounter("MSExchange OWA", "IM - Total Presence Queries", string.Empty, null, new ExPerformanceCounter[]
		{
			OwaSingleCounters.IMPresenceQueriesPerSecond
		});

		// Token: 0x04000198 RID: 408
		private static readonly ExPerformanceCounter IMMessageDeliveryFailuresPerSecond = new ExPerformanceCounter("MSExchange OWA", "IM - Message Delivery Failures/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000199 RID: 409
		public static readonly ExPerformanceCounter IMTotalMessageDeliveryFailures = new ExPerformanceCounter("MSExchange OWA", "IM - Total Message Delivery Failures", string.Empty, null, new ExPerformanceCounter[]
		{
			OwaSingleCounters.IMMessageDeliveryFailuresPerSecond
		});

		// Token: 0x0400019A RID: 410
		private static readonly ExPerformanceCounter IMLogonFailuresPerSecond = new ExPerformanceCounter("MSExchange OWA", "IM - Sign-In Failures/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400019B RID: 411
		public static readonly ExPerformanceCounter IMTotalLogonFailures = new ExPerformanceCounter("MSExchange OWA", "IM - Sign-In Failures", string.Empty, null, new ExPerformanceCounter[]
		{
			OwaSingleCounters.IMLogonFailuresPerSecond
		});

		// Token: 0x0400019C RID: 412
		public static readonly ExPerformanceCounter IMCurrentUsers = new ExPerformanceCounter("MSExchange OWA", "IM - Users Currently Signed In", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400019D RID: 413
		public static readonly ExPerformanceCounter IMTotalUsers = new ExPerformanceCounter("MSExchange OWA", "IM - Total Users", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400019E RID: 414
		public static readonly ExPerformanceCounter IMAverageSignOnTime = new ExPerformanceCounter("MSExchange OWA", "IM - Average Sign-In Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400019F RID: 415
		public static readonly ExPerformanceCounter IMLogonFailuresPercent = new ExPerformanceCounter("MSExchange OWA", "IM - Sign-In Failure %", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001A0 RID: 416
		public static readonly ExPerformanceCounter IMSentMessageDeliveryFailuresPercent = new ExPerformanceCounter("MSExchange OWA", "IM - Sent Message Delivery Failure %", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001A1 RID: 417
		public static readonly ExPerformanceCounter OwaToEwsRequestFailureRate = new ExPerformanceCounter("MSExchange OWA", "Failure rate of requests from OWA to EWS.", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001A2 RID: 418
		public static readonly ExPerformanceCounter RequestTimeouts = new ExPerformanceCounter("MSExchange OWA", "Request Time-Outs", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001A3 RID: 419
		public static readonly ExPerformanceCounter SenderPhotosTotalLDAPCallsWithPicture = new ExPerformanceCounter("MSExchange OWA", "Sender Photos - Total LDAP calls returned non-empty image data", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001A4 RID: 420
		private static readonly ExPerformanceCounter SenderPhotosLDAPCallsPerSecond = new ExPerformanceCounter("MSExchange OWA", "Sender Photos - LDAP calls/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001A5 RID: 421
		public static readonly ExPerformanceCounter SenderPhotosTotalLDAPCalls = new ExPerformanceCounter("MSExchange OWA", "Sender Photos - Total LDAP calls", string.Empty, null, new ExPerformanceCounter[]
		{
			OwaSingleCounters.SenderPhotosLDAPCallsPerSecond
		});

		// Token: 0x040001A6 RID: 422
		public static readonly ExPerformanceCounter SenderPhotosNegativeCacheCount = new ExPerformanceCounter("MSExchange OWA", "Sender Photos - Total entries in Recipients Negative Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001A7 RID: 423
		public static readonly ExPerformanceCounter SenderPhotosDataFromNegativeCacheCount = new ExPerformanceCounter("MSExchange OWA", "Sender Photos - Total number of avoided LDAP calls due to cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001A8 RID: 424
		public static readonly ExPerformanceCounter AggregatedUserConfigurationPartsRebuilt = new ExPerformanceCounter("MSExchange OWA", "Aggregated Configuration - Rebuilds", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001A9 RID: 425
		public static readonly ExPerformanceCounter AggregatedUserConfigurationPartsRead = new ExPerformanceCounter("MSExchange OWA", "Aggregated Configuration - Reads", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001AA RID: 426
		public static readonly ExPerformanceCounter AggregatedUserConfigurationPartsRequested = new ExPerformanceCounter("MSExchange OWA", "Aggregated Configuration - Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001AB RID: 427
		public static readonly ExPerformanceCounter SessionDataCacheBuildsStarted = new ExPerformanceCounter("MSExchange OWA", "Session Data Cache - build starts", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001AC RID: 428
		public static readonly ExPerformanceCounter SessionDataCacheBuildsCompleted = new ExPerformanceCounter("MSExchange OWA", "Session Data Cache - builds completed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001AD RID: 429
		public static readonly ExPerformanceCounter SessionDataCacheWaitedForPreload = new ExPerformanceCounter("MSExchange OWA", "Session Data Cache - waited for preload to complete", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001AE RID: 430
		public static readonly ExPerformanceCounter SessionDataCacheUsed = new ExPerformanceCounter("MSExchange OWA", "Session Data Cache - used", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001AF RID: 431
		public static readonly ExPerformanceCounter SessionDataCacheWaitTimeout = new ExPerformanceCounter("MSExchange OWA", "Session Data Cache - timeout", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001B0 RID: 432
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			OwaSingleCounters.CurrentUsers,
			OwaSingleCounters.CurrentUniqueUsers,
			OwaSingleCounters.TotalUsers,
			OwaSingleCounters.TotalUniqueUsers,
			OwaSingleCounters.PeakUserCount,
			OwaSingleCounters.CurrentUsersLight,
			OwaSingleCounters.CurrentUniqueUsersLight,
			OwaSingleCounters.TotalUsersLight,
			OwaSingleCounters.TotalUniqueUsersLight,
			OwaSingleCounters.PeakUserCountLight,
			OwaSingleCounters.CurrentUsersPremium,
			OwaSingleCounters.CurrentUniqueUsersPremium,
			OwaSingleCounters.TotalUsersPremium,
			OwaSingleCounters.TotalUniqueUsersPremium,
			OwaSingleCounters.PeakUserCountPremium,
			OwaSingleCounters.AverageResponseTime,
			OwaSingleCounters.TotalSessionsEndedByLogoff,
			OwaSingleCounters.TotalSessionsEndedByTimeout,
			OwaSingleCounters.CalendarViewsLoaded,
			OwaSingleCounters.CalendarViewsRefreshed,
			OwaSingleCounters.ItemsCreated,
			OwaSingleCounters.ItemsUpdated,
			OwaSingleCounters.ItemsDeleted,
			OwaSingleCounters.AttachmentsUploaded,
			OwaSingleCounters.MailViewsLoaded,
			OwaSingleCounters.MailViewRefreshes,
			OwaSingleCounters.MessagesSent,
			OwaSingleCounters.IRMMessagesSent,
			OwaSingleCounters.AverageSearchTime,
			OwaSingleCounters.TotalSearches,
			OwaSingleCounters.SearchesTimedOut,
			OwaSingleCounters.TotalRequests,
			OwaSingleCounters.TotalRequestsFailed,
			OwaSingleCounters.PID,
			OwaSingleCounters.AverageSpellcheckTime,
			OwaSingleCounters.TotalSpellchecks,
			OwaSingleCounters.TotalConversions,
			OwaSingleCounters.ActiveConversions,
			OwaSingleCounters.TotalRejectedConversions,
			OwaSingleCounters.QueuedConversionRequests,
			OwaSingleCounters.TotalTimeoutConversions,
			OwaSingleCounters.TotalErrorConversions,
			OwaSingleCounters.TotalConvertingRequestsRate,
			OwaSingleCounters.SuccessfulConversionRequestRate,
			OwaSingleCounters.TotalConvertingResponseRate,
			OwaSingleCounters.AverageConvertingTime,
			OwaSingleCounters.AverageConversionQueuingTime,
			OwaSingleCounters.InvalidCanaryRequests,
			OwaSingleCounters.NamesChecked,
			OwaSingleCounters.PasswordChanges,
			OwaSingleCounters.CurrentProxiedUsers,
			OwaSingleCounters.ProxiedUserRequests,
			OwaSingleCounters.ProxiedResponseTimeAverage,
			OwaSingleCounters.ProxyRequestBytes,
			OwaSingleCounters.ProxyResponseBytes,
			OwaSingleCounters.WssBytes,
			OwaSingleCounters.UncBytes,
			OwaSingleCounters.WssRequests,
			OwaSingleCounters.UncRequests,
			OwaSingleCounters.ASQueries,
			OwaSingleCounters.ASQueriesFailurePercent,
			OwaSingleCounters.StoreLogonFailurePercent,
			OwaSingleCounters.CASIntraSiteRedirectionLatertoEarlierVersion,
			OwaSingleCounters.CASIntraSiteRedirectionEarliertoLaterVersion,
			OwaSingleCounters.CASCrossSiteRedirectionLatertoEarlierVersion,
			OwaSingleCounters.CASCrossSiteRedirectionEarliertoLaterVersion,
			OwaSingleCounters.ActiveMailboxSubscriptions,
			OwaSingleCounters.TotalMailboxNotifications,
			OwaSingleCounters.TotalUserContextReInitializationRequests,
			OwaSingleCounters.MailboxOfflineExceptionFailuresPercent,
			OwaSingleCounters.ConnectionFailedTransientExceptionPercent,
			OwaSingleCounters.StorageTransientExceptionPercent,
			OwaSingleCounters.StoragePermanentExceptionPercent,
			OwaSingleCounters.IMTotalInstantMessagesSent,
			OwaSingleCounters.IMTotalInstantMessagesReceived,
			OwaSingleCounters.IMTotalPresenceQueries,
			OwaSingleCounters.IMTotalMessageDeliveryFailures,
			OwaSingleCounters.IMTotalLogonFailures,
			OwaSingleCounters.IMCurrentUsers,
			OwaSingleCounters.IMTotalUsers,
			OwaSingleCounters.IMAverageSignOnTime,
			OwaSingleCounters.IMLogonFailuresPercent,
			OwaSingleCounters.IMSentMessageDeliveryFailuresPercent,
			OwaSingleCounters.OwaToEwsRequestFailureRate,
			OwaSingleCounters.RequestTimeouts,
			OwaSingleCounters.SenderPhotosTotalLDAPCalls,
			OwaSingleCounters.SenderPhotosTotalLDAPCallsWithPicture,
			OwaSingleCounters.SenderPhotosNegativeCacheCount,
			OwaSingleCounters.SenderPhotosDataFromNegativeCacheCount,
			OwaSingleCounters.AggregatedUserConfigurationPartsRebuilt,
			OwaSingleCounters.AggregatedUserConfigurationPartsRead,
			OwaSingleCounters.AggregatedUserConfigurationPartsRequested,
			OwaSingleCounters.SessionDataCacheBuildsStarted,
			OwaSingleCounters.SessionDataCacheBuildsCompleted,
			OwaSingleCounters.SessionDataCacheWaitedForPreload,
			OwaSingleCounters.SessionDataCacheUsed,
			OwaSingleCounters.SessionDataCacheWaitTimeout
		};
	}
}
