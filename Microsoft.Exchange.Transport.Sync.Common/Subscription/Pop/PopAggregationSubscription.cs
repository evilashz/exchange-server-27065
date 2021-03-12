using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription.Pop
{
	// Token: 0x020000EF RID: 239
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class PopAggregationSubscription : PimAggregationSubscription
	{
		// Token: 0x06000707 RID: 1799 RVA: 0x00020FB2 File Offset: 0x0001F1B2
		public PopAggregationSubscription()
		{
			this.PopPort = 110;
			this.flags = PopAggregationFlags.LeaveOnServer;
			base.SubscriptionProtocolName = "POP";
			base.SubscriptionProtocolVersion = 196608;
			base.SubscriptionType = AggregationSubscriptionType.Pop;
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x00020FE7 File Offset: 0x0001F1E7
		public override string IncomingServerName
		{
			get
			{
				return this.popServer;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x00020FF4 File Offset: 0x0001F1F4
		public override int IncomingServerPort
		{
			get
			{
				return this.popPort;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x00020FFC File Offset: 0x0001F1FC
		public override string AuthenticationType
		{
			get
			{
				return this.PopAuthentication.ToString();
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x0002100E File Offset: 0x0001F20E
		public override string EncryptionType
		{
			get
			{
				return this.PopSecurity.ToString();
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x00021020 File Offset: 0x0001F220
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x00021028 File Offset: 0x0001F228
		public Fqdn PopServer
		{
			get
			{
				return this.popServer;
			}
			set
			{
				this.popServer = value;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x00021031 File Offset: 0x0001F231
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x00021039 File Offset: 0x0001F239
		public int PopPort
		{
			get
			{
				return this.popPort;
			}
			set
			{
				this.popPort = value;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x00021042 File Offset: 0x0001F242
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x0002104A File Offset: 0x0001F24A
		public string PopLogonName
		{
			get
			{
				return this.popLogonName;
			}
			set
			{
				this.popLogonName = value;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x00021053 File Offset: 0x0001F253
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x0002105D File Offset: 0x0001F25D
		public SecurityMechanism PopSecurity
		{
			get
			{
				return (SecurityMechanism)(this.flags & PopAggregationFlags.SecurityMask);
			}
			set
			{
				this.flags &= ~(PopAggregationFlags.UseSsl | PopAggregationFlags.UseTls);
				this.flags |= (PopAggregationFlags)value;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x0002107C File Offset: 0x0001F27C
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x0002108A File Offset: 0x0001F28A
		public AuthenticationMechanism PopAuthentication
		{
			get
			{
				return (AuthenticationMechanism)(this.flags & PopAggregationFlags.AuthenticationMask);
			}
			set
			{
				this.flags &= (PopAggregationFlags)(-28673);
				this.flags |= (PopAggregationFlags)value;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x000210AC File Offset: 0x0001F2AC
		public bool ShouldSyncDelete
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x000210AF File Offset: 0x0001F2AF
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x000210BD File Offset: 0x0001F2BD
		public bool ShouldLeaveOnServer
		{
			get
			{
				return PopAggregationFlags.UseBasicAuth < (this.flags & PopAggregationFlags.LeaveOnServer);
			}
			set
			{
				if (value)
				{
					this.flags |= PopAggregationFlags.LeaveOnServer;
					return;
				}
				this.flags &= ~PopAggregationFlags.LeaveOnServer;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x000210E1 File Offset: 0x0001F2E1
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x000210E9 File Offset: 0x0001F2E9
		public PopAggregationFlags Flags
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

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x000210F2 File Offset: 0x0001F2F2
		public override string VerifiedUserName
		{
			get
			{
				return this.PopLogonName;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x000210FA File Offset: 0x0001F2FA
		public override string VerifiedIncomingServer
		{
			get
			{
				return this.PopServer;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x00021107 File Offset: 0x0001F307
		public override bool SendAsNeedsVerification
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x0002110A File Offset: 0x0001F30A
		public override FolderSupport FolderSupport
		{
			get
			{
				return FolderSupport.InboxOnly;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0002110D File Offset: 0x0001F30D
		public override ItemSupport ItemSupport
		{
			get
			{
				return ItemSupport.Email | ItemSupport.Generic;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x00021111 File Offset: 0x0001F311
		public override SyncQuirks SyncQuirks
		{
			get
			{
				return SyncQuirks.EnumerateNativeDeletesOnly;
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00021114 File Offset: 0x0001F314
		public override PimSubscriptionProxy CreateSubscriptionProxy()
		{
			return new PopSubscriptionProxy(this);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0002111C File Offset: 0x0001F31C
		protected override void SetPropertiesToMessageObject(MessageItem message)
		{
			base.SetPropertiesToMessageObject(message);
			message[StoreObjectSchema.ItemClass] = "IPM.Aggregation.Pop";
			message[MessageItemSchema.SharingProviderGuid] = PopAggregationSubscription.PopProviderGuid;
			message[MessageItemSchema.SharingRemotePath] = this.PopServer.ToString() + ":" + this.PopPort.ToString();
			message[AggregationSubscriptionMessageSchema.SharingRemoteUser] = this.PopLogonName;
			message[MessageItemSchema.SharingDetail] = (int)this.Flags;
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x000211AC File Offset: 0x0001F3AC
		protected override void LoadProperties(MessageItem message)
		{
			base.LoadProperties(message);
			base.GetFqdnProperty(message, MessageItemSchema.SharingRemotePath, out this.popServer, out this.popPort);
			base.GetStringProperty(message, AggregationSubscriptionMessageSchema.SharingRemoteUser, false, new uint?(0U), new uint?(256U), out this.popLogonName);
			base.GetEnumProperty<PopAggregationFlags>(message, MessageItemSchema.SharingDetail, null, out this.flags);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00021216 File Offset: 0x0001F416
		protected override void Serialize(AggregationSubscription.SubscriptionSerializer subscriptionSerializer)
		{
			subscriptionSerializer.SerializePopSubscription(this);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0002121F File Offset: 0x0001F41F
		protected override void Deserialize(AggregationSubscription.SubscriptionDeserializer deserializer)
		{
			deserializer.DeserializePopSubscription(this);
		}

		// Token: 0x040003D8 RID: 984
		private const string PopProtocolName = "POP";

		// Token: 0x040003D9 RID: 985
		private const int PopProtocolVersion = 196608;

		// Token: 0x040003DA RID: 986
		private static readonly Guid PopProviderGuid = new Guid("1df33d70-5bf4-4bf2-844c-afe75ff140fb");

		// Token: 0x040003DB RID: 987
		private Fqdn popServer;

		// Token: 0x040003DC RID: 988
		private int popPort;

		// Token: 0x040003DD RID: 989
		private string popLogonName;

		// Token: 0x040003DE RID: 990
		private PopAggregationFlags flags;
	}
}
