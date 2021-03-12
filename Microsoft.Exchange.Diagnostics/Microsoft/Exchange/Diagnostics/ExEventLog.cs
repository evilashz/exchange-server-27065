using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Diagnostics.Components.Common;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200002C RID: 44
	[CLSCompliant(true)]
	public sealed class ExEventLog
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x000046D3 File Offset: 0x000028D3
		public ExEventLog(Guid componentGuid, string sourceName) : this(componentGuid, sourceName, null)
		{
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000046DE File Offset: 0x000028DE
		public ExEventLog(Guid componentGuid, string sourceName, string logName)
		{
			this.impl = ExEventLog.hookableEventLogFactory.Value(componentGuid, sourceName, logName);
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000046FE File Offset: 0x000028FE
		public ExEventSourceInfo EventSource
		{
			get
			{
				return this.impl.EventSource;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x0000470B File Offset: 0x0000290B
		internal ExEventLog.IImpl TestHook
		{
			get
			{
				if (this.impl is ExEventLog.Impl)
				{
					return null;
				}
				return this.impl;
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004722 File Offset: 0x00002922
		public bool IsEventCategoryEnabled(short categoryNumber, ExEventLog.EventLevel level)
		{
			return this.impl.IsEventCategoryEnabled(categoryNumber, level);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004731 File Offset: 0x00002931
		public void SetEventPeriod(int seconds)
		{
			this.EventSource.EventPeriodTime = seconds;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004740 File Offset: 0x00002940
		public bool LogEvent(ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs)
		{
			bool flag;
			return this.LogEvent(tuple, periodicKey, out flag, messageArgs);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004758 File Offset: 0x00002958
		public bool LogEventWithExtraData(ExEventLog.EventTuple tuple, string periodicKey, byte[] extraData, params object[] messageArgs)
		{
			bool flag;
			return this.impl.LogEvent(string.Empty, tuple.EventId, tuple.CategoryId, tuple.Level, tuple.EntryType, tuple.Period, periodicKey, out flag, extraData, messageArgs);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000047A0 File Offset: 0x000029A0
		public bool LogEvent(IOrganizationIdForEventLog organizationId, ExEventLog.EventTuple tuple, string periodicKey, object arg0)
		{
			return this.LogEvent(organizationId, tuple, periodicKey, new object[]
			{
				arg0
			});
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000047C4 File Offset: 0x000029C4
		public bool LogEvent(IOrganizationIdForEventLog organizationId, ExEventLog.EventTuple tuple, string periodicKey, object arg0, object arg1)
		{
			return this.LogEvent(organizationId, tuple, periodicKey, new object[]
			{
				arg0,
				arg1
			});
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000047EC File Offset: 0x000029EC
		public bool LogEvent(IOrganizationIdForEventLog organizationId, ExEventLog.EventTuple tuple, string periodicKey, object arg0, object arg1, object arg2)
		{
			return this.LogEvent(organizationId, tuple, periodicKey, new object[]
			{
				arg0,
				arg1,
				arg2
			});
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000481C File Offset: 0x00002A1C
		public bool LogEvent(IOrganizationIdForEventLog organizationId, ExEventLog.EventTuple tuple, string periodicKey, object arg0, object arg1, object arg2, object arg3)
		{
			return this.LogEvent(organizationId, tuple, periodicKey, new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3
			});
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004850 File Offset: 0x00002A50
		public bool LogEvent(IOrganizationIdForEventLog organizationId, ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs)
		{
			if (organizationId != null && !string.IsNullOrEmpty(organizationId.IdForEventLog) && tuple.Period == ExEventLog.EventPeriod.LogOneTime)
			{
				throw new ArgumentException("Per-tenant one-time events are not supported.", "tuple");
			}
			bool flag;
			return this.impl.LogEvent((organizationId != null) ? organizationId.IdForEventLog : string.Empty, tuple.EventId, tuple.CategoryId, tuple.Level, tuple.EntryType, tuple.Period, periodicKey, out flag, null, messageArgs);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000048CC File Offset: 0x00002ACC
		public bool LogEvent(IOrganizationIdForEventLog organizationId, ExEventLog.EventTuple tuple, string periodicKey, out bool fEventSuppressed, params object[] messageArgs)
		{
			if (organizationId != null && !string.IsNullOrEmpty(organizationId.IdForEventLog) && tuple.Period == ExEventLog.EventPeriod.LogOneTime)
			{
				throw new ArgumentException("Per-tenant one-time events are not supported.", "tuple");
			}
			return this.impl.LogEvent((organizationId != null) ? organizationId.IdForEventLog : string.Empty, tuple.EventId, tuple.CategoryId, tuple.Level, tuple.EntryType, tuple.Period, periodicKey, out fEventSuppressed, null, messageArgs);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004948 File Offset: 0x00002B48
		public bool LogEvent(ExEventLog.EventTuple tuple, string periodicKey, out bool fEventSuppressed, params object[] messageArgs)
		{
			return this.impl.LogEvent(string.Empty, tuple.EventId, tuple.CategoryId, tuple.Level, tuple.EntryType, tuple.Period, periodicKey, out fEventSuppressed, null, messageArgs);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000498D File Offset: 0x00002B8D
		internal static IDisposable SetFactoryTestHook(Func<Guid, string, string, ExEventLog.IImpl> eventLogFactory)
		{
			return ExEventLog.hookableEventLogFactory.SetTestHook(eventLogFactory);
		}

		// Token: 0x040000C0 RID: 192
		private static readonly Hookable<Func<Guid, string, string, ExEventLog.IImpl>> hookableEventLogFactory = Hookable<Func<Guid, string, string, ExEventLog.IImpl>>.Create(true, (Guid componentGuid, string sourceName, string logName) => new ExEventLog.Impl(componentGuid, sourceName, logName));

		// Token: 0x040000C1 RID: 193
		private ExEventLog.IImpl impl;

		// Token: 0x0200002D RID: 45
		public enum EventLevel
		{
			// Token: 0x040000C4 RID: 196
			Lowest,
			// Token: 0x040000C5 RID: 197
			Low,
			// Token: 0x040000C6 RID: 198
			Medium = 3,
			// Token: 0x040000C7 RID: 199
			High = 5,
			// Token: 0x040000C8 RID: 200
			Expert = 7
		}

		// Token: 0x0200002E RID: 46
		public enum EventPeriod
		{
			// Token: 0x040000CA RID: 202
			LogAlways,
			// Token: 0x040000CB RID: 203
			LogOneTime,
			// Token: 0x040000CC RID: 204
			LogPeriodic
		}

		// Token: 0x0200002F RID: 47
		[CLSCompliant(false)]
		public interface IImpl
		{
			// Token: 0x17000038 RID: 56
			// (get) Token: 0x06000102 RID: 258
			ExEventSourceInfo EventSource { get; }

			// Token: 0x06000103 RID: 259
			bool IsEventCategoryEnabled(short categoryNumber, ExEventLog.EventLevel level);

			// Token: 0x06000104 RID: 260
			bool LogEvent(string organizationId, uint eventId, short category, ExEventLog.EventLevel level, EventLogEntryType type, ExEventLog.EventPeriod period, string periodicKey, out bool fEventSuppressed, byte[] extraData, params object[] messageArgs);
		}

		// Token: 0x02000030 RID: 48
		public struct EventTuple
		{
			// Token: 0x06000105 RID: 261 RVA: 0x000049CE File Offset: 0x00002BCE
			[CLSCompliant(false)]
			public EventTuple(uint eventId, short categoryId, EventLogEntryType entryType, ExEventLog.EventLevel level, ExEventLog.EventPeriod period)
			{
				this.eventId = eventId;
				this.categoryId = categoryId;
				this.entryType = entryType;
				this.level = level;
				this.period = period;
			}

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x06000106 RID: 262 RVA: 0x000049F5 File Offset: 0x00002BF5
			[CLSCompliant(false)]
			public uint EventId
			{
				get
				{
					return this.eventId;
				}
			}

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x06000107 RID: 263 RVA: 0x000049FD File Offset: 0x00002BFD
			[CLSCompliant(false)]
			public short CategoryId
			{
				get
				{
					return this.categoryId;
				}
			}

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x06000108 RID: 264 RVA: 0x00004A05 File Offset: 0x00002C05
			[CLSCompliant(false)]
			public ExEventLog.EventLevel Level
			{
				get
				{
					return this.level;
				}
			}

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x06000109 RID: 265 RVA: 0x00004A0D File Offset: 0x00002C0D
			[CLSCompliant(false)]
			public ExEventLog.EventPeriod Period
			{
				get
				{
					return this.period;
				}
			}

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x0600010A RID: 266 RVA: 0x00004A15 File Offset: 0x00002C15
			[CLSCompliant(false)]
			public EventLogEntryType EntryType
			{
				get
				{
					return this.entryType;
				}
			}

			// Token: 0x040000CD RID: 205
			private readonly uint eventId;

			// Token: 0x040000CE RID: 206
			private readonly short categoryId;

			// Token: 0x040000CF RID: 207
			private readonly ExEventLog.EventLevel level;

			// Token: 0x040000D0 RID: 208
			private readonly ExEventLog.EventPeriod period;

			// Token: 0x040000D1 RID: 209
			private readonly EventLogEntryType entryType;
		}

		// Token: 0x02000031 RID: 49
		private struct PeriodicCheckKey
		{
			// Token: 0x0600010B RID: 267 RVA: 0x00004A1D File Offset: 0x00002C1D
			public PeriodicCheckKey(uint eid, string sn)
			{
				this.EventId = eid;
				this.SourceName = sn;
			}

			// Token: 0x040000D2 RID: 210
			public uint EventId;

			// Token: 0x040000D3 RID: 211
			public string SourceName;
		}

		// Token: 0x02000032 RID: 50
		internal sealed class PeriodicEventsHistory<T>
		{
			// Token: 0x0600010C RID: 268 RVA: 0x00004A2D File Offset: 0x00002C2D
			internal PeriodicEventsHistory(int length)
			{
				if (length < 0)
				{
					throw new ArgumentOutOfRangeException("length");
				}
				this.length = length;
			}

			// Token: 0x0600010D RID: 269 RVA: 0x00004A4B File Offset: 0x00002C4B
			internal bool Log(T evt, long eventTime)
			{
				this.EnsureHistoryInitialized();
				if (this.InHistory(evt, eventTime))
				{
					return false;
				}
				this.RecordEvent(evt, eventTime);
				return true;
			}

			// Token: 0x0600010E RID: 270 RVA: 0x00004A80 File Offset: 0x00002C80
			private bool InHistory(T evt, long eventTime)
			{
				long num = this.mostRecentEventTime - (long)this.length + 1L;
				long num2 = eventTime - (long)this.length + 1L;
				if (this.mostRecentEventTime < num2)
				{
					return false;
				}
				if (eventTime < num)
				{
					return false;
				}
				long time = Math.Max(num, num2);
				long time2 = Math.Min(this.mostRecentEventTime, eventTime);
				long lowerOffset = this.MapTimeToHistory(time);
				long upperOffset = this.MapTimeToHistory(time2);
				return this.history.PeekRange(lowerOffset, upperOffset).Any((HashSet<T> h) => h.Contains(evt));
			}

			// Token: 0x0600010F RID: 271 RVA: 0x00004B14 File Offset: 0x00002D14
			private long MapTimeToHistory(long time)
			{
				return time - this.mostRecentEventTime;
			}

			// Token: 0x06000110 RID: 272 RVA: 0x00004B20 File Offset: 0x00002D20
			private void RecordEvent(T evt, long eventTime)
			{
				if (eventTime <= this.mostRecentEventTime)
				{
					this.history.Peek().Add(evt);
					return;
				}
				for (long num = Math.Min(eventTime - this.mostRecentEventTime, (long)this.length); num > 0L; num -= 1L)
				{
					this.history.Advance().Clear();
				}
				this.mostRecentEventTime = eventTime;
				this.history.Peek().Add(evt);
			}

			// Token: 0x06000111 RID: 273 RVA: 0x00004B93 File Offset: 0x00002D93
			private void EnsureHistoryInitialized()
			{
				if (this.history == null)
				{
					this.history = new ExEventLog.PeriodicEventsHistory<T>.SlidingWindow<HashSet<T>>(this.length);
				}
			}

			// Token: 0x040000D4 RID: 212
			private readonly int length;

			// Token: 0x040000D5 RID: 213
			private ExEventLog.PeriodicEventsHistory<T>.SlidingWindow<HashSet<T>> history;

			// Token: 0x040000D6 RID: 214
			private long mostRecentEventTime;

			// Token: 0x02000033 RID: 51
			private sealed class SlidingWindow<U> where U : new()
			{
				// Token: 0x06000112 RID: 274 RVA: 0x00004BB0 File Offset: 0x00002DB0
				public SlidingWindow(int length)
				{
					if (length < 0)
					{
						throw new ArgumentOutOfRangeException();
					}
					this.readingPositon = 0;
					this.length = length;
					this.buffer = new U[length];
					for (int i = 0; i < length; i++)
					{
						this.buffer[i] = ((default(U) == null) ? Activator.CreateInstance<U>() : default(U));
					}
				}

				// Token: 0x06000113 RID: 275 RVA: 0x00004C1F File Offset: 0x00002E1F
				internal U Peek()
				{
					return this.Peek(0L);
				}

				// Token: 0x06000114 RID: 276 RVA: 0x00004D48 File Offset: 0x00002F48
				internal IEnumerable<U> PeekRange(long lowerOffset, long upperOffset)
				{
					for (long offset = lowerOffset; offset <= upperOffset; offset += 1L)
					{
						yield return this.Peek(offset);
					}
					yield break;
				}

				// Token: 0x06000115 RID: 277 RVA: 0x00004D73 File Offset: 0x00002F73
				internal U Advance()
				{
					this.readingPositon = (this.readingPositon + 1) % this.length;
					return this.buffer[this.readingPositon];
				}

				// Token: 0x06000116 RID: 278 RVA: 0x00004D9C File Offset: 0x00002F9C
				private U Peek(long offset)
				{
					long num = ((long)this.readingPositon + offset) % (long)this.length;
					return this.buffer[(int)(checked((IntPtr)((num < 0L) ? unchecked(num + (long)this.length) : num)))];
				}

				// Token: 0x040000D7 RID: 215
				private readonly int length;

				// Token: 0x040000D8 RID: 216
				private U[] buffer;

				// Token: 0x040000D9 RID: 217
				private int readingPositon;
			}
		}

		// Token: 0x02000034 RID: 52
		private class Impl : ExEventLog.IImpl
		{
			// Token: 0x06000117 RID: 279 RVA: 0x00004DD8 File Offset: 0x00002FD8
			public Impl(Guid componentGuid, string sourceName, string logName)
			{
				if (string.IsNullOrEmpty(sourceName))
				{
					throw new ArgumentException("sourceName must be non-NULL and non-zero-length", "sourceName");
				}
				if (!EventLog.SourceExists(sourceName))
				{
					ExTraceGlobals.EventLogTracer.TraceInformation(22683, 0L, "Creating Event Source");
					try
					{
						EventLog.CreateEventSource(sourceName, logName);
					}
					catch (ArgumentException)
					{
					}
				}
				this.eventLog = new EventLog();
				this.eventLog.Source = sourceName;
				ExTraceGlobals.EventLogTracer.TraceInformation(31849, 0L, "RegisterEventSource succeeded");
				this.periodicKeys = new Dictionary<ExEventLog.PeriodicCheckKey, DateTime>(new ExEventLog.PeriodicKeysComparer());
				this.perTenantPeriodicEventsHistory = new ExEventLog.PeriodicEventsHistory<int>(15);
				this.eventSource = new ExEventSourceInfo(sourceName);
			}

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x06000118 RID: 280 RVA: 0x00004E94 File Offset: 0x00003094
			public ExEventSourceInfo EventSource
			{
				get
				{
					return this.eventSource;
				}
			}

			// Token: 0x06000119 RID: 281 RVA: 0x00004E9C File Offset: 0x0000309C
			public bool IsEventCategoryEnabled(short categoryNumber, ExEventLog.EventLevel level)
			{
				switch (level)
				{
				case ExEventLog.EventLevel.Lowest:
					return true;
				case ExEventLog.EventLevel.Low:
				case ExEventLog.EventLevel.Medium:
				case ExEventLog.EventLevel.High:
				case ExEventLog.EventLevel.Expert:
					goto IL_2F;
				}
				level = ExEventLog.EventLevel.Expert;
				IL_2F:
				ExEventLog.EventLevel eventLevel = ExEventLog.EventLevel.Lowest;
				ExEventCategory category = this.EventSource.GetCategory((int)categoryNumber);
				if (category != null)
				{
					eventLevel = category.EventLevel;
				}
				return level <= eventLevel;
			}

			// Token: 0x0600011A RID: 282 RVA: 0x00004EF8 File Offset: 0x000030F8
			public bool LogEvent(string organizationId, uint eventId, short category, ExEventLog.EventLevel level, EventLogEntryType type, ExEventLog.EventPeriod period, string periodicKey, out bool fEventSuppressed, byte[] extraData, params object[] messageArgs)
			{
				fEventSuppressed = false;
				if (messageArgs != null && messageArgs.Length > 32767)
				{
					throw new ArgumentException("There were too many strings passed in as messageArgs", "messageArgs");
				}
				if (!this.IsEventCategoryEnabled(category, level))
				{
					return true;
				}
				if (!this.CanLogPeriodic(period, eventId, periodicKey, organizationId))
				{
					fEventSuppressed = true;
					return true;
				}
				EventInstance instance = new EventInstance((long)((ulong)eventId), (int)category, type);
				Exception ex = null;
				try
				{
					byte[] array;
					if (!string.IsNullOrEmpty(organizationId))
					{
						int num = string.IsNullOrEmpty(periodicKey) ? 0 : periodicKey.GetHashCode();
						array = Encoding.UTF8.GetBytes(string.Format("<ExchangeEventInfo><OrganizationId>{0}</OrganizationId><PeriodicKey>{1}</PeriodicKey></ExchangeEventInfo>", organizationId, num.ToString("X", CultureInfo.InvariantCulture)));
						if (extraData != null)
						{
							array = array.Concat(extraData).ToArray<byte>();
						}
					}
					else
					{
						array = extraData;
					}
					if (messageArgs != null)
					{
						for (int i = 0; i < messageArgs.Length; i++)
						{
							string text = (messageArgs[i] != null) ? messageArgs[i].ToString() : string.Empty;
							if (!string.IsNullOrEmpty(text) && text.Length > 31000)
							{
								messageArgs[i] = text.Substring(0, 31000) + "...";
							}
						}
					}
					this.eventLog.WriteEvent(instance, array, messageArgs);
				}
				catch (Win32Exception ex2)
				{
					ex = ex2;
				}
				catch (InvalidOperationException ex3)
				{
					ex = ex3;
				}
				catch (AccessViolationException ex4)
				{
					ex = ex4;
				}
				if (ex != null)
				{
					ExTraceGlobals.EventLogTracer.TraceInformation<Exception>(17513, 0L, "WriteEvent returned {0}", ex);
					return false;
				}
				return true;
			}

			// Token: 0x0600011B RID: 283 RVA: 0x00005084 File Offset: 0x00003284
			private bool CanLogPeriodic(ExEventLog.EventPeriod period, uint eventId, string eventKey, string organizationId)
			{
				if (period == ExEventLog.EventPeriod.LogAlways)
				{
					return true;
				}
				if (!string.IsNullOrEmpty(organizationId))
				{
					return this.CanLogPeriodicTenantEvent(eventId, eventKey, organizationId);
				}
				ExEventLog.PeriodicCheckKey key = new ExEventLog.PeriodicCheckKey(eventId, eventKey);
				DateTime dateTime;
				bool flag2;
				lock (this.periodicKeys)
				{
					flag2 = this.periodicKeys.TryGetValue(key, out dateTime);
				}
				if (!flag2)
				{
					lock (this.periodicKeys)
					{
						this.periodicKeys[key] = DateTime.UtcNow;
					}
					return true;
				}
				if (ExEventLog.EventPeriod.LogOneTime == period)
				{
					return false;
				}
				int eventPeriodTime = this.EventSource.EventPeriodTime;
				if (DateTime.UtcNow >= dateTime.AddSeconds((double)eventPeriodTime))
				{
					lock (this.periodicKeys)
					{
						this.periodicKeys.Remove(key);
						this.periodicKeys[key] = DateTime.UtcNow;
					}
					return true;
				}
				return false;
			}

			// Token: 0x0600011C RID: 284 RVA: 0x000051AC File Offset: 0x000033AC
			private bool CanLogPeriodicTenantEvent(uint eventId, string periodicKey, string organizationId)
			{
				int evt = eventId.GetHashCode() ^ (string.IsNullOrEmpty(periodicKey) ? 0 : periodicKey.GetHashCode()) ^ (string.IsNullOrEmpty(organizationId) ? 0 : organizationId.ToLowerInvariant().GetHashCode());
				long eventTime = DateTime.UtcNow.Ticks / 600000000L;
				bool result;
				lock (this.perTenantPeriodicEventsHistory)
				{
					result = this.perTenantPeriodicEventsHistory.Log(evt, eventTime);
				}
				return result;
			}

			// Token: 0x040000DA RID: 218
			private const int PerTenantEventPeriodInMinutes = 15;

			// Token: 0x040000DB RID: 219
			private const int MaxLogEntryStringLength = 31000;

			// Token: 0x040000DC RID: 220
			private readonly EventLog eventLog;

			// Token: 0x040000DD RID: 221
			private Dictionary<ExEventLog.PeriodicCheckKey, DateTime> periodicKeys;

			// Token: 0x040000DE RID: 222
			private ExEventLog.PeriodicEventsHistory<int> perTenantPeriodicEventsHistory;

			// Token: 0x040000DF RID: 223
			private ExEventSourceInfo eventSource;
		}

		// Token: 0x02000035 RID: 53
		private class PeriodicKeysComparer : IEqualityComparer<ExEventLog.PeriodicCheckKey>
		{
			// Token: 0x0600011E RID: 286 RVA: 0x00005248 File Offset: 0x00003448
			bool IEqualityComparer<ExEventLog.PeriodicCheckKey>.Equals(ExEventLog.PeriodicCheckKey a, ExEventLog.PeriodicCheckKey b)
			{
				return a.EventId == b.EventId && 0 == string.Compare(a.SourceName, b.SourceName, StringComparison.Ordinal);
			}

			// Token: 0x0600011F RID: 287 RVA: 0x00005273 File Offset: 0x00003473
			int IEqualityComparer<ExEventLog.PeriodicCheckKey>.GetHashCode(ExEventLog.PeriodicCheckKey key)
			{
				return (int)(key.EventId ^ (uint)((key.SourceName != null) ? key.SourceName.GetHashCode() : 0));
			}
		}
	}
}
