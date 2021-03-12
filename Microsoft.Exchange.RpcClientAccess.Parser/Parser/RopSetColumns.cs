using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000337 RID: 823
	internal sealed class RopSetColumns : InputRop
	{
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060013A1 RID: 5025 RVA: 0x000348FD File Offset: 0x00032AFD
		internal PropertyTag[] PropertyTags
		{
			get
			{
				return this.propertyTags;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x00034905 File Offset: 0x00032B05
		internal override RopId RopId
		{
			get
			{
				return RopId.SetColumns;
			}
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x00034909 File Offset: 0x00032B09
		internal static Rop CreateRop()
		{
			return new RopSetColumns();
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x00034910 File Offset: 0x00032B10
		internal void SetInput(byte logonIndex, byte handleTableIndex, SetColumnsFlags flags, PropertyTag[] propertyTags)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.flags = flags;
			this.propertyTags = propertyTags;
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0003492C File Offset: 0x00032B2C
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte((byte)this.flags);
			writer.WriteUInt16((ushort)this.propertyTags.Length);
			for (int i = 0; i < this.propertyTags.Length; i++)
			{
				writer.WritePropertyTag(this.propertyTags[i]);
			}
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x00034986 File Offset: 0x00032B86
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulSetColumnsResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x000349B4 File Offset: 0x00032BB4
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopSetColumns.resultFactory;
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x000349BC File Offset: 0x00032BBC
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.flags = (SetColumnsFlags)reader.ReadByte();
			ushort num = reader.ReadUInt16();
			reader.CheckBoundary((uint)num, 4U);
			this.propertyTags = new PropertyTag[(int)num];
			for (int i = 0; i < (int)num; i++)
			{
				this.propertyTags[i] = reader.ReadPropertyTag();
			}
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x00034A1B File Offset: 0x00032C1B
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x00034A30 File Offset: 0x00032C30
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.SetColumns(serverObject, this.flags, this.propertyTags, RopSetColumns.resultFactory);
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x00034A50 File Offset: 0x00032C50
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Flags=").Append(this.flags);
			stringBuilder.Append(" Tags=[");
			Util.AppendToString<PropertyTag>(stringBuilder, this.propertyTags);
			stringBuilder.Append("]");
		}

		// Token: 0x04000A76 RID: 2678
		private const RopId RopType = RopId.SetColumns;

		// Token: 0x04000A77 RID: 2679
		private static SetColumnsResultFactory resultFactory = new SetColumnsResultFactory();

		// Token: 0x04000A78 RID: 2680
		private SetColumnsFlags flags;

		// Token: 0x04000A79 RID: 2681
		private PropertyTag[] propertyTags;
	}
}
