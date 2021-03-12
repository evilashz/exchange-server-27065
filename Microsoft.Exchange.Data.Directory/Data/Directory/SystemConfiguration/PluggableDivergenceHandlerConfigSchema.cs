using System;
using System.Configuration;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200040F RID: 1039
	internal sealed class PluggableDivergenceHandlerConfigSchema : ConfigSchemaBase
	{
		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x06002F16 RID: 12054 RVA: 0x000BE7BD File Offset: 0x000BC9BD
		public override string Name
		{
			get
			{
				return "ProvisioningDivergenceHandler";
			}
		}

		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x06002F17 RID: 12055 RVA: 0x000BE7C4 File Offset: 0x000BC9C4
		public override string SectionName
		{
			get
			{
				return "ProvisioningDivergenceHandlerConfiguration";
			}
		}

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x06002F18 RID: 12056 RVA: 0x000BE7CB File Offset: 0x000BC9CB
		// (set) Token: 0x06002F19 RID: 12057 RVA: 0x000BE7DD File Offset: 0x000BC9DD
		[ConfigurationProperty("ProvisioningDivergenceHandlerConfig", DefaultValue = "")]
		public string PluggableDivergenceHandlerConfig
		{
			get
			{
				return (string)base["ProvisioningDivergenceHandlerConfig"];
			}
			set
			{
				base["ProvisioningDivergenceHandlerConfig"] = value;
			}
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x000BE7EB File Offset: 0x000BC9EB
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			ExTraceGlobals.DirectoryTasksTracer.TraceDebug<string, string>(0L, "Unrecognized configuration attribute {0}={1}", name, value);
			return base.OnDeserializeUnrecognizedAttribute(name, value);
		}

		// Token: 0x02000410 RID: 1040
		internal static class Setting
		{
			// Token: 0x04001F97 RID: 8087
			public const string PluggableDivergenceHandlerConfig = "ProvisioningDivergenceHandlerConfig";
		}
	}
}
