using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002E6 RID: 742
	internal sealed class RopImportDelete : InputRop
	{
		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x0002FB1B File Offset: 0x0002DD1B
		internal override RopId RopId
		{
			get
			{
				return RopId.ImportDelete;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x0002FB1F File Offset: 0x0002DD1F
		internal ImportDeleteFlags ImportDeleteFlags
		{
			get
			{
				return this.importDeleteFlags;
			}
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x0002FB27 File Offset: 0x0002DD27
		internal static Rop CreateRop()
		{
			return new RopImportDelete();
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0002FB2E File Offset: 0x0002DD2E
		internal void SetInput(byte logonIndex, byte handleTableIndex, ImportDeleteFlags importDeleteFlags, PropertyValue[] deleteChanges)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.importDeleteFlags = importDeleteFlags;
			this.deleteChanges = deleteChanges;
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x0002FB47 File Offset: 0x0002DD47
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte((byte)this.importDeleteFlags);
			writer.WriteCountAndPropertyValueList(this.deleteChanges, string8Encoding, WireFormatStyle.Rop);
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0002FB6B File Offset: 0x0002DD6B
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0002FB99 File Offset: 0x0002DD99
		protected override void InternalExecute(IServerObject sourceServerObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.ImportDelete(sourceServerObject, this.importDeleteFlags, this.deleteChanges, RopImportDelete.resultFactory);
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0002FBB9 File Offset: 0x0002DDB9
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopImportDelete.resultFactory;
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x0002FBC0 File Offset: 0x0002DDC0
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.importDeleteFlags = (ImportDeleteFlags)reader.ReadByte();
			this.deleteChanges = reader.ReadCountAndPropertyValueList(WireFormatStyle.Rop);
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x0002FBE3 File Offset: 0x0002DDE3
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0002FBF8 File Offset: 0x0002DDF8
		internal override void ResolveString8Values(Encoding string8Encoding)
		{
			base.ResolveString8Values(string8Encoding);
			foreach (PropertyValue propertyValue in this.deleteChanges)
			{
				propertyValue.ResolveString8Values(string8Encoding);
			}
		}

		// Token: 0x04000927 RID: 2343
		private const RopId RopType = RopId.ImportDelete;

		// Token: 0x04000928 RID: 2344
		private static ImportDeleteResultFactory resultFactory = new ImportDeleteResultFactory();

		// Token: 0x04000929 RID: 2345
		private ImportDeleteFlags importDeleteFlags;

		// Token: 0x0400092A RID: 2346
		private PropertyValue[] deleteChanges;
	}
}
