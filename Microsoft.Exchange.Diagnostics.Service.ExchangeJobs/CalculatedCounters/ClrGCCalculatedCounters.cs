using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Etw;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;
using Microsoft.Win32;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.CalculatedCounters
{
	// Token: 0x02000003 RID: 3
	public class ClrGCCalculatedCounters : ICalculatedCounter, IDisposable
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002240 File Offset: 0x00000440
		public ClrGCCalculatedCounters(PerfLogExtension perfLogExt, string guidFolderPath, bool shouldCreateTimer)
		{
			this.perfLogExt = perfLogExt;
			this.lastStatisticsRefresh = DateTime.MinValue;
			this.statisticsRefreshFrequency = Configuration.GetConfigTimeSpan("EtwTraceCollectionRepeatInterval", TimeSpan.FromSeconds(30.0), TimeSpan.FromDays(7.0), TimeSpan.FromMinutes(1.0));
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(ClrGCCalculatedCounters.DiagnosticsRegistryKey))
			{
				if (registryKey != null)
				{
					string text = (string)registryKey.GetValue("MsiInstallPath");
					string text2 = (string)registryKey.GetValue("LogFolderPath");
					if (!string.IsNullOrEmpty(text))
					{
						this.traceLogPath = string.Format("{0}\\tracelog.exe", text);
					}
					if (!string.IsNullOrEmpty(text2))
					{
						string text3;
						if (guidFolderPath == null)
						{
							text3 = string.Format("{0}\\EtwTraces", text2);
						}
						else
						{
							text3 = guidFolderPath;
						}
						HashSet<Guid> etwGuids = new HashSet<Guid>
						{
							GCPrivateEventsParser.ProviderGuid
						};
						if (!Directory.Exists(text3))
						{
							throw new ApplicationException(string.Format("Cannot find path {0}", text3));
						}
						this.guidFilePath = this.CreateGuidFile(text3, "guid.guid", etwGuids);
						this.etlFilePaths = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
						{
							{
								"CalculatedCounterTrace",
								Path.Combine(text3, "CalculatedCounterTrace.etl")
							}
						};
					}
				}
			}
			this.parserCollection.Add(GCPrivateEventsParser.ProviderGuid, new GCPrivateEventsParser());
			if (shouldCreateTimer)
			{
				this.statsTimer = new Timer(new TimerCallback(this.TryRefreshCounters), null, 10L, (long)this.statisticsRefreshFrequency.TotalMilliseconds);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000023F4 File Offset: 0x000005F4
		public void OnLogHeader(List<KeyValuePair<int, DiagnosticMeasurement>> currentInputCounters)
		{
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000023F6 File Offset: 0x000005F6
		public void Dispose()
		{
			if (this.statsTimer != null)
			{
				this.statsTimer.Dispose();
			}
			this.currentCounters = null;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000241C File Offset: 0x0000061C
		public void OnLogLine(Dictionary<DiagnosticMeasurement, float?> countersAndValues, DateTime? timestamp = null)
		{
			Dictionary<string, Dictionary<int, double>> dictionary;
			if (this.GetCurrentCounters(out dictionary))
			{
				lock (this.dictLock)
				{
					foreach (KeyValuePair<string, Dictionary<int, double>> keyValuePair in dictionary)
					{
						HashSet<int> hashSet = new HashSet<int>();
						try
						{
							foreach (KeyValuePair<int, double> keyValuePair2 in keyValuePair.Value)
							{
								string empty = string.Empty;
								if (this.perfLogExt.PidToProcessName.TryGetValue((float)keyValuePair2.Key, out empty))
								{
									DiagnosticMeasurement measure = DiagnosticMeasurement.GetMeasure(Environment.MachineName, ".NET CLR Memory", keyValuePair.Key, empty);
									int num = PerfLogExtension.CounterRemapType(measure);
									if (num >= 0)
									{
										this.perfLogExt.RemapInstance[num].AddCounter(measure, new float?((float)keyValuePair2.Value));
									}
									else
									{
										countersAndValues[measure] = new float?((float)keyValuePair2.Value);
									}
								}
								else
								{
									hashSet.Add(keyValuePair2.Key);
								}
							}
							this.perfLogExt.RemapInstance[1].Process(countersAndValues);
							double num2 = (from a in keyValuePair.Value
							select a.Value).Sum();
							DiagnosticMeasurement measure2 = DiagnosticMeasurement.GetMeasure(Environment.MachineName, ".NET CLR Memory", keyValuePair.Key, "_Global_");
							countersAndValues.Add(measure2, new float?((float)num2));
							this.RemovePidsOnExit(hashSet);
						}
						catch (Exception ex)
						{
							Log.LogErrorMessage("ClrGCCalculatedCounter.OnLogLine failed due to exception {0}.", new object[]
							{
								ex
							});
						}
					}
				}
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002658 File Offset: 0x00000858
		private void RefreshCounters()
		{
			try
			{
				EtwTraceCollector etwTraceCollector = new EtwTraceCollector(this.guidFilePath, this.etlFilePaths, this.traceLogPath);
				Log.LogInformationMessage("ETW calculated counters: calling initialize", new object[0]);
				if (etwTraceCollector.Initialize())
				{
					ClrGCCalculatedCounters.EtwCounterDataContainer etwCounterDataContainer = new ClrGCCalculatedCounters.EtwCounterDataContainer(this.etlFilePaths, this.parserCollection);
					etwCounterDataContainer.BeginParsing();
					Log.LogInformationMessage("ETW calculated counters: Started refreshing counters from fresh data.", new object[0]);
					Dictionary<string, Dictionary<int, double>> gccounters = etwCounterDataContainer.GetGCCounters();
					if (this.currentCounters == null)
					{
						Log.LogInformationMessage("ETW calculated counters: 1st time population of counter data.", new object[0]);
						this.currentCounters = gccounters;
						this.doingWork = false;
						return;
					}
					using (Dictionary<string, Dictionary<int, double>>.Enumerator enumerator = gccounters.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<string, Dictionary<int, double>> keyValuePair = enumerator.Current;
							lock (this.dictLock)
							{
								Dictionary<int, double> dictionary;
								if (this.currentCounters.TryGetValue(keyValuePair.Key, out dictionary))
								{
									using (Dictionary<int, double>.Enumerator enumerator2 = keyValuePair.Value.GetEnumerator())
									{
										while (enumerator2.MoveNext())
										{
											KeyValuePair<int, double> keyValuePair2 = enumerator2.Current;
											double num;
											if (!dictionary.TryGetValue(keyValuePair2.Key, out num))
											{
												this.currentCounters[keyValuePair.Key].Add(keyValuePair2.Key, keyValuePair2.Value);
											}
											else
											{
												this.currentCounters[keyValuePair.Key][keyValuePair2.Key] = keyValuePair2.Value;
											}
										}
										continue;
									}
								}
								this.currentCounters.Add(keyValuePair.Key, keyValuePair.Value);
							}
						}
						goto IL_1CB;
					}
				}
				this.lastStatisticsRefresh = DateTime.MinValue;
				Log.LogErrorMessage("ETW calculated counter: Unable to query ETW data for '{0}' server.", new object[]
				{
					Environment.MachineName
				});
				IL_1CB:;
			}
			catch (Exception ex)
			{
				Log.LogErrorMessage("ETW calculated counter: Unable to query ETW data for '{0}' server. due to exception {1}, {2}", new object[]
				{
					Environment.MachineName,
					ex.Message,
					ex.StackTrace
				});
			}
			this.doingWork = false;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000028D8 File Offset: 0x00000AD8
		private string CreateGuidFile(string outputFilePath, string guidFileName, HashSet<Guid> etwGuids)
		{
			string text = Path.Combine(outputFilePath, guidFileName);
			string contents = string.Join<Guid>(Environment.NewLine, etwGuids);
			File.WriteAllText(text, contents);
			if (File.Exists(text))
			{
				Log.LogInformationMessage("ETW calculated counter: Created GUID file {0}", new object[]
				{
					text
				});
				return text;
			}
			Log.LogErrorMessage("ETW calculated counter: Could not create GUID file", new object[0]);
			return null;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002934 File Offset: 0x00000B34
		private void RemovePidsOnExit(HashSet<int> pids)
		{
			foreach (int key in pids)
			{
				if (this.currentCounters == null)
				{
					break;
				}
				lock (this.dictLock)
				{
					foreach (KeyValuePair<string, Dictionary<int, double>> keyValuePair in this.currentCounters)
					{
						keyValuePair.Value.Remove(key);
					}
				}
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000029FC File Offset: 0x00000BFC
		private bool GetCurrentCounters(out Dictionary<string, Dictionary<int, double>> currentCounters)
		{
			currentCounters = this.currentCounters;
			return currentCounters != null;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002A18 File Offset: 0x00000C18
		private void TryRefreshCounters(object obj)
		{
			bool flag = DateTime.UtcNow - this.lastStatisticsRefresh >= this.statisticsRefreshFrequency || this.currentCounters == null;
			Log.LogInformationMessage("ETW calculated counter: TryRefresh was triggered, Last update time: {0}, Statistics refresh freq is set to:{1}, will collect ETL?:{2}", new object[]
			{
				this.lastStatisticsRefresh,
				this.statisticsRefreshFrequency,
				flag
			});
			if (flag && !this.doingWork)
			{
				this.doingWork = true;
				Log.LogInformationMessage("ETW calculated counter: Triggering threadpool", new object[0]);
				ThreadPool.QueueUserWorkItem(delegate(object state)
				{
					this.RefreshCounters();
				});
				this.lastStatisticsRefresh = DateTime.UtcNow;
			}
		}

		// Token: 0x04000003 RID: 3
		public const string EtwTraceCollectionRepeatInterval = "EtwTraceCollectionRepeatInterval";

		// Token: 0x04000004 RID: 4
		internal const string ClrMemoryObject = ".NET CLR Memory";

		// Token: 0x04000005 RID: 5
		internal const string Gen2FragmentationInMBCounterName = "Gen 2 Fragmentation(MB)";

		// Token: 0x04000006 RID: 6
		internal const string LOHFragmentationInMBCounterName = "LOH Fragmentation(MB)";

		// Token: 0x04000007 RID: 7
		internal const string TotalInstanceName = "_Global_";

		// Token: 0x04000008 RID: 8
		private const string CalculatedCounterTraceLoggerName = "CalculatedCounterTrace";

		// Token: 0x04000009 RID: 9
		private const string CalculatedCounterTraceFile = "CalculatedCounterTrace.etl";

		// Token: 0x0400000A RID: 10
		private static readonly string DiagnosticsRegistryKey = string.Format("Software\\Microsoft\\ExchangeServer\\{0}\\Diagnostics", "v15");

		// Token: 0x0400000B RID: 11
		private readonly string traceLogPath;

		// Token: 0x0400000C RID: 12
		private readonly string guidFilePath;

		// Token: 0x0400000D RID: 13
		private readonly Dictionary<string, string> etlFilePaths;

		// Token: 0x0400000E RID: 14
		private readonly Dictionary<Guid, IParser> parserCollection = new Dictionary<Guid, IParser>();

		// Token: 0x0400000F RID: 15
		private readonly TimeSpan statisticsRefreshFrequency;

		// Token: 0x04000010 RID: 16
		private Dictionary<string, Dictionary<int, double>> currentCounters;

		// Token: 0x04000011 RID: 17
		private Timer statsTimer;

		// Token: 0x04000012 RID: 18
		private DateTime lastStatisticsRefresh;

		// Token: 0x04000013 RID: 19
		private volatile bool doingWork;

		// Token: 0x04000014 RID: 20
		private object dictLock = new object();

		// Token: 0x04000015 RID: 21
		private PerfLogExtension perfLogExt;

		// Token: 0x02000004 RID: 4
		internal class EtwCounterDataContainer
		{
			// Token: 0x06000010 RID: 16 RVA: 0x00002AE8 File Offset: 0x00000CE8
			public EtwCounterDataContainer(Dictionary<string, string> etlFilePaths, Dictionary<Guid, IParser> parserCollection)
			{
				this.etlFilePaths = etlFilePaths;
				this.logFile = default(EtwTraceNativeComponents.EVENT_TRACE_LOGFILEW);
				this.parserCollection = parserCollection;
				this.handles = new ulong[1];
				if (parserCollection.Count < 1)
				{
					throw new ArgumentException("Not enough parsers in the parser list");
				}
			}

			// Token: 0x06000011 RID: 17 RVA: 0x00002B38 File Offset: 0x00000D38
			internal void BeginParsing()
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.etlFilePaths)
				{
					Log.LogInformationMessage("ETW calculated counter: Started processing {0}", new object[]
					{
						keyValuePair
					});
					this.logFile.LogFileName = keyValuePair.Value;
					this.logFile.BufferCallback = new EtwTraceNativeComponents.EventTraceBufferCallback(ClrGCCalculatedCounters.EtwCounterDataContainer.TraceEventBufferCallback);
					this.logFile.LogFileMode = 268435456U;
					this.logFile.LogFileMode = (this.logFile.LogFileMode | 4096U);
					this.logFile.EventCallback = new EtwTraceNativeComponents.EventTraceEventCallback(this.RawDispatch);
					this.handles[0] = EtwTraceNativeComponents.OpenTrace(ref this.logFile);
					int num = EtwTraceNativeComponents.ProcessTrace(this.handles, (uint)this.handles.Length, (IntPtr)0, (IntPtr)0);
					if (num == 6)
					{
						throw new ApplicationException(string.Format("Error opening ETL file {0}", keyValuePair));
					}
					EtwTraceNativeComponents.CloseTrace(this.handles[0]);
				}
			}

			// Token: 0x06000012 RID: 18 RVA: 0x00002C68 File Offset: 0x00000E68
			internal Dictionary<string, Dictionary<int, double>> GetGCCounters()
			{
				Dictionary<int, long[][]> dictionary = new Dictionary<int, long[][]>();
				Dictionary<string, Dictionary<int, double>> dictionary2 = new Dictionary<string, Dictionary<int, double>>();
				IParser parser;
				this.parserCollection.TryGetValue(GCPrivateEventsParser.ProviderGuid, out parser);
				if (parser != null)
				{
					GCPrivateEventsParser gcprivateEventsParser = (GCPrivateEventsParser)parser;
					dictionary = gcprivateEventsParser.GetGenData();
				}
				Dictionary<int, double> dictionary3 = new Dictionary<int, double>();
				Dictionary<int, double> dictionary4 = new Dictionary<int, double>();
				foreach (KeyValuePair<int, long[][]> keyValuePair in dictionary)
				{
					double value = (double)this.ConvertBytesToMegabytes((float)keyValuePair.Value[0][2]);
					double value2 = (double)this.ConvertBytesToMegabytes((float)keyValuePair.Value[0][3]);
					dictionary3.Add(keyValuePair.Key, value);
					dictionary4.Add(keyValuePair.Key, value2);
				}
				dictionary2.Add("Gen 2 Fragmentation(MB)", dictionary3);
				dictionary2.Add("LOH Fragmentation(MB)", dictionary4);
				return dictionary2;
			}

			// Token: 0x06000013 RID: 19 RVA: 0x00002D54 File Offset: 0x00000F54
			[AllowReversePInvokeCalls]
			private static bool TraceEventBufferCallback(IntPtr rawLogFile)
			{
				return true;
			}

			// Token: 0x06000014 RID: 20 RVA: 0x00002D57 File Offset: 0x00000F57
			private float ConvertBytesToMegabytes(float value)
			{
				return value * ClrGCCalculatedCounters.EtwCounterDataContainer.mbConverter;
			}

			// Token: 0x06000015 RID: 21 RVA: 0x00002D60 File Offset: 0x00000F60
			[AllowReversePInvokeCalls]
			private unsafe void RawDispatch(EtwTraceNativeComponents.EVENT_RECORD* rawData)
			{
				IParser parser;
				this.parserCollection.TryGetValue(rawData->EventHeader.ProviderId, out parser);
				if (parser != null)
				{
					parser.Parse(rawData);
				}
			}

			// Token: 0x04000017 RID: 23
			private static float mbConverter = 9.536743E-07f;

			// Token: 0x04000018 RID: 24
			private readonly Dictionary<Guid, IParser> parserCollection;

			// Token: 0x04000019 RID: 25
			private readonly Dictionary<string, string> etlFilePaths;

			// Token: 0x0400001A RID: 26
			private EtwTraceNativeComponents.EVENT_TRACE_LOGFILEW logFile;

			// Token: 0x0400001B RID: 27
			private ulong[] handles;
		}
	}
}
