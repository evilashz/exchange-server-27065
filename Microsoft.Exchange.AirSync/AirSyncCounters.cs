using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000005 RID: 5
	internal static class AirSyncCounters
	{
		// Token: 0x06000002 RID: 2 RVA: 0x0000258C File Offset: 0x0000078C
		public static void GetPerfCounterInfo(XElement element)
		{
			if (AirSyncCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in AirSyncCounters.AllCounters)
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

		// Token: 0x04000084 RID: 132
		public const string CategoryName = "MSExchange ActiveSync";

		// Token: 0x04000085 RID: 133
		public static readonly ExPerformanceCounter CurrentNumberOfRequests = new ExPerformanceCounter("MSExchange ActiveSync", "Current Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000086 RID: 134
		public static readonly ExPerformanceCounter NumberOfIncomingProxyRequests = new ExPerformanceCounter("MSExchange ActiveSync", "Incoming Proxy Requests Total", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000087 RID: 135
		public static readonly ExPerformanceCounter NumberOfOutgoingProxyRequests = new ExPerformanceCounter("MSExchange ActiveSync", "Outgoing Proxy Requests Total", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000088 RID: 136
		public static readonly ExPerformanceCounter NumberOfWrongCASProxyRequests = new ExPerformanceCounter("MSExchange ActiveSync", "Wrong CAS Proxy Requests Total", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000089 RID: 137
		public static readonly ExPerformanceCounter PID = new ExPerformanceCounter("MSExchange ActiveSync", "PID", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400008A RID: 138
		public static readonly ExPerformanceCounter AverageRequestTime = new ExPerformanceCounter("MSExchange ActiveSync", "Average Request Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400008B RID: 139
		public static readonly ExPerformanceCounter AverageHangingTime = new ExPerformanceCounter("MSExchange ActiveSync", "Average Hang Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400008C RID: 140
		private static readonly ExPerformanceCounter RateOfRequests = new ExPerformanceCounter("MSExchange ActiveSync", "Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400008D RID: 141
		public static readonly ExPerformanceCounter NumberOfRequests = new ExPerformanceCounter("MSExchange ActiveSync", "Requests Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfRequests
		});

		// Token: 0x0400008E RID: 142
		private static readonly ExPerformanceCounter RateOfGetHierarchy = new ExPerformanceCounter("MSExchange ActiveSync", "Get Hierarchy Commands/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400008F RID: 143
		public static readonly ExPerformanceCounter NumberOfGetHierarchy = new ExPerformanceCounter("MSExchange ActiveSync", "Get Hierarchy Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfGetHierarchy
		});

		// Token: 0x04000090 RID: 144
		private static readonly ExPerformanceCounter RateOfMoveItems = new ExPerformanceCounter("MSExchange ActiveSync", "Move Items Commands/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000091 RID: 145
		public static readonly ExPerformanceCounter NumberOfMoveItems = new ExPerformanceCounter("MSExchange ActiveSync", "Move Items Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfMoveItems
		});

		// Token: 0x04000092 RID: 146
		private static readonly ExPerformanceCounter RateOfMeetingResponse = new ExPerformanceCounter("MSExchange ActiveSync", "Meeting Response Commands/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000093 RID: 147
		public static readonly ExPerformanceCounter NumberOfMeetingResponse = new ExPerformanceCounter("MSExchange ActiveSync", "Meeting Response Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfMeetingResponse
		});

		// Token: 0x04000094 RID: 148
		private static readonly ExPerformanceCounter RateOfFolderSyncsCurrent = new ExPerformanceCounter("MSExchange ActiveSync", "Folder Sync Commands/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000095 RID: 149
		public static readonly ExPerformanceCounter NumberOfFolderSyncs = new ExPerformanceCounter("MSExchange ActiveSync", "Folder Sync Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfFolderSyncsCurrent
		});

		// Token: 0x04000096 RID: 150
		private static readonly ExPerformanceCounter RateOfFolderUpdatesCurrent = new ExPerformanceCounter("MSExchange ActiveSync", "Folder Update Commands/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000097 RID: 151
		public static readonly ExPerformanceCounter NumberOfFolderUpdates = new ExPerformanceCounter("MSExchange ActiveSync", "Folder Update Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfFolderUpdatesCurrent
		});

		// Token: 0x04000098 RID: 152
		private static readonly ExPerformanceCounter RateOfFolderCreatesCurrent = new ExPerformanceCounter("MSExchange ActiveSync", "Folder Create Commands/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000099 RID: 153
		public static readonly ExPerformanceCounter NumberOfFolderCreates = new ExPerformanceCounter("MSExchange ActiveSync", "Folder Create Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfFolderCreatesCurrent
		});

		// Token: 0x0400009A RID: 154
		private static readonly ExPerformanceCounter RateOfFolderDeletesCurrent = new ExPerformanceCounter("MSExchange ActiveSync", "Folder Delete Commands/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400009B RID: 155
		public static readonly ExPerformanceCounter NumberOfFolderDeletes = new ExPerformanceCounter("MSExchange ActiveSync", "Folder Delete Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfFolderDeletesCurrent
		});

		// Token: 0x0400009C RID: 156
		private static readonly ExPerformanceCounter RateOfOptionsCurrent = new ExPerformanceCounter("MSExchange ActiveSync", "Options Commands/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400009D RID: 157
		public static readonly ExPerformanceCounter NumberOfOptions = new ExPerformanceCounter("MSExchange ActiveSync", "Options Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfOptionsCurrent
		});

		// Token: 0x0400009E RID: 158
		private static readonly ExPerformanceCounter RateOfSyncRequests = new ExPerformanceCounter("MSExchange ActiveSync", "Sync Commands/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400009F RID: 159
		public static readonly ExPerformanceCounter NumberOfSyncRequests = new ExPerformanceCounter("MSExchange ActiveSync", "Sync Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfSyncRequests
		});

		// Token: 0x040000A0 RID: 160
		private static readonly ExPerformanceCounter RateOfRecoverySyncRequests = new ExPerformanceCounter("MSExchange ActiveSync", "Recovery Sync Commands/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000A1 RID: 161
		public static readonly ExPerformanceCounter NumberOfRecoverySyncRequests = new ExPerformanceCounter("MSExchange ActiveSync", "Recovery Sync Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfRecoverySyncRequests
		});

		// Token: 0x040000A2 RID: 162
		private static readonly ExPerformanceCounter RateOfItemEstimateRequests = new ExPerformanceCounter("MSExchange ActiveSync", "Get Item Estimate Commands/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000A3 RID: 163
		public static readonly ExPerformanceCounter NumberOfItemEstimateRequests = new ExPerformanceCounter("MSExchange ActiveSync", "Get Item Estimate Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfItemEstimateRequests
		});

		// Token: 0x040000A4 RID: 164
		private static readonly ExPerformanceCounter RateOfCreateCollectionsCurrent = new ExPerformanceCounter("MSExchange ActiveSync", "Create Collection Commands/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000A5 RID: 165
		public static readonly ExPerformanceCounter NumberOfCreateCollections = new ExPerformanceCounter("MSExchange ActiveSync", "Create Collection Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfCreateCollectionsCurrent
		});

		// Token: 0x040000A6 RID: 166
		private static readonly ExPerformanceCounter RateOfMoveCollectionsCurrent = new ExPerformanceCounter("MSExchange ActiveSync", "Move Collection Commands/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000A7 RID: 167
		public static readonly ExPerformanceCounter NumberOfMoveCollections = new ExPerformanceCounter("MSExchange ActiveSync", "Move Collection Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfMoveCollectionsCurrent
		});

		// Token: 0x040000A8 RID: 168
		private static readonly ExPerformanceCounter RateOfDeleteCollectionsCurrent = new ExPerformanceCounter("MSExchange ActiveSync", "Delete Collection Commands/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000A9 RID: 169
		public static readonly ExPerformanceCounter NumberOfDeleteCollections = new ExPerformanceCounter("MSExchange ActiveSync", "Delete Collection Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfDeleteCollectionsCurrent
		});

		// Token: 0x040000AA RID: 170
		private static readonly ExPerformanceCounter RateOfGetAttachmentsCurrent = new ExPerformanceCounter("MSExchange ActiveSync", "Get Attachment Commands/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000AB RID: 171
		public static readonly ExPerformanceCounter NumberOfGetAttachments = new ExPerformanceCounter("MSExchange ActiveSync", "Get Attachment Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfGetAttachmentsCurrent
		});

		// Token: 0x040000AC RID: 172
		private static readonly ExPerformanceCounter RateOfIRMMailsDownloadsCurrent = new ExPerformanceCounter("MSExchange ActiveSync", "IRM-protected Message Downloads/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000AD RID: 173
		public static readonly ExPerformanceCounter NumberOfIRMMailsDownloads = new ExPerformanceCounter("MSExchange ActiveSync", "IRM-protected Message Downloads - Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfIRMMailsDownloadsCurrent
		});

		// Token: 0x040000AE RID: 174
		private static readonly ExPerformanceCounter RateOfSendIRMMailsCurrent = new ExPerformanceCounter("MSExchange ActiveSync", "Send IRM-protected Messages/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000AF RID: 175
		public static readonly ExPerformanceCounter NumberOfSendIRMMails = new ExPerformanceCounter("MSExchange ActiveSync", "Send IRM-protected Messages - Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfSendIRMMailsCurrent
		});

		// Token: 0x040000B0 RID: 176
		private static readonly ExPerformanceCounter RateOfSendMailsCurrent = new ExPerformanceCounter("MSExchange ActiveSync", "Send Mail Commands/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000B1 RID: 177
		public static readonly ExPerformanceCounter NumberOfSendMails = new ExPerformanceCounter("MSExchange ActiveSync", "Send Mail Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfSendMailsCurrent
		});

		// Token: 0x040000B2 RID: 178
		private static readonly ExPerformanceCounter RateOfSmartReplysCurrent = new ExPerformanceCounter("MSExchange ActiveSync", "Smart Reply Commands/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000B3 RID: 179
		public static readonly ExPerformanceCounter NumberOfSmartReplys = new ExPerformanceCounter("MSExchange ActiveSync", "Smart Reply Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfSmartReplysCurrent
		});

		// Token: 0x040000B4 RID: 180
		private static readonly ExPerformanceCounter RateOfSmartForwardsCurrent = new ExPerformanceCounter("MSExchange ActiveSync", "Smart Forward Commands/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000B5 RID: 181
		public static readonly ExPerformanceCounter NumberOfSmartForwards = new ExPerformanceCounter("MSExchange ActiveSync", "Smart Forward Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfSmartForwardsCurrent
		});

		// Token: 0x040000B6 RID: 182
		private static readonly ExPerformanceCounter RateOfSearchesCurrent = new ExPerformanceCounter("MSExchange ActiveSync", "Search Commands/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000B7 RID: 183
		public static readonly ExPerformanceCounter NumberOfSearches = new ExPerformanceCounter("MSExchange ActiveSync", "Search Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfSearchesCurrent
		});

		// Token: 0x040000B8 RID: 184
		private static readonly ExPerformanceCounter RateOfGALSearchesCurrent = new ExPerformanceCounter("MSExchange ActiveSync", "GAL Searches/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000B9 RID: 185
		public static readonly ExPerformanceCounter NumberOfGALSearches = new ExPerformanceCounter("MSExchange ActiveSync", "GAL Search Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfGALSearchesCurrent
		});

		// Token: 0x040000BA RID: 186
		private static readonly ExPerformanceCounter RateOfMailboxSearchesCurrent = new ExPerformanceCounter("MSExchange ActiveSync", "Mailbox Searches/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000BB RID: 187
		public static readonly ExPerformanceCounter NumberOfMailboxSearches = new ExPerformanceCounter("MSExchange ActiveSync", "Mailbox Search Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfMailboxSearchesCurrent
		});

		// Token: 0x040000BC RID: 188
		private static readonly ExPerformanceCounter RateOfDocumentLibrarySearchesCurrent = new ExPerformanceCounter("MSExchange ActiveSync", "Document Library Searches/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000BD RID: 189
		public static readonly ExPerformanceCounter NumberOfDocumentLibrarySearches = new ExPerformanceCounter("MSExchange ActiveSync", "Document Library Search Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfDocumentLibrarySearchesCurrent
		});

		// Token: 0x040000BE RID: 190
		private static readonly ExPerformanceCounter RateOfItemOperationsCurrent = new ExPerformanceCounter("MSExchange ActiveSync", "Item Operations Commands/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000BF RID: 191
		public static readonly ExPerformanceCounter NumberOfItemOperations = new ExPerformanceCounter("MSExchange ActiveSync", "Item Operations Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfItemOperationsCurrent
		});

		// Token: 0x040000C0 RID: 192
		private static readonly ExPerformanceCounter RateOfDocumentLibraryFetchesCurrent = new ExPerformanceCounter("MSExchange ActiveSync", "Document Library Fetch Commands/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000C1 RID: 193
		public static readonly ExPerformanceCounter NumberOfDocumentLibraryFetches = new ExPerformanceCounter("MSExchange ActiveSync", "Document Library Fetch Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfDocumentLibraryFetchesCurrent
		});

		// Token: 0x040000C2 RID: 194
		private static readonly ExPerformanceCounter RateOfMailboxItemFetchesCurrent = new ExPerformanceCounter("MSExchange ActiveSync", "Mailbox Item Fetch Commands/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000C3 RID: 195
		public static readonly ExPerformanceCounter NumberOfMailboxItemFetches = new ExPerformanceCounter("MSExchange ActiveSync", "Mailbox Item Fetch Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfMailboxItemFetchesCurrent
		});

		// Token: 0x040000C4 RID: 196
		private static readonly ExPerformanceCounter RateOfEmptyFolderContentsCurrent = new ExPerformanceCounter("MSExchange ActiveSync", "Empty Folder Contents/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000C5 RID: 197
		public static readonly ExPerformanceCounter NumberOfEmptyFolderContents = new ExPerformanceCounter("MSExchange ActiveSync", "Empty Folder Contents Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfEmptyFolderContentsCurrent
		});

		// Token: 0x040000C6 RID: 198
		private static readonly ExPerformanceCounter RateOfMailboxAttachmentFetchesCurrent = new ExPerformanceCounter("MSExchange ActiveSync", "Mailbox Attachment Fetch Commands/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000C7 RID: 199
		public static readonly ExPerformanceCounter NumberOfMailboxAttachmentFetches = new ExPerformanceCounter("MSExchange ActiveSync", "Mailbox Attachment Fetch Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfMailboxAttachmentFetchesCurrent
		});

		// Token: 0x040000C8 RID: 200
		private static readonly ExPerformanceCounter RateOfSettingsRequestCurrent = new ExPerformanceCounter("MSExchange ActiveSync", "Settings Commands/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000C9 RID: 201
		public static readonly ExPerformanceCounter NumberOfSettingsRequests = new ExPerformanceCounter("MSExchange ActiveSync", "Settings Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfSettingsRequestCurrent
		});

		// Token: 0x040000CA RID: 202
		private static readonly ExPerformanceCounter RateOfPing = new ExPerformanceCounter("MSExchange ActiveSync", "Ping Commands/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000CB RID: 203
		public static readonly ExPerformanceCounter NumberOfPing = new ExPerformanceCounter("MSExchange ActiveSync", "Ping Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfPing
		});

		// Token: 0x040000CC RID: 204
		public static readonly ExPerformanceCounter CurrentlyPendingPing = new ExPerformanceCounter("MSExchange ActiveSync", "Ping Commands Pending", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000CD RID: 205
		private static readonly ExPerformanceCounter RateOfDroppedPing = new ExPerformanceCounter("MSExchange ActiveSync", "Ping Commands Dropped/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000CE RID: 206
		public static readonly ExPerformanceCounter NumberOfDroppedPing = new ExPerformanceCounter("MSExchange ActiveSync", "Ping Dropped Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfDroppedPing
		});

		// Token: 0x040000CF RID: 207
		public static readonly ExPerformanceCounter HeartbeatInterval = new ExPerformanceCounter("MSExchange ActiveSync", "Heartbeat Interval", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000D0 RID: 208
		private static readonly ExPerformanceCounter RateOfProvisionRequests = new ExPerformanceCounter("MSExchange ActiveSync", "Provision Commands/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000D1 RID: 209
		public static readonly ExPerformanceCounter NumberOfProvisionRequests = new ExPerformanceCounter("MSExchange ActiveSync", "Provision Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfProvisionRequests
		});

		// Token: 0x040000D2 RID: 210
		public static readonly ExPerformanceCounter NumberOfServerItemConversionFailure = new ExPerformanceCounter("MSExchange ActiveSync", "Failed Item Conversion Total", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000D3 RID: 211
		public static readonly ExPerformanceCounter NumberOfBadItemReportsGenerated = new ExPerformanceCounter("MSExchange ActiveSync", "Bad Item Reports Generated Total", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000D4 RID: 212
		public static readonly ExPerformanceCounter NumberOfProxyLoginSent = new ExPerformanceCounter("MSExchange ActiveSync", "Proxy Logon Commands Sent Total", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000D5 RID: 213
		public static readonly ExPerformanceCounter NumberOfProxyLoginReceived = new ExPerformanceCounter("MSExchange ActiveSync", "Proxy Logon Received Total", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000D6 RID: 214
		public static readonly ExPerformanceCounter SyncStateKbLeftCompressed = new ExPerformanceCounter("MSExchange ActiveSync", "Sync State KBytes Left Compressed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000D7 RID: 215
		public static readonly ExPerformanceCounter SyncStateKbTotal = new ExPerformanceCounter("MSExchange ActiveSync", "Sync State KBytes Total", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000D8 RID: 216
		public static readonly ExPerformanceCounter CurrentlyPendingSync = new ExPerformanceCounter("MSExchange ActiveSync", "Sync Commands Pending", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000D9 RID: 217
		private static readonly ExPerformanceCounter RateOfDroppedSync = new ExPerformanceCounter("MSExchange ActiveSync", "Sync Commands Dropped/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000DA RID: 218
		public static readonly ExPerformanceCounter NumberOfDroppedSync = new ExPerformanceCounter("MSExchange ActiveSync", "Sync Dropped Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfDroppedSync
		});

		// Token: 0x040000DB RID: 219
		private static readonly ExPerformanceCounter RateOfConflictingConcurrentSync = new ExPerformanceCounter("MSExchange ActiveSync", "Conflicting Concurrent Sync/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000DC RID: 220
		public static readonly ExPerformanceCounter NumberOfConflictingConcurrentSync = new ExPerformanceCounter("MSExchange ActiveSync", "Conflicting Concurrent Sync Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfConflictingConcurrentSync
		});

		// Token: 0x040000DD RID: 221
		public static readonly ExPerformanceCounter NumberOfADPolicyQueriesOnReconnect = new ExPerformanceCounter("MSExchange ActiveSync", "Number of AD Policy Queries on Reconnect", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000DE RID: 222
		public static readonly ExPerformanceCounter NumberOfNotificationManagerInCache = new ExPerformanceCounter("MSExchange ActiveSync", "Number of Notification Manager Objects in Memory", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000DF RID: 223
		private static readonly ExPerformanceCounter RateOfAvailabilityRequests = new ExPerformanceCounter("MSExchange ActiveSync", "Availability Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000E0 RID: 224
		public static readonly ExPerformanceCounter NumberOfAvailabilityRequests = new ExPerformanceCounter("MSExchange ActiveSync", "Availability Requests Total", string.Empty, null, new ExPerformanceCounter[]
		{
			AirSyncCounters.RateOfAvailabilityRequests
		});

		// Token: 0x040000E1 RID: 225
		public static readonly ExPerformanceCounter RatePerMinuteOfTransientMailboxConnectionFailures = new ExPerformanceCounter("MSExchange ActiveSync", "Transient Mailbox Connection Failures/minute", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000E2 RID: 226
		public static readonly ExPerformanceCounter RatePerMinuteOfMailboxOfflineErrors = new ExPerformanceCounter("MSExchange ActiveSync", "Mailbox Offline Errors/minute", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000E3 RID: 227
		public static readonly ExPerformanceCounter RatePerMinuteOfTransientStorageErrors = new ExPerformanceCounter("MSExchange ActiveSync", "Transient Storage Errors/minute", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000E4 RID: 228
		public static readonly ExPerformanceCounter RatePerMinuteOfPermanentStorageErrors = new ExPerformanceCounter("MSExchange ActiveSync", "Permanent Storage Errors/minute", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000E5 RID: 229
		public static readonly ExPerformanceCounter RatePerMinuteOfTransientActiveDirectoryErrors = new ExPerformanceCounter("MSExchange ActiveSync", "Transient Active Directory Errors/minute", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000E6 RID: 230
		public static readonly ExPerformanceCounter RatePerMinuteOfPermanentActiveDirectoryErrors = new ExPerformanceCounter("MSExchange ActiveSync", "Permanent Active Directory Errors/minute", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000E7 RID: 231
		public static readonly ExPerformanceCounter RatePerMinuteOfTransientErrors = new ExPerformanceCounter("MSExchange ActiveSync", "Transient Errors/minute", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000E8 RID: 232
		public static readonly ExPerformanceCounter AverageRpcLatency = new ExPerformanceCounter("MSExchange ActiveSync", "Average RPC Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000E9 RID: 233
		public static readonly ExPerformanceCounter AverageLdapLatency = new ExPerformanceCounter("MSExchange ActiveSync", "Average LDAP Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000EA RID: 234
		public static readonly ExPerformanceCounter AutoBlockedDevices = new ExPerformanceCounter("MSExchange ActiveSync", "Number of auto-blocked devices", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000EB RID: 235
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			AirSyncCounters.NumberOfRequests,
			AirSyncCounters.CurrentNumberOfRequests,
			AirSyncCounters.NumberOfIncomingProxyRequests,
			AirSyncCounters.NumberOfOutgoingProxyRequests,
			AirSyncCounters.NumberOfWrongCASProxyRequests,
			AirSyncCounters.PID,
			AirSyncCounters.AverageRequestTime,
			AirSyncCounters.AverageHangingTime,
			AirSyncCounters.NumberOfGetHierarchy,
			AirSyncCounters.NumberOfMoveItems,
			AirSyncCounters.NumberOfMeetingResponse,
			AirSyncCounters.NumberOfFolderSyncs,
			AirSyncCounters.NumberOfFolderUpdates,
			AirSyncCounters.NumberOfFolderCreates,
			AirSyncCounters.NumberOfFolderDeletes,
			AirSyncCounters.NumberOfOptions,
			AirSyncCounters.NumberOfSyncRequests,
			AirSyncCounters.NumberOfRecoverySyncRequests,
			AirSyncCounters.NumberOfItemEstimateRequests,
			AirSyncCounters.NumberOfCreateCollections,
			AirSyncCounters.NumberOfMoveCollections,
			AirSyncCounters.NumberOfDeleteCollections,
			AirSyncCounters.NumberOfGetAttachments,
			AirSyncCounters.NumberOfIRMMailsDownloads,
			AirSyncCounters.NumberOfSendIRMMails,
			AirSyncCounters.NumberOfSendMails,
			AirSyncCounters.NumberOfSmartReplys,
			AirSyncCounters.NumberOfSmartForwards,
			AirSyncCounters.NumberOfSearches,
			AirSyncCounters.NumberOfGALSearches,
			AirSyncCounters.NumberOfMailboxSearches,
			AirSyncCounters.NumberOfDocumentLibrarySearches,
			AirSyncCounters.NumberOfItemOperations,
			AirSyncCounters.NumberOfDocumentLibraryFetches,
			AirSyncCounters.NumberOfMailboxItemFetches,
			AirSyncCounters.NumberOfEmptyFolderContents,
			AirSyncCounters.NumberOfMailboxAttachmentFetches,
			AirSyncCounters.NumberOfSettingsRequests,
			AirSyncCounters.NumberOfPing,
			AirSyncCounters.CurrentlyPendingPing,
			AirSyncCounters.NumberOfDroppedPing,
			AirSyncCounters.HeartbeatInterval,
			AirSyncCounters.NumberOfProvisionRequests,
			AirSyncCounters.NumberOfServerItemConversionFailure,
			AirSyncCounters.NumberOfBadItemReportsGenerated,
			AirSyncCounters.NumberOfProxyLoginSent,
			AirSyncCounters.NumberOfProxyLoginReceived,
			AirSyncCounters.SyncStateKbLeftCompressed,
			AirSyncCounters.SyncStateKbTotal,
			AirSyncCounters.CurrentlyPendingSync,
			AirSyncCounters.NumberOfDroppedSync,
			AirSyncCounters.NumberOfConflictingConcurrentSync,
			AirSyncCounters.NumberOfADPolicyQueriesOnReconnect,
			AirSyncCounters.NumberOfNotificationManagerInCache,
			AirSyncCounters.NumberOfAvailabilityRequests,
			AirSyncCounters.RatePerMinuteOfTransientMailboxConnectionFailures,
			AirSyncCounters.RatePerMinuteOfMailboxOfflineErrors,
			AirSyncCounters.RatePerMinuteOfTransientStorageErrors,
			AirSyncCounters.RatePerMinuteOfPermanentStorageErrors,
			AirSyncCounters.RatePerMinuteOfTransientActiveDirectoryErrors,
			AirSyncCounters.RatePerMinuteOfPermanentActiveDirectoryErrors,
			AirSyncCounters.RatePerMinuteOfTransientErrors,
			AirSyncCounters.AverageRpcLatency,
			AirSyncCounters.AverageLdapLatency,
			AirSyncCounters.AutoBlockedDevices
		};
	}
}
