using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.LogUploaderProxy
{
	// Token: 0x02000007 RID: 7
	public class ExEventLog
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002136 File Offset: 0x00000336
		public ExEventLog(Guid componentGuid, string sourceName)
		{
			this.exEventLog = new ExEventLog(componentGuid, sourceName);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000214B File Offset: 0x0000034B
		public ExEventLog(Guid componentGuid, string sourceName, string logName)
		{
			this.exEventLog = new ExEventLog(componentGuid, sourceName, logName);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002161 File Offset: 0x00000361
		public bool LogEvent(ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs)
		{
			return this.exEventLog.LogEvent(tuple.EventTupleImpl, periodicKey, messageArgs);
		}

		// Token: 0x04000007 RID: 7
		private ExEventLog exEventLog;

		// Token: 0x02000008 RID: 8
		public enum EventLevel
		{
			// Token: 0x04000009 RID: 9
			Lowest,
			// Token: 0x0400000A RID: 10
			Low,
			// Token: 0x0400000B RID: 11
			Medium = 3,
			// Token: 0x0400000C RID: 12
			High = 5,
			// Token: 0x0400000D RID: 13
			Expert = 7
		}

		// Token: 0x02000009 RID: 9
		public enum EventPeriod
		{
			// Token: 0x0400000F RID: 15
			LogAlways,
			// Token: 0x04000010 RID: 16
			LogOneTime,
			// Token: 0x04000011 RID: 17
			LogPeriodic
		}

		// Token: 0x0200000A RID: 10
		public struct EventTuple
		{
			// Token: 0x0600000F RID: 15 RVA: 0x00002177 File Offset: 0x00000377
			public EventTuple(ExEventLog.EventTuple exEventTuple)
			{
				this.exEventTuple = exEventTuple;
			}

			// Token: 0x17000001 RID: 1
			// (get) Token: 0x06000010 RID: 16 RVA: 0x00002180 File Offset: 0x00000380
			public uint EventId
			{
				get
				{
					return this.exEventTuple.EventId;
				}
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000011 RID: 17 RVA: 0x0000219C File Offset: 0x0000039C
			public short CategoryId
			{
				get
				{
					return this.exEventTuple.CategoryId;
				}
			}

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000012 RID: 18 RVA: 0x000021B8 File Offset: 0x000003B8
			public ExEventLog.EventLevel Level
			{
				get
				{
					return (ExEventLog.EventLevel)this.exEventTuple.Level;
				}
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000013 RID: 19 RVA: 0x000021D4 File Offset: 0x000003D4
			public ExEventLog.EventPeriod Period
			{
				get
				{
					return (ExEventLog.EventPeriod)this.exEventTuple.Period;
				}
			}

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000014 RID: 20 RVA: 0x000021F0 File Offset: 0x000003F0
			public EventLogEntryType EntryType
			{
				get
				{
					return this.exEventTuple.EntryType;
				}
			}

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000015 RID: 21 RVA: 0x0000220B File Offset: 0x0000040B
			internal ExEventLog.EventTuple EventTupleImpl
			{
				get
				{
					return this.exEventTuple;
				}
			}

			// Token: 0x04000012 RID: 18
			private readonly ExEventLog.EventTuple exEventTuple;
		}
	}
}
