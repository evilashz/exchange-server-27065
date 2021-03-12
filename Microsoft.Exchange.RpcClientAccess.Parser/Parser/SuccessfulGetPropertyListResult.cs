using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200025D RID: 605
	internal sealed class SuccessfulGetPropertyListResult : RopResult
	{
		// Token: 0x06000D17 RID: 3351 RVA: 0x0002862C File Offset: 0x0002682C
		internal SuccessfulGetPropertyListResult(PropertyTag[] propertyTags) : base(RopId.GetPropertyList, ErrorCode.None, null)
		{
			if (propertyTags == null)
			{
				throw new ArgumentNullException("propertyTags");
			}
			this.propertyTags = propertyTags;
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0002864D File Offset: 0x0002684D
		internal SuccessfulGetPropertyListResult(Reader reader) : base(reader)
		{
			this.propertyTags = reader.ReadCountAndPropertyTagArray(FieldLength.WordSize);
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x00028663 File Offset: 0x00026863
		internal static SuccessfulGetPropertyListResult Parse(Reader reader)
		{
			return new SuccessfulGetPropertyListResult(reader);
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0002866B File Offset: 0x0002686B
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteCountAndPropertyTagArray(this.propertyTags, FieldLength.WordSize);
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000D1B RID: 3355 RVA: 0x00028681 File Offset: 0x00026881
		public PropertyTag[] PropertyTags
		{
			get
			{
				return this.propertyTags;
			}
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00028689 File Offset: 0x00026889
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Tags=[");
			Util.AppendToString<PropertyTag>(stringBuilder, this.propertyTags);
			stringBuilder.Append("]");
		}

		// Token: 0x040006FF RID: 1791
		private readonly PropertyTag[] propertyTags;
	}
}
