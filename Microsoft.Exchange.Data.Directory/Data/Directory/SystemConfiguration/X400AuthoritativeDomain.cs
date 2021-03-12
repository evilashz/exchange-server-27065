using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200061E RID: 1566
	[Serializable]
	public class X400AuthoritativeDomain : ADConfigurationObject
	{
		// Token: 0x06004A3B RID: 19003 RVA: 0x001127A9 File Offset: 0x001109A9
		public X400AuthoritativeDomain()
		{
			this[AcceptedDomainSchema.X400AddressType] = true;
		}

		// Token: 0x170018A1 RID: 6305
		// (get) Token: 0x06004A3C RID: 19004 RVA: 0x001127C2 File Offset: 0x001109C2
		// (set) Token: 0x06004A3D RID: 19005 RVA: 0x001127D4 File Offset: 0x001109D4
		[Parameter]
		public X400Domain X400DomainName
		{
			get
			{
				return (X400Domain)this[X400AuthoritativeDomainSchema.DomainName];
			}
			set
			{
				this[X400AuthoritativeDomainSchema.DomainName] = value;
			}
		}

		// Token: 0x170018A2 RID: 6306
		// (get) Token: 0x06004A3E RID: 19006 RVA: 0x001127E2 File Offset: 0x001109E2
		// (set) Token: 0x06004A3F RID: 19007 RVA: 0x001127F7 File Offset: 0x001109F7
		[Parameter]
		public bool X400ExternalRelay
		{
			get
			{
				return 1 == (int)this[AcceptedDomainSchema.AcceptedDomainType];
			}
			set
			{
				this[AcceptedDomainSchema.AcceptedDomainType] = (value ? 1 : 0);
			}
		}

		// Token: 0x170018A3 RID: 6307
		// (get) Token: 0x06004A40 RID: 19008 RVA: 0x00112810 File Offset: 0x00110A10
		internal override ADObjectSchema Schema
		{
			get
			{
				return X400AuthoritativeDomain.SchemaObject;
			}
		}

		// Token: 0x170018A4 RID: 6308
		// (get) Token: 0x06004A41 RID: 19009 RVA: 0x00112817 File Offset: 0x00110A17
		internal override ADObjectId ParentPath
		{
			get
			{
				return AcceptedDomain.AcceptedDomainContainer;
			}
		}

		// Token: 0x170018A5 RID: 6309
		// (get) Token: 0x06004A42 RID: 19010 RVA: 0x0011281E File Offset: 0x00110A1E
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchAcceptedDomain";
			}
		}

		// Token: 0x170018A6 RID: 6310
		// (get) Token: 0x06004A43 RID: 19011 RVA: 0x00112828 File Offset: 0x00110A28
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new AndFilter(new QueryFilter[]
				{
					base.ImplicitFilter,
					X400AuthoritativeDomain.X400DomainsFilter
				});
			}
		}

		// Token: 0x0400335C RID: 13148
		private static readonly X400AuthoritativeDomainSchema SchemaObject = ObjectSchema.GetInstance<X400AuthoritativeDomainSchema>();

		// Token: 0x0400335D RID: 13149
		private static readonly QueryFilter AuthoritativeFilter = new ComparisonFilter(ComparisonOperator.Equal, AcceptedDomainSchema.AcceptedDomainFlags, 16);

		// Token: 0x0400335E RID: 13150
		private static readonly QueryFilter RelayFilter = new ComparisonFilter(ComparisonOperator.Equal, AcceptedDomainSchema.AcceptedDomainFlags, 17);

		// Token: 0x0400335F RID: 13151
		private static readonly QueryFilter X400DomainsFilter = new OrFilter(new QueryFilter[]
		{
			X400AuthoritativeDomain.AuthoritativeFilter,
			X400AuthoritativeDomain.RelayFilter
		});

		// Token: 0x04003360 RID: 13152
		internal static readonly QueryFilter NonX400DomainsFilter = new NotFilter(X400AuthoritativeDomain.X400DomainsFilter);
	}
}
