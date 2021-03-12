using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E73 RID: 3699
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GroupMailboxFailedToResolveUserSidException : RecipientTaskException
	{
		// Token: 0x0600A71A RID: 42778 RVA: 0x00287FFA File Offset: 0x002861FA
		public GroupMailboxFailedToResolveUserSidException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A71B RID: 42779 RVA: 0x00288003 File Offset: 0x00286203
		public GroupMailboxFailedToResolveUserSidException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A71C RID: 42780 RVA: 0x0028800D File Offset: 0x0028620D
		protected GroupMailboxFailedToResolveUserSidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A71D RID: 42781 RVA: 0x00288017 File Offset: 0x00286217
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
