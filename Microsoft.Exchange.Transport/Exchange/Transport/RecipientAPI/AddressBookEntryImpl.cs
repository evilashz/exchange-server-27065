using System;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.RecipientAPI
{
	// Token: 0x0200052F RID: 1327
	internal class AddressBookEntryImpl : AddressBookEntry
	{
		// Token: 0x06003DE4 RID: 15844 RVA: 0x00103F4F File Offset: 0x0010214F
		internal AddressBookEntryImpl(TransportMiniRecipient entry)
		{
			this.entry = entry;
		}

		// Token: 0x06003DE5 RID: 15845 RVA: 0x00103F69 File Offset: 0x00102169
		internal AddressBookEntryImpl(TransportMiniRecipient entry, RoutingAddress primaryAddress) : this(entry)
		{
			this.primaryAddress = primaryAddress;
		}

		// Token: 0x170012C5 RID: 4805
		// (get) Token: 0x06003DE6 RID: 15846 RVA: 0x00103F7C File Offset: 0x0010217C
		public override RoutingAddress PrimaryAddress
		{
			get
			{
				if (RoutingAddress.Empty == this.primaryAddress)
				{
					ProxyAddress proxyAddress = this.entry.EmailAddresses.FindPrimary(ProxyAddressPrefix.Smtp);
					if (null != proxyAddress)
					{
						this.primaryAddress = (RoutingAddress)proxyAddress.AddressString;
					}
				}
				return this.primaryAddress;
			}
		}

		// Token: 0x170012C6 RID: 4806
		// (get) Token: 0x06003DE7 RID: 15847 RVA: 0x00103FD1 File Offset: 0x001021D1
		public override bool RequiresAuthentication
		{
			get
			{
				return this.entry.RequireAllSendersAreAuthenticated;
			}
		}

		// Token: 0x170012C7 RID: 4807
		// (get) Token: 0x06003DE8 RID: 15848 RVA: 0x00103FDE File Offset: 0x001021DE
		public override bool AntispamBypass
		{
			get
			{
				return this.entry.AntispamBypassEnabled;
			}
		}

		// Token: 0x170012C8 RID: 4808
		// (get) Token: 0x06003DE9 RID: 15849 RVA: 0x00103FEC File Offset: 0x001021EC
		public override Microsoft.Exchange.Data.Transport.RecipientType RecipientType
		{
			get
			{
				Microsoft.Exchange.Data.Transport.RecipientType result = Microsoft.Exchange.Data.Transport.RecipientType.Unknown;
				MultiValuedProperty<string> objectClass = this.entry.ObjectClass;
				if (objectClass.Contains("user"))
				{
					RecipientDisplayType? recipientDisplayType = this.entry.RecipientDisplayType;
					if (recipientDisplayType == RecipientDisplayType.ConferenceRoomMailbox || recipientDisplayType == RecipientDisplayType.SyncedConferenceRoomMailbox)
					{
						result = Microsoft.Exchange.Data.Transport.RecipientType.ConferenceRoom;
					}
					else if (recipientDisplayType == RecipientDisplayType.EquipmentMailbox || recipientDisplayType == RecipientDisplayType.SyncedEquipmentMailbox)
					{
						result = Microsoft.Exchange.Data.Transport.RecipientType.Equipment;
					}
					else
					{
						result = Microsoft.Exchange.Data.Transport.RecipientType.User;
					}
				}
				else if (objectClass.Contains("contact"))
				{
					result = Microsoft.Exchange.Data.Transport.RecipientType.Contact;
				}
				else if (objectClass.Contains("group"))
				{
					result = Microsoft.Exchange.Data.Transport.RecipientType.DistributionList;
				}
				else if (objectClass.Contains("msExchDynamicDistributionList"))
				{
					result = Microsoft.Exchange.Data.Transport.RecipientType.DynamicDistributionList;
				}
				else if (objectClass.Contains("publicFolder"))
				{
					result = Microsoft.Exchange.Data.Transport.RecipientType.PublicFolder;
				}
				return result;
			}
		}

		// Token: 0x170012C9 RID: 4809
		// (get) Token: 0x06003DEA RID: 15850 RVA: 0x001040E0 File Offset: 0x001022E0
		public override SecurityIdentifier UserAccountSid
		{
			get
			{
				return this.entry.Sid;
			}
		}

		// Token: 0x170012CA RID: 4810
		// (get) Token: 0x06003DEB RID: 15851 RVA: 0x001040ED File Offset: 0x001022ED
		public override SecurityIdentifier MasterAccountSid
		{
			get
			{
				return this.entry.MasterAccountSid;
			}
		}

		// Token: 0x170012CB RID: 4811
		// (get) Token: 0x06003DEC RID: 15852 RVA: 0x001040FC File Offset: 0x001022FC
		public override string WindowsLiveId
		{
			get
			{
				return this.entry.WindowsLiveID.ToString();
			}
		}

		// Token: 0x06003DED RID: 15853 RVA: 0x00104124 File Offset: 0x00102324
		public override int GetSpamConfidenceLevelThreshold(SpamAction action, int defaultValue)
		{
			if (0 > defaultValue || (9 < defaultValue && 2147483647 != defaultValue))
			{
				throw new ArgumentOutOfRangeException("defaultValue", Strings.GetSclThresholdDefaultValueOutOfRange);
			}
			bool? flag = null;
			int? num = null;
			switch (action)
			{
			case SpamAction.Quarantine:
				flag = this.entry.SCLQuarantineEnabled;
				num = this.entry.SCLQuarantineThreshold;
				break;
			case SpamAction.Reject:
				flag = this.entry.SCLRejectEnabled;
				num = this.entry.SCLRejectThreshold;
				break;
			case SpamAction.Delete:
				flag = this.entry.SCLDeleteEnabled;
				num = this.entry.SCLDeleteThreshold;
				break;
			default:
				throw new ArgumentOutOfRangeException("action");
			}
			if (flag == false)
			{
				return int.MaxValue;
			}
			int? num2 = num;
			if (num2 == null)
			{
				return defaultValue;
			}
			return num2.GetValueOrDefault();
		}

		// Token: 0x06003DEE RID: 15854 RVA: 0x00104208 File Offset: 0x00102408
		public override bool IsSafeSender(RoutingAddress senderAddress)
		{
			if (this.safeSenders == null && this.entry.SafeSendersHash != null)
			{
				this.safeSenders = new AddressHashes(this.entry.SafeSendersHash);
			}
			return this.safeSenders != null && this.safeSenders.Contains(senderAddress);
		}

		// Token: 0x06003DEF RID: 15855 RVA: 0x00104258 File Offset: 0x00102458
		public override bool IsSafeRecipient(RoutingAddress recipientAddress)
		{
			if (this.safeRecipients == null && this.entry.SafeRecipientsHash != null)
			{
				this.safeRecipients = new AddressHashes(this.entry.SafeRecipientsHash);
			}
			return this.safeRecipients != null && this.safeRecipients.Contains(recipientAddress);
		}

		// Token: 0x06003DF0 RID: 15856 RVA: 0x001042A8 File Offset: 0x001024A8
		public override bool IsBlockedSender(RoutingAddress senderAddress)
		{
			if (this.blockedSenders == null && this.entry.BlockedSendersHash != null)
			{
				this.blockedSenders = new AddressHashes(this.entry.BlockedSendersHash);
			}
			return this.blockedSenders != null && this.blockedSenders.Contains(senderAddress);
		}

		// Token: 0x06003DF1 RID: 15857 RVA: 0x001042F6 File Offset: 0x001024F6
		public override string ToString()
		{
			return (string)this.PrimaryAddress;
		}

		// Token: 0x04001F94 RID: 8084
		private TransportMiniRecipient entry;

		// Token: 0x04001F95 RID: 8085
		private RoutingAddress primaryAddress = RoutingAddress.Empty;

		// Token: 0x04001F96 RID: 8086
		private AddressHashes safeSenders;

		// Token: 0x04001F97 RID: 8087
		private AddressHashes safeRecipients;

		// Token: 0x04001F98 RID: 8088
		private AddressHashes blockedSenders;
	}
}
