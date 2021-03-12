using System;
using System.Configuration;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000409 RID: 1033
	internal class DirectoryTasksConfigSchema : ConfigSchemaBase
	{
		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x06002ECD RID: 11981 RVA: 0x000BE1EF File Offset: 0x000BC3EF
		public override string Name
		{
			get
			{
				return "DirectoryTasks";
			}
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x06002ECE RID: 11982 RVA: 0x000BE1F6 File Offset: 0x000BC3F6
		public override string SectionName
		{
			get
			{
				return "DirectoryTasksConfiguration";
			}
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x06002ECF RID: 11983 RVA: 0x000BE1FD File Offset: 0x000BC3FD
		// (set) Token: 0x06002ED0 RID: 11984 RVA: 0x000BE20F File Offset: 0x000BC40F
		[ConfigurationProperty("IsDirectoryTaskProcessingEnabled", DefaultValue = true)]
		public bool IsDirectoryTaskProcessingEnabled
		{
			get
			{
				return (bool)base["IsDirectoryTaskProcessingEnabled"];
			}
			set
			{
				base["IsDirectoryTaskProcessingEnabled"] = value;
			}
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x06002ED1 RID: 11985 RVA: 0x000BE222 File Offset: 0x000BC422
		// (set) Token: 0x06002ED2 RID: 11986 RVA: 0x000BE234 File Offset: 0x000BC434
		[ConfigurationProperty("MaxConcurrentNonRecurringTasks", DefaultValue = "1")]
		public uint MaxConcurrentNonRecurringTasks
		{
			get
			{
				return (uint)base["MaxConcurrentNonRecurringTasks"];
			}
			set
			{
				base["MaxConcurrentNonRecurringTasks"] = value;
			}
		}

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x06002ED3 RID: 11987 RVA: 0x000BE247 File Offset: 0x000BC447
		// (set) Token: 0x06002ED4 RID: 11988 RVA: 0x000BE259 File Offset: 0x000BC459
		[ConfigurationProperty("OffersRequiringSCT", DefaultValue = new string[]
		{
			"BPOS_L",
			"BPOS_M",
			"BPOS_S",
			"BPOS_Basic_CustomDomain"
		})]
		public string[] OffersRequiringSCT
		{
			get
			{
				return (string[])base["OffersRequiringSCT"];
			}
			set
			{
				base["OffersRequiringSCT"] = value;
			}
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x06002ED5 RID: 11989 RVA: 0x000BE267 File Offset: 0x000BC467
		// (set) Token: 0x06002ED6 RID: 11990 RVA: 0x000BE279 File Offset: 0x000BC479
		[ConfigurationProperty("DelayBetweenSCTChecksInMinutes", DefaultValue = "60")]
		public uint DelayBetweenSCTChecksInMinutes
		{
			get
			{
				return (uint)base["DelayBetweenSCTChecksInMinutes"];
			}
			set
			{
				base["DelayBetweenSCTChecksInMinutes"] = value;
			}
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x06002ED7 RID: 11991 RVA: 0x000BE28C File Offset: 0x000BC48C
		// (set) Token: 0x06002ED8 RID: 11992 RVA: 0x000BE29E File Offset: 0x000BC49E
		[ConfigurationProperty("SCTTaskMaxRandomStartDelayInMinutes", DefaultValue = "15")]
		public uint SCTTaskMaxStartDelayInMinutes
		{
			get
			{
				return (uint)base["SCTTaskMaxRandomStartDelayInMinutes"];
			}
			set
			{
				base["SCTTaskMaxRandomStartDelayInMinutes"] = value;
			}
		}

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x06002ED9 RID: 11993 RVA: 0x000BE2B1 File Offset: 0x000BC4B1
		// (set) Token: 0x06002EDA RID: 11994 RVA: 0x000BE2C3 File Offset: 0x000BC4C3
		[ConfigurationProperty("SCTCreateNumberOfRetries", DefaultValue = "3")]
		public uint SCTCreateNumberOfRetries
		{
			get
			{
				return (uint)base["SCTCreateNumberOfRetries"];
			}
			set
			{
				base["SCTCreateNumberOfRetries"] = value;
			}
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x06002EDB RID: 11995 RVA: 0x000BE2D6 File Offset: 0x000BC4D6
		// (set) Token: 0x06002EDC RID: 11996 RVA: 0x000BE2E8 File Offset: 0x000BC4E8
		[ConfigurationProperty("SCTCreateDelayBetweenRetriesInSeconds", DefaultValue = "10")]
		public uint SCTCreateDelayBetweenRetriesInSeconds
		{
			get
			{
				return (uint)base["SCTCreateDelayBetweenRetriesInSeconds"];
			}
			set
			{
				base["SCTCreateDelayBetweenRetriesInSeconds"] = value;
			}
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x06002EDD RID: 11997 RVA: 0x000BE2FB File Offset: 0x000BC4FB
		// (set) Token: 0x06002EDE RID: 11998 RVA: 0x000BE30D File Offset: 0x000BC50D
		[ConfigurationProperty("SCTCreateUseADHealthMonitor", DefaultValue = false)]
		public bool SCTCreateUseADHealthMonitor
		{
			get
			{
				return (bool)base["SCTCreateUseADHealthMonitor"];
			}
			set
			{
				base["SCTCreateUseADHealthMonitor"] = value;
			}
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x000BE320 File Offset: 0x000BC520
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			ExTraceGlobals.DirectoryTasksTracer.TraceDebug<string, string>(0L, "Unrecognized configuration attribute {0}={1}", name, value);
			return base.OnDeserializeUnrecognizedAttribute(name, value);
		}

		// Token: 0x0200040A RID: 1034
		public static class Setting
		{
			// Token: 0x04001F7F RID: 8063
			public const string IsDirectoryTaskProcessingEnabled = "IsDirectoryTaskProcessingEnabled";

			// Token: 0x04001F80 RID: 8064
			public const string MaxConcurrentNonRecurringTasks = "MaxConcurrentNonRecurringTasks";

			// Token: 0x04001F81 RID: 8065
			public const string OffersRequiringSCT = "OffersRequiringSCT";

			// Token: 0x04001F82 RID: 8066
			public const string DelayBetweenSCTChecksInMinutes = "DelayBetweenSCTChecksInMinutes";

			// Token: 0x04001F83 RID: 8067
			public const string SCTTaskMaxStartDelayInMinutes = "SCTTaskMaxRandomStartDelayInMinutes";

			// Token: 0x04001F84 RID: 8068
			public const string SCTCreateNumberOfRetries = "SCTCreateNumberOfRetries";

			// Token: 0x04001F85 RID: 8069
			public const string SCTCreateDelayBetweenRetriesInSeconds = "SCTCreateDelayBetweenRetriesInSeconds";

			// Token: 0x04001F86 RID: 8070
			public const string SCTCreateUseADHealthMonitor = "SCTCreateUseADHealthMonitor";
		}
	}
}
