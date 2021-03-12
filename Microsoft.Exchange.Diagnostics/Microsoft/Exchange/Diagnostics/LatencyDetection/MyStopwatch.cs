using System;

namespace Microsoft.Exchange.Diagnostics.LatencyDetection
{
	// Token: 0x02000182 RID: 386
	public class MyStopwatch
	{
		// Token: 0x06000B16 RID: 2838 RVA: 0x00028904 File Offset: 0x00026B04
		public MyStopwatch()
		{
			this.Reset();
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x00028912 File Offset: 0x00026B12
		public static bool IsHighResolution
		{
			get
			{
				return MyStopwatch.isHighResolution;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x00028919 File Offset: 0x00026B19
		public static long Frequency
		{
			get
			{
				return MyStopwatch.frequency;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x00028920 File Offset: 0x00026B20
		public static bool CpuTimeIsAvailable
		{
			get
			{
				return MyStopwatch.isWindowsVersionAtLeastLonghorn && !MyStopwatch.cpuTimeIsSuppressed;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x00028934 File Offset: 0x00026B34
		public static long CpuCycles
		{
			get
			{
				ulong result = 0UL;
				if (MyStopwatch.CpuTimeIsAvailable)
				{
					using (SafeThreadHandle currentThread = NativeMethods.GetCurrentThread())
					{
						if (!NativeMethods.QueryThreadCycleTime(currentThread, out result))
						{
							throw new InvalidOperationException("NativeMethods.QueryThreadCycleTime() unexpectedly returned false.");
						}
					}
				}
				return (long)result;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x00028988 File Offset: 0x00026B88
		public static long Timestamp
		{
			get
			{
				long result = 0L;
				if (MyStopwatch.IsHighResolution)
				{
					if (!NativeMethods.QueryPerformanceCounter(out result))
					{
						result = DateTime.UtcNow.Ticks;
						MyStopwatch.isHighResolution = false;
					}
				}
				else
				{
					result = DateTime.UtcNow.Ticks;
				}
				return result;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x000289CD File Offset: 0x00026BCD
		public TimeSpan Elapsed
		{
			get
			{
				return new TimeSpan(this.GetElapsedDateTimeTicks());
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x000289DC File Offset: 0x00026BDC
		public TimeSpan ElapsedCpu
		{
			get
			{
				if (MyStopwatch.CpuTimeIsAvailable && this.timerEndMaxMHz > 0)
				{
					if (this.isRunning)
					{
						this.InternalGetElapsedCpuStatus();
					}
					long ticks = 10L * this.elapsedCPU / (long)this.timerEndMaxMHz;
					return new TimeSpan(ticks);
				}
				return TimeSpan.Zero;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x00028A26 File Offset: 0x00026C26
		public long ElapsedCpuTicks
		{
			get
			{
				return this.elapsedCPU;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x00028A2E File Offset: 0x00026C2E
		public long ElapsedMilliseconds
		{
			get
			{
				return this.GetElapsedDateTimeTicks() / 10000L;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x00028A3D File Offset: 0x00026C3D
		public long ElapsedTicks
		{
			get
			{
				return this.GetRawElapsedTicks();
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00028A45 File Offset: 0x00026C45
		public bool ThreadContextSwitchOccurred
		{
			get
			{
				return this.threadSwitchOccurred;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x00028A4D File Offset: 0x00026C4D
		public bool FinishedOnDifferentProcessor
		{
			get
			{
				return this.cpuSwitchOccurred;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x00028A55 File Offset: 0x00026C55
		public bool PowerManagementChangeOccurred
		{
			get
			{
				return this.cpuspeedChangeOccurred;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x00028A5D File Offset: 0x00026C5D
		public int CurrentCpuMhz
		{
			get
			{
				return this.timerEndCurrentMHz;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x00028A65 File Offset: 0x00026C65
		public int MaxCpuMhz
		{
			get
			{
				return this.timerEndMaxMHz;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x00028A6D File Offset: 0x00026C6D
		public bool IsRunning
		{
			get
			{
				return this.isRunning;
			}
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00028A75 File Offset: 0x00026C75
		public static void SuppressCpuTimeMeasurement()
		{
			MyStopwatch.cpuTimeIsSuppressed = true;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00028A80 File Offset: 0x00026C80
		public static MyStopwatch StartNew()
		{
			MyStopwatch myStopwatch = new MyStopwatch();
			myStopwatch.Start();
			return myStopwatch;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00028A9C File Offset: 0x00026C9C
		public void Start()
		{
			if (!this.isRunning)
			{
				this.startTimeStamp = MyStopwatch.Timestamp;
				if (MyStopwatch.CpuTimeIsAvailable)
				{
					using (SafeThreadHandle currentThread = NativeMethods.GetCurrentThread())
					{
						this.timerStartThreadHandle = currentThread.DangerousGetHandle().ToInt64();
					}
					this.timerStartCPUID = (int)NativeMethods.GetCurrentProcessorNumber();
					this.timerStartCurrentMHz = MyStopwatch.GetCurrentMHz(this.timerStartCPUID);
					this.timerEndCurrentMHz = this.timerStartCurrentMHz;
					this.startCPUcycles = MyStopwatch.CpuCycles;
				}
				this.isRunning = true;
			}
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00028B34 File Offset: 0x00026D34
		public void Stop()
		{
			if (this.isRunning)
			{
				long timestamp = MyStopwatch.Timestamp;
				if (MyStopwatch.CpuTimeIsAvailable)
				{
					this.InternalGetElapsedCpuStatus();
				}
				long num = timestamp - this.startTimeStamp;
				this.elapsed += num;
				this.isRunning = false;
				if (this.elapsed < 0L)
				{
					this.elapsed = 0L;
				}
				if (MyStopwatch.CpuTimeIsAvailable && this.elapsedCPU > 0L)
				{
					this.timerEndCPUID = (int)NativeMethods.GetCurrentProcessorNumber();
					if (this.timerEndCPUID != this.timerStartCPUID)
					{
						this.cpuSwitchOccurred = true;
					}
					MyStopwatch.GetCurrentAndMaxMHz(this.timerEndCPUID, out this.timerEndCurrentMHz, out this.timerEndMaxMHz);
					if (this.timerStartCurrentMHz != this.timerEndCurrentMHz)
					{
						this.cpuspeedChangeOccurred = true;
					}
				}
			}
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00028BEC File Offset: 0x00026DEC
		public void Reset()
		{
			this.elapsed = 0L;
			this.isRunning = false;
			this.startTimeStamp = 0L;
			this.elapsedCPU = 0L;
			this.startCPUcycles = 0L;
			this.threadSwitchOccurred = false;
			this.cpuSwitchOccurred = false;
			this.cpuspeedChangeOccurred = false;
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00028C2C File Offset: 0x00026E2C
		private static int GetCurrentMHz(int cpuId)
		{
			int result;
			int num;
			MyStopwatch.GetCurrentAndMaxMHz(cpuId, out result, out num);
			return result;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00028C44 File Offset: 0x00026E44
		private static void GetCurrentAndMaxMHz(int cpuId, out int current, out int max)
		{
			if (cpuId < 0 || cpuId > Environment.ProcessorCount)
			{
				throw new ArgumentException("Needs to be between 0 and Environment.ProcessorCount-1.", "cpuId");
			}
			current = 0;
			max = 0;
			if (!MyStopwatch.isCpuThrottlingSupported)
			{
				current = MyStopwatch.nonThrottlingCpuMHz[cpuId];
				max = MyStopwatch.nonThrottlingCpuMHz[cpuId];
				return;
			}
			PROCESSOR_POWER_INFORMATION processor_POWER_INFORMATION;
			if (CPUPowerWrapper.GetCurrentPowerInfoForProcessor(cpuId, out processor_POWER_INFORMATION))
			{
				current = (int)processor_POWER_INFORMATION.CurrentMhz;
				max = (int)processor_POWER_INFORMATION.MaxMhz;
				return;
			}
			throw new InvalidOperationException("CPUPowerWrapper.GetCurrentPowerInfoForProcessor() unexpectedly returned false.");
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00028CB4 File Offset: 0x00026EB4
		private static long QueryOsForFrequency(out bool highFrequency)
		{
			long result;
			highFrequency = NativeMethods.QueryPerformanceFrequency(out result);
			if (!highFrequency)
			{
				result = 10000000L;
			}
			return result;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00028CD8 File Offset: 0x00026ED8
		private static bool GetIsCpuThrottlingSupported()
		{
			bool isProcessorThrottlingEnabled = CPUPowerWrapper.IsProcessorThrottlingEnabled;
			if (!isProcessorThrottlingEnabled && !CPUPowerWrapper.GetMaxProcessorSpeeds(out MyStopwatch.nonThrottlingCpuMHz))
			{
				throw new InvalidOperationException("CPUPowerWrapper.GetMaxProcessorSpeeds() unexpectedly returned false.");
			}
			return isProcessorThrottlingEnabled;
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00028D08 File Offset: 0x00026F08
		private void InternalGetElapsedCpuStatus()
		{
			using (SafeThreadHandle currentThread = NativeMethods.GetCurrentThread())
			{
				long cpuCycles = MyStopwatch.CpuCycles;
				if (currentThread.DangerousGetHandle().ToInt64() == this.timerStartThreadHandle)
				{
					if (cpuCycles > this.startCPUcycles)
					{
						long num = cpuCycles - this.startCPUcycles;
						this.elapsedCPU += num;
					}
					else
					{
						this.elapsedCPU = 0L;
						this.startCPUcycles = 0L;
					}
				}
				else
				{
					this.threadSwitchOccurred = true;
					this.startCPUcycles = 0L;
				}
			}
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00028D98 File Offset: 0x00026F98
		private long GetRawElapsedTicks()
		{
			long num = this.elapsed;
			if (this.isRunning)
			{
				long num2 = MyStopwatch.Timestamp - this.startTimeStamp;
				num += num2;
			}
			return num;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00028DC8 File Offset: 0x00026FC8
		private long GetElapsedDateTimeTicks()
		{
			long num = this.GetRawElapsedTicks();
			if (MyStopwatch.IsHighResolution)
			{
				double num2 = (double)num * MyStopwatch.elapsedTicksToDateTimeTicks;
				num = (long)num2;
			}
			return num;
		}

		// Token: 0x04000792 RID: 1938
		private const long TicksPerMillisecond = 10000L;

		// Token: 0x04000793 RID: 1939
		private const long TicksPerSecond = 10000000L;

		// Token: 0x04000794 RID: 1940
		private const long CyclesPerMHz = 10L;

		// Token: 0x04000795 RID: 1941
		private const int OSVista = 6;

		// Token: 0x04000796 RID: 1942
		private static readonly long frequency = MyStopwatch.QueryOsForFrequency(out MyStopwatch.isHighResolution);

		// Token: 0x04000797 RID: 1943
		private static readonly double elapsedTicksToDateTimeTicks = MyStopwatch.isHighResolution ? (10000000.0 / (double)MyStopwatch.frequency) : 1.0;

		// Token: 0x04000798 RID: 1944
		private static readonly bool isCpuThrottlingSupported = MyStopwatch.GetIsCpuThrottlingSupported();

		// Token: 0x04000799 RID: 1945
		private static readonly bool isWindowsVersionAtLeastLonghorn = Environment.OSVersion.Version.Major >= 6;

		// Token: 0x0400079A RID: 1946
		private static int[] nonThrottlingCpuMHz;

		// Token: 0x0400079B RID: 1947
		private static bool isHighResolution;

		// Token: 0x0400079C RID: 1948
		private static bool cpuTimeIsSuppressed;

		// Token: 0x0400079D RID: 1949
		private long elapsed;

		// Token: 0x0400079E RID: 1950
		private long elapsedCPU;

		// Token: 0x0400079F RID: 1951
		private long startTimeStamp;

		// Token: 0x040007A0 RID: 1952
		private long startCPUcycles;

		// Token: 0x040007A1 RID: 1953
		private int timerStartCPUID;

		// Token: 0x040007A2 RID: 1954
		private int timerEndCPUID;

		// Token: 0x040007A3 RID: 1955
		private int timerStartCurrentMHz;

		// Token: 0x040007A4 RID: 1956
		private int timerEndCurrentMHz;

		// Token: 0x040007A5 RID: 1957
		private int timerEndMaxMHz;

		// Token: 0x040007A6 RID: 1958
		private long timerStartThreadHandle;

		// Token: 0x040007A7 RID: 1959
		private bool threadSwitchOccurred;

		// Token: 0x040007A8 RID: 1960
		private bool cpuSwitchOccurred;

		// Token: 0x040007A9 RID: 1961
		private bool cpuspeedChangeOccurred;

		// Token: 0x040007AA RID: 1962
		private bool isRunning;
	}
}
