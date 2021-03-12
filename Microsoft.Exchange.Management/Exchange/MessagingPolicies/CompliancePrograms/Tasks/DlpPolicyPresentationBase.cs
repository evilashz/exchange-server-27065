using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x02000950 RID: 2384
	[Serializable]
	public abstract class DlpPolicyPresentationBase : IConfigurable, IVersionable
	{
		// Token: 0x17001971 RID: 6513
		// (get) Token: 0x06005503 RID: 21763 RVA: 0x0015E9F7 File Offset: 0x0015CBF7
		// (set) Token: 0x06005504 RID: 21764 RVA: 0x0015E9FF File Offset: 0x0015CBFF
		protected LocalizedString ErrorText { get; set; }

		// Token: 0x17001972 RID: 6514
		// (get) Token: 0x06005505 RID: 21765 RVA: 0x0015EA08 File Offset: 0x0015CC08
		public ObjectId Identity
		{
			get
			{
				return this.adDlpPolicy.Identity;
			}
		}

		// Token: 0x17001973 RID: 6515
		// (get) Token: 0x06005506 RID: 21766 RVA: 0x0015EA15 File Offset: 0x0015CC15
		public string DistinguishedName
		{
			get
			{
				return this.adDlpPolicy.DistinguishedName;
			}
		}

		// Token: 0x17001974 RID: 6516
		// (get) Token: 0x06005507 RID: 21767 RVA: 0x0015EA22 File Offset: 0x0015CC22
		public Guid Guid
		{
			get
			{
				return this.adDlpPolicy.Guid;
			}
		}

		// Token: 0x17001975 RID: 6517
		// (get) Token: 0x06005508 RID: 21768 RVA: 0x0015EA2F File Offset: 0x0015CC2F
		public OrganizationId OrganizationId
		{
			get
			{
				return this.adDlpPolicy.OrganizationId;
			}
		}

		// Token: 0x17001976 RID: 6518
		// (get) Token: 0x06005509 RID: 21769 RVA: 0x0015EA3C File Offset: 0x0015CC3C
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
		}

		// Token: 0x17001977 RID: 6519
		// (get) Token: 0x0600550A RID: 21770 RVA: 0x0015EA44 File Offset: 0x0015CC44
		public DateTime? WhenChanged
		{
			get
			{
				return this.adDlpPolicy.WhenChanged;
			}
		}

		// Token: 0x17001978 RID: 6520
		// (get) Token: 0x0600550B RID: 21771 RVA: 0x0015EA51 File Offset: 0x0015CC51
		public ExchangeObjectVersion ExchangeVersion
		{
			get
			{
				return this.adDlpPolicy.ExchangeVersion;
			}
		}

		// Token: 0x17001979 RID: 6521
		// (get) Token: 0x0600550C RID: 21772 RVA: 0x0015EA5E File Offset: 0x0015CC5E
		public ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return this.adDlpPolicy.MaximumSupportedExchangeObjectVersion;
			}
		}

		// Token: 0x1700197A RID: 6522
		// (get) Token: 0x0600550D RID: 21773 RVA: 0x0015EA6B File Offset: 0x0015CC6B
		public bool IsReadOnly
		{
			get
			{
				return this.adDlpPolicy.IsReadOnly;
			}
		}

		// Token: 0x1700197B RID: 6523
		// (get) Token: 0x0600550E RID: 21774 RVA: 0x0015EA78 File Offset: 0x0015CC78
		ObjectSchema IVersionable.ObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<DlpPolicyTemplateSchema>();
			}
		}

		// Token: 0x1700197C RID: 6524
		// (get) Token: 0x0600550F RID: 21775 RVA: 0x0015EA7F File Offset: 0x0015CC7F
		// (set) Token: 0x06005510 RID: 21776 RVA: 0x0015EA87 File Offset: 0x0015CC87
		protected ADComplianceProgram AdDlpPolicy
		{
			get
			{
				return this.adDlpPolicy;
			}
			set
			{
				this.adDlpPolicy = value;
			}
		}

		// Token: 0x1700197D RID: 6525
		// (get) Token: 0x06005511 RID: 21777 RVA: 0x0015EA90 File Offset: 0x0015CC90
		ObjectState IConfigurable.ObjectState
		{
			get
			{
				return ObjectState.Unchanged;
			}
		}

		// Token: 0x06005512 RID: 21778 RVA: 0x0015EA93 File Offset: 0x0015CC93
		bool IVersionable.IsPropertyAccessible(PropertyDefinition propertyDefinition)
		{
			return this.AdDlpPolicy.IsPropertyAccessible(propertyDefinition);
		}

		// Token: 0x1700197E RID: 6526
		// (get) Token: 0x06005513 RID: 21779 RVA: 0x0015EAA1 File Offset: 0x0015CCA1
		bool IVersionable.ExchangeVersionUpgradeSupported
		{
			get
			{
				return this.AdDlpPolicy.ExchangeVersionUpgradeSupported;
			}
		}

		// Token: 0x06005514 RID: 21780 RVA: 0x0015EAAE File Offset: 0x0015CCAE
		protected DlpPolicyPresentationBase(ADComplianceProgram adDlpPolicy)
		{
			this.adDlpPolicy = adDlpPolicy;
		}

		// Token: 0x06005515 RID: 21781 RVA: 0x0015EAC4 File Offset: 0x0015CCC4
		void IConfigurable.CopyChangesFrom(IConfigurable source)
		{
		}

		// Token: 0x06005516 RID: 21782 RVA: 0x0015EAC6 File Offset: 0x0015CCC6
		void IConfigurable.ResetChangeTracking()
		{
		}

		// Token: 0x06005517 RID: 21783 RVA: 0x0015EAC8 File Offset: 0x0015CCC8
		public virtual ValidationError[] Validate()
		{
			if (!this.isValid)
			{
				return new ValidationError[]
				{
					new ObjectValidationError(this.ErrorText, this.Identity, null)
				};
			}
			return ValidationError.None;
		}

		// Token: 0x06005518 RID: 21784 RVA: 0x0015EB00 File Offset: 0x0015CD00
		internal virtual void SuppressPiiData(PiiMap piiMap)
		{
			this.adDlpPolicy.DistinguishedName = (SuppressingPiiProperty.TryRedact(DlpPolicySchemaBase.DistinguishedName, this.adDlpPolicy.DistinguishedName, piiMap) as string);
		}

		// Token: 0x0400313A RID: 12602
		protected bool isValid = true;

		// Token: 0x0400313B RID: 12603
		private ADComplianceProgram adDlpPolicy;
	}
}
