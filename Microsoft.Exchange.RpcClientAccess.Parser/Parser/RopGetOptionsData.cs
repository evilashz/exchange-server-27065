using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002CC RID: 716
	internal sealed class RopGetOptionsData : InputRop
	{
		// Token: 0x17000301 RID: 769
		// (get) Token: 0x0600105F RID: 4191 RVA: 0x0002E6F8 File Offset: 0x0002C8F8
		internal override RopId RopId
		{
			get
			{
				return RopId.GetOptionsData;
			}
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0002E6FC File Offset: 0x0002C8FC
		internal static Rop CreateRop()
		{
			return new RopGetOptionsData();
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0002E703 File Offset: 0x0002C903
		internal void SetInput(byte logonIndex, byte handleTableIndex, string addressType, bool wantWin32)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.addressType = addressType;
			this.wantWin32 = wantWin32;
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0002E71C File Offset: 0x0002C91C
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteAsciiString(this.addressType, StringFlags.IncludeNull);
			writer.WriteBool(this.wantWin32);
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0002E758 File Offset: 0x0002C958
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, (Reader readerParameter) => SuccessfulGetOptionsDataResult.Parse(readerParameter, string8Encoding), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0002E7A3 File Offset: 0x0002C9A3
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopGetOptionsData.resultFactory;
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0002E7AA File Offset: 0x0002C9AA
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.addressType = reader.ReadAsciiString(StringFlags.IncludeNull);
			this.wantWin32 = reader.ReadBool();
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0002E7CD File Offset: 0x0002C9CD
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0002E7E2 File Offset: 0x0002C9E2
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.GetOptionsData(serverObject, this.addressType, this.wantWin32, RopGetOptionsData.resultFactory);
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0002E802 File Offset: 0x0002CA02
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" AddressType=").Append(this.addressType);
			stringBuilder.Append(" WantWin32=").Append(this.wantWin32);
		}

		// Token: 0x04000826 RID: 2086
		private const RopId RopType = RopId.GetOptionsData;

		// Token: 0x04000827 RID: 2087
		private static GetOptionsDataResultFactory resultFactory = new GetOptionsDataResultFactory();

		// Token: 0x04000828 RID: 2088
		private string addressType;

		// Token: 0x04000829 RID: 2089
		private bool wantWin32;
	}
}
