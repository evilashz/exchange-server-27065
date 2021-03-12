using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Sharing
{
	// Token: 0x02000A01 RID: 2561
	[Serializable]
	public sealed class TestOrganizationRelationshipResult : ConfigurableObject
	{
		// Token: 0x06005BD9 RID: 23513 RVA: 0x00183E01 File Offset: 0x00182001
		public TestOrganizationRelationshipResult() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x17001B7B RID: 7035
		// (get) Token: 0x06005BDA RID: 23514 RVA: 0x00183E0E File Offset: 0x0018200E
		public override ObjectId Identity
		{
			get
			{
				return this.propertyBag[SimpleProviderObjectSchema.Identity] as ObjectId;
			}
		}

		// Token: 0x17001B7C RID: 7036
		// (get) Token: 0x06005BDB RID: 23515 RVA: 0x00183E25 File Offset: 0x00182025
		// (set) Token: 0x06005BDC RID: 23516 RVA: 0x00183E3C File Offset: 0x0018203C
		public TestOrganizationRelationshipResultId Id
		{
			get
			{
				return (TestOrganizationRelationshipResultId)this.propertyBag[TestOrganizationRelationshipResultSchema.Id];
			}
			set
			{
				this.propertyBag[TestOrganizationRelationshipResultSchema.Id] = value;
			}
		}

		// Token: 0x17001B7D RID: 7037
		// (get) Token: 0x06005BDD RID: 23517 RVA: 0x00183E54 File Offset: 0x00182054
		// (set) Token: 0x06005BDE RID: 23518 RVA: 0x00183E6B File Offset: 0x0018206B
		public string Status
		{
			get
			{
				return this.propertyBag[TestOrganizationRelationshipResultSchema.Status] as string;
			}
			set
			{
				this.propertyBag[TestOrganizationRelationshipResultSchema.Status] = value;
			}
		}

		// Token: 0x17001B7E RID: 7038
		// (get) Token: 0x06005BDF RID: 23519 RVA: 0x00183E7E File Offset: 0x0018207E
		// (set) Token: 0x06005BE0 RID: 23520 RVA: 0x00183E95 File Offset: 0x00182095
		public LocalizedString Description
		{
			get
			{
				return (LocalizedString)this.propertyBag[TestOrganizationRelationshipResultSchema.Description];
			}
			set
			{
				this.propertyBag[TestOrganizationRelationshipResultSchema.Description] = value;
			}
		}

		// Token: 0x17001B7F RID: 7039
		// (get) Token: 0x06005BE1 RID: 23521 RVA: 0x00183EAD File Offset: 0x001820AD
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return TestOrganizationRelationshipResult.schema;
			}
		}

		// Token: 0x17001B80 RID: 7040
		// (get) Token: 0x06005BE2 RID: 23522 RVA: 0x00183EB4 File Offset: 0x001820B4
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04003442 RID: 13378
		private static ObjectSchema schema = ObjectSchema.GetInstance<TestOrganizationRelationshipResultSchema>();
	}
}
