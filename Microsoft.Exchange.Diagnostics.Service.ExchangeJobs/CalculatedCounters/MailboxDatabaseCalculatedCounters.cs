using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Service.Common;
using Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Common;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.CalculatedCounters
{
	// Token: 0x02000007 RID: 7
	public sealed class MailboxDatabaseCalculatedCounters : ICalculatedCounter
	{
		// Token: 0x0600001F RID: 31 RVA: 0x0000323C File Offset: 0x0000143C
		static MailboxDatabaseCalculatedCounters()
		{
			Dictionary<string, Func<float[], float>> dictionary = new Dictionary<string, Func<float[], float>>(StringComparer.OrdinalIgnoreCase);
			dictionary.Add("Consumed Megabytes", (float[] propertyValues) => MailboxDatabaseCalculatedCounters.ConvertBytesToMegabytes(propertyValues[0]));
			dictionary.Add("Available New Mailbox Space In Megabytes", (float[] propertyValues) => MailboxDatabaseCalculatedCounters.ConvertBytesToMegabytes(propertyValues[0]));
			dictionary.Add("Logical Consumed Megabytes", (float[] propertyValues) => MailboxDatabaseCalculatedCounters.ConvertBytesToMegabytes(propertyValues[0]));
			dictionary.Add("Disconnected Logical Consumed Megabytes", (float[] propertyValues) => MailboxDatabaseCalculatedCounters.ConvertBytesToMegabytes(propertyValues[0] + propertyValues[1]));
			dictionary.Add("Mailbox Count", (float[] propertyValues) => propertyValues[0]);
			dictionary.Add("Disconnected Mailbox Count", (float[] propertyValues) => propertyValues[0]);
			dictionary.Add("Log Consumed Megabytes", (float[] propertyValues) => MailboxDatabaseCalculatedCounters.ConvertBytesToMegabytes(propertyValues[0]));
			dictionary.Add("Catalog Consumed Megabytes", (float[] propertyValues) => MailboxDatabaseCalculatedCounters.ConvertBytesToMegabytes(propertyValues[0]));
			dictionary.Add("Logical To Physical Size Ratio", (float[] propertyValues) => propertyValues[0]);
			MailboxDatabaseCalculatedCounters.CounterLogic = dictionary;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003558 File Offset: 0x00001758
		public MailboxDatabaseCalculatedCounters()
		{
			this.databaseRefreshFrequency = Configuration.GetConfigTimeSpan("MailboxDatabaseCalculatedCounterADRefreshFrequency", TimeSpan.FromMinutes(1.0), TimeSpan.FromDays(7.0), TimeSpan.FromDays(1.0));
			this.statisticsRefreshFrequency = Configuration.GetConfigTimeSpan("MailboxDatabaseCalculatedCounterStatisticsRefreshFrequency", TimeSpan.FromMinutes(5.0), TimeSpan.FromDays(1.0), TimeSpan.FromMinutes(5.0));
			this.lastMailboxDatabaseNameRefresh = DateTime.MinValue;
			this.lastStatisticsRefresh = DateTime.MinValue;
			this.IsValidRole = ServerRole.IsRole("Mailbox");
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00003605 File Offset: 0x00001805
		// (set) Token: 0x06000022 RID: 34 RVA: 0x0000360D File Offset: 0x0000180D
		internal bool IsValidRole { get; set; }

		// Token: 0x06000023 RID: 35 RVA: 0x00003616 File Offset: 0x00001816
		public void OnLogHeader(List<KeyValuePair<int, DiagnosticMeasurement>> currentInputCounters)
		{
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003618 File Offset: 0x00001818
		public void OnLogLine(Dictionary<DiagnosticMeasurement, float?> countersAndValues, DateTime? timestamp = null)
		{
			if (!this.IsValidRole)
			{
				return;
			}
			Dictionary<MailboxDatabase, List<MailboxDatabaseCalculatedCounters.DiagnosticMeasurementValue>> dictionary;
			if (this.TryRefreshCounters(out dictionary))
			{
				HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
				foreach (KeyValuePair<MailboxDatabase, List<MailboxDatabaseCalculatedCounters.DiagnosticMeasurementValue>> keyValuePair in dictionary)
				{
					foreach (MailboxDatabaseCalculatedCounters.DiagnosticMeasurementValue diagnosticMeasurementValue in keyValuePair.Value)
					{
						if (!diagnosticMeasurementValue.Measure.InstanceName.Equals("_Total", StringComparison.OrdinalIgnoreCase) || hashSet.Add(diagnosticMeasurementValue.Measure.CounterName))
						{
							countersAndValues.Add(diagnosticMeasurementValue.Measure, diagnosticMeasurementValue.Value);
						}
					}
				}
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000036FC File Offset: 0x000018FC
		internal static float ConvertBytesToMegabytes(float value)
		{
			return value / 1024f / 1024f;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000370C File Offset: 0x0000190C
		internal static Dictionary<MailboxDatabase, List<MailboxDatabaseCalculatedCounters.DiagnosticMeasurementValue>> CreateCounters(IEnumerable<MailboxDatabase> databases)
		{
			if (databases == null)
			{
				throw new ArgumentNullException("databases");
			}
			Dictionary<MailboxDatabase, List<MailboxDatabaseCalculatedCounters.DiagnosticMeasurementValue>> dictionary = new Dictionary<MailboxDatabase, List<MailboxDatabaseCalculatedCounters.DiagnosticMeasurementValue>>();
			Dictionary<string, MailboxDatabaseCalculatedCounters.DiagnosticMeasurementValue> dictionary2 = new Dictionary<string, MailboxDatabaseCalculatedCounters.DiagnosticMeasurementValue>(StringComparer.OrdinalIgnoreCase);
			foreach (MailboxDatabase mailboxDatabase in databases)
			{
				if (!dictionary.ContainsKey(mailboxDatabase))
				{
					List<MailboxDatabaseCalculatedCounters.DiagnosticMeasurementValue> list = new List<MailboxDatabaseCalculatedCounters.DiagnosticMeasurementValue>(MailboxDatabaseCalculatedCounters.CounterNames.Count * 2);
					foreach (string text in MailboxDatabaseCalculatedCounters.CounterNames)
					{
						DiagnosticMeasurement measure = DiagnosticMeasurement.GetMeasure(Environment.MachineName, "MSExchangeIS Store", text, mailboxDatabase.Name);
						list.Add(new MailboxDatabaseCalculatedCounters.DiagnosticMeasurementValue(measure));
						MailboxDatabaseCalculatedCounters.DiagnosticMeasurementValue diagnosticMeasurementValue;
						if (!dictionary2.TryGetValue(text, out diagnosticMeasurementValue))
						{
							measure = DiagnosticMeasurement.GetMeasure(Environment.MachineName, "MSExchangeIS Store", text, "_Total");
							diagnosticMeasurementValue = new MailboxDatabaseCalculatedCounters.DiagnosticMeasurementValue(measure);
							dictionary2.Add(text, diagnosticMeasurementValue);
						}
						list.Add(diagnosticMeasurementValue);
					}
					dictionary.Add(mailboxDatabase, list);
				}
			}
			return dictionary;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003840 File Offset: 0x00001A40
		internal static bool TryCreateCounters(IEnumerable<MailboxDatabase> databases, out Dictionary<MailboxDatabase, List<MailboxDatabaseCalculatedCounters.DiagnosticMeasurementValue>> counters)
		{
			if (databases == null)
			{
				throw new ArgumentNullException("databases");
			}
			counters = null;
			Dictionary<MailboxDatabase, List<MailboxDatabaseCalculatedCounters.DiagnosticMeasurementValue>> dictionary = MailboxDatabaseCalculatedCounters.CreateCounters(databases);
			List<MailboxDatabase> list = new List<MailboxDatabase>(dictionary.Count);
			foreach (KeyValuePair<MailboxDatabase, List<MailboxDatabaseCalculatedCounters.DiagnosticMeasurementValue>> keyValuePair in dictionary)
			{
				Dictionary<string, float> dictionary2;
				if (!keyValuePair.Key.TryLoadStatistics(out dictionary2))
				{
					list.Add(keyValuePair.Key);
				}
				else
				{
					foreach (MailboxDatabaseCalculatedCounters.DiagnosticMeasurementValue diagnosticMeasurementValue in keyValuePair.Value)
					{
						string[] array;
						Func<float[], float> func;
						if (MailboxDatabaseCalculatedCounters.CounterProperties.TryGetValue(diagnosticMeasurementValue.Measure.CounterName, out array) && MailboxDatabaseCalculatedCounters.CounterLogic.TryGetValue(diagnosticMeasurementValue.Measure.CounterName, out func))
						{
							bool flag = true;
							float[] array2 = new float[array.Length];
							for (int i = 0; i < array.Length; i++)
							{
								if (!dictionary2.TryGetValue(array[i], out array2[i]))
								{
									flag = false;
									break;
								}
							}
							if (!flag)
							{
								list.Add(keyValuePair.Key);
								break;
							}
							if (diagnosticMeasurementValue.Value == null)
							{
								diagnosticMeasurementValue.Value = new float?(0f);
							}
							MailboxDatabaseCalculatedCounters.DiagnosticMeasurementValue diagnosticMeasurementValue2 = diagnosticMeasurementValue;
							float? value = diagnosticMeasurementValue2.Value;
							float num = func(array2);
							diagnosticMeasurementValue2.Value = ((value != null) ? new float?(value.GetValueOrDefault() + num) : null);
						}
					}
				}
			}
			foreach (MailboxDatabase key in list)
			{
				dictionary.Remove(key);
			}
			bool flag2 = dictionary.Any<KeyValuePair<MailboxDatabase, List<MailboxDatabaseCalculatedCounters.DiagnosticMeasurementValue>>>();
			if (flag2)
			{
				counters = dictionary;
			}
			return flag2;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003A7C File Offset: 0x00001C7C
		internal void RefreshCounters(bool updateDatabases, bool updateStatistics)
		{
			if (updateDatabases)
			{
				IEnumerable<MailboxDatabase> enumerable;
				if (MailboxDatabase.TryDiscoverLocalMailboxDatabases(out enumerable))
				{
					this.currentDatabases = enumerable;
				}
				else
				{
					this.lastMailboxDatabaseNameRefresh = DateTime.MinValue;
					Log.LogErrorMessage("Unable to discover any mailbox databases in AD for the '{0}' server.", new object[]
					{
						Environment.MachineName
					});
				}
			}
			if (updateStatistics && this.currentDatabases != null)
			{
				Dictionary<MailboxDatabase, List<MailboxDatabaseCalculatedCounters.DiagnosticMeasurementValue>> dictionary;
				if (MailboxDatabaseCalculatedCounters.TryCreateCounters(this.currentDatabases, out dictionary))
				{
					this.currentCounters = dictionary;
				}
				else
				{
					this.lastStatisticsRefresh = DateTime.MinValue;
					Log.LogErrorMessage("Unable to create counters.", new object[0]);
				}
			}
			this.doingWork = false;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003B2C File Offset: 0x00001D2C
		private bool TryRefreshCounters(out Dictionary<MailboxDatabase, List<MailboxDatabaseCalculatedCounters.DiagnosticMeasurementValue>> currentCounters)
		{
			bool updateDatabases = DateTime.UtcNow - this.lastMailboxDatabaseNameRefresh >= this.databaseRefreshFrequency || this.currentCounters == null || this.currentDatabases == null;
			bool updateStatistics = updateDatabases | DateTime.UtcNow - this.lastStatisticsRefresh >= this.statisticsRefreshFrequency;
			if ((updateDatabases || updateStatistics) && !this.doingWork)
			{
				if (updateDatabases)
				{
					this.lastMailboxDatabaseNameRefresh = DateTime.UtcNow;
				}
				if (updateStatistics)
				{
					this.lastStatisticsRefresh = DateTime.UtcNow;
				}
				this.doingWork = true;
				ThreadPool.QueueUserWorkItem(delegate(object state)
				{
					this.RefreshCounters(updateDatabases, updateStatistics);
				});
			}
			currentCounters = this.currentCounters;
			return currentCounters != null;
		}

		// Token: 0x0400002F RID: 47
		public const string MailboxDatabaseCalculatedCounterADRefreshFrequencyProperty = "MailboxDatabaseCalculatedCounterADRefreshFrequency";

		// Token: 0x04000030 RID: 48
		public const string MailboxDatabaseCalculatedCounterStatisticsRefreshFrequencyProperty = "MailboxDatabaseCalculatedCounterStatisticsRefreshFrequency";

		// Token: 0x04000031 RID: 49
		internal const string MSExchangeISStoreObjectName = "MSExchangeIS Store";

		// Token: 0x04000032 RID: 50
		internal const string ConsumedMegabytesCounterName = "Consumed Megabytes";

		// Token: 0x04000033 RID: 51
		internal const string AvailableNewMailboxSpaceInMegabytesCounterName = "Available New Mailbox Space In Megabytes";

		// Token: 0x04000034 RID: 52
		internal const string LogicalConsumedMegabytesCounterName = "Logical Consumed Megabytes";

		// Token: 0x04000035 RID: 53
		internal const string DisconnectedLogicalConsumedMegabytesCounterName = "Disconnected Logical Consumed Megabytes";

		// Token: 0x04000036 RID: 54
		internal const string CatalogConsumedMegabytesCounterName = "Catalog Consumed Megabytes";

		// Token: 0x04000037 RID: 55
		internal const string LogConsumedMegabytesCounterName = "Log Consumed Megabytes";

		// Token: 0x04000038 RID: 56
		internal const string MailboxCountCounterName = "Mailbox Count";

		// Token: 0x04000039 RID: 57
		internal const string DisconnectedMailboxCountCounterName = "Disconnected Mailbox Count";

		// Token: 0x0400003A RID: 58
		internal const string LogicalPhysicalSizeRatioCounterName = "Logical To Physical Size Ratio";

		// Token: 0x0400003B RID: 59
		internal const string TotalInstanceName = "_Total";

		// Token: 0x0400003C RID: 60
		private static readonly List<string> CounterNames = new List<string>
		{
			"Consumed Megabytes",
			"Available New Mailbox Space In Megabytes",
			"Logical Consumed Megabytes",
			"Disconnected Logical Consumed Megabytes",
			"Mailbox Count",
			"Disconnected Mailbox Count",
			"Catalog Consumed Megabytes",
			"Log Consumed Megabytes",
			"Logical To Physical Size Ratio"
		};

		// Token: 0x0400003D RID: 61
		private static readonly Dictionary<string, string[]> CounterProperties = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"Consumed Megabytes",
				new string[]
				{
					"DatabasePhysicalUsedSize"
				}
			},
			{
				"Available New Mailbox Space In Megabytes",
				new string[]
				{
					"AvailableNewMailboxSpace"
				}
			},
			{
				"Logical Consumed Megabytes",
				new string[]
				{
					"DatabaseLogicalSize"
				}
			},
			{
				"Disconnected Logical Consumed Megabytes",
				new string[]
				{
					"DisconnectedTotalItemSize",
					"DisconnectedTotalDeletedItemSize"
				}
			},
			{
				"Mailbox Count",
				new string[]
				{
					"MailboxCount"
				}
			},
			{
				"Disconnected Mailbox Count",
				new string[]
				{
					"DisconnectedMailboxCount"
				}
			},
			{
				"Log Consumed Megabytes",
				new string[]
				{
					"LogSize"
				}
			},
			{
				"Catalog Consumed Megabytes",
				new string[]
				{
					"CatalogSize"
				}
			},
			{
				"Logical To Physical Size Ratio",
				new string[]
				{
					"LogicalPhysicalSizeRatio"
				}
			}
		};

		// Token: 0x0400003E RID: 62
		private static readonly Dictionary<string, Func<float[], float>> CounterLogic;

		// Token: 0x0400003F RID: 63
		private readonly TimeSpan databaseRefreshFrequency;

		// Token: 0x04000040 RID: 64
		private readonly TimeSpan statisticsRefreshFrequency;

		// Token: 0x04000041 RID: 65
		private Dictionary<MailboxDatabase, List<MailboxDatabaseCalculatedCounters.DiagnosticMeasurementValue>> currentCounters;

		// Token: 0x04000042 RID: 66
		private IEnumerable<MailboxDatabase> currentDatabases;

		// Token: 0x04000043 RID: 67
		private DateTime lastMailboxDatabaseNameRefresh;

		// Token: 0x04000044 RID: 68
		private DateTime lastStatisticsRefresh;

		// Token: 0x04000045 RID: 69
		private volatile bool doingWork;

		// Token: 0x02000008 RID: 8
		internal sealed class DiagnosticMeasurementValue
		{
			// Token: 0x06000033 RID: 51 RVA: 0x00003C17 File Offset: 0x00001E17
			public DiagnosticMeasurementValue(DiagnosticMeasurement measure)
			{
				if (measure == null)
				{
					throw new ArgumentNullException("measure");
				}
				this.measure = measure;
			}

			// Token: 0x06000034 RID: 52 RVA: 0x00003C34 File Offset: 0x00001E34
			public DiagnosticMeasurementValue(MailboxDatabaseCalculatedCounters.DiagnosticMeasurementValue other)
			{
				this.measure = other.Measure;
				this.Value = other.Value;
			}

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000035 RID: 53 RVA: 0x00003C54 File Offset: 0x00001E54
			public DiagnosticMeasurement Measure
			{
				get
				{
					return this.measure;
				}
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000036 RID: 54 RVA: 0x00003C5C File Offset: 0x00001E5C
			// (set) Token: 0x06000037 RID: 55 RVA: 0x00003C64 File Offset: 0x00001E64
			public float? Value { get; set; }

			// Token: 0x04000050 RID: 80
			private readonly DiagnosticMeasurement measure;
		}
	}
}
