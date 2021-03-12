using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000E9 RID: 233
	[DataContract(Name = "SetDistributionGroupBase{0}{1}")]
	public abstract class SetDistributionGroupBase<T, U> : SetObjectProperties where T : SetGroupBase, new() where U : UpdateDistributionGroupMemberBase, new()
	{
		// Token: 0x06001E3F RID: 7743 RVA: 0x0005B704 File Offset: 0x00059904
		public SetDistributionGroupBase()
		{
			this.OnDeserializing(default(StreamingContext));
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x0005B726 File Offset: 0x00059926
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.SetGroup = Activator.CreateInstance<T>();
			this.UpdateDistributionGroupMember = Activator.CreateInstance<U>();
		}

		// Token: 0x170019A9 RID: 6569
		// (get) Token: 0x06001E41 RID: 7745 RVA: 0x0005B73E File Offset: 0x0005993E
		// (set) Token: 0x06001E42 RID: 7746 RVA: 0x0005B746 File Offset: 0x00059946
		public T SetGroup { get; private set; }

		// Token: 0x06001E43 RID: 7747 RVA: 0x0005B750 File Offset: 0x00059950
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (RbacPrincipal.Current.IsInRole("MultiTenant") && (this.PrimaryEAAlias != null || this.DomainName != null))
			{
				SmtpAddress smtpAddress = new SmtpAddress(this.HiddenPrimarySmtpAddress);
				string str = this.PrimaryEAAlias ?? smtpAddress.Local;
				string str2 = this.DomainName ?? smtpAddress.Domain;
				this.PrimarySmtpAddress = str + "@" + str2;
			}
		}

		// Token: 0x170019AA RID: 6570
		// (get) Token: 0x06001E44 RID: 7748 RVA: 0x0005B7C1 File Offset: 0x000599C1
		// (set) Token: 0x06001E45 RID: 7749 RVA: 0x0005B7C9 File Offset: 0x000599C9
		public U UpdateDistributionGroupMember { get; private set; }

		// Token: 0x170019AB RID: 6571
		// (get) Token: 0x06001E46 RID: 7750 RVA: 0x0005B7D2 File Offset: 0x000599D2
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-DistributionGroup";
			}
		}

		// Token: 0x170019AC RID: 6572
		// (get) Token: 0x06001E47 RID: 7751 RVA: 0x0005B7DC File Offset: 0x000599DC
		// (set) Token: 0x06001E48 RID: 7752 RVA: 0x0005B800 File Offset: 0x00059A00
		[DataMember]
		public string Notes
		{
			get
			{
				T setGroup = this.SetGroup;
				return setGroup.Notes;
			}
			set
			{
				T setGroup = this.SetGroup;
				setGroup.Notes = value;
			}
		}

		// Token: 0x170019AD RID: 6573
		// (get) Token: 0x06001E49 RID: 7753 RVA: 0x0005B824 File Offset: 0x00059A24
		// (set) Token: 0x06001E4A RID: 7754 RVA: 0x0005B848 File Offset: 0x00059A48
		[DataMember]
		public Identity[] Members
		{
			get
			{
				U updateDistributionGroupMember = this.UpdateDistributionGroupMember;
				return updateDistributionGroupMember.Members;
			}
			set
			{
				U updateDistributionGroupMember = this.UpdateDistributionGroupMember;
				updateDistributionGroupMember.Members = value;
			}
		}

		// Token: 0x170019AE RID: 6574
		// (get) Token: 0x06001E4B RID: 7755 RVA: 0x0005B86A File Offset: 0x00059A6A
		// (set) Token: 0x06001E4C RID: 7756 RVA: 0x0005B87C File Offset: 0x00059A7C
		[DataMember]
		public string DisplayName
		{
			get
			{
				return (string)base[MailEnabledRecipientSchema.DisplayName];
			}
			set
			{
				base[MailEnabledRecipientSchema.DisplayName] = value;
			}
		}

		// Token: 0x170019AF RID: 6575
		// (get) Token: 0x06001E4D RID: 7757 RVA: 0x0005B88A File Offset: 0x00059A8A
		// (set) Token: 0x06001E4E RID: 7758 RVA: 0x0005B89C File Offset: 0x00059A9C
		[DataMember]
		public Identity[] AcceptMessagesOnlyFromSendersOrMembers
		{
			get
			{
				return Identity.FromIdParameters(base[MailEnabledRecipientSchema.AcceptMessagesOnlyFromSendersOrMembers]);
			}
			set
			{
				base[MailEnabledRecipientSchema.AcceptMessagesOnlyFromSendersOrMembers] = value.ToIdParameters();
			}
		}

		// Token: 0x170019B0 RID: 6576
		// (get) Token: 0x06001E4F RID: 7759 RVA: 0x0005B8AF File Offset: 0x00059AAF
		// (set) Token: 0x06001E50 RID: 7760 RVA: 0x0005B8C1 File Offset: 0x00059AC1
		[DataMember]
		public Identity[] ManagedBy
		{
			get
			{
				return Identity.FromIdParameters(base[DistributionGroupSchema.ManagedBy]);
			}
			set
			{
				base[DistributionGroupSchema.ManagedBy] = value.ToIdParameters();
			}
		}

		// Token: 0x170019B1 RID: 6577
		// (get) Token: 0x06001E51 RID: 7761 RVA: 0x0005B8D4 File Offset: 0x00059AD4
		// (set) Token: 0x06001E52 RID: 7762 RVA: 0x0005B8DC File Offset: 0x00059ADC
		[DataMember]
		public CollectionDelta SendAsPermissionsEnterprise { get; set; }

		// Token: 0x170019B2 RID: 6578
		// (get) Token: 0x06001E53 RID: 7763 RVA: 0x0005B8E5 File Offset: 0x00059AE5
		// (set) Token: 0x06001E54 RID: 7764 RVA: 0x0005B8ED File Offset: 0x00059AED
		[DataMember]
		public CollectionDelta SendAsPermissionsCloud { get; set; }

		// Token: 0x170019B3 RID: 6579
		// (get) Token: 0x06001E55 RID: 7765 RVA: 0x0005B8F6 File Offset: 0x00059AF6
		// (set) Token: 0x06001E56 RID: 7766 RVA: 0x0005B908 File Offset: 0x00059B08
		[DataMember]
		public Identity[] GrantSendOnBehalfTo
		{
			get
			{
				return Identity.FromIdParameters(base[MailEnabledRecipientSchema.GrantSendOnBehalfTo]);
			}
			set
			{
				base[MailEnabledRecipientSchema.GrantSendOnBehalfTo] = value.ToIdParameters();
			}
		}

		// Token: 0x170019B4 RID: 6580
		// (get) Token: 0x06001E57 RID: 7767 RVA: 0x0005B91B File Offset: 0x00059B1B
		// (set) Token: 0x06001E58 RID: 7768 RVA: 0x0005B937 File Offset: 0x00059B37
		[DataMember]
		public bool ModerationEnabled
		{
			get
			{
				return (bool)(base[MailEnabledRecipientSchema.ModerationEnabled] ?? false);
			}
			set
			{
				base[MailEnabledRecipientSchema.ModerationEnabled] = value;
			}
		}

		// Token: 0x170019B5 RID: 6581
		// (get) Token: 0x06001E59 RID: 7769 RVA: 0x0005B94A File Offset: 0x00059B4A
		// (set) Token: 0x06001E5A RID: 7770 RVA: 0x0005B95C File Offset: 0x00059B5C
		[DataMember]
		public Identity[] ModeratedBy
		{
			get
			{
				return Identity.FromIdParameters(base[MailEnabledRecipientSchema.ModeratedBy]);
			}
			set
			{
				base[MailEnabledRecipientSchema.ModeratedBy] = value.ToIdParameters();
			}
		}

		// Token: 0x170019B6 RID: 6582
		// (get) Token: 0x06001E5B RID: 7771 RVA: 0x0005B96F File Offset: 0x00059B6F
		// (set) Token: 0x06001E5C RID: 7772 RVA: 0x0005B981 File Offset: 0x00059B81
		[DataMember]
		public Identity[] BypassModerationFromSendersOrMembers
		{
			get
			{
				return Identity.FromIdParameters(base[MailEnabledRecipientSchema.BypassModerationFromSendersOrMembers]);
			}
			set
			{
				base[MailEnabledRecipientSchema.BypassModerationFromSendersOrMembers] = value.ToIdParameters();
			}
		}

		// Token: 0x170019B7 RID: 6583
		// (get) Token: 0x06001E5D RID: 7773 RVA: 0x0005B994 File Offset: 0x00059B94
		// (set) Token: 0x06001E5E RID: 7774 RVA: 0x0005B9A6 File Offset: 0x00059BA6
		[DataMember]
		public string SendModerationNotifications
		{
			get
			{
				return (string)base[MailEnabledRecipientSchema.SendModerationNotifications];
			}
			set
			{
				base[MailEnabledRecipientSchema.SendModerationNotifications] = value;
			}
		}

		// Token: 0x170019B8 RID: 6584
		// (get) Token: 0x06001E5F RID: 7775 RVA: 0x0005B9B4 File Offset: 0x00059BB4
		// (set) Token: 0x06001E60 RID: 7776 RVA: 0x0005B9C6 File Offset: 0x00059BC6
		[DataMember]
		public string Alias
		{
			get
			{
				return (string)base[MailEnabledRecipientSchema.Alias];
			}
			set
			{
				base[MailEnabledRecipientSchema.Alias] = value;
			}
		}

		// Token: 0x170019B9 RID: 6585
		// (get) Token: 0x06001E61 RID: 7777 RVA: 0x0005B9D4 File Offset: 0x00059BD4
		// (set) Token: 0x06001E62 RID: 7778 RVA: 0x0005B9E6 File Offset: 0x00059BE6
		[DataMember]
		public bool IsSecurityGroupMemberJoinApprovalRequired
		{
			get
			{
				return this.MemberJoinRestriction == "ApprovalRequired";
			}
			set
			{
				this.MemberJoinRestriction = (value ? "ApprovalRequired" : "Closed");
			}
		}

		// Token: 0x170019BA RID: 6586
		// (get) Token: 0x06001E63 RID: 7779 RVA: 0x0005B9FD File Offset: 0x00059BFD
		// (set) Token: 0x06001E64 RID: 7780 RVA: 0x0005BA0F File Offset: 0x00059C0F
		[DataMember]
		public string MemberJoinRestriction
		{
			get
			{
				return (string)base[DistributionGroupSchema.MemberJoinRestriction];
			}
			set
			{
				base[DistributionGroupSchema.MemberJoinRestriction] = value;
			}
		}

		// Token: 0x170019BB RID: 6587
		// (get) Token: 0x06001E65 RID: 7781 RVA: 0x0005BA1D File Offset: 0x00059C1D
		// (set) Token: 0x06001E66 RID: 7782 RVA: 0x0005BA2F File Offset: 0x00059C2F
		public string PrimarySmtpAddress
		{
			get
			{
				return (string)base[MailEnabledRecipientSchema.PrimarySmtpAddress];
			}
			set
			{
				base[MailEnabledRecipientSchema.PrimarySmtpAddress] = value;
			}
		}

		// Token: 0x170019BC RID: 6588
		// (get) Token: 0x06001E67 RID: 7783 RVA: 0x0005BA3D File Offset: 0x00059C3D
		// (set) Token: 0x06001E68 RID: 7784 RVA: 0x0005BA45 File Offset: 0x00059C45
		[DataMember]
		public string PrimaryEAAlias { get; set; }

		// Token: 0x170019BD RID: 6589
		// (get) Token: 0x06001E69 RID: 7785 RVA: 0x0005BA4E File Offset: 0x00059C4E
		// (set) Token: 0x06001E6A RID: 7786 RVA: 0x0005BA56 File Offset: 0x00059C56
		[DataMember]
		public string DomainName { get; set; }

		// Token: 0x170019BE RID: 6590
		// (get) Token: 0x06001E6B RID: 7787 RVA: 0x0005BA5F File Offset: 0x00059C5F
		// (set) Token: 0x06001E6C RID: 7788 RVA: 0x0005BA67 File Offset: 0x00059C67
		[DataMember]
		public string HiddenPrimarySmtpAddress { get; set; }

		// Token: 0x170019BF RID: 6591
		// (get) Token: 0x06001E6D RID: 7789 RVA: 0x0005BA70 File Offset: 0x00059C70
		// (set) Token: 0x06001E6E RID: 7790 RVA: 0x0005BA82 File Offset: 0x00059C82
		[DataMember]
		public string MemberDepartRestriction
		{
			get
			{
				return (string)base[DistributionGroupSchema.MemberDepartRestriction];
			}
			set
			{
				base[DistributionGroupSchema.MemberDepartRestriction] = value;
			}
		}

		// Token: 0x170019C0 RID: 6592
		// (get) Token: 0x06001E6F RID: 7791 RVA: 0x0005BA90 File Offset: 0x00059C90
		// (set) Token: 0x06001E70 RID: 7792 RVA: 0x0005BAAC File Offset: 0x00059CAC
		[DataMember]
		public bool RequireSenderAuthenticationEnabled
		{
			get
			{
				return (bool)(base["RequireSenderAuthenticationEnabled"] ?? false);
			}
			set
			{
				base["RequireSenderAuthenticationEnabled"] = value;
			}
		}

		// Token: 0x170019C1 RID: 6593
		// (get) Token: 0x06001E71 RID: 7793 RVA: 0x0005BABF File Offset: 0x00059CBF
		// (set) Token: 0x06001E72 RID: 7794 RVA: 0x0005BAD1 File Offset: 0x00059CD1
		[DataMember]
		public string MailTip
		{
			get
			{
				return (string)base["MailTip"];
			}
			set
			{
				base["MailTip"] = value;
			}
		}

		// Token: 0x170019C2 RID: 6594
		// (get) Token: 0x06001E73 RID: 7795 RVA: 0x0005BADF File Offset: 0x00059CDF
		// (set) Token: 0x06001E74 RID: 7796 RVA: 0x0005BB00 File Offset: 0x00059D00
		[DataMember]
		public bool HiddenFromAddressListsEnabled
		{
			get
			{
				return base[MailEnabledRecipientSchema.HiddenFromAddressListsEnabled] != null && (bool)base[MailEnabledRecipientSchema.HiddenFromAddressListsEnabled];
			}
			set
			{
				base[MailEnabledRecipientSchema.HiddenFromAddressListsEnabled] = value;
			}
		}
	}
}
