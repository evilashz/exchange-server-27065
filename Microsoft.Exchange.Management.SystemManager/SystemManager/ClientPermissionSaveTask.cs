using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x020000B1 RID: 177
	public class ClientPermissionSaveTask : Saver
	{
		// Token: 0x060005A6 RID: 1446 RVA: 0x00015530 File Offset: 0x00013730
		public ClientPermissionSaveTask(string commandText, string workUnitTextColumn, string workUnitIconColumn) : base(workUnitTextColumn, workUnitIconColumn)
		{
			this.CommandText = commandText;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x000155B0 File Offset: 0x000137B0
		public ClientPermissionSaveTask() : base(null, null)
		{
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00015629 File Offset: 0x00013829
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x00015631 File Offset: 0x00013831
		[DefaultValue(null)]
		public string CommandText { get; set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0001563A File Offset: 0x0001383A
		public override string CommandToRun
		{
			get
			{
				return this.dataHandler.CommandToRun;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x00015647 File Offset: 0x00013847
		public override string ModifiedParametersDescription
		{
			get
			{
				return this.dataHandler.ModifiedParametersDescription;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x00015654 File Offset: 0x00013854
		public override object WorkUnits
		{
			get
			{
				return this.dataHandler.WorkUnits;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x00015661 File Offset: 0x00013861
		public override List<object> SavedResults
		{
			get
			{
				return this.dataHandler.SavedResults;
			}
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0001566E File Offset: 0x0001386E
		public override void Cancel()
		{
			this.dataHandler.Cancel();
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001567C File Offset: 0x0001387C
		public override bool IsRunnable(DataRow row, DataObjectStore store)
		{
			if (!base.IsRunnable(row, store))
			{
				return false;
			}
			DataTable values = this.GetValues(row);
			return values != null && values.Rows.Count > 0;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x000156B0 File Offset: 0x000138B0
		public override void BuildParameters(DataRow row, DataObjectStore store, IList<ParameterProfile> paramInfos)
		{
			this.paramInfos = paramInfos;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x000156BC File Offset: 0x000138BC
		public override void Run(object interactionHandler, DataRow row, DataObjectStore store)
		{
			this.dataHandler.ProgressReport += base.OnProgressReport;
			try
			{
				this.BuildCommandScript(row, this.paramInfos);
				this.dataHandler.Save(interactionHandler as CommandInteractionHandler);
			}
			finally
			{
				this.dataHandler.ProgressReport -= base.OnProgressReport;
			}
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00015728 File Offset: 0x00013928
		private DataTable GetValues(DataRow row)
		{
			return (row["ClientPermissionTable"] as DataTable).Copy();
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001573F File Offset: 0x0001393F
		public override void UpdateWorkUnits(DataRow row)
		{
			this.CreateDataHandlers(row);
			this.dataHandler.UpdateWorkUnits();
			this.UpdateWorkUnitsInfo(row);
			this.dataHandler.ResetCancel();
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00015768 File Offset: 0x00013968
		internal void UpdateConnection(MonadConnection connection)
		{
			foreach (DataHandler dataHandler in this.dataHandler.DataHandlers)
			{
				SingleTaskDataHandler singleTaskDataHandler = (SingleTaskDataHandler)dataHandler;
				singleTaskDataHandler.Command.Connection = connection;
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x000157C4 File Offset: 0x000139C4
		private void BuildCommandScript(DataRow row, IList<ParameterProfile> paramInfos)
		{
			DataRow dataRow = this.PrepareCombineRow(row);
			foreach (KeyValuePair<DataRow, SingleTaskDataHandler> keyValuePair in this.permissionItems)
			{
				this.CopyPermission(dataRow, keyValuePair.Key);
				keyValuePair.Value.CommandText = MonadPipelineSaveTask.BuildCommandScript(this.CommandText, dataRow, paramInfos);
			}
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0001583C File Offset: 0x00013A3C
		private void CreateDataHandlers(DataRow row)
		{
			DataTable values = this.GetValues(row);
			this.permissionItems.Clear();
			this.dataHandler.DataHandlers.Clear();
			if (values != null && values.Rows.Count > 0)
			{
				foreach (object obj in values.Rows)
				{
					DataRow key = (DataRow)obj;
					SingleTaskDataHandler singleTaskDataHandler = this.CreateDataHandler();
					this.permissionItems.Add(key, singleTaskDataHandler);
					this.dataHandler.DataHandlers.Add(singleTaskDataHandler);
				}
			}
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x000158EC File Offset: 0x00013AEC
		private SingleTaskDataHandler CreateDataHandler()
		{
			return new SingleTaskDataHandler
			{
				Command = 
				{
					CommandType = CommandType.Text
				}
			};
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0001590C File Offset: 0x00013B0C
		private void UpdateWorkUnitsInfo(DataRow row)
		{
			DataRow dataRow = this.PrepareCombineRow(row);
			foreach (KeyValuePair<DataRow, SingleTaskDataHandler> keyValuePair in this.permissionItems)
			{
				this.CopyPermission(dataRow, keyValuePair.Key);
				keyValuePair.Value.WorkUnit.Icon = this.GetDisplayIcon(dataRow);
				keyValuePair.Value.WorkUnit.Text = this.GetDisplayText(dataRow);
				if (this.paramInfos != null)
				{
					keyValuePair.Value.WorkUnit.Description = MonadSaveTask.BuildParametersDescription(dataRow, this.paramInfos);
				}
			}
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x000159C0 File Offset: 0x00013BC0
		private DataRow PrepareCombineRow(DataRow mainRow)
		{
			DataTable dataTable = mainRow.Table.Clone();
			foreach (KeyValuePair<string, string> keyValuePair in this.permissionColumnMap)
			{
				DataColumn column = new DataColumn(keyValuePair.Value, typeof(object));
				dataTable.Columns.Add(column);
			}
			DataRow dataRow = dataTable.NewRow();
			foreach (object obj in mainRow.Table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				dataRow[dataColumn.ColumnName] = mainRow[dataColumn];
			}
			dataTable.Rows.Add(dataRow);
			return dataRow;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00015AB4 File Offset: 0x00013CB4
		private void CopyPermission(DataRow combineRow, DataRow permissionRow)
		{
			combineRow.BeginEdit();
			foreach (KeyValuePair<string, string> keyValuePair in this.permissionColumnMap)
			{
				combineRow[keyValuePair.Value] = permissionRow[keyValuePair.Key];
			}
			combineRow.EndEdit();
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00015B28 File Offset: 0x00013D28
		private string GetDisplayText(DataRow row)
		{
			if (string.IsNullOrEmpty(base.WorkUnitTextColumn))
			{
				return null;
			}
			return row[base.WorkUnitTextColumn].ToString();
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00015B4A File Offset: 0x00013D4A
		private Icon GetDisplayIcon(DataRow row)
		{
			if (string.IsNullOrEmpty(base.WorkUnitIconColumn))
			{
				return null;
			}
			return WinformsHelper.GetIconFromIconLibrary(row[base.WorkUnitIconColumn].ToString());
		}

		// Token: 0x040001D5 RID: 469
		private const string ClientPermissionTableColumnName = "ClientPermissionTable";

		// Token: 0x040001D6 RID: 470
		private Dictionary<string, string> permissionColumnMap = new Dictionary<string, string>
		{
			{
				"Identity",
				"UserIdentity"
			},
			{
				"Name",
				"UserName"
			},
			{
				"RecipientTypeDetails",
				"UserTypes"
			},
			{
				"AccessRights",
				"AccessRights"
			}
		};

		// Token: 0x040001D7 RID: 471
		private DataHandler dataHandler = new DataHandler(false);

		// Token: 0x040001D8 RID: 472
		private IDictionary<DataRow, SingleTaskDataHandler> permissionItems = new Dictionary<DataRow, SingleTaskDataHandler>();

		// Token: 0x040001D9 RID: 473
		private IList<ParameterProfile> paramInfos;
	}
}
