using System;
using System.Data;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Management.SnapIn;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000167 RID: 359
	public class TableDataHandler : ExchangeDataHandler
	{
		// Token: 0x06000E9C RID: 3740 RVA: 0x00037FE4 File Offset: 0x000361E4
		public TableDataHandler(string selectCommandText, string updateCommandText, string insertCommandText, string deleteCommandText)
		{
			this.DataTable = new DataTable();
			this.dataAdapter = new MonadDataAdapter();
			this.dataAdapter.EnforceDataSetSchema = true;
			MonadConnection connection = new MonadConnection("timeout=30", new CommandInteractionHandler(), ADServerSettingsSingleton.GetInstance().CreateRunspaceServerSettingsObject(), PSConnectionInfoSingleton.GetInstance().GetMonadConnectionInfo());
			this.dataAdapter.SelectCommand = new LoggableMonadCommand(selectCommandText, connection);
			this.dataAdapter.UpdateCommand = new LoggableMonadCommand(updateCommandText, connection);
			this.dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;
			this.dataAdapter.DeleteCommand = new LoggableMonadCommand(deleteCommandText, connection);
			this.dataAdapter.InsertCommand = new LoggableMonadCommand(insertCommandText, connection);
			this.dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
			this.synchronizationContext = SynchronizationContext.Current;
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000E9D RID: 3741 RVA: 0x000380B3 File Offset: 0x000362B3
		// (set) Token: 0x06000E9E RID: 3742 RVA: 0x000380BB File Offset: 0x000362BB
		protected DataTable DataTable
		{
			get
			{
				return this.dataTable;
			}
			set
			{
				if (this.dataTable != value)
				{
					this.dataTable = value;
					base.DataSource = this.dataTable;
				}
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000E9F RID: 3743 RVA: 0x000380D9 File Offset: 0x000362D9
		internal MonadParameterCollection SelectCommandParameters
		{
			get
			{
				return this.dataAdapter.SelectCommand.Parameters;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x000380EB File Offset: 0x000362EB
		internal MonadParameterCollection UpdateCommandParameters
		{
			get
			{
				return this.dataAdapter.UpdateCommand.Parameters;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x000380FD File Offset: 0x000362FD
		internal MonadParameterCollection InsertCommandParameters
		{
			get
			{
				return this.dataAdapter.InsertCommand.Parameters;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x0003810F File Offset: 0x0003630F
		internal MonadParameterCollection DeleteCommandParameters
		{
			get
			{
				return this.dataAdapter.DeleteCommand.Parameters;
			}
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x00038124 File Offset: 0x00036324
		internal override void OnReadData(CommandInteractionHandler interactionHandler, string pageName)
		{
			DataTable dataTable = this.DataTable.Clone();
			this.OnFillTable(dataTable, interactionHandler);
			this.synchronizationContext.Send(new SendOrPostCallback(this.CopyChangeFromTableForRead), dataTable);
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0003815D File Offset: 0x0003635D
		internal virtual void OnFillTable(DataTable table, CommandInteractionHandler interactionHandler)
		{
			this.dataAdapter.SelectCommand.Connection.InteractionHandler = interactionHandler;
			this.dataAdapter.Fill(table);
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00038184 File Offset: 0x00036384
		internal override void OnSaveData(CommandInteractionHandler interactionHandler)
		{
			this.dataAdapter.SelectCommand.Connection.InteractionHandler = interactionHandler;
			DataTable state = this.CreateUpdateTable();
			try
			{
				this.dataAdapter.Update(state);
			}
			finally
			{
				this.synchronizationContext.Send(new SendOrPostCallback(this.CopyChangeFromTableForUpdate), state);
			}
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x000381E8 File Offset: 0x000363E8
		private void CopyChangeFromTableForRead(object arg)
		{
			DataTable table = (DataTable)arg;
			if (this.DataTable.HasErrors)
			{
				DataRow[] errors = this.DataTable.GetErrors();
				table.ImportRows(errors);
			}
			this.DataTable.Clear();
			this.DataTable.Merge(table);
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x00038238 File Offset: 0x00036438
		private void CopyChangeFromTableForUpdate(object arg)
		{
			DataTable table = (DataTable)arg;
			this.DataTable.Clear();
			this.DataTable.Merge(table);
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x00038264 File Offset: 0x00036464
		private void FillParametersFromDataRow(MonadCommand command, DataRow row)
		{
			DataRowVersion version = (row.RowState == DataRowState.Deleted) ? DataRowVersion.Original : DataRowVersion.Current;
			foreach (object obj in command.Parameters)
			{
				MonadParameter monadParameter = (MonadParameter)obj;
				if (row.Table.Columns.Contains(monadParameter.ParameterName))
				{
					monadParameter.Value = row[monadParameter.ParameterName, version];
				}
			}
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x000382F8 File Offset: 0x000364F8
		protected virtual DataTable CreateUpdateTable()
		{
			DataTable result = this.DataTable.Clone();
			DataRow[] array = this.DataTable.Select(null, this.DataTable.DefaultView.Sort, DataViewRowState.Deleted);
			DataRow[] array2 = this.DataTable.Select(null, this.DataTable.DefaultView.Sort, DataViewRowState.CurrentRows);
			DataRow[] array3 = new DataRow[array.Length + array2.Length];
			Array.Copy(array, array3, array.Length);
			Array.Copy(array2, 0, array3, array.Length, array2.Length);
			result.ImportRows(array3);
			return result;
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x0003837C File Offset: 0x0003657C
		internal override string CommandToRun
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				MonadCommand monadCommand = this.dataAdapter.InsertCommand.Clone();
				MonadCommand monadCommand2 = this.dataAdapter.DeleteCommand.Clone();
				MonadCommand monadCommand3 = this.dataAdapter.UpdateCommand.Clone();
				DataTable dataTable = this.CreateUpdateTable();
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					DataRowState rowState = dataRow.RowState;
					if (rowState != DataRowState.Added)
					{
						if (rowState != DataRowState.Deleted)
						{
							if (rowState == DataRowState.Modified)
							{
								this.FillParametersFromDataRow(monadCommand3, dataRow);
								stringBuilder.AppendLine(monadCommand3.ToString());
							}
						}
						else
						{
							this.FillParametersFromDataRow(monadCommand2, dataRow);
							stringBuilder.AppendLine(monadCommand2.ToString());
						}
					}
					else
					{
						this.FillParametersFromDataRow(monadCommand, dataRow);
						stringBuilder.AppendLine(monadCommand.ToString());
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x040005E5 RID: 1509
		private DataTable dataTable;

		// Token: 0x040005E6 RID: 1510
		private MonadDataAdapter dataAdapter;

		// Token: 0x040005E7 RID: 1511
		private SynchronizationContext synchronizationContext;
	}
}
