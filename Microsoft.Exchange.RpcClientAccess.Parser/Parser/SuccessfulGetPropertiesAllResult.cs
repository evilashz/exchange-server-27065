using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200025C RID: 604
	internal sealed class SuccessfulGetPropertiesAllResult : RopResult
	{
		// Token: 0x06000D10 RID: 3344 RVA: 0x00028518 File Offset: 0x00026718
		internal SuccessfulGetPropertiesAllResult(PropertyValue[] propertyValues) : base(RopId.GetPropertiesAll, ErrorCode.None, null)
		{
			if (propertyValues == null)
			{
				throw new ArgumentNullException("propertyValues cannot be null.");
			}
			this.propertyValues = propertyValues;
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00028538 File Offset: 0x00026738
		internal SuccessfulGetPropertiesAllResult(Reader reader, Encoding string8Encoding) : base(reader)
		{
			this.propertyValues = reader.ReadCountAndPropertyValueList(WireFormatStyle.Rop);
			foreach (PropertyValue propertyValue in this.propertyValues)
			{
				propertyValue.ResolveString8Values(string8Encoding);
			}
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00028583 File Offset: 0x00026783
		internal static SuccessfulGetPropertiesAllResult Parse(Reader reader, Encoding string8Encoding)
		{
			return new SuccessfulGetPropertiesAllResult(reader, string8Encoding);
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0002858C File Offset: 0x0002678C
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteCountAndPropertyValueList(this.propertyValues, base.String8Encoding, WireFormatStyle.Rop);
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x000285A8 File Offset: 0x000267A8
		internal PropertyValue[] PropertyValues
		{
			get
			{
				return this.propertyValues;
			}
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x000285B0 File Offset: 0x000267B0
		internal bool RemoveLargestProperty()
		{
			return PropertyRow.RemoveLargestValue(this.propertyValues);
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x000285C0 File Offset: 0x000267C0
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Properties: ");
			for (int i = 0; i < this.propertyValues.Length; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(" ");
				}
				stringBuilder.Append("[");
				this.propertyValues[i].AppendToString(stringBuilder);
				stringBuilder.Append("]");
			}
		}

		// Token: 0x040006FE RID: 1790
		private readonly PropertyValue[] propertyValues;
	}
}
