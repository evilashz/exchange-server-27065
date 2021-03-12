using System;
using System.Configuration;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000D RID: 13
	internal class MRSProxyConfiguration : ConfigurationSection
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00003D51 File Offset: 0x00001F51
		private MRSProxyConfiguration()
		{
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003D5C File Offset: 0x00001F5C
		public static MRSProxyConfiguration Instance
		{
			get
			{
				MRSProxyConfiguration mrsproxyConfiguration = null;
				try
				{
					mrsproxyConfiguration = (MRSProxyConfiguration)ConfigurationManager.GetSection("MRSProxyConfiguration");
				}
				catch (ConfigurationErrorsException ex)
				{
					MrsTracer.ProxyService.Error("Failed to load MrsProxyConfiguration: {0}", new object[]
					{
						ex.ToString()
					});
				}
				if (mrsproxyConfiguration == null)
				{
					mrsproxyConfiguration = new MRSProxyConfiguration();
				}
				return mrsproxyConfiguration;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00003DBC File Offset: 0x00001FBC
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00003DCE File Offset: 0x00001FCE
		[ConfigurationProperty("DataImportTimeout", DefaultValue = "00:05:00")]
		[TimeSpanValidator(MinValueString = "00:00:10", MaxValueString = "00:30:00", ExcludeRange = false)]
		internal TimeSpan DataImportTimeout
		{
			get
			{
				return (TimeSpan)base["DataImportTimeout"];
			}
			set
			{
				base["DataImportTimeout"] = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00003DE1 File Offset: 0x00001FE1
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00003DF3 File Offset: 0x00001FF3
		[ConfigurationProperty("MaxMRSConnections", DefaultValue = "100")]
		[IntegerValidator(MinValue = 0, MaxValue = 1000, ExcludeRange = false)]
		internal int MaxMRSConnections
		{
			get
			{
				return (int)base["MaxMRSConnections"];
			}
			set
			{
				base["MaxMRSConnections"] = value;
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003E08 File Offset: 0x00002008
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			MrsTracer.ProxyService.Warning("Ignoring unrecognized configuration attribute {0}={1}", new object[]
			{
				name,
				value
			});
			return true;
		}

		// Token: 0x0200000E RID: 14
		private static class MrsProxyConfigSchema
		{
			// Token: 0x04000036 RID: 54
			public const string SectionName = "MRSProxyConfiguration";

			// Token: 0x04000037 RID: 55
			public const string DataImportTimeout = "DataImportTimeout";

			// Token: 0x04000038 RID: 56
			public const string MaxMRSConnections = "MaxMRSConnections";
		}
	}
}
