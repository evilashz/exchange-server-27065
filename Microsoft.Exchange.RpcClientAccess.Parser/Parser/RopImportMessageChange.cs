using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002E9 RID: 745
	internal sealed class RopImportMessageChange : RopImportMessageChangeBase
	{
		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x0002FEC2 File Offset: 0x0002E0C2
		internal override RopId RopId
		{
			get
			{
				return RopId.ImportMessageChange;
			}
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x0002FEC6 File Offset: 0x0002E0C6
		internal static Rop CreateRop()
		{
			return new RopImportMessageChange();
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x0002FECD File Offset: 0x0002E0CD
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopImportMessageChange.resultFactory;
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x0002FED4 File Offset: 0x0002E0D4
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulImportMessageChangeResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x0002FF02 File Offset: 0x0002E102
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.ImportMessageChange(serverObject, this.importMessageChangeFlags, this.propertyValues, RopImportMessageChange.resultFactory);
		}

		// Token: 0x04000931 RID: 2353
		private const RopId RopType = RopId.ImportMessageChange;

		// Token: 0x04000932 RID: 2354
		private static ImportMessageChangeResultFactory resultFactory = new ImportMessageChangeResultFactory();
	}
}
