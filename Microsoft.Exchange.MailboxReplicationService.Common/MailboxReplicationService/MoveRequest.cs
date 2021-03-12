using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Provisioning;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001E9 RID: 489
	[ProvisioningObjectTag("MoveRequest")]
	[Serializable]
	public class MoveRequest : MailEnabledOrgPerson
	{
		// Token: 0x06001451 RID: 5201 RVA: 0x0002E604 File Offset: 0x0002C804
		public MoveRequest()
		{
			base.SetObjectClass("user");
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0002E617 File Offset: 0x0002C817
		public MoveRequest(ADUser dataObject) : base(dataObject)
		{
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x0002E620 File Offset: 0x0002C820
		public Guid ExchangeGuid
		{
			get
			{
				return (Guid)this[MoveRequestUserSchema.ExchangeGuid];
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x0002E632 File Offset: 0x0002C832
		public ADObjectId SourceDatabase
		{
			get
			{
				return (ADObjectId)this[MoveRequestUserSchema.SourceDatabase];
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x0002E644 File Offset: 0x0002C844
		public ADObjectId TargetDatabase
		{
			get
			{
				return (ADObjectId)this[MoveRequestUserSchema.TargetDatabase];
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x0002E656 File Offset: 0x0002C856
		public ADObjectId SourceArchiveDatabase
		{
			get
			{
				return (ADObjectId)this[MoveRequestUserSchema.SourceArchiveDatabase];
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x0002E668 File Offset: 0x0002C868
		public ADObjectId TargetArchiveDatabase
		{
			get
			{
				return (ADObjectId)this[MoveRequestUserSchema.TargetArchiveDatabase];
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x0002E67A File Offset: 0x0002C87A
		public RequestFlags Flags
		{
			get
			{
				return (RequestFlags)this[MoveRequestUserSchema.Flags];
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x0002E68C File Offset: 0x0002C88C
		public string RemoteHostName
		{
			get
			{
				return (string)this[MoveRequestUserSchema.RemoteHostName];
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x0002E69E File Offset: 0x0002C89E
		public string BatchName
		{
			get
			{
				return (string)this[MoveRequestUserSchema.BatchName];
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x0002E6B0 File Offset: 0x0002C8B0
		public RequestStatus Status
		{
			get
			{
				return (RequestStatus)this[MoveRequestUserSchema.Status];
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x0002E6C2 File Offset: 0x0002C8C2
		public RequestStyle RequestStyle
		{
			get
			{
				if ((this.Flags & RequestFlags.CrossOrg) == RequestFlags.None)
				{
					return RequestStyle.IntraOrg;
				}
				return RequestStyle.CrossOrg;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x0002E6D1 File Offset: 0x0002C8D1
		public RequestDirection Direction
		{
			get
			{
				if ((this.Flags & RequestFlags.Push) == RequestFlags.None)
				{
					return RequestDirection.Pull;
				}
				return RequestDirection.Push;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x0002E6E0 File Offset: 0x0002C8E0
		public bool IsOffline
		{
			get
			{
				return (this.Flags & RequestFlags.Offline) != RequestFlags.None;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x0002E6F1 File Offset: 0x0002C8F1
		public bool Protect
		{
			get
			{
				return (this.Flags & RequestFlags.Protected) != RequestFlags.None;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x0002E702 File Offset: 0x0002C902
		public bool Suspend
		{
			get
			{
				return (this.Flags & RequestFlags.Suspend) != RequestFlags.None;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x0002E716 File Offset: 0x0002C916
		public bool SuspendWhenReadyToComplete
		{
			get
			{
				return (this.Flags & RequestFlags.SuspendWhenReadyToComplete) != RequestFlags.None;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x0002E72A File Offset: 0x0002C92A
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return MoveRequest.schema;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x0002E731 File Offset: 0x0002C931
		// (set) Token: 0x06001464 RID: 5220 RVA: 0x0002E739 File Offset: 0x0002C939
		private new MultiValuedProperty<ADObjectId> AcceptMessagesOnlyFrom
		{
			get
			{
				return base.AcceptMessagesOnlyFrom;
			}
			set
			{
				base.AcceptMessagesOnlyFrom = value;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x0002E742 File Offset: 0x0002C942
		// (set) Token: 0x06001466 RID: 5222 RVA: 0x0002E74A File Offset: 0x0002C94A
		private new MultiValuedProperty<ADObjectId> AcceptMessagesOnlyFromDLMembers
		{
			get
			{
				return base.AcceptMessagesOnlyFromDLMembers;
			}
			set
			{
				base.AcceptMessagesOnlyFromDLMembers = value;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x0002E753 File Offset: 0x0002C953
		// (set) Token: 0x06001468 RID: 5224 RVA: 0x0002E75B File Offset: 0x0002C95B
		private new MultiValuedProperty<ADObjectId> AcceptMessagesOnlyFromSendersOrMembers
		{
			get
			{
				return base.AcceptMessagesOnlyFromSendersOrMembers;
			}
			set
			{
				base.AcceptMessagesOnlyFromSendersOrMembers = value;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x0002E764 File Offset: 0x0002C964
		private new MultiValuedProperty<ADObjectId> AddressListMembership
		{
			get
			{
				return base.AddressListMembership;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x0002E76C File Offset: 0x0002C96C
		// (set) Token: 0x0600146B RID: 5227 RVA: 0x0002E774 File Offset: 0x0002C974
		private new ADObjectId ArbitrationMailbox
		{
			get
			{
				return base.ArbitrationMailbox;
			}
			set
			{
				base.ArbitrationMailbox = value;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x0002E77D File Offset: 0x0002C97D
		// (set) Token: 0x0600146D RID: 5229 RVA: 0x0002E785 File Offset: 0x0002C985
		private new MultiValuedProperty<ADObjectId> BypassModerationFromSendersOrMembers
		{
			get
			{
				return base.BypassModerationFromSendersOrMembers;
			}
			set
			{
				base.BypassModerationFromSendersOrMembers = value;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x0002E78E File Offset: 0x0002C98E
		// (set) Token: 0x0600146F RID: 5231 RVA: 0x0002E796 File Offset: 0x0002C996
		private new string CustomAttribute1
		{
			get
			{
				return base.CustomAttribute1;
			}
			set
			{
				base.CustomAttribute1 = value;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x0002E79F File Offset: 0x0002C99F
		// (set) Token: 0x06001471 RID: 5233 RVA: 0x0002E7A7 File Offset: 0x0002C9A7
		private new string CustomAttribute10
		{
			get
			{
				return base.CustomAttribute10;
			}
			set
			{
				base.CustomAttribute10 = value;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x0002E7B0 File Offset: 0x0002C9B0
		// (set) Token: 0x06001473 RID: 5235 RVA: 0x0002E7B8 File Offset: 0x0002C9B8
		private new string CustomAttribute11
		{
			get
			{
				return base.CustomAttribute11;
			}
			set
			{
				base.CustomAttribute11 = value;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x0002E7C1 File Offset: 0x0002C9C1
		// (set) Token: 0x06001475 RID: 5237 RVA: 0x0002E7C9 File Offset: 0x0002C9C9
		private new string CustomAttribute12
		{
			get
			{
				return base.CustomAttribute12;
			}
			set
			{
				base.CustomAttribute12 = value;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x0002E7D2 File Offset: 0x0002C9D2
		// (set) Token: 0x06001477 RID: 5239 RVA: 0x0002E7DA File Offset: 0x0002C9DA
		private new string CustomAttribute13
		{
			get
			{
				return base.CustomAttribute13;
			}
			set
			{
				base.CustomAttribute13 = value;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x0002E7E3 File Offset: 0x0002C9E3
		// (set) Token: 0x06001479 RID: 5241 RVA: 0x0002E7EB File Offset: 0x0002C9EB
		private new string CustomAttribute14
		{
			get
			{
				return base.CustomAttribute14;
			}
			set
			{
				base.CustomAttribute14 = value;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x0002E7F4 File Offset: 0x0002C9F4
		// (set) Token: 0x0600147B RID: 5243 RVA: 0x0002E7FC File Offset: 0x0002C9FC
		private new string CustomAttribute15
		{
			get
			{
				return base.CustomAttribute15;
			}
			set
			{
				base.CustomAttribute15 = value;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x0002E805 File Offset: 0x0002CA05
		// (set) Token: 0x0600147D RID: 5245 RVA: 0x0002E80D File Offset: 0x0002CA0D
		private new string CustomAttribute2
		{
			get
			{
				return base.CustomAttribute2;
			}
			set
			{
				base.CustomAttribute2 = value;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x0600147E RID: 5246 RVA: 0x0002E816 File Offset: 0x0002CA16
		// (set) Token: 0x0600147F RID: 5247 RVA: 0x0002E81E File Offset: 0x0002CA1E
		private new string CustomAttribute3
		{
			get
			{
				return base.CustomAttribute3;
			}
			set
			{
				base.CustomAttribute3 = value;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001480 RID: 5248 RVA: 0x0002E827 File Offset: 0x0002CA27
		// (set) Token: 0x06001481 RID: 5249 RVA: 0x0002E82F File Offset: 0x0002CA2F
		private new string CustomAttribute4
		{
			get
			{
				return base.CustomAttribute4;
			}
			set
			{
				base.CustomAttribute4 = value;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001482 RID: 5250 RVA: 0x0002E838 File Offset: 0x0002CA38
		// (set) Token: 0x06001483 RID: 5251 RVA: 0x0002E840 File Offset: 0x0002CA40
		private new string CustomAttribute5
		{
			get
			{
				return base.CustomAttribute5;
			}
			set
			{
				base.CustomAttribute5 = value;
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001484 RID: 5252 RVA: 0x0002E849 File Offset: 0x0002CA49
		// (set) Token: 0x06001485 RID: 5253 RVA: 0x0002E851 File Offset: 0x0002CA51
		private new string CustomAttribute6
		{
			get
			{
				return base.CustomAttribute6;
			}
			set
			{
				base.CustomAttribute6 = value;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x0002E85A File Offset: 0x0002CA5A
		// (set) Token: 0x06001487 RID: 5255 RVA: 0x0002E862 File Offset: 0x0002CA62
		private new string CustomAttribute7
		{
			get
			{
				return base.CustomAttribute7;
			}
			set
			{
				base.CustomAttribute7 = value;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001488 RID: 5256 RVA: 0x0002E86B File Offset: 0x0002CA6B
		// (set) Token: 0x06001489 RID: 5257 RVA: 0x0002E873 File Offset: 0x0002CA73
		private new string CustomAttribute8
		{
			get
			{
				return base.CustomAttribute8;
			}
			set
			{
				base.CustomAttribute8 = value;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x0002E87C File Offset: 0x0002CA7C
		// (set) Token: 0x0600148B RID: 5259 RVA: 0x0002E884 File Offset: 0x0002CA84
		private new string CustomAttribute9
		{
			get
			{
				return base.CustomAttribute9;
			}
			set
			{
				base.CustomAttribute9 = value;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x0002E88D File Offset: 0x0002CA8D
		// (set) Token: 0x0600148D RID: 5261 RVA: 0x0002E895 File Offset: 0x0002CA95
		private new ProxyAddressCollection EmailAddresses
		{
			get
			{
				return base.EmailAddresses;
			}
			set
			{
				base.EmailAddresses = value;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x0002E89E File Offset: 0x0002CA9E
		// (set) Token: 0x0600148F RID: 5263 RVA: 0x0002E8A6 File Offset: 0x0002CAA6
		private new bool EmailAddressPolicyEnabled
		{
			get
			{
				return base.EmailAddressPolicyEnabled;
			}
			set
			{
				base.EmailAddressPolicyEnabled = value;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x0002E8AF File Offset: 0x0002CAAF
		private new MultiValuedProperty<string> Extensions
		{
			get
			{
				return base.Extensions;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x0002E8B7 File Offset: 0x0002CAB7
		// (set) Token: 0x06001492 RID: 5266 RVA: 0x0002E8BF File Offset: 0x0002CABF
		private new MultiValuedProperty<ADObjectId> GrantSendOnBehalfTo
		{
			get
			{
				return base.GrantSendOnBehalfTo;
			}
			set
			{
				base.GrantSendOnBehalfTo = value;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x0002E8C8 File Offset: 0x0002CAC8
		private new bool HasPicture
		{
			get
			{
				return base.HasPicture;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x0002E8D0 File Offset: 0x0002CAD0
		private new bool HasSpokenName
		{
			get
			{
				return base.HasSpokenName;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x0002E8D8 File Offset: 0x0002CAD8
		// (set) Token: 0x06001496 RID: 5270 RVA: 0x0002E8E0 File Offset: 0x0002CAE0
		private new bool HiddenFromAddressListsEnabled
		{
			get
			{
				return base.HiddenFromAddressListsEnabled;
			}
			set
			{
				base.HiddenFromAddressListsEnabled = value;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x0002E8E9 File Offset: 0x0002CAE9
		private new string LegacyExchangeDN
		{
			get
			{
				return base.LegacyExchangeDN;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x0002E8F1 File Offset: 0x0002CAF1
		// (set) Token: 0x06001499 RID: 5273 RVA: 0x0002E8F9 File Offset: 0x0002CAF9
		private new string MailTip
		{
			get
			{
				return base.MailTip;
			}
			set
			{
				base.MailTip = value;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x0002E902 File Offset: 0x0002CB02
		// (set) Token: 0x0600149B RID: 5275 RVA: 0x0002E90A File Offset: 0x0002CB0A
		private new MultiValuedProperty<string> MailTipTranslations
		{
			get
			{
				return base.MailTipTranslations;
			}
			set
			{
				base.MailTipTranslations = value;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x0002E913 File Offset: 0x0002CB13
		// (set) Token: 0x0600149D RID: 5277 RVA: 0x0002E91B File Offset: 0x0002CB1B
		private new Unlimited<ByteQuantifiedSize> MaxSendSize
		{
			get
			{
				return base.MaxSendSize;
			}
			set
			{
				base.MaxSendSize = value;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x0002E924 File Offset: 0x0002CB24
		// (set) Token: 0x0600149F RID: 5279 RVA: 0x0002E92C File Offset: 0x0002CB2C
		private new Unlimited<ByteQuantifiedSize> MaxReceiveSize
		{
			get
			{
				return base.MaxReceiveSize;
			}
			set
			{
				base.MaxReceiveSize = value;
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x0002E935 File Offset: 0x0002CB35
		// (set) Token: 0x060014A1 RID: 5281 RVA: 0x0002E93D File Offset: 0x0002CB3D
		private new MultiValuedProperty<ADObjectId> ModeratedBy
		{
			get
			{
				return base.ModeratedBy;
			}
			set
			{
				base.ModeratedBy = value;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x0002E946 File Offset: 0x0002CB46
		// (set) Token: 0x060014A3 RID: 5283 RVA: 0x0002E94E File Offset: 0x0002CB4E
		private new bool ModerationEnabled
		{
			get
			{
				return base.ModerationEnabled;
			}
			set
			{
				base.ModerationEnabled = value;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x0002E957 File Offset: 0x0002CB57
		// (set) Token: 0x060014A5 RID: 5285 RVA: 0x0002E95F File Offset: 0x0002CB5F
		private new ADObjectId ObjectCategory
		{
			get
			{
				return base.ObjectCategory;
			}
			set
			{
				base.ObjectCategory = value;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x0002E968 File Offset: 0x0002CB68
		private new MultiValuedProperty<string> ObjectClass
		{
			get
			{
				return base.ObjectClass;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x0002E970 File Offset: 0x0002CB70
		private new string OrganizationalUnit
		{
			get
			{
				return base.OrganizationalUnit;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x0002E978 File Offset: 0x0002CB78
		private new MultiValuedProperty<string> PoliciesIncluded
		{
			get
			{
				return base.PoliciesIncluded;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x0002E980 File Offset: 0x0002CB80
		private new MultiValuedProperty<string> PoliciesExcluded
		{
			get
			{
				return base.PoliciesExcluded;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x060014AA RID: 5290 RVA: 0x0002E988 File Offset: 0x0002CB88
		// (set) Token: 0x060014AB RID: 5291 RVA: 0x0002E990 File Offset: 0x0002CB90
		private new SmtpAddress PrimarySmtpAddress
		{
			get
			{
				return base.PrimarySmtpAddress;
			}
			set
			{
				base.PrimarySmtpAddress = value;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x060014AC RID: 5292 RVA: 0x0002E999 File Offset: 0x0002CB99
		// (set) Token: 0x060014AD RID: 5293 RVA: 0x0002E9A1 File Offset: 0x0002CBA1
		private new MultiValuedProperty<ADObjectId> RejectMessagesFrom
		{
			get
			{
				return base.RejectMessagesFrom;
			}
			set
			{
				base.RejectMessagesFrom = value;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x0002E9AA File Offset: 0x0002CBAA
		// (set) Token: 0x060014AF RID: 5295 RVA: 0x0002E9B2 File Offset: 0x0002CBB2
		private new MultiValuedProperty<ADObjectId> RejectMessagesFromDLMembers
		{
			get
			{
				return base.RejectMessagesFromDLMembers;
			}
			set
			{
				base.RejectMessagesFromDLMembers = value;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x0002E9BB File Offset: 0x0002CBBB
		// (set) Token: 0x060014B1 RID: 5297 RVA: 0x0002E9C3 File Offset: 0x0002CBC3
		private new MultiValuedProperty<ADObjectId> RejectMessagesFromSendersOrMembers
		{
			get
			{
				return base.RejectMessagesFromSendersOrMembers;
			}
			set
			{
				base.RejectMessagesFromSendersOrMembers = value;
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x0002E9CC File Offset: 0x0002CBCC
		// (set) Token: 0x060014B3 RID: 5299 RVA: 0x0002E9D4 File Offset: 0x0002CBD4
		private new bool RequireSenderAuthenticationEnabled
		{
			get
			{
				return base.RequireSenderAuthenticationEnabled;
			}
			set
			{
				base.RequireSenderAuthenticationEnabled = value;
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x0002E9DD File Offset: 0x0002CBDD
		// (set) Token: 0x060014B5 RID: 5301 RVA: 0x0002E9E5 File Offset: 0x0002CBE5
		private new TransportModerationNotificationFlags SendModerationNotifications
		{
			get
			{
				return base.SendModerationNotifications;
			}
			set
			{
				base.SendModerationNotifications = value;
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x0002E9EE File Offset: 0x0002CBEE
		// (set) Token: 0x060014B7 RID: 5303 RVA: 0x0002E9F6 File Offset: 0x0002CBF6
		private new string SimpleDisplayName
		{
			get
			{
				return base.SimpleDisplayName;
			}
			set
			{
				base.SimpleDisplayName = value;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x0002E9FF File Offset: 0x0002CBFF
		// (set) Token: 0x060014B9 RID: 5305 RVA: 0x0002EA07 File Offset: 0x0002CC07
		private new MultiValuedProperty<string> UMDtmfMap
		{
			get
			{
				return base.UMDtmfMap;
			}
			set
			{
				base.UMDtmfMap = value;
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x0002EA10 File Offset: 0x0002CC10
		private new DateTime? WhenChanged
		{
			get
			{
				return base.WhenChanged;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x060014BB RID: 5307 RVA: 0x0002EA18 File Offset: 0x0002CC18
		private new DateTime? WhenCreated
		{
			get
			{
				return base.WhenCreated;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x0002EA20 File Offset: 0x0002CC20
		private new DateTime? WhenChangedUTC
		{
			get
			{
				return base.WhenChangedUTC;
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x0002EA28 File Offset: 0x0002CC28
		private new DateTime? WhenCreatedUTC
		{
			get
			{
				return base.WhenCreatedUTC;
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x0002EA30 File Offset: 0x0002CC30
		// (set) Token: 0x060014BF RID: 5311 RVA: 0x0002EA38 File Offset: 0x0002CC38
		private new SmtpAddress WindowsEmailAddress
		{
			get
			{
				return base.WindowsEmailAddress;
			}
			set
			{
				base.WindowsEmailAddress = value;
			}
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0002EA44 File Offset: 0x0002CC44
		internal static MoveRequest FromDataObject(ADUser dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			if (dataObject.Database != null)
			{
				dataObject.Database = ADObjectIdResolutionHelper.ResolveDN(dataObject.Database);
			}
			if (dataObject.ArchiveDatabase != null)
			{
				dataObject.ArchiveDatabase = ADObjectIdResolutionHelper.ResolveDN(dataObject.ArchiveDatabase);
			}
			if (dataObject.MailboxMoveSourceMDB != null)
			{
				dataObject.MailboxMoveSourceMDB = ADObjectIdResolutionHelper.ResolveDN(dataObject.MailboxMoveSourceMDB);
			}
			if (dataObject.MailboxMoveTargetMDB != null)
			{
				dataObject.MailboxMoveTargetMDB = ADObjectIdResolutionHelper.ResolveDN(dataObject.MailboxMoveTargetMDB);
			}
			if (dataObject.MailboxMoveSourceArchiveMDB != null)
			{
				dataObject.MailboxMoveSourceArchiveMDB = ADObjectIdResolutionHelper.ResolveDN(dataObject.MailboxMoveSourceArchiveMDB);
			}
			if (dataObject.MailboxMoveTargetArchiveMDB != null)
			{
				dataObject.MailboxMoveTargetArchiveMDB = ADObjectIdResolutionHelper.ResolveDN(dataObject.MailboxMoveTargetArchiveMDB);
			}
			return new MoveRequest(dataObject);
		}

		// Token: 0x04000A4E RID: 2638
		private static MoveRequestUserSchema schema = ObjectSchema.GetInstance<MoveRequestUserSchema>();
	}
}
