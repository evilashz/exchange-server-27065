using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Inference.Common;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage.Clutter
{
	// Token: 0x02000443 RID: 1091
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ServerModelConfigurationWrapper : IServerModelConfiguration
	{
		// Token: 0x060030B2 RID: 12466 RVA: 0x000C7DAC File Offset: 0x000C5FAC
		private ServerModelConfigurationWrapper()
		{
			this.configurationSettings = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Inference.InferenceClutterModelConfigurationSettings;
		}

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x060030B3 RID: 12467 RVA: 0x000C7DDE File Offset: 0x000C5FDE
		public static Hookable<Func<IServerModelConfiguration>> HookableServerModelConfiguration
		{
			get
			{
				return ServerModelConfigurationWrapper.HookableWrapper;
			}
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x060030B4 RID: 12468 RVA: 0x000C7DE5 File Offset: 0x000C5FE5
		public static IServerModelConfiguration CurrentWrapper
		{
			get
			{
				return ServerModelConfigurationWrapper.HookableWrapper.Value();
			}
		}

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x060030B5 RID: 12469 RVA: 0x000C7DF6 File Offset: 0x000C5FF6
		public int MaxModelVersion
		{
			get
			{
				return this.configurationSettings.MaxModelVersion;
			}
		}

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x060030B6 RID: 12470 RVA: 0x000C7E03 File Offset: 0x000C6003
		public int MinModelVersion
		{
			get
			{
				return this.configurationSettings.MinModelVersion;
			}
		}

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x060030B7 RID: 12471 RVA: 0x000C7E10 File Offset: 0x000C6010
		public int NumberOfVersionCrumbsToRecord
		{
			get
			{
				return this.configurationSettings.NumberOfVersionCrumbsToRecord;
			}
		}

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x060030B8 RID: 12472 RVA: 0x000C7E1D File Offset: 0x000C601D
		public bool AllowTrainingOnMutipleModelVersions
		{
			get
			{
				return this.configurationSettings.AllowTrainingOnMutipleModelVersions;
			}
		}

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x060030B9 RID: 12473 RVA: 0x000C7E2A File Offset: 0x000C602A
		public int NumberOfModelVersionToTrain
		{
			get
			{
				return this.configurationSettings.NumberOfModelVersionToTrain;
			}
		}

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x060030BA RID: 12474 RVA: 0x000C7E37 File Offset: 0x000C6037
		public IEnumerable<int> BlockedModelVersions
		{
			get
			{
				return this.configurationSettings.BlockedModelVersions;
			}
		}

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x060030BB RID: 12475 RVA: 0x000C7E44 File Offset: 0x000C6044
		public IEnumerable<int> ClassificationModelVersions
		{
			get
			{
				return this.configurationSettings.ClassificationModelVersions;
			}
		}

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x060030BC RID: 12476 RVA: 0x000C7E51 File Offset: 0x000C6051
		public IEnumerable<int> DeprecatedModelVersions
		{
			get
			{
				return this.configurationSettings.DeprecatedModelVersions;
			}
		}

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x060030BD RID: 12477 RVA: 0x000C7E5E File Offset: 0x000C605E
		public double ProbabilityBehaviourSwitchPerWeek
		{
			get
			{
				return this.configurationSettings.ProbabilityBehaviourSwitchPerWeek;
			}
		}

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x060030BE RID: 12478 RVA: 0x000C7E6B File Offset: 0x000C606B
		public double SymmetricNoise
		{
			get
			{
				return this.configurationSettings.SymmetricNoise;
			}
		}

		// Token: 0x04001A77 RID: 6775
		private static readonly Hookable<Func<IServerModelConfiguration>> HookableWrapper = Hookable<Func<IServerModelConfiguration>>.Create(true, () => new ServerModelConfigurationWrapper());

		// Token: 0x04001A78 RID: 6776
		private readonly IClutterModelConfigurationSettings configurationSettings;
	}
}
