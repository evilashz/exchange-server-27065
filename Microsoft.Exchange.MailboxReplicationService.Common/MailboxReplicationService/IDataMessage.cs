using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000C8 RID: 200
	internal interface IDataMessage
	{
		// Token: 0x060007C4 RID: 1988
		int GetSize();

		// Token: 0x060007C5 RID: 1989
		void Serialize(bool useCompression, out DataMessageOpcode opcode, out byte[] data);
	}
}
