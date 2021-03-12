using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000C3 RID: 195
	public sealed class RestrictionProperty : Restriction
	{
		// Token: 0x06000A9D RID: 2717 RVA: 0x0005379D File Offset: 0x0005199D
		public RestrictionProperty(StorePropTag propertyTag, RelationOperator op, object value)
		{
			this.propertyTag = propertyTag;
			this.value = value;
			this.op = op;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x000537BC File Offset: 0x000519BC
		public RestrictionProperty(Context context, byte[] byteForm, ref int position, int posMax, Mailbox mailbox, ObjectType objectType)
		{
			ParseSerialize.GetByte(byteForm, ref position, posMax);
			this.op = (RelationOperator)ParseSerialize.GetByte(byteForm, ref position, posMax);
			this.propertyTag = Mailbox.GetStorePropTag(context, mailbox, ParseSerialize.GetDword(byteForm, ref position, posMax), objectType);
			StorePropTag storePropTag = Mailbox.GetStorePropTag(context, mailbox, ParseSerialize.GetDword(byteForm, ref position, posMax), objectType);
			this.value = Restriction.DeserializeValue(byteForm, ref position, storePropTag);
			Restriction.CheckRelationOperator(this.op);
			if (!Restriction.FSamePropType(storePropTag.PropType, this.propertyTag.PropType))
			{
				throw new StoreException((LID)63480U, ErrorCodeValue.TooComplex);
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x0005385C File Offset: 0x00051A5C
		public StorePropTag PropertyTag
		{
			get
			{
				return this.propertyTag;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x00053864 File Offset: 0x00051A64
		public RelationOperator Operator
		{
			get
			{
				return this.op;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x0005386C File Offset: 0x00051A6C
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00053874 File Offset: 0x00051A74
		internal override void AppendToString(StringBuilder sb)
		{
			sb.Append("PROPERTY([");
			this.propertyTag.AppendToString(sb);
			sb.Append("], ");
			sb.Append(this.op);
			sb.Append(", ");
			sb.AppendAsString(this.value);
			sb.Append(')');
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x000538DC File Offset: 0x00051ADC
		public override void Serialize(byte[] byteForm, ref int position)
		{
			ParseSerialize.SetByte(byteForm, ref position, 4);
			ParseSerialize.SetByte(byteForm, ref position, (byte)this.op);
			ParseSerialize.SetDword(byteForm, ref position, this.propertyTag.ExternalTag);
			StorePropTag propTag = this.propertyTag;
			if (propTag.IsMultiValued && (this.value == null || !this.value.GetType().IsArray || this.value is byte[]))
			{
				propTag = StorePropTag.CreateWithoutInfo(this.propertyTag.PropId, this.propertyTag.PropType & (PropertyType)61439, this.propertyTag.BaseObjectType);
			}
			else if (!propTag.IsMultiValued && this.value != null && this.value.GetType().IsArray && !(this.value is byte[]))
			{
				propTag = StorePropTag.CreateWithoutInfo(this.propertyTag.PropId, this.propertyTag.PropType | PropertyType.MVFlag, this.propertyTag.BaseObjectType);
			}
			ParseSerialize.SetDword(byteForm, ref position, propTag.ExternalTag);
			Restriction.SerializeValue(byteForm, ref position, propTag, this.value);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00053A0C File Offset: 0x00051C0C
		public override SearchCriteria ToSearchCriteria(StoreDatabase database, ObjectType objectType)
		{
			Column lhs = PropertySchema.MapToColumn(database, objectType, this.propertyTag);
			Column rhs = Factory.CreateConstantColumn(this.value);
			SearchCriteriaCompare.SearchRelOp searchRelOp = Restriction.GetSearchRelOp(this.op);
			return Factory.CreateSearchCriteriaCompare(lhs, searchRelOp, rhs);
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00053A47 File Offset: 0x00051C47
		public override bool RefersToProperty(StorePropTag propTag)
		{
			return this.propertyTag == propTag;
		}

		// Token: 0x040004F5 RID: 1269
		private readonly StorePropTag propertyTag;

		// Token: 0x040004F6 RID: 1270
		private readonly RelationOperator op;

		// Token: 0x040004F7 RID: 1271
		private readonly object value;
	}
}
