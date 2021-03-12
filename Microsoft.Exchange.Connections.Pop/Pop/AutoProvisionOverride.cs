using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;

namespace Microsoft.Exchange.Connections.Pop
{
	// Token: 0x02000021 RID: 33
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class AutoProvisionOverride
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00006658 File Offset: 0x00004858
		internal static bool TryGetOverrides(string domain, ConnectionType type, out string[] overrideHosts, out bool isTrustedForSendAs)
		{
			isTrustedForSendAs = false;
			AutoProvisionOverride.LoadOverridesIfNecessary();
			bool result;
			lock (AutoProvisionOverride.overrideSyncObj)
			{
				Dictionary<string, List<string>> dictionary;
				switch (type)
				{
				case ConnectionType.Imap:
					dictionary = AutoProvisionOverride.imapOverrides;
					break;
				case ConnectionType.Pop:
					dictionary = AutoProvisionOverride.popOverrides;
					break;
				default:
					overrideHosts = null;
					return false;
				}
				if (dictionary.ContainsKey(domain))
				{
					overrideHosts = dictionary[domain].ToArray();
					isTrustedForSendAs = AutoProvisionOverride.sendAsTrustedOverrideDomains.Contains(domain);
					result = true;
				}
				else
				{
					overrideHosts = null;
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000066F4 File Offset: 0x000048F4
		private static void LoadOverridesIfNecessary()
		{
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000066F8 File Offset: 0x000048F8
		private static void ParseOverride(XmlTextReader reader)
		{
			string attribute = reader.GetAttribute(AutoProvisionOverride.Domain);
			string attribute2 = reader.GetAttribute(AutoProvisionOverride.TrustedBySendAs);
			if (attribute2 != null && attribute2.Equals("true"))
			{
				AutoProvisionOverride.sendAsTrustedOverrideDomains.Add(attribute);
			}
			while (reader.Read())
			{
				if (reader.IsStartElement(AutoProvisionOverride.POP))
				{
					AutoProvisionOverride.ParsePOP(reader, attribute);
				}
				else if (reader.IsStartElement(AutoProvisionOverride.IMAP))
				{
					AutoProvisionOverride.ParseIMAP(reader, attribute);
				}
				if (AutoProvisionOverride.IsEndElement(reader, AutoProvisionOverride.Override))
				{
					return;
				}
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00006778 File Offset: 0x00004978
		private static void ParsePOP(XmlTextReader reader, string domainName)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Text)
				{
					AutoProvisionOverride.AddOverride(domainName, AutoProvisionOverride.popOverrides, reader.Value);
				}
				if (AutoProvisionOverride.IsEndElement(reader, AutoProvisionOverride.POP))
				{
					return;
				}
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000067AA File Offset: 0x000049AA
		private static void ParseIMAP(XmlTextReader reader, string domainName)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Text)
				{
					AutoProvisionOverride.AddOverride(domainName, AutoProvisionOverride.imapOverrides, reader.Value);
				}
				if (AutoProvisionOverride.IsEndElement(reader, AutoProvisionOverride.IMAP))
				{
					return;
				}
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000067DC File Offset: 0x000049DC
		private static bool IsEndElement(XmlTextReader reader, string elementName)
		{
			return reader.NodeType == XmlNodeType.EndElement && reader.Name == elementName;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000067F8 File Offset: 0x000049F8
		private static void AddOverride(string domainName, Dictionary<string, List<string>> domainToHostMap, string hostName)
		{
			List<string> list;
			if (domainToHostMap.TryGetValue(domainName, out list))
			{
				list.Add(hostName);
				return;
			}
			list = new List<string>();
			list.Add(hostName);
			domainToHostMap[domainName] = list;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000682D File Offset: 0x00004A2D
		private static void ClearCacheData()
		{
			AutoProvisionOverride.popOverrides.Clear();
			AutoProvisionOverride.imapOverrides.Clear();
			AutoProvisionOverride.sendAsTrustedOverrideDomains.Clear();
		}

		// Token: 0x040000CC RID: 204
		private const string OverrideXml = "AutoProvisionOverride.xml";

		// Token: 0x040000CD RID: 205
		internal static readonly string Override = "Override";

		// Token: 0x040000CE RID: 206
		internal static readonly string Domain = "Domain";

		// Token: 0x040000CF RID: 207
		internal static readonly string IMAP = "IMAP";

		// Token: 0x040000D0 RID: 208
		internal static readonly string POP = "POP";

		// Token: 0x040000D1 RID: 209
		internal static readonly string TrustedBySendAs = "TrustedBySendAs";

		// Token: 0x040000D2 RID: 210
		internal static readonly string AutoProvisionOverrides = "AutoProvisionOverrides";

		// Token: 0x040000D3 RID: 211
		private static readonly Trace diag = ExTraceGlobals.SubscriptionTaskTracer;

		// Token: 0x040000D4 RID: 212
		private static readonly object overrideSyncObj = new object();

		// Token: 0x040000D5 RID: 213
		private static readonly string AutoProvisionOverrideXML = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "AutoProvisionOverride.xml");

		// Token: 0x040000D6 RID: 214
		private static Dictionary<string, List<string>> popOverrides = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040000D7 RID: 215
		private static Dictionary<string, List<string>> imapOverrides = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040000D8 RID: 216
		private static HashSet<string> sendAsTrustedOverrideDomains = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040000D9 RID: 217
		private static DateTime overrideRefreshTime = DateTime.UtcNow;
	}
}
