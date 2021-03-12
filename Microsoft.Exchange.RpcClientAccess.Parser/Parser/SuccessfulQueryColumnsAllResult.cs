using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200027C RID: 636
	internal sealed class SuccessfulQueryColumnsAllResult : RopResult
	{
		// Token: 0x06000DC1 RID: 3521 RVA: 0x00029A60 File Offset: 0x00027C60
		internal SuccessfulQueryColumnsAllResult(PropertyTag[] propertyTags) : base(RopId.QueryColumnsAll, ErrorCode.None, null)
		{
			if (propertyTags == null)
			{
				throw new ArgumentNullException("propertyTags");
			}
			this.propertyTags = propertyTags;
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00029A81 File Offset: 0x00027C81
		internal SuccessfulQueryColumnsAllResult(Reader reader) : base(reader)
		{
			this.propertyTags = reader.ReadCountAndPropertyTagArray(FieldLength.WordSize);
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x00029A97 File Offset: 0x00027C97
		internal PropertyTag[] Columns
		{
			get
			{
				return this.propertyTags;
			}
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x00029A9F File Offset: 0x00027C9F
		internal static SuccessfulQueryColumnsAllResult Parse(Reader reader)
		{
			return new SuccessfulQueryColumnsAllResult(reader);
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x00029AA7 File Offset: 0x00027CA7
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteCountAndPropertyTagArray(this.propertyTags, FieldLength.WordSize);
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x00029ABD File Offset: 0x00027CBD
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Tags=[");
			Util.AppendToString<PropertyTag>(stringBuilder, this.propertyTags);
			stringBuilder.Append("]");
		}

		// Token: 0x04000727 RID: 1831
		private readonly PropertyTag[] propertyTags;
	}
}
