using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E71 RID: 3697
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GroupMailboxFailedToResolveOwnerException : RecipientTaskException
	{
		// Token: 0x0600A712 RID: 42770 RVA: 0x00287FAC File Offset: 0x002861AC
		public GroupMailboxFailedToResolveOwnerException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A713 RID: 42771 RVA: 0x00287FB5 File Offset: 0x002861B5
		public GroupMailboxFailedToResolveOwnerException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A714 RID: 42772 RVA: 0x00287FBF File Offset: 0x002861BF
		protected GroupMailboxFailedToResolveOwnerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A715 RID: 42773 RVA: 0x00287FC9 File Offset: 0x002861C9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
