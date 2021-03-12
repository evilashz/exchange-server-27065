using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200000F RID: 15
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MailboxFailureException : LocalizedException
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00004C4A File Offset: 0x00002E4A
		public MailboxFailureException() : base(Strings.MailboxFailure)
		{
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004C57 File Offset: 0x00002E57
		public MailboxFailureException(Exception innerException) : base(Strings.MailboxFailure, innerException)
		{
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004C65 File Offset: 0x00002E65
		protected MailboxFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004C6F File Offset: 0x00002E6F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
