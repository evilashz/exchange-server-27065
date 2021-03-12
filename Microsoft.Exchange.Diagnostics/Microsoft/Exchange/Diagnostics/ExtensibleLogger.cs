using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000126 RID: 294
	internal class ExtensibleLogger : DisposeTrackableBase, IExtensibleLogger, IWorkloadLogger
	{
		// Token: 0x06000877 RID: 2167 RVA: 0x00021940 File Offset: 0x0001FB40
		public ExtensibleLogger(ILogConfiguration configuration) : this(configuration, LogRowFormatter.DefaultEscapeLineBreaks)
		{
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00021950 File Offset: 0x0001FB50
		public ExtensibleLogger(ILogConfiguration configuration, bool escapeLineBreaks)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			this.configuration = configuration;
			this.escapeLineBreaks = escapeLineBreaks;
			try
			{
				this.buildNumber = ExchangeSetupContext.InstalledVersion.ToString();
			}
			catch (SetupVersionInformationCorruptException)
			{
				this.buildNumber = string.Empty;
			}
			string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
			this.logSchema = new LogSchema("Microsoft Exchange Server", version, this.configuration.LogType, ExtensibleLogger.Fields);
			this.log = new Log(this.configuration.LogPrefix, new LogHeaderFormatter(this.logSchema), this.configuration.LogComponent);
			this.log.Configure(this.configuration.LogPath, this.configuration.MaxLogAge, this.configuration.MaxLogDirectorySizeInBytes, this.configuration.MaxLogFileSizeInBytes);
			if (this.configuration.IsActivityEventHandler)
			{
				ActivityContext.OnActivityEvent += this.OnActivityContextEvent;
			}
			ActivityContext.RegisterMetadata(typeof(ExtensibleLoggerMetadata));
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x00021A78 File Offset: 0x0001FC78
		protected ILogConfiguration Configuration
		{
			get
			{
				return this.configuration;
			}
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00021A80 File Offset: 0x0001FC80
		public static string FormatPIIValue(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			return "<PII>" + value + "</PII>";
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00021AA0 File Offset: 0x0001FCA0
		public void LogEvent(ILogEvent logEvent)
		{
			if (logEvent == null)
			{
				throw new ArgumentNullException("logEvent");
			}
			if (!this.configuration.IsLoggingEnabled)
			{
				return;
			}
			this.LogRow(logEvent.EventId, this.GetEventData(logEvent));
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00021AD4 File Offset: 0x0001FCD4
		public void LogEvent(IEnumerable<ILogEvent> logEvents)
		{
			if (logEvents == null)
			{
				throw new ArgumentNullException("logEvents");
			}
			if (!this.configuration.IsLoggingEnabled)
			{
				return;
			}
			List<LogRowFormatter> list = new List<LogRowFormatter>(64);
			foreach (ILogEvent logEvent in logEvents)
			{
				list.Add(this.CreateRow(logEvent.EventId, this.GetEventData(logEvent)));
			}
			this.log.Append(list, 0);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00021B60 File Offset: 0x0001FD60
		public void LogActivityEvent(IActivityScope activityScope, ActivityEventType eventType)
		{
			if (activityScope == null)
			{
				throw new ArgumentNullException("activityScope");
			}
			if (!this.configuration.IsLoggingEnabled || !this.IsInterestingEvent(activityScope, eventType))
			{
				return;
			}
			ILogEvent logEvent = activityScope.UserState as ILogEvent;
			ICollection<KeyValuePair<string, object>> collection;
			string text;
			if (logEvent != null)
			{
				collection = this.GetEventData(logEvent);
				text = logEvent.EventId;
			}
			else
			{
				collection = new List<KeyValuePair<string, object>>(0);
				if (activityScope.ActivityType == ActivityType.Global)
				{
					text = "GlobalActivity";
				}
				else
				{
					text = activityScope.GetProperty(ExtensibleLoggerMetadata.EventId);
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				text = "<null>";
			}
			ICollection<KeyValuePair<string, object>> componentSpecificData = this.GetComponentSpecificData(activityScope, text);
			List<KeyValuePair<string, object>> list = this.FormatWlmActivity(activityScope);
			int capacity = collection.Count + componentSpecificData.Count + list.Count + 2;
			List<KeyValuePair<string, object>> list2 = new List<KeyValuePair<string, object>>(capacity);
			list2.AddRange(collection);
			list2.AddRange(componentSpecificData);
			list2.Add(new KeyValuePair<string, object>("Bld", this.buildNumber));
			list2.Add(new KeyValuePair<string, object>("ActID", activityScope.ActivityId));
			list2.AddRange(list);
			this.LogRow(text, list2);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00021C6F File Offset: 0x0001FE6F
		protected static void CopyPIIProperty(IActivityScope source, Dictionary<string, object> target, Enum sourceKey, string targetKey)
		{
			ExtensibleLogger.CopyProperty(source, target, sourceKey, targetKey, true);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00021C7B File Offset: 0x0001FE7B
		protected static void CopyProperty(IActivityScope source, Dictionary<string, object> target, Enum sourceKey, string targetKey)
		{
			ExtensibleLogger.CopyProperty(source, target, sourceKey, targetKey, false);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00021C88 File Offset: 0x0001FE88
		protected static void CopyProperties(IActivityScope source, Dictionary<string, object> target, IEnumerable<KeyValuePair<Enum, string>> enumToKeyMappings)
		{
			foreach (KeyValuePair<Enum, string> keyValuePair in enumToKeyMappings)
			{
				ExtensibleLogger.CopyProperty(source, target, keyValuePair.Key, keyValuePair.Value, false);
			}
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00021CE0 File Offset: 0x0001FEE0
		protected virtual ICollection<KeyValuePair<string, object>> GetEventData(ILogEvent logEvent)
		{
			return logEvent.GetEventData();
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00021CE8 File Offset: 0x0001FEE8
		protected virtual List<KeyValuePair<string, object>> FormatWlmActivity(IActivityScope activityScope)
		{
			List<KeyValuePair<string, object>> list = null;
			if (activityScope != null)
			{
				list = activityScope.GetFormattableStatistics();
				List<KeyValuePair<string, object>> formattableMetadata = activityScope.GetFormattableMetadata();
				if (formattableMetadata != null)
				{
					foreach (KeyValuePair<string, object> item in formattableMetadata)
					{
						if (item.Key.StartsWith("WLM"))
						{
							list.Add(item);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00021D60 File Offset: 0x0001FF60
		protected virtual ICollection<KeyValuePair<string, object>> GetComponentSpecificData(IActivityScope activityScope, string eventId)
		{
			return new KeyValuePair<string, object>[0];
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00021D68 File Offset: 0x0001FF68
		protected virtual bool IsInterestingEvent(IActivityScope activityScope, ActivityEventType eventType)
		{
			return eventType == ActivityEventType.EndActivity;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00021D6E File Offset: 0x0001FF6E
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ExtensibleLogger>(this);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00021D76 File Offset: 0x0001FF76
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				ActivityContext.OnActivityEvent -= this.OnActivityContextEvent;
				this.log.Flush();
				this.log.Close();
			}
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00021DA4 File Offset: 0x0001FFA4
		private static void CopyProperty(IActivityScope source, Dictionary<string, object> target, Enum sourceKey, string targetKey, bool isPII)
		{
			string property = source.GetProperty(sourceKey);
			if (property == null)
			{
				return;
			}
			if (target.ContainsKey(targetKey))
			{
				throw new ArgumentException(string.Format("targetKey '{0}' is already being used by another property: {1}", targetKey, sourceKey));
			}
			target.Add(targetKey, isPII ? ExtensibleLogger.FormatPIIValue(property) : property);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00021DEC File Offset: 0x0001FFEC
		private LogRowFormatter CreateRow(string eventId, IEnumerable<KeyValuePair<string, object>> eventData)
		{
			if (string.IsNullOrEmpty(eventId))
			{
				throw new ArgumentException("eventId cannot be null or empty");
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.logSchema, this.escapeLineBreaks);
			logRowFormatter[1] = ExtensibleLogger.MachineName;
			logRowFormatter[2] = eventId;
			logRowFormatter[3] = eventData;
			return logRowFormatter;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00021E3C File Offset: 0x0002003C
		private void LogRow(string eventId, IEnumerable<KeyValuePair<string, object>> eventData)
		{
			LogRowFormatter row = this.CreateRow(eventId, eventData);
			this.log.Append(row, 0);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00021E5F File Offset: 0x0002005F
		private void OnActivityContextEvent(object sender, ActivityEventArgs args)
		{
			this.LogActivityEvent((IActivityScope)sender, args.ActivityEventType);
		}

		// Token: 0x040005AC RID: 1452
		internal const string CorrelationIdKey = "CorrelationID";

		// Token: 0x040005AD RID: 1453
		private const string SoftwareName = "Microsoft Exchange Server";

		// Token: 0x040005AE RID: 1454
		private const string ActivityIdKey = "ActID";

		// Token: 0x040005AF RID: 1455
		private const string Build = "Bld";

		// Token: 0x040005B0 RID: 1456
		private static readonly string MachineName = Environment.MachineName;

		// Token: 0x040005B1 RID: 1457
		private static readonly string[] Fields = new string[]
		{
			"TimeStamp",
			"ServerName",
			"EventId",
			"EventData"
		};

		// Token: 0x040005B2 RID: 1458
		private readonly LogSchema logSchema;

		// Token: 0x040005B3 RID: 1459
		private readonly Log log;

		// Token: 0x040005B4 RID: 1460
		private readonly ILogConfiguration configuration;

		// Token: 0x040005B5 RID: 1461
		private readonly string buildNumber;

		// Token: 0x040005B6 RID: 1462
		private readonly bool escapeLineBreaks;

		// Token: 0x02000127 RID: 295
		private enum Field
		{
			// Token: 0x040005B8 RID: 1464
			TimeStamp,
			// Token: 0x040005B9 RID: 1465
			ServerName,
			// Token: 0x040005BA RID: 1466
			EventId,
			// Token: 0x040005BB RID: 1467
			EventData
		}
	}
}
