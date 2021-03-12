using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000079 RID: 121
	public class CorruptDataException : StoreException
	{
		// Token: 0x0600048B RID: 1163 RVA: 0x0001CF42 File Offset: 0x0001B142
		public CorruptDataException(LID lid, string message) : base(lid, ErrorCodeValue.CorruptData, message)
		{
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0001CF51 File Offset: 0x0001B151
		public CorruptDataException(LID lid, string message, Exception innerException) : base(lid, ErrorCodeValue.CorruptData, message, innerException)
		{
		}
	}
}
