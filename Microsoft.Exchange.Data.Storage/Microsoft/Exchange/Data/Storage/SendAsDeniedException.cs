using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200077A RID: 1914
	[Serializable]
	public class SendAsDeniedException : StoragePermanentException
	{
		// Token: 0x060048CB RID: 18635 RVA: 0x0013186F File Offset: 0x0012FA6F
		public SendAsDeniedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060048CC RID: 18636 RVA: 0x00131879 File Offset: 0x0012FA79
		protected SendAsDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
