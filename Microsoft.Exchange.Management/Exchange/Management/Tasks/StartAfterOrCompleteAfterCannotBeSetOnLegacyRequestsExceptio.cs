using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E9E RID: 3742
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class StartAfterOrCompleteAfterCannotBeSetOnLegacyRequestsException : RecipientTaskException
	{
		// Token: 0x0600A7EE RID: 42990 RVA: 0x002893A6 File Offset: 0x002875A6
		public StartAfterOrCompleteAfterCannotBeSetOnLegacyRequestsException() : base(Strings.StartAfterOrCompleteAfterCannotBeSetOnLegacyRequests)
		{
		}

		// Token: 0x0600A7EF RID: 42991 RVA: 0x002893B3 File Offset: 0x002875B3
		public StartAfterOrCompleteAfterCannotBeSetOnLegacyRequestsException(Exception innerException) : base(Strings.StartAfterOrCompleteAfterCannotBeSetOnLegacyRequests, innerException)
		{
		}

		// Token: 0x0600A7F0 RID: 42992 RVA: 0x002893C1 File Offset: 0x002875C1
		protected StartAfterOrCompleteAfterCannotBeSetOnLegacyRequestsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A7F1 RID: 42993 RVA: 0x002893CB File Offset: 0x002875CB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
