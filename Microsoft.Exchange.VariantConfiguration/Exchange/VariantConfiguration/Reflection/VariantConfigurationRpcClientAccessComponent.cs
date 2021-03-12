using System;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x0200011D RID: 285
	public sealed class VariantConfigurationRpcClientAccessComponent : VariantConfigurationComponent
	{
		// Token: 0x06000D54 RID: 3412 RVA: 0x00020244 File Offset: 0x0001E444
		internal VariantConfigurationRpcClientAccessComponent() : base("RpcClientAccess")
		{
			base.Add(new VariantConfigurationSection("RpcClientAccess.settings.ini", "FilterModernCalendarItemsMomtIcs", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("RpcClientAccess.settings.ini", "BlockInsufficientClientVersions", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("RpcClientAccess.settings.ini", "StreamInsightUploader", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("RpcClientAccess.settings.ini", "FilterModernCalendarItemsMomtSearch", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("RpcClientAccess.settings.ini", "RpcHttpClientAccessRulesEnabled", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("RpcClientAccess.settings.ini", "DetectCharsetAndConvertHtmlBodyOnSave", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("RpcClientAccess.settings.ini", "MimumResponseSizeEnforcement", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("RpcClientAccess.settings.ini", "XtcEndpoint", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("RpcClientAccess.settings.ini", "IncludeTheBodyPropertyBeingOpeningWhenEvaluatingIfAnyBodyPropertyIsDirty", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("RpcClientAccess.settings.ini", "ServerInformation", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("RpcClientAccess.settings.ini", "FilterModernCalendarItemsMomtView", typeof(IFeature), false));
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x000203BC File Offset: 0x0001E5BC
		public VariantConfigurationSection FilterModernCalendarItemsMomtIcs
		{
			get
			{
				return base["FilterModernCalendarItemsMomtIcs"];
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x000203C9 File Offset: 0x0001E5C9
		public VariantConfigurationSection BlockInsufficientClientVersions
		{
			get
			{
				return base["BlockInsufficientClientVersions"];
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x000203D6 File Offset: 0x0001E5D6
		public VariantConfigurationSection StreamInsightUploader
		{
			get
			{
				return base["StreamInsightUploader"];
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x000203E3 File Offset: 0x0001E5E3
		public VariantConfigurationSection FilterModernCalendarItemsMomtSearch
		{
			get
			{
				return base["FilterModernCalendarItemsMomtSearch"];
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06000D59 RID: 3417 RVA: 0x000203F0 File Offset: 0x0001E5F0
		public VariantConfigurationSection RpcHttpClientAccessRulesEnabled
		{
			get
			{
				return base["RpcHttpClientAccessRulesEnabled"];
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x000203FD File Offset: 0x0001E5FD
		public VariantConfigurationSection DetectCharsetAndConvertHtmlBodyOnSave
		{
			get
			{
				return base["DetectCharsetAndConvertHtmlBodyOnSave"];
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x0002040A File Offset: 0x0001E60A
		public VariantConfigurationSection MimumResponseSizeEnforcement
		{
			get
			{
				return base["MimumResponseSizeEnforcement"];
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x00020417 File Offset: 0x0001E617
		public VariantConfigurationSection XtcEndpoint
		{
			get
			{
				return base["XtcEndpoint"];
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x00020424 File Offset: 0x0001E624
		public VariantConfigurationSection IncludeTheBodyPropertyBeingOpeningWhenEvaluatingIfAnyBodyPropertyIsDirty
		{
			get
			{
				return base["IncludeTheBodyPropertyBeingOpeningWhenEvaluatingIfAnyBodyPropertyIsDirty"];
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x00020431 File Offset: 0x0001E631
		public VariantConfigurationSection ServerInformation
		{
			get
			{
				return base["ServerInformation"];
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x0002043E File Offset: 0x0001E63E
		public VariantConfigurationSection FilterModernCalendarItemsMomtView
		{
			get
			{
				return base["FilterModernCalendarItemsMomtView"];
			}
		}
	}
}
