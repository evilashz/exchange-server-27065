using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.EDiscovery;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Logging;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Infrastructure
{
	// Token: 0x0200002C RID: 44
	internal class Recorder
	{
		// Token: 0x060001F6 RID: 502 RVA: 0x000114F7 File Offset: 0x0000F6F7
		public Recorder(ISearchPolicy policy)
		{
			this.stopwatch.Start();
			this.policy = policy;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00011532 File Offset: 0x0000F732
		public long Timestamp
		{
			get
			{
				return this.stopwatch.ElapsedMilliseconds + this.offset;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00011546 File Offset: 0x0000F746
		public IEnumerable<KeyValuePair<string, Recorder.Record>> Records
		{
			get
			{
				return this.records;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0001154E File Offset: 0x0000F74E
		// (set) Token: 0x060001FA RID: 506 RVA: 0x00011556 File Offset: 0x0000F756
		public long ConversionTime { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0001155F File Offset: 0x0000F75F
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00011567 File Offset: 0x0000F767
		public long LoggingTime { get; set; }

		// Token: 0x060001FD RID: 509 RVA: 0x00011570 File Offset: 0x0000F770
		public static void Trace(long id, TraceType traceType, object parameter1)
		{
			if (ExTraceGlobals.WebServiceTracer.IsTraceEnabled(traceType))
			{
				string formatString = "{0}";
				switch (traceType)
				{
				case TraceType.DebugTrace:
					ExTraceGlobals.WebServiceTracer.TraceDebug(id, formatString, new object[]
					{
						parameter1
					});
					return;
				case TraceType.WarningTrace:
					ExTraceGlobals.WebServiceTracer.TraceWarning(id, formatString, new object[]
					{
						parameter1
					});
					return;
				case TraceType.ErrorTrace:
					ExTraceGlobals.WebServiceTracer.TraceError(id, formatString, new object[]
					{
						parameter1
					});
					return;
				case TraceType.PerformanceTrace:
					ExTraceGlobals.WebServiceTracer.TracePerformance(id, formatString, new object[]
					{
						parameter1
					});
					return;
				}
				ExTraceGlobals.WebServiceTracer.Information(id, formatString, new object[]
				{
					parameter1
				});
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0001163C File Offset: 0x0000F83C
		public static void Trace(long id, TraceType traceType, object parameter1, object parameter2)
		{
			if (ExTraceGlobals.WebServiceTracer.IsTraceEnabled(traceType))
			{
				string formatString = "{0} {1}";
				switch (traceType)
				{
				case TraceType.DebugTrace:
					ExTraceGlobals.WebServiceTracer.TraceDebug(id, formatString, new object[]
					{
						parameter1,
						parameter2
					});
					return;
				case TraceType.WarningTrace:
					ExTraceGlobals.WebServiceTracer.TraceWarning(id, formatString, new object[]
					{
						parameter1,
						parameter2
					});
					return;
				case TraceType.ErrorTrace:
					ExTraceGlobals.WebServiceTracer.TraceError(id, formatString, new object[]
					{
						parameter1,
						parameter2
					});
					return;
				case TraceType.PerformanceTrace:
					ExTraceGlobals.WebServiceTracer.TracePerformance(id, formatString, new object[]
					{
						parameter1,
						parameter2
					});
					return;
				}
				ExTraceGlobals.WebServiceTracer.Information(id, formatString, new object[]
				{
					parameter1,
					parameter2
				});
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00011720 File Offset: 0x0000F920
		public static void Trace(long id, TraceType traceType, params object[] parameters)
		{
			if (parameters != null && parameters.Length > 0 && ExTraceGlobals.WebServiceTracer.IsTraceEnabled(traceType))
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < parameters.Length; i++)
				{
					stringBuilder.AppendFormat("{{{0}}}", i);
				}
				string formatString = stringBuilder.ToString();
				switch (traceType)
				{
				case TraceType.DebugTrace:
					ExTraceGlobals.WebServiceTracer.TraceDebug(id, formatString, parameters);
					return;
				case TraceType.WarningTrace:
					ExTraceGlobals.WebServiceTracer.TraceWarning(id, formatString, parameters);
					return;
				case TraceType.ErrorTrace:
					ExTraceGlobals.WebServiceTracer.TraceError(id, formatString, parameters);
					return;
				case TraceType.PerformanceTrace:
					ExTraceGlobals.WebServiceTracer.TracePerformance(id, formatString, parameters);
					return;
				}
				ExTraceGlobals.WebServiceTracer.Information(id, formatString, parameters);
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000117E4 File Offset: 0x0000F9E4
		public Recorder.Record Start(string description, TraceType traceType = TraceType.InfoTrace, bool isAggregate = true)
		{
			return new Recorder.Record
			{
				Description = description,
				TraceType = traceType,
				IsAggregateRecord = isAggregate
			};
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000118F0 File Offset: 0x0000FAF0
		public void End(Recorder.Record record)
		{
			this.SafeLog(delegate
			{
				long timestamp = this.Timestamp;
				try
				{
					record.Attributes["DURATION"] = (double)this.Timestamp - record.StartTime;
					if (!record.IsAggregateRecord || record.TraceType == TraceType.ErrorTrace || record.TraceType == TraceType.FatalTrace)
					{
						this.WriteLog(record);
					}
					if (record.IsAggregateRecord)
					{
						this.AppendRecord(record);
					}
				}
				finally
				{
					this.LoggingTime += this.Timestamp - timestamp;
				}
			});
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00011A58 File Offset: 0x0000FC58
		public void Merge(string server, NameValueCollection headers)
		{
			this.SafeLog(delegate
			{
				long timestamp = this.Timestamp;
				try
				{
					if (headers != null && !string.IsNullOrEmpty(server))
					{
						StringBuilder stringBuilder = new StringBuilder();
						this.servers.TryAdd(server, server);
						for (int i = 0; i < 10; i++)
						{
							string name = string.Format("DiscoveryLog{0}", i);
							string value = headers.Get(name);
							if (string.IsNullOrEmpty(value))
							{
								break;
							}
							stringBuilder.Append(value);
						}
						if (stringBuilder.Length > 0)
						{
							JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
							ICollection<Recorder.Record> collection = javaScriptSerializer.Deserialize<ICollection<Recorder.Record>>(stringBuilder.ToString());
							foreach (Recorder.Record record in collection)
							{
								this.AppendRecord(record);
							}
						}
					}
				}
				finally
				{
					this.LoggingTime += this.Timestamp - timestamp;
				}
			});
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00011BD4 File Offset: 0x0000FDD4
		public void Write(NameValueCollection headers, Action<string, string> protocolLog)
		{
			this.SafeLog(delegate
			{
				StringBuilder stringBuilder = new StringBuilder();
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				Recorder.Record record = this.Start("Logging", TraceType.InfoTrace, true);
				record.Attributes["CONVTIME"] = this.ConversionTime;
				record.Attributes["LOGTIME"] = this.LoggingTime;
				this.End(record);
				javaScriptSerializer.Serialize(this.records.Values, stringBuilder);
				for (int i = 0; i < 10; i++)
				{
					string text = string.Format("DiscoveryLog{0}", i);
					string text2 = this.Segment(stringBuilder, i, 10000);
					if (!string.IsNullOrEmpty(text2))
					{
						this.WriteLog(text, text2, TraceType.InfoTrace);
						if (protocolLog != null)
						{
							protocolLog(text, text2.Replace(',', '~'));
						}
					}
					if (headers != null)
					{
						string value = this.Segment(stringBuilder, i, 500);
						if (!string.IsNullOrEmpty(value))
						{
							headers[text] = value;
						}
					}
				}
			});
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00011C6C File Offset: 0x0000FE6C
		public void WriteTimestampHeader(IDictionary<string, string> headers)
		{
			this.SafeLog(delegate
			{
				if (headers != null)
				{
					string value = string.Format("{0},{1}", this.Timestamp, DateTime.UtcNow.Ticks);
					headers["X-DiscoveryLogTimestamp"] = value;
				}
			});
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00011D94 File Offset: 0x0000FF94
		public void ReadTimestampHeader(NameValueCollection headers)
		{
			this.SafeLog(delegate
			{
				if (headers != null && headers.AllKeys.Contains("X-DiscoveryLogTimestamp"))
				{
					string text = headers["X-DiscoveryLogTimestamp"];
					if (!string.IsNullOrWhiteSpace(text))
					{
						string[] array = text.Split(new char[]
						{
							','
						});
						if (array.Length == 2)
						{
							long num = 0L;
							long num2 = 0L;
							if (long.TryParse(array[0], out num2) && long.TryParse(array[1], out num))
							{
								long num3 = DateTime.UtcNow.Ticks - num;
								if (num3 > 0L)
								{
									this.offset = num3 + num2;
									Recorder.Record record = this.Start("CallLatency", TraceType.InfoTrace, true);
									record.Attributes["WORKDURATION"] = num3;
									this.End(record);
								}
							}
						}
					}
				}
			});
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00011DE4 File Offset: 0x0000FFE4
		private void AppendRecord(Recorder.Record record)
		{
			this.records.AddOrUpdate(record.Description, record, (string key, Recorder.Record existing) => this.UpdateRecord(existing, record));
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00011ED4 File Offset: 0x000100D4
		private Recorder.Record UpdateRecord(Recorder.Record existing, Recorder.Record source)
		{
			if (existing != null && source != null)
			{
				using (IEnumerator<string> enumerator = source.Attributes.Keys.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Recorder.<>c__DisplayClass15 CS$<>8__locals2 = new Recorder.<>c__DisplayClass15();
						CS$<>8__locals2.key = enumerator.Current;
						object sourceValue;
						if (source.Attributes.TryGetValue(CS$<>8__locals2.key, out sourceValue))
						{
							existing.Attributes.AddOrUpdate(CS$<>8__locals2.key, sourceValue, delegate(string existingKey, object existingValue)
							{
								if (existingValue == null)
								{
									return sourceValue;
								}
								double num;
								double num2;
								if (double.TryParse(sourceValue.ToString(), out num) && double.TryParse(existingValue.ToString(), out num2))
								{
									return num + num2;
								}
								for (int i = 1; i < 10; i++)
								{
									string key = string.Format("{0}{1}", CS$<>8__locals2.key, i);
									if (existing.Attributes.TryAdd(key, sourceValue))
									{
										break;
									}
								}
								return existingValue;
							});
						}
					}
					goto IL_BE;
				}
			}
			if (source != null)
			{
				return source;
			}
			IL_BE:
			return existing;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00011FB8 File Offset: 0x000101B8
		private void WriteLog(Recorder.Record record)
		{
			StringBuilder stringBuilder = new StringBuilder();
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.Serialize(record, stringBuilder);
			this.WriteLog(record.Description, stringBuilder.ToString(), record.TraceType);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00011FF4 File Offset: 0x000101F4
		private void WriteLog(string title, string data, TraceType traceType)
		{
			string stateAttribute = null;
			if (this.policy != null && this.policy.CallerInfo != null)
			{
				stateAttribute = this.policy.CallerInfo.UserAgent;
			}
			ResultSeverityLevel severity = ResultSeverityLevel.Error;
			if (traceType != TraceType.ErrorTrace || traceType != TraceType.FatalTrace)
			{
				severity = ResultSeverityLevel.Informational;
			}
			if (traceType == TraceType.FatalTrace || traceType == TraceType.ErrorTrace)
			{
				string text = (traceType == TraceType.FatalTrace) ? "Search.FailureMonitor" : "Mailbox.FailureMonitor";
				LogItem logItem = new LogItem(ExchangeComponent.EdiscoveryProtocol.Name, text, data, ResultSeverityLevel.Error);
				logItem.ResultName = string.Format("{0}/{1}", ExchangeComponent.EdiscoveryProtocol.Name, text);
				logItem.CustomProperties["tenant"] = this.GetTenantDomain();
				logItem.Publish(false);
				return;
			}
			LogItem.Publish("EDiscovery", "Reporting", title, data, stateAttribute, severity, false);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000120B0 File Offset: 0x000102B0
		private string GetTenantDomain()
		{
			object organizationId = this.policy.CallerInfo.OrganizationId;
			if (organizationId != null)
			{
				return organizationId.ToString();
			}
			return string.Empty;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000120E0 File Offset: 0x000102E0
		private string Segment(StringBuilder data, int segment, int segmentSize)
		{
			if (segmentSize > 0)
			{
				int num = segment * segmentSize;
				if (data.Length > num)
				{
					int num2 = Math.Min(data.Length - num, segmentSize);
					if (num2 > 0)
					{
						return data.ToString(num, num2);
					}
				}
			}
			return null;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00012174 File Offset: 0x00010374
		private void SafeLog(Action logFunc)
		{
			try
			{
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					try
					{
						logFunc();
					}
					catch (ArgumentException)
					{
					}
					catch (InvalidOperationException)
					{
					}
					catch (ReflectionTypeLoadException)
					{
					}
				});
			}
			catch (GrayException)
			{
			}
		}

		// Token: 0x040000F7 RID: 247
		private const string DiscoveryLogKey = "DiscoveryLog{0}";

		// Token: 0x040000F8 RID: 248
		private const string DiscoveryTimestampHeader = "X-DiscoveryLogTimestamp";

		// Token: 0x040000F9 RID: 249
		private const int DiscoveryMaxLogs = 10;

		// Token: 0x040000FA RID: 250
		private const int DiscoveryMaxHeaderSize = 500;

		// Token: 0x040000FB RID: 251
		private const int DiscoveryMaxProtocolSize = 10000;

		// Token: 0x040000FC RID: 252
		private ConcurrentDictionary<string, Recorder.Record> records = new ConcurrentDictionary<string, Recorder.Record>();

		// Token: 0x040000FD RID: 253
		private ConcurrentDictionary<string, string> servers = new ConcurrentDictionary<string, string>();

		// Token: 0x040000FE RID: 254
		private Stopwatch stopwatch = new Stopwatch();

		// Token: 0x040000FF RID: 255
		private ISearchPolicy policy;

		// Token: 0x04000100 RID: 256
		private long offset;

		// Token: 0x0200002D RID: 45
		public class Layer
		{
			// Token: 0x04000103 RID: 259
			public const long EntryPoint = 1L;

			// Token: 0x04000104 RID: 260
			public const long Infrastrucuture = 2L;

			// Token: 0x04000105 RID: 261
			public const long Task = 4L;

			// Token: 0x04000106 RID: 262
			public const long ExternalProvider = 5L;
		}

		// Token: 0x0200002E RID: 46
		public class Record
		{
			// Token: 0x0600020E RID: 526 RVA: 0x000121C4 File Offset: 0x000103C4
			public Record()
			{
				this.Attributes = new ConcurrentDictionary<string, object>();
				this.IsAggregateRecord = true;
			}

			// Token: 0x17000074 RID: 116
			// (get) Token: 0x0600020F RID: 527 RVA: 0x000121DE File Offset: 0x000103DE
			// (set) Token: 0x06000210 RID: 528 RVA: 0x000121E6 File Offset: 0x000103E6
			[ScriptIgnore]
			public bool IsAggregateRecord { get; set; }

			// Token: 0x17000075 RID: 117
			// (get) Token: 0x06000211 RID: 529 RVA: 0x000121EF File Offset: 0x000103EF
			// (set) Token: 0x06000212 RID: 530 RVA: 0x000121F7 File Offset: 0x000103F7
			[ScriptIgnore]
			public double StartTime { get; set; }

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x06000213 RID: 531 RVA: 0x00012200 File Offset: 0x00010400
			// (set) Token: 0x06000214 RID: 532 RVA: 0x00012208 File Offset: 0x00010408
			public string Description { get; set; }

			// Token: 0x17000077 RID: 119
			// (get) Token: 0x06000215 RID: 533 RVA: 0x00012211 File Offset: 0x00010411
			// (set) Token: 0x06000216 RID: 534 RVA: 0x00012219 File Offset: 0x00010419
			public TraceType TraceType { get; set; }

			// Token: 0x17000078 RID: 120
			// (get) Token: 0x06000217 RID: 535 RVA: 0x00012222 File Offset: 0x00010422
			// (set) Token: 0x06000218 RID: 536 RVA: 0x0001222A File Offset: 0x0001042A
			public ConcurrentDictionary<string, object> Attributes { get; set; }
		}
	}
}
