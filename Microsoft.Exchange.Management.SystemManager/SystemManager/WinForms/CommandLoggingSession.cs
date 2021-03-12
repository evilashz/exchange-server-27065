using System;
using System.Data;
using System.Text;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200011E RID: 286
	internal class CommandLoggingSession : IDisposable
	{
		// Token: 0x06000AF2 RID: 2802 RVA: 0x00027438 File Offset: 0x00025638
		public void Dispose()
		{
			this.table.Dispose();
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x00027445 File Offset: 0x00025645
		// (set) Token: 0x06000AF4 RID: 2804 RVA: 0x00027450 File Offset: 0x00025650
		public int MaximumRecordCount
		{
			get
			{
				return this.maximumRecordCount;
			}
			set
			{
				if (!CommandLoggingSession.IsValidMaximumRecordCount(value))
				{
					throw new ArgumentOutOfRangeException(Strings.InvalidMaximumRecordNumber(CommandLoggingSession.MaximumRecordCountLimit), null);
				}
				lock (this.mutex)
				{
					while (this.table.Rows.Count > value)
					{
						this.RemoveOldestRow();
					}
					this.maximumRecordCount = value;
				}
			}
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x000274CC File Offset: 0x000256CC
		private void RemoveOldestRow()
		{
			if (this.table.Rows.Count > 0)
			{
				this.table.Rows.RemoveAt(0);
			}
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x000274F2 File Offset: 0x000256F2
		public static bool IsValidMaximumRecordCount(int value)
		{
			return 1 <= value && value <= CommandLoggingSession.MaximumRecordCountLimit;
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00027508 File Offset: 0x00025708
		public void Clear()
		{
			lock (this.mutex)
			{
				this.table.Rows.Clear();
			}
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00027554 File Offset: 0x00025754
		internal CommandLoggingSession()
		{
			this.table = new DataTable();
			this.table.Columns.Add(CommandLoggingSession.startExecutionTime, typeof(DateTime));
			this.table.Columns.Add(CommandLoggingSession.endExecutionTime, typeof(DateTime));
			this.table.Columns.Add(CommandLoggingSession.command).DefaultValue = string.Empty;
			this.table.Columns.Add(CommandLoggingSession.executionStatus).DefaultValue = string.Empty;
			this.table.Columns.Add(CommandLoggingSession.error).DefaultValue = string.Empty;
			this.table.Columns.Add(CommandLoggingSession.warning).DefaultValue = string.Empty;
			this.table.PrimaryKey = new DataColumn[]
			{
				this.table.Columns.Add("Identity", typeof(Guid))
			};
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x0002766E File Offset: 0x0002586E
		public DataView LoggingData
		{
			get
			{
				return new DataView(this.table);
			}
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0002767C File Offset: 0x0002587C
		public static CommandLoggingSession GetInstance()
		{
			if (CommandLoggingDialog.instance == null)
			{
				lock (CommandLoggingSession.entryLock)
				{
					if (CommandLoggingSession.instance == null)
					{
						CommandLoggingSession.instance = new CommandLoggingSession();
					}
				}
			}
			return CommandLoggingSession.instance;
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x000276D4 File Offset: 0x000258D4
		// (set) Token: 0x06000AFC RID: 2812 RVA: 0x000276DC File Offset: 0x000258DC
		public bool CommandLoggingEnabled
		{
			get
			{
				return this.commandLoggingEnabled;
			}
			set
			{
				lock (this.mutex)
				{
					this.commandLoggingEnabled = value;
				}
			}
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00027720 File Offset: 0x00025920
		public void LogStart(Guid guid, DateTime startTime, string commandText)
		{
			lock (this.mutex)
			{
				if (this.commandLoggingEnabled)
				{
					if (this.table.Rows.Count >= this.maximumRecordCount)
					{
						this.RemoveOldestRow();
					}
					DataRow dataRow = this.table.NewRow();
					dataRow["Identity"] = guid;
					dataRow[CommandLoggingSession.startExecutionTime] = startTime;
					dataRow[CommandLoggingSession.command] = commandText;
					this.table.Rows.Add(dataRow);
				}
			}
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x000277CC File Offset: 0x000259CC
		public void LogWarning(Guid guid, string warning)
		{
			lock (this.mutex)
			{
				if (this.commandLoggingEnabled)
				{
					DataRow dataRow = this.table.Rows.Find(guid);
					if (dataRow != null)
					{
						if (!string.IsNullOrEmpty((string)dataRow[CommandLoggingSession.warning]))
						{
							DataRow dataRow2;
							string columnName;
							(dataRow2 = dataRow)[columnName = CommandLoggingSession.warning] = dataRow2[columnName] + "\r\n";
						}
						DataRow dataRow3;
						string columnName2;
						(dataRow3 = dataRow)[columnName2 = CommandLoggingSession.warning] = dataRow3[columnName2] + Strings.WarningUpperCase(warning);
					}
				}
			}
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0002788C File Offset: 0x00025A8C
		public void LogError(Guid guid, string error)
		{
			lock (this.mutex)
			{
				if (this.commandLoggingEnabled)
				{
					DataRow dataRow = this.table.Rows.Find(guid);
					if (dataRow != null)
					{
						if (!string.IsNullOrEmpty((string)dataRow[CommandLoggingSession.error]))
						{
							DataRow dataRow2;
							string columnName;
							(dataRow2 = dataRow)[columnName = CommandLoggingSession.error] = dataRow2[columnName] + "\r\n";
						}
						DataRow dataRow3;
						string columnName2;
						(dataRow3 = dataRow)[columnName2 = CommandLoggingSession.error] = dataRow3[columnName2] + Strings.ErrorUpperCase(error);
					}
				}
			}
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0002794C File Offset: 0x00025B4C
		public void LogEnd(Guid guid, DateTime endTime)
		{
			lock (this.mutex)
			{
				if (this.commandLoggingEnabled)
				{
					DataRow dataRow = this.table.Rows.Find(guid);
					if (dataRow != null)
					{
						dataRow[CommandLoggingSession.endExecutionTime] = endTime;
						if (!string.IsNullOrEmpty((string)dataRow[CommandLoggingSession.error]))
						{
							dataRow[CommandLoggingSession.executionStatus] = Strings.ExecutionError;
						}
						else
						{
							dataRow[CommandLoggingSession.executionStatus] = Strings.ExecutionCompleted;
						}
					}
				}
			}
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x000279FC File Offset: 0x00025BFC
		public static void ErrorReport(object sender, ErrorReportEventArgs e)
		{
			CommandLoggingDialog.LogError(e.Guid, e.ErrorRecord.Exception.Message);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00027A19 File Offset: 0x00025C19
		public static void WarningReport(object sender, WarningReportEventArgs e)
		{
			CommandLoggingDialog.LogWarning(e.Guid, e.WarningMessage);
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00027A2C File Offset: 0x00025C2C
		public static void StartExecution(object sender, StartExecutionEventArgs e)
		{
			MonadCommand monadCommand = sender as MonadCommand;
			StringBuilder stringBuilder = new StringBuilder();
			if (e.Pipeline != null)
			{
				int num = 0;
				foreach (object value in e.Pipeline)
				{
					if (num != 0)
					{
						stringBuilder.Append(",");
					}
					num++;
					stringBuilder.Append(MonadCommand.FormatParameterValue(value));
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" | ");
				}
			}
			stringBuilder.Append(monadCommand.ToString());
			CommandLoggingDialog.LogStart(e.Guid, (DateTime)ExDateTime.Now, stringBuilder.ToString());
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00027AF4 File Offset: 0x00025CF4
		public static void EndExecution(object sender, RunGuidEventArgs e)
		{
			CommandLoggingDialog.LogEnd(e.Guid, (DateTime)ExDateTime.Now);
		}

		// Token: 0x0400048F RID: 1167
		internal static readonly string warning = "Warning";

		// Token: 0x04000490 RID: 1168
		internal static readonly string error = "Error";

		// Token: 0x04000491 RID: 1169
		internal static readonly string startExecutionTime = "StartExecutionTime";

		// Token: 0x04000492 RID: 1170
		internal static readonly string endExecutionTime = "EndExecutionTime";

		// Token: 0x04000493 RID: 1171
		internal static readonly string executionStatus = "ExecutionStatus";

		// Token: 0x04000494 RID: 1172
		internal static readonly string command = "Command";

		// Token: 0x04000495 RID: 1173
		private DataTable table;

		// Token: 0x04000496 RID: 1174
		private object mutex = new object();

		// Token: 0x04000497 RID: 1175
		private static object entryLock = new object();

		// Token: 0x04000498 RID: 1176
		internal static readonly int MaximumRecordCountLimit = 32767;

		// Token: 0x04000499 RID: 1177
		internal static readonly int DefaultMaximumRecordCount = 2048;

		// Token: 0x0400049A RID: 1178
		private int maximumRecordCount;

		// Token: 0x0400049B RID: 1179
		private static CommandLoggingSession instance;

		// Token: 0x0400049C RID: 1180
		private bool commandLoggingEnabled;
	}
}
