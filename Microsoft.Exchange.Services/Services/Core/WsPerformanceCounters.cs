using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000F7B RID: 3963
	internal static class WsPerformanceCounters
	{
		// Token: 0x06006770 RID: 26480 RVA: 0x001447E4 File Offset: 0x001429E4
		public static void GetPerfCounterInfo(XElement element)
		{
			if (WsPerformanceCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in WsPerformanceCounters.AllCounters)
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

		// Token: 0x040038D1 RID: 14545
		public const string CategoryName = "MSExchangeWS";

		// Token: 0x040038D2 RID: 14546
		private static readonly ExPerformanceCounter AddAggregatedAccountRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "AddAggregatedAccount Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038D3 RID: 14547
		public static readonly ExPerformanceCounter TotalAddAggregatedAccountRequests = new ExPerformanceCounter("MSExchangeWS", "AddAggregatedAccount Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.AddAggregatedAccountRequestsPerSecond
		});

		// Token: 0x040038D4 RID: 14548
		public static readonly ExPerformanceCounter AddAggregatedAccountAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "AddAggregatedAccount Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038D5 RID: 14549
		private static readonly ExPerformanceCounter IsOffice365DomainRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "IsOffice365Domain Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038D6 RID: 14550
		public static readonly ExPerformanceCounter TotalIsOffice365DomainRequests = new ExPerformanceCounter("MSExchangeWS", "IsOffice365Domain Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.IsOffice365DomainRequestsPerSecond
		});

		// Token: 0x040038D7 RID: 14551
		public static readonly ExPerformanceCounter IsOffice365DomainAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "IsOffice365Domain Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038D8 RID: 14552
		private static readonly ExPerformanceCounter GetAggregatedAccountRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetAggregatedAccount Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038D9 RID: 14553
		public static readonly ExPerformanceCounter TotalGetAggregatedAccountRequests = new ExPerformanceCounter("MSExchangeWS", "GetAggregatedAccount Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetAggregatedAccountRequestsPerSecond
		});

		// Token: 0x040038DA RID: 14554
		public static readonly ExPerformanceCounter GetAggregatedAccountAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetAggregatedAccount Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038DB RID: 14555
		private static readonly ExPerformanceCounter RemoveAggregatedAccountRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "RemoveAggregatedAccount Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038DC RID: 14556
		public static readonly ExPerformanceCounter TotalRemoveAggregatedAccountRequests = new ExPerformanceCounter("MSExchangeWS", "RemoveAggregatedAccount Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.RemoveAggregatedAccountRequestsPerSecond
		});

		// Token: 0x040038DD RID: 14557
		public static readonly ExPerformanceCounter RemoveAggregatedAccountAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "RemoveAggregatedAccount Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038DE RID: 14558
		private static readonly ExPerformanceCounter SetAggregatedAccountRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "SetAggregatedAccount Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038DF RID: 14559
		public static readonly ExPerformanceCounter TotalSetAggregatedAccountRequests = new ExPerformanceCounter("MSExchangeWS", "SetAggregatedAccount Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.SetAggregatedAccountRequestsPerSecond
		});

		// Token: 0x040038E0 RID: 14560
		public static readonly ExPerformanceCounter SetAggregatedAccountAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "SetAggregatedAccount Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038E1 RID: 14561
		private static readonly ExPerformanceCounter GetItemRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetItem Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038E2 RID: 14562
		public static readonly ExPerformanceCounter TotalGetItemRequests = new ExPerformanceCounter("MSExchangeWS", "GetItem Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetItemRequestsPerSecond
		});

		// Token: 0x040038E3 RID: 14563
		public static readonly ExPerformanceCounter GetItemAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetItem Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038E4 RID: 14564
		private static readonly ExPerformanceCounter ConvertIdRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "ConvertId Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038E5 RID: 14565
		public static readonly ExPerformanceCounter TotalConvertIdRequests = new ExPerformanceCounter("MSExchangeWS", "ConvertId Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.ConvertIdRequestsPerSecond
		});

		// Token: 0x040038E6 RID: 14566
		public static readonly ExPerformanceCounter ConvertIdAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "ConvertId Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038E7 RID: 14567
		private static readonly ExPerformanceCounter TotalIdsConvertedPerSecond = new ExPerformanceCounter("MSExchangeWS", "Ids Converted/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038E8 RID: 14568
		public static readonly ExPerformanceCounter TotalIdsConverted = new ExPerformanceCounter("MSExchangeWS", "Total Number of Ids converted", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.TotalIdsConvertedPerSecond
		});

		// Token: 0x040038E9 RID: 14569
		private static readonly ExPerformanceCounter CreateItemRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "CreateItem Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038EA RID: 14570
		public static readonly ExPerformanceCounter TotalCreateItemRequests = new ExPerformanceCounter("MSExchangeWS", "CreateItem Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.CreateItemRequestsPerSecond
		});

		// Token: 0x040038EB RID: 14571
		public static readonly ExPerformanceCounter CreateItemAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "CreateItem Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038EC RID: 14572
		private static readonly ExPerformanceCounter PostModernGroupItem = new ExPerformanceCounter("MSExchangeWS", "PostModernGroupItem Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038ED RID: 14573
		public static readonly ExPerformanceCounter TotalPostModernGroupItemRequests = new ExPerformanceCounter("MSExchangeWS", "PostModernGroupItem Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.PostModernGroupItem
		});

		// Token: 0x040038EE RID: 14574
		public static readonly ExPerformanceCounter PostModernGroupItemAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "PostModernGroupItem Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038EF RID: 14575
		private static readonly ExPerformanceCounter UpdateAndPostModernGroupItem = new ExPerformanceCounter("MSExchangeWS", "UpdateAndPostModernGroupItem Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038F0 RID: 14576
		public static readonly ExPerformanceCounter TotalUpdateAndPostModernGroupItemRequests = new ExPerformanceCounter("MSExchangeWS", "UpdateAndPostModernGroupItem Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.UpdateAndPostModernGroupItem
		});

		// Token: 0x040038F1 RID: 14577
		public static readonly ExPerformanceCounter UpdateAndPostModernGroupItemAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "UpdateAndPostModernGroupItem Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038F2 RID: 14578
		private static readonly ExPerformanceCounter CreateResponseFromModernGroupRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "CreateResponseFromModernGroup Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038F3 RID: 14579
		public static readonly ExPerformanceCounter TotalCreateResponseFromModernGroupRequests = new ExPerformanceCounter("MSExchangeWS", "CreateResponseFromModernGroup Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.CreateResponseFromModernGroupRequestsPerSecond
		});

		// Token: 0x040038F4 RID: 14580
		public static readonly ExPerformanceCounter CreateResponseFromModernGroupAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "CreateResponseFromModernGroup Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038F5 RID: 14581
		private static readonly ExPerformanceCounter UploadItemsRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "UploadItems Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038F6 RID: 14582
		public static readonly ExPerformanceCounter TotalUploadItemsRequests = new ExPerformanceCounter("MSExchangeWS", "UploadItems Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.UploadItemsRequestsPerSecond
		});

		// Token: 0x040038F7 RID: 14583
		public static readonly ExPerformanceCounter UploadItemsAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "UploadItems Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038F8 RID: 14584
		private static readonly ExPerformanceCounter UploadLargeItemRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "UploadLargeItem Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038F9 RID: 14585
		public static readonly ExPerformanceCounter TotalUploadLargeItemRequests = new ExPerformanceCounter("MSExchangeWS", "UploadLargeItem Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.UploadLargeItemRequestsPerSecond
		});

		// Token: 0x040038FA RID: 14586
		public static readonly ExPerformanceCounter UploadLargeItemAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "UploadLargeItem Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038FB RID: 14587
		private static readonly ExPerformanceCounter ChunkUploadRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "ChunkUpload Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038FC RID: 14588
		public static readonly ExPerformanceCounter TotalChunkUploadRequests = new ExPerformanceCounter("MSExchangeWS", "ChunkUpload Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.ChunkUploadRequestsPerSecond
		});

		// Token: 0x040038FD RID: 14589
		public static readonly ExPerformanceCounter ChunkUploadAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "ChunkUpload Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038FE RID: 14590
		private static readonly ExPerformanceCounter CompleteLargeItemUploadRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "CompleteLargeItemUpload Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040038FF RID: 14591
		public static readonly ExPerformanceCounter TotalCompleteLargeItemUploadRequests = new ExPerformanceCounter("MSExchangeWS", "CompleteLargeItemUpload Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.CompleteLargeItemUploadRequestsPerSecond
		});

		// Token: 0x04003900 RID: 14592
		public static readonly ExPerformanceCounter CompleteLargeItemUploadAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "CompleteLargeItemUpload Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003901 RID: 14593
		private static readonly ExPerformanceCounter ExportItemsRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "ExportItems Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003902 RID: 14594
		public static readonly ExPerformanceCounter TotalExportItemsRequests = new ExPerformanceCounter("MSExchangeWS", "ExportItems Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.ExportItemsRequestsPerSecond
		});

		// Token: 0x04003903 RID: 14595
		public static readonly ExPerformanceCounter ExportItemsAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "ExportItems Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003904 RID: 14596
		private static readonly ExPerformanceCounter DeleteItemRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "DeleteItem Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003905 RID: 14597
		public static readonly ExPerformanceCounter TotalDeleteItemRequests = new ExPerformanceCounter("MSExchangeWS", "DeleteItem Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.DeleteItemRequestsPerSecond
		});

		// Token: 0x04003906 RID: 14598
		public static readonly ExPerformanceCounter DeleteItemAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "DeleteItem Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003907 RID: 14599
		private static readonly ExPerformanceCounter UpdateItemRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "UpdateItem Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003908 RID: 14600
		public static readonly ExPerformanceCounter TotalUpdateItemRequests = new ExPerformanceCounter("MSExchangeWS", "UpdateItem Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.UpdateItemRequestsPerSecond
		});

		// Token: 0x04003909 RID: 14601
		public static readonly ExPerformanceCounter UpdateItemAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "UpdateItem Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400390A RID: 14602
		private static readonly ExPerformanceCounter UpdateItemInRecoverableItemsRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "UpdateItemInRecoverableItems Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400390B RID: 14603
		public static readonly ExPerformanceCounter TotalUpdateItemInRecoverableItemsRequests = new ExPerformanceCounter("MSExchangeWS", "UpdateItemInRecoverableItems Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.UpdateItemInRecoverableItemsRequestsPerSecond
		});

		// Token: 0x0400390C RID: 14604
		public static readonly ExPerformanceCounter UpdateItemInRecoverableItemsAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "UpdateItemInRecoverableItems Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400390D RID: 14605
		private static readonly ExPerformanceCounter MarkAllItemsAsReadRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "MarkAllItemsAsRead Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400390E RID: 14606
		public static readonly ExPerformanceCounter TotalMarkAllItemsAsReadRequests = new ExPerformanceCounter("MSExchangeWS", "MarkAllItemsAsRead Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.MarkAllItemsAsReadRequestsPerSecond
		});

		// Token: 0x0400390F RID: 14607
		public static readonly ExPerformanceCounter MarkAllItemsAsReadAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "MarkAllItemsAsRead Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003910 RID: 14608
		private static readonly ExPerformanceCounter MarkAsJunkRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "MarkAsJunk Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003911 RID: 14609
		public static readonly ExPerformanceCounter TotalMarkAsJunkRequests = new ExPerformanceCounter("MSExchangeWS", "MarkAsJunk Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.MarkAsJunkRequestsPerSecond
		});

		// Token: 0x04003912 RID: 14610
		public static readonly ExPerformanceCounter MarkAsJunkAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "MarkAsJunk Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003913 RID: 14611
		public static readonly ExPerformanceCounter TotalMarkAsJunkSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "MarkAsJunk Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003914 RID: 14612
		private static readonly ExPerformanceCounter GetClientExtensionRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetClientExtension Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003915 RID: 14613
		public static readonly ExPerformanceCounter TotalGetClientExtensionRequests = new ExPerformanceCounter("MSExchangeWS", "GetClientExtension Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetClientExtensionRequestsPerSecond
		});

		// Token: 0x04003916 RID: 14614
		public static readonly ExPerformanceCounter GetClientExtensionAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetClientExtension Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003917 RID: 14615
		private static readonly ExPerformanceCounter GetEncryptionConfigurationRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetEncryptionConfiguration Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003918 RID: 14616
		public static readonly ExPerformanceCounter TotalGetEncryptionConfigurationRequests = new ExPerformanceCounter("MSExchangeWS", "GetEncryptionConfiguration Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetEncryptionConfigurationRequestsPerSecond
		});

		// Token: 0x04003919 RID: 14617
		public static readonly ExPerformanceCounter GetEncryptionConfigurationAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetEncryptionConfiguration Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400391A RID: 14618
		private static readonly ExPerformanceCounter SetClientExtensionRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "SetClientExtension Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400391B RID: 14619
		public static readonly ExPerformanceCounter TotalSetClientExtensionRequests = new ExPerformanceCounter("MSExchangeWS", "SetClientExtension Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.SetClientExtensionRequestsPerSecond
		});

		// Token: 0x0400391C RID: 14620
		public static readonly ExPerformanceCounter SetClientExtensionAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "SetClientExtension Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400391D RID: 14621
		private static readonly ExPerformanceCounter SetEncryptionConfigurationRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "SetEncryptionConfiguration Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400391E RID: 14622
		public static readonly ExPerformanceCounter TotalSetEncryptionConfigurationRequests = new ExPerformanceCounter("MSExchangeWS", "SetEncryptionConfiguration Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.SetEncryptionConfigurationRequestsPerSecond
		});

		// Token: 0x0400391F RID: 14623
		public static readonly ExPerformanceCounter SetEncryptionConfigurationAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "SetEncryptionConfiguration Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003920 RID: 14624
		private static readonly ExPerformanceCounter CreateUnifiedMailboxRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "CreateUnifiedMailbox Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003921 RID: 14625
		public static readonly ExPerformanceCounter TotalCreateUnifiedMailboxRequests = new ExPerformanceCounter("MSExchangeWS", "CreateUnifiedMailbox Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.CreateUnifiedMailboxRequestsPerSecond
		});

		// Token: 0x04003922 RID: 14626
		public static readonly ExPerformanceCounter CreateUnifiedMailboxAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "CreateUnifiedMailbox Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003923 RID: 14627
		private static readonly ExPerformanceCounter GetAppManifestsRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetAppManifests Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003924 RID: 14628
		public static readonly ExPerformanceCounter TotalGetAppManifestsRequests = new ExPerformanceCounter("MSExchangeWS", "GetAppManifests Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetAppManifestsRequestsPerSecond
		});

		// Token: 0x04003925 RID: 14629
		public static readonly ExPerformanceCounter GetAppManifestsAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetAppManifests Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003926 RID: 14630
		private static readonly ExPerformanceCounter GetClientExtensionTokenRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetClientExtensionToken Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003927 RID: 14631
		public static readonly ExPerformanceCounter TotalGetClientExtensionTokenRequests = new ExPerformanceCounter("MSExchangeWS", "GetClientExtensionToken Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetClientExtensionTokenRequestsPerSecond
		});

		// Token: 0x04003928 RID: 14632
		public static readonly ExPerformanceCounter GetClientExtensionTokenAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetClientExtensionToken Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003929 RID: 14633
		private static readonly ExPerformanceCounter GetEncryptionConfigurationTokenRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetEncryptionConfigurationToken Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400392A RID: 14634
		public static readonly ExPerformanceCounter TotalGetEncryptionConfigurationTokenRequests = new ExPerformanceCounter("MSExchangeWS", "GetEncryptionConfigurationToken Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetEncryptionConfigurationTokenRequestsPerSecond
		});

		// Token: 0x0400392B RID: 14635
		public static readonly ExPerformanceCounter GetEncryptionConfigurationTokenAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetEncryptionConfigurationToken Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400392C RID: 14636
		private static readonly ExPerformanceCounter InstallAppRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "InstallApp Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400392D RID: 14637
		public static readonly ExPerformanceCounter TotalInstallAppRequests = new ExPerformanceCounter("MSExchangeWS", "InstallApp Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.InstallAppRequestsPerSecond
		});

		// Token: 0x0400392E RID: 14638
		public static readonly ExPerformanceCounter InstallAppAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "InstallApp Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400392F RID: 14639
		private static readonly ExPerformanceCounter UninstallAppRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "UninstallApp Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003930 RID: 14640
		public static readonly ExPerformanceCounter TotalUninstallAppRequests = new ExPerformanceCounter("MSExchangeWS", "UninstallApp Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.UninstallAppRequestsPerSecond
		});

		// Token: 0x04003931 RID: 14641
		public static readonly ExPerformanceCounter UninstallAppAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "UninstallApp Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003932 RID: 14642
		private static readonly ExPerformanceCounter DisableAppRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "DisableApp Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003933 RID: 14643
		public static readonly ExPerformanceCounter TotalDisableAppRequests = new ExPerformanceCounter("MSExchangeWS", "DisableApp Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.DisableAppRequestsPerSecond
		});

		// Token: 0x04003934 RID: 14644
		public static readonly ExPerformanceCounter DisableAppAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "DisableApp Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003935 RID: 14645
		private static readonly ExPerformanceCounter GetAppMarketplaceUrlRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetAppMarketplaceUrl Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003936 RID: 14646
		public static readonly ExPerformanceCounter TotalGetAppMarketplaceUrlRequests = new ExPerformanceCounter("MSExchangeWS", "GetAppMarketplaceUrl Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetAppMarketplaceUrlRequestsPerSecond
		});

		// Token: 0x04003937 RID: 14647
		public static readonly ExPerformanceCounter GetAppMarketplaceUrlAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetAppMarketplaceUrl Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003938 RID: 14648
		private static readonly ExPerformanceCounter AddImContactToGroupRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "AddImContactToGroup Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003939 RID: 14649
		public static readonly ExPerformanceCounter TotalAddImContactToGroupRequests = new ExPerformanceCounter("MSExchangeWS", "AddImContactToGroup Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.AddImContactToGroupRequestsPerSecond
		});

		// Token: 0x0400393A RID: 14650
		public static readonly ExPerformanceCounter AddImContactToGroupAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "AddImContactToGroup Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400393B RID: 14651
		public static readonly ExPerformanceCounter TotalAddImContactToGroupSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "AddImContactToGroup Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400393C RID: 14652
		private static readonly ExPerformanceCounter RemoveImContactFromGroupRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "RemoveImContactFromGroup Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400393D RID: 14653
		public static readonly ExPerformanceCounter TotalRemoveImContactFromGroupRequests = new ExPerformanceCounter("MSExchangeWS", "RemoveImContactFromGroup Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.RemoveImContactFromGroupRequestsPerSecond
		});

		// Token: 0x0400393E RID: 14654
		public static readonly ExPerformanceCounter RemoveImContactFromGroupAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "RemoveImContactFromGroup Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400393F RID: 14655
		public static readonly ExPerformanceCounter TotalRemoveImContactFromGroupSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "RemoveImContactFromGroup Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003940 RID: 14656
		private static readonly ExPerformanceCounter AddNewImContactToGroupRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "AddNewImContactToGroup Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003941 RID: 14657
		public static readonly ExPerformanceCounter TotalAddNewImContactToGroupRequests = new ExPerformanceCounter("MSExchangeWS", "AddNewImContactToGroup Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.AddNewImContactToGroupRequestsPerSecond
		});

		// Token: 0x04003942 RID: 14658
		public static readonly ExPerformanceCounter AddNewImContactToGroupAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "AddNewImContactToGroup Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003943 RID: 14659
		public static readonly ExPerformanceCounter TotalAddNewImContactToGroupSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "AddNewImContactToGroup Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003944 RID: 14660
		private static readonly ExPerformanceCounter AddNewTelUriContactToGroupRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "AddNewTelUriContactToGroup Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003945 RID: 14661
		public static readonly ExPerformanceCounter TotalAddNewTelUriContactToGroupRequests = new ExPerformanceCounter("MSExchangeWS", "AddNewTelUriContactToGroup Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.AddNewTelUriContactToGroupRequestsPerSecond
		});

		// Token: 0x04003946 RID: 14662
		public static readonly ExPerformanceCounter AddNewTelUriContactToGroupAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "AddNewTelUriContactToGroup Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003947 RID: 14663
		public static readonly ExPerformanceCounter TotalAddNewTelUriContactToGroupSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "AddNewTelUriContactToGroup Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003948 RID: 14664
		private static readonly ExPerformanceCounter AddDistributionGroupToImListRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "AddDistributionGroupToImList Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003949 RID: 14665
		public static readonly ExPerformanceCounter TotalAddDistributionGroupToImListRequests = new ExPerformanceCounter("MSExchangeWS", "AddDistributionGroupToImList Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.AddDistributionGroupToImListRequestsPerSecond
		});

		// Token: 0x0400394A RID: 14666
		public static readonly ExPerformanceCounter AddDistributionGroupToImListAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "AddDistributionGroupToImList Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400394B RID: 14667
		public static readonly ExPerformanceCounter TotalAddDistributionGroupToImListSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "AddDistributionGroupToImList Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400394C RID: 14668
		private static readonly ExPerformanceCounter AddImGroupRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "AddImGroup Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400394D RID: 14669
		public static readonly ExPerformanceCounter TotalAddImGroupRequests = new ExPerformanceCounter("MSExchangeWS", "AddImGroup Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.AddImGroupRequestsPerSecond
		});

		// Token: 0x0400394E RID: 14670
		public static readonly ExPerformanceCounter AddImGroupAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "AddImGroup Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400394F RID: 14671
		public static readonly ExPerformanceCounter TotalAddImGroupSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "AddImGroup Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003950 RID: 14672
		private static readonly ExPerformanceCounter GetImItemListRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetImItemList Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003951 RID: 14673
		public static readonly ExPerformanceCounter TotalGetImItemListRequests = new ExPerformanceCounter("MSExchangeWS", "GetImItemList Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetImItemListRequestsPerSecond
		});

		// Token: 0x04003952 RID: 14674
		public static readonly ExPerformanceCounter GetImItemListAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetImItemList Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003953 RID: 14675
		public static readonly ExPerformanceCounter TotalGetImItemListSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetImItemList Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003954 RID: 14676
		private static readonly ExPerformanceCounter GetImItemsRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetImItems Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003955 RID: 14677
		public static readonly ExPerformanceCounter TotalGetImItemsRequests = new ExPerformanceCounter("MSExchangeWS", "GetImItems Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetImItemsRequestsPerSecond
		});

		// Token: 0x04003956 RID: 14678
		public static readonly ExPerformanceCounter GetImItemsAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetImItems Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003957 RID: 14679
		public static readonly ExPerformanceCounter TotalGetImItemsSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetImItems Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003958 RID: 14680
		private static readonly ExPerformanceCounter RemoveContactFromImListRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "RemoveContactFromImList Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003959 RID: 14681
		public static readonly ExPerformanceCounter TotalRemoveContactFromImListRequests = new ExPerformanceCounter("MSExchangeWS", "RemoveContactFromImList Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.RemoveContactFromImListRequestsPerSecond
		});

		// Token: 0x0400395A RID: 14682
		public static readonly ExPerformanceCounter RemoveContactFromImListAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "RemoveContactFromImList Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400395B RID: 14683
		public static readonly ExPerformanceCounter TotalRemoveContactFromImListSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "RemoveContactFromImList Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400395C RID: 14684
		private static readonly ExPerformanceCounter RemoveDistributionGroupFromImListRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "RemoveDistributionGroupFromImList Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400395D RID: 14685
		public static readonly ExPerformanceCounter TotalRemoveDistributionGroupFromImListRequests = new ExPerformanceCounter("MSExchangeWS", "RemoveDistributionGroupFromImList Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.RemoveDistributionGroupFromImListRequestsPerSecond
		});

		// Token: 0x0400395E RID: 14686
		public static readonly ExPerformanceCounter RemoveDistributionGroupFromImListAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "RemoveDistributionGroupFromImList Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400395F RID: 14687
		public static readonly ExPerformanceCounter TotalRemoveDistributionGroupFromImListSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "RemoveDistributionGroupFromImList Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003960 RID: 14688
		private static readonly ExPerformanceCounter RemoveImGroupRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "RemoveImGroup Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003961 RID: 14689
		public static readonly ExPerformanceCounter TotalRemoveImGroupRequests = new ExPerformanceCounter("MSExchangeWS", "RemoveImGroup Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.RemoveImGroupRequestsPerSecond
		});

		// Token: 0x04003962 RID: 14690
		public static readonly ExPerformanceCounter RemoveImGroupAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "RemoveImGroup Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003963 RID: 14691
		public static readonly ExPerformanceCounter TotalRemoveImGroupSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "RemoveImGroup Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003964 RID: 14692
		private static readonly ExPerformanceCounter SetImGroupRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "SetImGroup Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003965 RID: 14693
		public static readonly ExPerformanceCounter TotalSetImGroupRequests = new ExPerformanceCounter("MSExchangeWS", "SetImGroup Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.SetImGroupRequestsPerSecond
		});

		// Token: 0x04003966 RID: 14694
		public static readonly ExPerformanceCounter SetImGroupAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "SetImGroup Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003967 RID: 14695
		public static readonly ExPerformanceCounter TotalSetImGroupSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "SetImGroup Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003968 RID: 14696
		private static readonly ExPerformanceCounter SetImListMigrationCompletedRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "SetImListMigrationCompleted Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003969 RID: 14697
		public static readonly ExPerformanceCounter TotalSetImListMigrationCompletedRequests = new ExPerformanceCounter("MSExchangeWS", "SetImListMigrationCompleted Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.SetImListMigrationCompletedRequestsPerSecond
		});

		// Token: 0x0400396A RID: 14698
		public static readonly ExPerformanceCounter SetImListMigrationCompletedAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "SetImListMigrationCompleted Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400396B RID: 14699
		public static readonly ExPerformanceCounter TotalSetImListMigrationCompletedSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "SetImListMigrationCompleted Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400396C RID: 14700
		private static readonly ExPerformanceCounter GetConversationItemsRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetConversationItems Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400396D RID: 14701
		public static readonly ExPerformanceCounter TotalGetConversationItemsRequests = new ExPerformanceCounter("MSExchangeWS", "GetConversationItems Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetConversationItemsRequestsPerSecond
		});

		// Token: 0x0400396E RID: 14702
		public static readonly ExPerformanceCounter GetConversationItemsAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetConversationItems Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400396F RID: 14703
		private static readonly ExPerformanceCounter GetModernConversationItemsRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetModernConversationItems Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003970 RID: 14704
		public static readonly ExPerformanceCounter TotalGetModernConversationItemsRequests = new ExPerformanceCounter("MSExchangeWS", "GetModernConversationItems Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetModernConversationItemsRequestsPerSecond
		});

		// Token: 0x04003971 RID: 14705
		public static readonly ExPerformanceCounter GetModernConversationItemsAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetModernConversationItems Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003972 RID: 14706
		private static readonly ExPerformanceCounter GetThreadedConversationItemsRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetThreadedConversationItems Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003973 RID: 14707
		public static readonly ExPerformanceCounter TotalGetThreadedConversationItemsRequests = new ExPerformanceCounter("MSExchangeWS", "GetThreadedConversationItems Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetThreadedConversationItemsRequestsPerSecond
		});

		// Token: 0x04003974 RID: 14708
		public static readonly ExPerformanceCounter GetThreadedConversationItemsAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetThreadedConversationItems Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003975 RID: 14709
		private static readonly ExPerformanceCounter GetModernConversationAttachmentsRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetModernConversationAttachments Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003976 RID: 14710
		public static readonly ExPerformanceCounter TotalGetModernConversationAttachmentsRequests = new ExPerformanceCounter("MSExchangeWS", "GetModernConversationAttachments Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetModernConversationAttachmentsRequestsPerSecond
		});

		// Token: 0x04003977 RID: 14711
		public static readonly ExPerformanceCounter GetModernConversationAttachmentsAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetModernConversationAttachments Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003978 RID: 14712
		private static readonly ExPerformanceCounter SetModernGroupMembershipRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "SetModernGroupMembership Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003979 RID: 14713
		public static readonly ExPerformanceCounter TotalSetModernGroupMembershipRequests = new ExPerformanceCounter("MSExchangeWS", "SetModernGroupMembership Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.SetModernGroupMembershipRequestsPerSecond
		});

		// Token: 0x0400397A RID: 14714
		public static readonly ExPerformanceCounter SetModernGroupMembershipAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "SetModernGroupMembership Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400397B RID: 14715
		private static readonly ExPerformanceCounter SendItemRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "SendItem Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400397C RID: 14716
		public static readonly ExPerformanceCounter TotalSendItemRequests = new ExPerformanceCounter("MSExchangeWS", "SendItem Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.SendItemRequestsPerSecond
		});

		// Token: 0x0400397D RID: 14717
		public static readonly ExPerformanceCounter SendItemAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "SendItem Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400397E RID: 14718
		private static readonly ExPerformanceCounter ArchiveItemRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "ArchiveItem Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400397F RID: 14719
		public static readonly ExPerformanceCounter TotalArchiveItemRequests = new ExPerformanceCounter("MSExchangeWS", "ArchiveItem Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.ArchiveItemRequestsPerSecond
		});

		// Token: 0x04003980 RID: 14720
		public static readonly ExPerformanceCounter ArchiveItemAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "ArchiveItem Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003981 RID: 14721
		private static readonly ExPerformanceCounter MoveItemRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "MoveItem Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003982 RID: 14722
		public static readonly ExPerformanceCounter TotalMoveItemRequests = new ExPerformanceCounter("MSExchangeWS", "MoveItem Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.MoveItemRequestsPerSecond
		});

		// Token: 0x04003983 RID: 14723
		public static readonly ExPerformanceCounter MoveItemAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "MoveItem Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003984 RID: 14724
		private static readonly ExPerformanceCounter CopyItemRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "CopyItem Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003985 RID: 14725
		public static readonly ExPerformanceCounter TotalCopyItemRequests = new ExPerformanceCounter("MSExchangeWS", "CopyItem Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.CopyItemRequestsPerSecond
		});

		// Token: 0x04003986 RID: 14726
		public static readonly ExPerformanceCounter CopyItemAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "CopyItem Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003987 RID: 14727
		private static readonly ExPerformanceCounter GetFolderRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetFolder Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003988 RID: 14728
		public static readonly ExPerformanceCounter TotalGetFolderRequests = new ExPerformanceCounter("MSExchangeWS", "GetFolder Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetFolderRequestsPerSecond
		});

		// Token: 0x04003989 RID: 14729
		public static readonly ExPerformanceCounter GetFolderAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetFolder Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400398A RID: 14730
		private static readonly ExPerformanceCounter CreateFolderRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "CreateFolder Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400398B RID: 14731
		public static readonly ExPerformanceCounter TotalCreateFolderRequests = new ExPerformanceCounter("MSExchangeWS", "CreateFolder Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.CreateFolderRequestsPerSecond
		});

		// Token: 0x0400398C RID: 14732
		public static readonly ExPerformanceCounter CreateFolderAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "CreateFolder Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400398D RID: 14733
		private static readonly ExPerformanceCounter CreateFolderPathRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "CreateFolderPath Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400398E RID: 14734
		public static readonly ExPerformanceCounter TotalCreateFolderPathRequests = new ExPerformanceCounter("MSExchangeWS", "CreateFolderPath Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.CreateFolderPathRequestsPerSecond
		});

		// Token: 0x0400398F RID: 14735
		public static readonly ExPerformanceCounter CreateFolderPathAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "CreateFolderPath Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003990 RID: 14736
		private static readonly ExPerformanceCounter CreateManagedFolderRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "CreateManagedFolder Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003991 RID: 14737
		public static readonly ExPerformanceCounter TotalCreateManagedFolderRequests = new ExPerformanceCounter("MSExchangeWS", "CreateManagedFolder Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.CreateManagedFolderRequestsPerSecond
		});

		// Token: 0x04003992 RID: 14738
		public static readonly ExPerformanceCounter CreateManagedFolderAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "CreateManagedFolder Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003993 RID: 14739
		private static readonly ExPerformanceCounter DeleteFolderRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "DeleteFolder Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003994 RID: 14740
		public static readonly ExPerformanceCounter TotalDeleteFolderRequests = new ExPerformanceCounter("MSExchangeWS", "DeleteFolder Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.DeleteFolderRequestsPerSecond
		});

		// Token: 0x04003995 RID: 14741
		public static readonly ExPerformanceCounter DeleteFolderAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "DeleteFolder Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003996 RID: 14742
		private static readonly ExPerformanceCounter EmptyFolderRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "EmptyFolder Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003997 RID: 14743
		public static readonly ExPerformanceCounter TotalEmptyFolderRequests = new ExPerformanceCounter("MSExchangeWS", "EmptyFolder Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.EmptyFolderRequestsPerSecond
		});

		// Token: 0x04003998 RID: 14744
		public static readonly ExPerformanceCounter EmptyFolderAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "EmptyFolder Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003999 RID: 14745
		private static readonly ExPerformanceCounter UpdateFolderRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "UpdateFolder Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400399A RID: 14746
		public static readonly ExPerformanceCounter TotalUpdateFolderRequests = new ExPerformanceCounter("MSExchangeWS", "UpdateFolder Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.UpdateFolderRequestsPerSecond
		});

		// Token: 0x0400399B RID: 14747
		public static readonly ExPerformanceCounter UpdateFolderAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "UpdateFolder Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400399C RID: 14748
		private static readonly ExPerformanceCounter MoveFolderRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "MoveFolder Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400399D RID: 14749
		public static readonly ExPerformanceCounter TotalMoveFolderRequests = new ExPerformanceCounter("MSExchangeWS", "MoveFolder Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.MoveFolderRequestsPerSecond
		});

		// Token: 0x0400399E RID: 14750
		public static readonly ExPerformanceCounter MoveFolderAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "MoveFolder Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400399F RID: 14751
		private static readonly ExPerformanceCounter CopyFolderRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "CopyFolder Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039A0 RID: 14752
		public static readonly ExPerformanceCounter TotalCopyFolderRequests = new ExPerformanceCounter("MSExchangeWS", "CopyFolder Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.CopyFolderRequestsPerSecond
		});

		// Token: 0x040039A1 RID: 14753
		public static readonly ExPerformanceCounter CopyFolderAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "CopyFolder Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039A2 RID: 14754
		private static readonly ExPerformanceCounter FindItemRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "FindItem Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039A3 RID: 14755
		public static readonly ExPerformanceCounter TotalFindItemRequests = new ExPerformanceCounter("MSExchangeWS", "FindItem Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.FindItemRequestsPerSecond
		});

		// Token: 0x040039A4 RID: 14756
		public static readonly ExPerformanceCounter FindItemAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "FindItem Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039A5 RID: 14757
		private static readonly ExPerformanceCounter FindFolderRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "FindFolder Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039A6 RID: 14758
		public static readonly ExPerformanceCounter TotalFindFolderRequests = new ExPerformanceCounter("MSExchangeWS", "FindFolder Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.FindFolderRequestsPerSecond
		});

		// Token: 0x040039A7 RID: 14759
		public static readonly ExPerformanceCounter FindFolderAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "FindFolder Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039A8 RID: 14760
		private static readonly ExPerformanceCounter ResolveNamesRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "ResolveNames Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039A9 RID: 14761
		public static readonly ExPerformanceCounter TotalResolveNamesRequests = new ExPerformanceCounter("MSExchangeWS", "ResolveNames Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.ResolveNamesRequestsPerSecond
		});

		// Token: 0x040039AA RID: 14762
		public static readonly ExPerformanceCounter ResolveNamesAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "ResolveNames Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039AB RID: 14763
		private static readonly ExPerformanceCounter ExpandDLRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "ExpandDL Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039AC RID: 14764
		public static readonly ExPerformanceCounter TotalExpandDLRequests = new ExPerformanceCounter("MSExchangeWS", "ExpandDL Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.ExpandDLRequestsPerSecond
		});

		// Token: 0x040039AD RID: 14765
		public static readonly ExPerformanceCounter ExpandDLAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "ExpandDL Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039AE RID: 14766
		private static readonly ExPerformanceCounter GetPasswordExpirationDateRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetPasswordExpirationDate Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039AF RID: 14767
		public static readonly ExPerformanceCounter TotalGetPasswordExpirationDateRequests = new ExPerformanceCounter("MSExchangeWS", "GetPasswordExpirationDate Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetPasswordExpirationDateRequestsPerSecond
		});

		// Token: 0x040039B0 RID: 14768
		public static readonly ExPerformanceCounter GetPasswordExpirationDateAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetPasswordExpirationDate Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039B1 RID: 14769
		public static readonly ExPerformanceCounter TotalGetPasswordExpirationDateSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetPasswordExpirationDate Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039B2 RID: 14770
		private static readonly ExPerformanceCounter CreateAttachmentRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "CreateAttachment Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039B3 RID: 14771
		public static readonly ExPerformanceCounter TotalCreateAttachmentRequests = new ExPerformanceCounter("MSExchangeWS", "CreateAttachment Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.CreateAttachmentRequestsPerSecond
		});

		// Token: 0x040039B4 RID: 14772
		public static readonly ExPerformanceCounter CreateAttachmentAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "CreateAttachment Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039B5 RID: 14773
		private static readonly ExPerformanceCounter DeleteAttachmentRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "DeleteAttachment Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039B6 RID: 14774
		public static readonly ExPerformanceCounter TotalDeleteAttachmentRequests = new ExPerformanceCounter("MSExchangeWS", "DeleteAttachment Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.DeleteAttachmentRequestsPerSecond
		});

		// Token: 0x040039B7 RID: 14775
		public static readonly ExPerformanceCounter DeleteAttachmentAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "DeleteAttachment Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039B8 RID: 14776
		private static readonly ExPerformanceCounter GetAttachmentRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetAttachment Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039B9 RID: 14777
		public static readonly ExPerformanceCounter TotalGetAttachmentRequests = new ExPerformanceCounter("MSExchangeWS", "GetAttachment Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetAttachmentRequestsPerSecond
		});

		// Token: 0x040039BA RID: 14778
		public static readonly ExPerformanceCounter GetAttachmentAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetAttachment Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039BB RID: 14779
		private static readonly ExPerformanceCounter GetClientAccessTokenRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetClientAccessToken Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039BC RID: 14780
		public static readonly ExPerformanceCounter TotalGetClientAccessTokenRequests = new ExPerformanceCounter("MSExchangeWS", "GetClientAccessToken Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetClientAccessTokenRequestsPerSecond
		});

		// Token: 0x040039BD RID: 14781
		public static readonly ExPerformanceCounter GetClientAccessTokenAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetClientAccessToken Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039BE RID: 14782
		private static readonly ExPerformanceCounter SubscribeRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "Subscribe Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039BF RID: 14783
		public static readonly ExPerformanceCounter TotalSubscribeRequests = new ExPerformanceCounter("MSExchangeWS", "Subscribe Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.SubscribeRequestsPerSecond
		});

		// Token: 0x040039C0 RID: 14784
		public static readonly ExPerformanceCounter SubscribeAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "Subscribe Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039C1 RID: 14785
		private static readonly ExPerformanceCounter UnsubscribeRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "Unsubscribe Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039C2 RID: 14786
		public static readonly ExPerformanceCounter TotalUnsubscribeRequests = new ExPerformanceCounter("MSExchangeWS", "Unsubscribe Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.UnsubscribeRequestsPerSecond
		});

		// Token: 0x040039C3 RID: 14787
		public static readonly ExPerformanceCounter UnsubscribeAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "Unsubscribe Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039C4 RID: 14788
		public static readonly ExPerformanceCounter TotalGetStreamingEventsRequests = new ExPerformanceCounter("MSExchangeWS", "GetStreamingEvents Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039C5 RID: 14789
		private static readonly ExPerformanceCounter GetStreamingEventsRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetStreamingEvents Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039C6 RID: 14790
		public static readonly ExPerformanceCounter GetStreamingEventsAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetStreamingEvents Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039C7 RID: 14791
		private static readonly ExPerformanceCounter GetEventsRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetEvents Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039C8 RID: 14792
		public static readonly ExPerformanceCounter TotalGetEventsRequests = new ExPerformanceCounter("MSExchangeWS", "GetEvents Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetStreamingEventsRequestsPerSecond,
			WsPerformanceCounters.GetEventsRequestsPerSecond
		});

		// Token: 0x040039C9 RID: 14793
		public static readonly ExPerformanceCounter GetEventsAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetEvents Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039CA RID: 14794
		private static readonly ExPerformanceCounter GetServiceConfigurationRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetServiceConfiguration Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039CB RID: 14795
		public static readonly ExPerformanceCounter TotalGetServiceConfigurationRequests = new ExPerformanceCounter("MSExchangeWS", "GetServiceConfiguration Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetServiceConfigurationRequestsPerSecond
		});

		// Token: 0x040039CC RID: 14796
		public static readonly ExPerformanceCounter GetServiceConfigurationAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetServiceConfiguration Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039CD RID: 14797
		private static readonly ExPerformanceCounter GetMailTipsRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetMailTips Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039CE RID: 14798
		public static readonly ExPerformanceCounter TotalGetMailTipsRequests = new ExPerformanceCounter("MSExchangeWS", "GetMailTips Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetMailTipsRequestsPerSecond
		});

		// Token: 0x040039CF RID: 14799
		public static readonly ExPerformanceCounter GetMailTipsAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetMailTips Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039D0 RID: 14800
		private static readonly ExPerformanceCounter SyncFolderHierarchyRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "SyncFolderHierarchy Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039D1 RID: 14801
		public static readonly ExPerformanceCounter TotalSyncFolderHierarchyRequests = new ExPerformanceCounter("MSExchangeWS", "SyncFolderHierarchy Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.SyncFolderHierarchyRequestsPerSecond
		});

		// Token: 0x040039D2 RID: 14802
		public static readonly ExPerformanceCounter SyncFolderHierarchyAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "SyncFolderHierarchy Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039D3 RID: 14803
		private static readonly ExPerformanceCounter SyncFolderItemsRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "SyncFolderItems Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039D4 RID: 14804
		public static readonly ExPerformanceCounter TotalSyncFolderItemsRequests = new ExPerformanceCounter("MSExchangeWS", "SyncFolderItems Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.SyncFolderItemsRequestsPerSecond
		});

		// Token: 0x040039D5 RID: 14805
		public static readonly ExPerformanceCounter SyncFolderItemsAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "SyncFolderItems Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039D6 RID: 14806
		private static readonly ExPerformanceCounter GetDelegateRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetDelegate Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039D7 RID: 14807
		public static readonly ExPerformanceCounter TotalGetDelegateRequests = new ExPerformanceCounter("MSExchangeWS", "GetDelegate Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetDelegateRequestsPerSecond
		});

		// Token: 0x040039D8 RID: 14808
		public static readonly ExPerformanceCounter GetDelegateAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetDelegate Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039D9 RID: 14809
		private static readonly ExPerformanceCounter AddDelegateRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "AddDelegate Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039DA RID: 14810
		public static readonly ExPerformanceCounter TotalAddDelegateRequests = new ExPerformanceCounter("MSExchangeWS", "AddDelegate Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.AddDelegateRequestsPerSecond
		});

		// Token: 0x040039DB RID: 14811
		public static readonly ExPerformanceCounter AddDelegateAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "AddDelegate Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039DC RID: 14812
		private static readonly ExPerformanceCounter RemoveDelegateRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "RemoveDelegate Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039DD RID: 14813
		public static readonly ExPerformanceCounter TotalRemoveDelegateRequests = new ExPerformanceCounter("MSExchangeWS", "RemoveDelegate Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.RemoveDelegateRequestsPerSecond
		});

		// Token: 0x040039DE RID: 14814
		public static readonly ExPerformanceCounter RemoveDelegateAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "RemoveDelegate Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039DF RID: 14815
		private static readonly ExPerformanceCounter UpdateDelegateRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "UpdateDelegate Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039E0 RID: 14816
		public static readonly ExPerformanceCounter TotalUpdateDelegateRequests = new ExPerformanceCounter("MSExchangeWS", "UpdateDelegate Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.UpdateDelegateRequestsPerSecond
		});

		// Token: 0x040039E1 RID: 14817
		public static readonly ExPerformanceCounter UpdateDelegateAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "UpdateDelegate Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039E2 RID: 14818
		private static readonly ExPerformanceCounter CreateUserConfigurationRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "CreateUserConfiguration Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039E3 RID: 14819
		public static readonly ExPerformanceCounter TotalCreateUserConfigurationRequests = new ExPerformanceCounter("MSExchangeWS", "CreateUserConfiguration Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.CreateUserConfigurationRequestsPerSecond
		});

		// Token: 0x040039E4 RID: 14820
		public static readonly ExPerformanceCounter CreateUserConfigurationAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "CreateUserConfiguration Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039E5 RID: 14821
		private static readonly ExPerformanceCounter GetUserConfigurationRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetUserConfiguration Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039E6 RID: 14822
		public static readonly ExPerformanceCounter TotalGetUserConfigurationRequests = new ExPerformanceCounter("MSExchangeWS", "GetUserConfiguration Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetUserConfigurationRequestsPerSecond
		});

		// Token: 0x040039E7 RID: 14823
		public static readonly ExPerformanceCounter GetUserConfigurationAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetUserConfiguration Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039E8 RID: 14824
		private static readonly ExPerformanceCounter UpdateUserConfigurationRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "UpdateUserConfiguration Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039E9 RID: 14825
		public static readonly ExPerformanceCounter TotalUpdateUserConfigurationRequests = new ExPerformanceCounter("MSExchangeWS", "UpdateUserConfiguration Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.UpdateUserConfigurationRequestsPerSecond
		});

		// Token: 0x040039EA RID: 14826
		public static readonly ExPerformanceCounter UpdateUserConfigurationAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "UpdateUserConfiguration Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039EB RID: 14827
		private static readonly ExPerformanceCounter DeleteUserConfigurationRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "DeleteUserConfiguration Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039EC RID: 14828
		public static readonly ExPerformanceCounter TotalDeleteUserConfigurationRequests = new ExPerformanceCounter("MSExchangeWS", "DeleteUserConfiguration Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.DeleteUserConfigurationRequestsPerSecond
		});

		// Token: 0x040039ED RID: 14829
		public static readonly ExPerformanceCounter DeleteUserConfigurationAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "DeleteUserConfiguration Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039EE RID: 14830
		private static readonly ExPerformanceCounter GetUserAvailabilityRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetUserAvailability Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039EF RID: 14831
		public static readonly ExPerformanceCounter TotalGetUserAvailabilityRequests = new ExPerformanceCounter("MSExchangeWS", "GetUserAvailability Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetUserAvailabilityRequestsPerSecond
		});

		// Token: 0x040039F0 RID: 14832
		public static readonly ExPerformanceCounter GetUserAvailabilityAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetUserAvailability Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039F1 RID: 14833
		private static readonly ExPerformanceCounter GetUserOofSettingsRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetUserOofSettings Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039F2 RID: 14834
		public static readonly ExPerformanceCounter TotalGetUserOofSettingsRequests = new ExPerformanceCounter("MSExchangeWS", "GetUserOofSettings Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetUserOofSettingsRequestsPerSecond
		});

		// Token: 0x040039F3 RID: 14835
		public static readonly ExPerformanceCounter GetUserOofSettingsAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetUserOofSettings Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039F4 RID: 14836
		private static readonly ExPerformanceCounter SetUserOofSettingsRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "SetUserOofSettings Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039F5 RID: 14837
		public static readonly ExPerformanceCounter TotalSetUserOofSettingsRequests = new ExPerformanceCounter("MSExchangeWS", "SetUserOofSettings Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.SetUserOofSettingsRequestsPerSecond
		});

		// Token: 0x040039F6 RID: 14838
		public static readonly ExPerformanceCounter SetUserOofSettingsAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "SetUserOofSettings Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039F7 RID: 14839
		private static readonly ExPerformanceCounter GetSharingMetadataRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetSharingMetadata Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039F8 RID: 14840
		public static readonly ExPerformanceCounter TotalGetSharingMetadataRequests = new ExPerformanceCounter("MSExchangeWS", "GetSharingMetadata Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetSharingMetadataRequestsPerSecond
		});

		// Token: 0x040039F9 RID: 14841
		public static readonly ExPerformanceCounter GetSharingMetadataAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetSharingMetadata Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039FA RID: 14842
		private static readonly ExPerformanceCounter RefreshSharingFolderRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "RefreshSharingFolder Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039FB RID: 14843
		public static readonly ExPerformanceCounter TotalRefreshSharingFolderRequests = new ExPerformanceCounter("MSExchangeWS", "RefreshSharingFolder Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.RefreshSharingFolderRequestsPerSecond
		});

		// Token: 0x040039FC RID: 14844
		public static readonly ExPerformanceCounter RefreshSharingFolderAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "RefreshSharingFolder Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039FD RID: 14845
		private static readonly ExPerformanceCounter GetSharingFolderRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetSharingFolder Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040039FE RID: 14846
		public static readonly ExPerformanceCounter TotalGetSharingFolderRequests = new ExPerformanceCounter("MSExchangeWS", "GetSharingFolder Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetSharingFolderRequestsPerSecond
		});

		// Token: 0x040039FF RID: 14847
		public static readonly ExPerformanceCounter GetSharingFolderAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetSharingFolder Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A00 RID: 14848
		private static readonly ExPerformanceCounter SetTeamMailboxRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "SetTeamMailbox Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A01 RID: 14849
		public static readonly ExPerformanceCounter TotalSetTeamMailboxRequests = new ExPerformanceCounter("MSExchangeWS", "SetTeamMailbox Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.SetTeamMailboxRequestsPerSecond
		});

		// Token: 0x04003A02 RID: 14850
		public static readonly ExPerformanceCounter SetTeamMailboxAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "SetTeamMailbox Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A03 RID: 14851
		public static readonly ExPerformanceCounter TotalSetTeamMailboxSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "SetTeamMailbox Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A04 RID: 14852
		private static readonly ExPerformanceCounter UnpinTeamMailboxRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "UnpinTeamMailbox Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A05 RID: 14853
		public static readonly ExPerformanceCounter TotalUnpinTeamMailboxRequests = new ExPerformanceCounter("MSExchangeWS", "UnpinTeamMailbox Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.UnpinTeamMailboxRequestsPerSecond
		});

		// Token: 0x04003A06 RID: 14854
		public static readonly ExPerformanceCounter UnpinTeamMailboxAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "UnpinTeamMailbox Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A07 RID: 14855
		public static readonly ExPerformanceCounter TotalUnpinTeamMailboxSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "UnpinTeamMailbox Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A08 RID: 14856
		private static readonly ExPerformanceCounter GetRoomListsRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetRoomLists Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A09 RID: 14857
		public static readonly ExPerformanceCounter TotalGetRoomListsRequests = new ExPerformanceCounter("MSExchangeWS", "GetRoomLists Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetRoomListsRequestsPerSecond
		});

		// Token: 0x04003A0A RID: 14858
		public static readonly ExPerformanceCounter GetRoomListsAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetRoomLists Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A0B RID: 14859
		private static readonly ExPerformanceCounter SubscribeToPushNotificationPerSecond = new ExPerformanceCounter("MSExchangeWS", "SubscribeToPushNotification Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A0C RID: 14860
		public static readonly ExPerformanceCounter SubscribeToPushNotificationRequests = new ExPerformanceCounter("MSExchangeWS", "SubscribeToPushNotification Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.SubscribeToPushNotificationPerSecond
		});

		// Token: 0x04003A0D RID: 14861
		public static readonly ExPerformanceCounter SubscribeToPushNotificationAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "SubscribeToPushNotification Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A0E RID: 14862
		private static readonly ExPerformanceCounter RequestDeviceRegistrationChallengePerSecond = new ExPerformanceCounter("MSExchangeWS", "Push Notification Registration Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A0F RID: 14863
		public static readonly ExPerformanceCounter RequestDeviceRegistrationChallengeRequest = new ExPerformanceCounter("MSExchangeWS", "RequestDeviceRegistrationChallenge Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.RequestDeviceRegistrationChallengePerSecond
		});

		// Token: 0x04003A10 RID: 14864
		public static readonly ExPerformanceCounter RequestDeviceRegistrationChallengeAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "RequestDeviceRegistrationChallenge Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A11 RID: 14865
		public static readonly ExPerformanceCounter DeviceRegistrationChallengeRequestSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "RequestDeviceRegistrationChallenge Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A12 RID: 14866
		private static readonly ExPerformanceCounter UnsubscribeToPushNotificationPerSecond = new ExPerformanceCounter("MSExchangeWS", "UnsubscribeToPushNotification Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A13 RID: 14867
		public static readonly ExPerformanceCounter UnsubscribeToPushNotificationRequests = new ExPerformanceCounter("MSExchangeWS", "UnsubscribeToPushNotification Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.UnsubscribeToPushNotificationPerSecond
		});

		// Token: 0x04003A14 RID: 14868
		public static readonly ExPerformanceCounter UnsubscribeToPushNotificationAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "UnsubscribeToPushNotification Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A15 RID: 14869
		private static readonly ExPerformanceCounter GetRoomsRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetRooms Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A16 RID: 14870
		public static readonly ExPerformanceCounter TotalGetRoomsRequests = new ExPerformanceCounter("MSExchangeWS", "GetRooms Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetRoomsRequestsPerSecond
		});

		// Token: 0x04003A17 RID: 14871
		public static readonly ExPerformanceCounter GetRoomsAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetRooms Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A18 RID: 14872
		private static readonly ExPerformanceCounter PerformReminderActionRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "PerformReminderAction Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A19 RID: 14873
		public static readonly ExPerformanceCounter TotalPerformReminderActionRequests = new ExPerformanceCounter("MSExchangeWS", "PerformReminderAction Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.PerformReminderActionRequestsPerSecond
		});

		// Token: 0x04003A1A RID: 14874
		public static readonly ExPerformanceCounter PerformReminderActionAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "PerformReminderAction Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A1B RID: 14875
		private static readonly ExPerformanceCounter GetRemindersRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetReminders Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A1C RID: 14876
		public static readonly ExPerformanceCounter TotalGetRemindersRequests = new ExPerformanceCounter("MSExchangeWS", "GetReminders Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetRemindersRequestsPerSecond
		});

		// Token: 0x04003A1D RID: 14877
		public static readonly ExPerformanceCounter GetRemindersAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetReminders Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A1E RID: 14878
		private static readonly ExPerformanceCounter ProvisionRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "Provision Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A1F RID: 14879
		public static readonly ExPerformanceCounter TotalProvisionRequests = new ExPerformanceCounter("MSExchangeWS", "Provision Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.ProvisionRequestsPerSecond
		});

		// Token: 0x04003A20 RID: 14880
		public static readonly ExPerformanceCounter ProvisionAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "Provision Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A21 RID: 14881
		private static readonly ExPerformanceCounter LogPushNotificationDataRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "LogPushNotificationData Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A22 RID: 14882
		public static readonly ExPerformanceCounter TotalLogPushNotificationDataRequests = new ExPerformanceCounter("MSExchangeWS", "LogPushNotificationData Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.LogPushNotificationDataRequestsPerSecond
		});

		// Token: 0x04003A23 RID: 14883
		public static readonly ExPerformanceCounter LogPushNotificationDataAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "LogPushNotificationData Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A24 RID: 14884
		private static readonly ExPerformanceCounter DeprovisionRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "Deprovision Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A25 RID: 14885
		public static readonly ExPerformanceCounter TotalDeprovisionRequests = new ExPerformanceCounter("MSExchangeWS", "Deprovision Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.DeprovisionRequestsPerSecond
		});

		// Token: 0x04003A26 RID: 14886
		public static readonly ExPerformanceCounter DeprovisionAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "Deprovision Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A27 RID: 14887
		private static readonly ExPerformanceCounter FindConversationRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "FindConversation Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A28 RID: 14888
		public static readonly ExPerformanceCounter TotalFindConversationRequests = new ExPerformanceCounter("MSExchangeWS", "FindConversation Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.FindConversationRequestsPerSecond
		});

		// Token: 0x04003A29 RID: 14889
		public static readonly ExPerformanceCounter FindConversationAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "FindConversation Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A2A RID: 14890
		private static readonly ExPerformanceCounter FindPeopleRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "FindPeople Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A2B RID: 14891
		public static readonly ExPerformanceCounter TotalFindPeopleRequests = new ExPerformanceCounter("MSExchangeWS", "FindPeople Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.FindPeopleRequestsPerSecond
		});

		// Token: 0x04003A2C RID: 14892
		public static readonly ExPerformanceCounter FindPeopleAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "FindPeople Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A2D RID: 14893
		private static readonly ExPerformanceCounter SyncPeopleRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "SyncPeople Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A2E RID: 14894
		public static readonly ExPerformanceCounter TotalSyncPeopleRequests = new ExPerformanceCounter("MSExchangeWS", "SyncPeople Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.SyncPeopleRequestsPerSecond
		});

		// Token: 0x04003A2F RID: 14895
		public static readonly ExPerformanceCounter SyncPeopleAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "SyncPeople Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A30 RID: 14896
		private static readonly ExPerformanceCounter SyncAutoCompleteRecipientsRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "SyncAutoCompleteRecipients Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A31 RID: 14897
		public static readonly ExPerformanceCounter TotalSyncAutoCompleteRecipientsRequests = new ExPerformanceCounter("MSExchangeWS", "SyncAutoCompleteRecipients Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.SyncAutoCompleteRecipientsRequestsPerSecond
		});

		// Token: 0x04003A32 RID: 14898
		public static readonly ExPerformanceCounter SyncAutoCompleteRecipientsAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "SyncAutoCompleteRecipients Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A33 RID: 14899
		private static readonly ExPerformanceCounter GetPersonaRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetPersona Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A34 RID: 14900
		public static readonly ExPerformanceCounter TotalGetPersonaRequests = new ExPerformanceCounter("MSExchangeWS", "GetPersona Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetPersonaRequestsPerSecond
		});

		// Token: 0x04003A35 RID: 14901
		public static readonly ExPerformanceCounter GetPersonaAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetPersona Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A36 RID: 14902
		private static readonly ExPerformanceCounter SyncConversationRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "SyncConversation Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A37 RID: 14903
		public static readonly ExPerformanceCounter TotalSyncConversationRequests = new ExPerformanceCounter("MSExchangeWS", "SyncConversation Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.SyncConversationRequestsPerSecond
		});

		// Token: 0x04003A38 RID: 14904
		public static readonly ExPerformanceCounter SyncConversationAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "SyncConversation Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A39 RID: 14905
		private static readonly ExPerformanceCounter GetTimeZoneOffsetsRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetTimeZoneOffsets Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A3A RID: 14906
		public static readonly ExPerformanceCounter TotalGetTimeZoneOffsetsRequests = new ExPerformanceCounter("MSExchangeWS", "GetTimeZoneOffsets Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetTimeZoneOffsetsRequestsPerSecond
		});

		// Token: 0x04003A3B RID: 14907
		public static readonly ExPerformanceCounter GetTimeZoneOffsetsAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetTimeZoneOffsets Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A3C RID: 14908
		public static readonly ExPerformanceCounter TotalTimeZoneOffsetsTablesBuilt = new ExPerformanceCounter("MSExchangeWS", "TimeZoneOffsets Tables Built", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A3D RID: 14909
		private static readonly ExPerformanceCounter FindMailboxStatisticsByKeywordsRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "FindMailboxStatisticsByKeywords Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A3E RID: 14910
		public static readonly ExPerformanceCounter TotalFindMailboxStatisticsByKeywordsRequests = new ExPerformanceCounter("MSExchangeWS", "FindMailboxStatisticsByKeywords Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.FindMailboxStatisticsByKeywordsRequestsPerSecond
		});

		// Token: 0x04003A3F RID: 14911
		public static readonly ExPerformanceCounter FindMailboxStatisticsByKeywordsAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "FindMailboxStatisticsByKeywords Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A40 RID: 14912
		private static readonly ExPerformanceCounter GetSearchableMailboxesRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetSearchableMailboxes Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A41 RID: 14913
		public static readonly ExPerformanceCounter TotalGetSearchableMailboxesRequests = new ExPerformanceCounter("MSExchangeWS", "GetSearchableMailboxes Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetSearchableMailboxesRequestsPerSecond
		});

		// Token: 0x04003A42 RID: 14914
		public static readonly ExPerformanceCounter GetSearchableMailboxesAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetSearchableMailboxes Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A43 RID: 14915
		private static readonly ExPerformanceCounter SearchMailboxesRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "SearchMailboxes Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A44 RID: 14916
		public static readonly ExPerformanceCounter TotalSearchMailboxesRequests = new ExPerformanceCounter("MSExchangeWS", "SearchMailboxes Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.SearchMailboxesRequestsPerSecond
		});

		// Token: 0x04003A45 RID: 14917
		public static readonly ExPerformanceCounter SearchMailboxesAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "SearchMailboxes Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A46 RID: 14918
		private static readonly ExPerformanceCounter GetDiscoverySearchConfigurationRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetDiscoverySearchConfiguration Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A47 RID: 14919
		public static readonly ExPerformanceCounter TotalGetDiscoverySearchConfigurationRequests = new ExPerformanceCounter("MSExchangeWS", "GetDiscoverySearchConfiguration Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetDiscoverySearchConfigurationRequestsPerSecond
		});

		// Token: 0x04003A48 RID: 14920
		public static readonly ExPerformanceCounter GetDiscoverySearchConfigurationAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetDiscoverySearchConfiguration Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A49 RID: 14921
		private static readonly ExPerformanceCounter GetHoldOnMailboxesRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetHoldOnMailboxes Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A4A RID: 14922
		public static readonly ExPerformanceCounter TotalGetHoldOnMailboxesRequests = new ExPerformanceCounter("MSExchangeWS", "GetHoldOnMailboxes Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetHoldOnMailboxesRequestsPerSecond
		});

		// Token: 0x04003A4B RID: 14923
		public static readonly ExPerformanceCounter GetHoldOnMailboxesAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetHoldOnMailboxes Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A4C RID: 14924
		private static readonly ExPerformanceCounter SetHoldOnMailboxesRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "SetHoldOnMailboxes Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A4D RID: 14925
		public static readonly ExPerformanceCounter TotalSetHoldOnMailboxesRequests = new ExPerformanceCounter("MSExchangeWS", "SetHoldOnMailboxes Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.SetHoldOnMailboxesRequestsPerSecond
		});

		// Token: 0x04003A4E RID: 14926
		public static readonly ExPerformanceCounter SetHoldOnMailboxesAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "SetHoldOnMailboxes Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A4F RID: 14927
		private static readonly ExPerformanceCounter GetNonIndexableItemStatisticsRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetNonIndexableItemStatistics Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A50 RID: 14928
		public static readonly ExPerformanceCounter TotalGetNonIndexableItemStatisticsRequests = new ExPerformanceCounter("MSExchangeWS", "GetNonIndexableItemStatistics Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetNonIndexableItemStatisticsRequestsPerSecond
		});

		// Token: 0x04003A51 RID: 14929
		public static readonly ExPerformanceCounter GetNonIndexableItemStatisticsAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetNonIndexableItemStatistics Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A52 RID: 14930
		private static readonly ExPerformanceCounter GetNonIndexableItemDetailsRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetNonIndexableItemDetails Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A53 RID: 14931
		public static readonly ExPerformanceCounter TotalGetNonIndexableItemDetailsRequests = new ExPerformanceCounter("MSExchangeWS", "GetNonIndexableItemDetails Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetNonIndexableItemDetailsRequestsPerSecond
		});

		// Token: 0x04003A54 RID: 14932
		public static readonly ExPerformanceCounter GetNonIndexableItemDetailsAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetNonIndexableItemDetails Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A55 RID: 14933
		private static readonly ExPerformanceCounter GetUserRetentionPolicyTagsRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetUserRetentionPolicyTags Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A56 RID: 14934
		public static readonly ExPerformanceCounter TotalGetUserRetentionPolicyTagsRequests = new ExPerformanceCounter("MSExchangeWS", "GetUserRetentionPolicyTags Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetUserRetentionPolicyTagsRequestsPerSecond
		});

		// Token: 0x04003A57 RID: 14935
		public static readonly ExPerformanceCounter GetUserRetentionPolicyTagsAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetUserRetentionPolicyTags Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A58 RID: 14936
		private static readonly ExPerformanceCounter PlayOnPhoneRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "PlayOnPhone Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A59 RID: 14937
		public static readonly ExPerformanceCounter TotalPlayOnPhoneRequests = new ExPerformanceCounter("MSExchangeWS", "PlayOnPhone Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.PlayOnPhoneRequestsPerSecond
		});

		// Token: 0x04003A5A RID: 14938
		public static readonly ExPerformanceCounter PlayOnPhoneAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "PlayOnPhone Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A5B RID: 14939
		private static readonly ExPerformanceCounter GetPhoneCallInformationRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetPhoneCallInformation Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A5C RID: 14940
		public static readonly ExPerformanceCounter TotalGetPhoneCallInformationRequests = new ExPerformanceCounter("MSExchangeWS", "GetPhoneCallInformation Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetPhoneCallInformationRequestsPerSecond
		});

		// Token: 0x04003A5D RID: 14941
		public static readonly ExPerformanceCounter GetPhoneCallInformationAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetPhoneCallInformation Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A5E RID: 14942
		private static readonly ExPerformanceCounter DisconnectPhoneCallRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "DisconnectPhoneCall Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A5F RID: 14943
		public static readonly ExPerformanceCounter TotalDisconnectPhoneCallRequests = new ExPerformanceCounter("MSExchangeWS", "DisconnectPhoneCall Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.DisconnectPhoneCallRequestsPerSecond
		});

		// Token: 0x04003A60 RID: 14944
		public static readonly ExPerformanceCounter DisconnectPhoneCallAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "DisconnectPhoneCall Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A61 RID: 14945
		private static readonly ExPerformanceCounter CreateUMPromptRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "CreateUMPrompt Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A62 RID: 14946
		public static readonly ExPerformanceCounter TotalCreateUMPromptRequests = new ExPerformanceCounter("MSExchangeWS", "CreateUMPrompt Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.CreateUMPromptRequestsPerSecond
		});

		// Token: 0x04003A63 RID: 14947
		public static readonly ExPerformanceCounter CreateUMPromptAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "CreateUMPrompt Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A64 RID: 14948
		private static readonly ExPerformanceCounter GetUMPromptRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetUMPrompt Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A65 RID: 14949
		public static readonly ExPerformanceCounter TotalGetUMPromptRequests = new ExPerformanceCounter("MSExchangeWS", "GetUMPrompt Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetUMPromptRequestsPerSecond
		});

		// Token: 0x04003A66 RID: 14950
		public static readonly ExPerformanceCounter GetUMPromptAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetUMPrompt Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A67 RID: 14951
		private static readonly ExPerformanceCounter GetUMPromptNamesRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetUMPromptNames Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A68 RID: 14952
		public static readonly ExPerformanceCounter TotalGetUMPromptNamesRequests = new ExPerformanceCounter("MSExchangeWS", "GetUMPromptNames Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetUMPromptNamesRequestsPerSecond
		});

		// Token: 0x04003A69 RID: 14953
		public static readonly ExPerformanceCounter GetUMPromptNamesAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetUMPromptNames Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A6A RID: 14954
		private static readonly ExPerformanceCounter DeleteUMPromptsRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "DeleteUMPrompts Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A6B RID: 14955
		public static readonly ExPerformanceCounter TotalDeleteUMPromptsRequests = new ExPerformanceCounter("MSExchangeWS", "DeleteUMPrompts Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.DeleteUMPromptsRequestsPerSecond
		});

		// Token: 0x04003A6C RID: 14956
		public static readonly ExPerformanceCounter DeleteUMPromptsAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "DeleteUMPrompts Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A6D RID: 14957
		private static readonly ExPerformanceCounter GetServerTimeZonesRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetServerTimeZones Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A6E RID: 14958
		public static readonly ExPerformanceCounter TotalGetServerTimeZonesRequests = new ExPerformanceCounter("MSExchangeWS", "GetServerTimeZones Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetServerTimeZonesRequestsPerSecond
		});

		// Token: 0x04003A6F RID: 14959
		public static readonly ExPerformanceCounter GetServerTimeZonesAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetServerTimeZones Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A70 RID: 14960
		private static readonly ExPerformanceCounter SendNotificationRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "SendNotification Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A71 RID: 14961
		public static readonly ExPerformanceCounter TotalSendNotificationRequests = new ExPerformanceCounter("MSExchangeWS", "SendNotification Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.SendNotificationRequestsPerSecond
		});

		// Token: 0x04003A72 RID: 14962
		public static readonly ExPerformanceCounter SendNotificationAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "SendNotification Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A73 RID: 14963
		private static readonly ExPerformanceCounter FindMessageTrackingReportRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "FindMessageTrackingReport Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A74 RID: 14964
		public static readonly ExPerformanceCounter TotalFindMessageTrackingReportRequests = new ExPerformanceCounter("MSExchangeWS", "FindMessageTrackingReport Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.FindMessageTrackingReportRequestsPerSecond
		});

		// Token: 0x04003A75 RID: 14965
		public static readonly ExPerformanceCounter FindMessageTrackingReportAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "FindMessageTrackingReport Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A76 RID: 14966
		private static readonly ExPerformanceCounter GetMessageTrackingReportRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetMessageTrackingReport Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A77 RID: 14967
		public static readonly ExPerformanceCounter TotalGetMessageTrackingReportRequests = new ExPerformanceCounter("MSExchangeWS", "GetMessageTrackingReport Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetMessageTrackingReportRequestsPerSecond
		});

		// Token: 0x04003A78 RID: 14968
		public static readonly ExPerformanceCounter GetMessageTrackingReportAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetMessageTrackingReport Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A79 RID: 14969
		private static readonly ExPerformanceCounter ApplyConversationActionRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "ApplyConversationAction Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A7A RID: 14970
		public static readonly ExPerformanceCounter TotalApplyConversationActionRequests = new ExPerformanceCounter("MSExchangeWS", "ApplyConversationAction Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.ApplyConversationActionRequestsPerSecond
		});

		// Token: 0x04003A7B RID: 14971
		public static readonly ExPerformanceCounter ApplyConversationActionAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "ApplyConversationAction Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A7C RID: 14972
		private static readonly ExPerformanceCounter ExecuteDiagnosticMethodRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "ExecuteDiagnosticMethod Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A7D RID: 14973
		public static readonly ExPerformanceCounter TotalExecuteDiagnosticMethodRequests = new ExPerformanceCounter("MSExchangeWS", "ExecuteDiagnosticMethod Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.ExecuteDiagnosticMethodRequestsPerSecond
		});

		// Token: 0x04003A7E RID: 14974
		public static readonly ExPerformanceCounter ExecuteDiagnosticMethodAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "ExecuteDiagnosticMethod Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A7F RID: 14975
		private static readonly ExPerformanceCounter GetInboxRulesRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetInboxRules Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A80 RID: 14976
		public static readonly ExPerformanceCounter TotalGetInboxRulesRequests = new ExPerformanceCounter("MSExchangeWS", "GetInboxRules Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetInboxRulesRequestsPerSecond
		});

		// Token: 0x04003A81 RID: 14977
		public static readonly ExPerformanceCounter GetInboxRulesAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetInboxRules Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A82 RID: 14978
		public static readonly ExPerformanceCounter TotalGetInboxRulesSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetInboxRules Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A83 RID: 14979
		private static readonly ExPerformanceCounter UpdateInboxRulesRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "UpdateInboxRules Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A84 RID: 14980
		public static readonly ExPerformanceCounter TotalUpdateInboxRulesRequests = new ExPerformanceCounter("MSExchangeWS", "UpdateInboxRules Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.UpdateInboxRulesRequestsPerSecond
		});

		// Token: 0x04003A85 RID: 14981
		public static readonly ExPerformanceCounter UpdateInboxRulesAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "UpdateInboxRules Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A86 RID: 14982
		public static readonly ExPerformanceCounter TotalUpdateInboxRulesSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "UpdateInboxRules Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A87 RID: 14983
		private static readonly ExPerformanceCounter IsUMEnabledRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "IsUMEnabled Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A88 RID: 14984
		public static readonly ExPerformanceCounter TotalIsUMEnabledRequests = new ExPerformanceCounter("MSExchangeWS", "IsUMEnabled Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.IsUMEnabledRequestsPerSecond
		});

		// Token: 0x04003A89 RID: 14985
		public static readonly ExPerformanceCounter IsUMEnabledAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "IsUMEnabled Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A8A RID: 14986
		public static readonly ExPerformanceCounter TotalIsUMEnabledSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "IsUMEnabled Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A8B RID: 14987
		private static readonly ExPerformanceCounter GetUMPropertiesRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetUMProperties Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A8C RID: 14988
		public static readonly ExPerformanceCounter TotalGetUMPropertiesRequests = new ExPerformanceCounter("MSExchangeWS", "GetUMProperties Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetUMPropertiesRequestsPerSecond
		});

		// Token: 0x04003A8D RID: 14989
		public static readonly ExPerformanceCounter GetUMPropertiesAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetUMProperties Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A8E RID: 14990
		public static readonly ExPerformanceCounter TotalGetUMPropertiesSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetUMProperties Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A8F RID: 14991
		private static readonly ExPerformanceCounter SetOofStatusRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "SetOofStatus Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A90 RID: 14992
		public static readonly ExPerformanceCounter TotalSetOofStatusRequests = new ExPerformanceCounter("MSExchangeWS", "SetOofStatus Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.SetOofStatusRequestsPerSecond
		});

		// Token: 0x04003A91 RID: 14993
		public static readonly ExPerformanceCounter SetOofStatusAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "SetOofStatus Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A92 RID: 14994
		public static readonly ExPerformanceCounter TotalSetOofStatusSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "SetOofStatus Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A93 RID: 14995
		private static readonly ExPerformanceCounter SetPlayOnPhoneDialStringRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "SetPlayOnPhoneDialString Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A94 RID: 14996
		public static readonly ExPerformanceCounter TotalSetPlayOnPhoneDialStringRequests = new ExPerformanceCounter("MSExchangeWS", "SetPlayOnPhoneDialString Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.SetPlayOnPhoneDialStringRequestsPerSecond
		});

		// Token: 0x04003A95 RID: 14997
		public static readonly ExPerformanceCounter SetPlayOnPhoneDialStringAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "SetPlayOnPhoneDialString Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A96 RID: 14998
		public static readonly ExPerformanceCounter TotalSetPlayOnPhoneDialStringSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "SetPlayOnPhoneDialString Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A97 RID: 14999
		private static readonly ExPerformanceCounter SetTelephoneAccessFolderEmailRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "SetTelephoneAccessFolderEmail Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A98 RID: 15000
		public static readonly ExPerformanceCounter TotalSetTelephoneAccessFolderEmailRequests = new ExPerformanceCounter("MSExchangeWS", "SetTelephoneAccessFolderEmail Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.SetTelephoneAccessFolderEmailRequestsPerSecond
		});

		// Token: 0x04003A99 RID: 15001
		public static readonly ExPerformanceCounter SetTelephoneAccessFolderEmailAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "SetTelephoneAccessFolderEmail Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A9A RID: 15002
		public static readonly ExPerformanceCounter TotalSetTelephoneAccessFolderEmailSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "SetTelephoneAccessFolderEmail Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A9B RID: 15003
		private static readonly ExPerformanceCounter SetMissedCallNotificationEnabledRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "SetMissedCallNotificationEnabled Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A9C RID: 15004
		public static readonly ExPerformanceCounter TotalSetMissedCallNotificationEnabledRequests = new ExPerformanceCounter("MSExchangeWS", "SetMissedCallNotificationEnabled Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.SetMissedCallNotificationEnabledRequestsPerSecond
		});

		// Token: 0x04003A9D RID: 15005
		public static readonly ExPerformanceCounter SetMissedCallNotificationEnabledAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "SetMissedCallNotificationEnabled Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A9E RID: 15006
		public static readonly ExPerformanceCounter TotalSetMissedCallNotificationEnabledSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "SetMissedCallNotificationEnabled Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003A9F RID: 15007
		private static readonly ExPerformanceCounter ResetPINRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "ResetPIN Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AA0 RID: 15008
		public static readonly ExPerformanceCounter TotalResetPINRequests = new ExPerformanceCounter("MSExchangeWS", "ResetPIN Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.ResetPINRequestsPerSecond
		});

		// Token: 0x04003AA1 RID: 15009
		public static readonly ExPerformanceCounter ResetPINAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "ResetPIN Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AA2 RID: 15010
		public static readonly ExPerformanceCounter TotalResetPINSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "ResetPIN Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AA3 RID: 15011
		private static readonly ExPerformanceCounter GetCallInfoRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetCallInfo Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AA4 RID: 15012
		public static readonly ExPerformanceCounter TotalGetCallInfoRequests = new ExPerformanceCounter("MSExchangeWS", "GetCallInfo Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetCallInfoRequestsPerSecond
		});

		// Token: 0x04003AA5 RID: 15013
		public static readonly ExPerformanceCounter GetCallInfoAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetCallInfo Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AA6 RID: 15014
		public static readonly ExPerformanceCounter TotalGetCallInfoSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetCallInfo Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AA7 RID: 15015
		private static readonly ExPerformanceCounter DisconnectRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "Disconnect Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AA8 RID: 15016
		public static readonly ExPerformanceCounter TotalDisconnectRequests = new ExPerformanceCounter("MSExchangeWS", "Disconnect Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.DisconnectRequestsPerSecond
		});

		// Token: 0x04003AA9 RID: 15017
		public static readonly ExPerformanceCounter DisconnectAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "Disconnect Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AAA RID: 15018
		public static readonly ExPerformanceCounter TotalDisconnectSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "Disconnect Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AAB RID: 15019
		private static readonly ExPerformanceCounter PlayOnPhoneGreetingRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "PlayOnPhoneGreeting Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AAC RID: 15020
		public static readonly ExPerformanceCounter TotalPlayOnPhoneGreetingRequests = new ExPerformanceCounter("MSExchangeWS", "PlayOnPhoneGreeting Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.PlayOnPhoneGreetingRequestsPerSecond
		});

		// Token: 0x04003AAD RID: 15021
		public static readonly ExPerformanceCounter PlayOnPhoneGreetingAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "PlayOnPhoneGreeting Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AAE RID: 15022
		public static readonly ExPerformanceCounter TotalPlayOnPhoneGreetingSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "PlayOnPhoneGreeting Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AAF RID: 15023
		private static readonly ExPerformanceCounter StreamedEventsPerSecond = new ExPerformanceCounter("MSExchangeWS", "StreamedEvents/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AB0 RID: 15024
		public static readonly ExPerformanceCounter TotalStreamedEvents = new ExPerformanceCounter("MSExchangeWS", "Total StreamedEvents", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.StreamedEventsPerSecond
		});

		// Token: 0x04003AB1 RID: 15025
		private static readonly ExPerformanceCounter TotalRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AB2 RID: 15026
		public static readonly ExPerformanceCounter TotalRequests = new ExPerformanceCounter("MSExchangeWS", "Total Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.TotalRequestsPerSecond
		});

		// Token: 0x04003AB3 RID: 15027
		public static readonly ExPerformanceCounter AverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AB4 RID: 15028
		private static readonly ExPerformanceCounter TotalItemsCreatedPerSecond = new ExPerformanceCounter("MSExchangeWS", "Items Created/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AB5 RID: 15029
		public static readonly ExPerformanceCounter TotalItemsCreated = new ExPerformanceCounter("MSExchangeWS", "Items Created", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.TotalItemsCreatedPerSecond
		});

		// Token: 0x04003AB6 RID: 15030
		private static readonly ExPerformanceCounter TotalItemsDeletedPerSecond = new ExPerformanceCounter("MSExchangeWS", "Items Deleted/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AB7 RID: 15031
		public static readonly ExPerformanceCounter TotalItemsDeleted = new ExPerformanceCounter("MSExchangeWS", "Items Deleted", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.TotalItemsDeletedPerSecond
		});

		// Token: 0x04003AB8 RID: 15032
		private static readonly ExPerformanceCounter TotalItemsSentPerSecond = new ExPerformanceCounter("MSExchangeWS", "Items Sent/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AB9 RID: 15033
		public static readonly ExPerformanceCounter TotalItemsSent = new ExPerformanceCounter("MSExchangeWS", "Items Sent", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.TotalItemsSentPerSecond
		});

		// Token: 0x04003ABA RID: 15034
		private static readonly ExPerformanceCounter TotalItemsReadPerSecond = new ExPerformanceCounter("MSExchangeWS", "Items Read/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003ABB RID: 15035
		public static readonly ExPerformanceCounter TotalItemsRead = new ExPerformanceCounter("MSExchangeWS", "Items Read", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.TotalItemsReadPerSecond
		});

		// Token: 0x04003ABC RID: 15036
		private static readonly ExPerformanceCounter TotalItemsUpdatedPerSecond = new ExPerformanceCounter("MSExchangeWS", "Items Updated/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003ABD RID: 15037
		public static readonly ExPerformanceCounter TotalItemsUpdated = new ExPerformanceCounter("MSExchangeWS", "Items Updated", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.TotalItemsUpdatedPerSecond
		});

		// Token: 0x04003ABE RID: 15038
		private static readonly ExPerformanceCounter TotalItemsCopiedPerSecond = new ExPerformanceCounter("MSExchangeWS", "Items Copied/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003ABF RID: 15039
		public static readonly ExPerformanceCounter TotalItemsCopied = new ExPerformanceCounter("MSExchangeWS", "Items Copied", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.TotalItemsCopiedPerSecond
		});

		// Token: 0x04003AC0 RID: 15040
		private static readonly ExPerformanceCounter TotalItemsMovedPerSecond = new ExPerformanceCounter("MSExchangeWS", "Items Moved/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AC1 RID: 15041
		public static readonly ExPerformanceCounter TotalItemsMoved = new ExPerformanceCounter("MSExchangeWS", "Items Moved", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.TotalItemsMovedPerSecond
		});

		// Token: 0x04003AC2 RID: 15042
		private static readonly ExPerformanceCounter TotalFoldersCreatedPerSecond = new ExPerformanceCounter("MSExchangeWS", "Folders Created/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AC3 RID: 15043
		public static readonly ExPerformanceCounter TotalFoldersCreated = new ExPerformanceCounter("MSExchangeWS", "Folders Created", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.TotalFoldersCreatedPerSecond
		});

		// Token: 0x04003AC4 RID: 15044
		private static readonly ExPerformanceCounter TotalFoldersDeletedPerSecond = new ExPerformanceCounter("MSExchangeWS", "Folders Deleted/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AC5 RID: 15045
		public static readonly ExPerformanceCounter TotalFoldersDeleted = new ExPerformanceCounter("MSExchangeWS", "Folders Deleted", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.TotalFoldersDeletedPerSecond
		});

		// Token: 0x04003AC6 RID: 15046
		private static readonly ExPerformanceCounter TotalFoldersReadPerSecond = new ExPerformanceCounter("MSExchangeWS", "Folders Sent/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AC7 RID: 15047
		public static readonly ExPerformanceCounter TotalFoldersRead = new ExPerformanceCounter("MSExchangeWS", "Folders Read", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.TotalFoldersReadPerSecond
		});

		// Token: 0x04003AC8 RID: 15048
		private static readonly ExPerformanceCounter TotalFoldersUpdatedPerSecond = new ExPerformanceCounter("MSExchangeWS", "Folders Updated/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AC9 RID: 15049
		public static readonly ExPerformanceCounter TotalFoldersUpdated = new ExPerformanceCounter("MSExchangeWS", "Folders Updated", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.TotalFoldersUpdatedPerSecond
		});

		// Token: 0x04003ACA RID: 15050
		private static readonly ExPerformanceCounter TotalFoldersCopiedPerSecond = new ExPerformanceCounter("MSExchangeWS", "Folders Copied/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003ACB RID: 15051
		public static readonly ExPerformanceCounter TotalFoldersCopied = new ExPerformanceCounter("MSExchangeWS", "Folders Copied", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.TotalFoldersCopiedPerSecond
		});

		// Token: 0x04003ACC RID: 15052
		private static readonly ExPerformanceCounter TotalFoldersMovedPerSecond = new ExPerformanceCounter("MSExchangeWS", "Folders Moved/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003ACD RID: 15053
		public static readonly ExPerformanceCounter TotalFoldersMoved = new ExPerformanceCounter("MSExchangeWS", "Folders Moved", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.TotalFoldersMovedPerSecond
		});

		// Token: 0x04003ACE RID: 15054
		private static readonly ExPerformanceCounter TotalFoldersSyncedPerSecond = new ExPerformanceCounter("MSExchangeWS", "Folders Synced/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003ACF RID: 15055
		public static readonly ExPerformanceCounter TotalFoldersSynced = new ExPerformanceCounter("MSExchangeWS", "Folders Synchronized", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.TotalFoldersSyncedPerSecond
		});

		// Token: 0x04003AD0 RID: 15056
		private static readonly ExPerformanceCounter TotalItemsSyncedPerSecond = new ExPerformanceCounter("MSExchangeWS", "Items Synced/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AD1 RID: 15057
		public static readonly ExPerformanceCounter TotalItemsSynced = new ExPerformanceCounter("MSExchangeWS", "Items Synchronized", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.TotalItemsSyncedPerSecond
		});

		// Token: 0x04003AD2 RID: 15058
		public static readonly ExPerformanceCounter TotalPushNotificationSuccesses = new ExPerformanceCounter("MSExchangeWS", "Push Notifications Succeeded", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AD3 RID: 15059
		public static readonly ExPerformanceCounter TotalPushNotificationFailures = new ExPerformanceCounter("MSExchangeWS", "Push Notifications Failed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AD4 RID: 15060
		public static readonly ExPerformanceCounter ActiveStreamingConnections = new ExPerformanceCounter("MSExchangeWS", "Active Streaming Connections", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AD5 RID: 15061
		public static readonly ExPerformanceCounter ActiveSubscriptions = new ExPerformanceCounter("MSExchangeWS", "Active Subscriptions", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AD6 RID: 15062
		private static readonly ExPerformanceCounter FailedSubscriptionsPerSecond = new ExPerformanceCounter("MSExchangeWS", "Failed Subscriptions/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AD7 RID: 15063
		public static readonly ExPerformanceCounter TotalFailedSubscriptions = new ExPerformanceCounter("MSExchangeWS", "Total Failed Subscriptions", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.FailedSubscriptionsPerSecond
		});

		// Token: 0x04003AD8 RID: 15064
		public static readonly ExPerformanceCounter TotalClientDisconnects = new ExPerformanceCounter("MSExchangeWS", "TotalClientDisconnects", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AD9 RID: 15065
		public static readonly ExPerformanceCounter PID = new ExPerformanceCounter("MSExchangeWS", "Process ID", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003ADA RID: 15066
		private static readonly ExPerformanceCounter CompletedRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "Completed requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003ADB RID: 15067
		public static readonly ExPerformanceCounter TotalCompletedRequests = new ExPerformanceCounter("MSExchangeWS", "Completed Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.CompletedRequestsPerSecond
		});

		// Token: 0x04003ADC RID: 15068
		private static readonly ExPerformanceCounter RequestRejectionsPerSecond = new ExPerformanceCounter("MSExchangeWS", "Request rejections/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003ADD RID: 15069
		public static readonly ExPerformanceCounter TotalRequestRejections = new ExPerformanceCounter("MSExchangeWS", "Request rejections", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.RequestRejectionsPerSecond
		});

		// Token: 0x04003ADE RID: 15070
		public static readonly ExPerformanceCounter CurrentProxyCalls = new ExPerformanceCounter("MSExchangeWS", "Number of current proxy calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003ADF RID: 15071
		private static readonly ExPerformanceCounter ProxyRequestsPerSeconds = new ExPerformanceCounter("MSExchangeWS", "Number of Proxied Requests per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AE0 RID: 15072
		public static readonly ExPerformanceCounter TotalProxyRequests = new ExPerformanceCounter("MSExchangeWS", "Total number of proxied requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.ProxyRequestsPerSeconds
		});

		// Token: 0x04003AE1 RID: 15073
		public static readonly ExPerformanceCounter TotalProxyRequestBytes = new ExPerformanceCounter("MSExchangeWS", "Total number of bytes proxied", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AE2 RID: 15074
		public static readonly ExPerformanceCounter TotalProxyResponseBytes = new ExPerformanceCounter("MSExchangeWS", "Total number of proxy response bytes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AE3 RID: 15075
		private static readonly ExPerformanceCounter ProxyFailoversPerSecond = new ExPerformanceCounter("MSExchangeWS", "Number of Proxy Failovers per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AE4 RID: 15076
		public static readonly ExPerformanceCounter TotalProxyFailovers = new ExPerformanceCounter("MSExchangeWS", "Total number of proxy failover", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.ProxyFailoversPerSecond
		});

		// Token: 0x04003AE5 RID: 15077
		public static readonly ExPerformanceCounter ProxyAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "Proxy Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AE6 RID: 15078
		private static readonly ExPerformanceCounter GetUserPhotoRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetUserPhoto Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AE7 RID: 15079
		public static readonly ExPerformanceCounter TotalGetUserPhotoRequests = new ExPerformanceCounter("MSExchangeWS", "GetUserPhoto Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetUserPhotoRequestsPerSecond
		});

		// Token: 0x04003AE8 RID: 15080
		public static readonly ExPerformanceCounter GetUserPhotoAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetUserPhoto Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AE9 RID: 15081
		public static readonly ExPerformanceCounter TotalAddAggregatedAccountSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "AddAggregatedAccount Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AEA RID: 15082
		public static readonly ExPerformanceCounter TotalIsOffice365DomainSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "IsOffice365Domain Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AEB RID: 15083
		public static readonly ExPerformanceCounter TotalGetAggregatedAccountSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetAggregatedAccount Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AEC RID: 15084
		public static readonly ExPerformanceCounter TotalRemoveAggregatedAccountSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "RemoveAggregatedAccount Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AED RID: 15085
		public static readonly ExPerformanceCounter TotalSetAggregatedAccountSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "SetAggregatedAccount Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AEE RID: 15086
		public static readonly ExPerformanceCounter TotalCopyFolderSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "CopyFolder Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AEF RID: 15087
		public static readonly ExPerformanceCounter TotalArchiveItemItemSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "ArchiveItem Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AF0 RID: 15088
		public static readonly ExPerformanceCounter TotalCopyItemSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "CopyItem Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AF1 RID: 15089
		public static readonly ExPerformanceCounter TotalCreateFolderSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "CreateFolder Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AF2 RID: 15090
		public static readonly ExPerformanceCounter TotalCreateFolderPathSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "CreateFolderPath Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AF3 RID: 15091
		public static readonly ExPerformanceCounter TotalCreateItemSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "CreateItem Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AF4 RID: 15092
		public static readonly ExPerformanceCounter TotalPostModernGroupItemSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "PostModernGroupItem Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AF5 RID: 15093
		public static readonly ExPerformanceCounter TotalUpdateAndPostModernGroupItemSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "UpdateAndPostModernGroupItem Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AF6 RID: 15094
		public static readonly ExPerformanceCounter TotalCreateResponseFromModernGroupSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "CreateResponseFromModernGroup Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AF7 RID: 15095
		public static readonly ExPerformanceCounter TotalCreateManagedFolderSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "CreateManagedFolder Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AF8 RID: 15096
		public static readonly ExPerformanceCounter TotalDeleteFolderSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "DeleteFolder Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AF9 RID: 15097
		public static readonly ExPerformanceCounter TotalDeleteItemSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "DeleteItem Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AFA RID: 15098
		public static readonly ExPerformanceCounter TotalExpandDLSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "ExpandDL Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AFB RID: 15099
		public static readonly ExPerformanceCounter TotalFindFolderSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "FindFolder Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AFC RID: 15100
		public static readonly ExPerformanceCounter TotalFindItemSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "FindItem Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AFD RID: 15101
		public static readonly ExPerformanceCounter TotalFindConversationSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "FindConversation Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AFE RID: 15102
		public static readonly ExPerformanceCounter TotalFindPeopleSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "FindPeople Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003AFF RID: 15103
		public static readonly ExPerformanceCounter TotalSyncPeopleSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "SyncPeople Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B00 RID: 15104
		public static readonly ExPerformanceCounter TotalSyncAutoCompleteRecipientsSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "SyncAutoCompleteRecipients Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B01 RID: 15105
		public static readonly ExPerformanceCounter TotalGetPersonaSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetPersona Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B02 RID: 15106
		public static readonly ExPerformanceCounter TotalSyncConversationSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "SyncConversation Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B03 RID: 15107
		public static readonly ExPerformanceCounter TotalGetTimeZoneOffsetsSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetTimeZoneOffsets Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B04 RID: 15108
		public static readonly ExPerformanceCounter TotalGetEventsSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetEvents Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B05 RID: 15109
		public static readonly ExPerformanceCounter TotalGetStreamingEventsSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetStreamingEvents Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B06 RID: 15110
		public static readonly ExPerformanceCounter TotalGetFolderSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetFolder Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B07 RID: 15111
		public static readonly ExPerformanceCounter TotalGetMailTipsSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetMailTips Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B08 RID: 15112
		public static readonly ExPerformanceCounter TotalPlayOnPhoneSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "PlayOnPhone Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B09 RID: 15113
		public static readonly ExPerformanceCounter TotalGetPhoneCallInformationSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetPhoneCallInformation Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B0A RID: 15114
		public static readonly ExPerformanceCounter TotalDisconnectPhoneCallSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "DisconnectPhoneCall Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B0B RID: 15115
		public static readonly ExPerformanceCounter TotalCreateUMPromptSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "CreateUMPrompt Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B0C RID: 15116
		public static readonly ExPerformanceCounter TotalGetUMPromptSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetUMPrompt Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B0D RID: 15117
		public static readonly ExPerformanceCounter TotalGetUMPromptNamesSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetUMPromptNames Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B0E RID: 15118
		public static readonly ExPerformanceCounter TotalDeleteUMPromptsSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "DeleteUMPrompts Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B0F RID: 15119
		public static readonly ExPerformanceCounter TotalGetServiceConfigurationSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetServiceConfiguration Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B10 RID: 15120
		public static readonly ExPerformanceCounter TotalGetItemSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetItem Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B11 RID: 15121
		public static readonly ExPerformanceCounter TotalGetServerTimeZonesSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetServerTimeZones Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B12 RID: 15122
		public static readonly ExPerformanceCounter TotalMoveFolderSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "MoveFolder Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B13 RID: 15123
		public static readonly ExPerformanceCounter TotalMoveItemSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "MoveItem Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B14 RID: 15124
		public static readonly ExPerformanceCounter TotalResolveNamesSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "ResolveNames Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B15 RID: 15125
		public static readonly ExPerformanceCounter TotalSendItemSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "SendItem Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B16 RID: 15126
		public static readonly ExPerformanceCounter TotalSubscribeSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "Subscribe Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B17 RID: 15127
		public static readonly ExPerformanceCounter TotalUnsubscribeSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "Unsubscribe Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B18 RID: 15128
		public static readonly ExPerformanceCounter TotalUpdateFolderSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "UpdateFolder Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B19 RID: 15129
		public static readonly ExPerformanceCounter TotalUpdateItemSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "UpdateItem Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B1A RID: 15130
		public static readonly ExPerformanceCounter TotalUpdateItemInRecoverableItemsSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "UpdateItemInRecoverableItems Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B1B RID: 15131
		public static readonly ExPerformanceCounter TotalCreateAttachmentSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "CreateAttachment Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B1C RID: 15132
		public static readonly ExPerformanceCounter TotalDeleteAttachmentSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "DeleteAttachment Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B1D RID: 15133
		public static readonly ExPerformanceCounter TotalGetAttachmentSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetAttachment Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B1E RID: 15134
		public static readonly ExPerformanceCounter TotalGetClientAccessTokenSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetClientAccessToken Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B1F RID: 15135
		public static readonly ExPerformanceCounter TotalSendNotificationSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "SendNotification Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B20 RID: 15136
		public static readonly ExPerformanceCounter TotalSyncFolderItemsSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "SyncFolderItems Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B21 RID: 15137
		public static readonly ExPerformanceCounter TotalSyncFolderHierarchySuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "SyncFolderHierarchy Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B22 RID: 15138
		public static readonly ExPerformanceCounter TotalConvertIdSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "ConvertId Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B23 RID: 15139
		public static readonly ExPerformanceCounter TotalGetDelegateSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetDelegate Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B24 RID: 15140
		public static readonly ExPerformanceCounter TotalAddDelegateSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "AddDelegate Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B25 RID: 15141
		public static readonly ExPerformanceCounter TotalRemoveDelegateSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "RemoveDelegate Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B26 RID: 15142
		public static readonly ExPerformanceCounter TotalUpdateDelegateSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "UpdateDelegate Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B27 RID: 15143
		public static readonly ExPerformanceCounter TotalCreateUserConfigurationSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "CreateUserConfiguration Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B28 RID: 15144
		public static readonly ExPerformanceCounter TotalDeleteUserConfigurationSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "DeleteUserConfiguration Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B29 RID: 15145
		public static readonly ExPerformanceCounter TotalGetUserConfigurationSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetUserConfiguration Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B2A RID: 15146
		public static readonly ExPerformanceCounter TotalUpdateUserConfigurationSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "UpdateUserConfiguration Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B2B RID: 15147
		public static readonly ExPerformanceCounter TotalGetUserAvailabilitySuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetUserAvailability Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B2C RID: 15148
		public static readonly ExPerformanceCounter TotalGetUserOofSettingsSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetUserOofSettings Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B2D RID: 15149
		public static readonly ExPerformanceCounter TotalSetUserOofSettingsSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "SetUserOofSettings Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B2E RID: 15150
		public static readonly ExPerformanceCounter TotalGetSharingMetadataSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetSharingMetadata Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B2F RID: 15151
		public static readonly ExPerformanceCounter TotalRefreshSharingFolderSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "RefreshSharingFolder Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B30 RID: 15152
		public static readonly ExPerformanceCounter TotalGetSharingFolderSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetSharingFolder Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B31 RID: 15153
		public static readonly ExPerformanceCounter TotalGetRoomListsSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetRoomLists Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B32 RID: 15154
		public static readonly ExPerformanceCounter TotalGetRoomsSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetRooms Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B33 RID: 15155
		public static readonly ExPerformanceCounter TotalPerformReminderActionSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "PerformReminderAction Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B34 RID: 15156
		public static readonly ExPerformanceCounter TotalGetRemindersSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetReminders Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B35 RID: 15157
		public static readonly ExPerformanceCounter TotalProvisionSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "Provision Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B36 RID: 15158
		public static readonly ExPerformanceCounter TotalDeprovisionSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "Deprovision Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B37 RID: 15159
		public static readonly ExPerformanceCounter TotalLogPushNotificationDataSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "LogPushNotificationData Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B38 RID: 15160
		public static readonly ExPerformanceCounter TotalFindMessageTrackingReportSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "FindMessageTrackingReport Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B39 RID: 15161
		public static readonly ExPerformanceCounter TotalGetMessageTrackingReportSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetMessageTrackingReport Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B3A RID: 15162
		public static readonly ExPerformanceCounter TotalApplyConversationActionSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "ApplyConversationAction Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B3B RID: 15163
		public static readonly ExPerformanceCounter TotalEmptyFolderSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "EmptyFolder Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B3C RID: 15164
		public static readonly ExPerformanceCounter TotalUploadItemsSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "UploadItems Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B3D RID: 15165
		public static readonly ExPerformanceCounter TotalExportItemsSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "ExportItems Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B3E RID: 15166
		public static readonly ExPerformanceCounter TotalExecuteDiagnosticMethodSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "ExecuteDiagnosticMethod Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B3F RID: 15167
		public static readonly ExPerformanceCounter TotalFindMailboxStatisticsByKeywordsSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "FindMailboxStatisticsByKeywords Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B40 RID: 15168
		public static readonly ExPerformanceCounter TotalGetSearchableMailboxesSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetSearchableMailboxes Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B41 RID: 15169
		public static readonly ExPerformanceCounter TotalSearchMailboxesSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "SearchMailboxes Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B42 RID: 15170
		public static readonly ExPerformanceCounter TotalGetDiscoverySearchConfigurationSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetDiscoverySearchConfiguration Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B43 RID: 15171
		public static readonly ExPerformanceCounter TotalGetHoldOnMailboxesSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetHoldOnMailboxes Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B44 RID: 15172
		public static readonly ExPerformanceCounter TotalSetHoldOnMailboxesSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "SetHoldOnMailboxes Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B45 RID: 15173
		public static readonly ExPerformanceCounter TotalGetNonIndexableItemStatisticsSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetNonIndexableItemStatistics Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B46 RID: 15174
		public static readonly ExPerformanceCounter TotalGetNonIndexableItemDetailsSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetNonIndexableItemDetails Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B47 RID: 15175
		public static readonly ExPerformanceCounter TotalMarkAllItemsAsReadSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "MarkAllItemsAsRead Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B48 RID: 15176
		public static readonly ExPerformanceCounter TotalGetClientExtensionSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetClientExtension Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B49 RID: 15177
		public static readonly ExPerformanceCounter TotalGetEncryptionConfigurationSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetEncryptionConfiguration Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B4A RID: 15178
		public static readonly ExPerformanceCounter TotalSetClientExtensionSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "SetClientExtension Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B4B RID: 15179
		public static readonly ExPerformanceCounter TotalSetEncryptionConfigurationSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "SetEncryptionConfiguration Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B4C RID: 15180
		public static readonly ExPerformanceCounter TotalSubscribeToPushNotificationSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "SubscribeToPushNotification Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B4D RID: 15181
		public static readonly ExPerformanceCounter TotalUnsubscribeToPushNotificationSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "UnsubscribeToPushNotification Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B4E RID: 15182
		public static readonly ExPerformanceCounter TotalCreateUnifiedMailboxSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "CreateUnifiedMailbox Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B4F RID: 15183
		public static readonly ExPerformanceCounter TotalGetAppManifestsSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetAppManifests Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B50 RID: 15184
		public static readonly ExPerformanceCounter TotalInstallAppSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "InstallApp Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B51 RID: 15185
		public static readonly ExPerformanceCounter TotalUninstallAppSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "UninstallApp Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B52 RID: 15186
		public static readonly ExPerformanceCounter TotalDisableAppSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "DisableApp Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B53 RID: 15187
		public static readonly ExPerformanceCounter TotalGetAppMarketplaceUrlSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetAppMarketplaceUrl Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B54 RID: 15188
		public static readonly ExPerformanceCounter TotalGetClientExtensionTokenSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetClientExtensionToken Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B55 RID: 15189
		public static readonly ExPerformanceCounter TotalGetEncryptionConfigurationTokenSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetEncryptionConfigurationToken Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B56 RID: 15190
		public static readonly ExPerformanceCounter TotalGetConversationItemsSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetConversationItems Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B57 RID: 15191
		public static readonly ExPerformanceCounter TotalGetModernConversationItemsSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetModernConversationItems Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B58 RID: 15192
		public static readonly ExPerformanceCounter TotalGetThreadedConversationItemsSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetThreadedConversationItems Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B59 RID: 15193
		public static readonly ExPerformanceCounter TotalGetModernConversationAttachmentsSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetModernConversationAttachments Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B5A RID: 15194
		public static readonly ExPerformanceCounter TotalSetModernGroupMembershipSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "SetModernGroupMembership Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B5B RID: 15195
		public static readonly ExPerformanceCounter TotalGetUserRetentionPolicyTagsSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetUserRetentionPolicyTags Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B5C RID: 15196
		public static readonly ExPerformanceCounter TotalGetUserPhotoSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetUserPhoto Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B5D RID: 15197
		private static readonly ExPerformanceCounter StartFindInGALSpeechRecognitionRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "StartFindInGALSpeechRecognition Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B5E RID: 15198
		public static readonly ExPerformanceCounter TotalStartFindInGALSpeechRecognitionRequests = new ExPerformanceCounter("MSExchangeWS", "StartFindInGALSpeechRecognition Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.StartFindInGALSpeechRecognitionRequestsPerSecond
		});

		// Token: 0x04003B5F RID: 15199
		public static readonly ExPerformanceCounter StartFindInGALSpeechRecognitionAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "StartFindInGALSpeechRecognition Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B60 RID: 15200
		public static readonly ExPerformanceCounter TotalStartFindInGALSpeechRecognitionSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "StartFindInGALSpeechRecognition Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B61 RID: 15201
		private static readonly ExPerformanceCounter CompleteFindInGALSpeechRecognitionRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "CompleteFindInGALSpeechRecognition Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B62 RID: 15202
		public static readonly ExPerformanceCounter TotalCompleteFindInGALSpeechRecognitionRequests = new ExPerformanceCounter("MSExchangeWS", "CompleteFindInGALSpeechRecognition Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.CompleteFindInGALSpeechRecognitionRequestsPerSecond
		});

		// Token: 0x04003B63 RID: 15203
		public static readonly ExPerformanceCounter CompleteFindInGALSpeechRecognitionAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "CompleteFindInGALSpeechRecognition Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B64 RID: 15204
		public static readonly ExPerformanceCounter TotalCompleteFindInGALSpeechRecognitionSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "CompleteFindInGALSpeechRecognition Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B65 RID: 15205
		private static readonly ExPerformanceCounter CreateUMCallDataRecordRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "CreateUMCallDataRecord Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B66 RID: 15206
		public static readonly ExPerformanceCounter TotalCreateUMCallDataRecordRequests = new ExPerformanceCounter("MSExchangeWS", "CreateUMCallDataRecord Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.CreateUMCallDataRecordRequestsPerSecond
		});

		// Token: 0x04003B67 RID: 15207
		public static readonly ExPerformanceCounter CreateUMCallDataRecordAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "CreateUMCallDataRecord Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B68 RID: 15208
		public static readonly ExPerformanceCounter TotalCreateUMCallDataRecordSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "CreateUMCallDataRecord Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B69 RID: 15209
		private static readonly ExPerformanceCounter GetUMCallDataRecordsRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetUMCallDataRecords Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B6A RID: 15210
		public static readonly ExPerformanceCounter TotalGetUMCallDataRecordsRequests = new ExPerformanceCounter("MSExchangeWS", "GetUMCallDataRecords Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetUMCallDataRecordsRequestsPerSecond
		});

		// Token: 0x04003B6B RID: 15211
		public static readonly ExPerformanceCounter GetUMCallDataRecordsAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetUMCallDataRecords Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B6C RID: 15212
		public static readonly ExPerformanceCounter TotalGetUMCallDataRecordsSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetUMCallDataRecords Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B6D RID: 15213
		private static readonly ExPerformanceCounter GetUMCallSummaryRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetUMCallSummary Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B6E RID: 15214
		public static readonly ExPerformanceCounter TotalGetUMCallSummaryRequests = new ExPerformanceCounter("MSExchangeWS", "GetUMCallSummary Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetUMCallSummaryRequestsPerSecond
		});

		// Token: 0x04003B6F RID: 15215
		public static readonly ExPerformanceCounter GetUMCallSummaryAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetUMCallSummary Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B70 RID: 15216
		public static readonly ExPerformanceCounter TotalGetUMCallSummarySuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetUMCallSummary Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B71 RID: 15217
		private static readonly ExPerformanceCounter GetUserPhotoDataRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetUserPhotoData Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B72 RID: 15218
		public static readonly ExPerformanceCounter TotalGetUserPhotoDataRequests = new ExPerformanceCounter("MSExchangeWS", "GetUserPhotoData Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetUserPhotoDataRequestsPerSecond
		});

		// Token: 0x04003B73 RID: 15219
		public static readonly ExPerformanceCounter GetUserPhotoDataAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetUserPhotoData Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B74 RID: 15220
		public static readonly ExPerformanceCounter TotalGetUserPhotoDataSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetUserPhotoData Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B75 RID: 15221
		private static readonly ExPerformanceCounter InitUMMailboxRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "InitUMMailbox Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B76 RID: 15222
		public static readonly ExPerformanceCounter TotalInitUMMailboxRequests = new ExPerformanceCounter("MSExchangeWS", "InitUMMailbox Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.InitUMMailboxRequestsPerSecond
		});

		// Token: 0x04003B77 RID: 15223
		public static readonly ExPerformanceCounter InitUMMailboxAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "InitUMMailbox Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B78 RID: 15224
		public static readonly ExPerformanceCounter TotalInitUMMailboxSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "InitUMMailbox Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B79 RID: 15225
		private static readonly ExPerformanceCounter ResetUMMailboxRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "ResetUMMailbox Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B7A RID: 15226
		public static readonly ExPerformanceCounter TotalResetUMMailboxRequests = new ExPerformanceCounter("MSExchangeWS", "ResetUMMailbox Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.ResetUMMailboxRequestsPerSecond
		});

		// Token: 0x04003B7B RID: 15227
		public static readonly ExPerformanceCounter ResetUMMailboxAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "ResetUMMailbox Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B7C RID: 15228
		public static readonly ExPerformanceCounter TotalResetUMMailboxSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "ResetUMMailbox Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B7D RID: 15229
		private static readonly ExPerformanceCounter ValidateUMPinRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "ValidateUMPin Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B7E RID: 15230
		public static readonly ExPerformanceCounter TotalValidateUMPinRequests = new ExPerformanceCounter("MSExchangeWS", "ValidateUMPin Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.ValidateUMPinRequestsPerSecond
		});

		// Token: 0x04003B7F RID: 15231
		public static readonly ExPerformanceCounter ValidateUMPinAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "ValidateUMPin Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B80 RID: 15232
		public static readonly ExPerformanceCounter TotalValidateUMPinSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "ValidateUMPin Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B81 RID: 15233
		private static readonly ExPerformanceCounter SaveUMPinRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "SaveUMPin Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B82 RID: 15234
		public static readonly ExPerformanceCounter TotalSaveUMPinRequests = new ExPerformanceCounter("MSExchangeWS", "SaveUMPin Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.SaveUMPinRequestsPerSecond
		});

		// Token: 0x04003B83 RID: 15235
		public static readonly ExPerformanceCounter SaveUMPinAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "SaveUMPin Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B84 RID: 15236
		public static readonly ExPerformanceCounter TotalSaveUMPinSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "SaveUMPin Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B85 RID: 15237
		private static readonly ExPerformanceCounter GetUMPinRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetUMPin Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B86 RID: 15238
		public static readonly ExPerformanceCounter TotalGetUMPinRequests = new ExPerformanceCounter("MSExchangeWS", "GetUMPin Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetUMPinRequestsPerSecond
		});

		// Token: 0x04003B87 RID: 15239
		public static readonly ExPerformanceCounter GetUMPinAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetUMPin Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B88 RID: 15240
		public static readonly ExPerformanceCounter TotalGetUMPinSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetUMPin Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B89 RID: 15241
		private static readonly ExPerformanceCounter GetClientIntentRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetClientIntent Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B8A RID: 15242
		public static readonly ExPerformanceCounter TotalGetClientIntentRequests = new ExPerformanceCounter("MSExchangeWS", "GetClientIntent Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetClientIntentRequestsPerSecond
		});

		// Token: 0x04003B8B RID: 15243
		public static readonly ExPerformanceCounter GetClientIntentAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetClientIntent Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B8C RID: 15244
		public static readonly ExPerformanceCounter TotalGetClientIntentSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetClientIntent Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B8D RID: 15245
		private static readonly ExPerformanceCounter GetUMSubscriberCallAnsweringDataRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetUMSubscriberCallAnsweringData Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B8E RID: 15246
		public static readonly ExPerformanceCounter TotalGetUMSubscriberCallAnsweringDataRequests = new ExPerformanceCounter("MSExchangeWS", "GetUMSubscriberCallAnsweringData Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetUMSubscriberCallAnsweringDataRequestsPerSecond
		});

		// Token: 0x04003B8F RID: 15247
		public static readonly ExPerformanceCounter GetUMSubscriberCallAnsweringDataAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetUMSubscriberCallAnsweringData Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B90 RID: 15248
		public static readonly ExPerformanceCounter TotalGetUMSubscriberCallAnsweringDataSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetUMSubscriberCallAnsweringData Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B91 RID: 15249
		private static readonly ExPerformanceCounter UpdateMailboxAssociationRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "UpdateMailboxAssociation Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B92 RID: 15250
		public static readonly ExPerformanceCounter TotalUpdateMailboxAssociationRequests = new ExPerformanceCounter("MSExchangeWS", "UpdateMailboxAssociation Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.UpdateMailboxAssociationRequestsPerSecond
		});

		// Token: 0x04003B93 RID: 15251
		public static readonly ExPerformanceCounter UpdateMailboxAssociationAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "UpdateMailboxAssociation Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B94 RID: 15252
		public static readonly ExPerformanceCounter TotalUpdateMailboxAssociationSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "UpdateMailboxAssociation Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B95 RID: 15253
		private static readonly ExPerformanceCounter UpdateGroupMailboxRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "UpdateGroupMailbox Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B96 RID: 15254
		public static readonly ExPerformanceCounter TotalUpdateGroupMailboxRequests = new ExPerformanceCounter("MSExchangeWS", "UpdateGroupMailbox Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.UpdateGroupMailboxRequestsPerSecond
		});

		// Token: 0x04003B97 RID: 15255
		public static readonly ExPerformanceCounter UpdateGroupMailboxAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "UpdateGroupMailbox Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B98 RID: 15256
		public static readonly ExPerformanceCounter TotalUpdateGroupMailboxSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "UpdateGroupMailbox Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B99 RID: 15257
		private static readonly ExPerformanceCounter GetCalendarEventRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetCalendarEvent Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B9A RID: 15258
		public static readonly ExPerformanceCounter TotalGetCalendarEventRequests = new ExPerformanceCounter("MSExchangeWS", "GetCalendarEvent Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetCalendarEventRequestsPerSecond
		});

		// Token: 0x04003B9B RID: 15259
		public static readonly ExPerformanceCounter GetCalendarEventAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetCalendarEvent Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B9C RID: 15260
		public static readonly ExPerformanceCounter TotalGetCalendarEventSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetCalendarEvent Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B9D RID: 15261
		private static readonly ExPerformanceCounter GetCalendarViewRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetCalendarView Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003B9E RID: 15262
		public static readonly ExPerformanceCounter TotalGetCalendarViewRequests = new ExPerformanceCounter("MSExchangeWS", "GetCalendarView Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetCalendarViewRequestsPerSecond
		});

		// Token: 0x04003B9F RID: 15263
		public static readonly ExPerformanceCounter GetCalendarViewAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetCalendarView Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BA0 RID: 15264
		public static readonly ExPerformanceCounter TotalGetCalendarViewSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetCalendarView Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BA1 RID: 15265
		private static readonly ExPerformanceCounter GetBirthdayCalendarViewRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetBirthdayCalendarView Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BA2 RID: 15266
		public static readonly ExPerformanceCounter TotalGetBirthdayCalendarViewRequests = new ExPerformanceCounter("MSExchangeWS", "GetBirthdayCalendarView Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetBirthdayCalendarViewRequestsPerSecond
		});

		// Token: 0x04003BA3 RID: 15267
		public static readonly ExPerformanceCounter GetBirthdayCalendarViewAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetBirthdayCalendarView Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BA4 RID: 15268
		public static readonly ExPerformanceCounter TotalGetBirthdayCalendarViewSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetBirthdayCalendarView Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BA5 RID: 15269
		private static readonly ExPerformanceCounter CreateCalendarEventRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "CreateCalendarEvent Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BA6 RID: 15270
		public static readonly ExPerformanceCounter TotalCreateCalendarEventRequests = new ExPerformanceCounter("MSExchangeWS", "CreateCalendarEvent Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.CreateCalendarEventRequestsPerSecond
		});

		// Token: 0x04003BA7 RID: 15271
		public static readonly ExPerformanceCounter CreateCalendarEventAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "CreateCalendarEvent Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BA8 RID: 15272
		public static readonly ExPerformanceCounter TotalCreateCalendarEventSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "CreateCalendarEvent Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BA9 RID: 15273
		private static readonly ExPerformanceCounter UpdateCalendarEventRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "UpdateCalendarEvent Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BAA RID: 15274
		public static readonly ExPerformanceCounter TotalUpdateCalendarEventRequests = new ExPerformanceCounter("MSExchangeWS", "UpdateCalendarEvent Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.UpdateCalendarEventRequestsPerSecond
		});

		// Token: 0x04003BAB RID: 15275
		public static readonly ExPerformanceCounter UpdateCalendarEventAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "UpdateCalendarEvent Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BAC RID: 15276
		public static readonly ExPerformanceCounter TotalUpdateCalendarEventSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "UpdateCalendarEvent Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BAD RID: 15277
		private static readonly ExPerformanceCounter CancelCalendarEventRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "CancelCalendarEvent Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BAE RID: 15278
		public static readonly ExPerformanceCounter TotalCancelCalendarEventRequests = new ExPerformanceCounter("MSExchangeWS", "CancelCalendarEvent Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.CancelCalendarEventRequestsPerSecond
		});

		// Token: 0x04003BAF RID: 15279
		public static readonly ExPerformanceCounter CancelCalendarEventAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "CancelCalendarEvent Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BB0 RID: 15280
		public static readonly ExPerformanceCounter TotalCancelCalendarEventSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "CancelCalendarEvent Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BB1 RID: 15281
		private static readonly ExPerformanceCounter RespondToCalendarEventRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "RespondToCalendarEvent Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BB2 RID: 15282
		public static readonly ExPerformanceCounter TotalRespondToCalendarEventRequests = new ExPerformanceCounter("MSExchangeWS", "RespondToCalendarEvent Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.RespondToCalendarEventRequestsPerSecond
		});

		// Token: 0x04003BB3 RID: 15283
		public static readonly ExPerformanceCounter RespondToCalendarEventAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "RespondToCalendarEvent Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BB4 RID: 15284
		public static readonly ExPerformanceCounter TotalRespondToCalendarEventSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "RespondToCalendarEvent Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BB5 RID: 15285
		private static readonly ExPerformanceCounter RefreshGALContactsFolderRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "RefreshGALContactsFolder Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BB6 RID: 15286
		public static readonly ExPerformanceCounter TotalRefreshGALContactsFolderRequests = new ExPerformanceCounter("MSExchangeWS", "RefreshGALContactsFolder Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.RefreshGALContactsFolderRequestsPerSecond
		});

		// Token: 0x04003BB7 RID: 15287
		public static readonly ExPerformanceCounter RefreshGALContactsFolderAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "RefreshGALContactsFolder Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BB8 RID: 15288
		public static readonly ExPerformanceCounter TotalRefreshGALContactsFolderSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "RefreshGALContactsFolder Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BB9 RID: 15289
		private static readonly ExPerformanceCounter SubscribeToConversationChangesRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "SubscribeToConversationChanges Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BBA RID: 15290
		public static readonly ExPerformanceCounter TotalSubscribeToConversationChangesRequests = new ExPerformanceCounter("MSExchangeWS", "SubscribeToConversationChanges Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.SubscribeToConversationChangesRequestsPerSecond
		});

		// Token: 0x04003BBB RID: 15291
		public static readonly ExPerformanceCounter SubscribeToConversationChangesAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "SubscribeToConversationChanges Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BBC RID: 15292
		public static readonly ExPerformanceCounter TotalSubscribeToConversationChangesSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "SubscribeToConversationChanges Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BBD RID: 15293
		private static readonly ExPerformanceCounter SubscribeToHierarchyChangesRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "SubscribeToHierarchyChanges Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BBE RID: 15294
		public static readonly ExPerformanceCounter TotalSubscribeToHierarchyChangesRequests = new ExPerformanceCounter("MSExchangeWS", "SubscribeToHierarchyChanges Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.SubscribeToHierarchyChangesRequestsPerSecond
		});

		// Token: 0x04003BBF RID: 15295
		public static readonly ExPerformanceCounter SubscribeToHierarchyChangesAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "SubscribeToHierarchyChanges Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BC0 RID: 15296
		public static readonly ExPerformanceCounter TotalSubscribeToHierarchyChangesSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "SubscribeToHierarchyChanges Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BC1 RID: 15297
		private static readonly ExPerformanceCounter SubscribeToMessageChangesRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "SubscribeToMessageChanges Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BC2 RID: 15298
		public static readonly ExPerformanceCounter TotalSubscribeToMessageChangesRequests = new ExPerformanceCounter("MSExchangeWS", "SubscribeToMessageChanges Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.SubscribeToMessageChangesRequestsPerSecond
		});

		// Token: 0x04003BC3 RID: 15299
		public static readonly ExPerformanceCounter SubscribeToMessageChangesAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "SubscribeToMessageChanges Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BC4 RID: 15300
		public static readonly ExPerformanceCounter TotalSubscribeToMessageChangesSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "SubscribeToMessageChanges Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BC5 RID: 15301
		private static readonly ExPerformanceCounter DeleteCalendarEventRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "DeleteCalendarEvent Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BC6 RID: 15302
		public static readonly ExPerformanceCounter TotalDeleteCalendarEventRequests = new ExPerformanceCounter("MSExchangeWS", "DeleteCalendarEvent Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.DeleteCalendarEventRequestsPerSecond
		});

		// Token: 0x04003BC7 RID: 15303
		public static readonly ExPerformanceCounter DeleteCalendarEventAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "DeleteCalendarEvent Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BC8 RID: 15304
		public static readonly ExPerformanceCounter TotalDeleteCalendarEventSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "DeleteCalendarEvent Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BC9 RID: 15305
		private static readonly ExPerformanceCounter ForwardCalendarEventRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "ForwardCalendarEvent Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BCA RID: 15306
		public static readonly ExPerformanceCounter TotalForwardCalendarEventRequests = new ExPerformanceCounter("MSExchangeWS", "ForwardCalendarEvent Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.ForwardCalendarEventRequestsPerSecond
		});

		// Token: 0x04003BCB RID: 15307
		public static readonly ExPerformanceCounter ForwardCalendarEventAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "ForwardCalendarEvent Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BCC RID: 15308
		public static readonly ExPerformanceCounter TotalForwardCalendarEventSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "ForwardCalendarEvent Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BCD RID: 15309
		public static readonly ExPerformanceCounter LikeItemRequests = new ExPerformanceCounter("MSExchangeWS", "LikeItem Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BCE RID: 15310
		private static readonly ExPerformanceCounter LikeItemPerSecond = new ExPerformanceCounter("MSExchangeWS", "LikeItem Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BCF RID: 15311
		public static readonly ExPerformanceCounter LikeItemAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "LikeItem Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BD0 RID: 15312
		public static readonly ExPerformanceCounter LikeItemSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "LikeItem Successful Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.LikeItemPerSecond
		});

		// Token: 0x04003BD1 RID: 15313
		public static readonly ExPerformanceCounter GetLikersRequests = new ExPerformanceCounter("MSExchangeWS", "GetLikers Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BD2 RID: 15314
		private static readonly ExPerformanceCounter GetLikersPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetLikers Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BD3 RID: 15315
		public static readonly ExPerformanceCounter GetLikersAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetLikers Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BD4 RID: 15316
		public static readonly ExPerformanceCounter GetLikersSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetLikers Successful Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetLikersPerSecond
		});

		// Token: 0x04003BD5 RID: 15317
		private static readonly ExPerformanceCounter ExpandCalendarEventRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "ExpandCalendarEvent Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BD6 RID: 15318
		public static readonly ExPerformanceCounter TotalExpandCalendarEventRequests = new ExPerformanceCounter("MSExchangeWS", "ExpandCalendarEvent Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.ExpandCalendarEventRequestsPerSecond
		});

		// Token: 0x04003BD7 RID: 15319
		public static readonly ExPerformanceCounter ExpandCalendarEventAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "ExpandCalendarEvent Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BD8 RID: 15320
		public static readonly ExPerformanceCounter TotalExpandCalendarEventSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "ExpandCalendarEvent Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BD9 RID: 15321
		public static readonly ExPerformanceCounter GetConversationItemsDiagnosticsRequests = new ExPerformanceCounter("MSExchangeWS", "GetConversationItemsDiagnostics Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BDA RID: 15322
		private static readonly ExPerformanceCounter GetConversationItemsDiagnosticsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetConversationItemsDiagnostics Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BDB RID: 15323
		public static readonly ExPerformanceCounter GetConversationItemsDiagnosticsAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetConversationItemsDiagnostics Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BDC RID: 15324
		public static readonly ExPerformanceCounter GetConversationItemsDiagnosticsSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetConversationItemsDiagnostics Successful Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetConversationItemsDiagnosticsPerSecond
		});

		// Token: 0x04003BDD RID: 15325
		private static readonly ExPerformanceCounter GetComplianceConfigurationPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetComplianceConfiguration Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BDE RID: 15326
		public static readonly ExPerformanceCounter TotalGetComplianceConfiguration = new ExPerformanceCounter("MSExchangeWS", "GetComplianceConfiguration Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetComplianceConfigurationPerSecond
		});

		// Token: 0x04003BDF RID: 15327
		public static readonly ExPerformanceCounter GetComplianceConfigurationAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetComplianceConfiguration Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BE0 RID: 15328
		public static readonly ExPerformanceCounter TotalGetComplianceConfigurationSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetComplianceConfiguration Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BE1 RID: 15329
		private static readonly ExPerformanceCounter PerformInstantSearchRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "PerformInstantSearch Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BE2 RID: 15330
		public static readonly ExPerformanceCounter TotalPerformInstantSearchRequests = new ExPerformanceCounter("MSExchangeWS", "PerformInstantSearch Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.PerformInstantSearchRequestsPerSecond
		});

		// Token: 0x04003BE3 RID: 15331
		public static readonly ExPerformanceCounter PerformInstantSearchAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "PerformInstantSearch Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BE4 RID: 15332
		public static readonly ExPerformanceCounter PerformInstantSearchSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "PerformInstantSearch Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BE5 RID: 15333
		private static readonly ExPerformanceCounter EndInstantSearchSessionRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "EndInstantSearchSession Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BE6 RID: 15334
		public static readonly ExPerformanceCounter TotalEndInstantSearchSessionRequests = new ExPerformanceCounter("MSExchangeWS", "EndInstantSearchSession Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.EndInstantSearchSessionRequestsPerSecond
		});

		// Token: 0x04003BE7 RID: 15335
		public static readonly ExPerformanceCounter EndInstantSearchSessionAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "EndInstantSearchSession Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BE8 RID: 15336
		public static readonly ExPerformanceCounter EndInstantSearchSessionSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "EndInstantSearchSession Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BE9 RID: 15337
		public static readonly ExPerformanceCounter GetUserUnifiedGroupsRequests = new ExPerformanceCounter("MSExchangeWS", "GetUserUnifiedGroups Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BEA RID: 15338
		private static readonly ExPerformanceCounter GetUserUnifiedGroupsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetUserUnifiedGroups Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BEB RID: 15339
		public static readonly ExPerformanceCounter GetUserUnifiedGroupsAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetUserUnifiedGroups Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BEC RID: 15340
		public static readonly ExPerformanceCounter GetUserUnifiedGroupsSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetUserUnifiedGroups Successful Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetUserUnifiedGroupsPerSecond
		});

		// Token: 0x04003BED RID: 15341
		private static readonly ExPerformanceCounter GetPeopleICommunicateWithRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetPeopleICommunicateWith Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BEE RID: 15342
		public static readonly ExPerformanceCounter TotalGetPeopleICommunicateWithRequests = new ExPerformanceCounter("MSExchangeWS", "GetPeopleICommunicateWith Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetPeopleICommunicateWithRequestsPerSecond
		});

		// Token: 0x04003BEF RID: 15343
		public static readonly ExPerformanceCounter GetPeopleICommunicateWithAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetPeopleICommunicateWith Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BF0 RID: 15344
		public static readonly ExPerformanceCounter TotalGetPeopleICommunicateWithSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetPeopleICommunicateWith Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BF1 RID: 15345
		private static readonly ExPerformanceCounter MaskAutoCompleteRecipientRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "MaskAutoCompleteRecipient Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BF2 RID: 15346
		public static readonly ExPerformanceCounter MaskAutoCompleteRecipientRequests = new ExPerformanceCounter("MSExchangeWS", "MaskAutoCompleteRecipient Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.MaskAutoCompleteRecipientRequestsPerSecond
		});

		// Token: 0x04003BF3 RID: 15347
		public static readonly ExPerformanceCounter MaskAutoCompleteRecipientAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "MaskAutoCompleteRecipient Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BF4 RID: 15348
		public static readonly ExPerformanceCounter TotalMaskAutoCompleteRecipientSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "MaskAutoCompleteRecipient Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BF5 RID: 15349
		private static readonly ExPerformanceCounter GetClutterStateRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "GetClutterState Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BF6 RID: 15350
		public static readonly ExPerformanceCounter TotalGetClutterStateRequests = new ExPerformanceCounter("MSExchangeWS", "GetClutterState Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.GetClutterStateRequestsPerSecond
		});

		// Token: 0x04003BF7 RID: 15351
		public static readonly ExPerformanceCounter GetClutterStateAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "GetClutterState Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BF8 RID: 15352
		public static readonly ExPerformanceCounter TotalGetClutterStateSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "GetClutterState Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BF9 RID: 15353
		private static readonly ExPerformanceCounter SetClutterStateRequestsPerSecond = new ExPerformanceCounter("MSExchangeWS", "SetClutterState Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BFA RID: 15354
		public static readonly ExPerformanceCounter TotalSetClutterStateRequests = new ExPerformanceCounter("MSExchangeWS", "SetClutterState Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			WsPerformanceCounters.SetClutterStateRequestsPerSecond
		});

		// Token: 0x04003BFB RID: 15355
		public static readonly ExPerformanceCounter SetClutterStateAverageResponseTime = new ExPerformanceCounter("MSExchangeWS", "SetClutterState Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BFC RID: 15356
		public static readonly ExPerformanceCounter TotalSetClutterStateSuccessfulRequests = new ExPerformanceCounter("MSExchangeWS", "SetClutterState Successful Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003BFD RID: 15357
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			WsPerformanceCounters.TotalAddAggregatedAccountRequests,
			WsPerformanceCounters.AddAggregatedAccountAverageResponseTime,
			WsPerformanceCounters.TotalIsOffice365DomainRequests,
			WsPerformanceCounters.IsOffice365DomainAverageResponseTime,
			WsPerformanceCounters.TotalGetAggregatedAccountRequests,
			WsPerformanceCounters.GetAggregatedAccountAverageResponseTime,
			WsPerformanceCounters.TotalRemoveAggregatedAccountRequests,
			WsPerformanceCounters.RemoveAggregatedAccountAverageResponseTime,
			WsPerformanceCounters.TotalSetAggregatedAccountRequests,
			WsPerformanceCounters.SetAggregatedAccountAverageResponseTime,
			WsPerformanceCounters.TotalGetItemRequests,
			WsPerformanceCounters.GetItemAverageResponseTime,
			WsPerformanceCounters.TotalConvertIdRequests,
			WsPerformanceCounters.ConvertIdAverageResponseTime,
			WsPerformanceCounters.TotalIdsConverted,
			WsPerformanceCounters.TotalCreateItemRequests,
			WsPerformanceCounters.CreateItemAverageResponseTime,
			WsPerformanceCounters.TotalPostModernGroupItemRequests,
			WsPerformanceCounters.PostModernGroupItemAverageResponseTime,
			WsPerformanceCounters.TotalUpdateAndPostModernGroupItemRequests,
			WsPerformanceCounters.UpdateAndPostModernGroupItemAverageResponseTime,
			WsPerformanceCounters.TotalCreateResponseFromModernGroupRequests,
			WsPerformanceCounters.CreateResponseFromModernGroupAverageResponseTime,
			WsPerformanceCounters.TotalUploadItemsRequests,
			WsPerformanceCounters.UploadItemsAverageResponseTime,
			WsPerformanceCounters.TotalUploadLargeItemRequests,
			WsPerformanceCounters.UploadLargeItemAverageResponseTime,
			WsPerformanceCounters.TotalChunkUploadRequests,
			WsPerformanceCounters.ChunkUploadAverageResponseTime,
			WsPerformanceCounters.TotalCompleteLargeItemUploadRequests,
			WsPerformanceCounters.CompleteLargeItemUploadAverageResponseTime,
			WsPerformanceCounters.TotalExportItemsRequests,
			WsPerformanceCounters.ExportItemsAverageResponseTime,
			WsPerformanceCounters.TotalDeleteItemRequests,
			WsPerformanceCounters.DeleteItemAverageResponseTime,
			WsPerformanceCounters.TotalUpdateItemRequests,
			WsPerformanceCounters.UpdateItemAverageResponseTime,
			WsPerformanceCounters.TotalUpdateItemInRecoverableItemsRequests,
			WsPerformanceCounters.UpdateItemInRecoverableItemsAverageResponseTime,
			WsPerformanceCounters.TotalMarkAllItemsAsReadRequests,
			WsPerformanceCounters.MarkAllItemsAsReadAverageResponseTime,
			WsPerformanceCounters.TotalMarkAsJunkRequests,
			WsPerformanceCounters.MarkAsJunkAverageResponseTime,
			WsPerformanceCounters.TotalMarkAsJunkSuccessfulRequests,
			WsPerformanceCounters.TotalGetClientExtensionRequests,
			WsPerformanceCounters.GetClientExtensionAverageResponseTime,
			WsPerformanceCounters.TotalGetEncryptionConfigurationRequests,
			WsPerformanceCounters.GetEncryptionConfigurationAverageResponseTime,
			WsPerformanceCounters.TotalSetClientExtensionRequests,
			WsPerformanceCounters.SetClientExtensionAverageResponseTime,
			WsPerformanceCounters.TotalSetEncryptionConfigurationRequests,
			WsPerformanceCounters.SetEncryptionConfigurationAverageResponseTime,
			WsPerformanceCounters.TotalCreateUnifiedMailboxRequests,
			WsPerformanceCounters.CreateUnifiedMailboxAverageResponseTime,
			WsPerformanceCounters.TotalGetAppManifestsRequests,
			WsPerformanceCounters.GetAppManifestsAverageResponseTime,
			WsPerformanceCounters.TotalGetClientExtensionTokenRequests,
			WsPerformanceCounters.GetClientExtensionTokenAverageResponseTime,
			WsPerformanceCounters.TotalGetEncryptionConfigurationTokenRequests,
			WsPerformanceCounters.GetEncryptionConfigurationTokenAverageResponseTime,
			WsPerformanceCounters.TotalInstallAppRequests,
			WsPerformanceCounters.InstallAppAverageResponseTime,
			WsPerformanceCounters.TotalUninstallAppRequests,
			WsPerformanceCounters.UninstallAppAverageResponseTime,
			WsPerformanceCounters.TotalDisableAppRequests,
			WsPerformanceCounters.DisableAppAverageResponseTime,
			WsPerformanceCounters.TotalGetAppMarketplaceUrlRequests,
			WsPerformanceCounters.GetAppMarketplaceUrlAverageResponseTime,
			WsPerformanceCounters.TotalAddImContactToGroupRequests,
			WsPerformanceCounters.AddImContactToGroupAverageResponseTime,
			WsPerformanceCounters.TotalAddImContactToGroupSuccessfulRequests,
			WsPerformanceCounters.TotalRemoveImContactFromGroupRequests,
			WsPerformanceCounters.RemoveImContactFromGroupAverageResponseTime,
			WsPerformanceCounters.TotalRemoveImContactFromGroupSuccessfulRequests,
			WsPerformanceCounters.TotalAddNewImContactToGroupRequests,
			WsPerformanceCounters.AddNewImContactToGroupAverageResponseTime,
			WsPerformanceCounters.TotalAddNewImContactToGroupSuccessfulRequests,
			WsPerformanceCounters.TotalAddNewTelUriContactToGroupRequests,
			WsPerformanceCounters.AddNewTelUriContactToGroupAverageResponseTime,
			WsPerformanceCounters.TotalAddNewTelUriContactToGroupSuccessfulRequests,
			WsPerformanceCounters.TotalAddDistributionGroupToImListRequests,
			WsPerformanceCounters.AddDistributionGroupToImListAverageResponseTime,
			WsPerformanceCounters.TotalAddDistributionGroupToImListSuccessfulRequests,
			WsPerformanceCounters.TotalAddImGroupRequests,
			WsPerformanceCounters.AddImGroupAverageResponseTime,
			WsPerformanceCounters.TotalAddImGroupSuccessfulRequests,
			WsPerformanceCounters.TotalGetImItemListRequests,
			WsPerformanceCounters.GetImItemListAverageResponseTime,
			WsPerformanceCounters.TotalGetImItemListSuccessfulRequests,
			WsPerformanceCounters.TotalGetImItemsRequests,
			WsPerformanceCounters.GetImItemsAverageResponseTime,
			WsPerformanceCounters.TotalGetImItemsSuccessfulRequests,
			WsPerformanceCounters.TotalRemoveContactFromImListRequests,
			WsPerformanceCounters.RemoveContactFromImListAverageResponseTime,
			WsPerformanceCounters.TotalRemoveContactFromImListSuccessfulRequests,
			WsPerformanceCounters.TotalRemoveDistributionGroupFromImListRequests,
			WsPerformanceCounters.RemoveDistributionGroupFromImListAverageResponseTime,
			WsPerformanceCounters.TotalRemoveDistributionGroupFromImListSuccessfulRequests,
			WsPerformanceCounters.TotalRemoveImGroupRequests,
			WsPerformanceCounters.RemoveImGroupAverageResponseTime,
			WsPerformanceCounters.TotalRemoveImGroupSuccessfulRequests,
			WsPerformanceCounters.TotalSetImGroupRequests,
			WsPerformanceCounters.SetImGroupAverageResponseTime,
			WsPerformanceCounters.TotalSetImGroupSuccessfulRequests,
			WsPerformanceCounters.TotalSetImListMigrationCompletedRequests,
			WsPerformanceCounters.SetImListMigrationCompletedAverageResponseTime,
			WsPerformanceCounters.TotalSetImListMigrationCompletedSuccessfulRequests,
			WsPerformanceCounters.TotalGetConversationItemsRequests,
			WsPerformanceCounters.GetConversationItemsAverageResponseTime,
			WsPerformanceCounters.TotalGetModernConversationItemsRequests,
			WsPerformanceCounters.GetModernConversationItemsAverageResponseTime,
			WsPerformanceCounters.TotalGetThreadedConversationItemsRequests,
			WsPerformanceCounters.GetThreadedConversationItemsAverageResponseTime,
			WsPerformanceCounters.TotalGetModernConversationAttachmentsRequests,
			WsPerformanceCounters.GetModernConversationAttachmentsAverageResponseTime,
			WsPerformanceCounters.TotalSetModernGroupMembershipRequests,
			WsPerformanceCounters.SetModernGroupMembershipAverageResponseTime,
			WsPerformanceCounters.TotalSendItemRequests,
			WsPerformanceCounters.SendItemAverageResponseTime,
			WsPerformanceCounters.TotalArchiveItemRequests,
			WsPerformanceCounters.ArchiveItemAverageResponseTime,
			WsPerformanceCounters.TotalMoveItemRequests,
			WsPerformanceCounters.MoveItemAverageResponseTime,
			WsPerformanceCounters.TotalCopyItemRequests,
			WsPerformanceCounters.CopyItemAverageResponseTime,
			WsPerformanceCounters.TotalGetFolderRequests,
			WsPerformanceCounters.GetFolderAverageResponseTime,
			WsPerformanceCounters.TotalCreateFolderRequests,
			WsPerformanceCounters.CreateFolderAverageResponseTime,
			WsPerformanceCounters.TotalCreateFolderPathRequests,
			WsPerformanceCounters.CreateFolderPathAverageResponseTime,
			WsPerformanceCounters.TotalCreateManagedFolderRequests,
			WsPerformanceCounters.CreateManagedFolderAverageResponseTime,
			WsPerformanceCounters.TotalDeleteFolderRequests,
			WsPerformanceCounters.DeleteFolderAverageResponseTime,
			WsPerformanceCounters.TotalEmptyFolderRequests,
			WsPerformanceCounters.EmptyFolderAverageResponseTime,
			WsPerformanceCounters.TotalUpdateFolderRequests,
			WsPerformanceCounters.UpdateFolderAverageResponseTime,
			WsPerformanceCounters.TotalMoveFolderRequests,
			WsPerformanceCounters.MoveFolderAverageResponseTime,
			WsPerformanceCounters.TotalCopyFolderRequests,
			WsPerformanceCounters.CopyFolderAverageResponseTime,
			WsPerformanceCounters.TotalFindItemRequests,
			WsPerformanceCounters.FindItemAverageResponseTime,
			WsPerformanceCounters.TotalFindFolderRequests,
			WsPerformanceCounters.FindFolderAverageResponseTime,
			WsPerformanceCounters.TotalResolveNamesRequests,
			WsPerformanceCounters.ResolveNamesAverageResponseTime,
			WsPerformanceCounters.TotalExpandDLRequests,
			WsPerformanceCounters.ExpandDLAverageResponseTime,
			WsPerformanceCounters.TotalGetPasswordExpirationDateRequests,
			WsPerformanceCounters.GetPasswordExpirationDateAverageResponseTime,
			WsPerformanceCounters.TotalGetPasswordExpirationDateSuccessfulRequests,
			WsPerformanceCounters.TotalCreateAttachmentRequests,
			WsPerformanceCounters.CreateAttachmentAverageResponseTime,
			WsPerformanceCounters.TotalDeleteAttachmentRequests,
			WsPerformanceCounters.DeleteAttachmentAverageResponseTime,
			WsPerformanceCounters.TotalGetAttachmentRequests,
			WsPerformanceCounters.GetAttachmentAverageResponseTime,
			WsPerformanceCounters.TotalGetClientAccessTokenRequests,
			WsPerformanceCounters.GetClientAccessTokenAverageResponseTime,
			WsPerformanceCounters.TotalSubscribeRequests,
			WsPerformanceCounters.SubscribeAverageResponseTime,
			WsPerformanceCounters.TotalUnsubscribeRequests,
			WsPerformanceCounters.UnsubscribeAverageResponseTime,
			WsPerformanceCounters.TotalGetStreamingEventsRequests,
			WsPerformanceCounters.GetStreamingEventsAverageResponseTime,
			WsPerformanceCounters.TotalGetEventsRequests,
			WsPerformanceCounters.GetEventsAverageResponseTime,
			WsPerformanceCounters.TotalGetServiceConfigurationRequests,
			WsPerformanceCounters.GetServiceConfigurationAverageResponseTime,
			WsPerformanceCounters.TotalGetMailTipsRequests,
			WsPerformanceCounters.GetMailTipsAverageResponseTime,
			WsPerformanceCounters.TotalSyncFolderHierarchyRequests,
			WsPerformanceCounters.SyncFolderHierarchyAverageResponseTime,
			WsPerformanceCounters.TotalSyncFolderItemsRequests,
			WsPerformanceCounters.SyncFolderItemsAverageResponseTime,
			WsPerformanceCounters.TotalGetDelegateRequests,
			WsPerformanceCounters.GetDelegateAverageResponseTime,
			WsPerformanceCounters.TotalAddDelegateRequests,
			WsPerformanceCounters.AddDelegateAverageResponseTime,
			WsPerformanceCounters.TotalRemoveDelegateRequests,
			WsPerformanceCounters.RemoveDelegateAverageResponseTime,
			WsPerformanceCounters.TotalUpdateDelegateRequests,
			WsPerformanceCounters.UpdateDelegateAverageResponseTime,
			WsPerformanceCounters.TotalCreateUserConfigurationRequests,
			WsPerformanceCounters.CreateUserConfigurationAverageResponseTime,
			WsPerformanceCounters.TotalGetUserConfigurationRequests,
			WsPerformanceCounters.GetUserConfigurationAverageResponseTime,
			WsPerformanceCounters.TotalUpdateUserConfigurationRequests,
			WsPerformanceCounters.UpdateUserConfigurationAverageResponseTime,
			WsPerformanceCounters.TotalDeleteUserConfigurationRequests,
			WsPerformanceCounters.DeleteUserConfigurationAverageResponseTime,
			WsPerformanceCounters.TotalGetUserAvailabilityRequests,
			WsPerformanceCounters.GetUserAvailabilityAverageResponseTime,
			WsPerformanceCounters.TotalGetUserOofSettingsRequests,
			WsPerformanceCounters.GetUserOofSettingsAverageResponseTime,
			WsPerformanceCounters.TotalSetUserOofSettingsRequests,
			WsPerformanceCounters.SetUserOofSettingsAverageResponseTime,
			WsPerformanceCounters.TotalGetSharingMetadataRequests,
			WsPerformanceCounters.GetSharingMetadataAverageResponseTime,
			WsPerformanceCounters.TotalRefreshSharingFolderRequests,
			WsPerformanceCounters.RefreshSharingFolderAverageResponseTime,
			WsPerformanceCounters.TotalGetSharingFolderRequests,
			WsPerformanceCounters.GetSharingFolderAverageResponseTime,
			WsPerformanceCounters.TotalSetTeamMailboxRequests,
			WsPerformanceCounters.SetTeamMailboxAverageResponseTime,
			WsPerformanceCounters.TotalSetTeamMailboxSuccessfulRequests,
			WsPerformanceCounters.TotalUnpinTeamMailboxRequests,
			WsPerformanceCounters.UnpinTeamMailboxAverageResponseTime,
			WsPerformanceCounters.TotalUnpinTeamMailboxSuccessfulRequests,
			WsPerformanceCounters.TotalGetRoomListsRequests,
			WsPerformanceCounters.GetRoomListsAverageResponseTime,
			WsPerformanceCounters.SubscribeToPushNotificationRequests,
			WsPerformanceCounters.SubscribeToPushNotificationAverageResponseTime,
			WsPerformanceCounters.RequestDeviceRegistrationChallengeRequest,
			WsPerformanceCounters.RequestDeviceRegistrationChallengeAverageResponseTime,
			WsPerformanceCounters.DeviceRegistrationChallengeRequestSuccessfulRequests,
			WsPerformanceCounters.UnsubscribeToPushNotificationRequests,
			WsPerformanceCounters.UnsubscribeToPushNotificationAverageResponseTime,
			WsPerformanceCounters.TotalGetRoomsRequests,
			WsPerformanceCounters.GetRoomsAverageResponseTime,
			WsPerformanceCounters.TotalPerformReminderActionRequests,
			WsPerformanceCounters.PerformReminderActionAverageResponseTime,
			WsPerformanceCounters.TotalGetRemindersRequests,
			WsPerformanceCounters.GetRemindersAverageResponseTime,
			WsPerformanceCounters.TotalProvisionRequests,
			WsPerformanceCounters.ProvisionAverageResponseTime,
			WsPerformanceCounters.TotalLogPushNotificationDataRequests,
			WsPerformanceCounters.LogPushNotificationDataAverageResponseTime,
			WsPerformanceCounters.TotalDeprovisionRequests,
			WsPerformanceCounters.DeprovisionAverageResponseTime,
			WsPerformanceCounters.TotalFindConversationRequests,
			WsPerformanceCounters.FindConversationAverageResponseTime,
			WsPerformanceCounters.TotalFindPeopleRequests,
			WsPerformanceCounters.FindPeopleAverageResponseTime,
			WsPerformanceCounters.TotalSyncPeopleRequests,
			WsPerformanceCounters.SyncPeopleAverageResponseTime,
			WsPerformanceCounters.TotalSyncAutoCompleteRecipientsRequests,
			WsPerformanceCounters.SyncAutoCompleteRecipientsAverageResponseTime,
			WsPerformanceCounters.TotalGetPersonaRequests,
			WsPerformanceCounters.GetPersonaAverageResponseTime,
			WsPerformanceCounters.TotalSyncConversationRequests,
			WsPerformanceCounters.SyncConversationAverageResponseTime,
			WsPerformanceCounters.TotalGetTimeZoneOffsetsRequests,
			WsPerformanceCounters.GetTimeZoneOffsetsAverageResponseTime,
			WsPerformanceCounters.TotalTimeZoneOffsetsTablesBuilt,
			WsPerformanceCounters.TotalFindMailboxStatisticsByKeywordsRequests,
			WsPerformanceCounters.FindMailboxStatisticsByKeywordsAverageResponseTime,
			WsPerformanceCounters.TotalGetSearchableMailboxesRequests,
			WsPerformanceCounters.GetSearchableMailboxesAverageResponseTime,
			WsPerformanceCounters.TotalSearchMailboxesRequests,
			WsPerformanceCounters.SearchMailboxesAverageResponseTime,
			WsPerformanceCounters.TotalGetDiscoverySearchConfigurationRequests,
			WsPerformanceCounters.GetDiscoverySearchConfigurationAverageResponseTime,
			WsPerformanceCounters.TotalGetHoldOnMailboxesRequests,
			WsPerformanceCounters.GetHoldOnMailboxesAverageResponseTime,
			WsPerformanceCounters.TotalSetHoldOnMailboxesRequests,
			WsPerformanceCounters.SetHoldOnMailboxesAverageResponseTime,
			WsPerformanceCounters.TotalGetNonIndexableItemStatisticsRequests,
			WsPerformanceCounters.GetNonIndexableItemStatisticsAverageResponseTime,
			WsPerformanceCounters.TotalGetNonIndexableItemDetailsRequests,
			WsPerformanceCounters.GetNonIndexableItemDetailsAverageResponseTime,
			WsPerformanceCounters.TotalGetUserRetentionPolicyTagsRequests,
			WsPerformanceCounters.GetUserRetentionPolicyTagsAverageResponseTime,
			WsPerformanceCounters.TotalPlayOnPhoneRequests,
			WsPerformanceCounters.PlayOnPhoneAverageResponseTime,
			WsPerformanceCounters.TotalGetPhoneCallInformationRequests,
			WsPerformanceCounters.GetPhoneCallInformationAverageResponseTime,
			WsPerformanceCounters.TotalDisconnectPhoneCallRequests,
			WsPerformanceCounters.DisconnectPhoneCallAverageResponseTime,
			WsPerformanceCounters.TotalCreateUMPromptRequests,
			WsPerformanceCounters.CreateUMPromptAverageResponseTime,
			WsPerformanceCounters.TotalGetUMPromptRequests,
			WsPerformanceCounters.GetUMPromptAverageResponseTime,
			WsPerformanceCounters.TotalGetUMPromptNamesRequests,
			WsPerformanceCounters.GetUMPromptNamesAverageResponseTime,
			WsPerformanceCounters.TotalDeleteUMPromptsRequests,
			WsPerformanceCounters.DeleteUMPromptsAverageResponseTime,
			WsPerformanceCounters.TotalGetServerTimeZonesRequests,
			WsPerformanceCounters.GetServerTimeZonesAverageResponseTime,
			WsPerformanceCounters.TotalSendNotificationRequests,
			WsPerformanceCounters.SendNotificationAverageResponseTime,
			WsPerformanceCounters.TotalFindMessageTrackingReportRequests,
			WsPerformanceCounters.FindMessageTrackingReportAverageResponseTime,
			WsPerformanceCounters.TotalGetMessageTrackingReportRequests,
			WsPerformanceCounters.GetMessageTrackingReportAverageResponseTime,
			WsPerformanceCounters.TotalApplyConversationActionRequests,
			WsPerformanceCounters.ApplyConversationActionAverageResponseTime,
			WsPerformanceCounters.TotalExecuteDiagnosticMethodRequests,
			WsPerformanceCounters.ExecuteDiagnosticMethodAverageResponseTime,
			WsPerformanceCounters.TotalGetInboxRulesRequests,
			WsPerformanceCounters.GetInboxRulesAverageResponseTime,
			WsPerformanceCounters.TotalGetInboxRulesSuccessfulRequests,
			WsPerformanceCounters.TotalUpdateInboxRulesRequests,
			WsPerformanceCounters.UpdateInboxRulesAverageResponseTime,
			WsPerformanceCounters.TotalUpdateInboxRulesSuccessfulRequests,
			WsPerformanceCounters.TotalIsUMEnabledRequests,
			WsPerformanceCounters.IsUMEnabledAverageResponseTime,
			WsPerformanceCounters.TotalIsUMEnabledSuccessfulRequests,
			WsPerformanceCounters.TotalGetUMPropertiesRequests,
			WsPerformanceCounters.GetUMPropertiesAverageResponseTime,
			WsPerformanceCounters.TotalGetUMPropertiesSuccessfulRequests,
			WsPerformanceCounters.TotalSetOofStatusRequests,
			WsPerformanceCounters.SetOofStatusAverageResponseTime,
			WsPerformanceCounters.TotalSetOofStatusSuccessfulRequests,
			WsPerformanceCounters.TotalSetPlayOnPhoneDialStringRequests,
			WsPerformanceCounters.SetPlayOnPhoneDialStringAverageResponseTime,
			WsPerformanceCounters.TotalSetPlayOnPhoneDialStringSuccessfulRequests,
			WsPerformanceCounters.TotalSetTelephoneAccessFolderEmailRequests,
			WsPerformanceCounters.SetTelephoneAccessFolderEmailAverageResponseTime,
			WsPerformanceCounters.TotalSetTelephoneAccessFolderEmailSuccessfulRequests,
			WsPerformanceCounters.TotalSetMissedCallNotificationEnabledRequests,
			WsPerformanceCounters.SetMissedCallNotificationEnabledAverageResponseTime,
			WsPerformanceCounters.TotalSetMissedCallNotificationEnabledSuccessfulRequests,
			WsPerformanceCounters.TotalResetPINRequests,
			WsPerformanceCounters.ResetPINAverageResponseTime,
			WsPerformanceCounters.TotalResetPINSuccessfulRequests,
			WsPerformanceCounters.TotalGetCallInfoRequests,
			WsPerformanceCounters.GetCallInfoAverageResponseTime,
			WsPerformanceCounters.TotalGetCallInfoSuccessfulRequests,
			WsPerformanceCounters.TotalDisconnectRequests,
			WsPerformanceCounters.DisconnectAverageResponseTime,
			WsPerformanceCounters.TotalDisconnectSuccessfulRequests,
			WsPerformanceCounters.TotalPlayOnPhoneGreetingRequests,
			WsPerformanceCounters.PlayOnPhoneGreetingAverageResponseTime,
			WsPerformanceCounters.TotalPlayOnPhoneGreetingSuccessfulRequests,
			WsPerformanceCounters.TotalStreamedEvents,
			WsPerformanceCounters.TotalRequests,
			WsPerformanceCounters.AverageResponseTime,
			WsPerformanceCounters.TotalItemsCreated,
			WsPerformanceCounters.TotalItemsDeleted,
			WsPerformanceCounters.TotalItemsSent,
			WsPerformanceCounters.TotalItemsRead,
			WsPerformanceCounters.TotalItemsUpdated,
			WsPerformanceCounters.TotalItemsCopied,
			WsPerformanceCounters.TotalItemsMoved,
			WsPerformanceCounters.TotalFoldersCreated,
			WsPerformanceCounters.TotalFoldersDeleted,
			WsPerformanceCounters.TotalFoldersRead,
			WsPerformanceCounters.TotalFoldersUpdated,
			WsPerformanceCounters.TotalFoldersCopied,
			WsPerformanceCounters.TotalFoldersMoved,
			WsPerformanceCounters.TotalFoldersSynced,
			WsPerformanceCounters.TotalItemsSynced,
			WsPerformanceCounters.TotalPushNotificationSuccesses,
			WsPerformanceCounters.TotalPushNotificationFailures,
			WsPerformanceCounters.ActiveStreamingConnections,
			WsPerformanceCounters.ActiveSubscriptions,
			WsPerformanceCounters.TotalFailedSubscriptions,
			WsPerformanceCounters.TotalClientDisconnects,
			WsPerformanceCounters.PID,
			WsPerformanceCounters.TotalCompletedRequests,
			WsPerformanceCounters.TotalRequestRejections,
			WsPerformanceCounters.CurrentProxyCalls,
			WsPerformanceCounters.TotalProxyRequests,
			WsPerformanceCounters.TotalProxyRequestBytes,
			WsPerformanceCounters.TotalProxyResponseBytes,
			WsPerformanceCounters.TotalProxyFailovers,
			WsPerformanceCounters.ProxyAverageResponseTime,
			WsPerformanceCounters.TotalGetUserPhotoRequests,
			WsPerformanceCounters.GetUserPhotoAverageResponseTime,
			WsPerformanceCounters.TotalAddAggregatedAccountSuccessfulRequests,
			WsPerformanceCounters.TotalIsOffice365DomainSuccessfulRequests,
			WsPerformanceCounters.TotalGetAggregatedAccountSuccessfulRequests,
			WsPerformanceCounters.TotalRemoveAggregatedAccountSuccessfulRequests,
			WsPerformanceCounters.TotalSetAggregatedAccountSuccessfulRequests,
			WsPerformanceCounters.TotalCopyFolderSuccessfulRequests,
			WsPerformanceCounters.TotalArchiveItemItemSuccessfulRequests,
			WsPerformanceCounters.TotalCopyItemSuccessfulRequests,
			WsPerformanceCounters.TotalCreateFolderSuccessfulRequests,
			WsPerformanceCounters.TotalCreateFolderPathSuccessfulRequests,
			WsPerformanceCounters.TotalCreateItemSuccessfulRequests,
			WsPerformanceCounters.TotalPostModernGroupItemSuccessfulRequests,
			WsPerformanceCounters.TotalUpdateAndPostModernGroupItemSuccessfulRequests,
			WsPerformanceCounters.TotalCreateResponseFromModernGroupSuccessfulRequests,
			WsPerformanceCounters.TotalCreateManagedFolderSuccessfulRequests,
			WsPerformanceCounters.TotalDeleteFolderSuccessfulRequests,
			WsPerformanceCounters.TotalDeleteItemSuccessfulRequests,
			WsPerformanceCounters.TotalExpandDLSuccessfulRequests,
			WsPerformanceCounters.TotalFindFolderSuccessfulRequests,
			WsPerformanceCounters.TotalFindItemSuccessfulRequests,
			WsPerformanceCounters.TotalFindConversationSuccessfulRequests,
			WsPerformanceCounters.TotalFindPeopleSuccessfulRequests,
			WsPerformanceCounters.TotalSyncPeopleSuccessfulRequests,
			WsPerformanceCounters.TotalSyncAutoCompleteRecipientsSuccessfulRequests,
			WsPerformanceCounters.TotalGetPersonaSuccessfulRequests,
			WsPerformanceCounters.TotalSyncConversationSuccessfulRequests,
			WsPerformanceCounters.TotalGetTimeZoneOffsetsSuccessfulRequests,
			WsPerformanceCounters.TotalGetEventsSuccessfulRequests,
			WsPerformanceCounters.TotalGetStreamingEventsSuccessfulRequests,
			WsPerformanceCounters.TotalGetFolderSuccessfulRequests,
			WsPerformanceCounters.TotalGetMailTipsSuccessfulRequests,
			WsPerformanceCounters.TotalPlayOnPhoneSuccessfulRequests,
			WsPerformanceCounters.TotalGetPhoneCallInformationSuccessfulRequests,
			WsPerformanceCounters.TotalDisconnectPhoneCallSuccessfulRequests,
			WsPerformanceCounters.TotalCreateUMPromptSuccessfulRequests,
			WsPerformanceCounters.TotalGetUMPromptSuccessfulRequests,
			WsPerformanceCounters.TotalGetUMPromptNamesSuccessfulRequests,
			WsPerformanceCounters.TotalDeleteUMPromptsSuccessfulRequests,
			WsPerformanceCounters.TotalGetServiceConfigurationSuccessfulRequests,
			WsPerformanceCounters.TotalGetItemSuccessfulRequests,
			WsPerformanceCounters.TotalGetServerTimeZonesSuccessfulRequests,
			WsPerformanceCounters.TotalMoveFolderSuccessfulRequests,
			WsPerformanceCounters.TotalMoveItemSuccessfulRequests,
			WsPerformanceCounters.TotalResolveNamesSuccessfulRequests,
			WsPerformanceCounters.TotalSendItemSuccessfulRequests,
			WsPerformanceCounters.TotalSubscribeSuccessfulRequests,
			WsPerformanceCounters.TotalUnsubscribeSuccessfulRequests,
			WsPerformanceCounters.TotalUpdateFolderSuccessfulRequests,
			WsPerformanceCounters.TotalUpdateItemSuccessfulRequests,
			WsPerformanceCounters.TotalUpdateItemInRecoverableItemsSuccessfulRequests,
			WsPerformanceCounters.TotalCreateAttachmentSuccessfulRequests,
			WsPerformanceCounters.TotalDeleteAttachmentSuccessfulRequests,
			WsPerformanceCounters.TotalGetAttachmentSuccessfulRequests,
			WsPerformanceCounters.TotalGetClientAccessTokenSuccessfulRequests,
			WsPerformanceCounters.TotalSendNotificationSuccessfulRequests,
			WsPerformanceCounters.TotalSyncFolderItemsSuccessfulRequests,
			WsPerformanceCounters.TotalSyncFolderHierarchySuccessfulRequests,
			WsPerformanceCounters.TotalConvertIdSuccessfulRequests,
			WsPerformanceCounters.TotalGetDelegateSuccessfulRequests,
			WsPerformanceCounters.TotalAddDelegateSuccessfulRequests,
			WsPerformanceCounters.TotalRemoveDelegateSuccessfulRequests,
			WsPerformanceCounters.TotalUpdateDelegateSuccessfulRequests,
			WsPerformanceCounters.TotalCreateUserConfigurationSuccessfulRequests,
			WsPerformanceCounters.TotalDeleteUserConfigurationSuccessfulRequests,
			WsPerformanceCounters.TotalGetUserConfigurationSuccessfulRequests,
			WsPerformanceCounters.TotalUpdateUserConfigurationSuccessfulRequests,
			WsPerformanceCounters.TotalGetUserAvailabilitySuccessfulRequests,
			WsPerformanceCounters.TotalGetUserOofSettingsSuccessfulRequests,
			WsPerformanceCounters.TotalSetUserOofSettingsSuccessfulRequests,
			WsPerformanceCounters.TotalGetSharingMetadataSuccessfulRequests,
			WsPerformanceCounters.TotalRefreshSharingFolderSuccessfulRequests,
			WsPerformanceCounters.TotalGetSharingFolderSuccessfulRequests,
			WsPerformanceCounters.TotalGetRoomListsSuccessfulRequests,
			WsPerformanceCounters.TotalGetRoomsSuccessfulRequests,
			WsPerformanceCounters.TotalPerformReminderActionSuccessfulRequests,
			WsPerformanceCounters.TotalGetRemindersSuccessfulRequests,
			WsPerformanceCounters.TotalProvisionSuccessfulRequests,
			WsPerformanceCounters.TotalDeprovisionSuccessfulRequests,
			WsPerformanceCounters.TotalLogPushNotificationDataSuccessfulRequests,
			WsPerformanceCounters.TotalFindMessageTrackingReportSuccessfulRequests,
			WsPerformanceCounters.TotalGetMessageTrackingReportSuccessfulRequests,
			WsPerformanceCounters.TotalApplyConversationActionSuccessfulRequests,
			WsPerformanceCounters.TotalEmptyFolderSuccessfulRequests,
			WsPerformanceCounters.TotalUploadItemsSuccessfulRequests,
			WsPerformanceCounters.TotalExportItemsSuccessfulRequests,
			WsPerformanceCounters.TotalExecuteDiagnosticMethodSuccessfulRequests,
			WsPerformanceCounters.TotalFindMailboxStatisticsByKeywordsSuccessfulRequests,
			WsPerformanceCounters.TotalGetSearchableMailboxesSuccessfulRequests,
			WsPerformanceCounters.TotalSearchMailboxesSuccessfulRequests,
			WsPerformanceCounters.TotalGetDiscoverySearchConfigurationSuccessfulRequests,
			WsPerformanceCounters.TotalGetHoldOnMailboxesSuccessfulRequests,
			WsPerformanceCounters.TotalSetHoldOnMailboxesSuccessfulRequests,
			WsPerformanceCounters.TotalGetNonIndexableItemStatisticsSuccessfulRequests,
			WsPerformanceCounters.TotalGetNonIndexableItemDetailsSuccessfulRequests,
			WsPerformanceCounters.TotalMarkAllItemsAsReadSuccessfulRequests,
			WsPerformanceCounters.TotalGetClientExtensionSuccessfulRequests,
			WsPerformanceCounters.TotalGetEncryptionConfigurationSuccessfulRequests,
			WsPerformanceCounters.TotalSetClientExtensionSuccessfulRequests,
			WsPerformanceCounters.TotalSetEncryptionConfigurationSuccessfulRequests,
			WsPerformanceCounters.TotalSubscribeToPushNotificationSuccessfulRequests,
			WsPerformanceCounters.TotalUnsubscribeToPushNotificationSuccessfulRequests,
			WsPerformanceCounters.TotalCreateUnifiedMailboxSuccessfulRequests,
			WsPerformanceCounters.TotalGetAppManifestsSuccessfulRequests,
			WsPerformanceCounters.TotalInstallAppSuccessfulRequests,
			WsPerformanceCounters.TotalUninstallAppSuccessfulRequests,
			WsPerformanceCounters.TotalDisableAppSuccessfulRequests,
			WsPerformanceCounters.TotalGetAppMarketplaceUrlSuccessfulRequests,
			WsPerformanceCounters.TotalGetClientExtensionTokenSuccessfulRequests,
			WsPerformanceCounters.TotalGetEncryptionConfigurationTokenSuccessfulRequests,
			WsPerformanceCounters.TotalGetConversationItemsSuccessfulRequests,
			WsPerformanceCounters.TotalGetModernConversationItemsSuccessfulRequests,
			WsPerformanceCounters.TotalGetThreadedConversationItemsSuccessfulRequests,
			WsPerformanceCounters.TotalGetModernConversationAttachmentsSuccessfulRequests,
			WsPerformanceCounters.TotalSetModernGroupMembershipSuccessfulRequests,
			WsPerformanceCounters.TotalGetUserRetentionPolicyTagsSuccessfulRequests,
			WsPerformanceCounters.TotalGetUserPhotoSuccessfulRequests,
			WsPerformanceCounters.TotalStartFindInGALSpeechRecognitionRequests,
			WsPerformanceCounters.StartFindInGALSpeechRecognitionAverageResponseTime,
			WsPerformanceCounters.TotalStartFindInGALSpeechRecognitionSuccessfulRequests,
			WsPerformanceCounters.TotalCompleteFindInGALSpeechRecognitionRequests,
			WsPerformanceCounters.CompleteFindInGALSpeechRecognitionAverageResponseTime,
			WsPerformanceCounters.TotalCompleteFindInGALSpeechRecognitionSuccessfulRequests,
			WsPerformanceCounters.TotalCreateUMCallDataRecordRequests,
			WsPerformanceCounters.CreateUMCallDataRecordAverageResponseTime,
			WsPerformanceCounters.TotalCreateUMCallDataRecordSuccessfulRequests,
			WsPerformanceCounters.TotalGetUMCallDataRecordsRequests,
			WsPerformanceCounters.GetUMCallDataRecordsAverageResponseTime,
			WsPerformanceCounters.TotalGetUMCallDataRecordsSuccessfulRequests,
			WsPerformanceCounters.TotalGetUMCallSummaryRequests,
			WsPerformanceCounters.GetUMCallSummaryAverageResponseTime,
			WsPerformanceCounters.TotalGetUMCallSummarySuccessfulRequests,
			WsPerformanceCounters.TotalGetUserPhotoDataRequests,
			WsPerformanceCounters.GetUserPhotoDataAverageResponseTime,
			WsPerformanceCounters.TotalGetUserPhotoDataSuccessfulRequests,
			WsPerformanceCounters.TotalInitUMMailboxRequests,
			WsPerformanceCounters.InitUMMailboxAverageResponseTime,
			WsPerformanceCounters.TotalInitUMMailboxSuccessfulRequests,
			WsPerformanceCounters.TotalResetUMMailboxRequests,
			WsPerformanceCounters.ResetUMMailboxAverageResponseTime,
			WsPerformanceCounters.TotalResetUMMailboxSuccessfulRequests,
			WsPerformanceCounters.TotalValidateUMPinRequests,
			WsPerformanceCounters.ValidateUMPinAverageResponseTime,
			WsPerformanceCounters.TotalValidateUMPinSuccessfulRequests,
			WsPerformanceCounters.TotalSaveUMPinRequests,
			WsPerformanceCounters.SaveUMPinAverageResponseTime,
			WsPerformanceCounters.TotalSaveUMPinSuccessfulRequests,
			WsPerformanceCounters.TotalGetUMPinRequests,
			WsPerformanceCounters.GetUMPinAverageResponseTime,
			WsPerformanceCounters.TotalGetUMPinSuccessfulRequests,
			WsPerformanceCounters.TotalGetClientIntentRequests,
			WsPerformanceCounters.GetClientIntentAverageResponseTime,
			WsPerformanceCounters.TotalGetClientIntentSuccessfulRequests,
			WsPerformanceCounters.TotalGetUMSubscriberCallAnsweringDataRequests,
			WsPerformanceCounters.GetUMSubscriberCallAnsweringDataAverageResponseTime,
			WsPerformanceCounters.TotalGetUMSubscriberCallAnsweringDataSuccessfulRequests,
			WsPerformanceCounters.TotalUpdateMailboxAssociationRequests,
			WsPerformanceCounters.UpdateMailboxAssociationAverageResponseTime,
			WsPerformanceCounters.TotalUpdateMailboxAssociationSuccessfulRequests,
			WsPerformanceCounters.TotalUpdateGroupMailboxRequests,
			WsPerformanceCounters.UpdateGroupMailboxAverageResponseTime,
			WsPerformanceCounters.TotalUpdateGroupMailboxSuccessfulRequests,
			WsPerformanceCounters.TotalGetCalendarEventRequests,
			WsPerformanceCounters.GetCalendarEventAverageResponseTime,
			WsPerformanceCounters.TotalGetCalendarEventSuccessfulRequests,
			WsPerformanceCounters.TotalGetCalendarViewRequests,
			WsPerformanceCounters.GetCalendarViewAverageResponseTime,
			WsPerformanceCounters.TotalGetCalendarViewSuccessfulRequests,
			WsPerformanceCounters.TotalGetBirthdayCalendarViewRequests,
			WsPerformanceCounters.GetBirthdayCalendarViewAverageResponseTime,
			WsPerformanceCounters.TotalGetBirthdayCalendarViewSuccessfulRequests,
			WsPerformanceCounters.TotalCreateCalendarEventRequests,
			WsPerformanceCounters.CreateCalendarEventAverageResponseTime,
			WsPerformanceCounters.TotalCreateCalendarEventSuccessfulRequests,
			WsPerformanceCounters.TotalUpdateCalendarEventRequests,
			WsPerformanceCounters.UpdateCalendarEventAverageResponseTime,
			WsPerformanceCounters.TotalUpdateCalendarEventSuccessfulRequests,
			WsPerformanceCounters.TotalCancelCalendarEventRequests,
			WsPerformanceCounters.CancelCalendarEventAverageResponseTime,
			WsPerformanceCounters.TotalCancelCalendarEventSuccessfulRequests,
			WsPerformanceCounters.TotalRespondToCalendarEventRequests,
			WsPerformanceCounters.RespondToCalendarEventAverageResponseTime,
			WsPerformanceCounters.TotalRespondToCalendarEventSuccessfulRequests,
			WsPerformanceCounters.TotalRefreshGALContactsFolderRequests,
			WsPerformanceCounters.RefreshGALContactsFolderAverageResponseTime,
			WsPerformanceCounters.TotalRefreshGALContactsFolderSuccessfulRequests,
			WsPerformanceCounters.TotalSubscribeToConversationChangesRequests,
			WsPerformanceCounters.SubscribeToConversationChangesAverageResponseTime,
			WsPerformanceCounters.TotalSubscribeToConversationChangesSuccessfulRequests,
			WsPerformanceCounters.TotalSubscribeToHierarchyChangesRequests,
			WsPerformanceCounters.SubscribeToHierarchyChangesAverageResponseTime,
			WsPerformanceCounters.TotalSubscribeToHierarchyChangesSuccessfulRequests,
			WsPerformanceCounters.TotalSubscribeToMessageChangesRequests,
			WsPerformanceCounters.SubscribeToMessageChangesAverageResponseTime,
			WsPerformanceCounters.TotalSubscribeToMessageChangesSuccessfulRequests,
			WsPerformanceCounters.TotalDeleteCalendarEventRequests,
			WsPerformanceCounters.DeleteCalendarEventAverageResponseTime,
			WsPerformanceCounters.TotalDeleteCalendarEventSuccessfulRequests,
			WsPerformanceCounters.TotalForwardCalendarEventRequests,
			WsPerformanceCounters.ForwardCalendarEventAverageResponseTime,
			WsPerformanceCounters.TotalForwardCalendarEventSuccessfulRequests,
			WsPerformanceCounters.LikeItemRequests,
			WsPerformanceCounters.LikeItemAverageResponseTime,
			WsPerformanceCounters.LikeItemSuccessfulRequests,
			WsPerformanceCounters.GetLikersRequests,
			WsPerformanceCounters.GetLikersAverageResponseTime,
			WsPerformanceCounters.GetLikersSuccessfulRequests,
			WsPerformanceCounters.TotalExpandCalendarEventRequests,
			WsPerformanceCounters.ExpandCalendarEventAverageResponseTime,
			WsPerformanceCounters.TotalExpandCalendarEventSuccessfulRequests,
			WsPerformanceCounters.GetConversationItemsDiagnosticsRequests,
			WsPerformanceCounters.GetConversationItemsDiagnosticsAverageResponseTime,
			WsPerformanceCounters.GetConversationItemsDiagnosticsSuccessfulRequests,
			WsPerformanceCounters.TotalGetComplianceConfiguration,
			WsPerformanceCounters.GetComplianceConfigurationAverageResponseTime,
			WsPerformanceCounters.TotalGetComplianceConfigurationSuccessfulRequests,
			WsPerformanceCounters.TotalPerformInstantSearchRequests,
			WsPerformanceCounters.PerformInstantSearchAverageResponseTime,
			WsPerformanceCounters.PerformInstantSearchSuccessfulRequests,
			WsPerformanceCounters.TotalEndInstantSearchSessionRequests,
			WsPerformanceCounters.EndInstantSearchSessionAverageResponseTime,
			WsPerformanceCounters.EndInstantSearchSessionSuccessfulRequests,
			WsPerformanceCounters.GetUserUnifiedGroupsRequests,
			WsPerformanceCounters.GetUserUnifiedGroupsAverageResponseTime,
			WsPerformanceCounters.GetUserUnifiedGroupsSuccessfulRequests,
			WsPerformanceCounters.TotalGetPeopleICommunicateWithRequests,
			WsPerformanceCounters.GetPeopleICommunicateWithAverageResponseTime,
			WsPerformanceCounters.TotalGetPeopleICommunicateWithSuccessfulRequests,
			WsPerformanceCounters.MaskAutoCompleteRecipientRequests,
			WsPerformanceCounters.MaskAutoCompleteRecipientAverageResponseTime,
			WsPerformanceCounters.TotalMaskAutoCompleteRecipientSuccessfulRequests,
			WsPerformanceCounters.TotalGetClutterStateRequests,
			WsPerformanceCounters.GetClutterStateAverageResponseTime,
			WsPerformanceCounters.TotalGetClutterStateSuccessfulRequests,
			WsPerformanceCounters.TotalSetClutterStateRequests,
			WsPerformanceCounters.SetClutterStateAverageResponseTime,
			WsPerformanceCounters.TotalSetClutterStateSuccessfulRequests
		};
	}
}
