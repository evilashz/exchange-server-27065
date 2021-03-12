using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200012D RID: 301
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxNotFoundException : DataSourceOperationException
	{
		// Token: 0x06001478 RID: 5240 RVA: 0x0006AEF4 File Offset: 0x000690F4
		public MailboxNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x0006AEFD File Offset: 0x000690FD
		public MailboxNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x0006AF07 File Offset: 0x00069107
		protected MailboxNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x0006AF11 File Offset: 0x00069111
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
