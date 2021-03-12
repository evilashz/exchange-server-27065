using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription.Imap
{
	// Token: 0x020000E5 RID: 229
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class IMAPAggregationSubscription : PimAggregationSubscription
	{
		// Token: 0x060006D1 RID: 1745 RVA: 0x00020A11 File Offset: 0x0001EC11
		public IMAPAggregationSubscription()
		{
			this.IMAPPort = 143;
			base.SubscriptionProtocolName = "IMAP";
			base.SubscriptionProtocolVersion = 1;
			base.SubscriptionType = AggregationSubscriptionType.IMAP;
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x00020A3E File Offset: 0x0001EC3E
		public override string IncomingServerName
		{
			get
			{
				return this.imapServer;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x00020A4B File Offset: 0x0001EC4B
		public override int IncomingServerPort
		{
			get
			{
				return this.imapPort;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x00020A53 File Offset: 0x0001EC53
		public override string AuthenticationType
		{
			get
			{
				return this.IMAPAuthentication.ToString();
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x00020A65 File Offset: 0x0001EC65
		public override string EncryptionType
		{
			get
			{
				return this.IMAPSecurity.ToString();
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x00020A77 File Offset: 0x0001EC77
		// (set) Token: 0x060006D7 RID: 1751 RVA: 0x00020A7F File Offset: 0x0001EC7F
		public Fqdn IMAPServer
		{
			get
			{
				return this.imapServer;
			}
			set
			{
				this.imapServer = value;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00020A88 File Offset: 0x0001EC88
		// (set) Token: 0x060006D9 RID: 1753 RVA: 0x00020A90 File Offset: 0x0001EC90
		public int IMAPPort
		{
			get
			{
				return this.imapPort;
			}
			set
			{
				this.imapPort = value;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x00020A99 File Offset: 0x0001EC99
		// (set) Token: 0x060006DB RID: 1755 RVA: 0x00020AA1 File Offset: 0x0001ECA1
		public string IMAPLogOnName
		{
			get
			{
				return this.imapLogOnName;
			}
			set
			{
				this.imapLogOnName = value;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x00020AAA File Offset: 0x0001ECAA
		// (set) Token: 0x060006DD RID: 1757 RVA: 0x00020AB4 File Offset: 0x0001ECB4
		public IMAPSecurityMechanism IMAPSecurity
		{
			get
			{
				return (IMAPSecurityMechanism)(this.flags & IMAPAggregationFlags.SecurityMask);
			}
			set
			{
				this.flags &= ~(IMAPAggregationFlags.UseSsl | IMAPAggregationFlags.UseTls);
				this.flags |= (IMAPAggregationFlags)value;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x00020AD3 File Offset: 0x0001ECD3
		// (set) Token: 0x060006DF RID: 1759 RVA: 0x00020AE1 File Offset: 0x0001ECE1
		public IMAPAuthenticationMechanism IMAPAuthentication
		{
			get
			{
				return (IMAPAuthenticationMechanism)(this.flags & IMAPAggregationFlags.UseNtlmAuth);
			}
			set
			{
				this.flags &= ~IMAPAggregationFlags.UseNtlmAuth;
				this.flags |= (IMAPAggregationFlags)value;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x00020B03 File Offset: 0x0001ED03
		// (set) Token: 0x060006E1 RID: 1761 RVA: 0x00020B0B File Offset: 0x0001ED0B
		public IMAPAggregationFlags Flags
		{
			get
			{
				return this.flags;
			}
			internal set
			{
				this.flags = value;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x00020B14 File Offset: 0x0001ED14
		public override string VerifiedUserName
		{
			get
			{
				return this.IMAPLogOnName;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x00020B1C File Offset: 0x0001ED1C
		public override string VerifiedIncomingServer
		{
			get
			{
				return this.IMAPServer;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x00020B29 File Offset: 0x0001ED29
		public override bool SendAsNeedsVerification
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x00020B2C File Offset: 0x0001ED2C
		public override FolderSupport FolderSupport
		{
			get
			{
				return FolderSupport.FullHierarchy;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x00020B2F File Offset: 0x0001ED2F
		public override ItemSupport ItemSupport
		{
			get
			{
				return ItemSupport.Email | ItemSupport.Generic;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x00020B33 File Offset: 0x0001ED33
		public override SyncQuirks SyncQuirks
		{
			get
			{
				return SyncQuirks.EnumerateItemChangeAsDeleteAndAdd | SyncQuirks.OnlyDeleteFoldersIfNoSubFolders | SyncQuirks.AllowDirectCloudFolderUpdates;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x00020B37 File Offset: 0x0001ED37
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x00020B3F File Offset: 0x0001ED3F
		public string ImapPathPrefix
		{
			get
			{
				return this.imapPathPrefix;
			}
			set
			{
				this.imapPathPrefix = value;
			}
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00020B48 File Offset: 0x0001ED48
		public override PimSubscriptionProxy CreateSubscriptionProxy()
		{
			return new IMAPSubscriptionProxy(this);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00020B50 File Offset: 0x0001ED50
		protected override void SetPropertiesToMessageObject(MessageItem message)
		{
			base.SetPropertiesToMessageObject(message);
			message[StoreObjectSchema.ItemClass] = "IPM.Aggregation.IMAP";
			message[MessageItemSchema.SharingProviderGuid] = IMAPAggregationSubscription.IMAPProviderGuid;
			message[MessageItemSchema.SharingRemotePath] = this.IMAPServer.ToString() + ":" + this.IMAPPort.ToString(CultureInfo.InvariantCulture);
			message[AggregationSubscriptionMessageSchema.SharingRemoteUser] = this.IMAPLogOnName;
			message[MessageItemSchema.SharingDetail] = (int)(this.IMAPSecurity | (IMAPSecurityMechanism)this.IMAPAuthentication);
			if (string.IsNullOrEmpty(this.imapPathPrefix))
			{
				message.Delete(AggregationSubscriptionMessageSchema.SharingImapPathPrefix);
				return;
			}
			message[AggregationSubscriptionMessageSchema.SharingImapPathPrefix] = this.imapPathPrefix;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00020C14 File Offset: 0x0001EE14
		protected override void LoadProperties(MessageItem message)
		{
			base.LoadProperties(message);
			base.GetFqdnProperty(message, MessageItemSchema.SharingRemotePath, out this.imapServer, out this.imapPort);
			base.GetStringProperty(message, AggregationSubscriptionMessageSchema.SharingRemoteUser, false, new uint?(0U), new uint?(256U), out this.imapLogOnName);
			base.GetEnumProperty<IMAPAggregationFlags>(message, MessageItemSchema.SharingDetail, null, out this.flags);
			if (base.Version >= 6L)
			{
				base.GetStringProperty(message, AggregationSubscriptionMessageSchema.SharingImapPathPrefix, true, true, new uint?(0U), new uint?(256U), out this.imapPathPrefix);
				return;
			}
			this.imapPathPrefix = null;
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00020CB4 File Offset: 0x0001EEB4
		protected override void Serialize(AggregationSubscription.SubscriptionSerializer subscriptionSerializer)
		{
			subscriptionSerializer.SerializeImapSubscription(this);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00020CBD File Offset: 0x0001EEBD
		protected override void Deserialize(AggregationSubscription.SubscriptionDeserializer deserializer)
		{
			deserializer.DeserializeImapSubscription(this);
		}

		// Token: 0x040003B5 RID: 949
		private const string IMAPProtocolName = "IMAP";

		// Token: 0x040003B6 RID: 950
		private const int IMAPProtocolVersion = 1;

		// Token: 0x040003B7 RID: 951
		private const int DefaultIMAPPort = 143;

		// Token: 0x040003B8 RID: 952
		private static readonly Guid IMAPProviderGuid = new Guid("E59E7F14-1FDF-41d4-BDAB-DF7F95BF8F47");

		// Token: 0x040003B9 RID: 953
		private Fqdn imapServer;

		// Token: 0x040003BA RID: 954
		private int imapPort;

		// Token: 0x040003BB RID: 955
		private string imapLogOnName;

		// Token: 0x040003BC RID: 956
		private IMAPAggregationFlags flags;

		// Token: 0x040003BD RID: 957
		private string imapPathPrefix;
	}
}
