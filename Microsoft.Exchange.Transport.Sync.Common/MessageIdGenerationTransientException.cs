using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000027 RID: 39
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MessageIdGenerationTransientException : NonPromotableTransientException
	{
		// Token: 0x06000169 RID: 361 RVA: 0x000055EA File Offset: 0x000037EA
		public MessageIdGenerationTransientException() : base(Strings.MessageIdGenerationTransientException)
		{
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000055F7 File Offset: 0x000037F7
		public MessageIdGenerationTransientException(Exception innerException) : base(Strings.MessageIdGenerationTransientException, innerException)
		{
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00005605 File Offset: 0x00003805
		protected MessageIdGenerationTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000560F File Offset: 0x0000380F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
