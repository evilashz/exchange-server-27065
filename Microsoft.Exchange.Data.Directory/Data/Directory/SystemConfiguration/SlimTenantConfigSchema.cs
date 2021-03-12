using System;
using System.Configuration;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005A7 RID: 1447
	internal class SlimTenantConfigSchema : ConfigSchemaBase
	{
		// Token: 0x170015E8 RID: 5608
		// (get) Token: 0x06004303 RID: 17155 RVA: 0x000FC264 File Offset: 0x000FA464
		public override string Name
		{
			get
			{
				return "SlimTenant";
			}
		}

		// Token: 0x170015E9 RID: 5609
		// (get) Token: 0x06004304 RID: 17156 RVA: 0x000FC26B File Offset: 0x000FA46B
		public override string SectionName
		{
			get
			{
				return "SlimTenantConfiguration";
			}
		}

		// Token: 0x170015EA RID: 5610
		// (get) Token: 0x06004305 RID: 17157 RVA: 0x000FC272 File Offset: 0x000FA472
		// (set) Token: 0x06004306 RID: 17158 RVA: 0x000FC284 File Offset: 0x000FA484
		[ConfigurationProperty("IsHydrationAllowed", DefaultValue = true)]
		public bool IsHydrationAllowed
		{
			get
			{
				return (bool)base["IsHydrationAllowed"];
			}
			set
			{
				base["IsHydrationAllowed"] = value;
			}
		}

		// Token: 0x06004307 RID: 17159 RVA: 0x000FC297 File Offset: 0x000FA497
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			ExTraceGlobals.SlimTenantTracer.TraceDebug<string, string>(0L, "Unrecognized configuration attribute {0}={1}", name, value);
			return base.OnDeserializeUnrecognizedAttribute(name, value);
		}

		// Token: 0x020005A8 RID: 1448
		public static class Setting
		{
			// Token: 0x04002D84 RID: 11652
			public const string IsHydrationAllowed = "IsHydrationAllowed";
		}
	}
}
