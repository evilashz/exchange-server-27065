using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200029B RID: 667
	internal abstract class RopDeletePropertiesBase : InputRop
	{
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x0002B893 File Offset: 0x00029A93
		protected PropertyTag[] PropertyTags
		{
			get
			{
				return this.propertyTags;
			}
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0002B89B File Offset: 0x00029A9B
		internal void SetInput(byte logonIndex, byte handleTableIndex, PropertyTag[] propertyTags)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.propertyTags = propertyTags;
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0002B8AC File Offset: 0x00029AAC
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteCountAndPropertyTagArray(this.propertyTags, FieldLength.WordSize);
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0002B8C3 File Offset: 0x00029AC3
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.propertyTags = reader.ReadCountAndPropertyTagArray(FieldLength.WordSize);
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x0002B8DA File Offset: 0x00029ADA
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Tags=[");
			Util.AppendToString<PropertyTag>(stringBuilder, this.propertyTags);
			stringBuilder.Append("]");
		}

		// Token: 0x0400078A RID: 1930
		private PropertyTag[] propertyTags;
	}
}
