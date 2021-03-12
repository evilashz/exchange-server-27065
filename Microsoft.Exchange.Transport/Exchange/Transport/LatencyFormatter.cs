using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Protocols.Smtp;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000170 RID: 368
	internal class LatencyFormatter
	{
		// Token: 0x06001024 RID: 4132 RVA: 0x000414A4 File Offset: 0x0003F6A4
		public LatencyFormatter(LatencyTracker latencyTracker, string localServerFqdn, DateTime localArrivalTime, DateTime orgArrivalTime, bool includeOrgArrivalTime, bool reportEndToEnd, bool setUpdateTotalPerfCounter = false)
		{
			if (setUpdateTotalPerfCounter)
			{
				this.flags |= LatencyFormatter.FormatFlags.UpdateTotalPerfCounter;
			}
			this.priority = DeliveryPriority.Normal;
			this.localTracker = latencyTracker;
			this.localArrivalTime = localArrivalTime;
			this.orgArrivalTime = orgArrivalTime;
			this.localServerFqdn = (string.IsNullOrEmpty(localServerFqdn) ? ComputerInformation.DnsPhysicalFullyQualifiedDomainName : localServerFqdn);
			if (reportEndToEnd)
			{
				this.flags |= LatencyFormatter.FormatFlags.ReportEndToEnd;
			}
			if (includeOrgArrivalTime)
			{
				this.flags |= LatencyFormatter.FormatFlags.IncludeOrgArrivalTime;
			}
			this.externalDeliveryEnqueueTime = DateTime.MinValue;
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0004154E File Offset: 0x0003F74E
		public LatencyFormatter(IReadOnlyMailItem mailItem, string localServerFqdn, bool reportEndToEnd, DateTime externalDeliveryEnqueueTime, DateTime orgArrivalTime) : this(mailItem, localServerFqdn, LatencyFormatter.FormatFlags.IncludeOrgArrivalTime | LatencyFormatter.FormatFlags.UpdateTotalPerfCounter | (reportEndToEnd ? LatencyFormatter.FormatFlags.ReportEndToEnd : LatencyFormatter.FormatFlags.None), externalDeliveryEnqueueTime, orgArrivalTime)
		{
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x00041567 File Offset: 0x0003F767
		public LatencyFormatter(IReadOnlyMailItem mailItem, string localServerFqdn, bool reportEndToEnd) : this(mailItem, localServerFqdn, LatencyFormatter.FormatFlags.IncludeOrgArrivalTime | LatencyFormatter.FormatFlags.UpdateTotalPerfCounter | (reportEndToEnd ? LatencyFormatter.FormatFlags.ReportEndToEnd : LatencyFormatter.FormatFlags.None), DateTime.MinValue, DateTime.MinValue)
		{
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x00041588 File Offset: 0x0003F788
		private LatencyFormatter(IReadOnlyMailItem mailItem, string localServerFqdn, LatencyFormatter.FormatFlags flags, DateTime externalDeliveryEnqueueTime, DateTime orgArrivalTime)
		{
			this.priority = mailItem.Priority;
			this.localTracker = mailItem.LatencyTracker;
			this.flags = flags;
			this.localServerFqdn = (string.IsNullOrEmpty(localServerFqdn) ? ComputerInformation.DnsPhysicalFullyQualifiedDomainName : localServerFqdn);
			this.localArrivalTime = mailItem.DateReceived;
			if (orgArrivalTime != DateTime.MinValue)
			{
				this.orgArrivalTime = orgArrivalTime;
			}
			else
			{
				Util.TryGetOrganizationalMessageArrivalTime(mailItem, out this.orgArrivalTime);
			}
			this.externalDeliveryEnqueueTime = externalDeliveryEnqueueTime;
			if (this.ReportEndToEnd)
			{
				this.previousHops = LatencyHeaderManager.GetPreviousHops(mailItem);
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x0004163E File Offset: 0x0003F83E
		private bool EnableHeaderFolding
		{
			get
			{
				return (this.flags & LatencyFormatter.FormatFlags.EnableHeaderFolding) != LatencyFormatter.FormatFlags.None;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x0004164E File Offset: 0x0003F84E
		private bool IgnoreTotalThreshold
		{
			get
			{
				return (this.flags & LatencyFormatter.FormatFlags.IgnoreTotalThreshold) != LatencyFormatter.FormatFlags.None;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x0004165E File Offset: 0x0003F85E
		private bool IncludeCurrentTime
		{
			get
			{
				return (this.flags & LatencyFormatter.FormatFlags.IncludeCurrentTime) != LatencyFormatter.FormatFlags.None;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x0004166E File Offset: 0x0003F86E
		private bool IncludeOrgArrivalTime
		{
			get
			{
				return (this.flags & LatencyFormatter.FormatFlags.IncludeOrgArrivalTime) != LatencyFormatter.FormatFlags.None;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x0004167E File Offset: 0x0003F87E
		private bool ReportEndToEnd
		{
			get
			{
				return (this.flags & LatencyFormatter.FormatFlags.ReportEndToEnd) != LatencyFormatter.FormatFlags.None;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x0004168F File Offset: 0x0003F88F
		private bool UpdateTotalPerfCounter
		{
			get
			{
				return (this.flags & LatencyFormatter.FormatFlags.UpdateTotalPerfCounter) != LatencyFormatter.FormatFlags.None;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x000416A0 File Offset: 0x0003F8A0
		public TimeSpan EndToEndLatency
		{
			get
			{
				return this.endToEndLatency;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x0600102F RID: 4143 RVA: 0x000416A8 File Offset: 0x0003F8A8
		public TimeSpan ExternalSendLatency
		{
			get
			{
				return this.externalSendLatency;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x000416B0 File Offset: 0x0003F8B0
		private bool TotalFromOrgArrivalTime
		{
			get
			{
				return (this.flags & LatencyFormatter.FormatFlags.CalculateTotalFromOrgArrivalTime) != LatencyFormatter.FormatFlags.None;
			}
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x000416C4 File Offset: 0x0003F8C4
		public static string FormatLatencyInProgressHeader(IReadOnlyMailItem mailItem, string localServerFqdn, DateTime orgArrivalTime, bool useTreeFormat)
		{
			LatencyFormatter.FormatFlags formatFlags = LatencyFormatter.FormatFlags.None;
			if (orgArrivalTime != DateTime.MinValue)
			{
				formatFlags = LatencyFormatter.FormatFlags.CalculateTotalFromOrgArrivalTime;
			}
			LatencyFormatter latencyFormatter = new LatencyFormatter(mailItem, localServerFqdn, LatencyFormatter.FormatFlags.EnableHeaderFolding | LatencyFormatter.FormatFlags.IgnoreTotalThreshold | LatencyFormatter.FormatFlags.IncludeCurrentTime | formatFlags, DateTime.MinValue, orgArrivalTime);
			return latencyFormatter.FormatAndUpdatePerfCounters(useTreeFormat);
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x000416FC File Offset: 0x0003F8FC
		public static string FormatExternalLatencyHeader(string fqdn, TimeSpan totalLatency)
		{
			if (string.IsNullOrEmpty(fqdn))
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			LatencyFormatter.AppendServerFqdn(stringBuilder, true, fqdn, 0, fqdn.Length);
			LatencyRecord record;
			if (LatencyTracker.TryGetTotalLatencyRecord(totalLatency, false, out record))
			{
				LatencyFormatter.AppendLatencyRecord(stringBuilder, record, null);
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x00041748 File Offset: 0x0003F948
		public static string FormatLatencyHeader(LatencyInProgressParser parser, DateTime localArrivalTime, LatencyComponent previousHopDeliveryComponent, LatencyComponent previousHopSubComponent, TimeSpan previousHopSubComponentLatency)
		{
			TimeSpan timeSpan;
			if (localArrivalTime < parser.PreDeliveryTime)
			{
				timeSpan = TimeSpan.Zero;
			}
			else
			{
				timeSpan = localArrivalTime - parser.PreDeliveryTime;
			}
			StringBuilder stringBuilder = new StringBuilder();
			LatencyFormatter.AppendServerFqdn(stringBuilder, true, parser.StringToParse, parser.ServerFqdnStartIndex, parser.ServerFqdnLength);
			bool flag = false;
			LatencyRecord record;
			if ((ushort)parser.TotalSeconds != 65535 && LatencyTracker.TryGetTotalLatencyRecord(timeSpan + TimeSpan.FromSeconds((double)parser.TotalSeconds), false, out record))
			{
				LatencyFormatter.AppendLatencyRecord(stringBuilder, record, parser.PreviousHopProcess);
				flag = true;
			}
			if (parser.ComponentsStartIndex != -1)
			{
				if (flag)
				{
					stringBuilder.Append('|');
				}
				stringBuilder.Append(parser.StringToParse, parser.ComponentsStartIndex, parser.ComponentsLength);
				flag = true;
			}
			LatencyRecord record2;
			if (previousHopSubComponent != LatencyComponent.None && LatencyTracker.TryGetComponentLatencyRecord(previousHopSubComponent, previousHopSubComponentLatency, out record2))
			{
				if (flag)
				{
					stringBuilder.Append('|');
				}
				LatencyFormatter.AppendLatencyRecord(stringBuilder, record2, null);
				flag = true;
			}
			LatencyRecord record3;
			if (LatencyTracker.TryGetComponentLatencyRecord(previousHopDeliveryComponent, timeSpan, out record3))
			{
				if (flag)
				{
					stringBuilder.Append('|');
				}
				LatencyFormatter.AppendLatencyRecord(stringBuilder, record3, null);
				flag = true;
			}
			if (parser.PendingStartIndex != -1)
			{
				if (flag)
				{
					stringBuilder.Append(';');
				}
				stringBuilder.Append(parser.StringToParse, parser.PendingStartIndex, parser.PreDeliveryTimeSeparatorIndex - parser.PendingStartIndex);
				flag = true;
			}
			if (!flag)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0004188C File Offset: 0x0003FA8C
		public static bool IsSeparator(char c)
		{
			return c == ';' || c == ':' || c == '=' || c == '|' || c == '(';
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x000418AC File Offset: 0x0003FAAC
		public static XElement GetDiagnosticInfo(LatencyTracker latencyTracker)
		{
			XElement xelement = new XElement("Latency");
			foreach (LatencyRecord latencyRecord in latencyTracker.GetCompletedRecords())
			{
				xelement.Add(new XElement("item", new object[]
				{
					new XAttribute("name", latencyRecord.ComponentShortName),
					latencyRecord.Latency
				}));
			}
			return xelement;
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x00041948 File Offset: 0x0003FB48
		public string FormatAndUpdatePerfCounters()
		{
			return this.FormatAndUpdatePerfCounters(LatencyTracker.TreeLatencyTrackingEnabled);
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x00041958 File Offset: 0x0003FB58
		public string FormatAndUpdatePerfCounters(bool useTreeFormat)
		{
			if (useTreeFormat && !this.localTracker.SupportsTreeFormatting)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			this.ComputeEndToEndLatencies();
			string text = this.FormatOrgArrivalTime();
			DateTime d = this.TotalFromOrgArrivalTime ? this.orgArrivalTime : this.localArrivalTime;
			TimeSpan timeSpan = LatencyTracker.TimeProvider() - d;
			if (this.UpdateTotalPerfCounter)
			{
				LatencyTracker.UpdateTotalPerfCounter((this.externalDeliveryEnqueueTime != DateTime.MinValue) ? (this.externalDeliveryEnqueueTime - d) : timeSpan, this.priority);
			}
			LatencyRecord record;
			bool flag = LatencyTracker.TryGetTotalLatencyRecord(timeSpan, this.IgnoreTotalThreshold, out record);
			bool flag2 = false;
			bool flag3 = false;
			if (this.localTracker != null)
			{
				flag2 = this.localTracker.HasCompletedComponents;
				flag3 = this.localTracker.HasPendingComponents;
			}
			bool flag4 = false;
			if (text != null)
			{
				stringBuilder.Append(text);
				flag4 = true;
			}
			if (this.previousHops != null)
			{
				if (flag4)
				{
					stringBuilder.Append(';');
					flag4 = false;
				}
				this.AppendPreviousHops(stringBuilder);
			}
			if (this.ReportEndToEnd)
			{
				TimeSpan t = (this.externalSendLatency != TimeSpan.MinValue) ? this.externalSendLatency : this.endToEndLatency;
				if (t != TimeSpan.MinValue)
				{
					LatencyTracker.UpdateTotalEndToEndLatency(this.priority, (long)t.TotalSeconds);
				}
			}
			if (flag || flag2 || flag3)
			{
				if (flag4)
				{
					stringBuilder.Append(';');
				}
				else if (this.previousHops != null)
				{
					stringBuilder.Append(';');
				}
				LatencyFormatter.AppendServerFqdn(stringBuilder, this.ReportEndToEnd, this.localServerFqdn, 0, this.localServerFqdn.Length);
			}
			if (flag)
			{
				LatencyFormatter.AppendLatencyRecord(stringBuilder, record, LatencyTracker.ProcessShortName);
			}
			if (this.localTracker != null && flag2 && this.localTracker.AggregatedUnderThresholdTicks > 0L)
			{
				flag |= LatencyFormatter.AppendComponent(stringBuilder, flag, LatencyComponent.UnderThreshold, TimeSpan.FromTicks(this.localTracker.AggregatedUnderThresholdTicks));
			}
			if (this.localTracker != null)
			{
				this.localTracker.AppendLatencyString(stringBuilder, useTreeFormat, flag, this.EnableHeaderFolding);
			}
			if (this.IncludeCurrentTime && (flag || flag2 || flag3))
			{
				stringBuilder.Append(';');
				stringBuilder.Append(LatencyFormatter.FormatDateTime(LatencyTracker.TimeProvider()));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00041B8B File Offset: 0x0003FD8B
		public string FormatOrgArrivalTime()
		{
			if (this.orgArrivalTime == DateTime.MinValue || !this.IncludeOrgArrivalTime)
			{
				return null;
			}
			return LatencyFormatter.FormatDateTime(this.orgArrivalTime);
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x00041BB4 File Offset: 0x0003FDB4
		private void ComputeEndToEndLatencies()
		{
			DateTime dateTime = LatencyTracker.TimeProvider();
			DateTime dateTime2 = (this.orgArrivalTime == DateTime.MinValue) ? DateTime.MinValue : this.orgArrivalTime.ToUniversalTime();
			DateTime dateTime3 = (this.externalDeliveryEnqueueTime == DateTime.MinValue) ? DateTime.MinValue : this.externalDeliveryEnqueueTime.ToUniversalTime();
			if (dateTime2 != DateTime.MinValue && dateTime2 < dateTime)
			{
				this.endToEndLatency = dateTime - dateTime2;
				if (dateTime3 != DateTime.MinValue && dateTime2 < dateTime3 && dateTime3 < dateTime)
				{
					this.externalSendLatency = dateTime3 - dateTime2;
				}
			}
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x00041C68 File Offset: 0x0003FE68
		private static void AppendServerFqdn(StringBuilder builder, bool endToEnd, string s, int startIndex, int count)
		{
			builder.Append(endToEnd ? "SRV" : "LSRV");
			builder.Append('=');
			builder.Append(s, startIndex, count);
			builder.Append(':');
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x00041CA0 File Offset: 0x0003FEA0
		internal static void AppendLatencyRecord(StringBuilder builder, LatencyRecord record, string suffix = null)
		{
			builder.Append(record.ComponentShortName);
			if (!string.IsNullOrEmpty(suffix))
			{
				builder.Append('-');
				builder.Append(suffix);
			}
			builder.Append('=');
			builder.Append(record.Latency.TotalSeconds.ToString("F3", CultureInfo.InvariantCulture));
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00041D08 File Offset: 0x0003FF08
		internal static void AppendPendingLatencyRecord(StringBuilder builder, PendingLatencyRecord record, TimeSpan latency)
		{
			builder.Append(record.ComponentShortName);
			builder.Append('-');
			builder.Append("PEN");
			builder.Append('=');
			builder.Append(latency.TotalSeconds.ToString("F3", CultureInfo.InvariantCulture));
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x00041D61 File Offset: 0x0003FF61
		internal static int AddFolding(StringBuilder builder, int lastFoldingPoint, bool enableHeaderFoldering)
		{
			if (enableHeaderFoldering && builder.Length - lastFoldingPoint > 80)
			{
				builder.Append(' ');
				lastFoldingPoint = builder.Length;
			}
			return lastFoldingPoint;
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x00041D84 File Offset: 0x0003FF84
		private static string FormatDateTime(DateTime dt)
		{
			return dt.ToUniversalTime().ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffZ", DateTimeFormatInfo.InvariantInfo);
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x00041DAC File Offset: 0x0003FFAC
		private static bool AppendComponent(StringBuilder builder, bool needsRecordSeparator, LatencyComponent componentId, TimeSpan latencyTimeSpan)
		{
			LatencyRecord record;
			if (!LatencyTracker.TryGetComponentLatencyRecord(componentId, latencyTimeSpan, out record))
			{
				return false;
			}
			if (needsRecordSeparator)
			{
				builder.Append('|');
			}
			LatencyFormatter.AppendLatencyRecord(builder, record, null);
			return true;
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00041DDC File Offset: 0x0003FFDC
		private void AppendPreviousHops(StringBuilder builder)
		{
			for (int i = 0; i < this.previousHops.Count; i++)
			{
				if (i > 0)
				{
					builder.Append(';');
				}
				builder.Append(this.previousHops[i]);
			}
		}

		// Token: 0x04000809 RID: 2057
		public const char AgentNameSeparator = '-';

		// Token: 0x0400080A RID: 2058
		public const char ComponentSuffixSeparator = '-';

		// Token: 0x0400080B RID: 2059
		public const char OrgArrivalTimeSeparator = ';';

		// Token: 0x0400080C RID: 2060
		public const char CurrentTimeSeparator = ';';

		// Token: 0x0400080D RID: 2061
		public const char ServerSeparator = ';';

		// Token: 0x0400080E RID: 2062
		public const char ServerFqdnSeparator = ':';

		// Token: 0x0400080F RID: 2063
		public const char ComponentValueSeparator = '=';

		// Token: 0x04000810 RID: 2064
		public const char LatencyRecordSeparator = '|';

		// Token: 0x04000811 RID: 2065
		public const char ChildBegin = '(';

		// Token: 0x04000812 RID: 2066
		public const char ChildEnd = ')';

		// Token: 0x04000813 RID: 2067
		public const char PendingPartSeparator = ';';

		// Token: 0x04000814 RID: 2068
		public const string EndToEndServerPrefix = "SRV";

		// Token: 0x04000815 RID: 2069
		public const string LocalServerPrefix = "LSRV";

		// Token: 0x04000816 RID: 2070
		public const string PendingSuffix = "PEN";

		// Token: 0x04000817 RID: 2071
		public const string IncompleteSuffix = "INC";

		// Token: 0x04000818 RID: 2072
		public const string FormatSpecifier = "F3";

		// Token: 0x04000819 RID: 2073
		public const int HeaderFoldingThreshold = 80;

		// Token: 0x0400081A RID: 2074
		private readonly DateTime externalDeliveryEnqueueTime = DateTime.MinValue;

		// Token: 0x0400081B RID: 2075
		private LatencyTracker localTracker;

		// Token: 0x0400081C RID: 2076
		private string localServerFqdn;

		// Token: 0x0400081D RID: 2077
		private DateTime localArrivalTime;

		// Token: 0x0400081E RID: 2078
		private DateTime orgArrivalTime;

		// Token: 0x0400081F RID: 2079
		private TimeSpan endToEndLatency = TimeSpan.MinValue;

		// Token: 0x04000820 RID: 2080
		private TimeSpan externalSendLatency = TimeSpan.MinValue;

		// Token: 0x04000821 RID: 2081
		private IList<string> previousHops;

		// Token: 0x04000822 RID: 2082
		private LatencyFormatter.FormatFlags flags;

		// Token: 0x04000823 RID: 2083
		private DeliveryPriority priority;

		// Token: 0x02000171 RID: 369
		[Flags]
		private enum FormatFlags
		{
			// Token: 0x04000825 RID: 2085
			None = 0,
			// Token: 0x04000826 RID: 2086
			EnableHeaderFolding = 1,
			// Token: 0x04000827 RID: 2087
			IgnoreTotalThreshold = 2,
			// Token: 0x04000828 RID: 2088
			IncludeCurrentTime = 4,
			// Token: 0x04000829 RID: 2089
			IncludeOrgArrivalTime = 8,
			// Token: 0x0400082A RID: 2090
			ReportEndToEnd = 16,
			// Token: 0x0400082B RID: 2091
			UpdateTotalPerfCounter = 32,
			// Token: 0x0400082C RID: 2092
			CalculateTotalFromOrgArrivalTime = 64
		}
	}
}
