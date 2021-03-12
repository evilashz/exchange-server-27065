using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription.DeltaSync
{
	// Token: 0x020000E1 RID: 225
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class DeltaSyncAggregationSubscription : WindowsLiveServiceAggregationSubscription
	{
		// Token: 0x060006AA RID: 1706 RVA: 0x000205A7 File Offset: 0x0001E7A7
		public DeltaSyncAggregationSubscription()
		{
			base.SubscriptionProtocolName = "Delta Sync";
			base.SubscriptionProtocolVersion = 131078;
			base.SubscriptionType = AggregationSubscriptionType.DeltaSyncMail;
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x000205CC File Offset: 0x0001E7CC
		public override string IncomingServerName
		{
			get
			{
				if (string.IsNullOrEmpty(base.IncommingServerUrl))
				{
					return "DeltaSyncMailProxyEndPoint";
				}
				return base.IncommingServerUrl;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x000205E7 File Offset: 0x0001E7E7
		// (set) Token: 0x060006AD RID: 1709 RVA: 0x000205EF File Offset: 0x0001E7EF
		public int MinSyncPollInterval
		{
			get
			{
				return this.minSyncPollInterval;
			}
			set
			{
				this.minSyncPollInterval = value;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x000205F8 File Offset: 0x0001E7F8
		// (set) Token: 0x060006AF RID: 1711 RVA: 0x00020600 File Offset: 0x0001E800
		public int MinSettingPollInterval
		{
			get
			{
				return this.minSettingPollInterval;
			}
			set
			{
				this.minSettingPollInterval = value;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x00020609 File Offset: 0x0001E809
		// (set) Token: 0x060006B1 RID: 1713 RVA: 0x00020611 File Offset: 0x0001E811
		public double SyncMultiplier
		{
			get
			{
				return this.syncMultiplier;
			}
			set
			{
				this.syncMultiplier = value;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0002061A File Offset: 0x0001E81A
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x00020622 File Offset: 0x0001E822
		public int MaxObjectInSync
		{
			get
			{
				return this.maxObjectInSync;
			}
			set
			{
				this.maxObjectInSync = value;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x0002062B File Offset: 0x0001E82B
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x00020633 File Offset: 0x0001E833
		public int MaxNumberOfEmailAdds
		{
			get
			{
				return this.maxNumberOfEmailAdds;
			}
			set
			{
				this.maxNumberOfEmailAdds = value;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0002063C File Offset: 0x0001E83C
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x00020644 File Offset: 0x0001E844
		public int MaxNumberOfFolderAdds
		{
			get
			{
				return this.maxNumberOfFolderAdds;
			}
			set
			{
				this.maxNumberOfFolderAdds = value;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x0002064D File Offset: 0x0001E84D
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x00020655 File Offset: 0x0001E855
		public int MaxAttachments
		{
			get
			{
				return this.maxAttachments;
			}
			set
			{
				this.maxAttachments = value;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x0002065E File Offset: 0x0001E85E
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x00020666 File Offset: 0x0001E866
		public long MaxMessageSize
		{
			get
			{
				return this.maxMessageSize;
			}
			set
			{
				this.maxMessageSize = value;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x0002066F File Offset: 0x0001E86F
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x00020677 File Offset: 0x0001E877
		public int MaxRecipients
		{
			get
			{
				return this.maxRecipients;
			}
			set
			{
				this.maxRecipients = value;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x00020680 File Offset: 0x0001E880
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x00020688 File Offset: 0x0001E888
		public DeltaSyncAccountStatus AccountStatus
		{
			get
			{
				return this.accountStatus;
			}
			set
			{
				this.accountStatus = value;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x00020691 File Offset: 0x0001E891
		public override int? EnumeratedItemsLimitPerConnection
		{
			get
			{
				return new int?(Math.Max(0, Math.Min(Math.Min(this.maxNumberOfFolderAdds, this.maxObjectInSync), this.maxNumberOfEmailAdds)));
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x000206BA File Offset: 0x0001E8BA
		public override FolderSupport FolderSupport
		{
			get
			{
				return FolderSupport.RootFoldersOnly;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x000206BD File Offset: 0x0001E8BD
		public override ItemSupport ItemSupport
		{
			get
			{
				return ItemSupport.Email | ItemSupport.Generic;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x000206C1 File Offset: 0x0001E8C1
		public override SyncQuirks SyncQuirks
		{
			get
			{
				return SyncQuirks.EnumerateItemChangeAsDeleteAndAdd | SyncQuirks.DoNotTerminateSlowSyncs;
			}
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x000206C5 File Offset: 0x0001E8C5
		public override PimSubscriptionProxy CreateSubscriptionProxy()
		{
			return new HotmailSubscriptionProxy(this);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x000206D0 File Offset: 0x0001E8D0
		protected override void SetPropertiesToMessageObject(MessageItem message)
		{
			base.SetPropertiesToMessageObject(message);
			message[StoreObjectSchema.ItemClass] = "IPM.Aggregation.DeltaSync";
			message[MessageItemSchema.SharingProviderGuid] = DeltaSyncAggregationSubscription.DeltaSyncProviderGuid;
			message[AggregationSubscriptionMessageSchema.SharingMinSyncPollInterval] = this.MinSyncPollInterval;
			message[AggregationSubscriptionMessageSchema.SharingMinSettingPollInterval] = this.MinSettingPollInterval;
			message[AggregationSubscriptionMessageSchema.SharingSyncMultiplier] = this.SyncMultiplier;
			message[AggregationSubscriptionMessageSchema.SharingMaxObjectsInSync] = this.MaxObjectInSync;
			message[AggregationSubscriptionMessageSchema.SharingMaxNumberOfEmails] = this.MaxNumberOfEmailAdds;
			message[AggregationSubscriptionMessageSchema.SharingMaxNumberOfFolders] = this.MaxNumberOfFolderAdds;
			message[AggregationSubscriptionMessageSchema.SharingMaxAttachments] = this.MaxAttachments;
			message[AggregationSubscriptionMessageSchema.SharingMaxMessageSize] = this.MaxMessageSize;
			message[AggregationSubscriptionMessageSchema.SharingMaxRecipients] = this.MaxRecipients;
			message[MessageItemSchema.SharingDetail] = (((int)message[MessageItemSchema.SharingDetail] & -769) | (int)this.AccountStatus);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x000207FC File Offset: 0x0001E9FC
		protected override void LoadProperties(MessageItem message)
		{
			base.LoadProperties(message);
			base.GetIntProperty(message, AggregationSubscriptionMessageSchema.SharingMinSyncPollInterval, null, null, out this.minSyncPollInterval);
			base.GetIntProperty(message, AggregationSubscriptionMessageSchema.SharingMinSettingPollInterval, null, null, out this.minSettingPollInterval);
			base.GetDoubleProperty(message, AggregationSubscriptionMessageSchema.SharingSyncMultiplier, null, null, out this.syncMultiplier);
			base.GetIntProperty(message, AggregationSubscriptionMessageSchema.SharingMaxObjectsInSync, null, null, out this.maxObjectInSync);
			base.GetIntProperty(message, AggregationSubscriptionMessageSchema.SharingMaxNumberOfEmails, null, null, out this.maxNumberOfEmailAdds);
			base.GetIntProperty(message, AggregationSubscriptionMessageSchema.SharingMaxNumberOfFolders, null, null, out this.maxNumberOfFolderAdds);
			base.GetIntProperty(message, AggregationSubscriptionMessageSchema.SharingMaxAttachments, null, null, out this.maxAttachments);
			base.GetLongProperty(message, AggregationSubscriptionMessageSchema.SharingMaxMessageSize, null, null, out this.maxMessageSize);
			base.GetIntProperty(message, AggregationSubscriptionMessageSchema.SharingMaxRecipients, null, null, out this.maxRecipients);
			base.GetEnumProperty<DeltaSyncAccountStatus>(message, MessageItemSchema.SharingDetail, new int?(768), out this.accountStatus);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0002097E File Offset: 0x0001EB7E
		protected override bool ValidateIncomingServerUrl(string incomingServerUrl)
		{
			return Uri.CheckHostName(incomingServerUrl) != UriHostNameType.Unknown;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0002098C File Offset: 0x0001EB8C
		protected override void Serialize(AggregationSubscription.SubscriptionSerializer subscriptionSerializer)
		{
			subscriptionSerializer.SerializeDeltaSyncSubscription(this);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00020995 File Offset: 0x0001EB95
		protected override void Deserialize(AggregationSubscription.SubscriptionDeserializer deserializer)
		{
			deserializer.DeserializeDeltaSyncSubscription(this);
		}

		// Token: 0x0400039E RID: 926
		private const string DefaultDeltaSyncIncomingServerName = "DeltaSyncMailProxyEndPoint";

		// Token: 0x0400039F RID: 927
		private const string DeltaSyncProtocolName = "Delta Sync";

		// Token: 0x040003A0 RID: 928
		private const int DeltaSyncProtocolVersion = 131078;

		// Token: 0x040003A1 RID: 929
		private static readonly Guid DeltaSyncProviderGuid = new Guid("633ebb53-78bf-4eb0-a849-2447b815d5c7");

		// Token: 0x040003A2 RID: 930
		private int minSyncPollInterval;

		// Token: 0x040003A3 RID: 931
		private int minSettingPollInterval;

		// Token: 0x040003A4 RID: 932
		private double syncMultiplier;

		// Token: 0x040003A5 RID: 933
		private int maxObjectInSync;

		// Token: 0x040003A6 RID: 934
		private int maxNumberOfEmailAdds;

		// Token: 0x040003A7 RID: 935
		private int maxNumberOfFolderAdds;

		// Token: 0x040003A8 RID: 936
		private int maxAttachments;

		// Token: 0x040003A9 RID: 937
		private long maxMessageSize;

		// Token: 0x040003AA RID: 938
		private int maxRecipients;

		// Token: 0x040003AB RID: 939
		private DeltaSyncAccountStatus accountStatus;
	}
}
