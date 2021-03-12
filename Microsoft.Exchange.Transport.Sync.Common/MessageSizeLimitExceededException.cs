using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000028 RID: 40
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MessageSizeLimitExceededException : LocalizedException
	{
		// Token: 0x0600016D RID: 365 RVA: 0x00005619 File Offset: 0x00003819
		public MessageSizeLimitExceededException() : base(Strings.MessageSizeLimitExceededException)
		{
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00005626 File Offset: 0x00003826
		public MessageSizeLimitExceededException(Exception innerException) : base(Strings.MessageSizeLimitExceededException, innerException)
		{
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00005634 File Offset: 0x00003834
		protected MessageSizeLimitExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000563E File Offset: 0x0000383E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
