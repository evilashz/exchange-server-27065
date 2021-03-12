using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001B2 RID: 434
	internal interface IParseLogonTracker
	{
		// Token: 0x060008A2 RID: 2210
		void ParseBegin(ServerObjectHandleTable serverObjectHandleTable);

		// Token: 0x060008A3 RID: 2211
		void ParseEnd();

		// Token: 0x060008A4 RID: 2212
		void ParseRecordLogon(byte logonIndex, byte handleTableIndex, LogonFlags logonFlags);

		// Token: 0x060008A5 RID: 2213
		void ParseRecordRelease(byte handleTableIndex);

		// Token: 0x060008A6 RID: 2214
		void ParseRecordInputOutput(byte handleTableIndex);

		// Token: 0x060008A7 RID: 2215
		bool ParseIsValidLogon(byte logonIndex);

		// Token: 0x060008A8 RID: 2216
		bool ParseIsPublicLogon(byte logonIndex);
	}
}
