using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Win32;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000006 RID: 6
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CommonConfiguration : CommonConfigurationBase
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002428 File Offset: 0x00000628
		public static ICommonConfiguration Singleton
		{
			get
			{
				return CommonConfiguration.singletonHook.Value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002434 File Offset: 0x00000634
		public override bool OutlookActivityProcessingEnabled
		{
			get
			{
				if (this.outlookActivityProcessingEnabled == null)
				{
					this.outlookActivityProcessingEnabled = new bool?(CommonConfiguration.GetInferenceRegistryValue<bool>("OutlookActivityProcessingEnabled", true));
				}
				return this.outlookActivityProcessingEnabled.Value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002464 File Offset: 0x00000664
		public override bool OutlookActivityProcessingEnabledInEba
		{
			get
			{
				if (this.outlookActivityProcessingEnabledInEba == null)
				{
					this.outlookActivityProcessingEnabledInEba = new bool?(CommonConfiguration.GetInferenceRegistryValue<bool>("OutlookActivityProcessingEnabledInEba", true));
				}
				return this.outlookActivityProcessingEnabledInEba.Value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002494 File Offset: 0x00000694
		public override TimeSpan OutlookActivityProcessingCutoffWindow
		{
			get
			{
				if (this.outlookActivityProcessingCutoffWindow == null)
				{
					string inferenceRegistryValue = CommonConfiguration.GetInferenceRegistryValue<string>("OutlookActivityProcessingCutoffWindow", null);
					TimeSpan value;
					if (inferenceRegistryValue != null && TimeSpan.TryParse(inferenceRegistryValue, out value))
					{
						this.outlookActivityProcessingCutoffWindow = new TimeSpan?(value);
					}
					else
					{
						this.outlookActivityProcessingCutoffWindow = new TimeSpan?(TimeSpan.FromDays(2.0));
					}
				}
				return this.outlookActivityProcessingCutoffWindow.Value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000024F9 File Offset: 0x000006F9
		public override bool PersistedLabelsEnabled
		{
			get
			{
				if (this.persistedLabelsEnabled == null)
				{
					this.persistedLabelsEnabled = new bool?(CommonConfiguration.GetInferenceRegistryValue<bool>("PersistedLabelsEnabled", true));
				}
				return this.persistedLabelsEnabled.Value;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002529 File Offset: 0x00000729
		internal CommonConfiguration()
		{
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002561 File Offset: 0x00000761
		private static T GetInferenceRegistryValue<T>(string valueName, T defaultValue = default(T))
		{
			return RegistryReader.Instance.GetValue<T>(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Inference", valueName, defaultValue);
		}

		// Token: 0x04000009 RID: 9
		private const string InferenceRegistryBaseKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Inference";

		// Token: 0x0400000A RID: 10
		private const string outlookActivityProcessingEnabledValueName = "OutlookActivityProcessingEnabled";

		// Token: 0x0400000B RID: 11
		private const string outlookActivityProcessingCutoffWindowValueName = "OutlookActivityProcessingCutoffWindow";

		// Token: 0x0400000C RID: 12
		private const string outlookActivityProcessingEnabledInEbaValueName = "OutlookActivityProcessingEnabledInEba";

		// Token: 0x0400000D RID: 13
		private const string persistedLabelsEnabledValueName = "PersistedLabelsEnabled";

		// Token: 0x0400000E RID: 14
		internal static readonly Hookable<ICommonConfiguration> singletonHook = Hookable<ICommonConfiguration>.Create(true, new CommonConfiguration());

		// Token: 0x0400000F RID: 15
		private bool? outlookActivityProcessingEnabled = null;

		// Token: 0x04000010 RID: 16
		private TimeSpan? outlookActivityProcessingCutoffWindow = null;

		// Token: 0x04000011 RID: 17
		private bool? outlookActivityProcessingEnabledInEba = null;

		// Token: 0x04000012 RID: 18
		private bool? persistedLabelsEnabled = null;
	}
}
