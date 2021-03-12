using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002B8 RID: 696
	[Serializable]
	public sealed class HybridConfiguration : ADConfigurationObject
	{
		// Token: 0x06001FC5 RID: 8133 RVA: 0x0008CE10 File Offset: 0x0008B010
		internal static ADObjectId GetWellKnownLocation(ADObjectId orgContainerId)
		{
			return HybridConfiguration.GetWellKnownParentLocation(orgContainerId).GetDescendantId(HybridConfiguration.parentPath);
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x0008CE22 File Offset: 0x0008B022
		internal static ADObjectId GetWellKnownParentLocation(ADObjectId orgContainerId)
		{
			return orgContainerId.GetDescendantId(HybridConfiguration.parentPath);
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x0008CE30 File Offset: 0x0008B030
		internal static object FeaturesGetter(IPropertyBag propertyBag)
		{
			HybridFeatureFlags hybridFeaturesFlags = (HybridFeatureFlags)propertyBag[HybridConfigurationSchema.Flags];
			return HybridConfiguration.HybridFeaturesFlagsToHybridFeaturesPropertyValue(hybridFeaturesFlags);
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x0008CE54 File Offset: 0x0008B054
		internal static MultiValuedProperty<HybridFeature> HybridFeaturesFlagsToHybridFeaturesPropertyValue(HybridFeatureFlags hybridFeaturesFlags)
		{
			List<HybridFeature> list = new List<HybridFeature>(9);
			if ((hybridFeaturesFlags & HybridFeatureFlags.FreeBusy) == HybridFeatureFlags.FreeBusy)
			{
				list.Add(HybridFeature.FreeBusy);
			}
			if ((hybridFeaturesFlags & HybridFeatureFlags.MoveMailbox) == HybridFeatureFlags.MoveMailbox)
			{
				list.Add(HybridFeature.MoveMailbox);
			}
			if ((hybridFeaturesFlags & HybridFeatureFlags.Mailtips) == HybridFeatureFlags.Mailtips)
			{
				list.Add(HybridFeature.Mailtips);
			}
			if ((hybridFeaturesFlags & HybridFeatureFlags.MessageTracking) == HybridFeatureFlags.MessageTracking)
			{
				list.Add(HybridFeature.MessageTracking);
			}
			if ((hybridFeaturesFlags & HybridFeatureFlags.OwaRedirection) == HybridFeatureFlags.OwaRedirection)
			{
				list.Add(HybridFeature.OwaRedirection);
			}
			if ((hybridFeaturesFlags & HybridFeatureFlags.OnlineArchive) == HybridFeatureFlags.OnlineArchive)
			{
				list.Add(HybridFeature.OnlineArchive);
			}
			if ((hybridFeaturesFlags & HybridFeatureFlags.SecureMail) == HybridFeatureFlags.SecureMail)
			{
				list.Add(HybridFeature.SecureMail);
			}
			if ((hybridFeaturesFlags & HybridFeatureFlags.CentralizedTransportOnPrem) == HybridFeatureFlags.CentralizedTransportOnPrem)
			{
				list.Add(HybridFeature.CentralizedTransport);
			}
			if ((hybridFeaturesFlags & HybridFeatureFlags.Photos) == HybridFeatureFlags.Photos)
			{
				list.Add(HybridFeature.Photos);
			}
			return new MultiValuedProperty<HybridFeature>(list);
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x0008CEFC File Offset: 0x0008B0FC
		internal static void FeaturesSetter(object value, IPropertyBag propertyBag)
		{
			HybridFeatureFlags hybridFeatureFlags = HybridConfiguration.HybridFeaturePropertyValueToHybridFeatureFlags((MultiValuedProperty<HybridFeature>)value);
			propertyBag[HybridConfigurationSchema.Flags] = (int)hybridFeatureFlags;
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x0008CF28 File Offset: 0x0008B128
		internal static HybridFeatureFlags HybridFeaturePropertyValueToHybridFeatureFlags(MultiValuedProperty<HybridFeature> hybridFeatures)
		{
			HybridFeatureFlags hybridFeatureFlags = HybridFeatureFlags.None;
			if (hybridFeatures != null)
			{
				foreach (HybridFeature hybridFeature in hybridFeatures)
				{
					if (hybridFeature == HybridFeature.FreeBusy)
					{
						hybridFeatureFlags |= HybridFeatureFlags.FreeBusy;
					}
					else if (hybridFeature == HybridFeature.MoveMailbox)
					{
						hybridFeatureFlags |= HybridFeatureFlags.MoveMailbox;
					}
					else if (hybridFeature == HybridFeature.Mailtips)
					{
						hybridFeatureFlags |= HybridFeatureFlags.Mailtips;
					}
					else if (hybridFeature == HybridFeature.MessageTracking)
					{
						hybridFeatureFlags |= HybridFeatureFlags.MessageTracking;
					}
					else if (hybridFeature == HybridFeature.OwaRedirection)
					{
						hybridFeatureFlags |= HybridFeatureFlags.OwaRedirection;
					}
					else if (hybridFeature == HybridFeature.OnlineArchive)
					{
						hybridFeatureFlags |= HybridFeatureFlags.OnlineArchive;
					}
					else if (hybridFeature == HybridFeature.SecureMail)
					{
						hybridFeatureFlags |= HybridFeatureFlags.SecureMail;
					}
					else if (hybridFeature == HybridFeature.CentralizedTransport)
					{
						hybridFeatureFlags |= HybridFeatureFlags.CentralizedTransportOnPrem;
					}
					else
					{
						if (hybridFeature != HybridFeature.Photos)
						{
							throw new ArgumentOutOfRangeException("value");
						}
						hybridFeatureFlags |= HybridFeatureFlags.Photos;
					}
				}
			}
			return hybridFeatureFlags;
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001FCB RID: 8139 RVA: 0x0008CFE8 File Offset: 0x0008B1E8
		internal override ADObjectSchema Schema
		{
			get
			{
				return HybridConfiguration.schema;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001FCC RID: 8140 RVA: 0x0008CFEF File Offset: 0x0008B1EF
		internal override string MostDerivedObjectClass
		{
			get
			{
				return HybridConfiguration.mostDerivedClass;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001FCD RID: 8141 RVA: 0x0008CFF6 File Offset: 0x0008B1F6
		internal override ADObjectId ParentPath
		{
			get
			{
				return HybridConfiguration.parentPath;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001FCE RID: 8142 RVA: 0x0008CFFD File Offset: 0x0008B1FD
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06001FCF RID: 8143 RVA: 0x0008D004 File Offset: 0x0008B204
		// (set) Token: 0x06001FD0 RID: 8144 RVA: 0x0008D016 File Offset: 0x0008B216
		public MultiValuedProperty<ADObjectId> ClientAccessServers
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[HybridConfigurationSchema.ClientAccessServers];
			}
			set
			{
				this[HybridConfigurationSchema.ClientAccessServers] = value;
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001FD1 RID: 8145 RVA: 0x0008D024 File Offset: 0x0008B224
		// (set) Token: 0x06001FD2 RID: 8146 RVA: 0x0008D036 File Offset: 0x0008B236
		public MultiValuedProperty<ADObjectId> EdgeTransportServers
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[HybridConfigurationSchema.EdgeTransportServers];
			}
			set
			{
				this[HybridConfigurationSchema.EdgeTransportServers] = value;
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06001FD3 RID: 8147 RVA: 0x0008D044 File Offset: 0x0008B244
		// (set) Token: 0x06001FD4 RID: 8148 RVA: 0x0008D056 File Offset: 0x0008B256
		public MultiValuedProperty<ADObjectId> ReceivingTransportServers
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[HybridConfigurationSchema.ReceivingTransportServers];
			}
			set
			{
				this[HybridConfigurationSchema.ReceivingTransportServers] = value;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06001FD5 RID: 8149 RVA: 0x0008D064 File Offset: 0x0008B264
		// (set) Token: 0x06001FD6 RID: 8150 RVA: 0x0008D076 File Offset: 0x0008B276
		public MultiValuedProperty<ADObjectId> SendingTransportServers
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[HybridConfigurationSchema.SendingTransportServers];
			}
			set
			{
				this[HybridConfigurationSchema.SendingTransportServers] = value;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001FD7 RID: 8151 RVA: 0x0008D084 File Offset: 0x0008B284
		// (set) Token: 0x06001FD8 RID: 8152 RVA: 0x0008D096 File Offset: 0x0008B296
		public SmtpDomain OnPremisesSmartHost
		{
			get
			{
				return (SmtpDomain)this[HybridConfigurationSchema.OnPremisesSmartHost];
			}
			set
			{
				this[HybridConfigurationSchema.OnPremisesSmartHost] = value;
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001FD9 RID: 8153 RVA: 0x0008D0A4 File Offset: 0x0008B2A4
		// (set) Token: 0x06001FDA RID: 8154 RVA: 0x0008D0B6 File Offset: 0x0008B2B6
		public MultiValuedProperty<AutoDiscoverSmtpDomain> Domains
		{
			get
			{
				return (MultiValuedProperty<AutoDiscoverSmtpDomain>)this[HybridConfigurationSchema.Domains];
			}
			set
			{
				this[HybridConfigurationSchema.Domains] = value;
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001FDB RID: 8155 RVA: 0x0008D0C4 File Offset: 0x0008B2C4
		// (set) Token: 0x06001FDC RID: 8156 RVA: 0x0008D0D6 File Offset: 0x0008B2D6
		public MultiValuedProperty<HybridFeature> Features
		{
			get
			{
				return (MultiValuedProperty<HybridFeature>)this[HybridConfigurationSchema.Features];
			}
			set
			{
				this[HybridConfigurationSchema.Features] = value;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001FDD RID: 8157 RVA: 0x0008D0E4 File Offset: 0x0008B2E4
		// (set) Token: 0x06001FDE RID: 8158 RVA: 0x0008D0F6 File Offset: 0x0008B2F6
		internal bool FreeBusySharingEnabled
		{
			get
			{
				return (bool)this[HybridConfigurationSchema.FreeBusySharingEnabled];
			}
			set
			{
				this[HybridConfigurationSchema.FreeBusySharingEnabled] = value;
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06001FDF RID: 8159 RVA: 0x0008D109 File Offset: 0x0008B309
		// (set) Token: 0x06001FE0 RID: 8160 RVA: 0x0008D11B File Offset: 0x0008B31B
		internal bool MoveMailboxEnabled
		{
			get
			{
				return (bool)this[HybridConfigurationSchema.MoveMailboxEnabled];
			}
			set
			{
				this[HybridConfigurationSchema.MoveMailboxEnabled] = value;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06001FE1 RID: 8161 RVA: 0x0008D12E File Offset: 0x0008B32E
		// (set) Token: 0x06001FE2 RID: 8162 RVA: 0x0008D140 File Offset: 0x0008B340
		internal bool MailtipsEnabled
		{
			get
			{
				return (bool)this[HybridConfigurationSchema.MailtipsEnabled];
			}
			set
			{
				this[HybridConfigurationSchema.MailtipsEnabled] = value;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06001FE3 RID: 8163 RVA: 0x0008D153 File Offset: 0x0008B353
		// (set) Token: 0x06001FE4 RID: 8164 RVA: 0x0008D165 File Offset: 0x0008B365
		internal bool MessageTrackingEnabled
		{
			get
			{
				return (bool)this[HybridConfigurationSchema.MessageTrackingEnabled];
			}
			set
			{
				this[HybridConfigurationSchema.MessageTrackingEnabled] = value;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06001FE5 RID: 8165 RVA: 0x0008D178 File Offset: 0x0008B378
		// (set) Token: 0x06001FE6 RID: 8166 RVA: 0x0008D18A File Offset: 0x0008B38A
		internal bool PhotosEnabled
		{
			get
			{
				return (bool)this[HybridConfigurationSchema.PhotosEnabled];
			}
			set
			{
				this[HybridConfigurationSchema.PhotosEnabled] = value;
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001FE7 RID: 8167 RVA: 0x0008D19D File Offset: 0x0008B39D
		// (set) Token: 0x06001FE8 RID: 8168 RVA: 0x0008D1AF File Offset: 0x0008B3AF
		internal bool OwaRedirectionEnabled
		{
			get
			{
				return (bool)this[HybridConfigurationSchema.OwaRedirectionEnabled];
			}
			set
			{
				this[HybridConfigurationSchema.OwaRedirectionEnabled] = value;
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001FE9 RID: 8169 RVA: 0x0008D1C2 File Offset: 0x0008B3C2
		// (set) Token: 0x06001FEA RID: 8170 RVA: 0x0008D1D4 File Offset: 0x0008B3D4
		internal bool OnlineArchiveEnabled
		{
			get
			{
				return (bool)this[HybridConfigurationSchema.OnlineArchiveEnabled];
			}
			set
			{
				this[HybridConfigurationSchema.OnlineArchiveEnabled] = value;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06001FEB RID: 8171 RVA: 0x0008D1E7 File Offset: 0x0008B3E7
		// (set) Token: 0x06001FEC RID: 8172 RVA: 0x0008D1F9 File Offset: 0x0008B3F9
		internal bool SecureMailEnabled
		{
			get
			{
				return (bool)this[HybridConfigurationSchema.SecureMailEnabled];
			}
			set
			{
				this[HybridConfigurationSchema.SecureMailEnabled] = value;
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001FED RID: 8173 RVA: 0x0008D20C File Offset: 0x0008B40C
		// (set) Token: 0x06001FEE RID: 8174 RVA: 0x0008D21E File Offset: 0x0008B41E
		internal bool CentralizedTransportOnPremEnabled
		{
			get
			{
				return (bool)this[HybridConfigurationSchema.CentralizedTransportOnPremEnabled];
			}
			set
			{
				this[HybridConfigurationSchema.CentralizedTransportOnPremEnabled] = value;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06001FEF RID: 8175 RVA: 0x0008D231 File Offset: 0x0008B431
		// (set) Token: 0x06001FF0 RID: 8176 RVA: 0x0008D243 File Offset: 0x0008B443
		internal bool CentralizedTransportInCloudEnabled
		{
			get
			{
				return (bool)this[HybridConfigurationSchema.CentralizedTransportInCloudEnabled];
			}
			set
			{
				this[HybridConfigurationSchema.CentralizedTransportInCloudEnabled] = value;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06001FF1 RID: 8177 RVA: 0x0008D256 File Offset: 0x0008B456
		// (set) Token: 0x06001FF2 RID: 8178 RVA: 0x0008D268 File Offset: 0x0008B468
		internal HybridFeatureFlags FeatureFlags
		{
			get
			{
				return (HybridFeatureFlags)this[HybridConfigurationSchema.Flags];
			}
			set
			{
				this[HybridConfigurationSchema.Flags] = (int)value;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06001FF3 RID: 8179 RVA: 0x0008D27B File Offset: 0x0008B47B
		// (set) Token: 0x06001FF4 RID: 8180 RVA: 0x0008D28D File Offset: 0x0008B48D
		public MultiValuedProperty<IPRange> ExternalIPAddresses
		{
			get
			{
				return (MultiValuedProperty<IPRange>)this[HybridConfigurationSchema.ExternalIPAddresses];
			}
			set
			{
				this[HybridConfigurationSchema.ExternalIPAddresses] = value;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06001FF5 RID: 8181 RVA: 0x0008D29C File Offset: 0x0008B49C
		// (set) Token: 0x06001FF6 RID: 8182 RVA: 0x0008D2E4 File Offset: 0x0008B4E4
		public SmtpX509Identifier TlsCertificateName
		{
			get
			{
				string text = this[HybridConfigurationSchema.TlsCertificateName] as string;
				if (!string.IsNullOrEmpty(text))
				{
					try
					{
						return SmtpX509Identifier.Parse(text);
					}
					catch
					{
					}
				}
				return null;
			}
			set
			{
				this[HybridConfigurationSchema.TlsCertificateName] = ((value == null) ? null : value.ToString());
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001FF7 RID: 8183 RVA: 0x0008D2FD File Offset: 0x0008B4FD
		// (set) Token: 0x06001FF8 RID: 8184 RVA: 0x0008D30F File Offset: 0x0008B50F
		public int ServiceInstance
		{
			get
			{
				return (int)this[HybridConfigurationSchema.ServiceInstance];
			}
			set
			{
				this[HybridConfigurationSchema.ServiceInstance] = value;
			}
		}

		// Token: 0x04001331 RID: 4913
		private static HybridConfigurationSchema schema = ObjectSchema.GetInstance<HybridConfigurationSchema>();

		// Token: 0x04001332 RID: 4914
		private static string mostDerivedClass = "msExchCoexistenceRelationship";

		// Token: 0x04001333 RID: 4915
		private static ADObjectId parentPath = new ADObjectId("CN=Hybrid Configuration");
	}
}
