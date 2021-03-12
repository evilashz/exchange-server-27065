using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200019C RID: 412
	public abstract class WSDataHandler : AutomatedDataHandlerBase
	{
		// Token: 0x17001ABF RID: 6847
		// (get) Token: 0x06002311 RID: 8977 RVA: 0x00069C89 File Offset: 0x00067E89
		// (set) Token: 0x06002312 RID: 8978 RVA: 0x00069C91 File Offset: 0x00067E91
		public Workflow ExecutingWorkflow { get; private set; }

		// Token: 0x06002313 RID: 8979 RVA: 0x00069C9A File Offset: 0x00067E9A
		public WSDataHandler(Service service, string workflowName) : base(service)
		{
			this.Construct(workflowName);
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x00069CAA File Offset: 0x00067EAA
		public WSDataHandler(string schemaFilesInstallPath, string resourceName, string workflowName, DDIParameters parameters = null) : base(schemaFilesInstallPath, resourceName)
		{
			this.Construct(workflowName);
			this.parameters = parameters;
		}

		// Token: 0x17001AC0 RID: 6848
		// (get) Token: 0x06002315 RID: 8981 RVA: 0x00069CC3 File Offset: 0x00067EC3
		// (set) Token: 0x06002316 RID: 8982 RVA: 0x00069CCB File Offset: 0x00067ECB
		public string OutputVariableWorkflow { get; set; }

		// Token: 0x17001AC1 RID: 6849
		// (get) Token: 0x06002317 RID: 8983 RVA: 0x00069CD4 File Offset: 0x00067ED4
		// (set) Token: 0x06002318 RID: 8984 RVA: 0x00069CF9 File Offset: 0x00067EF9
		internal string UniqueLogonUserIdentity
		{
			get
			{
				if (string.IsNullOrEmpty(this.uniqueLogonUserIdentity))
				{
					this.uniqueLogonUserIdentity = HttpContext.Current.GetCachedUserUniqueKey();
				}
				return this.uniqueLogonUserIdentity;
			}
			set
			{
				this.uniqueLogonUserIdentity = value;
			}
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x00069D02 File Offset: 0x00067F02
		public void InputIdentity(params Identity[] identities)
		{
			if (identities != null && identities.Length > 0)
			{
				this.InputValue("Identity", (identities.Length == 1) ? identities[0] : identities);
			}
		}

		// Token: 0x0600231A RID: 8986 RVA: 0x00069D98 File Offset: 0x00067F98
		public PowerShellResults<JsonDictionary<object>> Execute()
		{
			PowerShellResults<JsonDictionary<object>> result;
			using (EcpPerformanceData.DDIServiceExecution.StartRequestTimer())
			{
				switch (this.ExecutingWorkflow.AsyncMode)
				{
				case AsyncMode.AsynchronousOnly:
					this.ExecutingWorkflow.AsyncRunning = true;
					break;
				case AsyncMode.SynchronousAndAsynchronous:
					this.ExecutingWorkflow.AsyncRunning = (base.DataObjectStore.AsyncType == ListAsyncType.GetListAsync || base.DataObjectStore.AsyncType == ListAsyncType.GetListPreLoad);
					break;
				}
				if (!this.ExecutingWorkflow.AsyncRunning || DDIHelper.ForGetListProgress)
				{
					result = this.ExecuteCore(this.ExecutingWorkflow);
				}
				else
				{
					if (!typeof(ProgressCalculatorBase).IsAssignableFrom(this.ExecutingWorkflow.ProgressCalculator))
					{
						throw new ArgumentException("A valid ProgressCalculator type must be specified for async running workflow.");
					}
					Func<PowerShellResults> callback = delegate()
					{
						IList<string> outputVariables = this.GetOutputVariables(this.OutputVariableWorkflow);
						AsyncGetListContext asyncGetListContext = new AsyncGetListContext
						{
							WorkflowOutput = string.Join(",", outputVariables ?? ((IList<string>)Array<string>.Empty)),
							UnicodeOutputColumnNames = this.GetUnicodeVariablesFrom(outputVariables),
							Parameters = this.parameters
						};
						AsyncServiceManager.RegisterWorkflow(this.ExecutingWorkflow, asyncGetListContext);
						return this.ExecuteCore(this.ExecutingWorkflow);
					};
					AsyncTaskType taskType = AsyncTaskType.Default;
					string text = this.ExecutingWorkflow.Name;
					if (base.DataObjectStore.AsyncType == ListAsyncType.GetListAsync)
					{
						taskType = AsyncTaskType.AsyncGetList;
					}
					else if (base.DataObjectStore.AsyncType == ListAsyncType.GetListPreLoad)
					{
						taskType = AsyncTaskType.AsyncGetListPreLoad;
						text += "_PreLoad";
					}
					PowerShellResults powerShellResults = AsyncServiceManager.InvokeAsync(callback, null, this.UniqueLogonUserIdentity, taskType, text);
					result = new PowerShellResults<JsonDictionary<object>>
					{
						ProgressId = powerShellResults.ProgressId,
						ErrorRecords = powerShellResults.ErrorRecords
					};
				}
			}
			return result;
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x00069F0C File Offset: 0x0006810C
		public virtual void InputValue(string columnName, object value)
		{
			Variable variable = base.Table.Columns[columnName].ExtendedProperties["Variable"] as Variable;
			if (variable != null && variable.InputConverter != null && variable.InputConverter.CanConvert(value))
			{
				value = variable.InputConverter.Convert(value);
			}
			DDIHelper.Trace("      Initial value: " + EcpTraceExtensions.FormatParameterValue(value) + ", Column: " + columnName);
			base.Input[columnName] = value;
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x00069FAC File Offset: 0x000681AC
		public IList<string> GetOutputVariables(string referWorkflowName)
		{
			IEnumerable<Workflow> source = from c in this.workflows
			where c.Name == referWorkflowName
			select c;
			IList<string> outputVariables = this.GetWorkFlow(this.executingWorkflowName).GetOutputVariables();
			if (outputVariables == null && 0 < source.Count<Workflow>())
			{
				return source.First<Workflow>().GetOutputVariables();
			}
			return outputVariables;
		}

		// Token: 0x0600231D RID: 8989 RVA: 0x0006A00C File Offset: 0x0006820C
		public List<string> GetUnicodeVariablesFrom(IList<string> variables)
		{
			List<string> list = new List<string>((variables != null) ? variables.Count : 0);
			if (variables != null)
			{
				foreach (Variable variable in base.ProfileBuilder.Variables)
				{
					if (variable.UnicodeString && variables.Contains(variable.Name))
					{
						list.Add(variable.Name);
					}
				}
			}
			return list;
		}

		// Token: 0x0600231E RID: 8990 RVA: 0x0006A0AC File Offset: 0x000682AC
		public Workflow GetWorkFlow(string workflowName)
		{
			IEnumerable<Workflow> source = from c in this.workflows
			where c.Name == workflowName
			select c;
			if (source.Count<Workflow>() != 1)
			{
				throw new WorkflowNotExistException(workflowName);
			}
			return source.First<Workflow>();
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x0006A0FC File Offset: 0x000682FC
		protected void ExtractDataRow(DataRow row, IEnumerable<DataColumn> columns, out Dictionary<string, object> output)
		{
			output = new Dictionary<string, object>();
			foreach (DataColumn dataColumn in columns)
			{
				Variable variable = dataColumn.ExtendedProperties["Variable"] as Variable;
				if (variable != null)
				{
					base.FillColumnsBasedOnLambdaExpression(row, variable);
					output[dataColumn.ColumnName] = DDIHelper.PrepareVariableForSerialization(row[dataColumn.ColumnName], variable);
				}
			}
		}

		// Token: 0x06002320 RID: 8992
		protected abstract PowerShellResults<JsonDictionary<object>> ExecuteCore(Workflow workflow);

		// Token: 0x06002321 RID: 8993 RVA: 0x0006A184 File Offset: 0x00068384
		private void Construct(string workflowName)
		{
			this.executingWorkflowName = workflowName;
			this.workflows = base.ProfileBuilder.Workflows;
			this.ExtendTableSchema();
			this.ExecutingWorkflow = this.GetWorkFlow(this.executingWorkflowName);
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x0006A1B6 File Offset: 0x000683B6
		private void ExtendTableSchema()
		{
			base.Table.Columns.AddRange(base.ProfileBuilder.ExtendedColumns.Distinct(new WSDataHandler.DataColumnComparer()).ToArray<DataColumn>());
		}

		// Token: 0x04001DB1 RID: 7601
		private IList<Workflow> workflows;

		// Token: 0x04001DB2 RID: 7602
		private string executingWorkflowName;

		// Token: 0x04001DB3 RID: 7603
		private string uniqueLogonUserIdentity;

		// Token: 0x04001DB4 RID: 7604
		private DDIParameters parameters;

		// Token: 0x0200019D RID: 413
		private class DataColumnComparer : IEqualityComparer<DataColumn>
		{
			// Token: 0x06002324 RID: 8996 RVA: 0x0006A1E2 File Offset: 0x000683E2
			public bool Equals(DataColumn x, DataColumn y)
			{
				return object.ReferenceEquals(x, y) || (!object.ReferenceEquals(x, null) && !object.ReferenceEquals(y, null) && x.ColumnName == y.ColumnName);
			}

			// Token: 0x06002325 RID: 8997 RVA: 0x0006A214 File Offset: 0x00068414
			public int GetHashCode(DataColumn column)
			{
				if (object.ReferenceEquals(column, null))
				{
					return 0;
				}
				return (column.ColumnName == null) ? 0 : column.ColumnName.GetHashCode();
			}
		}
	}
}
