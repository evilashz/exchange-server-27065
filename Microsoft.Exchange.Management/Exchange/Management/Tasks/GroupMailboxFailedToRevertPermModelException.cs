using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E77 RID: 3703
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GroupMailboxFailedToRevertPermModelException : RecipientTaskException
	{
		// Token: 0x0600A72A RID: 42794 RVA: 0x00288096 File Offset: 0x00286296
		public GroupMailboxFailedToRevertPermModelException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A72B RID: 42795 RVA: 0x0028809F File Offset: 0x0028629F
		public GroupMailboxFailedToRevertPermModelException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A72C RID: 42796 RVA: 0x002880A9 File Offset: 0x002862A9
		protected GroupMailboxFailedToRevertPermModelException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A72D RID: 42797 RVA: 0x002880B3 File Offset: 0x002862B3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
