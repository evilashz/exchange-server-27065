using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200020A RID: 522
	internal sealed class NearRestriction : SingleRestriction
	{
		// Token: 0x06000B57 RID: 2903 RVA: 0x000243AE File Offset: 0x000225AE
		internal NearRestriction(uint distance, bool ordered, Restriction childRestriction) : base(childRestriction)
		{
			this.distance = distance;
			this.ordered = ordered;
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x000243C5 File Offset: 0x000225C5
		internal override RestrictionType RestrictionType
		{
			get
			{
				return RestrictionType.Near;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x000243C9 File Offset: 0x000225C9
		public uint Distance
		{
			get
			{
				return this.distance;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x000243D1 File Offset: 0x000225D1
		public bool Ordered
		{
			get
			{
				return this.ordered;
			}
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x000243DC File Offset: 0x000225DC
		internal new static NearRestriction InternalParse(Reader reader, WireFormatStyle wireFormatStyle, uint depth)
		{
			uint num = reader.ReadUInt32();
			bool flag = reader.ReadUInt32() == 1U;
			Restriction childRestriction = Restriction.InternalParse(reader, wireFormatStyle, depth);
			return new NearRestriction(num, flag, childRestriction);
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0002440C File Offset: 0x0002260C
		public override ErrorCode Validate()
		{
			AndRestriction andRestriction = base.ChildRestriction as AndRestriction;
			if (andRestriction == null)
			{
				return (ErrorCode)2147942487U;
			}
			if (andRestriction.ChildRestrictions == null || andRestriction.ChildRestrictions.Length < 2)
			{
				return (ErrorCode)2147942487U;
			}
			if (!NearRestriction.ValidateNestedRestriction(andRestriction.ChildRestrictions))
			{
				return (ErrorCode)2147746071U;
			}
			return base.Validate();
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x00024460 File Offset: 0x00022660
		private static bool ValidateNestedRestriction(Restriction[] nestedRestrictions)
		{
			if (nestedRestrictions == null || nestedRestrictions.Length == 0)
			{
				return false;
			}
			foreach (Restriction restriction in nestedRestrictions)
			{
				if (restriction is OrRestriction)
				{
					if (!NearRestriction.ValidateNestedRestriction(((OrRestriction)restriction).ChildRestrictions))
					{
						return false;
					}
				}
				else if (!(restriction is ContentRestriction))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x000244B8 File Offset: 0x000226B8
		public override void Serialize(Writer writer, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			base.Serialize(writer, string8Encoding, wireFormatStyle);
			writer.WriteUInt32(this.distance);
			writer.WriteUInt32(this.ordered ? 1U : 0U);
			base.ChildRestriction.Serialize(writer, string8Encoding, wireFormatStyle);
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x000244F0 File Offset: 0x000226F0
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" [Distance=").Append(this.Distance);
			stringBuilder.Append(", Ordered=").Append(this.Ordered);
			if (base.ChildRestriction != null)
			{
				stringBuilder.Append(" Child=[").Append(base.ChildRestriction).Append("]");
			}
			stringBuilder.Append("]");
		}

		// Token: 0x04000671 RID: 1649
		private readonly uint distance;

		// Token: 0x04000672 RID: 1650
		private readonly bool ordered;
	}
}
