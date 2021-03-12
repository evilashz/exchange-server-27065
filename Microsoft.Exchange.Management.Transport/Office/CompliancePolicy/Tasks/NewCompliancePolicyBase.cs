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
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;
using Microsoft.Office.CompliancePolicy.Validators;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000ED RID: 237
	[Cmdlet("New", "CompliancePolicyBase", SupportsShouldProcess = true)]
	public abstract class NewCompliancePolicyBase : NewMultitenancyFixedNameSystemConfigurationObjectTask<PolicyStorage>
	{
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x00026D69 File Offset: 0x00024F69
		// (set) Token: 0x06000979 RID: 2425 RVA: 0x00026D71 File Offset: 0x00024F71
		protected PsCompliancePolicyBase PsPolicyPresentationObject { get; set; }

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x00026D7A File Offset: 0x00024F7A
		// (set) Token: 0x0600097B RID: 2427 RVA: 0x00026D82 File Offset: 0x00024F82
		protected PolicyScenario Scenario { get; set; }

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x00026D8B File Offset: 0x00024F8B
		protected Workload TenantWorkloadConfig
		{
			get
			{
				return Workload.Exchange | Workload.SharePoint;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x00026D8E File Offset: 0x00024F8E
		// (set) Token: 0x0600097E RID: 2430 RVA: 0x00026DA5 File Offset: 0x00024FA5
		[Parameter(Mandatory = true, Position = 0)]
		public string Name
		{
			get
			{
				return (string)base.Fields[ADObjectSchema.Name];
			}
			set
			{
				base.Fields[ADObjectSchema.Name] = value;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x00026DB8 File Offset: 0x00024FB8
		// (set) Token: 0x06000980 RID: 2432 RVA: 0x00026DCF File Offset: 0x00024FCF
		[Parameter(Mandatory = false)]
		public string Comment
		{
			get
			{
				return (string)base.Fields[PsCompliancePolicyBaseSchema.Comment];
			}
			set
			{
				base.Fields[PsCompliancePolicyBaseSchema.Comment] = value;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x00026DE4 File Offset: 0x00024FE4
		// (set) Token: 0x06000982 RID: 2434 RVA: 0x00026E0D File Offset: 0x0002500D
		[Parameter(Mandatory = false)]
		public bool Enabled
		{
			get
			{
				object obj = base.Fields[PsCompliancePolicyBaseSchema.Enabled];
				return obj == null || (bool)obj;
			}
			set
			{
				base.Fields[PsCompliancePolicyBaseSchema.Enabled] = value;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x00026E25 File Offset: 0x00025025
		// (set) Token: 0x06000984 RID: 2436 RVA: 0x00026E4B File Offset: 0x0002504B
		[Parameter(Mandatory = false)]
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

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x00026E63 File Offset: 0x00025063
		// (set) Token: 0x06000986 RID: 2438 RVA: 0x00026E70 File Offset: 0x00025070
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ExchangeBinding
		{
			get
			{
				return Utils.BindingParameterGetter(this.exchangeBindingParameter);
			}
			set
			{
				this.exchangeBindingParameter = Utils.BindingParameterSetter(value);
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x00026E7E File Offset: 0x0002507E
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x00026E8B File Offset: 0x0002508B
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> SharePointBinding
		{
			get
			{
				return Utils.BindingParameterGetter(this.sharePointBindingParameter);
			}
			set
			{
				this.sharePointBindingParameter = Utils.BindingParameterSetter(value);
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x00026E99 File Offset: 0x00025099
		// (set) Token: 0x0600098A RID: 2442 RVA: 0x00026EA6 File Offset: 0x000250A6
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> OneDriveBinding
		{
			get
			{
				return Utils.BindingParameterGetter(this.oneDriveBindingParameter);
			}
			set
			{
				this.oneDriveBindingParameter = Utils.BindingParameterSetter(value);
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x00026EB4 File Offset: 0x000250B4
		protected override bool SkipWriteResult
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x00026EB7 File Offset: 0x000250B7
		// (set) Token: 0x0600098D RID: 2445 RVA: 0x00026EBF File Offset: 0x000250BF
		private protected MultiValuedProperty<BindingMetadata> InternalExchangeBindings { protected get; private set; }

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x00026EC8 File Offset: 0x000250C8
		// (set) Token: 0x0600098F RID: 2447 RVA: 0x00026ED0 File Offset: 0x000250D0
		private protected MultiValuedProperty<BindingMetadata> InternalSharePointBindings { protected get; private set; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x00026ED9 File Offset: 0x000250D9
		// (set) Token: 0x06000991 RID: 2449 RVA: 0x00026EE1 File Offset: 0x000250E1
		private protected MultiValuedProperty<BindingMetadata> InternalOneDriveBindings { protected get; private set; }

		// Token: 0x06000992 RID: 2450 RVA: 0x00026EEA File Offset: 0x000250EA
		public NewCompliancePolicyBase()
		{
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x00026EFD File Offset: 0x000250FD
		protected NewCompliancePolicyBase(PolicyScenario scenario)
		{
			this.Scenario = scenario;
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00026F18 File Offset: 0x00025118
		protected override IConfigurable PrepareDataObject()
		{
			PolicyStorage policyStorage = (PolicyStorage)base.PrepareDataObject();
			policyStorage.SetId(base.DataSession as IConfigurationSession, this.Name);
			policyStorage.MasterIdentity = Guid.NewGuid();
			policyStorage.Scenario = this.Scenario;
			return policyStorage;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00026F60 File Offset: 0x00025160
		protected override IConfigDataProvider CreateSession()
		{
			return PolicyConfigProviderManager<ExPolicyConfigProviderManager>.Instance.CreateForCmdlet(base.CreateSession() as IConfigurationSession, this.executionLogger);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00026F94 File Offset: 0x00025194
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || Utils.KnownExceptions.Any((Type exceptionType) => exceptionType.IsInstanceOfType(exception));
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00026FE8 File Offset: 0x000251E8
		protected override void InternalValidate()
		{
			Utils.ThrowIfNotRunInEOP();
			base.InternalValidate();
			Utils.ValidateNotForestWideOrganization(base.CurrentOrganizationId);
			IEnumerable<PolicyStorage> source = Utils.LoadPolicyStorages(base.DataSession, this.Scenario);
			if (source.Count<PolicyStorage>() > 1000)
			{
				throw new CompliancePolicyCountExceedsLimitException(1000);
			}
			if (source.Any((PolicyStorage x) => x.Name.Equals(this.Name, StringComparison.InvariantCultureIgnoreCase)))
			{
				throw new CompliancePolicyAlreadyExistsException(this.Name);
			}
			this.ValidateBindingParameter();
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0002705C File Offset: 0x0002525C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.PsPolicyPresentationObject.UpdateStorageProperties(this, base.DataSession as IConfigurationSession, true);
			base.InternalProcessRecord();
			foreach (BindingStorage bindingStorage in this.PsPolicyPresentationObject.StorageBindings)
			{
				base.DataSession.Save(bindingStorage);
				base.WriteVerbose(Strings.VerboseSaveBindingStorageObjects(bindingStorage.ToString(), this.PsPolicyPresentationObject.ToString()));
			}
			if (!base.HasErrors)
			{
				this.WriteResult();
			}
			PolicySettingStatusHelpers.CheckNotificationResultsAndUpdateStatus(this, (IConfigurationSession)base.DataSession, this.OnNotifyChanges());
			TaskLogger.LogExit();
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0002711C File Offset: 0x0002531C
		protected virtual IEnumerable<ChangeNotificationData> OnNotifyChanges()
		{
			return AggregatedNotificationClients.NotifyChanges(this, (IConfigurationSession)base.DataSession, this.DataObject, this.executionLogger, base.GetType(), this.PsPolicyPresentationObject.StorageBindings, null);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00027170 File Offset: 0x00025370
		internal static MultiValuedProperty<string> ValidateWideScopeBinding(IEnumerable<string> bindings, string wideScopeBindingName, string wideScopeBindingValue, Exception validationException)
		{
			if (NewCompliancePolicyBase.IsBindingEnabled(bindings, wideScopeBindingValue) && bindings.Count<string>() > 1)
			{
				throw validationException;
			}
			if (string.Compare(wideScopeBindingName, wideScopeBindingValue, StringComparison.InvariantCultureIgnoreCase) != 0)
			{
				List<string> list = new List<string>(bindings.Count<string>());
				list.AddRange(bindings.Select(delegate(string binding)
				{
					if (string.Compare(binding, wideScopeBindingName, StringComparison.InvariantCultureIgnoreCase) != 0)
					{
						return binding;
					}
					return wideScopeBindingValue;
				}));
				return new MultiValuedProperty<string>(list);
			}
			return new MultiValuedProperty<string>(bindings);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00027210 File Offset: 0x00025410
		internal static bool IsBindingEnabled(IEnumerable<string> bindings, string valueToCheck)
		{
			return bindings.Any((string b) => string.Compare(b, valueToCheck, StringComparison.InvariantCultureIgnoreCase) == 0);
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0002723C File Offset: 0x0002543C
		internal static void SetBindingsSubWorkload(MultiValuedProperty<BindingMetadata> bindings, Workload subWorkload)
		{
			foreach (BindingMetadata bindingMetadata in bindings)
			{
				bindingMetadata.Workload = subWorkload;
			}
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0002728C File Offset: 0x0002548C
		private void ValidateBindingParameter()
		{
			if ((this.TenantWorkloadConfig & Workload.Exchange) == Workload.None && this.ExchangeBinding.Any<string>())
			{
				throw new ExBindingWithoutExWorkloadException();
			}
			if ((this.TenantWorkloadConfig & Workload.SharePoint) == Workload.None && this.SharePointBinding.Any<string>())
			{
				throw new SpBindingWithoutSpWorkloadException();
			}
			this.InternalExchangeBindings = this.ValidateExchangeBindings(this.ExchangeBinding, 1000);
			this.InternalSharePointBindings = this.ValidateSharepointBindings(this.SharePointBinding, Workload.SharePoint, "Sharepoint", 100);
			this.InternalOneDriveBindings = this.ValidateSharepointBindings(this.OneDriveBinding, Workload.OneDriveForBusiness, "OneDriveBusiness", 100);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00027328 File Offset: 0x00025528
		private MultiValuedProperty<BindingMetadata> ValidateExchangeBindings(IEnumerable<string> bindings, int maxCount)
		{
			base.WriteVerbose(Strings.VerboseValidatingExchangeBinding);
			MultiValuedProperty<BindingMetadata> multiValuedProperty = new MultiValuedProperty<BindingMetadata>();
			if (bindings.Any<string>())
			{
				List<string> list = (from binding in bindings
				where SourceValidator.IsWideScope(binding)
				select binding).ToList<string>();
				if (list.Any<string>())
				{
					throw new ExCannotContainWideScopeBindingsException(string.Join(", ", list));
				}
				this.ExchangeBinding = NewCompliancePolicyBase.ValidateWideScopeBinding(this.ExchangeBinding, "All", "All", new BindingCannotCombineAllWithIndividualBindingsException("Exchange"));
				ExchangeValidator exchangeValidator = this.CreateExchangeValidator(true, "Validating ExchangeBinding");
				multiValuedProperty = exchangeValidator.ValidateRecipients(this.ExchangeBinding);
				if (this.ExchangeBinding.Count<string>() > 1000)
				{
					throw new BindingCountExceedsLimitException("Exchange", 1000);
				}
			}
			NewCompliancePolicyBase.SetBindingsSubWorkload(multiValuedProperty, Workload.Exchange);
			return multiValuedProperty;
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x000273FC File Offset: 0x000255FC
		private MultiValuedProperty<BindingMetadata> ValidateSharepointBindings(IEnumerable<string> bindings, Workload subWorkload, string workloadName, int maxCount)
		{
			base.WriteVerbose(Strings.VerboseValidatingSharepointBinding(workloadName));
			MultiValuedProperty<BindingMetadata> multiValuedProperty = new MultiValuedProperty<BindingMetadata>();
			if (bindings.Any<string>())
			{
				bindings = NewCompliancePolicyBase.ValidateWideScopeBinding(bindings, "All", "All", new BindingCannotCombineAllWithIndividualBindingsException(workloadName));
				SharepointValidator sharepointValidator = this.CreateSharepointValidator(string.Format("Validating {0} Binding", workloadName));
				multiValuedProperty = sharepointValidator.ValidateLocations(bindings);
				if (multiValuedProperty.Count<BindingMetadata>() > maxCount)
				{
					throw new BindingCountExceedsLimitException(workloadName, maxCount);
				}
				NewCompliancePolicyBase.SetBindingsSubWorkload(multiValuedProperty, subWorkload);
			}
			return multiValuedProperty;
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x000274B8 File Offset: 0x000256B8
		private ExchangeValidator CreateExchangeValidator(bool allowGroups, string logTag)
		{
			return ExchangeValidator.Create((IConfigurationSession)base.DataSession, (RecipientIdParameter recipientId, IRecipientSession recipientSession) => base.GetDataObject<ReducedRecipient>(recipientId, recipientSession, null, new LocalizedString?(Strings.ErrorUserObjectNotFound(recipientId.RawIdentity)), new LocalizedString?(Strings.ErrorUserObjectAmbiguous(recipientId.RawIdentity))) as ReducedRecipient, new Task.TaskErrorLoggingDelegate(base.WriteError), new Action<LocalizedString>(this.WriteWarning), (LocalizedString locString) => this.Force || base.ShouldContinue(locString), allowGroups, logTag, SourceValidator.Clients.NewCompliancePolicy, 0, this.executionLogger);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00027528 File Offset: 0x00025728
		private SharepointValidator CreateSharepointValidator(string logTag)
		{
			return SharepointValidator.Create((IConfigurationSession)base.DataSession, base.ExchangeRunspaceConfig, new Task.TaskErrorLoggingDelegate(base.WriteError), new Action<LocalizedString>(this.WriteWarning), (LocalizedString locString) => this.Force || base.ShouldContinue(locString), logTag, SourceValidator.Clients.NewCompliancePolicy, 0, this.executionLogger);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00027579 File Offset: 0x00025779
		protected override void InternalStateReset()
		{
			this.DisposePolicyConfigProvider();
			base.InternalStateReset();
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00027587 File Offset: 0x00025787
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			this.DisposePolicyConfigProvider();
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00027598 File Offset: 0x00025798
		private void DisposePolicyConfigProvider()
		{
			PolicyConfigProvider policyConfigProvider = base.DataSession as PolicyConfigProvider;
			if (policyConfigProvider != null)
			{
				policyConfigProvider.Dispose();
			}
		}

		// Token: 0x0400040B RID: 1035
		private MultiValuedProperty<string> exchangeBindingParameter;

		// Token: 0x0400040C RID: 1036
		private MultiValuedProperty<string> sharePointBindingParameter;

		// Token: 0x0400040D RID: 1037
		private MultiValuedProperty<string> oneDriveBindingParameter;

		// Token: 0x0400040E RID: 1038
		protected ExecutionLog executionLogger = ExExecutionLog.CreateForCmdlet();
	}
}
