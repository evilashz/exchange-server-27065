using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000146 RID: 326
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DiscoveryHoldSearchException : LocalizedException
	{
		// Token: 0x06000D32 RID: 3378 RVA: 0x00052071 File Offset: 0x00050271
		public DiscoveryHoldSearchException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0005207A File Offset: 0x0005027A
		public DiscoveryHoldSearchException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00052084 File Offset: 0x00050284
		protected DiscoveryHoldSearchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0005208E File Offset: 0x0005028E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
