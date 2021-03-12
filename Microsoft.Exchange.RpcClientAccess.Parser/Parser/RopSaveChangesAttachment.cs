using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200032C RID: 812
	internal sealed class RopSaveChangesAttachment : RopSaveChanges
	{
		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06001351 RID: 4945 RVA: 0x000341A8 File Offset: 0x000323A8
		internal override RopId RopId
		{
			get
			{
				return RopId.SaveChangesAttachment;
			}
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x000341AC File Offset: 0x000323AC
		internal static Rop CreateRop()
		{
			return new RopSaveChangesAttachment();
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x000341B3 File Offset: 0x000323B3
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x000341E1 File Offset: 0x000323E1
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopSaveChangesAttachment.resultFactory;
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x000341E8 File Offset: 0x000323E8
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x000341FD File Offset: 0x000323FD
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.SaveChangesAttachment(serverObject, this.saveChangesMode, RopSaveChangesAttachment.resultFactory);
		}

		// Token: 0x04000A50 RID: 2640
		private const RopId RopType = RopId.SaveChangesAttachment;

		// Token: 0x04000A51 RID: 2641
		private static SaveChangesAttachmentResultFactory resultFactory = new SaveChangesAttachmentResultFactory();
	}
}
