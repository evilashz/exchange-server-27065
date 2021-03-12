using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E70 RID: 3696
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GroupMailboxFailedToSetPermissionException : RecipientTaskException
	{
		// Token: 0x0600A70E RID: 42766 RVA: 0x00287F85 File Offset: 0x00286185
		public GroupMailboxFailedToSetPermissionException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A70F RID: 42767 RVA: 0x00287F8E File Offset: 0x0028618E
		public GroupMailboxFailedToSetPermissionException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A710 RID: 42768 RVA: 0x00287F98 File Offset: 0x00286198
		protected GroupMailboxFailedToSetPermissionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A711 RID: 42769 RVA: 0x00287FA2 File Offset: 0x002861A2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
