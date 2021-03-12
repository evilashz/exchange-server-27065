using System;
using System.Data;
using System.Text;
using Microsoft.Exchange.Configuration.MonadDataProvider;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000016 RID: 22
	internal class CommandLoggingSession : IDisposable
	{
		// Token: 0x0600014D RID: 333 RVA: 0x00005934 File Offset: 0x00003B34
		public void Dispose()
		{
			this.table.Dispose();
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00005941 File Offset: 0x00003B41
		// (set) Token: 0x0600014F RID: 335 RVA: 0x0000594C File Offset: 0x00003B4C
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

		// Token: 0x06000150 RID: 336 RVA: 0x000059C8 File Offset: 0x00003BC8
		private void RemoveOldestRow()
		{
			if (this.table.Rows.Count > 0)
			{
				this.table.Rows.RemoveAt(0);
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000059EE File Offset: 0x00003BEE
		public static bool IsValidMaximumRecordCount(int value)
		{
			return 1 <= value && value <= CommandLoggingSession.MaximumRecordCountLimit;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005A04 File Offset: 0x00003C04
		public void Clear()
		{
			lock (this.mutex)
			{
				this.table.Rows.Clear();
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005A50 File Offset: 0x00003C50
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

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00005B6A File Offset: 0x00003D6A
		public DataView LoggingData
		{
			get
			{
				return new DataView(this.table);
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005B78 File Offset: 0x00003D78
		public static CommandLoggingSession GetInstance()
		{
			if (CommandLoggingSession.instance == null)
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

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00005BD0 File Offset: 0x00003DD0
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00005BD8 File Offset: 0x00003DD8
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

		// Token: 0x06000158 RID: 344 RVA: 0x00005C1C File Offset: 0x00003E1C
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

		// Token: 0x06000159 RID: 345 RVA: 0x00005CC8 File Offset: 0x00003EC8
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

		// Token: 0x0600015A RID: 346 RVA: 0x00005D88 File Offset: 0x00003F88
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

		// Token: 0x0600015B RID: 347 RVA: 0x00005E48 File Offset: 0x00004048
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

		// Token: 0x0600015C RID: 348 RVA: 0x00005EF8 File Offset: 0x000040F8
		public static void ErrorReport(object sender, ErrorReportEventArgs e)
		{
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005EFA File Offset: 0x000040FA
		public static void WarningReport(object sender, WarningReportEventArgs e)
		{
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005EFC File Offset: 0x000040FC
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
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005FAC File Offset: 0x000041AC
		public static void EndExecution(object sender, RunGuidEventArgs e)
		{
		}

		// Token: 0x04000044 RID: 68
		internal static readonly string warning = "Warning";

		// Token: 0x04000045 RID: 69
		internal static readonly string error = "Error";

		// Token: 0x04000046 RID: 70
		internal static readonly string startExecutionTime = "StartExecutionTime";

		// Token: 0x04000047 RID: 71
		internal static readonly string endExecutionTime = "EndExecutionTime";

		// Token: 0x04000048 RID: 72
		internal static readonly string executionStatus = "ExecutionStatus";

		// Token: 0x04000049 RID: 73
		internal static readonly string command = "Command";

		// Token: 0x0400004A RID: 74
		private DataTable table;

		// Token: 0x0400004B RID: 75
		private object mutex = new object();

		// Token: 0x0400004C RID: 76
		private static object entryLock = new object();

		// Token: 0x0400004D RID: 77
		internal static readonly int MaximumRecordCountLimit = 32767;

		// Token: 0x0400004E RID: 78
		internal static readonly int DefaultMaximumRecordCount = 2048;

		// Token: 0x0400004F RID: 79
		private int maximumRecordCount;

		// Token: 0x04000050 RID: 80
		private static CommandLoggingSession instance;

		// Token: 0x04000051 RID: 81
		private bool commandLoggingEnabled;
	}
}
