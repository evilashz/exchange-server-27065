using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000041 RID: 65
	public static class CorrelationIdHelper
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x00017158 File Offset: 0x00015358
		public static Guid GetCorrelationId(int mailboxNumber, long fid)
		{
			return new Guid(mailboxNumber, 0, 0, (byte)(fid & 255L), (byte)(fid >> 8 & 255L), (byte)(fid >> 16 & 255L), (byte)(fid >> 24 & 255L), (byte)(fid >> 32 & 255L), (byte)(fid >> 40 & 255L), (byte)(fid >> 48 & 255L), (byte)(fid >> 56 & 255L));
		}
	}
}
