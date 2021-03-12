using System;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x0200011C RID: 284
	public sealed class VariantConfigurationPopComponent : VariantConfigurationComponent
	{
		// Token: 0x06000D49 RID: 3401 RVA: 0x00020068 File Offset: 0x0001E268
		internal VariantConfigurationPopComponent() : base("Pop")
		{
			base.Add(new VariantConfigurationSection("Pop.settings.ini", "PopClientAccessRulesEnabled", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Pop.settings.ini", "IgnoreNonProvisionedServers", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Pop.settings.ini", "UseSamAccountNameAsUsername", typeof(IFeature), true));
			base.Add(new VariantConfigurationSection("Pop.settings.ini", "SkipAuthOnCafe", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Pop.settings.ini", "GlobalCriminalCompliance", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Pop.settings.ini", "CheckOnlyAuthenticationStatus", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Pop.settings.ini", "EnforceLogsRetentionPolicy", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Pop.settings.ini", "AppendServerNameInBanner", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Pop.settings.ini", "UsePrimarySmtpAddress", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Pop.settings.ini", "LrsLogging", typeof(IFeature), false));
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x000201C0 File Offset: 0x0001E3C0
		public VariantConfigurationSection PopClientAccessRulesEnabled
		{
			get
			{
				return base["PopClientAccessRulesEnabled"];
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x000201CD File Offset: 0x0001E3CD
		public VariantConfigurationSection IgnoreNonProvisionedServers
		{
			get
			{
				return base["IgnoreNonProvisionedServers"];
			}
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x000201DA File Offset: 0x0001E3DA
		public VariantConfigurationSection UseSamAccountNameAsUsername
		{
			get
			{
				return base["UseSamAccountNameAsUsername"];
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x000201E7 File Offset: 0x0001E3E7
		public VariantConfigurationSection SkipAuthOnCafe
		{
			get
			{
				return base["SkipAuthOnCafe"];
			}
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x000201F4 File Offset: 0x0001E3F4
		public VariantConfigurationSection GlobalCriminalCompliance
		{
			get
			{
				return base["GlobalCriminalCompliance"];
			}
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x00020201 File Offset: 0x0001E401
		public VariantConfigurationSection CheckOnlyAuthenticationStatus
		{
			get
			{
				return base["CheckOnlyAuthenticationStatus"];
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x0002020E File Offset: 0x0001E40E
		public VariantConfigurationSection EnforceLogsRetentionPolicy
		{
			get
			{
				return base["EnforceLogsRetentionPolicy"];
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x0002021B File Offset: 0x0001E41B
		public VariantConfigurationSection AppendServerNameInBanner
		{
			get
			{
				return base["AppendServerNameInBanner"];
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x00020228 File Offset: 0x0001E428
		public VariantConfigurationSection UsePrimarySmtpAddress
		{
			get
			{
				return base["UsePrimarySmtpAddress"];
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x00020235 File Offset: 0x0001E435
		public VariantConfigurationSection LrsLogging
		{
			get
			{
				return base["LrsLogging"];
			}
		}
	}
}
