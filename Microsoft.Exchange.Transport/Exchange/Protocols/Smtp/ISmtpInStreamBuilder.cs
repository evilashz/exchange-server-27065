using System;
using System.IO;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200040C RID: 1036
	internal interface ISmtpInStreamBuilder
	{
		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x06002F8E RID: 12174
		// (set) Token: 0x06002F8F RID: 12175
		bool IsDiscardingData { get; set; }

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x06002F90 RID: 12176
		// (set) Token: 0x06002F91 RID: 12177
		Stream BodyStream { get; set; }

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06002F92 RID: 12178
		long TotalBytesRead { get; }

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06002F93 RID: 12179
		long TotalBytesWritten { get; }

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06002F94 RID: 12180
		long EohPos { get; }

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x06002F95 RID: 12181
		bool IsEodSeen { get; }

		// Token: 0x06002F96 RID: 12182
		void Reset();

		// Token: 0x06002F97 RID: 12183
		bool Write(byte[] data, int offset, int numBytes, out int numBytesConsumed);

		// Token: 0x06002F98 RID: 12184
		bool Write(CommandContext commandContext, out int numBytesConsumed);
	}
}
