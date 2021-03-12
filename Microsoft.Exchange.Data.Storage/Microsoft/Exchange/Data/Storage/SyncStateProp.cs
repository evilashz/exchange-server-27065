using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E33 RID: 3635
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct SyncStateProp
	{
		// Token: 0x170021AA RID: 8618
		// (get) Token: 0x06007DC5 RID: 32197 RVA: 0x0022A33E File Offset: 0x0022853E
		public static string ClientState
		{
			get
			{
				return SyncStateProp.GetStaticString("ClientState_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
			}
		}

		// Token: 0x170021AB RID: 8619
		// (get) Token: 0x06007DC6 RID: 32198 RVA: 0x0022A34A File Offset: 0x0022854A
		public static string ColdDataKeys
		{
			get
			{
				return SyncStateProp.GetStaticString("ColdDataKeys_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
			}
		}

		// Token: 0x170021AC RID: 8620
		// (get) Token: 0x06007DC7 RID: 32199 RVA: 0x0022A356 File Offset: 0x00228556
		public static string CumulativeClientManifest
		{
			get
			{
				return SyncStateProp.GetStaticString("CumulativeClientManifest_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
			}
		}

		// Token: 0x170021AD RID: 8621
		// (get) Token: 0x06007DC8 RID: 32200 RVA: 0x0022A362 File Offset: 0x00228562
		public static string CurDelayedServerOperationQueue
		{
			get
			{
				return SyncStateProp.GetStaticString("CurDelayedServerOperationQueue_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
			}
		}

		// Token: 0x170021AE RID: 8622
		// (get) Token: 0x06007DC9 RID: 32201 RVA: 0x0022A36E File Offset: 0x0022856E
		public static string CurFilterId
		{
			get
			{
				return SyncStateProp.GetStaticString("CurFilterId_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
			}
		}

		// Token: 0x170021AF RID: 8623
		// (get) Token: 0x06007DCA RID: 32202 RVA: 0x0022A37A File Offset: 0x0022857A
		public static string CurMaxWatermark
		{
			get
			{
				return SyncStateProp.GetStaticString("CurMaxWatermark_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
			}
		}

		// Token: 0x170021B0 RID: 8624
		// (get) Token: 0x06007DCB RID: 32203 RVA: 0x0022A386 File Offset: 0x00228586
		public static string CurFTSMaxWatermark
		{
			get
			{
				return SyncStateProp.GetStaticString("CurFTSMaxWatermark_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
			}
		}

		// Token: 0x170021B1 RID: 8625
		// (get) Token: 0x06007DCC RID: 32204 RVA: 0x0022A392 File Offset: 0x00228592
		public static string CurServerManifest
		{
			get
			{
				return SyncStateProp.GetStaticString("CurServerManifest_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
			}
		}

		// Token: 0x170021B2 RID: 8626
		// (get) Token: 0x06007DCD RID: 32205 RVA: 0x0022A39E File Offset: 0x0022859E
		public static string CurSnapShotWatermark
		{
			get
			{
				return SyncStateProp.GetStaticString("CurSnapShotWatermark_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
			}
		}

		// Token: 0x170021B3 RID: 8627
		// (get) Token: 0x06007DCE RID: 32206 RVA: 0x0022A3AA File Offset: 0x002285AA
		public static string CustomVersion
		{
			get
			{
				return SyncStateProp.GetStaticString("CustomVersion_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
			}
		}

		// Token: 0x170021B4 RID: 8628
		// (get) Token: 0x06007DCF RID: 32207 RVA: 0x0022A3B6 File Offset: 0x002285B6
		public static string PrevDelayedServerOperationQueue
		{
			get
			{
				return SyncStateProp.GetStaticString("PrevDelayedServerOperationQueue_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
			}
		}

		// Token: 0x170021B5 RID: 8629
		// (get) Token: 0x06007DD0 RID: 32208 RVA: 0x0022A3C2 File Offset: 0x002285C2
		public static string PrevFilterId
		{
			get
			{
				return SyncStateProp.GetStaticString("PrevFilterId_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
			}
		}

		// Token: 0x170021B6 RID: 8630
		// (get) Token: 0x06007DD1 RID: 32209 RVA: 0x0022A3CE File Offset: 0x002285CE
		public static string PrevMaxWatermark
		{
			get
			{
				return SyncStateProp.GetStaticString("PrevMaxWatermark_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
			}
		}

		// Token: 0x170021B7 RID: 8631
		// (get) Token: 0x06007DD2 RID: 32210 RVA: 0x0022A3DA File Offset: 0x002285DA
		public static string PrevFTSMaxWatermark
		{
			get
			{
				return SyncStateProp.GetStaticString("PrevFTSMaxWatermark_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
			}
		}

		// Token: 0x170021B8 RID: 8632
		// (get) Token: 0x06007DD3 RID: 32211 RVA: 0x0022A3E6 File Offset: 0x002285E6
		public static string PrevServerManifest
		{
			get
			{
				return SyncStateProp.GetStaticString("PrevServerManifest_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
			}
		}

		// Token: 0x170021B9 RID: 8633
		// (get) Token: 0x06007DD4 RID: 32212 RVA: 0x0022A3F2 File Offset: 0x002285F2
		public static string PrevSnapShotWatermark
		{
			get
			{
				return SyncStateProp.GetStaticString("PrevSnapShotWatermark_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
			}
		}

		// Token: 0x170021BA RID: 8634
		// (get) Token: 0x06007DD5 RID: 32213 RVA: 0x0022A3FE File Offset: 0x002285FE
		public static string RecoveryClientManifest
		{
			get
			{
				return SyncStateProp.GetStaticString("RecoveryClientManifest_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
			}
		}

		// Token: 0x170021BB RID: 8635
		// (get) Token: 0x06007DD6 RID: 32214 RVA: 0x0022A40A File Offset: 0x0022860A
		public static string UsingQueryBasedFilter
		{
			get
			{
				return SyncStateProp.GetStaticString("UsingQueryBasedFilter_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
			}
		}

		// Token: 0x170021BC RID: 8636
		// (get) Token: 0x06007DD7 RID: 32215 RVA: 0x0022A416 File Offset: 0x00228616
		public static string Version
		{
			get
			{
				return SyncStateProp.GetStaticString("Version_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
			}
		}

		// Token: 0x170021BD RID: 8637
		// (get) Token: 0x06007DD8 RID: 32216 RVA: 0x0022A422 File Offset: 0x00228622
		public static string CurLastSyncConversationMode
		{
			get
			{
				return SyncStateProp.GetStaticString("CurLastSyncConversationMode_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
			}
		}

		// Token: 0x170021BE RID: 8638
		// (get) Token: 0x06007DD9 RID: 32217 RVA: 0x0022A42E File Offset: 0x0022862E
		public static string PrevLastSyncConversationMode
		{
			get
			{
				return SyncStateProp.GetStaticString("PrevLastSyncConversationMode_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
			}
		}

		// Token: 0x06007DDA RID: 32218 RVA: 0x0022A43A File Offset: 0x0022863A
		private static string GetStaticString(string key)
		{
			if (!SyncStateProp.hasBeenInitialized)
			{
				SyncStateProp.Initialize();
			}
			return key;
		}

		// Token: 0x06007DDB RID: 32219 RVA: 0x0022A44C File Offset: 0x0022864C
		private static void Initialize()
		{
			lock ("_C70D1604-09E7-489f-B18B-DB337B9A3F0F")
			{
				if (!SyncStateProp.hasBeenInitialized)
				{
					StaticStringPool.Instance.Intern("UsingQueryBasedFilter_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
					StaticStringPool.Instance.Intern("ColdDataKeys_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
					StaticStringPool.Instance.Intern("Version_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
					StaticStringPool.Instance.Intern("CustomVersion_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
					StaticStringPool.Instance.Intern("CurMaxWatermark_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
					StaticStringPool.Instance.Intern("PrevMaxWatermark_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
					StaticStringPool.Instance.Intern("ClientState_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
					StaticStringPool.Instance.Intern("CumulativeClientManifest_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
					StaticStringPool.Instance.Intern("RecoveryClientManifest_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
					StaticStringPool.Instance.Intern("CurServerManifest_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
					StaticStringPool.Instance.Intern("PrevServerManifest_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
					StaticStringPool.Instance.Intern("CurFilterId_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
					StaticStringPool.Instance.Intern("PrevFilterId_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
					StaticStringPool.Instance.Intern("CurSnapShotWatermark_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
					StaticStringPool.Instance.Intern("PrevSnapShotWatermark_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
					StaticStringPool.Instance.Intern("CurDelayedServerOperationQueue_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
					StaticStringPool.Instance.Intern("PrevDelayedServerOperationQueue_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
					StaticStringPool.Instance.Intern("CurLastSyncConversationMode_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
					StaticStringPool.Instance.Intern("PrevLastSyncConversationMode_C70D1604-09E7-489f-B18B-DB337B9A3F0F");
					SyncStateProp.hasBeenInitialized = true;
				}
			}
		}

		// Token: 0x040055B7 RID: 21943
		public const string PrevDelayedServerOperationQueueString = "PrevDelayedServerOperationQueue_C70D1604-09E7-489f-B18B-DB337B9A3F0F";

		// Token: 0x040055B8 RID: 21944
		public const string PrevMaxWatermarkString = "PrevMaxWatermark_C70D1604-09E7-489f-B18B-DB337B9A3F0F";

		// Token: 0x040055B9 RID: 21945
		public const string PrevFTSMaxWatermarkString = "PrevFTSMaxWatermark_C70D1604-09E7-489f-B18B-DB337B9A3F0F";

		// Token: 0x040055BA RID: 21946
		private const string ClientStateString = "ClientState_C70D1604-09E7-489f-B18B-DB337B9A3F0F";

		// Token: 0x040055BB RID: 21947
		private const string ColdDataKeysString = "ColdDataKeys_C70D1604-09E7-489f-B18B-DB337B9A3F0F";

		// Token: 0x040055BC RID: 21948
		private const string CumulativeClientManifestString = "CumulativeClientManifest_C70D1604-09E7-489f-B18B-DB337B9A3F0F";

		// Token: 0x040055BD RID: 21949
		private const string CurDelayedServerOperationQueueString = "CurDelayedServerOperationQueue_C70D1604-09E7-489f-B18B-DB337B9A3F0F";

		// Token: 0x040055BE RID: 21950
		private const string CurFilterIdString = "CurFilterId_C70D1604-09E7-489f-B18B-DB337B9A3F0F";

		// Token: 0x040055BF RID: 21951
		private const string CurMaxWatermarkString = "CurMaxWatermark_C70D1604-09E7-489f-B18B-DB337B9A3F0F";

		// Token: 0x040055C0 RID: 21952
		private const string CurFTSMaxWatermarkString = "CurFTSMaxWatermark_C70D1604-09E7-489f-B18B-DB337B9A3F0F";

		// Token: 0x040055C1 RID: 21953
		private const string CurServerManifestString = "CurServerManifest_C70D1604-09E7-489f-B18B-DB337B9A3F0F";

		// Token: 0x040055C2 RID: 21954
		private const string CurSnapShotWatermarkString = "CurSnapShotWatermark_C70D1604-09E7-489f-B18B-DB337B9A3F0F";

		// Token: 0x040055C3 RID: 21955
		private const string CustomVersionString = "CustomVersion_C70D1604-09E7-489f-B18B-DB337B9A3F0F";

		// Token: 0x040055C4 RID: 21956
		private const string PrevFilterIdString = "PrevFilterId_C70D1604-09E7-489f-B18B-DB337B9A3F0F";

		// Token: 0x040055C5 RID: 21957
		private const string PrevServerManifestString = "PrevServerManifest_C70D1604-09E7-489f-B18B-DB337B9A3F0F";

		// Token: 0x040055C6 RID: 21958
		private const string PrevSnapShotWatermarkString = "PrevSnapShotWatermark_C70D1604-09E7-489f-B18B-DB337B9A3F0F";

		// Token: 0x040055C7 RID: 21959
		private const string RecoveryClientManifestString = "RecoveryClientManifest_C70D1604-09E7-489f-B18B-DB337B9A3F0F";

		// Token: 0x040055C8 RID: 21960
		private const string UsingQueryBasedFilterString = "UsingQueryBasedFilter_C70D1604-09E7-489f-B18B-DB337B9A3F0F";

		// Token: 0x040055C9 RID: 21961
		private const string VersionString = "Version_C70D1604-09E7-489f-B18B-DB337B9A3F0F";

		// Token: 0x040055CA RID: 21962
		private const string CurLastSyncConversationModeString = "CurLastSyncConversationMode_C70D1604-09E7-489f-B18B-DB337B9A3F0F";

		// Token: 0x040055CB RID: 21963
		private const string PrevLastSyncConversationModeString = "PrevLastSyncConversationMode_C70D1604-09E7-489f-B18B-DB337B9A3F0F";

		// Token: 0x040055CC RID: 21964
		private const string UID = "_C70D1604-09E7-489f-B18B-DB337B9A3F0F";

		// Token: 0x040055CD RID: 21965
		private static bool hasBeenInitialized;
	}
}
