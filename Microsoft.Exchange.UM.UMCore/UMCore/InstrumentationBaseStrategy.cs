using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200015F RID: 351
	internal abstract class InstrumentationBaseStrategy
	{
		// Token: 0x06000A5C RID: 2652 RVA: 0x0002BEF3 File Offset: 0x0002A0F3
		protected InstrumentationBaseStrategy()
		{
			this.syncLock = new object();
			this.PerfCounters = new List<PerformanceCounter>();
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x0002BF11 File Offset: 0x0002A111
		// (set) Token: 0x06000A5E RID: 2654 RVA: 0x0002BF19 File Offset: 0x0002A119
		private protected List<PerformanceCounter> PerfCounters { protected get; private set; }

		// Token: 0x06000A5F RID: 2655 RVA: 0x0002BF24 File Offset: 0x0002A124
		internal void Initialize()
		{
			lock (this.syncLock)
			{
				this.PerfCounters.Clear();
				this.InternalInitialize();
			}
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0002BF70 File Offset: 0x0002A170
		internal void CollectData(StringBuilder sb)
		{
			lock (this.syncLock)
			{
				ValidateArgument.NotNull(sb, "sb");
				this.CollectPerfCounterData(sb);
				this.InternalCollectData(sb);
			}
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0002BFC4 File Offset: 0x0002A1C4
		internal string GetSystemInformation()
		{
			string result = string.Empty;
			try
			{
				StringBuilder stringBuilder = new StringBuilder(5000);
				try
				{
					InstrumentationBaseStrategy.CollectProcessInformation(stringBuilder);
					stringBuilder.AppendLine();
					InstrumentationBaseStrategy.CollectEventLogEntries(stringBuilder, TimeSpan.FromMinutes(10.0), 200);
				}
				finally
				{
					result = stringBuilder.ToString();
				}
			}
			catch (Exception ex)
			{
				InstrumentationBaseStrategy.TraceDebug("GetSystemInformation: Ecountered error while collecting information - error {0}", new object[]
				{
					ex
				});
			}
			return result;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0002C04C File Offset: 0x0002A24C
		protected static void TraceDebug(string message, params object[] args)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, message, args);
		}

		// Token: 0x06000A63 RID: 2659
		protected abstract void InternalInitialize();

		// Token: 0x06000A64 RID: 2660 RVA: 0x0002C060 File Offset: 0x0002A260
		protected virtual void InternalCollectData(StringBuilder sb)
		{
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0002C110 File Offset: 0x0002A310
		private static void CollectProcessInformation(StringBuilder reportBuilder)
		{
			List<string> list = new List<string>
			{
				"Private Bytes",
				"Working Set",
				"IO Data Bytes/sec",
				"IO Data Operations/sec"
			};
			reportBuilder.AppendLine();
			reportBuilder.AppendLine("--- System Processes Information ---");
			List<PerformanceCounter> pcList = new List<PerformanceCounter>();
			try
			{
				reportBuilder.Append("Process");
				list.ForEach(delegate(string name)
				{
					reportBuilder.AppendFormat(",{0}", name);
				});
				PerformanceCounterCategory performanceCounterCategory = new PerformanceCounterCategory("Process");
				string[] instanceNames = performanceCounterCategory.GetInstanceNames();
				Array.Sort<string>(instanceNames);
				pcList.Capacity = instanceNames.Length * list.Count;
				string[] array = instanceNames;
				for (int i = 0; i < array.Length; i++)
				{
					string processInstance = array[i];
					list.ForEach(delegate(string name)
					{
						PerformanceCounter performanceCounter2 = new PerformanceCounter("Process", name, processInstance);
						try
						{
							performanceCounter2.NextValue();
						}
						catch (Exception ex3)
						{
							if (!(ex3 is InvalidOperationException) && !(ex3 is Win32Exception))
							{
								InstrumentationBaseStrategy.TraceDebug("GetSystemInformation In perfCounter init - error {0}", new object[]
								{
									ex3.Message ?? string.Empty
								});
							}
						}
						pcList.Add(performanceCounter2);
					});
				}
				Thread.Sleep(TimeSpan.FromSeconds(1.0));
				int num = 0;
				foreach (PerformanceCounter performanceCounter in pcList)
				{
					if (num == 0)
					{
						reportBuilder.AppendLine();
						reportBuilder.Append(performanceCounter.InstanceName);
					}
					try
					{
						reportBuilder.AppendFormat(",{0:0.00}", performanceCounter.NextValue());
					}
					catch (Exception ex)
					{
						if (ex is InvalidOperationException || ex is Win32Exception)
						{
							reportBuilder.AppendFormat(",err", new object[0]);
						}
						else
						{
							reportBuilder.Append(ex.Message);
						}
					}
					num = ++num % list.Count;
				}
			}
			catch (Exception ex2)
			{
				InstrumentationBaseStrategy.TraceDebug("CollectEventLogEntries: Ecountered error while collecting running process information - error {0}", new object[]
				{
					ex2
				});
			}
			finally
			{
				if (pcList != null)
				{
					pcList.ForEach(delegate(PerformanceCounter counter)
					{
						counter.Dispose();
					});
				}
			}
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0002C3DC File Offset: 0x0002A5DC
		private static void CollectEventLogEntries(StringBuilder reportBuilder, TimeSpan timeSpan, int maxEntries)
		{
			DateTime t = DateTime.UtcNow - timeSpan;
			string format = "{0},{1},{2},{3}" + Environment.NewLine;
			try
			{
				reportBuilder.AppendLine();
				reportBuilder.AppendLine("--- Event Log Information ---");
				reportBuilder.AppendFormat(format, new object[]
				{
					"DateTime",
					"Type",
					"Source",
					"Message"
				});
				using (EventLog eventLog = new EventLog("Application"))
				{
					int num = 0;
					int i = eventLog.Entries.Count - 1;
					while (i >= 0)
					{
						EventLogEntry eventLogEntry = eventLog.Entries[i];
						reportBuilder.AppendFormat(format, new object[]
						{
							eventLogEntry.TimeGenerated,
							eventLogEntry.EntryType,
							eventLogEntry.Source,
							eventLogEntry.Message
						});
						if (eventLogEntry.TimeGenerated.ToUniversalTime() < t || num > maxEntries)
						{
							break;
						}
						i--;
						num++;
					}
				}
			}
			catch (Exception ex)
			{
				InstrumentationBaseStrategy.TraceDebug("CollectEventLogEntries: Ecountered error while collecting event log information - error {0}", new object[]
				{
					ex
				});
			}
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0002C53C File Offset: 0x0002A73C
		private void CollectPerfCounterData(StringBuilder sb)
		{
			for (int i = 0; i < this.PerfCounters.Count; i++)
			{
				PerformanceCounter performanceCounter = this.PerfCounters[i];
				try
				{
					if (performanceCounter.InstanceName == null || string.Equals(performanceCounter.InstanceName, "_Total", StringComparison.OrdinalIgnoreCase))
					{
						sb.AppendFormat("{0}={1:0.00}", performanceCounter.CounterName, performanceCounter.NextValue());
					}
					else
					{
						sb.AppendFormat("{0}({1})={2:0.00}", performanceCounter.CounterName, performanceCounter.InstanceName, performanceCounter.NextValue());
					}
					sb.Append(",");
				}
				catch (InvalidOperationException ex)
				{
					InstrumentationBaseStrategy.TraceDebug("PopulatePerfCounterInfo: Removing counter {0} - error={1}", new object[]
					{
						performanceCounter.CounterName,
						ex.Message ?? string.Empty
					});
					this.PerfCounters.Remove(performanceCounter);
					i--;
				}
				catch (Win32Exception ex2)
				{
					InstrumentationBaseStrategy.TraceDebug("PopulatePerfCounterInfo: Encountered error while reading the counter {0} - error={1}", new object[]
					{
						performanceCounter.CounterName,
						ex2.Message ?? string.Empty
					});
				}
			}
		}

		// Token: 0x04000959 RID: 2393
		protected const string TotalInstance = "_Total";

		// Token: 0x0400095A RID: 2394
		private object syncLock;
	}
}
