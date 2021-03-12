using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EBD RID: 3773
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PreventCompletionCannotSetException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A88B RID: 43147 RVA: 0x0028A340 File Offset: 0x00288540
		public PreventCompletionCannotSetException() : base(Strings.ErrorPreventCompletionCannotSet)
		{
		}

		// Token: 0x0600A88C RID: 43148 RVA: 0x0028A34D File Offset: 0x0028854D
		public PreventCompletionCannotSetException(Exception innerException) : base(Strings.ErrorPreventCompletionCannotSet, innerException)
		{
		}

		// Token: 0x0600A88D RID: 43149 RVA: 0x0028A35B File Offset: 0x0028855B
		protected PreventCompletionCannotSetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A88E RID: 43150 RVA: 0x0028A365 File Offset: 0x00288565
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
