using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Data.Storage.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;
using Microsoft.Office.CompliancePolicy.PolicySync;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000131 RID: 305
	[Cmdlet("Start", "UnifiedPolicySynchronization", DefaultParameterSetName = "SyncByType")]
	public sealed class StartUnifiedPolicySynchronization : GetMultitenancySystemConfigurationObjectTask<PolicyIdParameter, PolicyStorage>
	{
		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x0002FFF1 File Offset: 0x0002E1F1
		// (set) Token: 0x06000D63 RID: 3427 RVA: 0x00030025 File Offset: 0x0002E225
		[Parameter(Mandatory = true, ParameterSetName = "SyncDeleteObject")]
		[Parameter(Mandatory = false, ParameterSetName = "SyncByType")]
		[Parameter(Mandatory = true, ParameterSetName = "SyncUpdateObject")]
		public MultiValuedProperty<ConfigurationObjectType> ObjectType
		{
			get
			{
				if (base.Fields["ObjectType"] != null)
				{
					return (MultiValuedProperty<ConfigurationObjectType>)base.Fields["ObjectType"];
				}
				return new MultiValuedProperty<ConfigurationObjectType>(StartUnifiedPolicySynchronization.SupporttedObjectTypes);
			}
			set
			{
				base.Fields["ObjectType"] = value;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x00030038 File Offset: 0x0002E238
		// (set) Token: 0x06000D65 RID: 3429 RVA: 0x0003005E File Offset: 0x0002E25E
		[Parameter(Mandatory = false, ParameterSetName = "SyncByType")]
		public SwitchParameter FullSync
		{
			get
			{
				return (SwitchParameter)(base.Fields["FullSync"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["FullSync"] = value;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x00030076 File Offset: 0x0002E276
		// (set) Token: 0x06000D67 RID: 3431 RVA: 0x0003008D File Offset: 0x0002E28D
		[Parameter(Mandatory = true, ParameterSetName = "SyncUpdateObject")]
		public MultiValuedProperty<string> UpdateObjectId
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["UpdateObjectId"];
			}
			set
			{
				base.Fields["UpdateObjectId"] = value;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x000300A0 File Offset: 0x0002E2A0
		// (set) Token: 0x06000D69 RID: 3433 RVA: 0x000300B7 File Offset: 0x0002E2B7
		[Parameter(Mandatory = true, ParameterSetName = "SyncDeleteObject")]
		public MultiValuedProperty<Guid> DeleteObjectId
		{
			get
			{
				return (MultiValuedProperty<Guid>)base.Fields["DeleteObjectId"];
			}
			set
			{
				base.Fields["DeleteObjectId"] = value;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x000300CA File Offset: 0x0002E2CA
		// (set) Token: 0x06000D6B RID: 3435 RVA: 0x000300F5 File Offset: 0x0002E2F5
		[Parameter(Mandatory = false)]
		public Workload Workload
		{
			get
			{
				if (base.Fields["Workload"] != null)
				{
					return (Workload)base.Fields["Workload"];
				}
				return Workload.Exchange | Workload.SharePoint;
			}
			set
			{
				base.Fields["Workload"] = value;
			}
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00030144 File Offset: 0x0002E344
		protected override void InternalValidate()
		{
			base.InternalValidate();
			Utils.ThrowIfNotRunInEOP();
			Utils.ValidateNotForestWideOrganization(base.CurrentOrganizationId);
			if (base.Fields.IsModified("Workload"))
			{
				Utils.ValidateWorkloadParameter(this.Workload);
			}
			if (!base.ParameterSetName.Equals("SyncDeleteObject") && !base.ParameterSetName.Equals("SyncUpdateObject"))
			{
				if (base.Fields.IsModified("ObjectType"))
				{
					if (this.ObjectType.Any((ConfigurationObjectType x) => !StartUnifiedPolicySynchronization.SupporttedObjectTypes.Contains(x)))
					{
						throw new InvalidDeltaSyncAndFullSyncTypeException(string.Join(",", from x in StartUnifiedPolicySynchronization.SupporttedObjectTypes
						select x.ToString()));
					}
				}
				return;
			}
			if (this.ObjectType.Count != 1)
			{
				throw new MultipleObjectTypeForObjectLevelSyncException(string.Join(",", from x in StartUnifiedPolicySynchronization.SupporttedObjectTypesForObjectSync
				select x.ToString()));
			}
			if (!StartUnifiedPolicySynchronization.SupporttedObjectTypesForObjectSync.Contains(this.ObjectType.First<ConfigurationObjectType>()))
			{
				throw new InvalidObjectSyncTypeException(string.Join(",", from x in StartUnifiedPolicySynchronization.SupporttedObjectTypesForObjectSync
				select x.ToString()));
			}
			this.objectSyncGuids = (base.ParameterSetName.Equals("SyncDeleteObject") ? this.DeleteObjectId : this.ValidateUpdateObjectSyncParameters());
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x000302D8 File Offset: 0x0002E4D8
		protected override void InternalProcessRecord()
		{
			string parameterSetName;
			if ((parameterSetName = base.ParameterSetName) != null)
			{
				if (!(parameterSetName == "SyncByType"))
				{
					if (!(parameterSetName == "SyncUpdateObject"))
					{
						if (!(parameterSetName == "SyncDeleteObject"))
						{
							goto IL_124;
						}
						goto IL_D1;
					}
				}
				else
				{
					using (MultiValuedProperty<ConfigurationObjectType>.Enumerator enumerator = this.ObjectType.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ConfigurationObjectType objectType = enumerator.Current;
							this.syncChangeInfos.Add(new SyncChangeInfo(objectType));
						}
						goto IL_124;
					}
				}
				using (IEnumerator<Guid> enumerator2 = this.objectSyncGuids.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Guid value = enumerator2.Current;
						this.syncChangeInfos.Add(new SyncChangeInfo(this.ObjectType.First<ConfigurationObjectType>(), ChangeType.Update, null, new Guid?(value)));
					}
					goto IL_124;
				}
				IL_D1:
				foreach (Guid value2 in this.DeleteObjectId)
				{
					this.syncChangeInfos.Add(new SyncChangeInfo(this.ObjectType.First<ConfigurationObjectType>(), ChangeType.Delete, null, new Guid?(value2)));
				}
			}
			IL_124:
			this.SendNotification();
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00030438 File Offset: 0x0002E638
		private void SendNotification()
		{
			foreach (object obj in Enum.GetValues(typeof(Workload)))
			{
				Workload workload = (Workload)obj;
				if ((this.Workload & workload) != Workload.None)
				{
					string identity;
					string text = AggregatedNotificationClients.NotifyChangesByWorkload(this, base.DataSession as IConfigurationSession, workload, this.syncChangeInfos, this.FullSync.ToBool(), true, this.executionLogger, base.GetType(), out identity);
					if (string.IsNullOrEmpty(text))
					{
						this.WriteResult(new PsUnifiedPolicyNotification(workload, identity, this.syncChangeInfos, this.FullSync.ToBool()));
					}
					else
					{
						base.WriteWarning(text);
					}
				}
			}
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00030528 File Offset: 0x0002E728
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || Utils.KnownExceptions.Any((Type exceptionType) => exceptionType.IsInstanceOfType(exception));
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00030568 File Offset: 0x0002E768
		protected override IConfigDataProvider CreateSession()
		{
			return PolicyConfigProviderManager<ExPolicyConfigProviderManager>.Instance.CreateForCmdlet(base.CreateSession() as IConfigurationSession, this.executionLogger);
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00030588 File Offset: 0x0002E788
		private IList<Guid> ValidateUpdateObjectSyncParameters()
		{
			IList<Guid> list = new List<Guid>();
			foreach (string text in this.UpdateObjectId)
			{
				switch (this.ObjectType.First<ConfigurationObjectType>())
				{
				case ConfigurationObjectType.Policy:
				{
					PolicyStorage storageObject = (PolicyStorage)base.GetDataObject<PolicyStorage>(new PolicyIdParameter(text), base.DataSession, null, new LocalizedString?(Strings.ErrorPolicyNotFound(text)), new LocalizedString?(Strings.ErrorPolicyNotUnique(text)));
					list.Add(Utils.GetUniversalIdentity(storageObject));
					break;
				}
				case ConfigurationObjectType.Rule:
				{
					RuleStorage storageObject2 = (RuleStorage)base.GetDataObject<RuleStorage>(new ComplianceRuleIdParameter(text), base.DataSession, null, new LocalizedString?(Strings.ErrorRuleNotFound(text)), new LocalizedString?(Strings.ErrorRuleNotUnique(text)));
					list.Add(Utils.GetUniversalIdentity(storageObject2));
					break;
				}
				default:
					throw new NotImplementedException();
				}
			}
			return list;
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00030680 File Offset: 0x0002E880
		protected override void InternalStateReset()
		{
			this.DisposePolicyConfigProvider();
			base.InternalStateReset();
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0003068E File Offset: 0x0002E88E
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			this.DisposePolicyConfigProvider();
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x000306A0 File Offset: 0x0002E8A0
		private void DisposePolicyConfigProvider()
		{
			PolicyConfigProvider policyConfigProvider = base.DataSession as PolicyConfigProvider;
			if (policyConfigProvider != null)
			{
				policyConfigProvider.Dispose();
			}
		}

		// Token: 0x04000442 RID: 1090
		private ExecutionLog executionLogger = ExExecutionLog.CreateForCmdlet();

		// Token: 0x04000443 RID: 1091
		private static readonly List<ConfigurationObjectType> SupporttedObjectTypes = new List<ConfigurationObjectType>
		{
			ConfigurationObjectType.Policy,
			ConfigurationObjectType.Rule,
			ConfigurationObjectType.Binding
		};

		// Token: 0x04000444 RID: 1092
		private static readonly List<ConfigurationObjectType> SupporttedObjectTypesForObjectSync = new List<ConfigurationObjectType>
		{
			ConfigurationObjectType.Policy,
			ConfigurationObjectType.Rule
		};

		// Token: 0x04000445 RID: 1093
		private readonly List<SyncChangeInfo> syncChangeInfos = new List<SyncChangeInfo>();

		// Token: 0x04000446 RID: 1094
		private IList<Guid> objectSyncGuids = new List<Guid>();
	}
}
