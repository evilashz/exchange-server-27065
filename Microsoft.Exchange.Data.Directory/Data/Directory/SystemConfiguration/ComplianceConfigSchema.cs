using System;
using System.Configuration;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003BF RID: 959
	internal sealed class ComplianceConfigSchema : ConfigSchemaBase
	{
		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06002C05 RID: 11269 RVA: 0x000B5E37 File Offset: 0x000B4037
		public override string Name
		{
			get
			{
				return "Compliance";
			}
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06002C06 RID: 11270 RVA: 0x000B5E3E File Offset: 0x000B403E
		public override string SectionName
		{
			get
			{
				return "ComplianceConfiguration";
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06002C07 RID: 11271 RVA: 0x000B5E45 File Offset: 0x000B4045
		// (set) Token: 0x06002C08 RID: 11272 RVA: 0x000B5E57 File Offset: 0x000B4057
		[ConfigurationProperty("JournalArchivingHardeningEnabled", DefaultValue = true)]
		public bool JournalArchivingHardeningEnabled
		{
			get
			{
				return (bool)base["JournalArchivingHardeningEnabled"];
			}
			set
			{
				base["JournalArchivingHardeningEnabled"] = value;
			}
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06002C09 RID: 11273 RVA: 0x000B5E6A File Offset: 0x000B406A
		// (set) Token: 0x06002C0A RID: 11274 RVA: 0x000B5E7C File Offset: 0x000B407C
		[ConfigurationProperty("ArchivePropertiesHardeningEnabled", DefaultValue = true)]
		public bool ArchivePropertiesHardeningEnabled
		{
			get
			{
				return (bool)base["ArchivePropertiesHardeningEnabled"];
			}
			set
			{
				base["ArchivePropertiesHardeningEnabled"] = value;
			}
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x000B5E8F File Offset: 0x000B408F
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			ExTraceGlobals.ComplianceTracer.TraceDebug<string, string>(0L, "Unrecognized configuration attribute {0}={1}", name, value);
			return base.OnDeserializeUnrecognizedAttribute(name, value);
		}

		// Token: 0x020003C0 RID: 960
		internal static class Setting
		{
			// Token: 0x04001A5F RID: 6751
			public const string JournalArchivingHardeningEnabled = "JournalArchivingHardeningEnabled";

			// Token: 0x04001A60 RID: 6752
			public const string ArchivePropertiesHardeningEnabled = "ArchivePropertiesHardeningEnabled";
		}
	}
}
