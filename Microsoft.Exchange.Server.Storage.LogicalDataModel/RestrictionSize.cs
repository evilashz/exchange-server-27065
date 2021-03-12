using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000C4 RID: 196
	public sealed class RestrictionSize : Restriction
	{
		// Token: 0x06000AA6 RID: 2726 RVA: 0x00053A55 File Offset: 0x00051C55
		public RestrictionSize(StorePropTag propertyTag, RelationOperator op, int value)
		{
			this.propertyTag = propertyTag;
			this.op = op;
			this.value = value;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00053A74 File Offset: 0x00051C74
		public RestrictionSize(Context context, byte[] byteForm, ref int position, int posMax, Mailbox mailbox, ObjectType objectType)
		{
			ParseSerialize.GetByte(byteForm, ref position, posMax);
			this.op = (RelationOperator)ParseSerialize.GetByte(byteForm, ref position, posMax);
			this.propertyTag = Mailbox.GetStorePropTag(context, mailbox, ParseSerialize.GetDword(byteForm, ref position, posMax), objectType);
			this.value = (int)ParseSerialize.GetDword(byteForm, ref position, posMax);
			Restriction.CheckRelationOperator(this.op);
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x00053AD3 File Offset: 0x00051CD3
		public StorePropTag PropertyTag
		{
			get
			{
				return this.propertyTag;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x00053ADB File Offset: 0x00051CDB
		public RelationOperator Operator
		{
			get
			{
				return this.op;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x00053AE3 File Offset: 0x00051CE3
		public int Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00053AEC File Offset: 0x00051CEC
		internal override void AppendToString(StringBuilder sb)
		{
			sb.Append("SIZE([");
			this.propertyTag.AppendToString(sb);
			sb.Append("], ");
			sb.Append(this.op);
			sb.Append(", ");
			sb.Append(this.value.ToString());
			sb.Append(')');
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00053B5C File Offset: 0x00051D5C
		public override void Serialize(byte[] byteForm, ref int position)
		{
			ParseSerialize.SetByte(byteForm, ref position, 7);
			ParseSerialize.SetByte(byteForm, ref position, (byte)this.op);
			ParseSerialize.SetDword(byteForm, ref position, this.propertyTag.ExternalTag);
			ParseSerialize.SetDword(byteForm, ref position, this.value);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00053BA0 File Offset: 0x00051DA0
		public override SearchCriteria ToSearchCriteria(StoreDatabase database, ObjectType objectType)
		{
			Column termColumn = PropertySchema.MapToColumn(database, objectType, this.propertyTag);
			Column lhs = Factory.CreateSizeOfColumn(termColumn);
			Column rhs = Factory.CreateConstantColumn(this.value);
			SearchCriteriaCompare.SearchRelOp searchRelOp = Restriction.GetSearchRelOp(this.op);
			return Factory.CreateSearchCriteriaCompare(lhs, searchRelOp, rhs);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00053BE7 File Offset: 0x00051DE7
		public override bool RefersToProperty(StorePropTag propTag)
		{
			return this.propertyTag == propTag;
		}

		// Token: 0x040004F8 RID: 1272
		private readonly StorePropTag propertyTag;

		// Token: 0x040004F9 RID: 1273
		private readonly int value;

		// Token: 0x040004FA RID: 1274
		private readonly RelationOperator op;
	}
}
