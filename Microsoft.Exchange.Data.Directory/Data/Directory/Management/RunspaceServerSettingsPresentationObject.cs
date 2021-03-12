using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000753 RID: 1875
	[Serializable]
	public sealed class RunspaceServerSettingsPresentationObject : ConfigurableObject, IConfigurable, ICloneable, IEquatable<RunspaceServerSettingsPresentationObject>
	{
		// Token: 0x06005B05 RID: 23301 RVA: 0x0013F044 File Offset: 0x0013D244
		public RunspaceServerSettingsPresentationObject() : base(new SimpleProviderPropertyBag())
		{
			this.RecipientViewRoot = string.Empty;
		}

		// Token: 0x06005B06 RID: 23302 RVA: 0x0013F05C File Offset: 0x0013D25C
		internal RunspaceServerSettingsPresentationObject(RunspaceServerSettings serverSettings) : base(new SimpleProviderPropertyBag())
		{
			if (serverSettings == null)
			{
				throw new ArgumentNullException("The value of parameter serverSettings is null!");
			}
			this.UserPreferredGlobalCatalog = serverSettings.UserPreferredGlobalCatalog;
			this.DefaultGlobalCatalog = serverSettings.GetSingleDefaultGlobalCatalog();
			this.DefaultGlobalCatalogsForAllForests = RunspaceServerSettingsPresentationObject.ConvertStringDictionaryToMVP<string, Fqdn>(serverSettings.PreferredGlobalCatalogs);
			this.UserPreferredConfigurationDomainController = serverSettings.UserConfigurationDomainController;
			this.DefaultConfigurationDomainController = serverSettings.GetSingleDefaultConfigurationDomainController();
			this.DefaultConfigurationDomainControllersForAllForests = RunspaceServerSettingsPresentationObject.ConvertStringDictionaryToMVP<string, Fqdn>(serverSettings.ConfigurationDomainControllers);
			this.userServerPerDomain = RunspaceServerSettingsPresentationObject.ConvertStringDictionaryToMVP<ADObjectId, Fqdn>(serverSettings.UserServerPerDomain);
			this.UserPreferredDomainControllers = new MultiValuedProperty<Fqdn>(serverSettings.UserPreferredDomainControllers);
			this.DefaultPreferredDomainControllers = new MultiValuedProperty<Fqdn>(serverSettings.PreferredDomainControllers);
			if (serverSettings.RecipientViewRoot == null)
			{
				this.RecipientViewRoot = string.Empty;
			}
			else
			{
				this.RecipientViewRoot = serverSettings.RecipientViewRoot.ToCanonicalName();
			}
			this.ViewEntireForest = serverSettings.ViewEntireForest;
			this.WriteOriginatingChangeTimestamp = serverSettings.WriteOriginatingChangeTimestamp;
			this.WriteShadowProperties = serverSettings.WriteShadowProperties;
			this.rawServerSettings = serverSettings;
		}

		// Token: 0x17001F95 RID: 8085
		// (get) Token: 0x06005B07 RID: 23303 RVA: 0x0013F158 File Offset: 0x0013D358
		// (set) Token: 0x06005B08 RID: 23304 RVA: 0x0013F16A File Offset: 0x0013D36A
		public Fqdn DefaultGlobalCatalog
		{
			get
			{
				return (Fqdn)this[RunspaceServerSettingsPresentationObjectSchema.DefaultGlobalCatalog];
			}
			set
			{
				this[RunspaceServerSettingsPresentationObjectSchema.DefaultGlobalCatalog] = value;
			}
		}

		// Token: 0x17001F96 RID: 8086
		// (get) Token: 0x06005B09 RID: 23305 RVA: 0x0013F178 File Offset: 0x0013D378
		public MultiValuedProperty<string> PreferredDomainControllerForDomain
		{
			get
			{
				return this.userServerPerDomain;
			}
		}

		// Token: 0x17001F97 RID: 8087
		// (get) Token: 0x06005B0A RID: 23306 RVA: 0x0013F180 File Offset: 0x0013D380
		// (set) Token: 0x06005B0B RID: 23307 RVA: 0x0013F192 File Offset: 0x0013D392
		public Fqdn DefaultConfigurationDomainController
		{
			get
			{
				return (Fqdn)this[RunspaceServerSettingsPresentationObjectSchema.DefaultConfigurationDomainController];
			}
			set
			{
				this[RunspaceServerSettingsPresentationObjectSchema.DefaultConfigurationDomainController] = value;
			}
		}

		// Token: 0x17001F98 RID: 8088
		// (get) Token: 0x06005B0C RID: 23308 RVA: 0x0013F1A0 File Offset: 0x0013D3A0
		// (set) Token: 0x06005B0D RID: 23309 RVA: 0x0013F1B2 File Offset: 0x0013D3B2
		public MultiValuedProperty<Fqdn> DefaultPreferredDomainControllers
		{
			get
			{
				return (MultiValuedProperty<Fqdn>)this[RunspaceServerSettingsPresentationObjectSchema.DefaultPreferredDomainControllers];
			}
			set
			{
				this[RunspaceServerSettingsPresentationObjectSchema.DefaultPreferredDomainControllers] = value;
			}
		}

		// Token: 0x17001F99 RID: 8089
		// (get) Token: 0x06005B0E RID: 23310 RVA: 0x0013F1C0 File Offset: 0x0013D3C0
		// (set) Token: 0x06005B0F RID: 23311 RVA: 0x0013F1D2 File Offset: 0x0013D3D2
		public Fqdn UserPreferredGlobalCatalog
		{
			get
			{
				return (Fqdn)this[RunspaceServerSettingsPresentationObjectSchema.UserPreferredGlobalCatalog];
			}
			set
			{
				this[RunspaceServerSettingsPresentationObjectSchema.UserPreferredGlobalCatalog] = value;
			}
		}

		// Token: 0x17001F9A RID: 8090
		// (get) Token: 0x06005B10 RID: 23312 RVA: 0x0013F1E0 File Offset: 0x0013D3E0
		// (set) Token: 0x06005B11 RID: 23313 RVA: 0x0013F1F2 File Offset: 0x0013D3F2
		public Fqdn UserPreferredConfigurationDomainController
		{
			get
			{
				return (Fqdn)this[RunspaceServerSettingsPresentationObjectSchema.UserPreferredConfigurationDomainController];
			}
			set
			{
				this[RunspaceServerSettingsPresentationObjectSchema.UserPreferredConfigurationDomainController] = value;
			}
		}

		// Token: 0x17001F9B RID: 8091
		// (get) Token: 0x06005B12 RID: 23314 RVA: 0x0013F200 File Offset: 0x0013D400
		// (set) Token: 0x06005B13 RID: 23315 RVA: 0x0013F212 File Offset: 0x0013D412
		public MultiValuedProperty<Fqdn> UserPreferredDomainControllers
		{
			get
			{
				return (MultiValuedProperty<Fqdn>)this[RunspaceServerSettingsPresentationObjectSchema.UserPreferredDomainControllers];
			}
			set
			{
				this[RunspaceServerSettingsPresentationObjectSchema.UserPreferredDomainControllers] = value;
			}
		}

		// Token: 0x17001F9C RID: 8092
		// (get) Token: 0x06005B14 RID: 23316 RVA: 0x0013F220 File Offset: 0x0013D420
		// (set) Token: 0x06005B15 RID: 23317 RVA: 0x0013F232 File Offset: 0x0013D432
		public MultiValuedProperty<string> DefaultConfigurationDomainControllersForAllForests
		{
			get
			{
				return (MultiValuedProperty<string>)this[RunspaceServerSettingsPresentationObjectSchema.DefaultConfigurationDomainControllersForAllForests];
			}
			set
			{
				this[RunspaceServerSettingsPresentationObjectSchema.DefaultConfigurationDomainControllersForAllForests] = value;
			}
		}

		// Token: 0x17001F9D RID: 8093
		// (get) Token: 0x06005B16 RID: 23318 RVA: 0x0013F240 File Offset: 0x0013D440
		// (set) Token: 0x06005B17 RID: 23319 RVA: 0x0013F252 File Offset: 0x0013D452
		public MultiValuedProperty<string> DefaultGlobalCatalogsForAllForests
		{
			get
			{
				return (MultiValuedProperty<string>)this[RunspaceServerSettingsPresentationObjectSchema.DefaultGlobalCatalogsForAllForests];
			}
			set
			{
				this[RunspaceServerSettingsPresentationObjectSchema.DefaultGlobalCatalogsForAllForests] = value;
			}
		}

		// Token: 0x17001F9E RID: 8094
		// (get) Token: 0x06005B18 RID: 23320 RVA: 0x0013F260 File Offset: 0x0013D460
		// (set) Token: 0x06005B19 RID: 23321 RVA: 0x0013F272 File Offset: 0x0013D472
		public string RecipientViewRoot
		{
			get
			{
				return (string)this[RunspaceServerSettingsPresentationObjectSchema.RecipientViewRoot];
			}
			set
			{
				this[RunspaceServerSettingsPresentationObjectSchema.RecipientViewRoot] = value;
				this[RunspaceServerSettingsPresentationObjectSchema.ViewEntireForest] = string.IsNullOrEmpty(value);
			}
		}

		// Token: 0x17001F9F RID: 8095
		// (get) Token: 0x06005B1A RID: 23322 RVA: 0x0013F296 File Offset: 0x0013D496
		// (set) Token: 0x06005B1B RID: 23323 RVA: 0x0013F2A8 File Offset: 0x0013D4A8
		public bool ViewEntireForest
		{
			get
			{
				return (bool)this[RunspaceServerSettingsPresentationObjectSchema.ViewEntireForest];
			}
			set
			{
				this[RunspaceServerSettingsPresentationObjectSchema.ViewEntireForest] = value;
				if (value)
				{
					this[RunspaceServerSettingsPresentationObjectSchema.RecipientViewRoot] = string.Empty;
				}
			}
		}

		// Token: 0x17001FA0 RID: 8096
		// (get) Token: 0x06005B1C RID: 23324 RVA: 0x0013F2CE File Offset: 0x0013D4CE
		// (set) Token: 0x06005B1D RID: 23325 RVA: 0x0013F2E0 File Offset: 0x0013D4E0
		public bool WriteOriginatingChangeTimestamp
		{
			get
			{
				return (bool)this[RunspaceServerSettingsPresentationObjectSchema.WriteOriginatingChangeTimestamp];
			}
			set
			{
				this[RunspaceServerSettingsPresentationObjectSchema.WriteOriginatingChangeTimestamp] = value;
			}
		}

		// Token: 0x17001FA1 RID: 8097
		// (get) Token: 0x06005B1E RID: 23326 RVA: 0x0013F2F3 File Offset: 0x0013D4F3
		// (set) Token: 0x06005B1F RID: 23327 RVA: 0x0013F305 File Offset: 0x0013D505
		public bool WriteShadowProperties
		{
			get
			{
				return (bool)this[RunspaceServerSettingsPresentationObjectSchema.WriteShadowProperties];
			}
			set
			{
				this[RunspaceServerSettingsPresentationObjectSchema.WriteShadowProperties] = value;
			}
		}

		// Token: 0x17001FA2 RID: 8098
		// (get) Token: 0x06005B20 RID: 23328 RVA: 0x0013F318 File Offset: 0x0013D518
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return RunspaceServerSettingsPresentationObject.schema;
			}
		}

		// Token: 0x17001FA3 RID: 8099
		// (get) Token: 0x06005B21 RID: 23329 RVA: 0x0013F31F File Offset: 0x0013D51F
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17001FA4 RID: 8100
		// (get) Token: 0x06005B22 RID: 23330 RVA: 0x0013F326 File Offset: 0x0013D526
		internal RunspaceServerSettings RawServerSettings
		{
			get
			{
				return this.rawServerSettings;
			}
		}

		// Token: 0x06005B23 RID: 23331 RVA: 0x0013F330 File Offset: 0x0013D530
		public bool Equals(RunspaceServerSettingsPresentationObject other)
		{
			if (other == null)
			{
				return false;
			}
			if (!((this.UserPreferredGlobalCatalog != null) ? this.UserPreferredGlobalCatalog.Equals(other.UserPreferredGlobalCatalog) : (other.UserPreferredGlobalCatalog == null)) || !((this.DefaultGlobalCatalog != null) ? this.DefaultGlobalCatalog.Equals(other.DefaultGlobalCatalog) : (other.DefaultGlobalCatalog == null)) || !((this.DefaultConfigurationDomainController != null) ? this.DefaultConfigurationDomainController.Equals(other.DefaultConfigurationDomainController) : (other.DefaultConfigurationDomainController == null)) || !((this.UserPreferredConfigurationDomainController != null) ? this.UserPreferredConfigurationDomainController.Equals(other.UserPreferredConfigurationDomainController) : (other.UserPreferredConfigurationDomainController == null)) || !RunspaceServerSettingsPresentationObject.SequenceEquals(this.DefaultPreferredDomainControllers, other.DefaultPreferredDomainControllers) || !RunspaceServerSettingsPresentationObject.SequenceEquals(this.UserPreferredDomainControllers, other.UserPreferredDomainControllers) || !(this.RecipientViewRoot == other.RecipientViewRoot) || this.ViewEntireForest != other.ViewEntireForest || this.WriteOriginatingChangeTimestamp != other.WriteOriginatingChangeTimestamp || this.WriteShadowProperties != other.WriteShadowProperties)
			{
				return false;
			}
			if (this.rawServerSettings == null)
			{
				return other.rawServerSettings == null;
			}
			return this.rawServerSettings.Equals(other.rawServerSettings);
		}

		// Token: 0x06005B24 RID: 23332 RVA: 0x0013F470 File Offset: 0x0013D670
		internal static bool SequenceEquals(IList firstList, IList secondList)
		{
			if (firstList.Count != secondList.Count)
			{
				return false;
			}
			for (int i = 0; i < firstList.Count; i++)
			{
				if (!firstList[i].Equals(secondList[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005B25 RID: 23333 RVA: 0x0013F4B8 File Offset: 0x0013D6B8
		private static MultiValuedProperty<string> ConvertStringDictionaryToMVP<TKey, TValue>(IDictionary<TKey, TValue> hash)
		{
			List<string> list = new List<string>(hash.Keys.Count);
			foreach (KeyValuePair<TKey, TValue> keyValuePair in hash)
			{
				List<string> list2 = list;
				string format = "<{0}, {1}>";
				TKey key = keyValuePair.Key;
				object arg = key.ToString();
				TValue value = keyValuePair.Value;
				list2.Add(string.Format(format, arg, value.ToString()));
			}
			return new MultiValuedProperty<string>(list);
		}

		// Token: 0x04003E0E RID: 15886
		private static RunspaceServerSettingsPresentationObjectSchema schema = ObjectSchema.GetInstance<RunspaceServerSettingsPresentationObjectSchema>();

		// Token: 0x04003E0F RID: 15887
		private MultiValuedProperty<string> userServerPerDomain;

		// Token: 0x04003E10 RID: 15888
		private RunspaceServerSettings rawServerSettings;
	}
}
