using System;
using Microsoft.Exchange.Rpc.SharedCache;

namespace Microsoft.Exchange.SharedCache.Exceptions
{
	// Token: 0x0200001A RID: 26
	public class CorruptCacheEntryException : SharedCacheExceptionBase
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00003DAD File Offset: 0x00001FAD
		public CorruptCacheEntryException(string message) : base(ResponseCode.EntryCorrupt, message)
		{
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003DB7 File Offset: 0x00001FB7
		public CorruptCacheEntryException(string message, Exception innerException) : base(ResponseCode.EntryCorrupt, message, innerException)
		{
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003DC2 File Offset: 0x00001FC2
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00003DCA File Offset: 0x00001FCA
		public string Key { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003DD3 File Offset: 0x00001FD3
		public override string Message
		{
			get
			{
				return (base.Message + "(Key=" + this.Key) ?? "<null>)";
			}
		}
	}
}
