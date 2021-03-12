using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E78 RID: 3704
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GroupMailboxNotAuthorizedException : RecipientTaskException
	{
		// Token: 0x0600A72E RID: 42798 RVA: 0x002880BD File Offset: 0x002862BD
		public GroupMailboxNotAuthorizedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A72F RID: 42799 RVA: 0x002880C6 File Offset: 0x002862C6
		public GroupMailboxNotAuthorizedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A730 RID: 42800 RVA: 0x002880D0 File Offset: 0x002862D0
		protected GroupMailboxNotAuthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A731 RID: 42801 RVA: 0x002880DA File Offset: 0x002862DA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
