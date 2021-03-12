using System;
using System.Data.SqlClient;
using System.IO;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000D3 RID: 211
	public abstract class ManageSqlDatabase : Task
	{
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x0001B0EA File Offset: 0x000192EA
		// (set) Token: 0x06000653 RID: 1619 RVA: 0x0001B101 File Offset: 0x00019301
		[Parameter(Mandatory = true)]
		public string DatabaseName
		{
			get
			{
				return (string)base.Fields["DatabaseName"];
			}
			set
			{
				base.Fields["DatabaseName"] = value;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x0001B114 File Offset: 0x00019314
		// (set) Token: 0x06000655 RID: 1621 RVA: 0x0001B12B File Offset: 0x0001932B
		[Parameter(Mandatory = false)]
		public string ServerName
		{
			get
			{
				return (string)base.Fields["ServerName"];
			}
			set
			{
				base.Fields["ServerName"] = value;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x0001B13E File Offset: 0x0001933E
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x0001B155 File Offset: 0x00019355
		[Parameter(Mandatory = false)]
		public string MirrorServerName
		{
			get
			{
				return (string)base.Fields["MirrorServerName"];
			}
			set
			{
				base.Fields["MirrorServerName"] = value;
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0001B168 File Offset: 0x00019368
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (!base.Fields.IsModified("ServerName"))
			{
				this.ServerName = "localhost";
			}
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0001B198 File Offset: 0x00019398
		protected void Install(string databasePath, string logPath)
		{
			TaskLogger.LogEnter();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("create database {0}", this.DatabaseName);
			if (!string.IsNullOrEmpty(databasePath))
			{
				string arg = Path.Combine(databasePath, string.Format("{0}.mdf", this.DatabaseName));
				stringBuilder.AppendFormat(" ON (NAME = {0}, FILENAME = '{1}')", this.DatabaseName, arg);
			}
			if (!string.IsNullOrEmpty(logPath))
			{
				string arg2 = Path.Combine(logPath, string.Format("{0}_log.ldf", this.DatabaseName));
				stringBuilder.AppendFormat(" LOG ON (NAME = {0}_log, FILENAME = '{1}')", this.DatabaseName, arg2);
			}
			this.ExecuteCommand(stringBuilder.ToString(), "Master", false, 0);
			this.ExecuteCommand("exec sp_changedbowner 'sa'", this.DatabaseName, false, 0);
			TaskLogger.LogExit();
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0001B251 File Offset: 0x00019451
		protected void Uninstall()
		{
			TaskLogger.LogEnter();
			this.ExecuteCommand(string.Format("drop database {0}", this.DatabaseName), "Master", false, 0);
			TaskLogger.LogExit();
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0001B27A File Offset: 0x0001947A
		protected void ExecuteCommand(string commandToExecute, bool executeScalar, int timeout)
		{
			TaskLogger.LogEnter();
			this.ExecuteCommand(commandToExecute, this.DatabaseName, executeScalar, timeout);
			TaskLogger.LogExit();
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0001B298 File Offset: 0x00019498
		protected void ExecuteCommand(string commandToExecute, string databaseName, bool executeScalar, int timeout)
		{
			TaskLogger.LogEnter();
			try
			{
				using (SqlConnection sqlConnection = new SqlConnection(this.GetConnectionString(databaseName)))
				{
					sqlConnection.Open();
					this.ExecuteCommand(commandToExecute, databaseName, sqlConnection, executeScalar, timeout);
					sqlConnection.Close();
				}
			}
			catch (SqlException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001B30C File Offset: 0x0001950C
		private void ExecuteCommand(string commandToExecute, string databaseName, SqlConnection connection, bool executeScalar, int timeout)
		{
			TaskLogger.LogEnter();
			if (!string.IsNullOrEmpty(commandToExecute))
			{
				try
				{
					SqlCommand sqlCommand = new SqlCommand(commandToExecute, connection);
					if (timeout > 0)
					{
						sqlCommand.CommandTimeout = timeout;
					}
					object sendToPipeline;
					if (executeScalar)
					{
						sendToPipeline = sqlCommand.ExecuteScalar();
					}
					else
					{
						sendToPipeline = sqlCommand.ExecuteNonQuery();
					}
					base.WriteObject(sendToPipeline);
				}
				catch (SqlException exception)
				{
					base.WriteError(exception, ErrorCategory.InvalidOperation, null);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0001B380 File Offset: 0x00019580
		private string GetConnectionString(string databaseName)
		{
			return string.Format("Data Source={0};Initial Catalog={1};Integrated Security=SSPI;Failover Partner={2}", this.ServerName, databaseName, this.MirrorServerName);
		}
	}
}
