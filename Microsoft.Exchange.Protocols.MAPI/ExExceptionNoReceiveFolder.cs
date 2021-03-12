using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000031 RID: 49
	public class ExExceptionNoReceiveFolder : MapiException
	{
		// Token: 0x060000EC RID: 236 RVA: 0x00005A3F File Offset: 0x00003C3F
		public ExExceptionNoReceiveFolder(LID lid, string message) : base(lid, message, ErrorCodeValue.NoReceiveFolder)
		{
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005A4E File Offset: 0x00003C4E
		public ExExceptionNoReceiveFolder(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.NoReceiveFolder, innerException)
		{
		}
	}
}
