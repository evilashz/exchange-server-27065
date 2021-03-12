using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004E9 RID: 1257
	[Serializable]
	public class MiniTopologyServer : MiniObject
	{
		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x06003832 RID: 14386 RVA: 0x000D9C9B File Offset: 0x000D7E9B
		internal override ADObjectSchema Schema
		{
			get
			{
				return MiniTopologyServer.schema;
			}
		}

		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x06003833 RID: 14387 RVA: 0x000D9CA2 File Offset: 0x000D7EA2
		internal override string MostDerivedObjectClass
		{
			get
			{
				return MiniTopologyServer.mostDerivedClass;
			}
		}

		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x06003834 RID: 14388 RVA: 0x000D9CA9 File Offset: 0x000D7EA9
		public string Fqdn
		{
			get
			{
				return (string)this[MiniTopologyServerSchema.Fqdn];
			}
		}

		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x06003835 RID: 14389 RVA: 0x000D9CBB File Offset: 0x000D7EBB
		public int VersionNumber
		{
			get
			{
				return (int)this[MiniTopologyServerSchema.VersionNumber];
			}
		}

		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x06003836 RID: 14390 RVA: 0x000D9CCD File Offset: 0x000D7ECD
		public int MajorVersion
		{
			get
			{
				return (int)this[MiniTopologyServerSchema.MajorVersion];
			}
		}

		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x06003837 RID: 14391 RVA: 0x000D9CDF File Offset: 0x000D7EDF
		public ServerVersion AdminDisplayVersion
		{
			get
			{
				return (ServerVersion)this[MiniTopologyServerSchema.AdminDisplayVersion];
			}
		}

		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x06003838 RID: 14392 RVA: 0x000D9CF1 File Offset: 0x000D7EF1
		public bool IsE14OrLater
		{
			get
			{
				return (bool)this[MiniTopologyServerSchema.IsE14OrLater];
			}
		}

		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x06003839 RID: 14393 RVA: 0x000D9D03 File Offset: 0x000D7F03
		public bool IsExchange2007OrLater
		{
			get
			{
				return (bool)this[MiniTopologyServerSchema.IsExchange2007OrLater];
			}
		}

		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x0600383A RID: 14394 RVA: 0x000D9D15 File Offset: 0x000D7F15
		public bool IsE15OrLater
		{
			get
			{
				return (bool)this[MiniTopologyServerSchema.IsE15OrLater];
			}
		}

		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x0600383B RID: 14395 RVA: 0x000D9D27 File Offset: 0x000D7F27
		public ADObjectId ServerSite
		{
			get
			{
				return (ADObjectId)this[MiniTopologyServerSchema.ServerSite];
			}
		}

		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x0600383C RID: 14396 RVA: 0x000D9D39 File Offset: 0x000D7F39
		public string ExchangeLegacyDN
		{
			get
			{
				return (string)this[MiniTopologyServerSchema.ExchangeLegacyDN];
			}
		}

		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x0600383D RID: 14397 RVA: 0x000D9D4B File Offset: 0x000D7F4B
		public ServerRole CurrentServerRole
		{
			get
			{
				return (ServerRole)this[MiniTopologyServerSchema.CurrentServerRole];
			}
		}

		// Token: 0x17001162 RID: 4450
		// (get) Token: 0x0600383E RID: 14398 RVA: 0x000D9D5D File Offset: 0x000D7F5D
		public bool IsClientAccessServer
		{
			get
			{
				return (bool)this[MiniTopologyServerSchema.IsClientAccessServer];
			}
		}

		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x0600383F RID: 14399 RVA: 0x000D9D6F File Offset: 0x000D7F6F
		public bool IsCafeServer
		{
			get
			{
				return (bool)this[MiniTopologyServerSchema.IsCafeServer];
			}
		}

		// Token: 0x17001164 RID: 4452
		// (get) Token: 0x06003840 RID: 14400 RVA: 0x000D9D81 File Offset: 0x000D7F81
		public bool IsHubTransportServer
		{
			get
			{
				return (bool)this[MiniTopologyServerSchema.IsHubTransportServer];
			}
		}

		// Token: 0x17001165 RID: 4453
		// (get) Token: 0x06003841 RID: 14401 RVA: 0x000D9D93 File Offset: 0x000D7F93
		public bool IsEdgeServer
		{
			get
			{
				return (bool)this[MiniTopologyServerSchema.IsEdgeServer];
			}
		}

		// Token: 0x17001166 RID: 4454
		// (get) Token: 0x06003842 RID: 14402 RVA: 0x000D9DA5 File Offset: 0x000D7FA5
		public bool IsFrontendTransportServer
		{
			get
			{
				return (bool)this[MiniTopologyServerSchema.IsFrontendTransportServer];
			}
		}

		// Token: 0x17001167 RID: 4455
		// (get) Token: 0x06003843 RID: 14403 RVA: 0x000D9DB7 File Offset: 0x000D7FB7
		public bool IsMailboxServer
		{
			get
			{
				return (bool)this[MiniTopologyServerSchema.IsMailboxServer];
			}
		}

		// Token: 0x17001168 RID: 4456
		// (get) Token: 0x06003844 RID: 14404 RVA: 0x000D9DC9 File Offset: 0x000D7FC9
		public ADObjectId DatabaseAvailabilityGroup
		{
			get
			{
				return (ADObjectId)this[MiniTopologyServerSchema.DatabaseAvailabilityGroup];
			}
		}

		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x06003845 RID: 14405 RVA: 0x000D9DDB File Offset: 0x000D7FDB
		public MultiValuedProperty<string> ComponentStates
		{
			get
			{
				return (MultiValuedProperty<string>)this[MiniTopologyServerSchema.ComponentStates];
			}
		}

		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x06003846 RID: 14406 RVA: 0x000D9DED File Offset: 0x000D7FED
		public ADObjectId HomeRoutingGroup
		{
			get
			{
				return (ADObjectId)this[MiniTopologyServerSchema.HomeRoutingGroup];
			}
		}

		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x06003847 RID: 14407 RVA: 0x000D9DFF File Offset: 0x000D7FFF
		internal SmtpAddress? ExternalPostmasterAddress
		{
			get
			{
				return (SmtpAddress?)this[MiniTopologyServerSchema.ExternalPostmasterAddress];
			}
		}

		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x06003848 RID: 14408 RVA: 0x000D9E11 File Offset: 0x000D8011
		internal ITopologyConfigurationSession Session
		{
			get
			{
				return (ITopologyConfigurationSession)this.m_Session;
			}
		}

		// Token: 0x06003849 RID: 14409 RVA: 0x000D9E20 File Offset: 0x000D8020
		internal void SetProperties(ADObject server)
		{
			ADPropertyBag adpropertyBag = new ADPropertyBag();
			adpropertyBag.SetIsReadOnly(false);
			foreach (PropertyDefinition propertyDefinition in this.Schema.AllProperties)
			{
				ADPropertyDefinition key = (ADPropertyDefinition)propertyDefinition;
				object value = server.propertyBag.Contains(key) ? server.propertyBag[key] : null;
				adpropertyBag.SetField(key, value);
			}
			MultiValuedProperty<string> multiValuedProperty = adpropertyBag[ADObjectSchema.ObjectClass] as MultiValuedProperty<string>;
			if (multiValuedProperty == null || multiValuedProperty.Count == 0)
			{
				multiValuedProperty = new MultiValuedProperty<string>(this.MostDerivedObjectClass);
				adpropertyBag.SetField(ADObjectSchema.ObjectClass, multiValuedProperty);
			}
			if (adpropertyBag[ADObjectSchema.WhenChangedUTC] == null)
			{
				DateTime utcNow = DateTime.UtcNow;
				adpropertyBag.SetField(ADObjectSchema.WhenChangedUTC, utcNow);
				adpropertyBag.SetField(ADObjectSchema.WhenCreatedUTC, utcNow);
			}
			adpropertyBag.SetIsReadOnly(true);
			this.propertyBag = adpropertyBag;
		}

		// Token: 0x040025F7 RID: 9719
		private static MiniTopologyServerSchema schema = ObjectSchema.GetInstance<MiniTopologyServerSchema>();

		// Token: 0x040025F8 RID: 9720
		private static string mostDerivedClass = "msExchExchangeServer";
	}
}
