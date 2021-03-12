using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200050A RID: 1290
	[Serializable]
	public class ForeignConnector : MailGateway
	{
		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x0600391C RID: 14620 RVA: 0x000DC67D File Offset: 0x000DA87D
		// (set) Token: 0x0600391D RID: 14621 RVA: 0x000DC68F File Offset: 0x000DA88F
		[Parameter]
		public string DropDirectory
		{
			get
			{
				return (string)this[ForeignConnectorSchema.DropDirectory];
			}
			set
			{
				this[ForeignConnectorSchema.DropDirectory] = value;
			}
		}

		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x0600391E RID: 14622 RVA: 0x000DC69D File Offset: 0x000DA89D
		// (set) Token: 0x0600391F RID: 14623 RVA: 0x000DC6AF File Offset: 0x000DA8AF
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> DropDirectoryQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ForeignConnectorSchema.DropDirectoryQuota];
			}
			set
			{
				this[ForeignConnectorSchema.DropDirectoryQuota] = value;
			}
		}

		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x06003920 RID: 14624 RVA: 0x000DC6C2 File Offset: 0x000DA8C2
		// (set) Token: 0x06003921 RID: 14625 RVA: 0x000DC6D4 File Offset: 0x000DA8D4
		[Parameter]
		public bool RelayDsnRequired
		{
			get
			{
				return (bool)this[ForeignConnectorSchema.RelayDsnRequired];
			}
			set
			{
				this[ForeignConnectorSchema.RelayDsnRequired] = value;
			}
		}

		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x06003922 RID: 14626 RVA: 0x000DC6E7 File Offset: 0x000DA8E7
		// (set) Token: 0x06003923 RID: 14627 RVA: 0x000DC6FC File Offset: 0x000DA8FC
		[Parameter(Mandatory = false)]
		public override bool Enabled
		{
			get
			{
				return !(bool)this[ForeignConnectorSchema.Disabled];
			}
			set
			{
				this[ForeignConnectorSchema.Disabled] = !value;
			}
		}

		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x06003924 RID: 14628 RVA: 0x000DC712 File Offset: 0x000DA912
		internal override ADObjectSchema Schema
		{
			get
			{
				return ForeignConnector.schema;
			}
		}

		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x06003925 RID: 14629 RVA: 0x000DC71C File Offset: 0x000DA91C
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				ComparisonFilter comparisonFilter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, "mailGateway");
				ExistsFilter existsFilter = new ExistsFilter(ForeignConnectorSchema.DropDirectory);
				return new AndFilter(new QueryFilter[]
				{
					comparisonFilter,
					existsFilter
				});
			}
		}

		// Token: 0x04002701 RID: 9985
		private static ForeignConnectorSchema schema = ObjectSchema.GetInstance<ForeignConnectorSchema>();
	}
}
