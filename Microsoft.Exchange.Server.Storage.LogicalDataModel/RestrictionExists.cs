using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000BE RID: 190
	public sealed class RestrictionExists : Restriction
	{
		// Token: 0x06000A74 RID: 2676 RVA: 0x0005302A File Offset: 0x0005122A
		public RestrictionExists(StorePropTag propertyTag)
		{
			this.propertyTag = propertyTag;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00053039 File Offset: 0x00051239
		public RestrictionExists(Context context, byte[] byteForm, ref int position, int posMax, Mailbox mailbox, ObjectType objectType)
		{
			ParseSerialize.GetByte(byteForm, ref position, posMax);
			this.propertyTag = Mailbox.GetStorePropTag(context, mailbox, ParseSerialize.GetDword(byteForm, ref position, posMax), objectType);
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x00053064 File Offset: 0x00051264
		public StorePropTag PropertyTag
		{
			get
			{
				return this.propertyTag;
			}
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0005306C File Offset: 0x0005126C
		internal override void AppendToString(StringBuilder sb)
		{
			sb.Append("EXISTS([");
			this.propertyTag.AppendToString(sb);
			sb.Append("])");
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x000530A0 File Offset: 0x000512A0
		public override void Serialize(byte[] byteForm, ref int position)
		{
			ParseSerialize.SetByte(byteForm, ref position, 8);
			ParseSerialize.SetDword(byteForm, ref position, this.propertyTag.ExternalTag);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x000530CC File Offset: 0x000512CC
		public override SearchCriteria ToSearchCriteria(StoreDatabase database, ObjectType objectType)
		{
			Column column = PropertySchema.MapToColumn(database, objectType, this.propertyTag);
			if (!column.IsNullable)
			{
				return Factory.CreateSearchCriteriaTrue();
			}
			return Factory.CreateSearchCriteriaCompare(column, SearchCriteriaCompare.SearchRelOp.NotEqual, Factory.CreateConstantColumn(null, column));
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00053103 File Offset: 0x00051303
		public override bool RefersToProperty(StorePropTag propTag)
		{
			return this.propertyTag == propTag;
		}

		// Token: 0x040004EF RID: 1263
		private readonly StorePropTag propertyTag;
	}
}
