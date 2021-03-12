using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000040 RID: 64
	public interface ILogTransactionInformation
	{
		// Token: 0x0600047D RID: 1149
		byte Type();

		// Token: 0x0600047E RID: 1150
		int Serialize(byte[] buffer, int offset);

		// Token: 0x0600047F RID: 1151
		void Parse(byte[] buffer, ref int offset);
	}
}
