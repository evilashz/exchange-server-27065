using System;
using Microsoft.Exchange.Inference.Common;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x0200010C RID: 268
	public sealed class VariantConfigurationInferenceComponent : VariantConfigurationComponent
	{
		// Token: 0x06000C32 RID: 3122 RVA: 0x0001D0B8 File Offset: 0x0001B2B8
		internal VariantConfigurationInferenceComponent() : base("Inference")
		{
			base.Add(new VariantConfigurationSection("Inference.settings.ini", "ActivityLogging", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Inference.settings.ini", "InferenceTrainingConfigurationSettings", typeof(IInferenceTrainingConfigurationSettings), false));
			base.Add(new VariantConfigurationSection("Inference.settings.ini", "InferenceGroupingModel", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Inference.settings.ini", "InferenceLatentLabelModel", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Inference.settings.ini", "InferenceClutterInvitation", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Inference.settings.ini", "InferenceEventBasedAssistant", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Inference.settings.ini", "InferenceAutoEnableClutter", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Inference.settings.ini", "InferenceClutterModelConfigurationSettings", typeof(IClutterModelConfigurationSettings), false));
			base.Add(new VariantConfigurationSection("Inference.settings.ini", "InferenceClutterDataSelectionSettings", typeof(IClutterDataSelectionSettings), false));
			base.Add(new VariantConfigurationSection("Inference.settings.ini", "InferenceClutterAutoEnablementNotice", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Inference.settings.ini", "InferenceModelComparison", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Inference.settings.ini", "InferenceFolderBasedClutter", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Inference.settings.ini", "InferenceStampTracking", typeof(IFeature), false));
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0001D270 File Offset: 0x0001B470
		public VariantConfigurationSection ActivityLogging
		{
			get
			{
				return base["ActivityLogging"];
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x0001D27D File Offset: 0x0001B47D
		public VariantConfigurationSection InferenceTrainingConfigurationSettings
		{
			get
			{
				return base["InferenceTrainingConfigurationSettings"];
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x0001D28A File Offset: 0x0001B48A
		public VariantConfigurationSection InferenceGroupingModel
		{
			get
			{
				return base["InferenceGroupingModel"];
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x0001D297 File Offset: 0x0001B497
		public VariantConfigurationSection InferenceLatentLabelModel
		{
			get
			{
				return base["InferenceLatentLabelModel"];
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0001D2A4 File Offset: 0x0001B4A4
		public VariantConfigurationSection InferenceClutterInvitation
		{
			get
			{
				return base["InferenceClutterInvitation"];
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x0001D2B1 File Offset: 0x0001B4B1
		public VariantConfigurationSection InferenceEventBasedAssistant
		{
			get
			{
				return base["InferenceEventBasedAssistant"];
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0001D2BE File Offset: 0x0001B4BE
		public VariantConfigurationSection InferenceAutoEnableClutter
		{
			get
			{
				return base["InferenceAutoEnableClutter"];
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x0001D2CB File Offset: 0x0001B4CB
		public VariantConfigurationSection InferenceClutterModelConfigurationSettings
		{
			get
			{
				return base["InferenceClutterModelConfigurationSettings"];
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x0001D2D8 File Offset: 0x0001B4D8
		public VariantConfigurationSection InferenceClutterDataSelectionSettings
		{
			get
			{
				return base["InferenceClutterDataSelectionSettings"];
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x0001D2E5 File Offset: 0x0001B4E5
		public VariantConfigurationSection InferenceClutterAutoEnablementNotice
		{
			get
			{
				return base["InferenceClutterAutoEnablementNotice"];
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x0001D2F2 File Offset: 0x0001B4F2
		public VariantConfigurationSection InferenceModelComparison
		{
			get
			{
				return base["InferenceModelComparison"];
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x0001D2FF File Offset: 0x0001B4FF
		public VariantConfigurationSection InferenceFolderBasedClutter
		{
			get
			{
				return base["InferenceFolderBasedClutter"];
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x0001D30C File Offset: 0x0001B50C
		public VariantConfigurationSection InferenceStampTracking
		{
			get
			{
				return base["InferenceStampTracking"];
			}
		}
	}
}
