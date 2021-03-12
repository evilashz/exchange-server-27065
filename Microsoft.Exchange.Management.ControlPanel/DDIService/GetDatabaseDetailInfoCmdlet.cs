using System;
using System.Collections.Generic;
using System.Data;
using System.Management.Automation;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000104 RID: 260
	public class GetDatabaseDetailInfoCmdlet : CmdletActivity
	{
		// Token: 0x06001F81 RID: 8065 RVA: 0x0005EC68 File Offset: 0x0005CE68
		public GetDatabaseDetailInfoCmdlet()
		{
			base.AllowExceuteThruHttpGetRequest = true;
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x0005EC77 File Offset: 0x0005CE77
		protected GetDatabaseDetailInfoCmdlet(GetDatabaseDetailInfoCmdlet activity) : base(activity)
		{
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x0005EC80 File Offset: 0x0005CE80
		public override Activity Clone()
		{
			return new GetDatabaseDetailInfoCmdlet(this);
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x0005EC88 File Offset: 0x0005CE88
		public override RunResult Run(DataRow input, DataTable dataTable, DataObjectStore store, Type codeBehind, Workflow.UpdateTableDelegate updateTableDelegate)
		{
			DDIHelper.Trace(TraceType.InfoTrace, "Executing :" + base.GetType().Name);
			object[] array = (object[])input["DeferLoadNames"];
			object[] array2 = (object[])input["DeferLoadIdentities"];
			List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
			DataRow row = dataTable.NewRow();
			dataTable.Rows.Add(row);
			dataTable.BeginLoadData();
			for (int i = 0; i < array2.Length; i++)
			{
				object value = array2[i];
				object arg = array[i];
				base.Command = base.PowershellFactory.CreatePSCommand().AddCommand(base.GetCommandText(store));
				base.Command.AddParameter("Identity", string.Format("{0}\\*", arg));
				DDIHelper.Trace<IPSCommandWrapper>(TraceType.InfoTrace, base.Command);
				RunResult runResult = new RunResult();
				PowerShellResults<PSObject> powerShellResults;
				base.ExecuteCmdlet(null, runResult, out powerShellResults, false);
				base.StatusReport = powerShellResults;
				runResult.ErrorOccur = !base.StatusReport.Succeeded;
				if (!runResult.ErrorOccur && powerShellResults.Output != null && powerShellResults.Output.Length > 0)
				{
					string value2 = Strings.StatusUnknown.ToString();
					bool flag = false;
					int num = 0;
					PSObject[] output = powerShellResults.Output;
					int j = 0;
					while (j < output.Length)
					{
						PSObject psobject = output[j];
						DatabaseCopyStatusEntry databaseCopyStatusEntry = (DatabaseCopyStatusEntry)psobject.BaseObject;
						CopyStatus status = databaseCopyStatusEntry.Status;
						if (status == CopyStatus.Failed || status == CopyStatus.ServiceDown)
						{
							goto IL_186;
						}
						switch (status)
						{
						case CopyStatus.DisconnectedAndHealthy:
						case CopyStatus.FailedAndSuspended:
						case CopyStatus.DisconnectedAndResynchronizing:
						case CopyStatus.Misconfigured:
							goto IL_186;
						}
						IL_18C:
						if (databaseCopyStatusEntry.ActiveCopy)
						{
							switch (databaseCopyStatusEntry.Status)
							{
							case CopyStatus.Mounted:
							case CopyStatus.Mounting:
								value2 = Strings.StatusMounted.ToString();
								flag = true;
								break;
							case CopyStatus.Dismounted:
							case CopyStatus.Dismounting:
								value2 = Strings.StatusDismounted.ToString();
								flag = false;
								break;
							}
						}
						Dictionary<string, object> dictionary = new Dictionary<string, object>();
						list.Add(dictionary);
						dictionary["Identity"] = value;
						dictionary["CalcualtedMountStatus"] = value2;
						dictionary["CalcualtedMounted"] = flag;
						dictionary["CalcualtedBadCopies"] = num;
						j++;
						continue;
						IL_186:
						num++;
						goto IL_18C;
					}
				}
			}
			dataTable.Clear();
			foreach (Dictionary<string, object> dictionary2 in list)
			{
				DataRow dataRow = dataTable.NewRow();
				dataTable.Rows.Add(dataRow);
				dataRow["Identity"] = dictionary2["Identity"];
				dataRow["CalcualtedMountStatus"] = dictionary2["CalcualtedMountStatus"];
				dataRow["CalcualtedMounted"] = dictionary2["CalcualtedMounted"];
				dataRow["CalcualtedBadCopies"] = dictionary2["CalcualtedBadCopies"];
			}
			dataTable.EndLoadData();
			return new RunResult();
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x0005EFBC File Offset: 0x0005D1BC
		protected override void DoPreRunCore(DataRow input, DataTable dataTable, DataObjectStore store, Type codeBehind)
		{
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x0005EFBE File Offset: 0x0005D1BE
		protected override string GetVerb()
		{
			return "Get-";
		}
	}
}
