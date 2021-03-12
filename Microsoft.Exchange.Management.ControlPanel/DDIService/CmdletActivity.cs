using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000103 RID: 259
	public abstract class CmdletActivity : ParameterActivity
	{
		// Token: 0x06001F4E RID: 8014 RVA: 0x0005E1B0 File Offset: 0x0005C3B0
		public CmdletActivity()
		{
			this.StatusReport = new PowerShellResults();
			this.IdentityName = "Identity";
			this.IdentityVariable = "Identity";
			this.ShouldContinueParam = "Force";
			this.ShouldContinueOperation = ShouldContinueOperation.AddParam;
			this.DisableLogging = false;
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x0005E200 File Offset: 0x0005C400
		protected CmdletActivity(CmdletActivity activity) : base(activity)
		{
			this.StatusReport = activity.StatusReport;
			this.DataObjectName = activity.DataObjectName;
			this.CommandText = activity.CommandText;
			this.SnapInAlias = activity.SnapInAlias;
			this.IdentityVariable = activity.IdentityVariable;
			this.IdentityName = activity.IdentityName;
			this.ShouldContinueParam = activity.ShouldContinueParam;
			this.ShouldContinueOperation = activity.ShouldContinueOperation;
			this.AllowExceuteThruHttpGetRequest = activity.AllowExceuteThruHttpGetRequest;
			this.DisableLogging = activity.DisableLogging;
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x0005E294 File Offset: 0x0005C494
		public sealed override bool IsRunnable(DataRow input, DataTable dataTable, DataObjectStore store)
		{
			if (AsyncServiceManager.IsCurrentWorkCancelled())
			{
				return false;
			}
			List<Parameter> parametersToInvoke = this.GetParametersToInvoke(input, dataTable, store);
			bool flag = this.IsToExecuteCmdlet(input, dataTable, store, parametersToInvoke);
			if (!flag)
			{
				return false;
			}
			List<string> parameters = (from x in parametersToInvoke
			select x.Name).ToList<string>();
			string cmdletRbacString = this.GetCmdletRbacString(store, parameters);
			bool flag2 = base.RbacChecker.IsInRole(cmdletRbacString);
			DDIHelper.Trace("Checking RBAC: {0} : {1}", new object[]
			{
				cmdletRbacString,
				flag2
			});
			if (!flag2)
			{
				throw new CmdletAccessDeniedException(Strings.AccessDeniedMessage);
			}
			return true;
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x0005E338 File Offset: 0x0005C538
		public override IList<Parameter> GetEffectiveParameters(DataRow input, DataTable dataTable, DataObjectStore store)
		{
			IList<Parameter> list = base.GetEffectiveParameters(input, dataTable, store);
			list = (list ?? new List<Parameter>());
			ShouldContinueContext shouldContinueContext = base.EacHttpContext.ShouldContinueContext;
			if (shouldContinueContext == null && dataTable.Columns.Contains("ShouldContinue") && dataTable.Rows.Count > 0)
			{
				bool? flag = dataTable.Rows[0]["ShouldContinue"] as bool?;
				if (flag.IsTrue())
				{
					shouldContinueContext = (dataTable.Rows[0]["LastErrorContext"] as ShouldContinueContext);
					base.EacHttpContext.ShouldContinueContext = shouldContinueContext;
				}
			}
			if (shouldContinueContext != null && shouldContinueContext.CmdletsPrompted.Contains(this.GetCommandText(store)))
			{
				if (this.ShouldContinueOperation == ShouldContinueOperation.AddParam)
				{
					list.Add(new Parameter
					{
						Name = this.ShouldContinueParam,
						Type = ParameterType.Switch
					});
				}
				else if (this.ShouldContinueOperation == ShouldContinueOperation.RemoveParam)
				{
					list.Remove(new Parameter
					{
						Name = this.ShouldContinueParam,
						Type = ParameterType.Switch
					});
				}
			}
			return list;
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x0005E44F File Offset: 0x0005C64F
		protected virtual bool IsToExecuteCmdlet(DataRow input, DataTable dataTable, DataObjectStore store, List<Parameter> parameters)
		{
			return true;
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x0005E470 File Offset: 0x0005C670
		protected virtual List<Parameter> GetParametersToInvoke(DataRow input, DataTable dataTable, DataObjectStore store)
		{
			return (from c in this.GetEffectiveParameters(input, dataTable, store)
			where c.IsRunnable(input, dataTable)
			select c).ToList<Parameter>();
		}

		// Token: 0x17001A06 RID: 6662
		// (get) Token: 0x06001F54 RID: 8020 RVA: 0x0005E4BA File Offset: 0x0005C6BA
		// (set) Token: 0x06001F55 RID: 8021 RVA: 0x0005E4C2 File Offset: 0x0005C6C2
		public PowerShellResults StatusReport { get; set; }

		// Token: 0x17001A07 RID: 6663
		// (get) Token: 0x06001F56 RID: 8022 RVA: 0x0005E4CB File Offset: 0x0005C6CB
		// (set) Token: 0x06001F57 RID: 8023 RVA: 0x0005E4D3 File Offset: 0x0005C6D3
		[DDIDataObjectNameExist]
		public string DataObjectName { get; set; }

		// Token: 0x17001A08 RID: 6664
		// (get) Token: 0x06001F58 RID: 8024 RVA: 0x0005E4DC File Offset: 0x0005C6DC
		// (set) Token: 0x06001F59 RID: 8025 RVA: 0x0005E4E4 File Offset: 0x0005C6E4
		[DDIValidCommandText]
		public string CommandText { get; set; }

		// Token: 0x17001A09 RID: 6665
		// (get) Token: 0x06001F5A RID: 8026 RVA: 0x0005E4ED File Offset: 0x0005C6ED
		// (set) Token: 0x06001F5B RID: 8027 RVA: 0x0005E4F5 File Offset: 0x0005C6F5
		[DefaultValue("Force")]
		public string ShouldContinueParam { get; set; }

		// Token: 0x17001A0A RID: 6666
		// (get) Token: 0x06001F5C RID: 8028 RVA: 0x0005E4FE File Offset: 0x0005C6FE
		// (set) Token: 0x06001F5D RID: 8029 RVA: 0x0005E506 File Offset: 0x0005C706
		[DefaultValue(ShouldContinueOperation.AddParam)]
		public ShouldContinueOperation ShouldContinueOperation { get; set; }

		// Token: 0x17001A0B RID: 6667
		// (get) Token: 0x06001F5E RID: 8030 RVA: 0x0005E50F File Offset: 0x0005C70F
		// (set) Token: 0x06001F5F RID: 8031 RVA: 0x0005E517 File Offset: 0x0005C717
		[DefaultValue(false)]
		public bool DisableLogging { get; set; }

		// Token: 0x17001A0C RID: 6668
		// (get) Token: 0x06001F60 RID: 8032 RVA: 0x0005E520 File Offset: 0x0005C720
		// (set) Token: 0x06001F61 RID: 8033 RVA: 0x0005E528 File Offset: 0x0005C728
		public string SnapInAlias { get; set; }

		// Token: 0x17001A0D RID: 6669
		// (get) Token: 0x06001F62 RID: 8034 RVA: 0x0005E531 File Offset: 0x0005C731
		// (set) Token: 0x06001F63 RID: 8035 RVA: 0x0005E539 File Offset: 0x0005C739
		public bool AllowExceuteThruHttpGetRequest { get; set; }

		// Token: 0x17001A0E RID: 6670
		// (get) Token: 0x06001F64 RID: 8036 RVA: 0x0005E542 File Offset: 0x0005C742
		// (set) Token: 0x06001F65 RID: 8037 RVA: 0x0005E54A File Offset: 0x0005C74A
		[DDIVariableNameExist]
		public string IdentityVariable { get; set; }

		// Token: 0x17001A0F RID: 6671
		// (get) Token: 0x06001F66 RID: 8038 RVA: 0x0005E553 File Offset: 0x0005C753
		// (set) Token: 0x06001F67 RID: 8039 RVA: 0x0005E55B File Offset: 0x0005C75B
		public string IdentityName { get; set; }

		// Token: 0x17001A10 RID: 6672
		// (get) Token: 0x06001F68 RID: 8040 RVA: 0x0005E564 File Offset: 0x0005C764
		// (set) Token: 0x06001F69 RID: 8041 RVA: 0x0005E56C File Offset: 0x0005C76C
		internal object DataObject { get; set; }

		// Token: 0x17001A11 RID: 6673
		// (get) Token: 0x06001F6A RID: 8042 RVA: 0x0005E575 File Offset: 0x0005C775
		// (set) Token: 0x06001F6B RID: 8043 RVA: 0x0005E57D File Offset: 0x0005C77D
		internal IPSCommandWrapper Command { get; set; }

		// Token: 0x06001F6C RID: 8044 RVA: 0x0005E594 File Offset: 0x0005C794
		public override PowerShellResults[] GetStatusReport(DataRow input, DataTable dataTable, DataObjectStore store)
		{
			if (this.StatusReport != null)
			{
				if (base.ErrorBehavior == ErrorBehavior.SilentlyContinue)
				{
					this.StatusReport.ErrorRecords = Array<Microsoft.Exchange.Management.ControlPanel.ErrorRecord>.Empty;
				}
				else if (base.ErrorBehavior == ErrorBehavior.ErrorAsWarning && !this.StatusReport.ErrorRecords.IsNullOrEmpty())
				{
					string[] array = Array.ConvertAll<Microsoft.Exchange.Management.ControlPanel.ErrorRecord, string>(this.StatusReport.ErrorRecords, (Microsoft.Exchange.Management.ControlPanel.ErrorRecord x) => x.Exception.Message);
					this.StatusReport.Warnings = (this.StatusReport.Warnings.IsNullOrEmpty() ? array : this.StatusReport.Warnings.Concat(array).ToArray<string>());
					this.StatusReport.ErrorRecords = Array<Microsoft.Exchange.Management.ControlPanel.ErrorRecord>.Empty;
				}
			}
			return new PowerShellResults[]
			{
				this.StatusReport
			};
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x0005E670 File Offset: 0x0005C870
		public override RunResult Run(DataRow input, DataTable dataTable, DataObjectStore store, Type codeBehind, Workflow.UpdateTableDelegate updateTableDelegate)
		{
			RunResult runResult = new RunResult();
			PowerShellResults<PSObject> powerShellResults;
			this.ExecuteCmdlet(null, runResult, out powerShellResults, false);
			return runResult;
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x0005E690 File Offset: 0x0005C890
		internal void ExecuteCmdlet(IEnumerable pipelineInput, RunResult runResult, out PowerShellResults<PSObject> result, bool isGetListAsync = false)
		{
			DDIHelper.Trace(TraceType.InfoTrace, "Executing :" + base.GetType().Name);
			DDIHelper.Trace(TraceType.InfoTrace, "Task: {0}", new object[]
			{
				this.Command
			});
			DDIHelper.Trace(TraceType.InfoTrace, "Pipeline: {0}", new object[]
			{
				pipelineInput
			});
			WebServiceParameters parameters = null;
			if (this.AllowExceuteThruHttpGetRequest)
			{
				parameters = CmdletActivity.allowExceuteThruHttpGetRequestParameters;
			}
			result = this.Command.Invoke<PSObject>(DataSourceService.UserRunspaces, pipelineInput, parameters, this, isGetListAsync);
			if (this.DisableLogging)
			{
				result.CmdletLogInfo = null;
			}
			this.StatusReport = result;
			runResult.ErrorOccur = !this.StatusReport.Succeeded;
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x0005E73E File Offset: 0x0005C93E
		protected virtual string GetVerb()
		{
			return null;
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x0005E741 File Offset: 0x0005C941
		protected override void DoPreRunCore(DataRow input, DataTable dataTable, DataObjectStore store, Type codeBehind)
		{
			this.BuildCommand(input, dataTable, store, codeBehind);
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x0005E750 File Offset: 0x0005C950
		internal void BuildCommand(DataRow input, DataTable dataTable, DataObjectStore store, Type codeBehind)
		{
			this.Command = base.PowershellFactory.CreatePSCommand().AddCommand(this.GetCommandText(store));
			foreach (Parameter parameter in this.GetEffectiveParameters(input, dataTable, store))
			{
				if (parameter.IsRunnable(input, dataTable))
				{
					switch (parameter.Type)
					{
					case ParameterType.Switch:
						this.Command.AddParameter(parameter.Name);
						break;
					case ParameterType.Mandatory:
					case ParameterType.RunOnModified:
					{
						object value = Parameter.ConvertToParameterValue(input, dataTable, parameter, store);
						this.Command.AddParameter(parameter.Name, value);
						break;
					}
					default:
						throw new NotSupportedException();
					}
				}
			}
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x0005E838 File Offset: 0x0005CA38
		protected string GetCmdletRbacString(DataObjectStore store, IEnumerable<string> parameters)
		{
			string[] exculdedParameters = new string[]
			{
				this.ShouldContinueParam
			};
			parameters = from x in parameters
			where !exculdedParameters.Contains(x, StringComparer.OrdinalIgnoreCase)
			select x;
			if (parameters.Any<string>())
			{
				return this.GetRbacCommandText(store) + "?" + string.Join("&", parameters);
			}
			return this.GetRbacCommandText(store);
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x0005E8A2 File Offset: 0x0005CAA2
		protected string GetRbacCommandText(DataObjectStore store)
		{
			if (!string.IsNullOrEmpty(this.SnapInAlias))
			{
				return this.SnapInAlias + "\\" + this.GetCommandText(store);
			}
			return this.GetCommandText(store);
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x0005E8D0 File Offset: 0x0005CAD0
		protected string GetCommandText(DataObjectStore store)
		{
			if (string.IsNullOrEmpty(this.CommandText))
			{
				return this.GetVerb() + store.GetDataObjectType(this.DataObjectName).Name;
			}
			return this.CommandText;
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x0005E94C File Offset: 0x0005CB4C
		internal override bool HasPermission(DataRow input, DataTable dataTable, DataObjectStore store, Variable updatingVariable)
		{
			List<string> parameters;
			if (updatingVariable != null && !string.IsNullOrWhiteSpace(updatingVariable.MappingProperty))
			{
				parameters = (from c in this.GetEffectiveParameters(input, dataTable, store)
				where c.Type != ParameterType.RunOnModified || c.IsAccessingVariable(updatingVariable.MappingProperty)
				select c.Name).ToList<string>();
			}
			else
			{
				parameters = (from c in this.GetEffectiveParameters(input, dataTable, store)
				where c.IsRunnable(input, dataTable)
				select c.Name).ToList<string>();
			}
			return this.CheckPermission(store, parameters, updatingVariable);
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x0005EA48 File Offset: 0x0005CC48
		protected virtual bool CheckPermission(DataObjectStore store, List<string> parameters, Variable variable)
		{
			string name = this.DataObjectName;
			if (variable != null)
			{
				name = (variable.RbacDataObjectName ?? this.DataObjectName);
			}
			string cmdletRbacString = this.GetCmdletRbacString(store, parameters);
			ADRawEntry adrawEntry = store.IsDataObjectDummy(name) ? null : (store.GetDataObject(name) as ADRawEntry);
			bool flag = base.RbacChecker.IsInRole(cmdletRbacString, adrawEntry);
			DDIHelper.Trace("Checking {0} on object {1}: {2}", new object[]
			{
				cmdletRbacString,
				(adrawEntry != null) ? adrawEntry.ToString() : string.Empty,
				flag
			});
			return flag;
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x0005EAF4 File Offset: 0x0005CCF4
		internal override bool? FindAndCheckPermission(Func<Activity, bool> predicate, DataRow input, DataTable dataTable, DataObjectStore store, Variable updatingVariable)
		{
			IEnumerable<Activity> enumerable = new List<Activity>
			{
				this
			}.Where(predicate);
			bool? result = null;
			foreach (Activity activity in enumerable)
			{
				if (this is IReadOnlyChecker)
				{
					if (!base.Parameters.Any((Parameter p) => p.IsAccessingVariable(updatingVariable.Name)))
					{
						continue;
					}
				}
				result = new bool?(activity.HasPermission(input, dataTable, store, updatingVariable));
			}
			return result;
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x0005EBAC File Offset: 0x0005CDAC
		internal void OnPSProgressReport(ProgressReportEventArgs e)
		{
			this.ProgressPercent = e.Percent;
			this.OnPSProgressChanged(e);
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x0005EBC1 File Offset: 0x0005CDC1
		internal virtual void OnPSProgressChanged(ProgressReportEventArgs e)
		{
			if (this.PSProgressChanged != null)
			{
				this.PSProgressChanged(this, e);
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06001F7A RID: 8058 RVA: 0x0005EBD8 File Offset: 0x0005CDD8
		// (remove) Token: 0x06001F7B RID: 8059 RVA: 0x0005EC10 File Offset: 0x0005CE10
		public event EventHandler<ProgressReportEventArgs> PSProgressChanged;

		// Token: 0x04001C63 RID: 7267
		private static BaseWebServiceParameters allowExceuteThruHttpGetRequestParameters = new BaseWebServiceParameters
		{
			AllowExceuteThruHttpGetRequest = true
		};
	}
}
