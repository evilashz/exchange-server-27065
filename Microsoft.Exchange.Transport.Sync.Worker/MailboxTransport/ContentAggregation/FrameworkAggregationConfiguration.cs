using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000048 RID: 72
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class FrameworkAggregationConfiguration
	{
		// Token: 0x06000344 RID: 836 RVA: 0x0000F7A8 File Offset: 0x0000D9A8
		private FrameworkAggregationConfiguration()
		{
			this.deltaSyncSettingsUpdateInterval = TimeSpan.FromDays(1.0);
			this.deltaSyncEndPointUnreachableThreshold = TimeSpan.FromHours(1.0);
			this.imapMaxFoldersSupported = 2000;
			this.ReadAppConfig();
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000F7F4 File Offset: 0x0000D9F4
		internal static FrameworkAggregationConfiguration Instance
		{
			get
			{
				if (FrameworkAggregationConfiguration.instance == null)
				{
					lock (FrameworkAggregationConfiguration.instanceInitializationLock)
					{
						if (FrameworkAggregationConfiguration.instance == null)
						{
							FrameworkAggregationConfiguration.instance = new FrameworkAggregationConfiguration();
						}
					}
				}
				return FrameworkAggregationConfiguration.instance;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000F84C File Offset: 0x0000DA4C
		internal static ExEventLog EventLogger
		{
			get
			{
				return FrameworkAggregationConfiguration.eventLogger;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000F853 File Offset: 0x0000DA53
		public TimeSpan DeltaSyncSettingsUpdateInterval
		{
			get
			{
				return this.deltaSyncSettingsUpdateInterval;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000F85B File Offset: 0x0000DA5B
		public TimeSpan DeltaSyncEndPointUnreachableThreshold
		{
			get
			{
				return this.deltaSyncEndPointUnreachableThreshold;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000F863 File Offset: 0x0000DA63
		public int ImapMaxFoldersSupported
		{
			get
			{
				return this.imapMaxFoldersSupported;
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000F86C File Offset: 0x0000DA6C
		private void ReadAppConfig()
		{
			string[] array = new string[]
			{
				"DeltaSyncEndPointUnreachableThreshold",
				"DeltaSyncSettingsUpdateInterval",
				"ImapMaxFoldersSupported"
			};
			string exePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FrameworkAggregationConfiguration.TransportProcessName);
			Configuration configuration = ConfigurationManager.OpenExeConfiguration(exePath);
			foreach (string text in array)
			{
				string text2 = null;
				try
				{
					if (configuration.AppSettings.Settings[text] != null)
					{
						text2 = configuration.AppSettings.Settings[text].Value;
					}
				}
				catch (ConfigurationErrorsException arg)
				{
					ExTraceGlobals.CommonTracer.TraceWarning<string, ConfigurationErrorsException>(0L, "failed to read config {0}: {1}", text, arg);
				}
				if (string.IsNullOrEmpty(text2))
				{
					ExTraceGlobals.CommonTracer.TraceDebug<string>(0L, "cannot apply null/empty config {0}", text);
				}
				else
				{
					bool flag = true;
					string a;
					if ((a = text) != null)
					{
						TimeSpan timeSpan;
						if (!(a == "DeltaSyncEndPointUnreachableThreshold"))
						{
							if (!(a == "DeltaSyncSettingsUpdateInterval"))
							{
								if (a == "ImapMaxFoldersSupported")
								{
									int num;
									if (int.TryParse(text2, out num))
									{
										this.imapMaxFoldersSupported = num;
									}
									else
									{
										flag = false;
									}
								}
							}
							else if (TimeSpan.TryParse(text2, out timeSpan))
							{
								this.deltaSyncSettingsUpdateInterval = timeSpan;
							}
							else
							{
								flag = false;
							}
						}
						else if (TimeSpan.TryParse(text2, out timeSpan))
						{
							this.deltaSyncEndPointUnreachableThreshold = timeSpan;
						}
						else
						{
							flag = false;
						}
					}
					if (!flag)
					{
						ExTraceGlobals.CommonTracer.TraceWarning<string, string>(0L, "cannot apply config {0} with invalid value: {1}", text, text2);
					}
				}
			}
		}

		// Token: 0x040001BA RID: 442
		private static readonly string TransportProcessName = "EdgeTransport.exe";

		// Token: 0x040001BB RID: 443
		private static object instanceInitializationLock = new object();

		// Token: 0x040001BC RID: 444
		private static FrameworkAggregationConfiguration instance;

		// Token: 0x040001BD RID: 445
		private static ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.EventLogTracer.Category, "MSExchangeTransportSyncWorker");

		// Token: 0x040001BE RID: 446
		private TimeSpan deltaSyncSettingsUpdateInterval;

		// Token: 0x040001BF RID: 447
		private TimeSpan deltaSyncEndPointUnreachableThreshold;

		// Token: 0x040001C0 RID: 448
		private int imapMaxFoldersSupported;
	}
}
