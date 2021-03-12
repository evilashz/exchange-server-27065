using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Clients.Owa2.Server.Core;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x02000475 RID: 1141
	public class FeaturesManager : IFeaturesManager
	{
		// Token: 0x060026B4 RID: 9908 RVA: 0x0008C318 File Offset: 0x0008A518
		public static FeaturesManager Create(VariantConfigurationSnapshot configurationSnapshot, IConfigurationContext context, Func<VariantConfigurationSnapshot, IFeaturesStateOverride> featuresStateOverrideFactory)
		{
			IFeaturesStateOverride featureStateOverride = featuresStateOverrideFactory(configurationSnapshot);
			Func<IFeature, bool> isEnabled = (IFeature f) => (featureStateOverride == null || featureStateOverride.IsFeatureEnabled(f.Name)) && f.Enabled;
			HashSet<string> hashSet = new HashSet<string>();
			foreach (object obj in Enum.GetValues(typeof(Feature)))
			{
				if (context.IsFeatureEnabled((Feature)obj))
				{
					hashSet.Add(obj.ToString());
				}
			}
			foreach (KeyValuePair<string, IFeature> keyValuePair in configurationSnapshot.OwaClient.GetObjectsOfType<IFeature>())
			{
				if (isEnabled(keyValuePair.Value))
				{
					hashSet.Add(keyValuePair.Value.Name);
				}
			}
			foreach (KeyValuePair<string, IFeature> keyValuePair2 in configurationSnapshot.OwaClientServer.GetObjectsOfType<IFeature>())
			{
				if (isEnabled(keyValuePair2.Value))
				{
					hashSet.Add(keyValuePair2.Value.Name);
				}
			}
			FeaturesManager result;
			try
			{
				IDictionary<string, IFeature> objectsOfType = configurationSnapshot.OwaClient.GetObjectsOfType<IFeature>();
				HashSet<string> enabledClientOnlyFeature = new HashSet<string>(from f in objectsOfType.Values
				where isEnabled(f)
				select f.Name);
				IDictionary<string, IFeature> objectsOfType2 = configurationSnapshot.OwaServer.GetObjectsOfType<IFeature>();
				HashSet<string> enabledServerOnlyFeature = new HashSet<string>(from f in objectsOfType2.Values
				where isEnabled(f)
				select f.Name);
				IDictionary<string, IFeature> objectsOfType3 = configurationSnapshot.OwaClientServer.GetObjectsOfType<IFeature>();
				HashSet<string> enabledClientServerOnlyFeature = new HashSet<string>(from f in objectsOfType3.Values
				where isEnabled(f)
				select f.Name);
				result = new FeaturesManager(configurationSnapshot, hashSet, enabledClientOnlyFeature, enabledServerOnlyFeature, enabledClientServerOnlyFeature);
			}
			catch (KeyNotFoundException ex)
			{
				string message = string.Format("VariantConfigurationSnapshot could not find OWA component settings. Exception {0} {1}.", ex.GetType(), ex.Message);
				ExTraceGlobals.CoreTracer.TraceError(0L, message);
				throw new FlightConfigurationException(message);
			}
			return result;
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x0008C630 File Offset: 0x0008A830
		public FeaturesManager(VariantConfigurationSnapshot configurationSnapshot, HashSet<string> clientFeatures) : this(configurationSnapshot, clientFeatures, new HashSet<string>(), new HashSet<string>(), new HashSet<string>())
		{
		}

		// Token: 0x060026B6 RID: 9910 RVA: 0x0008C649 File Offset: 0x0008A849
		public FeaturesManager(VariantConfigurationSnapshot configurationSnapshot, HashSet<string> clientFeatures, HashSet<string> enabledClientOnlyFeature, HashSet<string> enabledServerOnlyFeature, HashSet<string> enabledClientServerOnlyFeature)
		{
			this.configurationSnapshot = configurationSnapshot;
			this.allEnabledClientFeatures = clientFeatures;
			this.enabledClientOnlyFeatures = enabledClientOnlyFeature;
			this.enabledServerOnlyFeatures = enabledServerOnlyFeature;
			this.enabledClientServerFeatures = enabledClientServerOnlyFeature;
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x060026B7 RID: 9911 RVA: 0x0008C676 File Offset: 0x0008A876
		public VariantConfigurationSnapshot ConfigurationSnapshot
		{
			get
			{
				return this.configurationSnapshot;
			}
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x060026B8 RID: 9912 RVA: 0x0008C67E File Offset: 0x0008A87E
		public VariantConfigurationSnapshot.OwaClientServerSettingsIni ClientServerSettings
		{
			get
			{
				return this.ConfigurationSnapshot.OwaClientServer;
			}
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x060026B9 RID: 9913 RVA: 0x0008C68B File Offset: 0x0008A88B
		public VariantConfigurationSnapshot.OwaClientSettingsIni ClientSettings
		{
			get
			{
				return this.ConfigurationSnapshot.OwaClient;
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x060026BA RID: 9914 RVA: 0x0008C698 File Offset: 0x0008A898
		public VariantConfigurationSnapshot.OwaServerSettingsIni ServerSettings
		{
			get
			{
				return this.ConfigurationSnapshot.OwaServer;
			}
		}

		// Token: 0x060026BB RID: 9915 RVA: 0x0008C6A5 File Offset: 0x0008A8A5
		public virtual string[] GetClientEnabledFeatures()
		{
			return this.allEnabledClientFeatures.ToArray<string>();
		}

		// Token: 0x060026BC RID: 9916 RVA: 0x0008C6B4 File Offset: 0x0008A8B4
		public HashSet<string> GetEnabledFlightedFeatures(FlightedFeatureScope scope)
		{
			HashSet<string> hashSet = new HashSet<string>();
			if (scope.HasFlag(FlightedFeatureScope.Client))
			{
				hashSet.UnionWith(this.enabledClientOnlyFeatures);
			}
			if (scope.HasFlag(FlightedFeatureScope.Server))
			{
				hashSet.UnionWith(this.enabledServerOnlyFeatures);
			}
			if (scope.HasFlag(FlightedFeatureScope.ClientServer))
			{
				hashSet.UnionWith(this.enabledClientServerFeatures);
			}
			return hashSet;
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x0008C725 File Offset: 0x0008A925
		public bool IsFeatureSupported(string featureId)
		{
			return this.allEnabledClientFeatures.Contains(featureId) || this.IsFlightedFeatureEnabled(featureId);
		}

		// Token: 0x060026BE RID: 9918 RVA: 0x0008C73E File Offset: 0x0008A93E
		private bool IsFlightedFeatureEnabled(string featureId)
		{
			return this.enabledClientOnlyFeatures.Contains(featureId) || this.enabledServerOnlyFeatures.Contains(featureId) || this.enabledClientServerFeatures.Contains(featureId);
		}

		// Token: 0x04001691 RID: 5777
		private readonly VariantConfigurationSnapshot configurationSnapshot;

		// Token: 0x04001692 RID: 5778
		private readonly HashSet<string> allEnabledClientFeatures;

		// Token: 0x04001693 RID: 5779
		private readonly HashSet<string> enabledClientOnlyFeatures;

		// Token: 0x04001694 RID: 5780
		private readonly HashSet<string> enabledClientServerFeatures;

		// Token: 0x04001695 RID: 5781
		private readonly HashSet<string> enabledServerOnlyFeatures;
	}
}
