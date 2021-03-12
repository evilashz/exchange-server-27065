using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverCommon
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	internal class NonNdrItemToTransportItemCopyException : LocalizedException
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002356 File Offset: 0x00000556
		public NonNdrItemToTransportItemCopyException(Exception innerException) : base(LocalizedString.Empty, innerException)
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002364 File Offset: 0x00000564
		protected NonNdrItemToTransportItemCopyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
