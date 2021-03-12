using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000095 RID: 149
	internal class EasFeaturesManager : IEasFeaturesManager
	{
		// Token: 0x06000871 RID: 2161 RVA: 0x000320E0 File Offset: 0x000302E0
		public static IEasFeaturesManager Create(MiniRecipient recipient, Dictionary<EasFeature, bool> flightingOverrides)
		{
			VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(recipient.GetContext(null), null, null);
			return new EasFeaturesManager(snapshot, flightingOverrides);
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00032103 File Offset: 0x00030303
		public EasFeaturesManager(VariantConfigurationSnapshot configurationSnapshot, Dictionary<EasFeature, bool> flightingOverrides)
		{
			this.configurationSnapshot = configurationSnapshot;
			this.flightingOverrides = flightingOverrides;
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x00032119 File Offset: 0x00030319
		public VariantConfigurationSnapshot ConfigurationSnapshot
		{
			get
			{
				return this.configurationSnapshot;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x00032121 File Offset: 0x00030321
		public VariantConfigurationSnapshot.ActiveSyncSettingsIni Settings
		{
			get
			{
				return this.ConfigurationSnapshot.ActiveSync;
			}
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00032130 File Offset: 0x00030330
		public bool IsEnabled(EasFeature featureId)
		{
			bool result;
			if (this.flightingOverrides.TryGetValue(featureId, out result))
			{
				return result;
			}
			IFeature @object = this.ConfigurationSnapshot.ActiveSync.GetObject<IFeature>(featureId.ToString());
			return @object != null && @object.Enabled;
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00032179 File Offset: 0x00030379
		public bool IsOverridden(EasFeature featureId)
		{
			return this.flightingOverrides.ContainsKey(featureId);
		}

		// Token: 0x04000581 RID: 1409
		private readonly VariantConfigurationSnapshot configurationSnapshot;

		// Token: 0x04000582 RID: 1410
		private Dictionary<EasFeature, bool> flightingOverrides;
	}
}
