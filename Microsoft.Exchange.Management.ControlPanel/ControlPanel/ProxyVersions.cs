using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200039F RID: 927
	internal sealed class ProxyVersions
	{
		// Token: 0x06003115 RID: 12565 RVA: 0x00095E20 File Offset: 0x00094020
		public ProxyVersions(string rootDirectory)
		{
			string[] directories = Directory.GetDirectories(rootDirectory);
			IEnumerable<Version> collection = from folderPath in directories
			let folderName = Path.GetFileName(folderPath)
			where char.IsDigit(folderName[0])
			select new Version(folderName);
			this.validProxyVersions = new HashSet<Version>(collection);
			this.MaxVersion = new Version(((AssemblyFileVersionAttribute)Attribute.GetCustomAttribute(typeof(ProxyVersions).Assembly, typeof(AssemblyFileVersionAttribute))).Version);
			this.MinVersion = new Version(14, 0, 634, 0);
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x00095EF5 File Offset: 0x000940F5
		public bool Contains(Version version)
		{
			return this.validProxyVersions.Contains(version);
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x00095F04 File Offset: 0x00094104
		public bool IsCompatible(Version internalApplicationVersion, TextWriter decisionLogWriter)
		{
			if (OutboundProxySession.Factory.ProxyToLocalHost)
			{
				return true;
			}
			DecisionLogger decisionLogger = new DecisionLogger(decisionLogWriter)
			{
				{
					internalApplicationVersion <= this.MaxVersion,
					Strings.ProxyServiceConditionGreaterVersion(internalApplicationVersion, this.MaxVersion)
				},
				{
					internalApplicationVersion >= this.MinVersion,
					Strings.ProxyServiceConditionLesserVersion(internalApplicationVersion, this.MinVersion)
				},
				{
					this.Contains(internalApplicationVersion),
					Strings.ProxyServiceConditionInstalledVersion(internalApplicationVersion)
				}
			};
			return decisionLogger.Decision;
		}

		// Token: 0x040023BB RID: 9147
		private readonly ICollection<Version> validProxyVersions;

		// Token: 0x040023BC RID: 9148
		public readonly Version MinVersion;

		// Token: 0x040023BD RID: 9149
		public readonly Version MaxVersion;
	}
}
