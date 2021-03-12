using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000BA RID: 186
	public sealed class RestrictionBitmask : Restriction
	{
		// Token: 0x06000A4C RID: 2636 RVA: 0x000527C5 File Offset: 0x000509C5
		public RestrictionBitmask(StorePropTag propertyTag, long mask, BitmaskOperation operation)
		{
			RestrictionBitmask.ValidateOperation(operation);
			RestrictionBitmask.ValidatePropertyTag(propertyTag);
			this.propertyTag = propertyTag;
			this.mask = mask;
			this.operation = operation;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x000527F0 File Offset: 0x000509F0
		public RestrictionBitmask(Context context, byte[] byteForm, ref int position, int posMax, Mailbox mailbox, ObjectType objectType)
		{
			ParseSerialize.GetByte(byteForm, ref position, posMax);
			this.operation = (BitmaskOperation)ParseSerialize.GetByte(byteForm, ref position, posMax);
			this.propertyTag = Mailbox.GetStorePropTag(context, mailbox, ParseSerialize.GetDword(byteForm, ref position, posMax), objectType);
			this.mask = (long)((ulong)ParseSerialize.GetDword(byteForm, ref position, posMax));
			RestrictionBitmask.ValidateOperation(this.operation);
			RestrictionBitmask.ValidatePropertyTag(this.propertyTag);
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x0005285B File Offset: 0x00050A5B
		public StorePropTag PropertyTag
		{
			get
			{
				return this.propertyTag;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x00052863 File Offset: 0x00050A63
		public BitmaskOperation Operation
		{
			get
			{
				return this.operation;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x0005286B File Offset: 0x00050A6B
		public long Mask
		{
			get
			{
				return this.mask;
			}
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00052874 File Offset: 0x00050A74
		internal override void AppendToString(StringBuilder sb)
		{
			sb.Append("BITMASK([");
			this.propertyTag.AppendToString(sb);
			sb.Append("] & 0x");
			sb.Append(this.mask.ToString("X16"));
			if (this.operation == BitmaskOperation.EqualToZero)
			{
				sb.Append(" = 0");
			}
			else
			{
				sb.Append(" <> 0");
			}
			sb.Append(")");
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x000528F0 File Offset: 0x00050AF0
		public override void Serialize(byte[] byteForm, ref int position)
		{
			ParseSerialize.SetByte(byteForm, ref position, 6);
			ParseSerialize.SetByte(byteForm, ref position, (byte)this.operation);
			ParseSerialize.SetDword(byteForm, ref position, this.propertyTag.ExternalTag);
			ParseSerialize.SetDword(byteForm, ref position, (int)this.mask);
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00052938 File Offset: 0x00050B38
		public override SearchCriteria ToSearchCriteria(StoreDatabase database, ObjectType objectType)
		{
			object value;
			if (this.propertyTag.PropType == PropertyType.Int16)
			{
				value = (short)this.mask;
			}
			else if (this.propertyTag.PropType == PropertyType.Int32)
			{
				value = (int)this.mask;
			}
			else
			{
				if (this.propertyTag.PropType != PropertyType.Int64)
				{
					return Factory.CreateSearchCriteriaFalse();
				}
				value = this.mask;
			}
			Column lhs = PropertySchema.MapToColumn(database, objectType, this.propertyTag);
			Column rhs = Factory.CreateConstantColumn(value);
			SearchCriteriaBitMask.SearchBitMaskOp op = (this.operation == BitmaskOperation.EqualToZero) ? SearchCriteriaBitMask.SearchBitMaskOp.EqualToZero : SearchCriteriaBitMask.SearchBitMaskOp.NotEqualToZero;
			return Factory.CreateSearchCriteriaBitMask(lhs, rhs, op);
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x000529D8 File Offset: 0x00050BD8
		public override bool RefersToProperty(StorePropTag propTag)
		{
			return this.propertyTag == propTag;
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x000529E6 File Offset: 0x00050BE6
		private static void ValidateOperation(BitmaskOperation operation)
		{
			if (operation != BitmaskOperation.EqualToZero && operation != BitmaskOperation.NotEqualToZero)
			{
				throw new StoreException((LID)33272U, ErrorCodeValue.TooComplex);
			}
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00052A04 File Offset: 0x00050C04
		private static void ValidatePropertyTag(StorePropTag propertyTag)
		{
			PropertyType propType = propertyTag.PropType;
			if (propType != PropertyType.Int16 && propType != PropertyType.Int32 && propType != PropertyType.Int64)
			{
				throw new StoreException((LID)57848U, ErrorCodeValue.TooComplex);
			}
		}

		// Token: 0x040004E4 RID: 1252
		private readonly StorePropTag propertyTag;

		// Token: 0x040004E5 RID: 1253
		private readonly long mask;

		// Token: 0x040004E6 RID: 1254
		private readonly BitmaskOperation operation;
	}
}
