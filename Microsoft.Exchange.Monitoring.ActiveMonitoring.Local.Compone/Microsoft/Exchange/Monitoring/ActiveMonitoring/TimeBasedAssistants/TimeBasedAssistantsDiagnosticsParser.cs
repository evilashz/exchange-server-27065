using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TimeBasedAssistants
{
	// Token: 0x02000526 RID: 1318
	internal static class TimeBasedAssistantsDiagnosticsParser
	{
		// Token: 0x06002063 RID: 8291 RVA: 0x000C6454 File Offset: 0x000C4654
		public static Dictionary<AssistantInfo, Dictionary<MailboxDatabase, WindowJob[]>> ParseDiagnosticsString(string tbaDiagnostics)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("tbaDiagnostics", tbaDiagnostics);
			Dictionary<AssistantInfo, Dictionary<MailboxDatabase, WindowJob[]>> result;
			try
			{
				XElement node = XElement.Parse(tbaDiagnostics);
				IEnumerable<XElement> enumerable = node.XPathSelectElements("/Components/MailboxAssistants/MailboxAssistant");
				IEnumerable<XElement> enumerable2 = (enumerable as IList<XElement>) ?? enumerable.ToList<XElement>();
				result = (enumerable2.Any<XElement>() ? TimeBasedAssistantsDiagnosticsParser.ParseAssistantsCollection(enumerable2) : new Dictionary<AssistantInfo, Dictionary<MailboxDatabase, WindowJob[]>>());
			}
			catch (XmlException ex)
			{
				throw new ParseDiagnosticsStringException(string.Format("Unable to parse Get-EDI output. Error is: '{0}'. Diagnostics string is: '{1}'", ex.Message, tbaDiagnostics));
			}
			return result;
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x000C64D4 File Offset: 0x000C46D4
		private static Dictionary<AssistantInfo, Dictionary<MailboxDatabase, WindowJob[]>> ParseAssistantsCollection(IEnumerable<XElement> assistants)
		{
			ArgumentValidator.ThrowIfNull("assistants", assistants);
			Dictionary<AssistantInfo, Dictionary<MailboxDatabase, WindowJob[]>> dictionary = new Dictionary<AssistantInfo, Dictionary<MailboxDatabase, WindowJob[]>>();
			foreach (XElement xelement in assistants)
			{
				XAttribute xattribute = xelement.Attribute("Type");
				if (xattribute == null || string.IsNullOrWhiteSpace(xattribute.Value))
				{
					throw new ParseDiagnosticsStringException(string.Format("Unable to parse Get-EDI output. Cannot read assistant name.", new object[0]));
				}
				XElement xelement2 = xelement.XPathSelectElement("./WorkcycleInterval");
				if (xelement2 == null || string.IsNullOrWhiteSpace(xelement2.Value))
				{
					throw new ParseDiagnosticsStringException(string.Format("Unable to parse Get-EDI output. Cannot read workcycle length for assistant '{0}'.", xattribute.Value));
				}
				XElement xelement3 = xelement.XPathSelectElement("./WorkcycleCheckpointInterval");
				if (xelement3 == null || string.IsNullOrWhiteSpace(xelement3.Value))
				{
					throw new ParseDiagnosticsStringException(string.Format("Unable to parse Get-EDI output. Cannot read workcycle checkpoint length for assistant '{0}'.", xattribute.Value));
				}
				TimeSpan workcycleLength = TimeSpan.Parse(xelement2.Value);
				TimeSpan workcycleCheckpointLength = TimeSpan.Parse(xelement3.Value);
				AssistantInfo key = new AssistantInfo
				{
					AssistantName = xattribute.Value,
					WorkcycleLength = workcycleLength,
					WorkcycleCheckpointLength = workcycleCheckpointLength
				};
				IEnumerable<XElement> enumerable = xelement.XPathSelectElements("./MailboxDatabases/MailboxDatabase");
				IEnumerable<XElement> databases = (enumerable as IList<XElement>) ?? enumerable.ToList<XElement>();
				dictionary.Add(key, TimeBasedAssistantsDiagnosticsParser.ParseDatabaseCollection(databases));
			}
			return dictionary;
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x000C6654 File Offset: 0x000C4854
		private static Dictionary<MailboxDatabase, WindowJob[]> ParseDatabaseCollection(IEnumerable<XElement> databases)
		{
			ArgumentValidator.ThrowIfNull("databases", databases);
			Dictionary<MailboxDatabase, WindowJob[]> dictionary = new Dictionary<MailboxDatabase, WindowJob[]>();
			foreach (XElement xelement in databases)
			{
				DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(MailboxDatabase));
				MailboxDatabase mailboxDatabase = dataContractSerializer.ReadObject(xelement.CreateReader()) as MailboxDatabase;
				if (mailboxDatabase == null)
				{
					throw new ParseDiagnosticsStringException("Unable to parse Get-EDI output. Cannot read mailbox database information.");
				}
				IEnumerable<XElement> enumerable = xelement.XPathSelectElements("./WindowJobs/WindowJob");
				IEnumerable<XElement> workcycles = (enumerable as IList<XElement>) ?? enumerable.ToList<XElement>();
				dictionary.Add(mailboxDatabase, TimeBasedAssistantsDiagnosticsParser.ParseWindowJobCollection(workcycles));
			}
			return dictionary;
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x000C670C File Offset: 0x000C490C
		private static WindowJob[] ParseWindowJobCollection(IEnumerable<XElement> workcycles)
		{
			ArgumentValidator.ThrowIfNull("workcycles", workcycles);
			List<WindowJob> list = new List<WindowJob>();
			foreach (XElement xelement in workcycles)
			{
				DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(WindowJob));
				WindowJob windowJob = dataContractSerializer.ReadObject(xelement.CreateReader()) as WindowJob;
				if (windowJob == null)
				{
					throw new ParseDiagnosticsStringException("Unable to parse Get-EDI output. Cannot read window job history information.");
				}
				list.Add(windowJob);
			}
			return list.ToArray();
		}

		// Token: 0x040017D2 RID: 6098
		private const string MailboxAssistantsXPath = "/Components/MailboxAssistants/MailboxAssistant";

		// Token: 0x040017D3 RID: 6099
		private const string WorkcycleIntervalXPath = "./WorkcycleInterval";

		// Token: 0x040017D4 RID: 6100
		private const string WorkcycleCheckpointIntervalXPath = "./WorkcycleCheckpointInterval";

		// Token: 0x040017D5 RID: 6101
		private const string MailboxDatabasesXPath = "./MailboxDatabases/MailboxDatabase";

		// Token: 0x040017D6 RID: 6102
		private const string WorkcyclesXPath = "./WindowJobs/WindowJob";

		// Token: 0x040017D7 RID: 6103
		private const string ParseErrorMessage = "Unable to parse Get-EDI output. ";
	}
}
