using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000211 RID: 529
	internal sealed class CountRestriction : SingleRestriction
	{
		// Token: 0x06000B7D RID: 2941 RVA: 0x000249AF File Offset: 0x00022BAF
		internal CountRestriction(uint count, Restriction childRestriction) : base(childRestriction)
		{
			this.count = count;
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x000249BF File Offset: 0x00022BBF
		internal override RestrictionType RestrictionType
		{
			get
			{
				return RestrictionType.Count;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x000249C3 File Offset: 0x00022BC3
		public uint Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x000249CC File Offset: 0x00022BCC
		internal new static CountRestriction InternalParse(Reader reader, WireFormatStyle wireFormatStyle, uint depth)
		{
			uint num = reader.ReadUInt32();
			Restriction childRestriction = Restriction.InternalParse(reader, wireFormatStyle, depth);
			return new CountRestriction(num, childRestriction);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x000249F0 File Offset: 0x00022BF0
		public override void Serialize(Writer writer, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			base.Serialize(writer, string8Encoding, wireFormatStyle);
			writer.WriteUInt32(this.count);
			base.ChildRestriction.Serialize(writer, string8Encoding, wireFormatStyle);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00024A18 File Offset: 0x00022C18
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" [Count=").Append(this.Count);
			if (base.ChildRestriction != null)
			{
				stringBuilder.Append(" Child=[").Append(base.ChildRestriction).Append("]");
			}
			stringBuilder.Append("]");
		}

		// Token: 0x04000686 RID: 1670
		private readonly uint count;
	}
}
