using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200023C RID: 572
	[DataContract]
	[KnownType(typeof(DistributionGroup))]
	[KnownType(typeof(TrusteeRow))]
	public class DistributionGroup : DistributionGroupRow
	{
		// Token: 0x060027E3 RID: 10211 RVA: 0x0007D34E File Offset: 0x0007B54E
		public DistributionGroup(DistributionGroup distributionGroup) : base(distributionGroup)
		{
			this.OriginalDistributionGroup = distributionGroup;
		}

		// Token: 0x17001C32 RID: 7218
		// (get) Token: 0x060027E4 RID: 10212 RVA: 0x0007D35E File Offset: 0x0007B55E
		// (set) Token: 0x060027E5 RID: 10213 RVA: 0x0007D366 File Offset: 0x0007B566
		public DistributionGroup OriginalDistributionGroup { get; private set; }

		// Token: 0x17001C33 RID: 7219
		// (get) Token: 0x060027E6 RID: 10214 RVA: 0x0007D36F File Offset: 0x0007B56F
		// (set) Token: 0x060027E7 RID: 10215 RVA: 0x0007D377 File Offset: 0x0007B577
		public WindowsGroup WindowsGroup { get; set; }

		// Token: 0x17001C34 RID: 7220
		// (get) Token: 0x060027E8 RID: 10216 RVA: 0x0007D380 File Offset: 0x0007B580
		// (set) Token: 0x060027E9 RID: 10217 RVA: 0x0007D38D File Offset: 0x0007B58D
		[DataMember]
		public string Caption
		{
			get
			{
				return this.OriginalDistributionGroup.DisplayName;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C35 RID: 7221
		// (get) Token: 0x060027EA RID: 10218 RVA: 0x0007D394 File Offset: 0x0007B594
		// (set) Token: 0x060027EB RID: 10219 RVA: 0x0007D3A1 File Offset: 0x0007B5A1
		[DataMember]
		public string Name
		{
			get
			{
				return this.OriginalDistributionGroup.Name;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C36 RID: 7222
		// (get) Token: 0x060027EC RID: 10220 RVA: 0x0007D3A8 File Offset: 0x0007B5A8
		// (set) Token: 0x060027ED RID: 10221 RVA: 0x0007D3B5 File Offset: 0x0007B5B5
		[DataMember]
		public string Alias
		{
			get
			{
				return this.OriginalDistributionGroup.Alias;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C37 RID: 7223
		// (get) Token: 0x060027EE RID: 10222 RVA: 0x0007D3BC File Offset: 0x0007B5BC
		// (set) Token: 0x060027EF RID: 10223 RVA: 0x0007D3D6 File Offset: 0x0007B5D6
		[DataMember]
		public bool IsSecurityGroupType
		{
			get
			{
				return (this.OriginalDistributionGroup.GroupType & GroupTypeFlags.SecurityEnabled) == GroupTypeFlags.SecurityEnabled;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C38 RID: 7224
		// (get) Token: 0x060027F0 RID: 10224 RVA: 0x0007D3E0 File Offset: 0x0007B5E0
		// (set) Token: 0x060027F1 RID: 10225 RVA: 0x0007D400 File Offset: 0x0007B600
		[DataMember]
		public string PrimaryEAAlias
		{
			get
			{
				return this.OriginalDistributionGroup.PrimarySmtpAddress.Local;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C39 RID: 7225
		// (get) Token: 0x060027F2 RID: 10226 RVA: 0x0007D408 File Offset: 0x0007B608
		// (set) Token: 0x060027F3 RID: 10227 RVA: 0x0007D428 File Offset: 0x0007B628
		[DataMember]
		public string DomainName
		{
			get
			{
				return this.OriginalDistributionGroup.PrimarySmtpAddress.Domain;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C3A RID: 7226
		// (get) Token: 0x060027F4 RID: 10228 RVA: 0x0007D42F File Offset: 0x0007B62F
		// (set) Token: 0x060027F5 RID: 10229 RVA: 0x0007D437 File Offset: 0x0007B637
		[DataMember]
		public string HiddenPrimarySmtpAddress
		{
			get
			{
				return base.PrimarySmtpAddress;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C3B RID: 7227
		// (get) Token: 0x060027F6 RID: 10230 RVA: 0x0007D43E File Offset: 0x0007B63E
		// (set) Token: 0x060027F7 RID: 10231 RVA: 0x0007D450 File Offset: 0x0007B650
		[DataMember]
		public bool IsSecurityGroupMemberJoinApprovalRequired
		{
			get
			{
				return this.MemberJoinRestriction == "ApprovalRequired";
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C3C RID: 7228
		// (get) Token: 0x060027F8 RID: 10232 RVA: 0x0007D457 File Offset: 0x0007B657
		// (set) Token: 0x060027F9 RID: 10233 RVA: 0x0007D46E File Offset: 0x0007B66E
		[DataMember]
		public string MemberJoinRestriction
		{
			get
			{
				return this.OriginalDistributionGroup.MemberJoinRestriction.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C3D RID: 7229
		// (get) Token: 0x060027FA RID: 10234 RVA: 0x0007D475 File Offset: 0x0007B675
		// (set) Token: 0x060027FB RID: 10235 RVA: 0x0007D48C File Offset: 0x0007B68C
		[DataMember]
		public string MemberDepartRestriction
		{
			get
			{
				return this.OriginalDistributionGroup.MemberDepartRestriction.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C3E RID: 7230
		// (get) Token: 0x060027FC RID: 10236 RVA: 0x0007D493 File Offset: 0x0007B693
		// (set) Token: 0x060027FD RID: 10237 RVA: 0x0007D4A0 File Offset: 0x0007B6A0
		[DataMember]
		public bool HiddenFromAddressListsEnabled
		{
			get
			{
				return this.OriginalDistributionGroup.HiddenFromAddressListsEnabled;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C3F RID: 7231
		// (get) Token: 0x060027FE RID: 10238 RVA: 0x0007D4A8 File Offset: 0x0007B6A8
		// (set) Token: 0x060027FF RID: 10239 RVA: 0x0007D4CD File Offset: 0x0007B6CD
		[DataMember]
		public string RequireSenderAuthenticationEnabled
		{
			get
			{
				return this.OriginalDistributionGroup.RequireSenderAuthenticationEnabled.ToString().ToLowerInvariant();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C40 RID: 7232
		// (get) Token: 0x06002800 RID: 10240 RVA: 0x0007D4D4 File Offset: 0x0007B6D4
		// (set) Token: 0x06002801 RID: 10241 RVA: 0x0007D4F4 File Offset: 0x0007B6F4
		[DataMember]
		public string MailTip
		{
			get
			{
				if (this.OriginalDistributionGroup.MailTip != null)
				{
					return this.OriginalDistributionGroup.MailTip;
				}
				return string.Empty;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C41 RID: 7233
		// (get) Token: 0x06002802 RID: 10242 RVA: 0x0007D4FB File Offset: 0x0007B6FB
		// (set) Token: 0x06002803 RID: 10243 RVA: 0x0007D50D File Offset: 0x0007B70D
		[DataMember]
		public IEnumerable<RecipientObjectResolverRow> ManagedBy
		{
			get
			{
				return this.OriginalDistributionGroup.ManagedBy.ResolveRecipients();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C42 RID: 7234
		// (get) Token: 0x06002804 RID: 10244 RVA: 0x0007D514 File Offset: 0x0007B714
		// (set) Token: 0x06002805 RID: 10245 RVA: 0x0007D526 File Offset: 0x0007B726
		[DataMember]
		public IEnumerable<RecipientObjectResolverRow> AcceptMessagesOnlyFromSendersOrMembers
		{
			get
			{
				return this.OriginalDistributionGroup.AcceptMessagesOnlyFromSendersOrMembers.ResolveRecipients();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C43 RID: 7235
		// (get) Token: 0x06002806 RID: 10246 RVA: 0x0007D52D File Offset: 0x0007B72D
		// (set) Token: 0x06002807 RID: 10247 RVA: 0x0007D53A File Offset: 0x0007B73A
		[DataMember]
		public bool ModerationEnabled
		{
			get
			{
				return this.OriginalDistributionGroup.ModerationEnabled;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C44 RID: 7236
		// (get) Token: 0x06002808 RID: 10248 RVA: 0x0007D541 File Offset: 0x0007B741
		// (set) Token: 0x06002809 RID: 10249 RVA: 0x0007D553 File Offset: 0x0007B753
		[DataMember]
		public IEnumerable<RecipientObjectResolverRow> ModeratedBy
		{
			get
			{
				return this.OriginalDistributionGroup.ModeratedBy.ResolveRecipients();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C45 RID: 7237
		// (get) Token: 0x0600280A RID: 10250 RVA: 0x0007D55A File Offset: 0x0007B75A
		// (set) Token: 0x0600280B RID: 10251 RVA: 0x0007D56C File Offset: 0x0007B76C
		[DataMember]
		public IEnumerable<RecipientObjectResolverRow> BypassModerationFromSendersOrMembers
		{
			get
			{
				return this.OriginalDistributionGroup.BypassModerationFromSendersOrMembers.ResolveRecipients();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C46 RID: 7238
		// (get) Token: 0x0600280C RID: 10252 RVA: 0x0007D573 File Offset: 0x0007B773
		// (set) Token: 0x0600280D RID: 10253 RVA: 0x0007D58A File Offset: 0x0007B78A
		[DataMember]
		public string SendModerationNotifications
		{
			get
			{
				return this.OriginalDistributionGroup.SendModerationNotifications.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C47 RID: 7239
		// (get) Token: 0x0600280E RID: 10254 RVA: 0x0007D5B0 File Offset: 0x0007B7B0
		// (set) Token: 0x0600280F RID: 10255 RVA: 0x0007D60C File Offset: 0x0007B80C
		[DataMember]
		public IEnumerable<Identity> EmailAddresses
		{
			get
			{
				return from address in this.OriginalDistributionGroup.EmailAddresses
				where address is SmtpProxyAddress
				select new Identity(address.AddressString, address.AddressString);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C48 RID: 7240
		// (get) Token: 0x06002810 RID: 10256 RVA: 0x0007D613 File Offset: 0x0007B813
		// (set) Token: 0x06002811 RID: 10257 RVA: 0x0007D620 File Offset: 0x0007B820
		[DataMember]
		public string OrganizationalUnit
		{
			get
			{
				return this.OriginalDistributionGroup.OrganizationalUnit;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C49 RID: 7241
		// (get) Token: 0x06002812 RID: 10258 RVA: 0x0007D627 File Offset: 0x0007B827
		// (set) Token: 0x06002813 RID: 10259 RVA: 0x0007D639 File Offset: 0x0007B839
		[DataMember]
		public IEnumerable<RecipientObjectResolverRow> GrantSendOnBehalfTo
		{
			get
			{
				return this.OriginalDistributionGroup.GrantSendOnBehalfTo.ResolveRecipients();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C4A RID: 7242
		// (get) Token: 0x06002814 RID: 10260 RVA: 0x0007D640 File Offset: 0x0007B840
		// (set) Token: 0x06002815 RID: 10261 RVA: 0x0007D657 File Offset: 0x0007B857
		[DataMember]
		public string Notes
		{
			get
			{
				if (this.WindowsGroup == null)
				{
					return null;
				}
				return this.WindowsGroup.Notes;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C4B RID: 7243
		// (get) Token: 0x06002816 RID: 10262 RVA: 0x0007D65E File Offset: 0x0007B85E
		// (set) Token: 0x06002817 RID: 10263 RVA: 0x0007D67A File Offset: 0x0007B87A
		[DataMember]
		public IEnumerable<RecipientObjectResolverRow> Members
		{
			get
			{
				if (this.WindowsGroup == null)
				{
					return null;
				}
				return this.WindowsGroup.Members.ResolveRecipients();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C4C RID: 7244
		// (get) Token: 0x06002818 RID: 10264 RVA: 0x0007D681 File Offset: 0x0007B881
		// (set) Token: 0x06002819 RID: 10265 RVA: 0x0007D689 File Offset: 0x0007B889
		[DataMember]
		public IEnumerable<AcePermissionRecipientRow> SendAsPermissionsEnterprise { get; set; }

		// Token: 0x17001C4D RID: 7245
		// (get) Token: 0x0600281A RID: 10266 RVA: 0x0007D692 File Offset: 0x0007B892
		// (set) Token: 0x0600281B RID: 10267 RVA: 0x0007D69A File Offset: 0x0007B89A
		[DataMember]
		public List<object> SendAsPermissionsCloud { get; set; }
	}
}
