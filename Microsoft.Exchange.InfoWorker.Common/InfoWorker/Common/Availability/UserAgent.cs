using System;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Availability;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000113 RID: 275
	internal class UserAgent
	{
		// Token: 0x0600075F RID: 1887 RVA: 0x0001F93C File Offset: 0x0001DB3C
		internal UserAgent(string category, string type, string source, string protocol)
		{
			if (category != null)
			{
				this.category = category;
			}
			if (type != null)
			{
				this.type = type;
			}
			if (source != null)
			{
				this.source = source;
			}
			if (protocol != null)
			{
				this.protocol = protocol;
			}
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0001F9A8 File Offset: 0x0001DBA8
		internal static UserAgent Parse(string userAgentString)
		{
			string[] array = userAgentString.Split(UserAgent.Delimiters, 5, StringSplitOptions.None);
			if (array.Length < 4)
			{
				UserAgent.SecurityTracer.TraceDebug<string, int, object>(0L, "{0}: User agent string {1} has {2} parts. Expected at least 4.", userAgentString, array.Length, TraceContext.Get());
				return null;
			}
			return new UserAgent(array[0], array[1], array[2], array[3]);
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x0001F9F6 File Offset: 0x0001DBF6
		internal string Category
		{
			get
			{
				return this.category;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x0001F9FE File Offset: 0x0001DBFE
		internal string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x0001FA06 File Offset: 0x0001DC06
		internal string Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x0001FA0E File Offset: 0x0001DC0E
		internal string Protocol
		{
			get
			{
				return this.protocol;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x0001FA16 File Offset: 0x0001DC16
		internal string Version
		{
			get
			{
				return UserAgent.VersionInfo.FileVersion;
			}
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001FA24 File Offset: 0x0001DC24
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.category,
				"/",
				this.type,
				"/",
				this.source,
				"/",
				this.protocol,
				"/",
				this.Version
			});
		}

		// Token: 0x04000472 RID: 1138
		private const int MaximumComponents = 5;

		// Token: 0x04000473 RID: 1139
		internal const string ProxyCategory = "ASProxy";

		// Token: 0x04000474 RID: 1140
		internal const string AutoDiscoverCategory = "ASAutoDiscover";

		// Token: 0x04000475 RID: 1141
		internal const string CrossSiteType = "CrossSite";

		// Token: 0x04000476 RID: 1142
		internal const string CrossForestType = "CrossForest";

		// Token: 0x04000477 RID: 1143
		internal const string ScpSource = "Directory";

		// Token: 0x04000478 RID: 1144
		internal const string DnsSource = "EmailDomain";

		// Token: 0x04000479 RID: 1145
		internal const string ASAutoDiscoverUserAgentPrefix = "ASAutoDiscover/CrossForest";

		// Token: 0x0400047A RID: 1146
		private static readonly FileVersionInfo VersionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);

		// Token: 0x0400047B RID: 1147
		private static readonly Microsoft.Exchange.Diagnostics.Trace SecurityTracer = ExTraceGlobals.SecurityTracer;

		// Token: 0x0400047C RID: 1148
		private readonly string category = string.Empty;

		// Token: 0x0400047D RID: 1149
		private readonly string type = string.Empty;

		// Token: 0x0400047E RID: 1150
		private readonly string source = string.Empty;

		// Token: 0x0400047F RID: 1151
		private readonly string protocol = string.Empty;

		// Token: 0x04000480 RID: 1152
		internal static readonly char[] Delimiters = new char[]
		{
			'/'
		};
	}
}
