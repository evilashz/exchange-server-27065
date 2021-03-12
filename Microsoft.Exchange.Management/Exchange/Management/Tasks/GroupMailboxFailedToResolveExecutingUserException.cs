using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E74 RID: 3700
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GroupMailboxFailedToResolveExecutingUserException : RecipientTaskException
	{
		// Token: 0x0600A71E RID: 42782 RVA: 0x00288021 File Offset: 0x00286221
		public GroupMailboxFailedToResolveExecutingUserException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A71F RID: 42783 RVA: 0x0028802A File Offset: 0x0028622A
		public GroupMailboxFailedToResolveExecutingUserException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A720 RID: 42784 RVA: 0x00288034 File Offset: 0x00286234
		protected GroupMailboxFailedToResolveExecutingUserException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A721 RID: 42785 RVA: 0x0028803E File Offset: 0x0028623E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
