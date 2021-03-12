using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x02000034 RID: 52
	public class ScheduledCheckTaskConfiguration
	{
		// Token: 0x06000107 RID: 263 RVA: 0x000086AC File Offset: 0x000068AC
		private ScheduledCheckTaskConfiguration()
		{
			this.isEnabled = false;
			this.taskIdsDetectOnly = new List<TaskId>(0);
			this.taskIdsDetectAndFix = new List<TaskId>(0);
			this.taskNamesDetectOnly = string.Empty;
			this.taskNamesDetectAndFix = string.Empty;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000086EC File Offset: 0x000068EC
		private ScheduledCheckTaskConfiguration(ScheduledCheckTaskConfiguration cachedConfig)
		{
			this.isEnabled = ConfigurationSchema.ScheduledISIntegEnabled.Value;
			this.taskNamesDetectOnly = cachedConfig.taskNamesDetectOnly;
			this.taskIdsDetectOnly = cachedConfig.taskIdsDetectOnly;
			this.taskNamesDetectAndFix = cachedConfig.taskNamesDetectAndFix;
			this.taskIdsDetectAndFix = cachedConfig.taskIdsDetectAndFix;
			if (this.isEnabled)
			{
				ScheduledCheckTaskConfiguration.ParseConfigurationString(ConfigurationSchema.ScheduledISIntegDetectOnly.Value, ref this.taskNamesDetectOnly, ref this.taskIdsDetectOnly);
				ScheduledCheckTaskConfiguration.ParseConfigurationString(ConfigurationSchema.ScheduledISIntegDetectAndFix.Value, ref this.taskNamesDetectAndFix, ref this.taskIdsDetectAndFix);
				return;
			}
			if (cachedConfig.isEnabled)
			{
				this.taskIdsDetectOnly = new List<TaskId>(0);
				this.taskIdsDetectAndFix = new List<TaskId>(0);
				this.taskNamesDetectOnly = string.Empty;
				this.taskNamesDetectAndFix = string.Empty;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000087B4 File Offset: 0x000069B4
		public bool IsEnabled
		{
			get
			{
				return this.isEnabled;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600010A RID: 266 RVA: 0x000087BC File Offset: 0x000069BC
		public List<TaskId> TaskIdsDetectOnly
		{
			get
			{
				return this.taskIdsDetectOnly;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000087C4 File Offset: 0x000069C4
		public List<TaskId> TaskIdsDetectAndFix
		{
			get
			{
				return this.taskIdsDetectAndFix;
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000087CC File Offset: 0x000069CC
		public static ScheduledCheckTaskConfiguration GetConfiguration()
		{
			ScheduledCheckTaskConfiguration result;
			using (LockManager.Lock(ScheduledCheckTaskConfiguration.configLock))
			{
				ScheduledCheckTaskConfiguration.instance = new ScheduledCheckTaskConfiguration(ScheduledCheckTaskConfiguration.instance);
				result = ScheduledCheckTaskConfiguration.instance;
			}
			return result;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000881C File Offset: 0x00006A1C
		internal static void ParseConfigurationString(string taskNamesParseNext, ref string taskNamesParseCached, ref List<TaskId> taskIds)
		{
			if (string.IsNullOrEmpty(taskNamesParseNext))
			{
				if (!string.IsNullOrEmpty(taskNamesParseCached))
				{
					taskIds = new List<TaskId>(0);
				}
			}
			else if (taskNamesParseCached != taskNamesParseNext || string.Compare(taskNamesParseCached, taskNamesParseNext, StringComparison.OrdinalIgnoreCase) != 0)
			{
				taskIds = new List<TaskId>(0);
				string[] array = taskNamesParseNext.Split(ScheduledCheckTaskConfiguration.taskListSeparator, StringSplitOptions.RemoveEmptyEntries);
				string text = string.Empty;
				foreach (string text2 in array)
				{
					if (!string.IsNullOrEmpty(text2))
					{
						TaskId taskId;
						bool flag = Enum.TryParse<TaskId>(text2, out taskId);
						if (flag && taskId != TaskId.ScheduledCheck)
						{
							taskIds.Add(taskId);
						}
						else
						{
							text += (string.IsNullOrEmpty(text) ? string.Empty : ScheduledCheckTaskConfiguration.taskListSeparator.ToString());
							text += text2;
						}
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_IntegrityCheckConfigurationSkippedEntry, new object[]
					{
						text,
						taskNamesParseNext
					});
				}
			}
			taskNamesParseCached = taskNamesParseNext;
		}

		// Token: 0x040000BA RID: 186
		private static char[] taskListSeparator = new char[]
		{
			',',
			';'
		};

		// Token: 0x040000BB RID: 187
		private static object configLock = new object();

		// Token: 0x040000BC RID: 188
		private static ScheduledCheckTaskConfiguration instance = new ScheduledCheckTaskConfiguration();

		// Token: 0x040000BD RID: 189
		private readonly bool isEnabled;

		// Token: 0x040000BE RID: 190
		private readonly string taskNamesDetectOnly;

		// Token: 0x040000BF RID: 191
		private readonly List<TaskId> taskIdsDetectOnly;

		// Token: 0x040000C0 RID: 192
		private readonly string taskNamesDetectAndFix;

		// Token: 0x040000C1 RID: 193
		private readonly List<TaskId> taskIdsDetectAndFix;
	}
}
