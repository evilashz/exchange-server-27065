using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000285 RID: 645
	internal sealed class RopAddressTypes : InputRop
	{
		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x0002A33C File Offset: 0x0002853C
		internal override RopId RopId
		{
			get
			{
				return RopId.AddressTypes;
			}
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0002A340 File Offset: 0x00028540
		internal static Rop CreateRop()
		{
			return new RopAddressTypes();
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0002A347 File Offset: 0x00028547
		internal void SetInput(byte logonIndex, byte handleTableIndex)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0002A351 File Offset: 0x00028551
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulAddressTypesResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0002A37F File Offset: 0x0002857F
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopAddressTypes.resultFactory;
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x0002A386 File Offset: 0x00028586
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0002A39B File Offset: 0x0002859B
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.AddressTypes(serverObject, RopAddressTypes.resultFactory);
		}

		// Token: 0x04000739 RID: 1849
		private const RopId RopType = RopId.AddressTypes;

		// Token: 0x0400073A RID: 1850
		private static AddressTypesResultFactory resultFactory = new AddressTypesResultFactory();
	}
}
