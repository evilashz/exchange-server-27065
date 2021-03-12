using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.Core.Diagnostics
{
	// Token: 0x0200009B RID: 155
	internal class OperatorDiagnostics : IComparable<OperatorDiagnostics>
	{
		// Token: 0x06000494 RID: 1172 RVA: 0x0000E1E8 File Offset: 0x0000C3E8
		public OperatorDiagnostics(string flowIdentifier, DiagnosticsLogConfig.LogDefaults logDefaults)
		{
			this.FlowIdentifier = flowIdentifier;
			this.DropBreadcrumb(OperatorLocation.DiagnosticsStarted, "OperatorDiagnostics", TimeSpan.Zero, null);
			this.failedItemsLog = new DiagnosticsLog(new DiagnosticsLogConfig(logDefaults), OperatorDiagnostics.failedItemsSchemaColumns);
			this.languageDetectionLog = new DiagnosticsLog(new DiagnosticsLogConfig(OperatorDiagnosticsFactory.LanguageDetectionLogDefaults), OperatorDiagnostics.languageDetectionSchemaColumns);
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0000E27C File Offset: 0x0000C47C
		// (set) Token: 0x06000496 RID: 1174 RVA: 0x0000E284 File Offset: 0x0000C484
		public string FlowIdentifier { get; private set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0000E28D File Offset: 0x0000C48D
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x0000E295 File Offset: 0x0000C495
		public Guid InstanceGuid { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0000E29E File Offset: 0x0000C49E
		// (set) Token: 0x0600049A RID: 1178 RVA: 0x0000E2A6 File Offset: 0x0000C4A6
		public string InstanceName { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0000E2B0 File Offset: 0x0000C4B0
		public OperatorTimingEntry LastEntry
		{
			get
			{
				OperatorTimingEntry result;
				lock (this.lockObject)
				{
					result = this.operatorTimings[this.operatorTimings.Count - 1];
				}
				return result;
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000E304 File Offset: 0x0000C504
		public TimeSpan GetSplitTime()
		{
			return this.timer.GetSplitTime();
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000E314 File Offset: 0x0000C514
		public XElement GetBreadcrumbs(bool verbose)
		{
			XElement xelement = new XElement("Session");
			xelement.Add(new XElement("FlowIdentifier", this.FlowIdentifier));
			XElement xelement2 = new XElement("Breadcrumbs");
			xelement.Add(xelement2);
			lock (this.lockObject)
			{
				if (verbose)
				{
					for (int i = 1; i < 33; i++)
					{
						int index = (this.breadcrumbIndex + i) % 32;
						XElement breadcrumb = this.GetBreadcrumb(index);
						if (breadcrumb != null)
						{
							xelement2.Add(breadcrumb);
						}
					}
				}
				else
				{
					xelement2.Add(this.GetBreadcrumb(this.breadcrumbIndex));
				}
			}
			return xelement;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000E3DC File Offset: 0x0000C5DC
		public TimeSpan DropBreadcrumb(OperatorLocation location, string operatorName)
		{
			return this.DropBreadcrumb(location, operatorName, this.timer.GetLapTime(), null);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000E3F2 File Offset: 0x0000C5F2
		public TimeSpan DropBreadcrumb(OperatorLocation location, string operatorName, string exception)
		{
			return this.DropBreadcrumb(location, operatorName, this.timer.GetLapTime(), null);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000E408 File Offset: 0x0000C608
		public TimeSpan DropBreadcrumb(OperatorLocation location, string operatorName, TimeSpan elapsed)
		{
			return this.DropBreadcrumb(location, operatorName, elapsed, null);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0000E414 File Offset: 0x0000C614
		public TimeSpan DropBreadcrumb(OperatorLocation location, string operatorName, TimeSpan elapsed, string exception)
		{
			if (location == OperatorLocation.None)
			{
				throw new ArgumentException("location");
			}
			lock (this.lockObject)
			{
				if (++this.breadcrumbIndex == 32)
				{
					this.breadcrumbIndex = 0;
				}
				this.breadcrumbs[this.breadcrumbIndex].OperatorName = operatorName;
				this.breadcrumbs[this.breadcrumbIndex].Location = location;
				this.breadcrumbs[this.breadcrumbIndex].Timestamp = DateTime.UtcNow;
				this.breadcrumbs[this.breadcrumbIndex].Elapsed = elapsed;
				this.breadcrumbs[this.breadcrumbIndex].Exception = exception;
			}
			if (this.operatorTimings.Count < 32)
			{
				this.operatorTimings.Add(new OperatorTimingEntry
				{
					Name = operatorName,
					Location = location,
					Elapsed = (long)elapsed.TotalMilliseconds
				});
			}
			return elapsed;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000E534 File Offset: 0x0000C734
		public void ClearOperatorTimings()
		{
			this.operatorTimings.Clear();
			this.timer.Reset();
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0000E54C File Offset: 0x0000C74C
		public string GetOperatorTimings(List<string> operatorTimingEntryNames)
		{
			return OperatorTimingEntry.SerializeList(this.operatorTimings, operatorTimingEntryNames);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000E55C File Offset: 0x0000C75C
		public bool GetOperatorTimingEntry(string entryName, OperatorLocation operatorLocation, out OperatorTimingEntry entry)
		{
			entry = default(OperatorTimingEntry);
			foreach (OperatorTimingEntry operatorTimingEntry in this.operatorTimings)
			{
				if (operatorTimingEntry.Name == entryName && operatorTimingEntry.Location == operatorLocation)
				{
					entry = operatorTimingEntry;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000E5D8 File Offset: 0x0000C7D8
		public int CompareTo(OperatorDiagnostics other)
		{
			int num = string.Compare(this.InstanceName, other.InstanceName, StringComparison.OrdinalIgnoreCase);
			if (num != 0)
			{
				return num;
			}
			num = this.InstanceGuid.CompareTo(other.InstanceGuid);
			if (num != 0)
			{
				return num;
			}
			return string.Compare(this.FlowIdentifier, other.FlowIdentifier, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000E62C File Offset: 0x0000C82C
		public void LogFailedItem(DateTime lastAttemptTime, string identity, Guid correlationId, bool partiallyProcessed, int attemptCount, string errorCode, string errorMessage)
		{
			this.failedItemsLog.Append(new object[]
			{
				null,
				lastAttemptTime.ToString("u"),
				identity,
				correlationId,
				partiallyProcessed,
				attemptCount,
				errorCode,
				errorMessage
			});
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000E688 File Offset: 0x0000C888
		public void LogLanguageDetection(Guid contextID, string detectedLanguage, long languageDetectorTime, long wordBreakerTime, long messageLength, string messageCodePage, string messageLocaleID, string internetCPID)
		{
			this.languageDetectionLog.Append(new object[]
			{
				null,
				contextID,
				detectedLanguage,
				languageDetectorTime,
				wordBreakerTime,
				messageLength,
				messageCodePage,
				messageLocaleID,
				internetCPID
			});
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0000E6E4 File Offset: 0x0000C8E4
		private XElement GetBreadcrumb(int index)
		{
			if (this.breadcrumbs[index].Location == OperatorLocation.None)
			{
				return null;
			}
			XElement xelement = new XElement("Breadcrumb");
			xelement.Add(new XElement("Operator", this.breadcrumbs[index].OperatorName));
			xelement.Add(new XElement("Location", this.breadcrumbs[index].Location));
			xelement.Add(new XElement("Timestamp", this.breadcrumbs[index].Timestamp));
			xelement.Add(new XElement("Elapsed", (long)this.breadcrumbs[index].Elapsed.TotalMilliseconds));
			if (this.breadcrumbs[index].Exception != null)
			{
				xelement.Add(new XElement("Exception", this.breadcrumbs[index].Exception));
			}
			return xelement;
		}

		// Token: 0x0400020C RID: 524
		private const int BreadcrumbsSize = 32;

		// Token: 0x0400020D RID: 525
		private static string[] failedItemsSchemaColumns = new string[]
		{
			"date-time",
			"failed-time",
			"identity",
			"correlation-id",
			"attempt-count",
			"partially-processed",
			"error-code",
			"error-message"
		};

		// Token: 0x0400020E RID: 526
		private static string[] languageDetectionSchemaColumns = new string[]
		{
			"date-time",
			"contextid",
			"detectedlanguage",
			"languagedetectortime",
			"wordbreakertime",
			"messagelength",
			"messagecodepage",
			"messagelocaleid",
			"internetcpid"
		};

		// Token: 0x0400020F RID: 527
		private readonly object lockObject = new object();

		// Token: 0x04000210 RID: 528
		private readonly OperatorDiagnostics.Breadcrumb[] breadcrumbs = new OperatorDiagnostics.Breadcrumb[32];

		// Token: 0x04000211 RID: 529
		private readonly LapTimer timer = new LapTimer();

		// Token: 0x04000212 RID: 530
		private int breadcrumbIndex = -1;

		// Token: 0x04000213 RID: 531
		private List<OperatorTimingEntry> operatorTimings = new List<OperatorTimingEntry>(20);

		// Token: 0x04000214 RID: 532
		private DiagnosticsLog failedItemsLog;

		// Token: 0x04000215 RID: 533
		private DiagnosticsLog languageDetectionLog;

		// Token: 0x0200009C RID: 156
		private struct Breadcrumb
		{
			// Token: 0x04000219 RID: 537
			public OperatorLocation Location;

			// Token: 0x0400021A RID: 538
			public string OperatorName;

			// Token: 0x0400021B RID: 539
			public DateTime Timestamp;

			// Token: 0x0400021C RID: 540
			public object Exception;

			// Token: 0x0400021D RID: 541
			public TimeSpan Elapsed;
		}
	}
}
