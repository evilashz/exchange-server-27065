using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200032D RID: 813
	internal sealed class RopSaveChangesMessage : RopSaveChanges
	{
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06001359 RID: 4953 RVA: 0x0003422B File Offset: 0x0003242B
		internal override RopId RopId
		{
			get
			{
				return RopId.SaveChangesMessage;
			}
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0003422F File Offset: 0x0003242F
		internal static Rop CreateRop()
		{
			return new RopSaveChangesMessage();
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x00034236 File Offset: 0x00032436
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulSaveChangesMessageResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x00034264 File Offset: 0x00032464
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new SaveChangesMessageResultFactory(this.realHandleTableIndex);
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x00034271 File Offset: 0x00032471
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00034288 File Offset: 0x00032488
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			SaveChangesMessageResultFactory resultFactory = new SaveChangesMessageResultFactory(this.realHandleTableIndex);
			this.result = ropHandler.SaveChangesMessage(serverObject, this.saveChangesMode, resultFactory);
		}

		// Token: 0x04000A52 RID: 2642
		private const RopId RopType = RopId.SaveChangesMessage;
	}
}
