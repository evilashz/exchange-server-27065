using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.Diagnostics.LatencyDetection
{
	// Token: 0x0200016E RID: 366
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class LatencyDetectionContext : IFormattable
	{
		// Token: 0x06000A53 RID: 2643 RVA: 0x0002697C File Offset: 0x00024B7C
		internal LatencyDetectionContext(LatencyDetectionLocation location, ContextOptions contextOptions, string version, object hash, params IPerformanceDataProvider[] providers)
		{
			this.latencyDetectionLocation = location;
			this.assemblyVersion = version;
			this.StackTraceContext = hash;
			this.contextOptions = contextOptions;
			if ((contextOptions & ContextOptions.DoNotMeasureTime) == ContextOptions.DoNotMeasureTime)
			{
				this.timer = null;
			}
			else
			{
				this.timer = MyStopwatch.StartNew();
			}
			this.SetDataProviders(providers);
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x000269D9 File Offset: 0x00024BD9
		internal LatencyDetectionContext(LatencyDetectionLocation location, string version, object hash, params IPerformanceDataProvider[] providers) : this(location, ContextOptions.Default, version, hash, providers)
		{
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x000269E7 File Offset: 0x00024BE7
		// (set) Token: 0x06000A56 RID: 2646 RVA: 0x000269EF File Offset: 0x00024BEF
		public string UserIdentity { get; set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x000269F8 File Offset: 0x00024BF8
		// (set) Token: 0x06000A58 RID: 2648 RVA: 0x00026A00 File Offset: 0x00024C00
		public TriggerOptions TriggerOptions
		{
			get
			{
				return this.triggerOptions;
			}
			set
			{
				this.triggerOptions = value;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x00026A09 File Offset: 0x00024C09
		public TimeSpan Elapsed
		{
			get
			{
				this.CheckDisallowedOptions(ContextOptions.DoNotMeasureTime);
				return this.timer.Elapsed;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x00026A1D File Offset: 0x00024C1D
		public TimeSpan ElapsedCpu
		{
			get
			{
				this.CheckDisallowedOptions(ContextOptions.DoNotMeasureTime);
				return this.timer.ElapsedCpu;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x00026A31 File Offset: 0x00024C31
		public string Version
		{
			get
			{
				return this.assemblyVersion;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x00026A39 File Offset: 0x00024C39
		public DateTime TimeStarted
		{
			get
			{
				return this.timeStarted;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x00026A41 File Offset: 0x00024C41
		public bool HasTrustworthyCpuTime
		{
			get
			{
				return this.timer != null && (MyStopwatch.CpuTimeIsAvailable && !this.timer.FinishedOnDifferentProcessor) && !this.timer.PowerManagementChangeOccurred;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x00026A71 File Offset: 0x00024C71
		internal static int EstimatedStringCapacity
		{
			get
			{
				return LatencyDetectionContext.estimatedStringCapacity;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x00026A78 File Offset: 0x00024C78
		// (set) Token: 0x06000A60 RID: 2656 RVA: 0x00026A80 File Offset: 0x00024C80
		internal object StackTraceContext { get; private set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x00026A89 File Offset: 0x00024C89
		internal LatencyDetectionLocation Location
		{
			get
			{
				return this.latencyDetectionLocation;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x00026A94 File Offset: 0x00024C94
		internal ICollection<LabeledTimeSpan> Latencies
		{
			get
			{
				if (this.latencies == null)
				{
					bool flag = MyStopwatch.CpuTimeIsAvailable && this.timer != null;
					int capacity = this.taskData.Length + (flag ? 1 : 0);
					this.latencies = new List<LabeledTimeSpan>(capacity);
					if (flag)
					{
						LabeledTimeSpan item = new LabeledTimeSpan("CPU", this.timer.ElapsedCpu);
						this.latencies.Add(item);
					}
					for (int i = 0; i < this.taskData.Length; i++)
					{
						int milliseconds = this.taskData[i].Difference.Milliseconds;
						LabeledTimeSpan item2 = new LabeledTimeSpan(this.providers[i].Name, TimeSpan.FromMilliseconds((double)milliseconds));
						this.latencies.Add(item2);
					}
				}
				return this.latencies;
			}
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00026B5F File Offset: 0x00024D5F
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00026B69 File Offset: 0x00024D69
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00026B74 File Offset: 0x00024D74
		public string ToString(string format, IFormatProvider formatProvider)
		{
			StringBuilder stringBuilder = new StringBuilder(LatencyDetectionContext.EstimatedStringCapacity);
			stringBuilder.AppendLine("---");
			LatencyDetectionContext.AppendLine(stringBuilder, "Location: ", this.latencyDetectionLocation.Identity);
			LatencyDetectionContext.AppendLine(stringBuilder, "Version: ", this.assemblyVersion);
			LatencyDetectionContext.AppendLine(stringBuilder, "Stack Trace Context: ", this.StackTraceContext.ToString());
			if (!string.IsNullOrEmpty(this.UserIdentity))
			{
				LatencyDetectionContext.AppendLine(stringBuilder, "User Identity: ", this.UserIdentity);
			}
			LatencyDetectionContext.AppendLine(stringBuilder, "Started: ", this.timeStarted.ToString(CultureInfo.InvariantCulture));
			TimeSpan elapsed = this.Elapsed;
			LatencyDetectionContext.AppendLine(stringBuilder, "Total Time: ", elapsed.ToString());
			if (MyStopwatch.CpuTimeIsAvailable && this.timer != null)
			{
				TimeSpan elapsedCpu = this.timer.ElapsedCpu;
				LatencyDetectionContext.AppendLine(stringBuilder, "Elapsed in CPU: ", elapsedCpu.ToString());
				LatencyDetectionContext.AppendLine(stringBuilder, "Elapsed in CPU (% of Latency): ", (100.0 * elapsedCpu.TotalMilliseconds / elapsed.TotalMilliseconds).ToString(CultureInfo.InvariantCulture));
				if (this.timer.FinishedOnDifferentProcessor)
				{
					stringBuilder.AppendLine("Finished on different processor.");
				}
				if (this.timer.PowerManagementChangeOccurred)
				{
					stringBuilder.AppendLine("Power management change occured.");
				}
			}
			for (int i = 0; i < this.providers.Length; i++)
			{
				TaskPerformanceData taskPerformanceData = this.taskData[i];
				uint count = taskPerformanceData.Difference.Count;
				if (count > 0U)
				{
					string name = this.providers[i].Name;
					LatencyDetectionContext.AppendLine<uint>(stringBuilder, name, " Count: ", count);
					LatencyDetectionContext.AppendLine<int>(stringBuilder, name, " Latency: ", taskPerformanceData.Difference.Milliseconds, " ms");
					if (format != "s" && !string.IsNullOrEmpty(taskPerformanceData.Operations))
					{
						LatencyDetectionContext.AppendLine<string>(stringBuilder, name, " Operations: ", taskPerformanceData.Operations);
					}
				}
			}
			LatencyDetectionContext.estimatedStringCapacity = Math.Min(Math.Max(LatencyDetectionContext.estimatedStringCapacity, stringBuilder.Capacity), 42000);
			return stringBuilder.ToString();
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00026DA4 File Offset: 0x00024FA4
		public TaskPerformanceData[] StopAndFinalizeCollection()
		{
			if (this.timer != null)
			{
				this.timer.Stop();
			}
			for (int i = 0; i < this.providers.Length; i++)
			{
				IPerformanceDataProvider performanceDataProvider = this.providers[i];
				TaskPerformanceData taskPerformanceData = this.taskData[i];
				taskPerformanceData.End = performanceDataProvider.TakeSnapshot(false);
				taskPerformanceData.Operations = performanceDataProvider.Operations;
				performanceDataProvider.ResetOperations();
				if (performanceDataProvider.ThreadLocal)
				{
					taskPerformanceData.InvalidateIfAsynchronous();
				}
			}
			if ((this.contextOptions & ContextOptions.DoNotCreateReport) != ContextOptions.DoNotCreateReport && LatencyDetectionContext.options.LatencyDetectionEnabled)
			{
				LatencyDetectionContext.reporter.Log(this);
			}
			return this.taskData;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00026E3E File Offset: 0x0002503E
		internal static void ValidateBinningParameters(LatencyDetectionLocation location, string version, object hash)
		{
			if (location == null)
			{
				throw new ArgumentNullException("location");
			}
			if (string.IsNullOrEmpty(version))
			{
				throw new ArgumentException("May not be null or empty.", "version");
			}
			if (hash == null)
			{
				throw new ArgumentNullException("hash");
			}
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00026E74 File Offset: 0x00025074
		internal LatencyDetectionException CreateLatencyDetectionException()
		{
			LatencyDetectionException ex = null;
			int num = (int)(this.Elapsed.TotalMilliseconds / 2.0);
			if (this.HasTrustworthyCpuTime && this.timer.ElapsedCpu.TotalMilliseconds >= (double)num)
			{
				ex = new CpuLatencyDetectionException(this);
			}
			else if (this.providers.Length > 0)
			{
				for (int i = 0; i < this.providers.Length; i++)
				{
					int milliseconds = this.taskData[i].Difference.Milliseconds;
					if (milliseconds >= num)
					{
						ex = new DataProviderLatencyDetectionException(this, this.providers[i]);
						break;
					}
				}
			}
			if (ex == null)
			{
				ex = new LatencyDetectionException(this);
			}
			return ex;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00026F1C File Offset: 0x0002511C
		private static void AppendLine(StringBuilder builder, string param1, string param2)
		{
			builder.Append(param1).AppendLine(param2);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00026F2C File Offset: 0x0002512C
		private static void AppendLine<T>(StringBuilder builder, string param1, string param2, T param3)
		{
			builder.Append(param1).Append(param2).Append(param3.ToString()).AppendLine();
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00026F53 File Offset: 0x00025153
		private static void AppendLine<T>(StringBuilder builder, string param1, string param2, T param3, string param4)
		{
			builder.Append(param1).Append(param2).Append(param3.ToString()).Append(param4).AppendLine();
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00026F84 File Offset: 0x00025184
		private void SetDataProviders(IPerformanceDataProvider[] dataProviders)
		{
			int num = (dataProviders != null) ? dataProviders.Length : 0;
			this.providers = new IPerformanceDataProvider[num];
			this.taskData = new TaskPerformanceData[num];
			for (int i = 0; i < num; i++)
			{
				IPerformanceDataProvider performanceDataProvider = dataProviders[i];
				if (performanceDataProvider == null)
				{
					string message = string.Format(CultureInfo.InvariantCulture, "dataProviders[{0}] was null.", new object[]
					{
						i
					});
					throw new ArgumentNullException("dataProviders", message);
				}
				this.providers[i] = performanceDataProvider;
				TaskPerformanceData taskPerformanceData = new TaskPerformanceData();
				taskPerformanceData.Start = performanceDataProvider.TakeSnapshot(true);
				this.taskData[i] = taskPerformanceData;
			}
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0002701E File Offset: 0x0002521E
		private void CheckDisallowedOptions(ContextOptions testOptions)
		{
			if ((this.contextOptions & testOptions) == testOptions)
			{
				throw new InvalidOperationException(string.Format("The operation is not allowed due to the mode selected at creation time. testOptions = {0}, contextOptions = {1}.", testOptions, this.contextOptions));
			}
		}

		// Token: 0x04000708 RID: 1800
		public const string UserIdentityLabel = "User Identity: ";

		// Token: 0x04000709 RID: 1801
		private const int MaxDepthToLog = 16;

		// Token: 0x0400070A RID: 1802
		internal const int MaxStringBuilderCapacity = 42000;

		// Token: 0x0400070B RID: 1803
		private const string NullOrEmptyError = "May not be null or empty.";

		// Token: 0x0400070C RID: 1804
		private const string ContextSlot = "LatencyDetectionStack";

		// Token: 0x0400070D RID: 1805
		private const string ContextDivider = "---";

		// Token: 0x0400070E RID: 1806
		private const string LocationLabel = "Location: ";

		// Token: 0x0400070F RID: 1807
		private const string VersionLabel = "Version: ";

		// Token: 0x04000710 RID: 1808
		private const string StackTraceContextLabel = "Stack Trace Context: ";

		// Token: 0x04000711 RID: 1809
		private const string StartTimeLabel = "Started: ";

		// Token: 0x04000712 RID: 1810
		private const string LatencyLabel = "Total Time: ";

		// Token: 0x04000713 RID: 1811
		private const string ProviderCountLabel = " Count: ";

		// Token: 0x04000714 RID: 1812
		private const string ProviderLatencyLabel = " Latency: ";

		// Token: 0x04000715 RID: 1813
		private const string ProviderOperationsLabel = " Operations: ";

		// Token: 0x04000716 RID: 1814
		private const string MsecUnits = " ms";

		// Token: 0x04000717 RID: 1815
		private const string ElapsedInCpuLabel = "Elapsed in CPU: ";

		// Token: 0x04000718 RID: 1816
		private const string CpuPercentage = "Elapsed in CPU (% of Latency): ";

		// Token: 0x04000719 RID: 1817
		private const string FinishedOnDifferentProcessor = "Finished on different processor.";

		// Token: 0x0400071A RID: 1818
		private const string PowerManagementChangeOccured = "Power management change occured.";

		// Token: 0x0400071B RID: 1819
		private static readonly PerformanceReportingOptions options = PerformanceReportingOptions.Instance;

		// Token: 0x0400071C RID: 1820
		private static readonly PerformanceReporter reporter = PerformanceReporter.Instance;

		// Token: 0x0400071D RID: 1821
		private static int estimatedStringCapacity = 360;

		// Token: 0x0400071E RID: 1822
		private readonly DateTime timeStarted = DateTime.UtcNow;

		// Token: 0x0400071F RID: 1823
		private readonly MyStopwatch timer;

		// Token: 0x04000720 RID: 1824
		private readonly ContextOptions contextOptions;

		// Token: 0x04000721 RID: 1825
		private TriggerOptions triggerOptions;

		// Token: 0x04000722 RID: 1826
		private LatencyDetectionLocation latencyDetectionLocation;

		// Token: 0x04000723 RID: 1827
		private string assemblyVersion;

		// Token: 0x04000724 RID: 1828
		private IPerformanceDataProvider[] providers;

		// Token: 0x04000725 RID: 1829
		private TaskPerformanceData[] taskData;

		// Token: 0x04000726 RID: 1830
		private List<LabeledTimeSpan> latencies;
	}
}
