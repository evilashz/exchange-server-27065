using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000BD RID: 189
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	internal class ADLink : ADObject
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x00014365 File Offset: 0x00012565
		public override ObjectId Identity
		{
			get
			{
				return base.Id;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x0001436D File Offset: 0x0001256D
		// (set) Token: 0x06000624 RID: 1572 RVA: 0x0001437F File Offset: 0x0001257F
		internal ADObjectId SourceId
		{
			get
			{
				return this[ADLinkSchema.SourceIdProperty] as ADObjectId;
			}
			set
			{
				this[ADLinkSchema.SourceIdProperty] = value;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0001438D File Offset: 0x0001258D
		// (set) Token: 0x06000626 RID: 1574 RVA: 0x0001439F File Offset: 0x0001259F
		internal ADObjectId DestinationId
		{
			get
			{
				return this[ADLinkSchema.DestinationIdProperty] as ADObjectId;
			}
			set
			{
				this[ADLinkSchema.DestinationIdProperty] = value;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x000143AD File Offset: 0x000125AD
		// (set) Token: 0x06000628 RID: 1576 RVA: 0x000143BF File Offset: 0x000125BF
		internal DirectoryObjectClass SourceType
		{
			get
			{
				return (DirectoryObjectClass)this[ADLinkSchema.SourceTypeProperty];
			}
			set
			{
				this[ADLinkSchema.SourceTypeProperty] = value;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x000143D2 File Offset: 0x000125D2
		// (set) Token: 0x0600062A RID: 1578 RVA: 0x000143E4 File Offset: 0x000125E4
		internal DirectoryObjectClass DestinationType
		{
			get
			{
				return (DirectoryObjectClass)this[ADLinkSchema.DestinationTypeProperty];
			}
			set
			{
				this[ADLinkSchema.DestinationTypeProperty] = value;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x000143F7 File Offset: 0x000125F7
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x00014409 File Offset: 0x00012609
		internal LinkType LinkType
		{
			get
			{
				return (LinkType)this[ADLinkSchema.LinkTypeProperty];
			}
			set
			{
				this[ADLinkSchema.LinkTypeProperty] = value;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x0001441C File Offset: 0x0001261C
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADLink.schema;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x00014423 File Offset: 0x00012623
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADLink.mostDerivedClass;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x0001442A File Offset: 0x0001262A
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x040003D5 RID: 981
		private static readonly ADLinkSchema schema = ObjectSchema.GetInstance<ADLinkSchema>();

		// Token: 0x040003D6 RID: 982
		private static string mostDerivedClass = "ADLink";
	}
}
