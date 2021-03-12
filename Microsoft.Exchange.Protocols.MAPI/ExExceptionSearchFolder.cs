using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000035 RID: 53
	public class ExExceptionSearchFolder : MapiException
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00005ABB File Offset: 0x00003CBB
		public ExExceptionSearchFolder(LID lid, string message) : base(lid, message, ErrorCodeValue.SearchFolder)
		{
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005ACA File Offset: 0x00003CCA
		public ExExceptionSearchFolder(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.SearchFolder, innerException)
		{
		}
	}
}
