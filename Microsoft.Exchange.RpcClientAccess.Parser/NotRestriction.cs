using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000210 RID: 528
	internal sealed class NotRestriction : SingleRestriction
	{
		// Token: 0x06000B78 RID: 2936 RVA: 0x00024935 File Offset: 0x00022B35
		internal NotRestriction(Restriction childRestriction) : base(childRestriction)
		{
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x0002493E File Offset: 0x00022B3E
		internal override RestrictionType RestrictionType
		{
			get
			{
				return RestrictionType.Not;
			}
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00024944 File Offset: 0x00022B44
		internal new static NotRestriction InternalParse(Reader reader, WireFormatStyle wireFormatStyle, uint depth)
		{
			Restriction childRestriction = Restriction.InternalParse(reader, wireFormatStyle, depth);
			return new NotRestriction(childRestriction);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x00024960 File Offset: 0x00022B60
		public override void Serialize(Writer writer, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			base.Serialize(writer, string8Encoding, wireFormatStyle);
			base.ChildRestriction.Serialize(writer, string8Encoding, wireFormatStyle);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x00024979 File Offset: 0x00022B79
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" [");
			if (base.ChildRestriction != null)
			{
				stringBuilder.Append(base.ChildRestriction);
			}
			stringBuilder.Append("]");
		}
	}
}
