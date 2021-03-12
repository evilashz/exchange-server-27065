using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Data.Storage.UnifiedPolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000E2 RID: 226
	internal static class AuditConfigUtility
	{
		// Token: 0x06000900 RID: 2304 RVA: 0x00025A54 File Offset: 0x00023C54
		internal static AuditConfigurationRule GetAuditConfigurationRule(Workload workload, OrganizationIdParameter organizationId, out IEnumerable<ErrorRecord> pipelineErrors)
		{
			Guid guid;
			if (!AuditPolicyUtility.GetRuleGuidFromWorkload(workload, out guid))
			{
				pipelineErrors = new List<ErrorRecord>();
				return null;
			}
			Command command = new Command("Get-AuditConfigurationRule");
			if (organizationId != null)
			{
				command.Parameters.Add("Organization", organizationId);
			}
			command.Parameters.Add("Identity", guid.ToString());
			return AuditConfigUtility.InvokeCommand(command, out pipelineErrors) as AuditConfigurationRule;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00025AC0 File Offset: 0x00023CC0
		internal static AuditConfigurationPolicy GetAuditConfigurationPolicy(Workload workload, OrganizationIdParameter organizationId, out IEnumerable<ErrorRecord> pipelineErrors)
		{
			Guid guid;
			if (!AuditPolicyUtility.GetPolicyGuidFromWorkload(workload, out guid))
			{
				pipelineErrors = new List<ErrorRecord>();
				return null;
			}
			Command command = new Command("Get-AuditConfigurationPolicy");
			if (organizationId != null)
			{
				command.Parameters.Add("Organization", organizationId);
			}
			command.Parameters.Add("Identity", guid.ToString());
			return AuditConfigUtility.InvokeCommand(command, out pipelineErrors) as AuditConfigurationPolicy;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00025B2C File Offset: 0x00023D2C
		internal static AuditConfigurationPolicy NewAuditConfigurationPolicy(Workload workload, OrganizationIdParameter organizationId, out IEnumerable<ErrorRecord> pipelineErrors)
		{
			Command command = new Command("New-AuditConfigurationPolicy");
			if (organizationId != null)
			{
				command.Parameters.Add("Organization", organizationId);
			}
			command.Parameters.Add("Workload", workload);
			return AuditConfigUtility.InvokeCommand(command, out pipelineErrors) as AuditConfigurationPolicy;
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00025B7C File Offset: 0x00023D7C
		internal static AuditConfigurationRule NewAuditConfigurationRule(Workload workload, OrganizationIdParameter organizationId, MultiValuedProperty<AuditableOperations> auditOpsToSet, out IEnumerable<ErrorRecord> pipelineErrors)
		{
			Command command = new Command("New-AuditConfigurationRule");
			if (organizationId != null)
			{
				command.Parameters.Add("Organization", organizationId);
			}
			command.Parameters.Add("Workload", workload);
			command.Parameters.Add("AuditOperation", auditOpsToSet);
			return AuditConfigUtility.InvokeCommand(command, out pipelineErrors) as AuditConfigurationRule;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00025BE0 File Offset: 0x00023DE0
		internal static void SetAuditConfigurationRule(Workload workload, OrganizationIdParameter organizationId, MultiValuedProperty<AuditableOperations> auditOpsToSet, out IEnumerable<ErrorRecord> pipelineErrors)
		{
			Guid guid;
			if (!AuditPolicyUtility.GetRuleGuidFromWorkload(workload, out guid))
			{
				pipelineErrors = new List<ErrorRecord>();
				return;
			}
			Command command = new Command("Get-AuditConfigurationRule");
			if (organizationId != null)
			{
				command.Parameters.Add("Organization", organizationId);
			}
			command.Parameters.Add("Identity", guid.ToString());
			Command command2 = new Command("Set-AuditConfigurationRule");
			command2.Parameters.Add("AuditOperation", auditOpsToSet);
			AuditConfigUtility.InvokeCommands(new List<Command>
			{
				command,
				command2
			}, out pipelineErrors);
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00025C73 File Offset: 0x00023E73
		internal static Workload GetEffectiveWorkload(Workload auditWorkload)
		{
			if (auditWorkload != Workload.OneDriveForBusiness)
			{
				return auditWorkload;
			}
			return Workload.SharePoint;
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x00025C7C File Offset: 0x00023E7C
		internal static List<Workload> AuditableWorkloads
		{
			get
			{
				return AuditConfigUtility.auditableWorkloads;
			}
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00025C84 File Offset: 0x00023E84
		private static object InvokeCommand(Command cmd, out IEnumerable<ErrorRecord> pipelineErrors)
		{
			return AuditConfigUtility.InvokeCommands(new List<Command>
			{
				cmd
			}, out pipelineErrors);
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00025CA8 File Offset: 0x00023EA8
		private static object InvokeCommands(List<Command> cmds, out IEnumerable<ErrorRecord> pipelineErrors)
		{
			List<ErrorRecord> list = new List<ErrorRecord>();
			pipelineErrors = list;
			object result = null;
			using (Pipeline pipeline = Runspace.DefaultRunspace.CreateNestedPipeline())
			{
				foreach (Command item in cmds)
				{
					pipeline.Commands.Add(item);
				}
				Collection<PSObject> collection = pipeline.Invoke();
				if (collection != null && collection.Count == 1)
				{
					result = collection[0].BaseObject;
				}
				if (pipeline.Error.Count > 0)
				{
					while (!pipeline.Error.EndOfPipeline)
					{
						PSObject psobject = pipeline.Error.Read() as PSObject;
						if (psobject != null)
						{
							ErrorRecord errorRecord = psobject.BaseObject as ErrorRecord;
							if (errorRecord != null)
							{
								list.Add(errorRecord);
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00025D9F File Offset: 0x00023F9F
		internal static bool ValidateErrorRecords(Cmdlet cmdLet, IEnumerable<ErrorRecord> errRecords)
		{
			return AuditConfigUtility.ValidateErrorRecords(cmdLet, errRecords, (ErrorRecord err) => true);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00025DC8 File Offset: 0x00023FC8
		internal static bool ValidateErrorRecords(Cmdlet cmdLet, IEnumerable<ErrorRecord> errRecords, Func<ErrorRecord, bool> predicate)
		{
			bool result = true;
			foreach (ErrorRecord errorRecord in errRecords.Where(predicate))
			{
				cmdLet.WriteError(errorRecord);
				result = false;
			}
			return result;
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00025E1C File Offset: 0x0002401C
		internal static AuditSwitchStatus ValidateAuditConfigurationRule(Workload workload, AuditConfigurationRule auditRule)
		{
			MultiValuedProperty<AuditableOperations> auditOperation = auditRule.AuditOperation;
			Tuple<MultiValuedProperty<AuditableOperations>, MultiValuedProperty<AuditableOperations>> tuple = AuditConfigUtility.auditMap[workload];
			if (tuple != null)
			{
				if (auditOperation.Equals(tuple.Item1))
				{
					return AuditSwitchStatus.Off;
				}
				if (auditOperation.Equals(tuple.Item2))
				{
					return AuditSwitchStatus.On;
				}
			}
			return AuditSwitchStatus.None;
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00025E60 File Offset: 0x00024060
		internal static MultiValuedProperty<AuditableOperations> GetAuditOperations(Workload workload, AuditSwitchStatus auditSwitch)
		{
			if (auditSwitch == AuditSwitchStatus.On)
			{
				return AuditConfigUtility.auditMap[workload].Item2;
			}
			return AuditConfigUtility.auditMap[workload].Item1;
		}

		// Token: 0x040003D4 RID: 980
		private static readonly List<Workload> auditableWorkloads = new List<Workload>
		{
			Workload.Exchange,
			Workload.SharePoint,
			Workload.OneDriveForBusiness
		};

		// Token: 0x040003D5 RID: 981
		private static readonly MultiValuedProperty<AuditableOperations> exchangeAuditOff = new MultiValuedProperty<AuditableOperations>
		{
			AuditableOperations.None
		};

		// Token: 0x040003D6 RID: 982
		private static readonly MultiValuedProperty<AuditableOperations> exchangeAuditOn = new MultiValuedProperty<AuditableOperations>
		{
			AuditableOperations.Administrate,
			AuditableOperations.CreateUpdate,
			AuditableOperations.Forward,
			AuditableOperations.MoveCopy,
			AuditableOperations.Search,
			AuditableOperations.SendAsOthers
		};

		// Token: 0x040003D7 RID: 983
		private static readonly MultiValuedProperty<AuditableOperations> sharePointAuditOff = new MultiValuedProperty<AuditableOperations>
		{
			AuditableOperations.None
		};

		// Token: 0x040003D8 RID: 984
		private static readonly MultiValuedProperty<AuditableOperations> sharePointAuditOn = new MultiValuedProperty<AuditableOperations>
		{
			AuditableOperations.CheckIn,
			AuditableOperations.CheckOut,
			AuditableOperations.CreateUpdate,
			AuditableOperations.Delete,
			AuditableOperations.MoveCopy,
			AuditableOperations.PermissionChange,
			AuditableOperations.ProfileChange,
			AuditableOperations.SchemaChange,
			AuditableOperations.Search,
			AuditableOperations.Workflow
		};

		// Token: 0x040003D9 RID: 985
		private static readonly MultiValuedProperty<AuditableOperations> oneDriveAuditOff = new MultiValuedProperty<AuditableOperations>
		{
			AuditableOperations.None
		};

		// Token: 0x040003DA RID: 986
		private static readonly MultiValuedProperty<AuditableOperations> oneDriveAuditOn = new MultiValuedProperty<AuditableOperations>
		{
			AuditableOperations.CreateUpdate,
			AuditableOperations.Delete,
			AuditableOperations.MoveCopy
		};

		// Token: 0x040003DB RID: 987
		private static readonly Dictionary<Workload, Tuple<MultiValuedProperty<AuditableOperations>, MultiValuedProperty<AuditableOperations>>> auditMap = new Dictionary<Workload, Tuple<MultiValuedProperty<AuditableOperations>, MultiValuedProperty<AuditableOperations>>>
		{
			{
				Workload.SharePoint,
				Tuple.Create<MultiValuedProperty<AuditableOperations>, MultiValuedProperty<AuditableOperations>>(AuditConfigUtility.sharePointAuditOff, AuditConfigUtility.sharePointAuditOn)
			},
			{
				Workload.Exchange,
				Tuple.Create<MultiValuedProperty<AuditableOperations>, MultiValuedProperty<AuditableOperations>>(AuditConfigUtility.exchangeAuditOff, AuditConfigUtility.exchangeAuditOn)
			},
			{
				Workload.OneDriveForBusiness,
				Tuple.Create<MultiValuedProperty<AuditableOperations>, MultiValuedProperty<AuditableOperations>>(AuditConfigUtility.oneDriveAuditOff, AuditConfigUtility.oneDriveAuditOn)
			}
		};
	}
}
