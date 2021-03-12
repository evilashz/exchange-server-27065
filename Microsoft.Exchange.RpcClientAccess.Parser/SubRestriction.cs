using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000213 RID: 531
	internal sealed class SubRestriction : SingleRestriction
	{
		// Token: 0x06000B83 RID: 2947 RVA: 0x00024A78 File Offset: 0x00022C78
		internal SubRestriction(SubRestrictionType subRestrictionType, Restriction childRestriction) : base(childRestriction)
		{
			this.subRestrictionType = subRestrictionType;
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x00024A88 File Offset: 0x00022C88
		internal override RestrictionType RestrictionType
		{
			get
			{
				return RestrictionType.SubRestriction;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x00024A8C File Offset: 0x00022C8C
		public SubRestrictionType SubRestrictionType
		{
			get
			{
				return this.subRestrictionType;
			}
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00024A94 File Offset: 0x00022C94
		internal new static SubRestriction InternalParse(Reader reader, WireFormatStyle wireFormatStyle, uint depth)
		{
			SubRestrictionType subRestrictionType = (SubRestrictionType)reader.ReadUInt32();
			Restriction childRestriction;
			if (wireFormatStyle == WireFormatStyle.Nspi && !reader.ReadBool())
			{
				childRestriction = null;
			}
			else
			{
				childRestriction = Restriction.InternalParse(reader, wireFormatStyle, depth);
			}
			return new SubRestriction(subRestrictionType, childRestriction);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00024AC8 File Offset: 0x00022CC8
		public override void Serialize(Writer writer, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			base.Serialize(writer, string8Encoding, wireFormatStyle);
			writer.WriteUInt32((uint)this.subRestrictionType);
			if (wireFormatStyle == WireFormatStyle.Nspi)
			{
				bool flag = base.ChildRestriction != null;
				writer.WriteBool(flag);
				if (flag)
				{
					base.ChildRestriction.Serialize(writer, string8Encoding, wireFormatStyle);
					return;
				}
			}
			else
			{
				base.ChildRestriction.Serialize(writer, string8Encoding, wireFormatStyle);
			}
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00024B22 File Offset: 0x00022D22
		public override ErrorCode Validate()
		{
			if (this.subRestrictionType != SubRestrictionType.Recipients && this.subRestrictionType != SubRestrictionType.Attachments)
			{
				return (ErrorCode)2147746050U;
			}
			return base.Validate();
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00024B4A File Offset: 0x00022D4A
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

		// Token: 0x0400068A RID: 1674
		private readonly SubRestrictionType subRestrictionType;
	}
}
