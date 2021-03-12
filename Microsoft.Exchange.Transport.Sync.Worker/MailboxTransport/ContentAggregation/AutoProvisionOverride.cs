using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x0200003E RID: 62
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class AutoProvisionOverride
	{
		// Token: 0x06000315 RID: 789 RVA: 0x0000EE04 File Offset: 0x0000D004
		internal static bool TryGetOverrides(string domain, AggregationSubscriptionType type, out string[] overrideHosts, out bool isTrustedForSendAs)
		{
			isTrustedForSendAs = false;
			AutoProvisionOverride.LoadOverridesIfNecessary();
			bool result;
			lock (AutoProvisionOverride.overrideSyncObj)
			{
				Dictionary<string, List<string>> dictionary;
				if (type != AggregationSubscriptionType.Pop)
				{
					if (type != AggregationSubscriptionType.IMAP)
					{
						overrideHosts = null;
						return false;
					}
					dictionary = AutoProvisionOverride.imapOverrides;
				}
				else
				{
					dictionary = AutoProvisionOverride.popOverrides;
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

		// Token: 0x06000316 RID: 790 RVA: 0x0000EE9C File Offset: 0x0000D09C
		private static void LoadOverridesIfNecessary()
		{
			if (DateTime.UtcNow >= AutoProvisionOverride.overrideRefreshTime)
			{
				lock (AutoProvisionOverride.overrideSyncObj)
				{
					if (DateTime.UtcNow >= AutoProvisionOverride.overrideRefreshTime)
					{
						AutoProvisionOverride.ClearCacheData();
						AutoProvisionOverride.overrideRefreshTime = DateTime.UtcNow.AddMinutes(2.0);
						if (File.Exists(AutoProvisionOverride.AutoProvisionOverrideXML))
						{
							try
							{
								using (XmlTextReader xmlTextReader = SafeXmlFactory.CreateSafeXmlTextReader(AutoProvisionOverride.AutoProvisionOverrideXML))
								{
									xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;
									xmlTextReader.ReadStartElement(AutoProvisionOverride.AutoProvisionOverrides);
									while (!AutoProvisionOverride.IsEndElement(xmlTextReader, AutoProvisionOverride.AutoProvisionOverrides))
									{
										if (xmlTextReader.IsStartElement(AutoProvisionOverride.Override))
										{
											AutoProvisionOverride.ParseOverride(xmlTextReader);
										}
										if (!xmlTextReader.Read())
										{
											break;
										}
									}
								}
							}
							catch (XmlException arg)
							{
								AutoProvisionOverride.ClearCacheData();
								AutoProvisionOverride.diag.TraceError<XmlException>(0L, "AutoProvisionOverrides failed with error: {0}", arg);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000EFB4 File Offset: 0x0000D1B4
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

		// Token: 0x06000318 RID: 792 RVA: 0x0000F034 File Offset: 0x0000D234
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

		// Token: 0x06000319 RID: 793 RVA: 0x0000F066 File Offset: 0x0000D266
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

		// Token: 0x0600031A RID: 794 RVA: 0x0000F098 File Offset: 0x0000D298
		private static bool IsEndElement(XmlTextReader reader, string elementName)
		{
			return reader.NodeType == XmlNodeType.EndElement && reader.Name == elementName;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000F0B4 File Offset: 0x0000D2B4
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

		// Token: 0x0600031C RID: 796 RVA: 0x0000F0E9 File Offset: 0x0000D2E9
		private static void ClearCacheData()
		{
			AutoProvisionOverride.popOverrides.Clear();
			AutoProvisionOverride.imapOverrides.Clear();
			AutoProvisionOverride.sendAsTrustedOverrideDomains.Clear();
		}

		// Token: 0x040001A2 RID: 418
		private const string OverrideXml = "AutoProvisionOverride.xml";

		// Token: 0x040001A3 RID: 419
		internal static readonly string Override = "Override";

		// Token: 0x040001A4 RID: 420
		internal static readonly string Domain = "Domain";

		// Token: 0x040001A5 RID: 421
		internal static readonly string IMAP = "IMAP";

		// Token: 0x040001A6 RID: 422
		internal static readonly string POP = "POP";

		// Token: 0x040001A7 RID: 423
		internal static readonly string TrustedBySendAs = "TrustedBySendAs";

		// Token: 0x040001A8 RID: 424
		internal static readonly string AutoProvisionOverrides = "AutoProvisionOverrides";

		// Token: 0x040001A9 RID: 425
		private static readonly Trace diag = ExTraceGlobals.SubscriptionTaskTracer;

		// Token: 0x040001AA RID: 426
		private static readonly object overrideSyncObj = new object();

		// Token: 0x040001AB RID: 427
		private static readonly string AutoProvisionOverrideXML = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "AutoProvisionOverride.xml");

		// Token: 0x040001AC RID: 428
		private static Dictionary<string, List<string>> popOverrides = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040001AD RID: 429
		private static Dictionary<string, List<string>> imapOverrides = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040001AE RID: 430
		private static HashSet<string> sendAsTrustedOverrideDomains = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040001AF RID: 431
		private static DateTime overrideRefreshTime = DateTime.UtcNow;
	}
}
