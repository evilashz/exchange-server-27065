using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverCommon
{
	// Token: 0x02000008 RID: 8
	[Serializable]
	internal class NdrItemToTransportItemCopyException : LocalizedException
	{
		// Token: 0x06000016 RID: 22 RVA: 0x0000236E File Offset: 0x0000056E
		public NdrItemToTransportItemCopyException(Exception innerException) : base(LocalizedString.Empty, innerException)
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000237C File Offset: 0x0000057C
		protected NdrItemToTransportItemCopyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
