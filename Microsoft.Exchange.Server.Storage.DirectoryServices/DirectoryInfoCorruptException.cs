using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.DirectoryServices
{
	// Token: 0x02000005 RID: 5
	public class DirectoryInfoCorruptException : StoreException
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002264 File Offset: 0x00000464
		public DirectoryInfoCorruptException(LID lid, string message) : base(lid, ErrorCodeValue.ADPropertyError, message)
		{
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002273 File Offset: 0x00000473
		public DirectoryInfoCorruptException(LID lid, string message, Exception innerException) : base(lid, ErrorCodeValue.ADPropertyError, message, innerException)
		{
		}
	}
}
