using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Data.ApplicationLogic;

namespace Microsoft.Exchange.Management.Search
{
	// Token: 0x02000150 RID: 336
	public class DiagnosticInfo
	{
		// Token: 0x06000C01 RID: 3073 RVA: 0x000379BF File Offset: 0x00035BBF
		public DiagnosticInfo(string serverName)
		{
			this.serverName = serverName;
			this.Refresh();
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x000379D4 File Offset: 0x00035BD4
		internal DiagnosticInfo()
		{
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x000379DC File Offset: 0x00035BDC
		// (set) Token: 0x06000C04 RID: 3076 RVA: 0x000379E3 File Offset: 0x00035BE3
		public static int RetryCount
		{
			get
			{
				return DiagnosticInfo.retryCount;
			}
			set
			{
				DiagnosticInfo.retryCount = value;
			}
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x000379EC File Offset: 0x00035BEC
		public static DiagnosticInfo GetCachedInstance(string serverName)
		{
			if (serverName == null)
			{
				serverName = Environment.MachineName;
			}
			lock (DiagnosticInfo.diagInfoCache)
			{
				if (DiagnosticInfo.timeoutTimes.ContainsKey(serverName) && DiagnosticInfo.timeoutTimes[serverName] > DateTime.UtcNow)
				{
					return DiagnosticInfo.diagInfoCache[serverName];
				}
			}
			DiagnosticInfo diagnosticInfo = new DiagnosticInfo(serverName);
			lock (DiagnosticInfo.diagInfoCache)
			{
				DiagnosticInfo.timeoutTimes[serverName] = DateTime.UtcNow + DiagnosticInfo.DiagnosticInfoCacheTimeout;
				DiagnosticInfo.diagInfoCache[serverName] = diagnosticInfo;
			}
			return diagnosticInfo;
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00037AC0 File Offset: 0x00035CC0
		public static DiagnosticInfo GetCachedInstance()
		{
			return DiagnosticInfo.GetCachedInstance(null);
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x00037AC8 File Offset: 0x00035CC8
		// (set) Token: 0x06000C08 RID: 3080 RVA: 0x00037AD0 File Offset: 0x00035CD0
		public bool ProcessLoadedInMemory { get; private set; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x00037AD9 File Offset: 0x00035CD9
		// (set) Token: 0x06000C0A RID: 3082 RVA: 0x00037AE1 File Offset: 0x00035CE1
		public int? ProcessId { get; private set; }

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x00037AEA File Offset: 0x00035CEA
		// (set) Token: 0x06000C0C RID: 3084 RVA: 0x00037AF2 File Offset: 0x00035CF2
		public int? ThreadCount { get; private set; }

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x00037AFB File Offset: 0x00035CFB
		// (set) Token: 0x06000C0E RID: 3086 RVA: 0x00037B03 File Offset: 0x00035D03
		public string ServerName { get; private set; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x00037B0C File Offset: 0x00035D0C
		// (set) Token: 0x06000C10 RID: 3088 RVA: 0x00037B14 File Offset: 0x00035D14
		public TimeSpan? ProcessUpTime { get; private set; }

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x00037B1D File Offset: 0x00035D1D
		// (set) Token: 0x06000C12 RID: 3090 RVA: 0x00037B25 File Offset: 0x00035D25
		public DateTime? Timestamp { get; private set; }

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x00037B2E File Offset: 0x00035D2E
		// (set) Token: 0x06000C14 RID: 3092 RVA: 0x00037B36 File Offset: 0x00035D36
		public string DiagnosticInfoXml { get; private set; }

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x00037B3F File Offset: 0x00035D3F
		// (set) Token: 0x06000C16 RID: 3094 RVA: 0x00037B47 File Offset: 0x00035D47
		public DateTime? RecentGracefulDegradationExecutionTime { get; private set; }

		// Token: 0x06000C17 RID: 3095 RVA: 0x00037B50 File Offset: 0x00035D50
		public DiagnosticInfo.FeedingControllerDiagnosticInfo GetFeedingControllerDiagnosticInfo(Guid mdbGuid)
		{
			DiagnosticInfo.FeedingControllerDiagnosticInfo result = null;
			if (this.ProcessLoadedInMemory)
			{
				this.perMdbFeedingController.TryGetValue(mdbGuid, out result);
			}
			return result;
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00037B78 File Offset: 0x00035D78
		public void Refresh()
		{
			for (int i = 1; i <= DiagnosticInfo.RetryCount + 1; i++)
			{
				string text = ProcessAccessManager.ClientRunProcessCommand(this.serverName, "Microsoft.Exchange.Search.Service", null, null, false, true, null);
				this.Parse(text);
				if (this.ProcessId != null || DiagnosticInfo.RetryCount == 0)
				{
					this.DiagnosticInfoXml = text;
					return;
				}
				if (i == DiagnosticInfo.RetryCount + 1)
				{
					throw new InvalidOperationException(string.Format("Process ID is not found from diagnostic info XML:\n{0}", text));
				}
				Thread.Sleep(TimeSpan.FromSeconds(3.0));
			}
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00037C04 File Offset: 0x00035E04
		public long LowWatermark(Guid mdbGuid)
		{
			long result = 0L;
			if (this.ProcessLoadedInMemory)
			{
				DiagnosticInfo.FeedingControllerDiagnosticInfo feedingControllerDiagnosticInfo = null;
				if (this.perMdbFeedingController.TryGetValue(mdbGuid, out feedingControllerDiagnosticInfo))
				{
					result = feedingControllerDiagnosticInfo.NotificationFeederLowWatermark;
				}
			}
			return result;
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00037C38 File Offset: 0x00035E38
		public long HighWatermark(Guid mdbGuid)
		{
			long result = 0L;
			if (this.ProcessLoadedInMemory)
			{
				DiagnosticInfo.FeedingControllerDiagnosticInfo feedingControllerDiagnosticInfo = null;
				if (this.perMdbFeedingController.TryGetValue(mdbGuid, out feedingControllerDiagnosticInfo))
				{
					result = feedingControllerDiagnosticInfo.NotificationFeederHighWatermark;
				}
			}
			return result;
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00037C6C File Offset: 0x00035E6C
		public long LastEvent(Guid mdbGuid)
		{
			long result = 0L;
			if (this.ProcessLoadedInMemory)
			{
				DiagnosticInfo.FeedingControllerDiagnosticInfo feedingControllerDiagnosticInfo = null;
				if (this.perMdbFeedingController.TryGetValue(mdbGuid, out feedingControllerDiagnosticInfo))
				{
					result = feedingControllerDiagnosticInfo.NotificationFeederLastEvent;
				}
			}
			return result;
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00037CA0 File Offset: 0x00035EA0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.ProcessLoadedInMemory)
			{
				stringBuilder.AppendFormat("{0}:{1}; ", "ProcessId", this.ProcessId);
				stringBuilder.AppendFormat("{0}:{1}; ", "ThreadCount", this.ThreadCount);
				stringBuilder.AppendFormat("{0}:{1}; ", "ServerName", this.ServerName);
				stringBuilder.AppendFormat("{0}:{1}; ", "ProcessUpTime", this.ProcessUpTime);
				stringBuilder.AppendFormat("{0}:{1}; ", "Timestamp", this.Timestamp);
				stringBuilder.AppendFormat("{0}:{1}; ", "RecentGracefulDegradationExecutionTime", this.RecentGracefulDegradationExecutionTime);
				stringBuilder.AppendLine();
				using (Dictionary<Guid, DiagnosticInfo.FeedingControllerDiagnosticInfo>.ValueCollection.Enumerator enumerator = this.perMdbFeedingController.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DiagnosticInfo.FeedingControllerDiagnosticInfo feedingControllerDiagnosticInfo = enumerator.Current;
						stringBuilder.AppendLine(feedingControllerDiagnosticInfo.ToString());
					}
					goto IL_108;
				}
			}
			stringBuilder.AppendLine("ProcessLoadedInMemory: False");
			IL_108:
			return stringBuilder.ToString();
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x00037DCC File Offset: 0x00035FCC
		internal void Parse(string diagnosticInformation)
		{
			this.perMdbFeedingController = new Dictionary<Guid, DiagnosticInfo.FeedingControllerDiagnosticInfo>(50);
			try
			{
				using (TextReader textReader = new StringReader(diagnosticInformation))
				{
					using (XmlReader xmlReader = XmlReader.Create(textReader, new XmlReaderSettings
					{
						IgnoreComments = true
					}))
					{
						while (xmlReader.Read())
						{
							string name;
							if ((name = xmlReader.Name) != null)
							{
								if (!(name == "ProcessInfo"))
								{
									if (!(name == "SearchFeedingController"))
									{
										if (name == "RecentGracefulDegradationExecutionTime")
										{
											this.RecentGracefulDegradationExecutionTime = new DateTime?(DateTime.Parse(xmlReader.ReadString()));
										}
									}
									else
									{
										this.ParseSearchFeedingControllerInfo(xmlReader);
									}
								}
								else
								{
									this.ProcessLoadedInMemory = true;
									this.ParseProcessInfo(xmlReader);
								}
							}
						}
					}
				}
			}
			catch (Exception innerException)
			{
				throw new InvalidOperationException(diagnosticInformation, innerException);
			}
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00037EC0 File Offset: 0x000360C0
		private void ParseProcessInfo(XmlReader xmlReader)
		{
			while (xmlReader.Read())
			{
				if (xmlReader.Name == "ProcessInfo")
				{
					return;
				}
				if (!string.IsNullOrWhiteSpace(xmlReader.Name))
				{
					string name = xmlReader.Name;
					string s = xmlReader.ReadString();
					string a;
					if ((a = name) != null)
					{
						if (!(a == "id"))
						{
							if (!(a == "serverName"))
							{
								if (!(a == "threadCount"))
								{
									if (!(a == "lifetime"))
									{
										if (a == "currentTime")
										{
											this.Timestamp = new DateTime?(DateTime.Parse(s).ToUniversalTime());
										}
									}
									else
									{
										this.ProcessUpTime = new TimeSpan?(TimeSpan.Parse(s));
									}
								}
								else
								{
									this.ThreadCount = new int?(int.Parse(s));
								}
							}
							else
							{
								this.ServerName = s;
							}
						}
						else
						{
							this.ProcessId = new int?(int.Parse(s));
						}
					}
				}
			}
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x00037FB4 File Offset: 0x000361B4
		private void ParseSearchFeedingControllerInfo(XmlReader xmlReader)
		{
			int depth = xmlReader.Depth;
			do
			{
				if (xmlReader.Name == "SearchFeedingController")
				{
					DiagnosticInfo.FeedingControllerDiagnosticInfo feedingControllerDiagnosticInfo = new DiagnosticInfo.FeedingControllerDiagnosticInfo(xmlReader);
					if (feedingControllerDiagnosticInfo.MdbGuid != Guid.Empty)
					{
						this.perMdbFeedingController[feedingControllerDiagnosticInfo.MdbGuid] = feedingControllerDiagnosticInfo;
					}
				}
				else
				{
					xmlReader.Read();
				}
			}
			while (xmlReader.Depth >= depth);
		}

		// Token: 0x040005C3 RID: 1475
		private const string ProcessName = "Microsoft.Exchange.Search.Service";

		// Token: 0x040005C4 RID: 1476
		private const int InitialPerMdbFeedingControllerDictionarySize = 50;

		// Token: 0x040005C5 RID: 1477
		private const int RetryIntervalSeconds = 3;

		// Token: 0x040005C6 RID: 1478
		private static int retryCount = 2;

		// Token: 0x040005C7 RID: 1479
		private static readonly TimeSpan DiagnosticInfoCacheTimeout = TimeSpan.FromMinutes(1.0);

		// Token: 0x040005C8 RID: 1480
		private readonly string serverName;

		// Token: 0x040005C9 RID: 1481
		private Dictionary<Guid, DiagnosticInfo.FeedingControllerDiagnosticInfo> perMdbFeedingController;

		// Token: 0x040005CA RID: 1482
		private static Dictionary<string, DiagnosticInfo> diagInfoCache = new Dictionary<string, DiagnosticInfo>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040005CB RID: 1483
		private static Dictionary<string, DateTime> timeoutTimes = new Dictionary<string, DateTime>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x02000151 RID: 337
		public class FeedingControllerDiagnosticInfo
		{
			// Token: 0x06000C21 RID: 3105 RVA: 0x00038050 File Offset: 0x00036250
			internal FeedingControllerDiagnosticInfo(XmlReader xmlReader)
			{
				if (xmlReader.Name != "SearchFeedingController")
				{
					throw new ArgumentException("XmlReader Invalid Xml Node");
				}
				string name = xmlReader.Name;
				while (!(xmlReader.Name == name) || xmlReader.Read())
				{
					name = xmlReader.Name;
					string key;
					switch (key = name)
					{
					case "MdbGuid":
						this.MdbGuid = new Guid(xmlReader.ReadString());
						break;
					case "MdbName":
						this.MdbName = xmlReader.ReadString();
						break;
					case "OwningServer":
						this.OwningServer = xmlReader.ReadString();
						break;
					case "HighWatermark":
						this.NotificationFeederHighWatermark = xmlReader.ReadElementContentAsLong();
						break;
					case "LowWatermark":
						this.NotificationFeederLowWatermark = xmlReader.ReadElementContentAsLong();
						break;
					case "LastEvent":
						this.NotificationFeederLastEvent = xmlReader.ReadElementContentAsLong();
						break;
					case "Error":
						this.Error = xmlReader.ReadString();
						break;
					case "NotificationLastPollTime":
						this.NotificationFeederLastPollTime = xmlReader.ReadElementContentAsDateTime().ToUniversalTime();
						break;
					case "AgeOfLastNotificationProcessed":
						this.AgeOfLastNotificationProcessed = xmlReader.ReadElementContentAsInt();
						break;
					case "RetryItems":
						this.RetryItems = xmlReader.ReadElementContentAsInt();
						break;
					case "FailedItems":
						this.FailedItems = xmlReader.ReadElementContentAsInt();
						break;
					case "MailboxesLeftToCrawl":
						this.MailboxesLeftToCrawl = xmlReader.ReadElementContentAsInt();
						break;
					}
					if (name == "SearchFeedingController")
					{
						xmlReader.Read();
						return;
					}
				}
			}

			// Token: 0x17000355 RID: 853
			// (get) Token: 0x06000C22 RID: 3106 RVA: 0x0003827C File Offset: 0x0003647C
			// (set) Token: 0x06000C23 RID: 3107 RVA: 0x00038284 File Offset: 0x00036484
			public Guid MdbGuid { get; private set; }

			// Token: 0x17000356 RID: 854
			// (get) Token: 0x06000C24 RID: 3108 RVA: 0x0003828D File Offset: 0x0003648D
			// (set) Token: 0x06000C25 RID: 3109 RVA: 0x00038295 File Offset: 0x00036495
			public string MdbName { get; private set; }

			// Token: 0x17000357 RID: 855
			// (get) Token: 0x06000C26 RID: 3110 RVA: 0x0003829E File Offset: 0x0003649E
			// (set) Token: 0x06000C27 RID: 3111 RVA: 0x000382A6 File Offset: 0x000364A6
			public string OwningServer { get; private set; }

			// Token: 0x17000358 RID: 856
			// (get) Token: 0x06000C28 RID: 3112 RVA: 0x000382AF File Offset: 0x000364AF
			// (set) Token: 0x06000C29 RID: 3113 RVA: 0x000382B7 File Offset: 0x000364B7
			public long NotificationFeederLowWatermark { get; private set; }

			// Token: 0x17000359 RID: 857
			// (get) Token: 0x06000C2A RID: 3114 RVA: 0x000382C0 File Offset: 0x000364C0
			// (set) Token: 0x06000C2B RID: 3115 RVA: 0x000382C8 File Offset: 0x000364C8
			public long NotificationFeederHighWatermark { get; private set; }

			// Token: 0x1700035A RID: 858
			// (get) Token: 0x06000C2C RID: 3116 RVA: 0x000382D1 File Offset: 0x000364D1
			// (set) Token: 0x06000C2D RID: 3117 RVA: 0x000382D9 File Offset: 0x000364D9
			public long NotificationFeederLastEvent { get; private set; }

			// Token: 0x1700035B RID: 859
			// (get) Token: 0x06000C2E RID: 3118 RVA: 0x000382E2 File Offset: 0x000364E2
			// (set) Token: 0x06000C2F RID: 3119 RVA: 0x000382EA File Offset: 0x000364EA
			public DateTime NotificationFeederLastPollTime { get; private set; }

			// Token: 0x1700035C RID: 860
			// (get) Token: 0x06000C30 RID: 3120 RVA: 0x000382F3 File Offset: 0x000364F3
			// (set) Token: 0x06000C31 RID: 3121 RVA: 0x000382FB File Offset: 0x000364FB
			public string Error { get; private set; }

			// Token: 0x1700035D RID: 861
			// (get) Token: 0x06000C32 RID: 3122 RVA: 0x00038304 File Offset: 0x00036504
			// (set) Token: 0x06000C33 RID: 3123 RVA: 0x0003830C File Offset: 0x0003650C
			public int AgeOfLastNotificationProcessed { get; private set; }

			// Token: 0x1700035E RID: 862
			// (get) Token: 0x06000C34 RID: 3124 RVA: 0x00038315 File Offset: 0x00036515
			// (set) Token: 0x06000C35 RID: 3125 RVA: 0x0003831D File Offset: 0x0003651D
			public int RetryItems { get; private set; }

			// Token: 0x1700035F RID: 863
			// (get) Token: 0x06000C36 RID: 3126 RVA: 0x00038326 File Offset: 0x00036526
			// (set) Token: 0x06000C37 RID: 3127 RVA: 0x0003832E File Offset: 0x0003652E
			public int FailedItems { get; private set; }

			// Token: 0x17000360 RID: 864
			// (get) Token: 0x06000C38 RID: 3128 RVA: 0x00038337 File Offset: 0x00036537
			// (set) Token: 0x06000C39 RID: 3129 RVA: 0x0003833F File Offset: 0x0003653F
			public int MailboxesLeftToCrawl { get; private set; }

			// Token: 0x06000C3A RID: 3130 RVA: 0x00038348 File Offset: 0x00036548
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("{0}:{1}; ", "MDBGuid", this.MdbGuid);
				stringBuilder.AppendFormat("{0}:{1}; ", "MdbName", this.MdbName);
				stringBuilder.AppendFormat("{0}:{1}; ", "OwningServer", this.OwningServer);
				stringBuilder.AppendFormat("{0}:{1}; ", "LowWatermark", this.NotificationFeederLowWatermark);
				stringBuilder.AppendFormat("{0}:{1}; ", "HighWatermark", this.NotificationFeederHighWatermark);
				stringBuilder.AppendFormat("{0}:{1}; ", "LastEvent", this.NotificationFeederLastEvent);
				stringBuilder.AppendFormat("{0}:{1}; ", "NotificationLastPollTime", this.NotificationFeederLastPollTime);
				stringBuilder.AppendFormat("{0}:{1}; ", "AgeOfLastNotificationProcessed", this.AgeOfLastNotificationProcessed);
				stringBuilder.AppendFormat("{0}:{1}; ", "RetryItems", this.RetryItems);
				stringBuilder.AppendFormat("{0}:{1}; ", "FailedItems", this.FailedItems);
				stringBuilder.AppendFormat("{0}:{1}; ", "MailboxesLeftToCrawl", this.MailboxesLeftToCrawl);
				stringBuilder.AppendFormat("{0}:{1}; ", "Error", this.Error);
				return stringBuilder.ToString();
			}
		}
	}
}
