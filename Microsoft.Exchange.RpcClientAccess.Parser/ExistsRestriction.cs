using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200020B RID: 523
	internal sealed class ExistsRestriction : Restriction
	{
		// Token: 0x06000B60 RID: 2912 RVA: 0x00024567 File Offset: 0x00022767
		internal ExistsRestriction(PropertyTag propertyTag)
		{
			this.propertyTag = propertyTag;
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x00024576 File Offset: 0x00022776
		internal override RestrictionType RestrictionType
		{
			get
			{
				return RestrictionType.Exists;
			}
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0002457C File Offset: 0x0002277C
		internal new static ExistsRestriction InternalParse(Reader reader, WireFormatStyle wireFormatStyle, uint depth)
		{
			PropertyTag propertyTag = reader.ReadPropertyTag();
			return new ExistsRestriction(propertyTag);
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00024596 File Offset: 0x00022796
		public override void Serialize(Writer writer, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			base.Serialize(writer, string8Encoding, wireFormatStyle);
			writer.WritePropertyTag(this.propertyTag);
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x000245AD File Offset: 0x000227AD
		public PropertyTag PropertyTag
		{
			get
			{
				return this.propertyTag;
			}
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x000245B8 File Offset: 0x000227B8
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" [Tag=").Append(this.PropertyTag.ToString()).Append("]");
		}

		// Token: 0x04000673 RID: 1651
		private readonly PropertyTag propertyTag;
	}
}
