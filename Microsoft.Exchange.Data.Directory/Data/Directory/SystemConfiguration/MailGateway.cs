using System;
using System.Management.Automation;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003F5 RID: 1013
	[Serializable]
	public class MailGateway : SendConnector
	{
		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06002E4B RID: 11851 RVA: 0x000BC709 File Offset: 0x000BA909
		// (set) Token: 0x06002E4C RID: 11852 RVA: 0x000BC71B File Offset: 0x000BA91B
		[Parameter]
		public MultiValuedProperty<AddressSpace> AddressSpaces
		{
			get
			{
				return (MultiValuedProperty<AddressSpace>)this[MailGatewaySchema.AddressSpaces];
			}
			set
			{
				this[MailGatewaySchema.AddressSpaces] = value;
			}
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06002E4D RID: 11853 RVA: 0x000BC729 File Offset: 0x000BA929
		public MultiValuedProperty<ConnectedDomain> ConnectedDomains
		{
			get
			{
				return (MultiValuedProperty<ConnectedDomain>)this[MailGatewaySchema.ConnectedDomains];
			}
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06002E4E RID: 11854 RVA: 0x000BC73B File Offset: 0x000BA93B
		// (set) Token: 0x06002E4F RID: 11855 RVA: 0x000BC73E File Offset: 0x000BA93E
		public virtual bool Enabled
		{
			get
			{
				return true;
			}
			set
			{
				throw new NotSupportedException("This property cannot be set for this connector type");
			}
		}

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x06002E50 RID: 11856 RVA: 0x000BC74A File Offset: 0x000BA94A
		// (set) Token: 0x06002E51 RID: 11857 RVA: 0x000BC752 File Offset: 0x000BA952
		[Parameter(Mandatory = false)]
		public bool IsScopedConnector
		{
			get
			{
				return this.GetScopedConnector();
			}
			set
			{
				this[MailGatewaySchema.IsScopedConnector] = value;
			}
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x06002E52 RID: 11858 RVA: 0x000BC765 File Offset: 0x000BA965
		public bool IsSmtpConnector
		{
			get
			{
				return (bool)this[MailGatewaySchema.IsSmtpConnector];
			}
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06002E53 RID: 11859 RVA: 0x000BC777 File Offset: 0x000BA977
		// (set) Token: 0x06002E54 RID: 11860 RVA: 0x000BC789 File Offset: 0x000BA989
		[Parameter]
		public string Comment
		{
			get
			{
				return (string)this[MailGatewaySchema.Comment];
			}
			set
			{
				this[MailGatewaySchema.Comment] = value;
			}
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06002E55 RID: 11861 RVA: 0x000BC797 File Offset: 0x000BA997
		internal override ADObjectSchema Schema
		{
			get
			{
				return ObjectSchema.GetInstance<MailGateway.AllMailGatewayProperties>();
			}
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06002E56 RID: 11862 RVA: 0x000BC79E File Offset: 0x000BA99E
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "mailGateway";
			}
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06002E57 RID: 11863 RVA: 0x000BC7A5 File Offset: 0x000BA9A5
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, "mailGateway");
			}
		}

		// Token: 0x06002E58 RID: 11864 RVA: 0x000BC7B8 File Offset: 0x000BA9B8
		internal static object IsSmtpConnectorGetter(IPropertyBag propertyBag)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[ADObjectSchema.ObjectClass];
			return multiValuedProperty.Contains(SmtpSendConnectorConfig.MostDerivedClass);
		}

		// Token: 0x06002E59 RID: 11865 RVA: 0x000BC7E8 File Offset: 0x000BA9E8
		internal bool GetScopedConnector()
		{
			if (this[MailGatewaySchema.IsScopedConnector] == null)
			{
				MultiValuedProperty<AddressSpace> addressSpaces = this.AddressSpaces;
				using (MultiValuedProperty<AddressSpace>.Enumerator enumerator = addressSpaces.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						AddressSpace addressSpace = enumerator.Current;
						return addressSpace.IsLocal;
					}
				}
				return false;
			}
			return (bool)this[MailGatewaySchema.IsScopedConnector];
		}

		// Token: 0x04001F19 RID: 7961
		public const string MostDerivedClass = "mailGateway";

		// Token: 0x04001F1A RID: 7962
		private const string LocalScopePrefix = "Local:";

		// Token: 0x020003F6 RID: 1014
		private class AllMailGatewayProperties : ADPropertyUnionSchema
		{
			// Token: 0x17000D11 RID: 3345
			// (get) Token: 0x06002E5A RID: 11866 RVA: 0x000BC860 File Offset: 0x000BAA60
			public override ReadOnlyCollection<ADObjectSchema> ObjectSchemas
			{
				get
				{
					return MailGateway.AllMailGatewayProperties.mailGatewaySchema;
				}
			}

			// Token: 0x04001F1B RID: 7963
			private static ReadOnlyCollection<ADObjectSchema> mailGatewaySchema = new ReadOnlyCollection<ADObjectSchema>(new ADObjectSchema[]
			{
				ObjectSchema.GetInstance<SmtpSendConnectorConfigSchema>(),
				ObjectSchema.GetInstance<LegacyGatewayConnectorSchema>(),
				ObjectSchema.GetInstance<ForeignConnectorSchema>(),
				ObjectSchema.GetInstance<DeliveryAgentConnectorSchema>()
			});
		}
	}
}
