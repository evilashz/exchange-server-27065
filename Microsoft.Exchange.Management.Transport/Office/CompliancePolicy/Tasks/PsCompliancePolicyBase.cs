using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000C5 RID: 197
	[Serializable]
	public class PsCompliancePolicyBase : ADPresentationObject
	{
		// Token: 0x06000712 RID: 1810 RVA: 0x0001E406 File Offset: 0x0001C606
		public PsCompliancePolicyBase()
		{
			this.InitializeBindings();
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001E41F File Offset: 0x0001C61F
		public PsCompliancePolicyBase(PolicyStorage policyStorage) : base(policyStorage)
		{
			this.InitializeBindings();
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x0001E439 File Offset: 0x0001C639
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return PsCompliancePolicyBase.schema;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0001E440 File Offset: 0x0001C640
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x0001E448 File Offset: 0x0001C648
		internal IList<BindingStorage> StorageBindings
		{
			get
			{
				return this.storageBindings;
			}
			set
			{
				this.storageBindings = value;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0001E451 File Offset: 0x0001C651
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x0001E459 File Offset: 0x0001C659
		public MultiValuedProperty<BindingMetadata> ExchangeBinding { get; set; }

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0001E462 File Offset: 0x0001C662
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x0001E46A File Offset: 0x0001C66A
		public MultiValuedProperty<BindingMetadata> SharePointBinding { get; set; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0001E473 File Offset: 0x0001C673
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x0001E47B File Offset: 0x0001C67B
		public MultiValuedProperty<BindingMetadata> OneDriveBinding { get; set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0001E484 File Offset: 0x0001C684
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x0001E496 File Offset: 0x0001C696
		public Workload Workload
		{
			get
			{
				return (Workload)this[PsCompliancePolicyBaseSchema.Workload];
			}
			set
			{
				this[PsCompliancePolicyBaseSchema.Workload] = value;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0001E4A9 File Offset: 0x0001C6A9
		public Guid ObjectVersion
		{
			get
			{
				return (Guid)this[PsCompliancePolicyBaseSchema.ObjectVersion];
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x0001E4BB File Offset: 0x0001C6BB
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x0001E4CD File Offset: 0x0001C6CD
		internal Guid MasterIdentity
		{
			get
			{
				return (Guid)this[PsComplianceRuleBaseSchema.MasterIdentity];
			}
			set
			{
				this[PsComplianceRuleBaseSchema.MasterIdentity] = value;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0001E4E0 File Offset: 0x0001C6E0
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x0001E4E8 File Offset: 0x0001C6E8
		public string CreatedBy { get; protected set; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x0001E4F1 File Offset: 0x0001C6F1
		// (set) Token: 0x06000725 RID: 1829 RVA: 0x0001E4F9 File Offset: 0x0001C6F9
		public string LastModifiedBy { get; protected set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x0001E502 File Offset: 0x0001C702
		// (set) Token: 0x06000727 RID: 1831 RVA: 0x0001E50A File Offset: 0x0001C70A
		public bool ReadOnly { get; internal set; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x0001E514 File Offset: 0x0001C714
		public string ExternalIdentity
		{
			get
			{
				if (this.MasterIdentity != Guid.Empty)
				{
					return this.MasterIdentity.ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x0001E54D File Offset: 0x0001C74D
		// (set) Token: 0x0600072A RID: 1834 RVA: 0x0001E55F File Offset: 0x0001C75F
		public string Comment
		{
			get
			{
				return (string)this[PsCompliancePolicyBaseSchema.Comment];
			}
			set
			{
				this[PsCompliancePolicyBaseSchema.Comment] = value;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x0001E56D File Offset: 0x0001C76D
		// (set) Token: 0x0600072C RID: 1836 RVA: 0x0001E57F File Offset: 0x0001C77F
		public bool Enabled
		{
			get
			{
				return (bool)this[PsCompliancePolicyBaseSchema.Enabled];
			}
			set
			{
				this[PsCompliancePolicyBaseSchema.Enabled] = value;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x0001E592 File Offset: 0x0001C792
		// (set) Token: 0x0600072E RID: 1838 RVA: 0x0001E5A4 File Offset: 0x0001C7A4
		public Mode Mode
		{
			get
			{
				return (Mode)this[PsCompliancePolicyBaseSchema.Mode];
			}
			set
			{
				this[PsCompliancePolicyBaseSchema.Mode] = value;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x0001E5B7 File Offset: 0x0001C7B7
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x0001E5BF File Offset: 0x0001C7BF
		public PolicyApplyStatus DistributionStatus { get; set; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x0001E5C8 File Offset: 0x0001C7C8
		// (set) Token: 0x06000732 RID: 1842 RVA: 0x0001E5D0 File Offset: 0x0001C7D0
		public MultiValuedProperty<PolicyDistributionErrorDetails> DistributionResults { get; set; }

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x0001E5D9 File Offset: 0x0001C7D9
		// (set) Token: 0x06000734 RID: 1844 RVA: 0x0001E5E1 File Offset: 0x0001C7E1
		public DateTime? LastStatusUpdateTime { get; set; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x0001E5EA File Offset: 0x0001C7EA
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001E5F4 File Offset: 0x0001C7F4
		internal virtual void PopulateTaskProperties(Task task, IConfigurationSession configurationSession)
		{
			this.InitializeBindings();
			foreach (BindingStorage bindingStorage in this.StorageBindings)
			{
				switch (bindingStorage.Workload)
				{
				case Workload.Exchange:
					this.ExchangeBinding = Utils.GetScopesFromStorage(bindingStorage);
					break;
				case Workload.SharePoint:
				{
					MultiValuedProperty<BindingMetadata> scopesFromStorage = Utils.GetScopesFromStorage(bindingStorage);
					MultiValuedProperty<BindingMetadata> multiValuedProperty = new MultiValuedProperty<BindingMetadata>(PsCompliancePolicyBase.GetBindingsBySubWorkload(scopesFromStorage, Workload.SharePoint));
					multiValuedProperty.SetIsReadOnly(false, null);
					if (multiValuedProperty.Any<BindingMetadata>())
					{
						this.SharePointBinding = multiValuedProperty;
					}
					else
					{
						multiValuedProperty = new MultiValuedProperty<BindingMetadata>(PsCompliancePolicyBase.GetBindingsBySubWorkload(scopesFromStorage, Workload.OneDriveForBusiness));
						multiValuedProperty.SetIsReadOnly(false, null);
						if (multiValuedProperty.Any<BindingMetadata>())
						{
							this.OneDriveBinding = scopesFromStorage;
						}
					}
					break;
				}
				default:
					this.ReadOnly = true;
					this.ExchangeBinding.Clear();
					this.SharePointBinding.Clear();
					this.OneDriveBinding.Clear();
					break;
				}
			}
			PolicyStorage policyStorage = base.DataObject as PolicyStorage;
			ADUser userObjectByExternalDirectoryObjectId = Utils.GetUserObjectByExternalDirectoryObjectId(policyStorage.CreatedBy, configurationSession);
			ADUser userObjectByExternalDirectoryObjectId2 = Utils.GetUserObjectByExternalDirectoryObjectId(policyStorage.LastModifiedBy, configurationSession);
			this.CreatedBy = ((!Utils.ExecutingUserIsForestWideAdmin(task) && userObjectByExternalDirectoryObjectId != null) ? userObjectByExternalDirectoryObjectId.DisplayName : policyStorage.CreatedBy);
			this.LastModifiedBy = ((!Utils.ExecutingUserIsForestWideAdmin(task) && userObjectByExternalDirectoryObjectId2 != null) ? userObjectByExternalDirectoryObjectId2.DisplayName : policyStorage.LastModifiedBy);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001E788 File Offset: 0x0001C988
		internal static IList<BindingMetadata> GetBindingsBySubWorkload(MultiValuedProperty<BindingMetadata> bindings, Workload subWorkload)
		{
			return (from s in bindings
			where s.Workload == subWorkload
			select s).ToList<BindingMetadata>();
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001E7BC File Offset: 0x0001C9BC
		internal virtual void UpdateStorageProperties(Task task, IConfigurationSession configurationSession, bool isNewPolicy)
		{
			PolicyStorage policyStorage = base.DataObject as PolicyStorage;
			Guid universalIdentity = Utils.GetUniversalIdentity(policyStorage);
			if (!Utils.ExecutingUserIsForestWideAdmin(task))
			{
				ADObjectId objectId;
				task.TryGetExecutingUserId(out objectId);
				ADUser userObjectByObjectId = Utils.GetUserObjectByObjectId(objectId, configurationSession);
				if (userObjectByObjectId != null)
				{
					policyStorage.LastModifiedBy = userObjectByObjectId.ExternalDirectoryObjectId;
					if (isNewPolicy)
					{
						policyStorage.CreatedBy = userObjectByObjectId.ExternalDirectoryObjectId;
					}
				}
			}
			this.UpdateWorkloadStorageBinding(universalIdentity, Workload.Exchange, this.ExchangeBinding, new MulipleExBindingObjectDetectedException());
			this.UpdateSharepointStorageBinding(universalIdentity, Workload.SharePoint, this.SharePointBinding, new MulipleSpBindingObjectDetectedException());
			this.UpdateSharepointStorageBinding(universalIdentity, Workload.OneDriveForBusiness, this.OneDriveBinding, new MulipleSpBindingObjectDetectedException());
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001E84C File Offset: 0x0001CA4C
		internal void SuppressPiiData(PiiMap piiMap)
		{
			base.Name = (SuppressingPiiProperty.TryRedact(ADObjectSchema.Name, base.Name, piiMap) as string);
			if (this.ExchangeBinding != null)
			{
				foreach (BindingMetadata binding in this.ExchangeBinding)
				{
					Utils.RedactBinding(binding, false);
				}
			}
			if (this.SharePointBinding != null)
			{
				foreach (BindingMetadata binding2 in this.SharePointBinding)
				{
					Utils.RedactBinding(binding2, true);
				}
			}
			if (this.OneDriveBinding != null)
			{
				foreach (BindingMetadata binding3 in this.OneDriveBinding)
				{
					Utils.RedactBinding(binding3, true);
				}
			}
			if (this.DistributionResults != null)
			{
				foreach (PolicyDistributionErrorDetails policyDistributionErrorDetails in this.DistributionResults)
				{
					policyDistributionErrorDetails.Redact();
				}
			}
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0001E9CC File Offset: 0x0001CBCC
		private void UpdateWorkloadStorageBinding(Guid universalIdentity, Workload workload, MultiValuedProperty<BindingMetadata> scopes, Exception mulipleStorageObjectsException)
		{
			ExAssert.RetailAssert(workload != Workload.SharePoint, "UpdateWorkloadBinding called for Sharepoint workload.");
			if (this.StorageBindings.Count((BindingStorage x) => x.Workload == workload) > 1)
			{
				throw mulipleStorageObjectsException;
			}
			BindingStorage bindingStorage = this.StorageBindings.FirstOrDefault((BindingStorage x) => x.Workload == workload);
			if (bindingStorage == null && scopes.Any<BindingMetadata>())
			{
				bindingStorage = Utils.CreateNewBindingStorage(base.OrganizationalUnitRoot, workload, universalIdentity);
				this.StorageBindings.Add(bindingStorage);
			}
			if (bindingStorage != null)
			{
				Utils.PopulateScopeStorages(bindingStorage, scopes);
			}
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0001EA70 File Offset: 0x0001CC70
		private void UpdateSharepointStorageBinding(Guid universalIdentity, Workload subWorkload, MultiValuedProperty<BindingMetadata> scopes, Exception mulipleStorageObjectsException)
		{
			ExAssert.RetailAssert(subWorkload == Workload.SharePoint || subWorkload == Workload.OneDriveForBusiness, "UpdateSharepointStorageBinding called for non-Sharepoint workload.");
			if (this.StorageBindings.Count((BindingStorage x) => x.Workload == Workload.SharePoint) > 2)
			{
				throw mulipleStorageObjectsException;
			}
			BindingStorage bindingStorageForSubWorkload = this.GetBindingStorageForSubWorkload(this.StorageBindings, subWorkload, universalIdentity, scopes.Any<BindingMetadata>());
			if (bindingStorageForSubWorkload != null)
			{
				Utils.PopulateScopeStorages(bindingStorageForSubWorkload, scopes);
			}
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001EB20 File Offset: 0x0001CD20
		private BindingStorage GetBindingStorageForSubWorkload(IEnumerable<BindingStorage> bindingContainers, Workload subWorkload, Guid universalIdentity, bool createNewIfNotFound)
		{
			IEnumerable<BindingStorage> enumerable = from s in bindingContainers
			where s.Workload == Workload.SharePoint
			select s;
			BindingStorage bindingStorage3 = (from bindingStorage in enumerable
			where bindingStorage.Scopes.Any<string>()
			select bindingStorage).FirstOrDefault((BindingStorage bindingStorage) => BindingMetadata.FromStorage(bindingStorage.Scopes.First<string>()).Workload == subWorkload);
			if (bindingStorage3 != null)
			{
				return bindingStorage3;
			}
			foreach (BindingStorage bindingStorage2 in enumerable)
			{
				if (!bindingStorage2.Scopes.Any<string>())
				{
					bindingStorage3 = bindingStorage2;
					return bindingStorage3;
				}
			}
			if (createNewIfNotFound)
			{
				bindingStorage3 = Utils.CreateNewBindingStorage(base.OrganizationalUnitRoot, Workload.SharePoint, universalIdentity);
				this.StorageBindings.Add(bindingStorage3);
			}
			return bindingStorage3;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0001EC08 File Offset: 0x0001CE08
		private void InitializeBindings()
		{
			this.ExchangeBinding = new MultiValuedProperty<BindingMetadata>();
			this.SharePointBinding = new MultiValuedProperty<BindingMetadata>();
			this.OneDriveBinding = new MultiValuedProperty<BindingMetadata>();
		}

		// Token: 0x040002A3 RID: 675
		private static PsCompliancePolicyBaseSchema schema = ObjectSchema.GetInstance<PsCompliancePolicyBaseSchema>();

		// Token: 0x040002A4 RID: 676
		private IList<BindingStorage> storageBindings = new List<BindingStorage>();
	}
}
