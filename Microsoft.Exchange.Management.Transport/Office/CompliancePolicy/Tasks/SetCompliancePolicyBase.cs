using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Data.Storage.UnifiedPolicy;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;
using Microsoft.Office.CompliancePolicy.PolicySync;
using Microsoft.Office.CompliancePolicy.Validators;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000F0 RID: 240
	[Cmdlet("Set", "CompliancePolicy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public abstract class SetCompliancePolicyBase : SetSystemConfigurationObjectTask<PolicyIdParameter, PsCompliancePolicyBase, PolicyStorage>
	{
		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x000276B7 File Offset: 0x000258B7
		// (set) Token: 0x060009B0 RID: 2480 RVA: 0x000276BF File Offset: 0x000258BF
		private protected PolicyScenario Scenario { protected get; private set; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x000276C8 File Offset: 0x000258C8
		protected ExecutionLog ExecutionLogger
		{
			get
			{
				return this.executionLogger;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x000276D0 File Offset: 0x000258D0
		// (set) Token: 0x060009B3 RID: 2483 RVA: 0x000276D8 File Offset: 0x000258D8
		protected PsCompliancePolicyBase PsPolicyPresentationObject { get; set; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x000276E1 File Offset: 0x000258E1
		// (set) Token: 0x060009B5 RID: 2485 RVA: 0x000276E9 File Offset: 0x000258E9
		protected bool IsRetryDistributionAllowed { get; set; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x000276F2 File Offset: 0x000258F2
		// (set) Token: 0x060009B7 RID: 2487 RVA: 0x00027709 File Offset: 0x00025909
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "RetryDistributionParameterSet", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public override PolicyIdParameter Identity
		{
			get
			{
				return (PolicyIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x0002771C File Offset: 0x0002591C
		// (set) Token: 0x060009B9 RID: 2489 RVA: 0x00027733 File Offset: 0x00025933
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public string Comment
		{
			get
			{
				return (string)base.Fields["Comment"];
			}
			set
			{
				base.Fields["Comment"] = value;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x00027746 File Offset: 0x00025946
		// (set) Token: 0x060009BB RID: 2491 RVA: 0x0002775D File Offset: 0x0002595D
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public bool Enabled
		{
			get
			{
				return (bool)base.Fields["Enabled"];
			}
			set
			{
				base.Fields["Enabled"] = value;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x00027775 File Offset: 0x00025975
		// (set) Token: 0x060009BD RID: 2493 RVA: 0x0002778C File Offset: 0x0002598C
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public MultiValuedProperty<string> AddExchangeBinding
		{
			get
			{
				return Utils.BindingParameterGetter(base.Fields["AddExchangeBinding"]);
			}
			set
			{
				base.Fields["AddExchangeBinding"] = Utils.BindingParameterSetter(value);
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x000277A4 File Offset: 0x000259A4
		// (set) Token: 0x060009BF RID: 2495 RVA: 0x000277BB File Offset: 0x000259BB
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public MultiValuedProperty<string> RemoveExchangeBinding
		{
			get
			{
				return Utils.BindingParameterGetter(base.Fields["RemoveExchangeBinding"]);
			}
			set
			{
				base.Fields["RemoveExchangeBinding"] = Utils.BindingParameterSetter(value);
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x000277D3 File Offset: 0x000259D3
		// (set) Token: 0x060009C1 RID: 2497 RVA: 0x000277EA File Offset: 0x000259EA
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public MultiValuedProperty<string> AddSharePointBinding
		{
			get
			{
				return Utils.BindingParameterGetter(base.Fields["AddSharePointBinding"]);
			}
			set
			{
				base.Fields["AddSharePointBinding"] = Utils.BindingParameterSetter(value);
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x00027802 File Offset: 0x00025A02
		// (set) Token: 0x060009C3 RID: 2499 RVA: 0x00027819 File Offset: 0x00025A19
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public MultiValuedProperty<string> RemoveSharePointBinding
		{
			get
			{
				return Utils.BindingParameterGetter(base.Fields["RemoveSharePointBinding"]);
			}
			set
			{
				base.Fields["RemoveSharePointBinding"] = Utils.BindingParameterSetter(value);
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x00027831 File Offset: 0x00025A31
		// (set) Token: 0x060009C5 RID: 2501 RVA: 0x00027848 File Offset: 0x00025A48
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public MultiValuedProperty<string> AddOneDriveBinding
		{
			get
			{
				return Utils.BindingParameterGetter(base.Fields["AddOneDriveBinding"]);
			}
			set
			{
				base.Fields["AddOneDriveBinding"] = Utils.BindingParameterSetter(value);
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x00027860 File Offset: 0x00025A60
		// (set) Token: 0x060009C7 RID: 2503 RVA: 0x00027877 File Offset: 0x00025A77
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public MultiValuedProperty<string> RemoveOneDriveBinding
		{
			get
			{
				return Utils.BindingParameterGetter(base.Fields["RemoveOneDriveBinding"]);
			}
			set
			{
				base.Fields["RemoveOneDriveBinding"] = Utils.BindingParameterSetter(value);
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x0002788F File Offset: 0x00025A8F
		// (set) Token: 0x060009C9 RID: 2505 RVA: 0x000278B5 File Offset: 0x00025AB5
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x000278CD File Offset: 0x00025ACD
		// (set) Token: 0x060009CB RID: 2507 RVA: 0x000278F3 File Offset: 0x00025AF3
		[Parameter(Mandatory = true, ValueFromPipeline = false, ParameterSetName = "RetryDistributionParameterSet")]
		public SwitchParameter RetryDistribution
		{
			get
			{
				return (SwitchParameter)(base.Fields["RetryDistribution"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["RetryDistribution"] = value;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x0002790B File Offset: 0x00025B0B
		protected override ObjectId RootId
		{
			get
			{
				return Utils.GetRootId(base.DataSession);
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x00027918 File Offset: 0x00025B18
		// (set) Token: 0x060009CE RID: 2510 RVA: 0x00027920 File Offset: 0x00025B20
		private protected MultiValuedProperty<BindingMetadata> InternalAddExchangeBindings { protected get; private set; }

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x00027929 File Offset: 0x00025B29
		// (set) Token: 0x060009D0 RID: 2512 RVA: 0x00027931 File Offset: 0x00025B31
		private protected MultiValuedProperty<BindingMetadata> InternalRemoveExchangeBindings { protected get; private set; }

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x0002793A File Offset: 0x00025B3A
		// (set) Token: 0x060009D2 RID: 2514 RVA: 0x00027942 File Offset: 0x00025B42
		private protected MultiValuedProperty<BindingMetadata> InternalAddSharePointBindings { protected get; private set; }

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x0002794B File Offset: 0x00025B4B
		// (set) Token: 0x060009D4 RID: 2516 RVA: 0x00027953 File Offset: 0x00025B53
		private protected MultiValuedProperty<BindingMetadata> InternalRemoveSharePointBindings { protected get; private set; }

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x0002795C File Offset: 0x00025B5C
		// (set) Token: 0x060009D6 RID: 2518 RVA: 0x00027964 File Offset: 0x00025B64
		private protected MultiValuedProperty<BindingMetadata> InternalAddOneDriveBindings { protected get; private set; }

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x0002796D File Offset: 0x00025B6D
		// (set) Token: 0x060009D8 RID: 2520 RVA: 0x00027975 File Offset: 0x00025B75
		private protected MultiValuedProperty<BindingMetadata> InternalRemoveOneDriveBindings { protected get; private set; }

		// Token: 0x060009D9 RID: 2521 RVA: 0x0002797E File Offset: 0x00025B7E
		protected override IConfigDataProvider CreateSession()
		{
			return PolicyConfigProviderManager<ExPolicyConfigProviderManager>.Instance.CreateForCmdlet(base.CreateSession() as IConfigurationSession, this.executionLogger);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0002799B File Offset: 0x00025B9B
		protected override bool ShouldSupportPreResolveOrgIdBasedOnIdentity()
		{
			return true;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x000279AC File Offset: 0x00025BAC
		protected override bool IsObjectStateChanged()
		{
			if (!base.IsObjectStateChanged())
			{
				return this.PsPolicyPresentationObject.StorageBindings.Any((BindingStorage binding) => binding.ObjectState != ObjectState.Unchanged);
			}
			return true;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x000279E5 File Offset: 0x00025BE5
		protected SetCompliancePolicyBase(PolicyScenario scenario)
		{
			this.Scenario = scenario;
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00027A40 File Offset: 0x00025C40
		protected override void InternalValidate()
		{
			Utils.ThrowIfNotRunInEOP();
			Utils.ValidateNotForestWideOrganization(base.CurrentOrganizationId);
			base.InternalValidate();
			if (this.DataObject.IsModified(ADObjectSchema.Name) && Utils.LoadPolicyStorages(base.DataSession, this.DataObject.Scenario).Any((PolicyStorage p) => p.Name.Equals((string)this.DataObject[ADObjectSchema.Name], StringComparison.InvariantCultureIgnoreCase)))
			{
				throw new CompliancePolicyAlreadyExistsException((string)this.DataObject[ADObjectSchema.Name]);
			}
			if (base.Fields.IsModified("Enabled") && this.Enabled)
			{
				IEnumerable<RuleStorage> enumerable = Utils.LoadRuleStoragesByPolicy(base.DataSession, this.DataObject, Utils.GetRootId(base.DataSession));
				foreach (RuleStorage ruleStorage in enumerable)
				{
					base.WriteVerbose(Strings.VerboseLoadRuleStorageObjectsForPolicy(ruleStorage.ToString(), this.DataObject.ToString()));
				}
				if (enumerable.Any((RuleStorage x) => !x.IsEnabled))
				{
					this.WriteWarning(Strings.WarningPolicyContainsDisabledRules(this.DataObject.Name));
				}
			}
			if (!this.RetryDistribution)
			{
				this.ValidateBindingParameter();
				return;
			}
			List<PolicyDistributionErrorDetails> source;
			DateTime? dateTime;
			PolicySettingStatusHelpers.GetPolicyDistributionStatus(this.DataObject, Utils.LoadBindingStoragesByPolicy(base.DataSession, this.DataObject), base.DataSession, out source, out dateTime);
			if (!source.Any((PolicyDistributionErrorDetails error) => error.ResultCode != UnifiedPolicyErrorCode.PolicySyncTimeout))
			{
				this.WriteWarning(Strings.ErrorCompliancePolicyHasNoObjectsToRetry(this.DataObject.Name));
				this.IsRetryDistributionAllowed = false;
				return;
			}
			this.IsRetryDistributionAllowed = true;
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00027C1C File Offset: 0x00025E1C
		protected override IConfigurable ResolveDataObject()
		{
			PolicyStorage policyStorage = base.GetDataObjects<PolicyStorage>(this.Identity, base.DataSession, null).FirstOrDefault((PolicyStorage p) => p.Scenario == this.Scenario);
			if (policyStorage == null)
			{
				base.WriteError(new ErrorPolicyNotFoundException(this.Identity.ToString()), ErrorCategory.InvalidOperation, null);
			}
			return policyStorage;
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00027C6C File Offset: 0x00025E6C
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			PolicyStorage policyStorage = (PolicyStorage)dataObject;
			policyStorage.ResetChangeTracking(true);
			if (policyStorage.Mode == Mode.PendingDeletion && !this.RetryDistribution)
			{
				base.WriteError(new ErrorCompliancePolicyIsDeletedException(policyStorage.Name), ErrorCategory.InvalidOperation, null);
			}
			this.PsPolicyPresentationObject = new PsCompliancePolicyBase(policyStorage)
			{
				StorageBindings = Utils.LoadBindingStoragesByPolicy(base.DataSession, policyStorage)
			};
			foreach (BindingStorage bindingStorage in this.PsPolicyPresentationObject.StorageBindings)
			{
				base.WriteVerbose(Strings.VerboseLoadBindingStorageObjects(bindingStorage.ToString(), this.PsPolicyPresentationObject.Name));
			}
			this.PsPolicyPresentationObject.PopulateTaskProperties(this, base.DataSession as IConfigurationSession);
			if (this.PsPolicyPresentationObject.ReadOnly)
			{
				throw new TaskRuleIsTooAdvancedToModifyException(this.PsPolicyPresentationObject.Name);
			}
			base.StampChangesOn(dataObject);
			this.ValidateBindingParameterBeforeMerge();
			this.CopyExplicitParameters();
			this.PsPolicyPresentationObject.UpdateStorageProperties(this, base.DataSession as IConfigurationSession, false);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00027D8C File Offset: 0x00025F8C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.RetryDistribution)
			{
				this.RetryPolicyDistribution();
			}
			else
			{
				foreach (BindingStorage bindingStorage in this.PsPolicyPresentationObject.StorageBindings)
				{
					if (bindingStorage.AppliedScopes.Any<ScopeStorage>())
					{
						base.DataSession.Save(bindingStorage);
						base.WriteVerbose(Strings.VerboseSaveBindingStorageObjects(bindingStorage.ToString(), this.PsPolicyPresentationObject.Name));
					}
					else
					{
						base.DataSession.Delete(bindingStorage);
						base.WriteVerbose(Strings.VerboseDeleteBindingStorageObjects(bindingStorage.ToString(), this.PsPolicyPresentationObject.Name));
					}
				}
				IEnumerable<RuleStorage> enumerable = Utils.LoadRuleStoragesByPolicy(base.DataSession, this.DataObject, this.RootId);
				foreach (RuleStorage ruleStorage in enumerable)
				{
					base.WriteVerbose(Strings.VerboseLoadRuleStorageObjectsForPolicy(ruleStorage.ToString(), this.DataObject.ToString()));
				}
				Utils.ThrowIfRulesInPolicyAreTooAdvanced(enumerable, this.DataObject, this, base.DataSession as IConfigurationSession);
				base.InternalProcessRecord();
				PolicySettingStatusHelpers.CheckNotificationResultsAndUpdateStatus(this, (IConfigurationSession)base.DataSession, this.OnNotifyChanges());
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00027EF8 File Offset: 0x000260F8
		protected virtual IEnumerable<ChangeNotificationData> OnNotifyChanges()
		{
			return AggregatedNotificationClients.NotifyChanges(this, (IConfigurationSession)base.DataSession, this.DataObject, this.executionLogger, base.GetType(), this.PsPolicyPresentationObject.StorageBindings, null);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00027F40 File Offset: 0x00026140
		protected virtual string OnNotifyRetryDistributionChanges(List<ChangeNotificationData> itemsToSync, Workload workload, bool isObjectLevelSync)
		{
			string text;
			return AggregatedNotificationClients.NotifyChangesByWorkload(this, (IConfigurationSession)base.DataSession, workload, from ols in itemsToSync
			select ols.CreateSyncChangeInfo(isObjectLevelSync), false, false, this.executionLogger, base.GetType(), out text);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00027FA4 File Offset: 0x000261A4
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || Utils.KnownExceptions.Any((Type exceptionType) => exceptionType.IsInstanceOfType(exception));
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00027FE4 File Offset: 0x000261E4
		protected virtual void CopyExplicitParameters()
		{
			if (base.Fields.IsModified("Comment"))
			{
				this.PsPolicyPresentationObject.Comment = this.Comment;
			}
			if (base.Fields.IsModified("Enabled"))
			{
				this.PsPolicyPresentationObject.Enabled = this.Enabled;
			}
			Utils.MergeBindings(this.PsPolicyPresentationObject.ExchangeBinding, this.InternalAddExchangeBindings, this.InternalRemoveExchangeBindings, (this.PsPolicyPresentationObject.Workload & Workload.Exchange) != Workload.Exchange);
			if (this.PsPolicyPresentationObject.ExchangeBinding.Count<BindingMetadata>() > 1000)
			{
				throw new BindingCountExceedsLimitException("Exchange", 1000);
			}
			Utils.MergeBindings(this.PsPolicyPresentationObject.SharePointBinding, this.InternalAddSharePointBindings, this.InternalRemoveSharePointBindings, (this.PsPolicyPresentationObject.Workload & Workload.SharePoint) != Workload.SharePoint);
			if (this.PsPolicyPresentationObject.SharePointBinding.Count<BindingMetadata>() > 100)
			{
				throw new BindingCountExceedsLimitException("Sharepoint", 100);
			}
			Utils.MergeBindings(this.PsPolicyPresentationObject.OneDriveBinding, this.InternalAddOneDriveBindings, this.InternalRemoveOneDriveBindings, (this.PsPolicyPresentationObject.Workload & Workload.SharePoint) != Workload.SharePoint);
			if (this.PsPolicyPresentationObject.OneDriveBinding.Count<BindingMetadata>() > 100)
			{
				throw new BindingCountExceedsLimitException("Sharepoint", 100);
			}
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0002812D File Offset: 0x0002632D
		protected override void InternalStateReset()
		{
			this.DisposePolicyConfigProvider();
			base.InternalStateReset();
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0002813B File Offset: 0x0002633B
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			this.DisposePolicyConfigProvider();
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0002814C File Offset: 0x0002634C
		private void DisposePolicyConfigProvider()
		{
			PolicyConfigProvider policyConfigProvider = base.DataSession as PolicyConfigProvider;
			if (policyConfigProvider != null)
			{
				policyConfigProvider.Dispose();
			}
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00028178 File Offset: 0x00026378
		private void ValidateBindingParameterBeforeMerge()
		{
			if (this.AddExchangeBinding.Intersect(this.RemoveExchangeBinding, StringComparer.InvariantCultureIgnoreCase).Any<string>())
			{
				throw new AddRemoveExBindingsOverlappedException(string.Join(",", this.AddExchangeBinding.Intersect(this.RemoveExchangeBinding, StringComparer.InvariantCultureIgnoreCase)));
			}
			if (this.AddSharePointBinding.Intersect(this.RemoveSharePointBinding, StringComparer.InvariantCultureIgnoreCase).Any<string>())
			{
				throw new AddRemoveSpBindingsOverlappedException(string.Join(",", this.AddSharePointBinding.Intersect(this.RemoveSharePointBinding, StringComparer.InvariantCultureIgnoreCase)));
			}
			if (this.AddOneDriveBinding.Intersect(this.RemoveOneDriveBinding, StringComparer.InvariantCultureIgnoreCase).Any<string>())
			{
				throw new AddRemoveSpBindingsOverlappedException(string.Join(",", this.AddOneDriveBinding.Intersect(this.RemoveOneDriveBinding, StringComparer.InvariantCultureIgnoreCase)));
			}
			this.InternalAddExchangeBindings = new MultiValuedProperty<BindingMetadata>();
			this.InternalRemoveExchangeBindings = new MultiValuedProperty<BindingMetadata>();
			this.InternalAddSharePointBindings = new MultiValuedProperty<BindingMetadata>();
			this.InternalRemoveSharePointBindings = new MultiValuedProperty<BindingMetadata>();
			this.InternalAddOneDriveBindings = new MultiValuedProperty<BindingMetadata>();
			this.InternalRemoveOneDriveBindings = new MultiValuedProperty<BindingMetadata>();
			if (this.RemoveExchangeBinding.Any<string>())
			{
				this.ValidateRecipientsForRemove();
			}
			if (this.AddExchangeBinding.Any<string>())
			{
				List<string> list = (from b in this.PsPolicyPresentationObject.ExchangeBinding
				select b.ImmutableIdentity).ToList<string>();
				list.AddRange(this.AddExchangeBinding);
				this.AddExchangeBinding = NewCompliancePolicyBase.ValidateWideScopeBinding(this.AddExchangeBinding, "All", "All", new BindingCannotCombineAllWithIndividualBindingsException("Exchange"));
				this.ExpandGroupsAndValidateRecipientsForAdd();
			}
			this.InternalRemoveSharePointBindings = this.ValidateSharepointSitesForRemove(this.RemoveSharePointBinding);
			SetCompliancePolicyBase.ValidateAddedSharepointBinding(this.AddSharePointBinding, this.RemoveSharePointBinding, this.PsPolicyPresentationObject.SharePointBinding, "Sharepoint");
			this.InternalAddSharePointBindings = this.ValidateSharepointSitesForAdd(this.PsPolicyPresentationObject.SharePointBinding, this.InternalRemoveSharePointBindings, this.AddSharePointBinding, Workload.SharePoint);
			this.InternalRemoveOneDriveBindings = this.ValidateSharepointSitesForRemove(this.RemoveOneDriveBinding);
			SetCompliancePolicyBase.ValidateAddedSharepointBinding(this.AddOneDriveBinding, this.RemoveOneDriveBinding, this.PsPolicyPresentationObject.OneDriveBinding, "OneDriveBusiness");
			this.InternalAddOneDriveBindings = this.ValidateSharepointSitesForAdd(this.PsPolicyPresentationObject.OneDriveBinding, this.InternalRemoveOneDriveBindings, this.AddOneDriveBinding, Workload.OneDriveForBusiness);
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x000283D0 File Offset: 0x000265D0
		internal static void ValidateAddedSharepointBinding(IEnumerable<string> addedBindings, IEnumerable<string> removedBindings, MultiValuedProperty<BindingMetadata> psObjectBindings, string subWorkloadName)
		{
			if (addedBindings.Any<string>())
			{
				addedBindings = NewCompliancePolicyBase.ValidateWideScopeBinding(addedBindings, "All", "All", new BindingCannotCombineAllWithIndividualBindingsException(subWorkloadName));
				if (NewCompliancePolicyBase.IsBindingEnabled(addedBindings, "All"))
				{
					psObjectBindings.Clear();
					return;
				}
				List<string> bindings = (from b in psObjectBindings
				select b.ImmutableIdentity).ToList<string>();
				if (NewCompliancePolicyBase.IsBindingEnabled(bindings, "All") && !NewCompliancePolicyBase.IsBindingEnabled(removedBindings, "All"))
				{
					throw new BindingCannotCombineAllWithIndividualBindingsException(subWorkloadName);
				}
			}
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0002845C File Offset: 0x0002665C
		private void ValidateBindingParameter()
		{
			if ((this.PsPolicyPresentationObject.Workload & Workload.Exchange) == Workload.None && this.PsPolicyPresentationObject.ExchangeBinding.Any<BindingMetadata>())
			{
				throw new ExBindingWithoutExWorkloadException();
			}
			if ((this.PsPolicyPresentationObject.Workload & Workload.SharePoint) == Workload.None && this.PsPolicyPresentationObject.SharePointBinding.Any<BindingMetadata>())
			{
				throw new SpBindingWithoutSpWorkloadException();
			}
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x000284B8 File Offset: 0x000266B8
		private void ExpandGroupsAndValidateRecipientsForAdd()
		{
			base.WriteVerbose(Strings.VerboseValidatingAddExchangeBinding);
			int existingRecipientsCount = SetCompliancePolicyBase.CalculateBindingCountAfterRemove(this.PsPolicyPresentationObject.ExchangeBinding, this.InternalRemoveExchangeBindings);
			ExchangeValidator exchangeValidator = this.CreateExchangeValidator(true, "Validating AddExchangeBinding", existingRecipientsCount);
			this.InternalAddExchangeBindings = exchangeValidator.ValidateRecipients(this.AddExchangeBinding);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00028508 File Offset: 0x00026708
		private void ValidateRecipientsForRemove()
		{
			base.WriteVerbose(Strings.VerboseValidatingRemoveExchangeBinding);
			ExchangeValidator exchangeValidator = this.CreateExchangeValidator(false, "Validating RemoveExchangeBinding", 0);
			this.InternalRemoveExchangeBindings = exchangeValidator.ValidateRecipients(this.RemoveExchangeBinding);
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00028540 File Offset: 0x00026740
		private MultiValuedProperty<BindingMetadata> ValidateSharepointSitesForAdd(MultiValuedProperty<BindingMetadata> psObjectBindings, MultiValuedProperty<BindingMetadata> internalRemoveBindings, MultiValuedProperty<string> addBindingParameter, Workload subWorkload)
		{
			base.WriteVerbose(Strings.VerboseValidatingAddSharepointBinding);
			int existingSitesCount = SetCompliancePolicyBase.CalculateBindingCountAfterRemove(psObjectBindings, internalRemoveBindings);
			SharepointValidator sharepointValidator = this.CreateSharepointValidator("Validating AddSharepointBinding", existingSitesCount);
			MultiValuedProperty<BindingMetadata> multiValuedProperty = sharepointValidator.ValidateLocations(addBindingParameter);
			NewCompliancePolicyBase.SetBindingsSubWorkload(multiValuedProperty, subWorkload);
			return multiValuedProperty;
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00028580 File Offset: 0x00026780
		private MultiValuedProperty<BindingMetadata> ValidateSharepointSitesForRemove(IEnumerable<string> sitesToRemove)
		{
			base.WriteVerbose(Strings.VerboseValidatingRemoveSharepointBinding);
			SharepointValidator sharepointValidator = this.CreateSharepointValidator("Validating RemoveSharepointBinding", 0);
			return sharepointValidator.ValidateLocations(sitesToRemove);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x000285F4 File Offset: 0x000267F4
		private ExchangeValidator CreateExchangeValidator(bool allowGroups, string logTag, int existingRecipientsCount)
		{
			return ExchangeValidator.Create((IConfigurationSession)base.DataSession, (RecipientIdParameter recipientId, IRecipientSession recipientSession) => base.GetDataObject<ReducedRecipient>(recipientId, recipientSession, null, new LocalizedString?(Strings.ErrorUserObjectNotFound(recipientId.RawIdentity)), new LocalizedString?(Strings.ErrorUserObjectAmbiguous(recipientId.RawIdentity))) as ReducedRecipient, new Task.TaskErrorLoggingDelegate(base.WriteError), new Action<LocalizedString>(this.WriteWarning), (LocalizedString locString) => this.Force || base.ShouldContinue(locString), allowGroups, logTag, SourceValidator.Clients.SetCompliancePolicy, existingRecipientsCount, this.executionLogger);
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0002864C File Offset: 0x0002684C
		private void RetryPolicyDistribution()
		{
			if (!this.IsRetryDistributionAllowed)
			{
				base.WriteVerbose(Strings.VerboseRetryDistributionNotApplicable);
				return;
			}
			IEnumerable<Workload> enumerable = new List<Workload>
			{
				Workload.Exchange,
				Workload.SharePoint
			};
			Dictionary<Workload, List<ChangeNotificationData>> dictionary = this.GenerateSyncsForFailedPolicies(enumerable);
			Dictionary<Workload, List<ChangeNotificationData>> dictionary2 = this.GenerateSyncsForFailedBindings();
			Dictionary<Workload, List<ChangeNotificationData>> dictionary3 = this.GenerateSyncsForFailedRules(enumerable);
			foreach (Workload workload in enumerable)
			{
				List<ChangeNotificationData> list = new List<ChangeNotificationData>();
				List<ChangeNotificationData> list2 = new List<ChangeNotificationData>();
				if (dictionary.ContainsKey(workload))
				{
					list.AddRange(dictionary[workload]);
				}
				if (dictionary3.ContainsKey(workload))
				{
					list.AddRange(dictionary3[workload]);
				}
				if (dictionary2.ContainsKey(workload))
				{
					list2.AddRange(dictionary2[workload]);
				}
				if (list.Any<ChangeNotificationData>())
				{
					this.WriteSyncVerbose(list, workload, true);
					this.HandleNotificationErrors(this.OnNotifyRetryDistributionChanges(list, workload, true), list);
				}
				if (list2.Any<ChangeNotificationData>())
				{
					this.WriteSyncVerbose(list2, workload, false);
					this.HandleNotificationErrors(this.OnNotifyRetryDistributionChanges(list2, workload, false), list2);
				}
			}
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00028788 File Offset: 0x00026988
		private void WriteSyncVerbose(List<ChangeNotificationData> syncData, Workload workload, bool isObjectLevelSync)
		{
			base.WriteVerbose(Strings.VerboseRetryDistributionNotifyingWorkload(workload.ToString(), isObjectLevelSync ? "object" : "delta"));
			foreach (ChangeNotificationData changeNotificationData in syncData)
			{
				base.WriteVerbose(Strings.VerboseRetryDistributionNotificationDetails(changeNotificationData.Id.ToString(), changeNotificationData.ChangeType.ToString(), changeNotificationData.ObjectType.ToString()));
			}
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00028834 File Offset: 0x00026A34
		private Dictionary<Workload, List<ChangeNotificationData>> GenerateSyncsForFailedPolicies(IEnumerable<Workload> workloads)
		{
			IEnumerable<UnifiedPolicySettingStatus> syncStatuses = PolicySettingStatusHelpers.LoadSyncStatuses(base.DataSession, Utils.GetUniversalIdentity(this.DataObject), typeof(PolicyStorage).Name);
			return SetCompliancePolicyBase.GenerateSyncs(syncStatuses, workloads, base.DataSession, this.DataObject, ConfigurationObjectType.Policy);
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0002887C File Offset: 0x00026A7C
		private Dictionary<Workload, List<ChangeNotificationData>> GenerateSyncsForFailedRules(IEnumerable<Workload> workloads)
		{
			Dictionary<Workload, List<ChangeNotificationData>> dictionary = new Dictionary<Workload, List<ChangeNotificationData>>();
			IList<RuleStorage> list = Utils.LoadRuleStoragesByPolicy(base.DataSession, this.DataObject, Utils.GetRootId(base.DataSession));
			foreach (RuleStorage storageObject in list)
			{
				IEnumerable<UnifiedPolicySettingStatus> syncStatuses = PolicySettingStatusHelpers.LoadSyncStatuses(base.DataSession, Utils.GetUniversalIdentity(storageObject), typeof(RuleStorage).Name);
				Dictionary<Workload, List<ChangeNotificationData>> dictionary2 = SetCompliancePolicyBase.GenerateSyncs(syncStatuses, workloads, base.DataSession, storageObject, ConfigurationObjectType.Rule);
				foreach (Workload key in dictionary2.Keys)
				{
					if (dictionary.ContainsKey(key))
					{
						dictionary[key].AddRange(dictionary2[key]);
					}
					else
					{
						dictionary[key] = dictionary2[key];
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x000289A8 File Offset: 0x00026BA8
		private Dictionary<Workload, List<ChangeNotificationData>> GenerateSyncsForFailedBindings()
		{
			Dictionary<Workload, List<ChangeNotificationData>> dictionary = new Dictionary<Workload, List<ChangeNotificationData>>();
			using (IEnumerator<BindingStorage> enumerator = this.PsPolicyPresentationObject.StorageBindings.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					BindingStorage bindingStorage = enumerator.Current;
					IEnumerable<UnifiedPolicySettingStatus> source = PolicySettingStatusHelpers.LoadSyncStatuses(base.DataSession, Utils.GetUniversalIdentity(bindingStorage), typeof(BindingStorage).Name);
					bool flag = false;
					if (source.Any((UnifiedPolicySettingStatus s) => SetCompliancePolicyBase.HasDistributionFailed(bindingStorage, s)))
					{
						flag = true;
						bindingStorage.PolicyVersion = CombGuidGenerator.NewGuid();
						if (!dictionary.ContainsKey(bindingStorage.Workload))
						{
							dictionary[bindingStorage.Workload] = new List<ChangeNotificationData>();
						}
						dictionary[bindingStorage.Workload].Add(AggregatedNotificationClients.CreateChangeData(bindingStorage.Workload, bindingStorage));
					}
					List<ChangeNotificationData> list = this.GenerateSyncsForFailedScopes(bindingStorage.AppliedScopes);
					list.AddRange(this.GenerateSyncsForFailedScopes(bindingStorage.RemovedScopes));
					if (list.Any<ChangeNotificationData>())
					{
						if (!dictionary.ContainsKey(bindingStorage.Workload))
						{
							dictionary[bindingStorage.Workload] = new List<ChangeNotificationData>();
						}
						dictionary[bindingStorage.Workload].AddRange(list);
						if (!flag)
						{
							flag = true;
							bindingStorage.PolicyVersion = CombGuidGenerator.NewGuid();
							base.DataSession.Save(bindingStorage);
							dictionary[bindingStorage.Workload].Add(AggregatedNotificationClients.CreateChangeData(bindingStorage.Workload, bindingStorage));
						}
					}
					if (flag)
					{
						base.DataSession.Save(bindingStorage);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00028BBC File Offset: 0x00026DBC
		private List<ChangeNotificationData> GenerateSyncsForFailedScopes(IEnumerable<ScopeStorage> scopeStorages)
		{
			List<ChangeNotificationData> list = new List<ChangeNotificationData>();
			foreach (ScopeStorage scopeStorage in scopeStorages)
			{
				IEnumerable<UnifiedPolicySettingStatus> enumerable = PolicySettingStatusHelpers.LoadSyncStatuses(base.DataSession, Utils.GetUniversalIdentity(scopeStorage), typeof(ScopeStorage).Name);
				foreach (UnifiedPolicySettingStatus status in enumerable)
				{
					if (SetCompliancePolicyBase.HasDistributionFailed(scopeStorage, status))
					{
						scopeStorage.PolicyVersion = CombGuidGenerator.NewGuid();
						list.Add(AggregatedNotificationClients.CreateChangeData(scopeStorage.Workload, scopeStorage));
					}
				}
			}
			return list;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00028CA0 File Offset: 0x00026EA0
		private static Dictionary<Workload, List<ChangeNotificationData>> GenerateSyncs(IEnumerable<UnifiedPolicySettingStatus> syncStatuses, IEnumerable<Workload> workloads, IConfigDataProvider dataSession, UnifiedPolicyStorageBase storageObject, ConfigurationObjectType objectType)
		{
			Dictionary<Workload, List<ChangeNotificationData>> dictionary = new Dictionary<Workload, List<ChangeNotificationData>>();
			if (syncStatuses.Any((UnifiedPolicySettingStatus s) => SetCompliancePolicyBase.HasDistributionFailed(storageObject, s)))
			{
				storageObject.PolicyVersion = CombGuidGenerator.NewGuid();
				dataSession.Save(storageObject);
				foreach (Workload workload in workloads)
				{
					dictionary[workload] = new List<ChangeNotificationData>
					{
						AggregatedNotificationClients.CreateChangeData(workload, storageObject)
					};
				}
			}
			return dictionary;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00028D4C File Offset: 0x00026F4C
		private void HandleNotificationErrors(string notificationError, IEnumerable<ChangeNotificationData> syncData)
		{
			if (!string.IsNullOrEmpty(notificationError))
			{
				base.WriteWarning(notificationError);
				AggregatedNotificationClients.SetNotificationResults(syncData, notificationError);
				PolicySettingStatusHelpers.CheckNotificationResultsAndUpdateStatus(this, (IConfigurationSession)base.DataSession, syncData);
			}
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00028D90 File Offset: 0x00026F90
		private SharepointValidator CreateSharepointValidator(string logTag, int existingSitesCount)
		{
			return SharepointValidator.Create((IConfigurationSession)base.DataSession, base.ExchangeRunspaceConfig, new Task.TaskErrorLoggingDelegate(base.WriteError), new Action<LocalizedString>(this.WriteWarning), (LocalizedString locString) => this.Force || base.ShouldContinue(locString), logTag, SourceValidator.Clients.SetCompliancePolicy, existingSitesCount, this.executionLogger);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00028DF4 File Offset: 0x00026FF4
		private static int CalculateBindingCountAfterRemove(MultiValuedProperty<BindingMetadata> existingBinding, MultiValuedProperty<BindingMetadata> removeBinding)
		{
			return existingBinding.Count - (from item in existingBinding
			select item.ImmutableIdentity).Intersect(from item in removeBinding
			select item.ImmutableIdentity, StringComparer.OrdinalIgnoreCase).Count<string>();
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00028E5D File Offset: 0x0002705D
		private static bool HasDistributionFailed(UnifiedPolicyStorageBase storageObject, UnifiedPolicySettingStatus status)
		{
			return status.ObjectVersion == storageObject.PolicyVersion && status.ErrorCode != 0;
		}

		// Token: 0x04000415 RID: 1045
		private const string RetryDistributionParameterSet = "RetryDistributionParameterSet";

		// Token: 0x04000416 RID: 1046
		protected ExecutionLog executionLogger = ExExecutionLog.CreateForCmdlet();
	}
}
