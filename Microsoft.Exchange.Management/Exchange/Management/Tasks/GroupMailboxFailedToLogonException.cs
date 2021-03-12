using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E72 RID: 3698
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GroupMailboxFailedToLogonException : RecipientTaskException
	{
		// Token: 0x0600A716 RID: 42774 RVA: 0x00287FD3 File Offset: 0x002861D3
		public GroupMailboxFailedToLogonException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A717 RID: 42775 RVA: 0x00287FDC File Offset: 0x002861DC
		public GroupMailboxFailedToLogonException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A718 RID: 42776 RVA: 0x00287FE6 File Offset: 0x002861E6
		protected GroupMailboxFailedToLogonException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A719 RID: 42777 RVA: 0x00287FF0 File Offset: 0x002861F0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
