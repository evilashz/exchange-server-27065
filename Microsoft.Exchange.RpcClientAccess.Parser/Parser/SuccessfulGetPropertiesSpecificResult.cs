using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002D4 RID: 724
	internal sealed class SuccessfulGetPropertiesSpecificResult : RopResult
	{
		// Token: 0x060010BA RID: 4282 RVA: 0x0002EFEB File Offset: 0x0002D1EB
		internal SuccessfulGetPropertiesSpecificResult(PropertyTag[] originalPropertyTags, PropertyValue[] propertyValues) : base(RopId.GetPropertiesSpecific, ErrorCode.None, null)
		{
			if (originalPropertyTags == null)
			{
				throw new ArgumentNullException("originalPropertyTags cannot be null.");
			}
			if (propertyValues == null)
			{
				throw new ArgumentNullException("propertyValues cannot be null.");
			}
			this.propertyRow = new PropertyRow(originalPropertyTags, propertyValues);
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0002F01F File Offset: 0x0002D21F
		internal SuccessfulGetPropertiesSpecificResult(Reader reader, PropertyTag[] propertyTags, Encoding string8Encoding) : base(reader)
		{
			this.propertyRow = PropertyRow.Parse(reader, propertyTags, WireFormatStyle.Rop);
			this.propertyRow.ResolveString8Values(string8Encoding);
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x0002F044 File Offset: 0x0002D244
		public PropertyValue[] PropertyValues
		{
			get
			{
				return this.propertyRow.PropertyValues;
			}
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x0002F05F File Offset: 0x0002D25F
		public bool RemoveLargestProperty()
		{
			return PropertyRow.RemoveLargestValue(this.PropertyValues);
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0002F06C File Offset: 0x0002D26C
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			this.propertyRow.Serialize(writer, base.String8Encoding, WireFormatStyle.Rop);
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0002F098 File Offset: 0x0002D298
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Properties: ");
			this.propertyRow.AppendToString(stringBuilder);
		}

		// Token: 0x0400083F RID: 2111
		private readonly PropertyRow propertyRow;
	}
}
