using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Reflection;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000724 RID: 1828
	[Serializable]
	public class User : OrgPersonPresentationObject
	{
		// Token: 0x17001CF9 RID: 7417
		// (get) Token: 0x0600567F RID: 22143 RVA: 0x00137C01 File Offset: 0x00135E01
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return User.schema;
			}
		}

		// Token: 0x06005680 RID: 22144 RVA: 0x00137C08 File Offset: 0x00135E08
		public User()
		{
			base.SetObjectClass("user");
		}

		// Token: 0x06005681 RID: 22145 RVA: 0x00137C1B File Offset: 0x00135E1B
		public User(ADUser dataObject) : base(dataObject)
		{
		}

		// Token: 0x06005682 RID: 22146 RVA: 0x00137C24 File Offset: 0x00135E24
		internal static User FromDataObject(ADUser dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new User(dataObject);
		}

		// Token: 0x17001CFA RID: 7418
		// (get) Token: 0x06005683 RID: 22147 RVA: 0x00137C31 File Offset: 0x00135E31
		protected override IEnumerable<PropertyInfo> CloneableProperties
		{
			get
			{
				IEnumerable<PropertyInfo> result;
				if ((result = User.cloneableProps) == null)
				{
					result = (User.cloneableProps = ADPresentationObject.GetCloneableProperties(this));
				}
				return result;
			}
		}

		// Token: 0x17001CFB RID: 7419
		// (get) Token: 0x06005684 RID: 22148 RVA: 0x00137C48 File Offset: 0x00135E48
		protected override IEnumerable<PropertyInfo> CloneableOnceProperties
		{
			get
			{
				IEnumerable<PropertyInfo> result;
				if ((result = User.cloneableOnceProps) == null)
				{
					result = (User.cloneableOnceProps = ADPresentationObject.GetCloneableOnceProperties(this));
				}
				return result;
			}
		}

		// Token: 0x17001CFC RID: 7420
		// (get) Token: 0x06005685 RID: 22149 RVA: 0x00137C5F File Offset: 0x00135E5F
		protected override IEnumerable<PropertyInfo> CloneableEnabledStateProperties
		{
			get
			{
				IEnumerable<PropertyInfo> result;
				if ((result = User.cloneableEnabledStateProps) == null)
				{
					result = (User.cloneableEnabledStateProps = ADPresentationObject.GetCloneableEnabledStateProperties(this));
				}
				return result;
			}
		}

		// Token: 0x17001CFD RID: 7421
		// (get) Token: 0x06005686 RID: 22150 RVA: 0x00137C76 File Offset: 0x00135E76
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ADRecipient.PublicFolderMailboxObjectVersion;
			}
		}

		// Token: 0x17001CFE RID: 7422
		// (get) Token: 0x06005687 RID: 22151 RVA: 0x00137C7D File Offset: 0x00135E7D
		public bool IsSecurityPrincipal
		{
			get
			{
				return (bool)this[UserSchema.IsSecurityPrincipal];
			}
		}

		// Token: 0x17001CFF RID: 7423
		// (get) Token: 0x06005688 RID: 22152 RVA: 0x00137C8F File Offset: 0x00135E8F
		// (set) Token: 0x06005689 RID: 22153 RVA: 0x00137CA1 File Offset: 0x00135EA1
		[Parameter(Mandatory = false)]
		public string SamAccountName
		{
			get
			{
				return (string)this[UserSchema.SamAccountName];
			}
			set
			{
				this[UserSchema.SamAccountName] = value;
			}
		}

		// Token: 0x17001D00 RID: 7424
		// (get) Token: 0x0600568A RID: 22154 RVA: 0x00137CAF File Offset: 0x00135EAF
		public SecurityIdentifier Sid
		{
			get
			{
				return (SecurityIdentifier)this[UserSchema.Sid];
			}
		}

		// Token: 0x17001D01 RID: 7425
		// (get) Token: 0x0600568B RID: 22155 RVA: 0x00137CC1 File Offset: 0x00135EC1
		public MultiValuedProperty<SecurityIdentifier> SidHistory
		{
			get
			{
				return (MultiValuedProperty<SecurityIdentifier>)this[UserSchema.SidHistory];
			}
		}

		// Token: 0x17001D02 RID: 7426
		// (get) Token: 0x0600568C RID: 22156 RVA: 0x00137CD3 File Offset: 0x00135ED3
		// (set) Token: 0x0600568D RID: 22157 RVA: 0x00137CE5 File Offset: 0x00135EE5
		[Parameter(Mandatory = false)]
		public string UserPrincipalName
		{
			get
			{
				return (string)this[UserSchema.UserPrincipalName];
			}
			set
			{
				this[UserSchema.UserPrincipalName] = value;
			}
		}

		// Token: 0x17001D03 RID: 7427
		// (get) Token: 0x0600568E RID: 22158 RVA: 0x00137CF3 File Offset: 0x00135EF3
		// (set) Token: 0x0600568F RID: 22159 RVA: 0x00137D05 File Offset: 0x00135F05
		[Parameter(Mandatory = false)]
		[ProvisionalCloneOnce(CloneSet.CloneLimitedSet)]
		public bool ResetPasswordOnNextLogon
		{
			get
			{
				return (bool)this[UserSchema.ResetPasswordOnNextLogon];
			}
			set
			{
				this[UserSchema.ResetPasswordOnNextLogon] = value;
			}
		}

		// Token: 0x17001D04 RID: 7428
		// (get) Token: 0x06005690 RID: 22160 RVA: 0x00137D18 File Offset: 0x00135F18
		// (set) Token: 0x06005691 RID: 22161 RVA: 0x00137D2A File Offset: 0x00135F2A
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<X509Identifier> CertificateSubject
		{
			get
			{
				return (MultiValuedProperty<X509Identifier>)this[ADUserSchema.CertificateSubject];
			}
			set
			{
				this[ADUserSchema.CertificateSubject] = value;
			}
		}

		// Token: 0x17001D05 RID: 7429
		// (get) Token: 0x06005692 RID: 22162 RVA: 0x00137D38 File Offset: 0x00135F38
		// (set) Token: 0x06005693 RID: 22163 RVA: 0x00137D4A File Offset: 0x00135F4A
		[Parameter(Mandatory = false)]
		public bool RemotePowerShellEnabled
		{
			get
			{
				return (bool)this[ADRecipientSchema.RemotePowerShellEnabled];
			}
			set
			{
				this[ADRecipientSchema.RemotePowerShellEnabled] = value;
			}
		}

		// Token: 0x17001D06 RID: 7430
		// (get) Token: 0x06005694 RID: 22164 RVA: 0x00137D5D File Offset: 0x00135F5D
		// (set) Token: 0x06005695 RID: 22165 RVA: 0x00137D6F File Offset: 0x00135F6F
		[Parameter(Mandatory = false)]
		public SmtpAddress WindowsLiveID
		{
			get
			{
				return (SmtpAddress)this[ADRecipientSchema.WindowsLiveID];
			}
			set
			{
				this[ADRecipientSchema.WindowsLiveID] = value;
			}
		}

		// Token: 0x17001D07 RID: 7431
		// (get) Token: 0x06005696 RID: 22166 RVA: 0x00137D82 File Offset: 0x00135F82
		// (set) Token: 0x06005697 RID: 22167 RVA: 0x00137D8A File Offset: 0x00135F8A
		[Parameter(Mandatory = false)]
		public SmtpAddress MicrosoftOnlineServicesID
		{
			get
			{
				return this.WindowsLiveID;
			}
			set
			{
				this.WindowsLiveID = value;
			}
		}

		// Token: 0x17001D08 RID: 7432
		// (get) Token: 0x06005698 RID: 22168 RVA: 0x00137D93 File Offset: 0x00135F93
		// (set) Token: 0x06005699 RID: 22169 RVA: 0x00137DA5 File Offset: 0x00135FA5
		[Parameter(Mandatory = false)]
		public NetID NetID
		{
			get
			{
				return (NetID)this[ADUserSchema.NetID];
			}
			set
			{
				this[ADUserSchema.NetID] = value;
			}
		}

		// Token: 0x17001D09 RID: 7433
		// (get) Token: 0x0600569A RID: 22170 RVA: 0x00137DB3 File Offset: 0x00135FB3
		// (set) Token: 0x0600569B RID: 22171 RVA: 0x00137DC5 File Offset: 0x00135FC5
		public NetID ConsumerNetID
		{
			get
			{
				return (NetID)this[ADUserSchema.ConsumerNetID];
			}
			set
			{
				this[ADUserSchema.ConsumerNetID] = value;
			}
		}

		// Token: 0x17001D0A RID: 7434
		// (get) Token: 0x0600569C RID: 22172 RVA: 0x00137DD3 File Offset: 0x00135FD3
		// (set) Token: 0x0600569D RID: 22173 RVA: 0x00137DE5 File Offset: 0x00135FE5
		public bool LEOEnabled
		{
			get
			{
				return (bool)this[ADRecipientSchema.LEOEnabled];
			}
			set
			{
				this[ADRecipientSchema.LEOEnabled] = value;
			}
		}

		// Token: 0x17001D0B RID: 7435
		// (get) Token: 0x0600569E RID: 22174 RVA: 0x00137DF8 File Offset: 0x00135FF8
		public UserAccountControlFlags UserAccountControl
		{
			get
			{
				return (UserAccountControlFlags)this[ADUserSchema.UserAccountControl];
			}
		}

		// Token: 0x17001D0C RID: 7436
		// (get) Token: 0x0600569F RID: 22175 RVA: 0x00137E0A File Offset: 0x0013600A
		public string OrganizationalUnit
		{
			get
			{
				return (string)this[UserSchema.OrganizationalUnit];
			}
		}

		// Token: 0x17001D0D RID: 7437
		// (get) Token: 0x060056A0 RID: 22176 RVA: 0x00137E1C File Offset: 0x0013601C
		public bool IsLinked
		{
			get
			{
				return (bool)this[UserSchema.IsLinked];
			}
		}

		// Token: 0x17001D0E RID: 7438
		// (get) Token: 0x060056A1 RID: 22177 RVA: 0x00137E2E File Offset: 0x0013602E
		// (set) Token: 0x060056A2 RID: 22178 RVA: 0x00137E40 File Offset: 0x00136040
		public string LinkedMasterAccount
		{
			get
			{
				return (string)this[UserSchema.LinkedMasterAccount];
			}
			internal set
			{
				this[UserSchema.LinkedMasterAccount] = value;
			}
		}

		// Token: 0x17001D0F RID: 7439
		// (get) Token: 0x060056A3 RID: 22179 RVA: 0x00137E4E File Offset: 0x0013604E
		public string ExternalDirectoryObjectId
		{
			get
			{
				return (string)this[UserSchema.ExternalDirectoryObjectId];
			}
		}

		// Token: 0x17001D10 RID: 7440
		// (get) Token: 0x060056A4 RID: 22180 RVA: 0x00137E60 File Offset: 0x00136060
		[Parameter(Mandatory = false)]
		public bool? SKUAssigned
		{
			get
			{
				return (bool?)this[UserSchema.SKUAssigned];
			}
		}

		// Token: 0x17001D11 RID: 7441
		// (get) Token: 0x060056A5 RID: 22181 RVA: 0x00137E72 File Offset: 0x00136072
		// (set) Token: 0x060056A6 RID: 22182 RVA: 0x00137E84 File Offset: 0x00136084
		public bool IsSoftDeletedByRemove
		{
			get
			{
				return (bool)this[MailboxSchema.IsSoftDeletedByRemove];
			}
			set
			{
				this[MailboxSchema.IsSoftDeletedByRemove] = value;
			}
		}

		// Token: 0x17001D12 RID: 7442
		// (get) Token: 0x060056A7 RID: 22183 RVA: 0x00137E97 File Offset: 0x00136097
		// (set) Token: 0x060056A8 RID: 22184 RVA: 0x00137EA9 File Offset: 0x001360A9
		public bool IsSoftDeletedByDisable
		{
			get
			{
				return (bool)this[MailboxSchema.IsSoftDeletedByDisable];
			}
			set
			{
				this[MailboxSchema.IsSoftDeletedByDisable] = value;
			}
		}

		// Token: 0x17001D13 RID: 7443
		// (get) Token: 0x060056A9 RID: 22185 RVA: 0x00137EBC File Offset: 0x001360BC
		// (set) Token: 0x060056AA RID: 22186 RVA: 0x00137ECE File Offset: 0x001360CE
		public DateTime? WhenSoftDeleted
		{
			get
			{
				return (DateTime?)this[MailboxSchema.WhenSoftDeleted];
			}
			set
			{
				this[MailboxSchema.WhenSoftDeleted] = value;
			}
		}

		// Token: 0x17001D14 RID: 7444
		// (get) Token: 0x060056AB RID: 22187 RVA: 0x00137EE1 File Offset: 0x001360E1
		public RecipientTypeDetails PreviousRecipientTypeDetails
		{
			get
			{
				return (RecipientTypeDetails)this[UserSchema.PreviousRecipientTypeDetails];
			}
		}

		// Token: 0x17001D15 RID: 7445
		// (get) Token: 0x060056AC RID: 22188 RVA: 0x00137EF3 File Offset: 0x001360F3
		// (set) Token: 0x060056AD RID: 22189 RVA: 0x00137F05 File Offset: 0x00136105
		[Parameter(Mandatory = false)]
		public UpgradeRequestTypes UpgradeRequest
		{
			get
			{
				return (UpgradeRequestTypes)this[UserSchema.UpgradeRequest];
			}
			set
			{
				this[UserSchema.UpgradeRequest] = value;
			}
		}

		// Token: 0x17001D16 RID: 7446
		// (get) Token: 0x060056AE RID: 22190 RVA: 0x00137F18 File Offset: 0x00136118
		// (set) Token: 0x060056AF RID: 22191 RVA: 0x00137F2A File Offset: 0x0013612A
		[Parameter(Mandatory = false)]
		public UpgradeStatusTypes UpgradeStatus
		{
			get
			{
				return (UpgradeStatusTypes)this[UserSchema.UpgradeStatus];
			}
			set
			{
				this[UserSchema.UpgradeStatus] = value;
			}
		}

		// Token: 0x17001D17 RID: 7447
		// (get) Token: 0x060056B0 RID: 22192 RVA: 0x00137F3D File Offset: 0x0013613D
		public string UpgradeDetails
		{
			get
			{
				return (string)this[UserSchema.UpgradeDetails];
			}
		}

		// Token: 0x17001D18 RID: 7448
		// (get) Token: 0x060056B1 RID: 22193 RVA: 0x00137F4F File Offset: 0x0013614F
		public string UpgradeMessage
		{
			get
			{
				return (string)this[UserSchema.UpgradeMessage];
			}
		}

		// Token: 0x17001D19 RID: 7449
		// (get) Token: 0x060056B2 RID: 22194 RVA: 0x00137F61 File Offset: 0x00136161
		public UpgradeStage? UpgradeStage
		{
			get
			{
				return (UpgradeStage?)this[UserSchema.UpgradeStage];
			}
		}

		// Token: 0x17001D1A RID: 7450
		// (get) Token: 0x060056B3 RID: 22195 RVA: 0x00137F73 File Offset: 0x00136173
		public DateTime? UpgradeStageTimeStamp
		{
			get
			{
				return (DateTime?)this[UserSchema.UpgradeStageTimeStamp];
			}
		}

		// Token: 0x17001D1B RID: 7451
		// (get) Token: 0x060056B4 RID: 22196 RVA: 0x00137F85 File Offset: 0x00136185
		public MailboxProvisioningConstraint MailboxProvisioningConstraint
		{
			get
			{
				return (MailboxProvisioningConstraint)this[UserSchema.MailboxProvisioningConstraint];
			}
		}

		// Token: 0x17001D1C RID: 7452
		// (get) Token: 0x060056B5 RID: 22197 RVA: 0x00137F97 File Offset: 0x00136197
		public MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
		{
			get
			{
				return (MultiValuedProperty<MailboxProvisioningConstraint>)this[UserSchema.MailboxProvisioningPreferences];
			}
		}

		// Token: 0x17001D1D RID: 7453
		// (get) Token: 0x060056B6 RID: 22198 RVA: 0x00137FA9 File Offset: 0x001361A9
		// (set) Token: 0x060056B7 RID: 22199 RVA: 0x00137FBB File Offset: 0x001361BB
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> InPlaceHoldsRaw
		{
			get
			{
				return (MultiValuedProperty<string>)this[UserSchema.InPlaceHoldsRaw];
			}
			set
			{
				this[UserSchema.InPlaceHoldsRaw] = value;
			}
		}

		// Token: 0x17001D1E RID: 7454
		// (get) Token: 0x060056B8 RID: 22200 RVA: 0x00137FCC File Offset: 0x001361CC
		// (set) Token: 0x060056B9 RID: 22201 RVA: 0x00137FF6 File Offset: 0x001361F6
		public MailboxRelease MailboxRelease
		{
			get
			{
				MailboxRelease result;
				if (!Enum.TryParse<MailboxRelease>((string)this[UserSchema.MailboxRelease], true, out result))
				{
					return MailboxRelease.None;
				}
				return result;
			}
			set
			{
				this[UserSchema.MailboxRelease] = value.ToString();
			}
		}

		// Token: 0x17001D1F RID: 7455
		// (get) Token: 0x060056BA RID: 22202 RVA: 0x00138010 File Offset: 0x00136210
		// (set) Token: 0x060056BB RID: 22203 RVA: 0x0013803A File Offset: 0x0013623A
		public MailboxRelease ArchiveRelease
		{
			get
			{
				MailboxRelease result;
				if (!Enum.TryParse<MailboxRelease>((string)this[UserSchema.ArchiveRelease], true, out result))
				{
					return MailboxRelease.None;
				}
				return result;
			}
			set
			{
				this[UserSchema.ArchiveRelease] = value.ToString();
			}
		}

		// Token: 0x04003AA0 RID: 15008
		private static UserSchema schema = ObjectSchema.GetInstance<UserSchema>();

		// Token: 0x04003AA1 RID: 15009
		private static IEnumerable<PropertyInfo> cloneableProps;

		// Token: 0x04003AA2 RID: 15010
		private static IEnumerable<PropertyInfo> cloneableOnceProps;

		// Token: 0x04003AA3 RID: 15011
		private static IEnumerable<PropertyInfo> cloneableEnabledStateProps;
	}
}
