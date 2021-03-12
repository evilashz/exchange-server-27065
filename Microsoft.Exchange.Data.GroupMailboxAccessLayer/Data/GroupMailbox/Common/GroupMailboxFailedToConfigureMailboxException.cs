using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.GroupMailbox.Common
{
	// Token: 0x02000062 RID: 98
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GroupMailboxFailedToConfigureMailboxException : LocalizedException
	{
		// Token: 0x06000327 RID: 807 RVA: 0x00011D68 File Offset: 0x0000FF68
		public GroupMailboxFailedToConfigureMailboxException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00011D71 File Offset: 0x0000FF71
		public GroupMailboxFailedToConfigureMailboxException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00011D7B File Offset: 0x0000FF7B
		protected GroupMailboxFailedToConfigureMailboxException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00011D85 File Offset: 0x0000FF85
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
