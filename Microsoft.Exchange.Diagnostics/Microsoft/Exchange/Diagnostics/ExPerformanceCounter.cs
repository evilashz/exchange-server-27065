using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Xml;
using Microsoft.Win32;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200003A RID: 58
	public class ExPerformanceCounter : IExPerformanceCounter
	{
		// Token: 0x06000144 RID: 324 RVA: 0x00005824 File Offset: 0x00003A24
		public ExPerformanceCounter(string categoryName, string counterName, string instanceName, bool processLifeTime, ExPerformanceCounter totalInstanceCounter, params ExPerformanceCounter[] autoUpdateCounters)
		{
			this.counter = PerformanceCounterFactory.CreatePerformanceCounter();
			this.counter.CategoryName = categoryName;
			this.counter.CounterName = counterName;
			this.counter.InstanceName = instanceName;
			this.counter.ReadOnly = false;
			this.counterIsUsable = true;
			if (processLifeTime)
			{
				this.counter.InstanceLifetime = PerformanceCounterInstanceLifetime.Process;
			}
			this.totalInstanceCounter = totalInstanceCounter;
			this.autoUpdateCounters = autoUpdateCounters;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00005899 File Offset: 0x00003A99
		public ExPerformanceCounter(string categoryName, string counterName, string instanceName, ExPerformanceCounter totalInstanceCounter, params ExPerformanceCounter[] autoUpdateCounters) : this(categoryName, counterName, instanceName, false, totalInstanceCounter, autoUpdateCounters)
		{
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000058A9 File Offset: 0x00003AA9
		public string CounterName
		{
			get
			{
				return this.counter.CounterName;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000058B6 File Offset: 0x00003AB6
		public string CategoryName
		{
			get
			{
				return this.counter.CategoryName;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000058C3 File Offset: 0x00003AC3
		public string CounterHelp
		{
			get
			{
				return this.counter.CounterHelp;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000149 RID: 329 RVA: 0x000058D0 File Offset: 0x00003AD0
		public PerformanceCounterType CounterType
		{
			get
			{
				return this.counter.CounterType;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000058DD File Offset: 0x00003ADD
		public string InstanceName
		{
			get
			{
				return this.counter.InstanceName;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000058EC File Offset: 0x00003AEC
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00005938 File Offset: 0x00003B38
		public virtual long RawValue
		{
			get
			{
				long result = 0L;
				if (this.counterIsUsable)
				{
					try
					{
						result = this.counter.RawValue;
					}
					catch (InvalidOperationException ex)
					{
						this.counterIsUsable = false;
						this.LogUpdateFailureEvent(ExPerformanceCounter.LogReason.Get, ex);
					}
				}
				return result;
			}
			set
			{
				if (this.counterIsUsable)
				{
					try
					{
						if (this.totalInstanceCounter != null)
						{
							this.totalInstanceCounter.IncrementBy(value - this.counter.RawValue);
						}
						this.counter.RawValue = value;
						for (int i = 0; i < this.autoUpdateCounters.Length; i++)
						{
							this.autoUpdateCounters[i].RawValue = value;
						}
					}
					catch (InvalidOperationException ex)
					{
						this.counterIsUsable = false;
						this.LogUpdateFailureEvent(ExPerformanceCounter.LogReason.Set, ex);
					}
				}
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000059C0 File Offset: 0x00003BC0
		public static string GetEncodedName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return name;
			}
			return XmlConvert.EncodeName(name.Replace(" ", string.Empty).Replace("-", "_").Replace(":", "__"));
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000059FF File Offset: 0x00003BFF
		public long Increment()
		{
			return this.IncrementBy(1L);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005A09 File Offset: 0x00003C09
		public long Decrement()
		{
			return this.IncrementBy(-1L);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005A13 File Offset: 0x00003C13
		public virtual void Reset()
		{
			this.RawValue = 0L;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005A20 File Offset: 0x00003C20
		public virtual long IncrementBy(long incrementValue)
		{
			long result = 0L;
			if (this.counterIsUsable)
			{
				try
				{
					if (this.totalInstanceCounter != null)
					{
						this.totalInstanceCounter.IncrementBy(incrementValue);
					}
					for (int i = 0; i < this.autoUpdateCounters.Length; i++)
					{
						this.autoUpdateCounters[i].IncrementBy(incrementValue);
					}
					result = this.counter.IncrementBy(incrementValue);
				}
				catch (InvalidOperationException ex)
				{
					this.counterIsUsable = false;
					this.LogUpdateFailureEvent(ExPerformanceCounter.LogReason.Inc, ex);
				}
			}
			return result;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005AA4 File Offset: 0x00003CA4
		public float NextValue()
		{
			float result = 0f;
			if (this.counterIsUsable)
			{
				try
				{
					result = this.counter.NextValue();
				}
				catch (InvalidOperationException ex)
				{
					this.counterIsUsable = false;
					this.LogUpdateFailureEvent(ExPerformanceCounter.LogReason.NextValue, ex);
				}
			}
			return result;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005AF0 File Offset: 0x00003CF0
		public virtual void Close()
		{
			this.counterIsUsable = false;
			this.counter.Close();
			this.counter.Dispose();
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005B10 File Offset: 0x00003D10
		public void RemoveInstance()
		{
			if (this.counterIsUsable)
			{
				try
				{
					this.counter.RemoveInstance();
				}
				catch (InvalidOperationException ex)
				{
					this.counterIsUsable = false;
					this.LogUpdateFailureEvent(ExPerformanceCounter.LogReason.Remove, ex);
				}
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005B54 File Offset: 0x00003D54
		public void ResetCounterIsUsable()
		{
			this.counterIsUsable = true;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005B60 File Offset: 0x00003D60
		private void LogUpdateFailureEvent(ExPerformanceCounter.LogReason action, Exception ex)
		{
			try
			{
				if (ExPerformanceCounter.eventLogger == null)
				{
					ExPerformanceCounter.eventLogger = new ExEventLog(new Guid("{1f3d39b3-c7ba-4494-94ad-c64cbd33e061}"), "MSExchange Common");
				}
				ExPerformanceCounter.eventLogger.LogEvent(CommonEventLogConstants.Tuple_PerfCounterProblem, this.CategoryName + this.CounterName, new object[]
				{
					(int)action,
					this.CounterName,
					this.CategoryName,
					this.GetProcessAndExceptionInfo(ex.ToString())
				});
			}
			catch
			{
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005BF8 File Offset: 0x00003DF8
		private string GetProcessAndExceptionInfo(string exception)
		{
			string lastWorkerProcessInfo = this.GetLastWorkerProcessInfo();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("The exception thrown is : " + exception);
			stringBuilder.AppendLine("Last worker process info : " + lastWorkerProcessInfo);
			stringBuilder.AppendLine("Processes running while Performance counter failed to update: ");
			try
			{
				Process[] processes = Process.GetProcesses();
				if (processes != null)
				{
					foreach (Process process in processes)
					{
						stringBuilder.AppendLine(process.Id + " " + process.ProcessName);
					}
				}
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("An Exception occured when getting running processes: " + ex.ToString());
			}
			string allInstancesLayout = this.GetAllInstancesLayout(this.CategoryName);
			stringBuilder.AppendLine("Performance Counters Layout information: " + allInstancesLayout);
			return stringBuilder.ToString();
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005CE0 File Offset: 0x00003EE0
		private string GetAllInstancesLayout(string categoryName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			try
			{
				using (PerformanceCounterMemoryMappedFile performanceCounterMemoryMappedFile = new PerformanceCounterMemoryMappedFile(categoryName, true))
				{
					CategoryEntry categoryEntry = performanceCounterMemoryMappedFile.FindCategory(categoryName);
					if (categoryEntry != null)
					{
						for (InstanceEntry instanceEntry = categoryEntry.FirstInstance; instanceEntry != null; instanceEntry = instanceEntry.Next)
						{
							CounterEntry firstCounter = instanceEntry.FirstCounter;
							if (firstCounter != null)
							{
								LifetimeEntry lifetime = firstCounter.Lifetime;
								if (lifetime != null && lifetime.Type == 1)
								{
									stringBuilder.AppendLine(string.Format("A process is holding onto a transport performance counter. processId : {0}, counter : {1}, currentInstance : {2}, categoryName: {3} ", new object[]
									{
										lifetime.ProcessId,
										firstCounter,
										instanceEntry,
										categoryName
									}));
								}
							}
						}
					}
				}
			}
			catch (Win32Exception ex)
			{
				stringBuilder.AppendLine(string.Concat(new object[]
				{
					"Win32Exception for category ",
					categoryName,
					" : ",
					ex
				}));
			}
			catch (FileMappingNotFoundException ex2)
			{
				stringBuilder.AppendLine(string.Concat(new object[]
				{
					"FileMappingNotFoundException for category ",
					categoryName,
					" : ",
					ex2
				}));
			}
			catch (Exception arg)
			{
				stringBuilder.AppendLine("Exception : " + arg);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005E48 File Offset: 0x00004048
		private string GetLastWorkerProcessInfo()
		{
			string result = "Last worker process info not available!";
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport"))
				{
					if (registryKey.GetValue("LastWorkerProcessId") != null)
					{
						int processId = (int)registryKey.GetValue("LastWorkerProcessId");
						using (Process processById = Process.GetProcessById(processId))
						{
							result = processId.ToString() + processById.HasExited;
						}
					}
				}
			}
			catch (Exception ex)
			{
				result = ex.ToString();
			}
			return result;
		}

		// Token: 0x040000E8 RID: 232
		public const string LastWorkerProcessIdLocation = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport";

		// Token: 0x040000E9 RID: 233
		public const string LastWorkerProcessIdKeyName = "LastWorkerProcessId";

		// Token: 0x040000EA RID: 234
		private static ExEventLog eventLogger;

		// Token: 0x040000EB RID: 235
		private IPerformanceCounter counter;

		// Token: 0x040000EC RID: 236
		private bool counterIsUsable;

		// Token: 0x040000ED RID: 237
		private ExPerformanceCounter totalInstanceCounter;

		// Token: 0x040000EE RID: 238
		private ExPerformanceCounter[] autoUpdateCounters;

		// Token: 0x0200003B RID: 59
		private enum LogReason
		{
			// Token: 0x040000F0 RID: 240
			Inc = 1,
			// Token: 0x040000F1 RID: 241
			Get,
			// Token: 0x040000F2 RID: 242
			Set,
			// Token: 0x040000F3 RID: 243
			Remove,
			// Token: 0x040000F4 RID: 244
			NextValue
		}
	}
}
