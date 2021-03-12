using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002E8 RID: 744
	internal abstract class RopImportMessageChangeBase : InputOutputRop
	{
		// Token: 0x0600114E RID: 4430 RVA: 0x0002FE04 File Offset: 0x0002E004
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte returnHandleTableIndex, ImportMessageChangeFlags importMessageChangeFlags, PropertyValue[] propertyValues)
		{
			base.SetCommonInput(logonIndex, handleTableIndex, returnHandleTableIndex);
			this.importMessageChangeFlags = importMessageChangeFlags;
			this.propertyValues = propertyValues;
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x0002FE1F File Offset: 0x0002E01F
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte((byte)this.importMessageChangeFlags);
			writer.WriteCountAndPropertyValueList(this.propertyValues, string8Encoding, WireFormatStyle.Rop);
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x0002FE44 File Offset: 0x0002E044
		internal override void ResolveString8Values(Encoding string8Encoding)
		{
			base.ResolveString8Values(string8Encoding);
			foreach (PropertyValue propertyValue in this.propertyValues)
			{
				propertyValue.ResolveString8Values(string8Encoding);
			}
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x0002FE82 File Offset: 0x0002E082
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x0002FE97 File Offset: 0x0002E097
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.importMessageChangeFlags = (ImportMessageChangeFlags)reader.ReadByte();
			this.propertyValues = reader.ReadCountAndPropertyValueList(WireFormatStyle.Rop);
		}

		// Token: 0x0400092F RID: 2351
		protected ImportMessageChangeFlags importMessageChangeFlags;

		// Token: 0x04000930 RID: 2352
		protected PropertyValue[] propertyValues;
	}
}
