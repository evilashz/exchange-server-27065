using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000BC RID: 188
	public sealed class RestrictionCompareProps : Restriction
	{
		// Token: 0x06000A61 RID: 2657 RVA: 0x00052D0C File Offset: 0x00050F0C
		public RestrictionCompareProps(StorePropTag propertyTag1, RelationOperator op, StorePropTag propertyTag2)
		{
			this.propertyTag1 = propertyTag1;
			this.op = op;
			this.propertyTag2 = propertyTag2;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00052D2C File Offset: 0x00050F2C
		public RestrictionCompareProps(Context context, byte[] byteForm, ref int position, int posMax, Mailbox mailbox, ObjectType objectType)
		{
			ParseSerialize.GetByte(byteForm, ref position, posMax);
			this.op = (RelationOperator)ParseSerialize.GetByte(byteForm, ref position, posMax);
			this.propertyTag1 = Mailbox.GetStorePropTag(context, mailbox, ParseSerialize.GetDword(byteForm, ref position, posMax), objectType);
			this.propertyTag2 = Mailbox.GetStorePropTag(context, mailbox, ParseSerialize.GetDword(byteForm, ref position, posMax), objectType);
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x00052D8A File Offset: 0x00050F8A
		public StorePropTag PropertyTag1
		{
			get
			{
				return this.propertyTag1;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x00052D92 File Offset: 0x00050F92
		public StorePropTag PropertyTag2
		{
			get
			{
				return this.propertyTag2;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x00052D9A File Offset: 0x00050F9A
		public RelationOperator Operator
		{
			get
			{
				return this.op;
			}
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00052DA4 File Offset: 0x00050FA4
		internal override void AppendToString(StringBuilder sb)
		{
			sb.Append("COMPAREPROPS([");
			this.propertyTag1.AppendToString(sb);
			sb.Append("], ");
			sb.Append(this.op);
			sb.Append(", [");
			this.propertyTag2.AppendToString(sb);
			sb.Append("])");
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00052E14 File Offset: 0x00051014
		public override void Serialize(byte[] byteForm, ref int position)
		{
			ParseSerialize.SetByte(byteForm, ref position, 5);
			ParseSerialize.SetByte(byteForm, ref position, (byte)this.op);
			ParseSerialize.SetDword(byteForm, ref position, this.propertyTag1.ExternalTag);
			ParseSerialize.SetDword(byteForm, ref position, this.propertyTag2.ExternalTag);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00052E60 File Offset: 0x00051060
		public override SearchCriteria ToSearchCriteria(StoreDatabase database, ObjectType objectType)
		{
			Column lhs = PropertySchema.MapToColumn(database, objectType, this.propertyTag1);
			Column rhs = PropertySchema.MapToColumn(database, objectType, this.propertyTag2);
			SearchCriteriaCompare.SearchRelOp searchRelOp = Restriction.GetSearchRelOp(this.op);
			return Factory.CreateSearchCriteriaCompare(lhs, searchRelOp, rhs);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00052E9D File Offset: 0x0005109D
		public override bool RefersToProperty(StorePropTag propTag)
		{
			return this.propertyTag1 == propTag || this.propertyTag2 == propTag;
		}

		// Token: 0x040004EA RID: 1258
		private readonly StorePropTag propertyTag1;

		// Token: 0x040004EB RID: 1259
		private readonly StorePropTag propertyTag2;

		// Token: 0x040004EC RID: 1260
		private readonly RelationOperator op;
	}
}
