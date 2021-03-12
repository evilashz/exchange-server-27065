using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E75 RID: 3701
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GroupMailboxFailedToResolvePublicToGroupException : RecipientTaskException
	{
		// Token: 0x0600A722 RID: 42786 RVA: 0x00288048 File Offset: 0x00286248
		public GroupMailboxFailedToResolvePublicToGroupException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A723 RID: 42787 RVA: 0x00288051 File Offset: 0x00286251
		public GroupMailboxFailedToResolvePublicToGroupException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A724 RID: 42788 RVA: 0x0028805B File Offset: 0x0028625B
		protected GroupMailboxFailedToResolvePublicToGroupException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A725 RID: 42789 RVA: 0x00288065 File Offset: 0x00286265
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
