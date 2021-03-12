using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Worker.Framework
{
	// Token: 0x020001F6 RID: 502
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class Pop3InvalidCommandException : Exception
	{
		// Token: 0x060010AC RID: 4268 RVA: 0x00036470 File Offset: 0x00034670
		public Pop3InvalidCommandException()
		{
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00036478 File Offset: 0x00034678
		public Pop3InvalidCommandException(string message) : base(message)
		{
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00036481 File Offset: 0x00034681
		public Pop3InvalidCommandException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0003648B File Offset: 0x0003468B
		internal Pop3InvalidCommandException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
