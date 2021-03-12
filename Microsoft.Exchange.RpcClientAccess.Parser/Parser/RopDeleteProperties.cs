using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200029C RID: 668
	internal sealed class RopDeleteProperties : RopDeletePropertiesBase
	{
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x0002B90F File Offset: 0x00029B0F
		internal override RopId RopId
		{
			get
			{
				return RopId.DeleteProperties;
			}
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0002B913 File Offset: 0x00029B13
		internal static Rop CreateRop()
		{
			return new RopDeleteProperties();
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x0002B91A File Offset: 0x00029B1A
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulDeletePropertiesResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0002B948 File Offset: 0x00029B48
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.DeleteProperties(serverObject, base.PropertyTags, RopDeleteProperties.resultFactory);
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0002B962 File Offset: 0x00029B62
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopDeleteProperties.resultFactory;
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0002B969 File Offset: 0x00029B69
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0400078B RID: 1931
		private const RopId RopType = RopId.DeleteProperties;

		// Token: 0x0400078C RID: 1932
		private static DeletePropertiesResultFactory resultFactory = new DeletePropertiesResultFactory();
	}
}
