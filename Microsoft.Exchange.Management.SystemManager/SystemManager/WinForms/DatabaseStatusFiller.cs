using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000067 RID: 103
	internal class DatabaseStatusFiller : MonadAdapterFiller
	{
		// Token: 0x060003D7 RID: 983 RVA: 0x0000E114 File Offset: 0x0000C314
		public DatabaseStatusFiller(string commandText, ICommandBuilder builder) : base(commandText, builder)
		{
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000E11E File Offset: 0x0000C31E
		public override void BuildCommand(string searchText, object[] pipeline, DataRow row)
		{
			this.BuildCommandWithScope(searchText, pipeline, row, null);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000E12A File Offset: 0x0000C32A
		public override void BuildCommandWithScope(string searchText, object[] pipeline, DataRow row, object scope)
		{
			this.DatabaseTable = (row["Databases"] as DataTable);
			base.BuildCommandWithScope(searchText, null, row, scope);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000E39C File Offset: 0x0000C59C
		private IEnumerable<string> GetCommandScripts()
		{
			if (this.DatabaseTable != null)
			{
				if (this.DatabaseTable.Rows.Count == 1)
				{
					yield return string.Format("{0} -Identity '{1}' | Filter-PropertyEqualTo -Property 'ActiveCopy' -Value $true", base.GetExecutingCommandText(), this.DatabaseTable.Rows[0]["Identity"].ToQuotationEscapedString());
				}
				else
				{
					foreach (string serverName in this.GetMailboxServers())
					{
						yield return base.Command.CommandText = string.Format("{0} -Server '{1}' | Filter-PropertyEqualTo -Property 'ActiveCopy' -Value $true", base.GetExecutingCommandText(), serverName.ToQuotationEscapedString());
					}
				}
			}
			yield break;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000E3BC File Offset: 0x0000C5BC
		private IEnumerable<string> GetMailboxServers()
		{
			HashSet<string> hashSet = new HashSet<string>();
			if (this.DatabaseTable != null)
			{
				foreach (object obj in this.DatabaseTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					DatabaseCopy[] array = dataRow["DatabaseCopies"] as DatabaseCopy[];
					if (array != null)
					{
						foreach (DatabaseCopy databaseCopy in array)
						{
							if (databaseCopy != null && !string.IsNullOrEmpty(databaseCopy.HostServerName))
							{
								hashSet.Add(databaseCopy.HostServerName);
							}
						}
					}
				}
			}
			return hashSet;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000E47C File Offset: 0x0000C67C
		private DataTable CreateDatabaseCopyEntryTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("DatabaseName", typeof(string));
			dataTable.Columns.Add("Status", typeof(CopyStatus));
			dataTable.Columns.Add("MailboxServer", typeof(string));
			dataTable.PrimaryKey = new DataColumn[]
			{
				dataTable.Columns["DatabaseName"]
			};
			return dataTable;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000E614 File Offset: 0x0000C814
		protected override void OnFill(DataTable table)
		{
			if (this.DatabaseTable != null)
			{
				using (DataTable databaseCopiesTable = this.CreateDatabaseCopyEntryTable())
				{
					databaseCopiesTable.RowChanged += delegate(object sender, DataRowChangeEventArgs rowChangedEvent)
					{
						if (rowChangedEvent.Action == DataRowAction.Add)
						{
							DataRow dataRow = this.FindDatabaseRowByName(rowChangedEvent.Row["DatabaseName"].ToString());
							if (dataRow != null)
							{
								bool? flag = this.ComputeDatabaseStatus(rowChangedEvent.Row);
								if (flag != null)
								{
									dataRow["Mounted"] = flag.Value;
								}
								else
								{
									dataRow["Mounted"] = DBNull.Value;
								}
								dataRow["MountedOnServer"] = rowChangedEvent.Row["MailboxServer"].ToString();
								table.Rows.Add(dataRow.ItemArray);
								this.DatabaseTable.Rows.Remove(dataRow);
							}
							databaseCopiesTable.Clear();
						}
					};
					this.FillTableWithDatabaseCopyEntries(databaseCopiesTable);
				}
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000E6A8 File Offset: 0x0000C8A8
		private DataRow FindDatabaseRowByName(string databaseName)
		{
			DataRow result = null;
			if (this.DatabaseTable != null && !string.IsNullOrEmpty(databaseName))
			{
				DataRow[] array = this.DatabaseTable.Select(string.Format("{0} = '{1}'", "Name", databaseName.ToQuotationEscapedString()));
				if (array != null && array.Length > 0)
				{
					result = array[0];
				}
			}
			return result;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000E6F8 File Offset: 0x0000C8F8
		private void FillTableWithDatabaseCopyEntries(DataTable table)
		{
			base.Command.CommandType = CommandType.Text;
			foreach (string commandText in this.GetCommandScripts())
			{
				base.Command.CommandText = commandText;
				using (MonadDataAdapter monadDataAdapter = new MonadDataAdapter(base.Command))
				{
					if (table.Columns.Count != 0)
					{
						monadDataAdapter.MissingSchemaAction = MissingSchemaAction.Ignore;
						monadDataAdapter.EnforceDataSetSchema = true;
					}
					monadDataAdapter.Fill(table);
				}
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000E7A0 File Offset: 0x0000C9A0
		private bool? ComputeDatabaseStatus(DataRow dataRow)
		{
			bool? result = null;
			if (!dataRow["Status"].IsNullValue())
			{
				switch ((CopyStatus)dataRow["Status"])
				{
				case CopyStatus.Mounted:
				case CopyStatus.Mounting:
					result = new bool?(true);
					break;
				case CopyStatus.Dismounted:
				case CopyStatus.Dismounting:
					result = new bool?(false);
					break;
				}
			}
			return result;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000E808 File Offset: 0x0000CA08
		public override object Clone()
		{
			return new DatabaseStatusFiller(base.CommandText, this.CommandBuilder)
			{
				ResolveCommandText = base.ResolveCommandText,
				IsResolving = base.IsResolving
			};
		}

		// Token: 0x04000103 RID: 259
		private const string DatabaseName = "DatabaseName";

		// Token: 0x04000104 RID: 260
		private const string DatabaseCopyStatus = "Status";

		// Token: 0x04000105 RID: 261
		private const string DatabaseCopyMailboxServer = "MailboxServer";

		// Token: 0x04000106 RID: 262
		private const string CommandScriptPerServer = "{0} -Server '{1}' | Filter-PropertyEqualTo -Property 'ActiveCopy' -Value $true";

		// Token: 0x04000107 RID: 263
		private const string CommandScriptPerDatabase = "{0} -Identity '{1}' | Filter-PropertyEqualTo -Property 'ActiveCopy' -Value $true";

		// Token: 0x04000108 RID: 264
		private const string EqualFilterString = "{0} = '{1}'";

		// Token: 0x04000109 RID: 265
		private DataTable DatabaseTable;
	}
}
