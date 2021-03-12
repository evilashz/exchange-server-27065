using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200032B RID: 811
	internal abstract class RopSaveChanges : InputRop
	{
		// Token: 0x17000361 RID: 865
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x00034116 File Offset: 0x00032316
		internal override byte InputHandleTableIndex
		{
			get
			{
				return this.realHandleTableIndex;
			}
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x0003411E File Offset: 0x0003231E
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte realHandleTableIndex, SaveChangesMode saveChangesMode)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.saveChangesMode = saveChangesMode;
			this.realHandleTableIndex = realHandleTableIndex;
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00034137 File Offset: 0x00032337
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte(this.realHandleTableIndex);
			writer.WriteByte((byte)this.saveChangesMode);
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x00034159 File Offset: 0x00032359
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.realHandleTableIndex = reader.ReadByte();
			this.saveChangesMode = (SaveChangesMode)reader.ReadByte();
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0003417B File Offset: 0x0003237B
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" SaveMode=").Append(this.saveChangesMode);
		}

		// Token: 0x04000A4E RID: 2638
		protected byte realHandleTableIndex;

		// Token: 0x04000A4F RID: 2639
		protected SaveChangesMode saveChangesMode;
	}
}
