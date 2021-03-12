using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000211 RID: 529
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SyncChangeEntry : ISyncClientOperation
	{
		// Token: 0x06001229 RID: 4649 RVA: 0x0003B967 File Offset: 0x00039B67
		internal SyncChangeEntry(ChangeType changeType, SchemaType schemaType, string cloudId) : this(changeType, schemaType, cloudId, null, null)
		{
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x0003B974 File Offset: 0x00039B74
		internal SyncChangeEntry(ChangeType changeType, SchemaType schemaType, string cloudId, object cloudObject) : this(changeType, schemaType, cloudId, cloudObject, null)
		{
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0003B982 File Offset: 0x00039B82
		internal SyncChangeEntry(ChangeType changeType, SchemaType schemaType, string cloudId, ISyncObject syncObject) : this(changeType, schemaType, cloudId, null, syncObject)
		{
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x0003B990 File Offset: 0x00039B90
		internal SyncChangeEntry(SyncOperation syncOperation, SchemaType schemaType) : this(syncOperation.ChangeType, schemaType, (StoreObjectId)syncOperation.Id.NativeId)
		{
			this.syncOperation = syncOperation;
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x0003B9B6 File Offset: 0x00039BB6
		internal SyncChangeEntry(HierarchySyncOperation hierarchySyncOperation) : this(hierarchySyncOperation.ChangeType, SchemaType.Folder, hierarchySyncOperation.ItemId)
		{
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x0003B9CC File Offset: 0x00039BCC
		internal SyncChangeEntry(ChangeType changeType, SchemaType schemaType, StoreObjectId nativeId)
		{
			this.changeType = changeType;
			this.nativeId = nativeId;
			this.schemaType = schemaType;
			this.persist = true;
			this.suspectedSyncPoisonItem = new SyncPoisonItem(this.nativeId.ToBase64String(), SyncPoisonEntitySource.Labs, SyncChangeEntry.GetSyncPoisonEntityType(schemaType));
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x0003BA18 File Offset: 0x00039C18
		internal SyncChangeEntry(SyncChangeEntry change)
		{
			this.changeKey = change.changeKey;
			this.changeType = change.changeType;
			this.cloudFolderId = change.cloudFolderId;
			this.cloudId = change.cloudId;
			this.cloudObject = change.cloudObject;
			this.cloudVersion = change.cloudVersion;
			this.exception = change.exception;
			this.nativeFolderId = change.nativeFolderId;
			this.nativeId = change.nativeId;
			this.newCloudFolderId = change.newCloudFolderId;
			this.newCloudId = change.newCloudId;
			this.newNativeFolderId = change.newNativeFolderId;
			this.newNativeId = change.newNativeId;
			this.persist = change.persist;
			this.recovered = change.recovered;
			this.schemaType = change.schemaType;
			this.suspectedSyncPoisonItem = change.suspectedSyncPoisonItem;
			this.syncObject = change.syncObject;
			this.syncReportObject = new SyncReportObject(this.syncObject, this.schemaType);
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0003BB1C File Offset: 0x00039D1C
		private SyncChangeEntry(ChangeType changeType, SchemaType schemaType, string cloudId, object cloudObject, ISyncObject syncObject)
		{
			this.changeType = changeType;
			this.schemaType = schemaType;
			this.cloudId = cloudId;
			this.cloudObject = cloudObject;
			this.syncObject = syncObject;
			this.persist = true;
			this.suspectedSyncPoisonItem = new SyncPoisonItem(cloudId, SyncPoisonEntitySource.Remote, SyncChangeEntry.GetSyncPoisonEntityType(schemaType));
			this.syncReportObject = new SyncReportObject(this.syncObject, this.schemaType);
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001231 RID: 4657 RVA: 0x0003BB85 File Offset: 0x00039D85
		// (set) Token: 0x06001232 RID: 4658 RVA: 0x0003BBA1 File Offset: 0x00039DA1
		public int?[] ChangeTrackingInformation
		{
			get
			{
				if (this.syncOperation != null)
				{
					return this.syncOperation.ChangeTrackingInformation;
				}
				return this.changeTrackingInformation;
			}
			set
			{
				if (this.syncOperation != null)
				{
					this.syncOperation.ChangeTrackingInformation = value;
					return;
				}
				this.changeTrackingInformation = value;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x0003BBBF File Offset: 0x00039DBF
		// (set) Token: 0x06001234 RID: 4660 RVA: 0x0003BBC7 File Offset: 0x00039DC7
		public ChangeType ChangeType
		{
			get
			{
				return this.changeType;
			}
			set
			{
				this.changeType = value;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x0003BBD0 File Offset: 0x00039DD0
		// (set) Token: 0x06001236 RID: 4662 RVA: 0x0003BBD8 File Offset: 0x00039DD8
		public SchemaType SchemaType
		{
			get
			{
				return this.schemaType;
			}
			set
			{
				this.schemaType = value;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x0003BBE1 File Offset: 0x00039DE1
		// (set) Token: 0x06001238 RID: 4664 RVA: 0x0003BBE9 File Offset: 0x00039DE9
		public string CloudId
		{
			get
			{
				return this.cloudId;
			}
			set
			{
				this.cloudId = value;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x0003BBF2 File Offset: 0x00039DF2
		// (set) Token: 0x0600123A RID: 4666 RVA: 0x0003BBFA File Offset: 0x00039DFA
		public StoreObjectId NativeId
		{
			get
			{
				return this.nativeId;
			}
			set
			{
				this.nativeId = value;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x0003BC03 File Offset: 0x00039E03
		// (set) Token: 0x0600123C RID: 4668 RVA: 0x0003BC0B File Offset: 0x00039E0B
		public StoreObjectId NativeFolderId
		{
			get
			{
				return this.nativeFolderId;
			}
			set
			{
				this.nativeFolderId = value;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x0003BC14 File Offset: 0x00039E14
		// (set) Token: 0x0600123E RID: 4670 RVA: 0x0003BC1C File Offset: 0x00039E1C
		public bool Recovered
		{
			get
			{
				return this.recovered;
			}
			set
			{
				this.recovered = value;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x0003BC25 File Offset: 0x00039E25
		// (set) Token: 0x06001240 RID: 4672 RVA: 0x0003BC2D File Offset: 0x00039E2D
		public StoreObjectId NewNativeFolderId
		{
			get
			{
				return this.newNativeFolderId;
			}
			set
			{
				this.newNativeFolderId = value;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x0003BC36 File Offset: 0x00039E36
		// (set) Token: 0x06001242 RID: 4674 RVA: 0x0003BC3E File Offset: 0x00039E3E
		public StoreObjectId NewNativeId
		{
			get
			{
				return this.newNativeId;
			}
			set
			{
				this.newNativeId = value;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001243 RID: 4675 RVA: 0x0003BC47 File Offset: 0x00039E47
		// (set) Token: 0x06001244 RID: 4676 RVA: 0x0003BC4F File Offset: 0x00039E4F
		public string NewCloudFolderId
		{
			get
			{
				return this.newCloudFolderId;
			}
			set
			{
				this.newCloudFolderId = value;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001245 RID: 4677 RVA: 0x0003BC58 File Offset: 0x00039E58
		// (set) Token: 0x06001246 RID: 4678 RVA: 0x0003BC60 File Offset: 0x00039E60
		public string NewCloudId
		{
			get
			{
				return this.newCloudId;
			}
			set
			{
				this.newCloudId = value;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001247 RID: 4679 RVA: 0x0003BC69 File Offset: 0x00039E69
		// (set) Token: 0x06001248 RID: 4680 RVA: 0x0003BC71 File Offset: 0x00039E71
		public string CloudFolderId
		{
			get
			{
				return this.cloudFolderId;
			}
			set
			{
				this.cloudFolderId = value;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001249 RID: 4681 RVA: 0x0003BC7A File Offset: 0x00039E7A
		// (set) Token: 0x0600124A RID: 4682 RVA: 0x0003BC82 File Offset: 0x00039E82
		public string CloudVersion
		{
			get
			{
				return this.cloudVersion;
			}
			set
			{
				this.cloudVersion = value;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x0600124B RID: 4683 RVA: 0x0003BC8B File Offset: 0x00039E8B
		public ISyncItem SyncItem
		{
			get
			{
				if (this.syncOperation != null)
				{
					return this.syncOperation.GetItem(new PropertyDefinition[0]);
				}
				return null;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x0003BCA8 File Offset: 0x00039EA8
		public SyncOperation SyncOperation
		{
			get
			{
				return this.syncOperation;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x0003BCB0 File Offset: 0x00039EB0
		// (set) Token: 0x0600124E RID: 4686 RVA: 0x0003BCB8 File Offset: 0x00039EB8
		public object CloudObject
		{
			get
			{
				return this.cloudObject;
			}
			set
			{
				this.cloudObject = value;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x0600124F RID: 4687 RVA: 0x0003BCC1 File Offset: 0x00039EC1
		// (set) Token: 0x06001250 RID: 4688 RVA: 0x0003BCC9 File Offset: 0x00039EC9
		public ISyncReportObject SyncReportObject
		{
			get
			{
				return this.syncReportObject;
			}
			set
			{
				this.syncReportObject = value;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x0003BCD2 File Offset: 0x00039ED2
		// (set) Token: 0x06001252 RID: 4690 RVA: 0x0003BCDC File Offset: 0x00039EDC
		public ISyncObject SyncObject
		{
			get
			{
				return this.syncObject;
			}
			set
			{
				if (value != null && value.Type != this.schemaType)
				{
					throw new InvalidOperationException("Either SyncObject is null or it must match the SchemaType of this change.");
				}
				this.syncObject = value;
				if (this.syncObject != null)
				{
					this.syncReportObject = new SyncReportObject(this.syncObject, this.schemaType);
				}
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x0003BD2B File Offset: 0x00039F2B
		public ISyncItem Item
		{
			get
			{
				return this.SyncItem;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x0003BD33 File Offset: 0x00039F33
		public ISyncItemId Id
		{
			get
			{
				if (this.syncOperation != null)
				{
					return this.syncOperation.Id;
				}
				return null;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x0003BD4A File Offset: 0x00039F4A
		public bool SendEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x0003BD4D File Offset: 0x00039F4D
		public string ClientAddId
		{
			get
			{
				return this.CloudId;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x0003BD55 File Offset: 0x00039F55
		// (set) Token: 0x06001258 RID: 4696 RVA: 0x0003BD5D File Offset: 0x00039F5D
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
			set
			{
				this.exception = value;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x0003BD66 File Offset: 0x00039F66
		// (set) Token: 0x0600125A RID: 4698 RVA: 0x0003BD6E File Offset: 0x00039F6E
		public bool Persist
		{
			get
			{
				return this.persist;
			}
			set
			{
				this.persist = value;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x0003BD77 File Offset: 0x00039F77
		public bool HasException
		{
			get
			{
				return this.exception != null;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x0003BD84 File Offset: 0x00039F84
		// (set) Token: 0x0600125D RID: 4701 RVA: 0x0003BD8C File Offset: 0x00039F8C
		public bool ApplyAttempted { get; set; }

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x0003BD95 File Offset: 0x00039F95
		public bool ResolvedSuccessfully
		{
			get
			{
				return this.resolvedSuccessfully;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x0003BD9D File Offset: 0x00039F9D
		// (set) Token: 0x06001260 RID: 4704 RVA: 0x0003BDA5 File Offset: 0x00039FA5
		public byte[] ChangeKey
		{
			get
			{
				return this.changeKey;
			}
			set
			{
				this.changeKey = value;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x0003BDAE File Offset: 0x00039FAE
		internal SyncPoisonItem SuspectedSyncPoisonItem
		{
			get
			{
				return this.suspectedSyncPoisonItem;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x0003BDB6 File Offset: 0x00039FB6
		// (set) Token: 0x06001263 RID: 4707 RVA: 0x0003BDBE File Offset: 0x00039FBE
		internal string MessageClass
		{
			get
			{
				return this.messageClass;
			}
			set
			{
				this.messageClass = value;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x0003BDC7 File Offset: 0x00039FC7
		// (set) Token: 0x06001265 RID: 4709 RVA: 0x0003BDE3 File Offset: 0x00039FE3
		internal Dictionary<string, string> Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.Properties = new Dictionary<string, string>(3);
				}
				return this.properties;
			}
			set
			{
				this.properties = value;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001266 RID: 4710 RVA: 0x0003BDEC File Offset: 0x00039FEC
		// (set) Token: 0x06001267 RID: 4711 RVA: 0x0003BDF4 File Offset: 0x00039FF4
		internal bool Submitted { get; set; }

		// Token: 0x06001268 RID: 4712 RVA: 0x0003BE00 File Offset: 0x0003A000
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} - {1} - CloudFolderId/CloudId: {2}/{3} - NativeFolderId/NativeID: {4}/{5} - Persist: {6} - Exception: {7}", new object[]
			{
				this.changeType,
				this.schemaType,
				this.cloudFolderId ?? string.Empty,
				this.cloudId ?? string.Empty,
				(this.nativeFolderId == null) ? string.Empty : this.nativeFolderId.ToString(),
				(this.nativeId == null) ? string.Empty : this.nativeId.ToString(),
				this.persist,
				(this.exception == null) ? string.Empty : this.exception.ToString()
			});
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x0003BEC9 File Offset: 0x0003A0C9
		internal void SetResolvedSuccessfully()
		{
			this.resolvedSuccessfully = true;
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x0003BED2 File Offset: 0x0003A0D2
		private static SyncPoisonEntityType GetSyncPoisonEntityType(SchemaType schemaType)
		{
			if (schemaType != SchemaType.Folder)
			{
				return SyncPoisonEntityType.Item;
			}
			return SyncPoisonEntityType.Folder;
		}

		// Token: 0x040009BE RID: 2494
		private const int DefaultEstimatePropertyCapacity = 3;

		// Token: 0x040009BF RID: 2495
		private readonly SyncPoisonItem suspectedSyncPoisonItem;

		// Token: 0x040009C0 RID: 2496
		private int?[] changeTrackingInformation;

		// Token: 0x040009C1 RID: 2497
		private ChangeType changeType;

		// Token: 0x040009C2 RID: 2498
		private string cloudId;

		// Token: 0x040009C3 RID: 2499
		private object cloudObject;

		// Token: 0x040009C4 RID: 2500
		private ISyncObject syncObject;

		// Token: 0x040009C5 RID: 2501
		private ISyncReportObject syncReportObject;

		// Token: 0x040009C6 RID: 2502
		private SyncOperation syncOperation;

		// Token: 0x040009C7 RID: 2503
		private Exception exception;

		// Token: 0x040009C8 RID: 2504
		private bool persist;

		// Token: 0x040009C9 RID: 2505
		private StoreObjectId nativeId;

		// Token: 0x040009CA RID: 2506
		private StoreObjectId nativeFolderId;

		// Token: 0x040009CB RID: 2507
		private string cloudFolderId;

		// Token: 0x040009CC RID: 2508
		private string cloudVersion;

		// Token: 0x040009CD RID: 2509
		private SchemaType schemaType;

		// Token: 0x040009CE RID: 2510
		private StoreObjectId newNativeFolderId;

		// Token: 0x040009CF RID: 2511
		private StoreObjectId newNativeId;

		// Token: 0x040009D0 RID: 2512
		private string newCloudFolderId;

		// Token: 0x040009D1 RID: 2513
		private string newCloudId;

		// Token: 0x040009D2 RID: 2514
		private bool recovered;

		// Token: 0x040009D3 RID: 2515
		private byte[] changeKey;

		// Token: 0x040009D4 RID: 2516
		private string messageClass;

		// Token: 0x040009D5 RID: 2517
		private bool resolvedSuccessfully;

		// Token: 0x040009D6 RID: 2518
		private Dictionary<string, string> properties;
	}
}
