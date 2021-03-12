using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverCommon
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	internal class PoisonHandlerNdrGenerationErrorException : LocalizedException
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002386 File Offset: 0x00000586
		public PoisonHandlerNdrGenerationErrorException(Exception innerException) : base(LocalizedString.Empty, innerException)
		{
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002394 File Offset: 0x00000594
		protected PoisonHandlerNdrGenerationErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
