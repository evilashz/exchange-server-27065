using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200029D RID: 669
	internal sealed class RopDeletePropertiesNoReplicate : RopDeletePropertiesBase
	{
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x0002B992 File Offset: 0x00029B92
		internal override RopId RopId
		{
			get
			{
				return RopId.DeletePropertiesNoReplicate;
			}
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0002B996 File Offset: 0x00029B96
		internal static Rop CreateRop()
		{
			return new RopDeletePropertiesNoReplicate();
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0002B99D File Offset: 0x00029B9D
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulDeletePropertiesNoReplicateResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0002B9CB File Offset: 0x00029BCB
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.DeletePropertiesNoReplicate(serverObject, base.PropertyTags, RopDeletePropertiesNoReplicate.resultFactory);
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0002B9E5 File Offset: 0x00029BE5
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopDeletePropertiesNoReplicate.resultFactory;
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0002B9EC File Offset: 0x00029BEC
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0400078D RID: 1933
		private const RopId RopType = RopId.DeletePropertiesNoReplicate;

		// Token: 0x0400078E RID: 1934
		private static DeletePropertiesNoReplicateResultFactory resultFactory = new DeletePropertiesNoReplicateResultFactory();
	}
}
