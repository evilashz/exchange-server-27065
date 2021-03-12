using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000029 RID: 41
	public class ExExceptionConditionViolation : MapiException
	{
		// Token: 0x060000DC RID: 220 RVA: 0x00005947 File Offset: 0x00003B47
		public ExExceptionConditionViolation(LID lid, string message) : base(lid, message, ErrorCodeValue.ConditionViolation)
		{
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005956 File Offset: 0x00003B56
		public ExExceptionConditionViolation(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.ConditionViolation, innerException)
		{
		}
	}
}
