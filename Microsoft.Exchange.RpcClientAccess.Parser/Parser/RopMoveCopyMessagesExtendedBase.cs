using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000308 RID: 776
	internal abstract class RopMoveCopyMessagesExtendedBase : RopMoveCopyMessagesBase
	{
		// Token: 0x06001210 RID: 4624 RVA: 0x00031C24 File Offset: 0x0002FE24
		internal void SetInput(byte logonIndex, byte sourceHandleTableIndex, byte destinationHandleTableIndex, StoreId[] messageIds, bool reportProgress, bool isCopy, PropertyValue[] propertyValues)
		{
			base.SetInput(logonIndex, sourceHandleTableIndex, destinationHandleTableIndex, messageIds, reportProgress, isCopy);
			this.propertyValues = propertyValues;
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x00031C3D File Offset: 0x0002FE3D
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteCountAndPropertyValueList(this.propertyValues, string8Encoding, WireFormatStyle.Rop);
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x00031C55 File Offset: 0x0002FE55
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.propertyValues = reader.ReadCountAndPropertyValueList(WireFormatStyle.Rop);
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x00031C6C File Offset: 0x0002FE6C
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00031C84 File Offset: 0x0002FE84
		internal override void ResolveString8Values(Encoding string8Encoding)
		{
			base.ResolveString8Values(string8Encoding);
			foreach (PropertyValue propertyValue in this.propertyValues)
			{
				propertyValue.ResolveString8Values(string8Encoding);
			}
		}

		// Token: 0x040009D1 RID: 2513
		protected PropertyValue[] propertyValues;
	}
}
