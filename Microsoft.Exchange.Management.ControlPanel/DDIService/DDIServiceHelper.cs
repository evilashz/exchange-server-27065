using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000123 RID: 291
	public class DDIServiceHelper
	{
		// Token: 0x06002062 RID: 8290 RVA: 0x00061CBD File Offset: 0x0005FEBD
		public DDIServiceHelper(string schema, string workflow) : this(DDIService.DDIPath, schema, workflow)
		{
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06002063 RID: 8291 RVA: 0x00061CCC File Offset: 0x0005FECC
		// (remove) Token: 0x06002064 RID: 8292 RVA: 0x00061D04 File Offset: 0x0005FF04
		internal event EventHandler<ExecutingWorkflowEventArgs> WSDataHandlerCreated;

		// Token: 0x06002065 RID: 8293 RVA: 0x00061D39 File Offset: 0x0005FF39
		internal DDIServiceHelper(string schemaFilesInstallPath, string schema, string workflow)
		{
			this.schemaFilesInstallPath = schemaFilesInstallPath;
			this.schema = schema;
			this.workflow = workflow;
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x00061D56 File Offset: 0x0005FF56
		internal PowerShellResults<JsonDictionary<object>> GetList(DDIParameters filter, SortOptions sort)
		{
			return this.GetListCommon(filter, sort, false);
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x00061D64 File Offset: 0x0005FF64
		internal PowerShellResults<JsonDictionary<object>> GetObject(Identity identity)
		{
			string text = this.workflow ?? "GetObject";
			return this.Get(text, identity);
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x00061D89 File Offset: 0x0005FF89
		internal PowerShellResults<JsonDictionary<object>> GetObjectOnDemand(Identity identity)
		{
			return this.Get(this.workflow, identity);
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x00061D98 File Offset: 0x0005FF98
		internal PowerShellResults<JsonDictionary<object>> GetObjectForNew(Identity identity)
		{
			string text = this.workflow ?? "GetObjectForNew";
			return this.Get(text, identity);
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x00061DC0 File Offset: 0x0005FFC0
		internal PowerShellResults<JsonDictionary<object>> SetObject(Identity identity, DDIParameters properties)
		{
			properties.FaultIfNull("properties");
			string text = this.workflow ?? "SetObject";
			return this.Execute(text, (identity == null) ? null : new Identity[]
			{
				identity
			}, properties);
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x00061E08 File Offset: 0x00060008
		internal PowerShellResults<JsonDictionary<object>> GetProgress(string progressId, bool async)
		{
			string text = this.workflow ?? "GetProgress";
			if (!string.IsNullOrEmpty(this.schema) && this.IsWorkflowDefined(text))
			{
				return this.ExecuteProgressWorkflow(text, progressId);
			}
			if (async)
			{
				DDIHelper.ProgressIdForGetListAsync = progressId;
				DDIParameters ddiparameters = new DDIParameters();
				AsyncGetListContext registeredContext = AsyncServiceManager.GetRegisteredContext(progressId);
				Dictionary<string, object> dictionary;
				if (registeredContext != null && registeredContext.Parameters != null && registeredContext.Parameters.Parameters != null)
				{
					dictionary = registeredContext.Parameters.Parameters.RawDictionary;
				}
				else
				{
					dictionary = new Dictionary<string, object>();
				}
				if (registeredContext != null && !string.IsNullOrEmpty(registeredContext.WorkflowOutput))
				{
					dictionary["workflowOutput"] = registeredContext.WorkflowOutput;
				}
				dictionary["ProgressId"] = progressId;
				ddiparameters.Parameters = new JsonDictionary<object>(dictionary);
				return this.GetListCommon(ddiparameters, null, true);
			}
			return AsyncServiceManager.GetProgress(progressId);
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x00061ED8 File Offset: 0x000600D8
		internal PowerShellResults Cancel(string progressId)
		{
			string text = this.workflow ?? "Cancel";
			if (!string.IsNullOrEmpty(this.schema) && this.IsWorkflowDefined(text))
			{
				return this.ExecuteProgressWorkflow(text, progressId);
			}
			return AsyncServiceManager.Cancel(progressId);
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x00061F1C File Offset: 0x0006011C
		internal PowerShellResults<JsonDictionary<object>> NewObject(DDIParameters properties)
		{
			string text = this.workflow ?? "NewObject";
			return this.Execute(text, null, properties);
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x00061F44 File Offset: 0x00060144
		internal PowerShellResults RemoveObjects(Identity[] identities, DDIParameters parameters)
		{
			if (identities == null)
			{
				throw new FaultException(new ArgumentNullException("identities").Message);
			}
			this.ValidateIdentities(identities);
			string text = this.workflow ?? "RemoveObjects";
			return new PowerShellResults(this.Execute(text, identities, parameters));
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x00061F8E File Offset: 0x0006018E
		internal PowerShellResults<JsonDictionary<object>> MultiObjectExecute(Identity[] identities, DDIParameters parameters)
		{
			this.ValidateIdentities(identities);
			return this.Execute(this.workflow, identities, parameters);
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x00061FA8 File Offset: 0x000601A8
		internal PowerShellResults<JsonDictionary<object>> SingleObjectExecute(Identity identity, DDIParameters properties)
		{
			return this.Execute(this.workflow, (identity == null) ? null : new Identity[]
			{
				identity
			}, properties);
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x00061FDC File Offset: 0x000601DC
		private PowerShellResults<JsonDictionary<object>> GetListCommon(DDIParameters filter, SortOptions sort, bool forGetProgress)
		{
			string workflowName = this.workflow ?? "GetList";
			WSDataHandler wsdataHandler = new WSListDataHandler(this.schemaFilesInstallPath, this.schema, workflowName, filter, sort);
			this.RaiseWSDataHandlerCreatedEvent(wsdataHandler);
			this.SetWorkflowOutput(workflowName, wsdataHandler, filter);
			wsdataHandler.DataObjectStore.SetModifiedColumns(this.InitializeDDIParameters(wsdataHandler, filter));
			wsdataHandler.DataObjectStore.IsGetListWorkflow = true;
			if (filter != null && filter.Parameters != null && filter.Parameters.ContainsKey("ProgressId") && !forGetProgress)
			{
				wsdataHandler.DataObjectStore.AsyncType = ListAsyncType.GetListEndLoad;
				DDIHelper.ProgressIdForGetListAsync = (string)filter.Parameters["ProgressId"];
			}
			else if (DDIHelper.IsGetListAsync)
			{
				wsdataHandler.DataObjectStore.AsyncType = ListAsyncType.GetListAsync;
			}
			else if (DDIHelper.IsGetListPreLoad)
			{
				wsdataHandler.DataObjectStore.AsyncType = ListAsyncType.GetListPreLoad;
			}
			PowerShellResults<JsonDictionary<object>> powerShellResults = wsdataHandler.Execute();
			if (powerShellResults.HasWarnings)
			{
				for (int i = 0; i < powerShellResults.Warnings.Length; i++)
				{
					if (powerShellResults.Warnings[i] == Strings.WarningMoreResultsAvailable)
					{
						powerShellResults.Warnings[i] = ClientStrings.ListViewMoreResultsWarning;
					}
				}
			}
			return powerShellResults;
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x00062104 File Offset: 0x00060304
		private PowerShellResults<JsonDictionary<object>> Execute(string workflow, Identity[] identities, DDIParameters properties)
		{
			WSDataHandler wsdataHandler = new WSSingleObjectDataHandler(this.schemaFilesInstallPath, this.schema, workflow);
			this.RaiseWSDataHandlerCreatedEvent(wsdataHandler);
			if (identities != null)
			{
				wsdataHandler.InputIdentity(identities);
			}
			wsdataHandler.OutputVariableWorkflow = "GetList";
			wsdataHandler.DataObjectStore.SetModifiedColumns(this.InitializeDDIParameters(wsdataHandler, properties));
			return wsdataHandler.Execute();
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x00062160 File Offset: 0x00060360
		private PowerShellResults<JsonDictionary<object>> Get(string workflow, Identity identity)
		{
			string workflowName = string.IsNullOrEmpty(workflow) ? "GetObject" : workflow;
			WSDataHandler wsdataHandler = new WSSingleObjectDataHandler(this.schemaFilesInstallPath, this.schema, workflowName);
			this.RaiseWSDataHandlerCreatedEvent(wsdataHandler);
			if (identity != null)
			{
				wsdataHandler.InputIdentity(new Identity[]
				{
					identity
				});
			}
			return wsdataHandler.Execute();
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x000621BD File Offset: 0x000603BD
		private void RaiseWSDataHandlerCreatedEvent(WSDataHandler dataHandler)
		{
			if (this.WSDataHandlerCreated != null)
			{
				this.WSDataHandlerCreated(this, new ExecutingWorkflowEventArgs(dataHandler.ExecutingWorkflow));
			}
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x000621E0 File Offset: 0x000603E0
		private List<string> InitializeDDIParameters(WSDataHandler dataHandler, DDIParameters parameters)
		{
			List<string> list = new List<string>();
			if (parameters != null && parameters.Parameters != null)
			{
				Dictionary<string, object> dictionary = parameters.Parameters;
				foreach (string text in dictionary.Keys)
				{
					if (dataHandler.Table.Columns.Contains(text))
					{
						dataHandler.InputValue(text, dictionary[text]);
						list.Add(text);
					}
				}
			}
			return list;
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x00062274 File Offset: 0x00060474
		private void ValidateIdentities(Identity[] identities)
		{
			int num = 0;
			if (int.TryParse(ConfigurationManager.AppSettings["MaximumIdentities"], out num) && identities != null && identities.Length > num)
			{
				throw new FaultException(new ArgumentOutOfRangeException("identities").Message);
			}
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x000622D8 File Offset: 0x000604D8
		private bool IsWorkflowDefined(string workflowName)
		{
			DDIHelper.Trace("IsWorkflowDefined: Schema:{0} WorkflowName:{1}", new object[]
			{
				this.schema,
				workflowName
			});
			bool flag = (from c in ServiceManager.GetInstance().GetService(this.schemaFilesInstallPath, this.schema).Workflows
			where c.Name.Equals(workflowName, StringComparison.InvariantCultureIgnoreCase)
			select c).Count<Workflow>() > 0;
			DDIHelper.Trace("IsWorkflowDefined({0}) returning {1}", new object[]
			{
				workflowName,
				flag
			});
			return flag;
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x00062370 File Offset: 0x00060570
		private PowerShellResults<JsonDictionary<object>> ExecuteProgressWorkflow(string executingWorkflowName, string progressId)
		{
			DDIParameters ddiparameters = new DDIParameters();
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["ProgressId"] = progressId;
			ddiparameters.Parameters = new JsonDictionary<object>(dictionary);
			return this.Execute(executingWorkflowName, null, ddiparameters);
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x000623AC File Offset: 0x000605AC
		private void SetWorkflowOutput(string workflowName, WSDataHandler dataHandler, DDIParameters filter)
		{
			Workflow workFlow = dataHandler.GetWorkFlow(workflowName);
			string text = null;
			if (filter != null && filter.Parameters != null)
			{
				Dictionary<string, object> dictionary = filter.Parameters;
				if (dictionary.ContainsKey("workflowOutput"))
				{
					text = (string)dictionary["workflowOutput"];
				}
				else if (dictionary.ContainsKey("AdditionalCustomOutputs"))
				{
					text = workFlow.Output + ',' + dictionary["AdditionalCustomOutputs"];
				}
			}
			if (!string.IsNullOrWhiteSpace(text) && workFlow != null)
			{
				workFlow.Output = text;
			}
		}

		// Token: 0x04001CD3 RID: 7379
		private const string GetObjectString = "GetObject";

		// Token: 0x04001CD4 RID: 7380
		private const string ProgressIdString = "ProgressId";

		// Token: 0x04001CD5 RID: 7381
		private const string FilterString = "filter";

		// Token: 0x04001CD6 RID: 7382
		private const string SortString = "sort";

		// Token: 0x04001CD7 RID: 7383
		private const string ASC = "ASC";

		// Token: 0x04001CD8 RID: 7384
		private const string DESC = "DESC";

		// Token: 0x04001CD9 RID: 7385
		private const string GetObjectForNewString = "GetObjectForNew";

		// Token: 0x04001CDA RID: 7386
		private const string SetObjectString = "SetObject";

		// Token: 0x04001CDB RID: 7387
		private const string NewObjectString = "NewObject";

		// Token: 0x04001CDC RID: 7388
		private const string GetProgressString = "GetProgress";

		// Token: 0x04001CDD RID: 7389
		private const string CancelString = "Cancel";

		// Token: 0x04001CDE RID: 7390
		private const string RemoveObjectsString = "RemoveObjects";

		// Token: 0x04001CDF RID: 7391
		public const string WorkflowOutput = "workflowOutput";

		// Token: 0x04001CE0 RID: 7392
		public const string AdditionalCustomOutputs = "AdditionalCustomOutputs";

		// Token: 0x04001CE1 RID: 7393
		private readonly string schema;

		// Token: 0x04001CE2 RID: 7394
		private readonly string workflow;

		// Token: 0x04001CE3 RID: 7395
		private readonly string schemaFilesInstallPath;
	}
}
