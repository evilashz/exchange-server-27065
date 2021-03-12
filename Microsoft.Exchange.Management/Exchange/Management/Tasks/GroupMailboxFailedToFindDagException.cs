using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E6F RID: 3695
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GroupMailboxFailedToFindDagException : RecipientTaskException
	{
		// Token: 0x0600A70A RID: 42762 RVA: 0x00287F5E File Offset: 0x0028615E
		public GroupMailboxFailedToFindDagException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A70B RID: 42763 RVA: 0x00287F67 File Offset: 0x00286167
		public GroupMailboxFailedToFindDagException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A70C RID: 42764 RVA: 0x00287F71 File Offset: 0x00286171
		protected GroupMailboxFailedToFindDagException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A70D RID: 42765 RVA: 0x00287F7B File Offset: 0x0028617B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
