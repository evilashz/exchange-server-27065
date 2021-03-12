using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200077D RID: 1917
	[Serializable]
	public class SessionDeadException : StoragePermanentException
	{
		// Token: 0x060048D3 RID: 18643 RVA: 0x001318BD File Offset: 0x0012FABD
		public SessionDeadException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060048D4 RID: 18644 RVA: 0x001318C6 File Offset: 0x0012FAC6
		public SessionDeadException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060048D5 RID: 18645 RVA: 0x001318D0 File Offset: 0x0012FAD0
		protected SessionDeadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
