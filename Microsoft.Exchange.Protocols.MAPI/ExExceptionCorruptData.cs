using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000038 RID: 56
	public class ExExceptionCorruptData : MapiException
	{
		// Token: 0x060000FA RID: 250 RVA: 0x00005B18 File Offset: 0x00003D18
		public ExExceptionCorruptData(LID lid, string message) : base(lid, message, ErrorCodeValue.CorruptData)
		{
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005B27 File Offset: 0x00003D27
		public ExExceptionCorruptData(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.CorruptData, innerException)
		{
		}
	}
}
