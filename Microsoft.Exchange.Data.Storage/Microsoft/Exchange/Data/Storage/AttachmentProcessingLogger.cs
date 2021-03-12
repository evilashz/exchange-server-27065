using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000352 RID: 850
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AttachmentProcessingLogger : ExtensibleLogger
	{
		// Token: 0x060025D6 RID: 9686 RVA: 0x00097B22 File Offset: 0x00095D22
		public AttachmentProcessingLogger() : base(new AttachmentProcessingLogger.AttachmentProcessingLogConfig())
		{
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x00097B2F File Offset: 0x00095D2F
		public static void Initialize()
		{
			if (AttachmentProcessingLogger.instance == null)
			{
				AttachmentProcessingLogger.instance = new AttachmentProcessingLogger();
			}
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x00097B44 File Offset: 0x00095D44
		public static void LogEvent(string eventId, string key, object value)
		{
			if (AttachmentProcessingLogger.instance != null)
			{
				AttachmentProcessingLogger.AttachmentProcessingLogEvent logEvent = new AttachmentProcessingLogger.AttachmentProcessingLogEvent(eventId, key, value);
				AttachmentProcessingLogger.instance.LogEvent(logEvent);
			}
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x00097B6C File Offset: 0x00095D6C
		public static void LogEvent(string eventId, string key1, object value1, string key2, object value2)
		{
			if (AttachmentProcessingLogger.instance != null)
			{
				AttachmentProcessingLogger.AttachmentProcessingLogEvent logEvent = new AttachmentProcessingLogger.AttachmentProcessingLogEvent(eventId, key1, value1, key2, value2);
				AttachmentProcessingLogger.instance.LogEvent(logEvent);
			}
		}

		// Token: 0x040016CA RID: 5834
		private static AttachmentProcessingLogger instance;

		// Token: 0x02000353 RID: 851
		private class AttachmentProcessingLogConfig : ILogConfiguration
		{
			// Token: 0x060025DA RID: 9690 RVA: 0x00097B98 File Offset: 0x00095D98
			public AttachmentProcessingLogConfig()
			{
				this.LogPath = AttachmentProcessingLogger.AttachmentProcessingLogConfig.DefaultLogFolderPath;
				this.MaxLogAge = TimeSpan.FromDays((double)AttachmentProcessingLogger.AttachmentProcessingLogConfig.MaxLogRetentionInDays.Value);
				this.MaxLogDirectorySizeInBytes = (long)(AttachmentProcessingLogger.AttachmentProcessingLogConfig.MaxLogDirectorySizeInGB.Value * 1024 * 1024 * 1024);
				this.MaxLogFileSizeInBytes = (long)(AttachmentProcessingLogger.AttachmentProcessingLogConfig.MaxLogFileSizeInMB.Value * 1024 * 1024);
			}

			// Token: 0x17000CA6 RID: 3238
			// (get) Token: 0x060025DB RID: 9691 RVA: 0x00097C0C File Offset: 0x00095E0C
			public bool IsLoggingEnabled
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000CA7 RID: 3239
			// (get) Token: 0x060025DC RID: 9692 RVA: 0x00097C0F File Offset: 0x00095E0F
			public bool IsActivityEventHandler
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000CA8 RID: 3240
			// (get) Token: 0x060025DD RID: 9693 RVA: 0x00097C12 File Offset: 0x00095E12
			// (set) Token: 0x060025DE RID: 9694 RVA: 0x00097C1A File Offset: 0x00095E1A
			public string LogPath { get; private set; }

			// Token: 0x17000CA9 RID: 3241
			// (get) Token: 0x060025DF RID: 9695 RVA: 0x00097C23 File Offset: 0x00095E23
			public string LogPrefix
			{
				get
				{
					return "XSOAttachmentProcessing_";
				}
			}

			// Token: 0x17000CAA RID: 3242
			// (get) Token: 0x060025E0 RID: 9696 RVA: 0x00097C2A File Offset: 0x00095E2A
			public string LogComponent
			{
				get
				{
					return "XSOAttachmentProcessing_Logs";
				}
			}

			// Token: 0x17000CAB RID: 3243
			// (get) Token: 0x060025E1 RID: 9697 RVA: 0x00097C31 File Offset: 0x00095E31
			public string LogType
			{
				get
				{
					return "XSOAttachmentProcessing Logs";
				}
			}

			// Token: 0x17000CAC RID: 3244
			// (get) Token: 0x060025E2 RID: 9698 RVA: 0x00097C38 File Offset: 0x00095E38
			// (set) Token: 0x060025E3 RID: 9699 RVA: 0x00097C40 File Offset: 0x00095E40
			public long MaxLogDirectorySizeInBytes { get; private set; }

			// Token: 0x17000CAD RID: 3245
			// (get) Token: 0x060025E4 RID: 9700 RVA: 0x00097C49 File Offset: 0x00095E49
			// (set) Token: 0x060025E5 RID: 9701 RVA: 0x00097C51 File Offset: 0x00095E51
			public long MaxLogFileSizeInBytes { get; private set; }

			// Token: 0x17000CAE RID: 3246
			// (get) Token: 0x060025E6 RID: 9702 RVA: 0x00097C5A File Offset: 0x00095E5A
			// (set) Token: 0x060025E7 RID: 9703 RVA: 0x00097C62 File Offset: 0x00095E62
			public TimeSpan MaxLogAge { get; private set; }

			// Token: 0x17000CAF RID: 3247
			// (get) Token: 0x060025E8 RID: 9704 RVA: 0x00097C6C File Offset: 0x00095E6C
			private static string DefaultLogFolderPath
			{
				get
				{
					string result;
					try
					{
						result = Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\XSOAttachmentProcessing");
					}
					catch (SetupVersionInformationCorruptException)
					{
						result = "C:\\Program Files\\Microsoft\\Exchange Server\\V15";
					}
					return result;
				}
			}

			// Token: 0x040016CB RID: 5835
			public const string LogPrefixValue = "XSOAttachmentProcessing_";

			// Token: 0x040016CC RID: 5836
			private const string LogTypeValue = "XSOAttachmentProcessing Logs";

			// Token: 0x040016CD RID: 5837
			private const string LogComponentValue = "XSOAttachmentProcessing_Logs";

			// Token: 0x040016CE RID: 5838
			private static readonly IntAppSettingsEntry MaxLogRetentionInDays = new IntAppSettingsEntry("MaxLogRetentionInDays", 30, null);

			// Token: 0x040016CF RID: 5839
			private static readonly IntAppSettingsEntry MaxLogDirectorySizeInGB = new IntAppSettingsEntry("MaxLogDirectorySizeInGB", 1, null);

			// Token: 0x040016D0 RID: 5840
			private static readonly IntAppSettingsEntry MaxLogFileSizeInMB = new IntAppSettingsEntry("MaxLogFileSizeInMB", 10, null);
		}

		// Token: 0x02000354 RID: 852
		private class AttachmentProcessingLogEvent : ILogEvent
		{
			// Token: 0x060025EA RID: 9706 RVA: 0x00097CDF File Offset: 0x00095EDF
			public AttachmentProcessingLogEvent(string eventId, string key, object value)
			{
				this.EventId = eventId;
				this.EventData = new List<KeyValuePair<string, object>>(1);
				this.EventData.Add(new KeyValuePair<string, object>(AttachmentProcessingLogger.AttachmentProcessingLogEvent.SanitizeKey(key), value));
			}

			// Token: 0x060025EB RID: 9707 RVA: 0x00097D14 File Offset: 0x00095F14
			public AttachmentProcessingLogEvent(string eventId, string key1, object value1, string key2, object value2)
			{
				this.EventId = eventId;
				this.EventData = new List<KeyValuePair<string, object>>(2);
				this.EventData.Add(new KeyValuePair<string, object>(AttachmentProcessingLogger.AttachmentProcessingLogEvent.SanitizeKey(key1), value1));
				this.EventData.Add(new KeyValuePair<string, object>(AttachmentProcessingLogger.AttachmentProcessingLogEvent.SanitizeKey(key2), value2));
			}

			// Token: 0x17000CB0 RID: 3248
			// (get) Token: 0x060025EC RID: 9708 RVA: 0x00097D6A File Offset: 0x00095F6A
			// (set) Token: 0x060025ED RID: 9709 RVA: 0x00097D72 File Offset: 0x00095F72
			public string EventId { get; private set; }

			// Token: 0x17000CB1 RID: 3249
			// (get) Token: 0x060025EE RID: 9710 RVA: 0x00097D7B File Offset: 0x00095F7B
			// (set) Token: 0x060025EF RID: 9711 RVA: 0x00097D83 File Offset: 0x00095F83
			public List<KeyValuePair<string, object>> EventData { get; private set; }

			// Token: 0x060025F0 RID: 9712 RVA: 0x00097D8C File Offset: 0x00095F8C
			public ICollection<KeyValuePair<string, object>> GetEventData()
			{
				return this.EventData;
			}

			// Token: 0x060025F1 RID: 9713 RVA: 0x00097D94 File Offset: 0x00095F94
			private static string SanitizeKey(string key)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.EnsureCapacity(key.Length);
				foreach (char c in key)
				{
					if (SpecialCharacters.IsValidKeyChar(c))
					{
						stringBuilder.Append(c);
					}
					else
					{
						stringBuilder.Append('.');
					}
				}
				return stringBuilder.ToString();
			}
		}
	}
}
