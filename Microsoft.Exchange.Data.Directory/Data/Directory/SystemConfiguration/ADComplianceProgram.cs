using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003C3 RID: 963
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class ADComplianceProgram : ADConfigurationObject
	{
		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06002C0F RID: 11279 RVA: 0x000B6083 File Offset: 0x000B4283
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADComplianceProgram.schema;
			}
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06002C10 RID: 11280 RVA: 0x000B608A File Offset: 0x000B428A
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADComplianceProgram.mostDerivedClass;
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06002C11 RID: 11281 RVA: 0x000B6091 File Offset: 0x000B4291
		// (set) Token: 0x06002C12 RID: 11282 RVA: 0x000B60A3 File Offset: 0x000B42A3
		public string TransportRulesXml
		{
			get
			{
				return (string)this[ADComplianceProgramSchema.TransportRulesXml];
			}
			internal set
			{
				this[ADComplianceProgramSchema.TransportRulesXml] = value;
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06002C13 RID: 11283 RVA: 0x000B60B1 File Offset: 0x000B42B1
		// (set) Token: 0x06002C14 RID: 11284 RVA: 0x000B60C3 File Offset: 0x000B42C3
		public string PublisherName
		{
			get
			{
				return (string)this[ADComplianceProgramSchema.PublisherName];
			}
			internal set
			{
				this[ADComplianceProgramSchema.PublisherName] = value;
			}
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06002C15 RID: 11285 RVA: 0x000B60D1 File Offset: 0x000B42D1
		// (set) Token: 0x06002C16 RID: 11286 RVA: 0x000B60E3 File Offset: 0x000B42E3
		public DlpPolicyState State
		{
			get
			{
				return (DlpPolicyState)this[ADComplianceProgramSchema.State];
			}
			internal set
			{
				this[ADComplianceProgramSchema.State] = value;
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06002C17 RID: 11287 RVA: 0x000B60F6 File Offset: 0x000B42F6
		// (set) Token: 0x06002C18 RID: 11288 RVA: 0x000B6108 File Offset: 0x000B4308
		public string Version
		{
			get
			{
				return (string)this[ADComplianceProgramSchema.Version];
			}
			internal set
			{
				this[ADComplianceProgramSchema.Version] = value;
			}
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06002C19 RID: 11289 RVA: 0x000B6116 File Offset: 0x000B4316
		// (set) Token: 0x06002C1A RID: 11290 RVA: 0x000B6128 File Offset: 0x000B4328
		public string Description
		{
			get
			{
				return (string)this[ADComplianceProgramSchema.Description];
			}
			internal set
			{
				this[ADComplianceProgramSchema.Description] = value;
			}
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06002C1B RID: 11291 RVA: 0x000B6138 File Offset: 0x000B4338
		// (set) Token: 0x06002C1C RID: 11292 RVA: 0x000B617F File Offset: 0x000B437F
		public Guid ImmutableId
		{
			get
			{
				Guid guid = (Guid)this[ADComplianceProgramSchema.ImmutableId];
				if (guid == Guid.Empty)
				{
					guid = ((base.Id == null) ? Guid.Empty : base.Id.ObjectGuid);
				}
				return guid;
			}
			internal set
			{
				this[ADComplianceProgramSchema.ImmutableId] = value;
			}
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06002C1D RID: 11293 RVA: 0x000B6192 File Offset: 0x000B4392
		// (set) Token: 0x06002C1E RID: 11294 RVA: 0x000B61AC File Offset: 0x000B43AC
		public string[] Countries
		{
			get
			{
				return ((MultiValuedProperty<string>)this[ADComplianceProgramSchema.Countries]).ToArray();
			}
			internal set
			{
				MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>();
				for (int i = 0; i < value.Length; i++)
				{
					string item = value[i];
					multiValuedProperty.Add(item);
				}
				this[ADComplianceProgramSchema.Countries] = multiValuedProperty;
			}
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06002C1F RID: 11295 RVA: 0x000B61E6 File Offset: 0x000B43E6
		// (set) Token: 0x06002C20 RID: 11296 RVA: 0x000B6200 File Offset: 0x000B4400
		public string[] Keywords
		{
			get
			{
				return ((MultiValuedProperty<string>)this[ADComplianceProgramSchema.Keywords]).ToArray();
			}
			internal set
			{
				MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>();
				for (int i = 0; i < value.Length; i++)
				{
					string item = value[i];
					multiValuedProperty.Add(item);
				}
				this[ADComplianceProgramSchema.Keywords] = multiValuedProperty;
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06002C21 RID: 11297 RVA: 0x000B623A File Offset: 0x000B443A
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x04001A70 RID: 6768
		private static ADComplianceProgramSchema schema = ObjectSchema.GetInstance<ADComplianceProgramSchema>();

		// Token: 0x04001A71 RID: 6769
		private static string mostDerivedClass = "msExchMailflowPolicy";
	}
}
