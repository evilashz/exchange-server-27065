using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E9F RID: 3743
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SuspendWRTCPreventCompletionCannotBeSetOnNewRequestsException : RecipientTaskException
	{
		// Token: 0x0600A7F2 RID: 42994 RVA: 0x002893D5 File Offset: 0x002875D5
		public SuspendWRTCPreventCompletionCannotBeSetOnNewRequestsException() : base(Strings.SuspendWRTCPreventCompletionCannotBeSetOnNewRequests)
		{
		}

		// Token: 0x0600A7F3 RID: 42995 RVA: 0x002893E2 File Offset: 0x002875E2
		public SuspendWRTCPreventCompletionCannotBeSetOnNewRequestsException(Exception innerException) : base(Strings.SuspendWRTCPreventCompletionCannotBeSetOnNewRequests, innerException)
		{
		}

		// Token: 0x0600A7F4 RID: 42996 RVA: 0x002893F0 File Offset: 0x002875F0
		protected SuspendWRTCPreventCompletionCannotBeSetOnNewRequestsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A7F5 RID: 42997 RVA: 0x002893FA File Offset: 0x002875FA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
