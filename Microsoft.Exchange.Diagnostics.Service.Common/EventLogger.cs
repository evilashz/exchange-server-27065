using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.Common
{
	// Token: 0x02000009 RID: 9
	public class EventLogger : ITriggerHandler
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00003FDC File Offset: 0x000021DC
		public EventLogger()
		{
			this.tuples = new Dictionary<string, EventLogger.EventData>();
			this.InitializeTuples();
			bool flag = EventLog.SourceExists("MSExchangeDiagnostics");
			if (flag)
			{
				this.eventLog = new ExEventLog(EventLogger.ComponentGuid, "MSExchangeDiagnostics");
				this.eventLog.SetEventPeriod(3600);
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00004033 File Offset: 0x00002233
		protected Dictionary<string, EventLogger.EventData> Tuples
		{
			get
			{
				return this.tuples;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000403B File Offset: 0x0000223B
		public bool Trigger(string triggerName, params object[] args)
		{
			return this.InternalTrigger(triggerName, args);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00004048 File Offset: 0x00002248
		public bool LogEvent(ExEventLog.EventTuple tuple, params object[] args)
		{
			if (this.eventLog != null)
			{
				for (int i = 0; i < args.Length; i++)
				{
					string text = args[i].ToString();
					if (text.Length > 32766)
					{
						args[i] = text.Substring(0, 32766);
					}
				}
				return this.eventLog.LogEvent(tuple, null, args);
			}
			Logger.LogErrorMessage("Unable to log event, event log source not configured.", new object[0]);
			return false;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000040B0 File Offset: 0x000022B0
		protected virtual bool InternalTrigger(string triggerName, params object[] args)
		{
			EventLogger.EventData eventData = this.tuples[triggerName];
			ExEventLog.EventTuple tuple = eventData.Tuple;
			TriggerHandler.TriggerData triggerData = TriggerHandler.Triggers[triggerName];
			Log.LogMessage(triggerData.Type, triggerData.Format, args);
			return this.LogEvent(tuple, args);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000040F7 File Offset: 0x000022F7
		private static T GetEnumValue<T>(XmlElement element, string name)
		{
			return (T)((object)Enum.Parse(typeof(T), element.SelectSingleNode(name).InnerText));
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000411C File Offset: 0x0000231C
		private static string ConvertSubstitution(Match match)
		{
			int num = int.Parse(match.ToString().Substring(1));
			return string.Format("{{{0}}}", num - 1);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00004150 File Offset: 0x00002350
		private static string FormatString(string format)
		{
			if (format.Contains("%{") || format.Contains("%%"))
			{
				throw new NotImplementedException();
			}
			format = Regex.Replace(format, "{", "{{");
			format = Regex.Replace(format, "}", "}}");
			format = Regex.Replace(format, "%\\d+", new MatchEvaluator(EventLogger.ConvertSubstitution));
			return format;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000041BC File Offset: 0x000023BC
		private void InitializeTuples()
		{
			string[] manifestResourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
			foreach (string text in manifestResourceNames)
			{
				if (text.EndsWith("EventLog.xml"))
				{
					SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
					safeXmlDocument.Load(Assembly.GetExecutingAssembly().GetManifestResourceStream(text));
					Dictionary<string, short> dictionary = new Dictionary<string, short>();
					foreach (object obj in safeXmlDocument.SelectNodes("/root/category"))
					{
						XmlElement xmlElement = (XmlElement)obj;
						string attribute = xmlElement.GetAttribute("name");
						short value = short.Parse(xmlElement.SelectSingleNode("number").InnerText);
						dictionary.Add(attribute, value);
					}
					foreach (object obj2 in safeXmlDocument.SelectNodes("/root/data"))
					{
						XmlElement xmlElement2 = (XmlElement)obj2;
						string attribute2 = xmlElement2.GetAttribute("name");
						uint eventId = (uint)Enum.Parse(typeof(MSExchangeDiagnosticsEventLogConstants.Message), attribute2);
						string innerText = xmlElement2.SelectSingleNode("category").InnerText;
						string innerText2 = xmlElement2.SelectSingleNode("stringvalue").InnerText;
						short categoryId = dictionary[innerText];
						string innerText3 = xmlElement2.SelectSingleNode("eventtype").InnerText;
						string component = string.Empty;
						XmlNode xmlNode = xmlElement2.SelectSingleNode("component");
						if (xmlNode != null)
						{
							component = EventLogger.FormatString(xmlNode.InnerText);
						}
						string tag = string.Empty;
						XmlNode xmlNode2 = xmlElement2.SelectSingleNode("tag");
						if (xmlNode2 != null)
						{
							tag = EventLogger.FormatString(xmlNode2.InnerText);
						}
						string exception = string.Empty;
						XmlNode xmlNode3 = xmlElement2.SelectSingleNode("exception");
						if (xmlNode3 != null)
						{
							exception = EventLogger.FormatString(xmlNode3.InnerText);
						}
						EventLogEntryType enumValue = EventLogger.GetEnumValue<EventLogEntryType>(xmlElement2, "eventtype");
						ExEventLog.EventLevel enumValue2 = EventLogger.GetEnumValue<ExEventLog.EventLevel>(xmlElement2, "level");
						ExEventLog.EventPeriod enumValue3 = EventLogger.GetEnumValue<ExEventLog.EventPeriod>(xmlElement2, "period");
						ExEventLog.EventTuple tuple = new ExEventLog.EventTuple(eventId, categoryId, enumValue, enumValue2, enumValue3);
						this.tuples.Add(attribute2, new EventLogger.EventData(tuple, component, tag, exception));
						string text2 = EventLogger.FormatString(innerText2);
						if (!TriggerHandler.Triggers.ContainsKey(attribute2))
						{
							TriggerHandler.Triggers.Add(attribute2, new TriggerHandler.TriggerData(innerText3, text2));
						}
					}
				}
			}
		}

		// Token: 0x040002C0 RID: 704
		private static readonly Guid ComponentGuid = new Guid("8404612E-E0A3-4367-A498-3FD73AFAC29F");

		// Token: 0x040002C1 RID: 705
		private readonly ExEventLog eventLog;

		// Token: 0x040002C2 RID: 706
		private readonly Dictionary<string, EventLogger.EventData> tuples;

		// Token: 0x0200000A RID: 10
		protected class EventData
		{
			// Token: 0x06000019 RID: 25 RVA: 0x00004481 File Offset: 0x00002681
			public EventData(ExEventLog.EventTuple tuple, string component, string tag, string exception)
			{
				this.Tuple = tuple;
				this.Component = component;
				this.Tag = tag;
				this.Exception = exception;
			}

			// Token: 0x040002C3 RID: 707
			public readonly ExEventLog.EventTuple Tuple;

			// Token: 0x040002C4 RID: 708
			public readonly string Component;

			// Token: 0x040002C5 RID: 709
			public readonly string Tag;

			// Token: 0x040002C6 RID: 710
			public readonly string Exception;
		}
	}
}
