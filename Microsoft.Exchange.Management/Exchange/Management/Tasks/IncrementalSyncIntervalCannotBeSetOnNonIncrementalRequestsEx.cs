using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EA7 RID: 3751
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IncrementalSyncIntervalCannotBeSetOnNonIncrementalRequestsException : RecipientTaskException
	{
		// Token: 0x0600A818 RID: 43032 RVA: 0x0028971D File Offset: 0x0028791D
		public IncrementalSyncIntervalCannotBeSetOnNonIncrementalRequestsException() : base(Strings.ErrorIncrementalSyncIntervalCannotBeSetOnNonIncrementalRequests)
		{
		}

		// Token: 0x0600A819 RID: 43033 RVA: 0x0028972A File Offset: 0x0028792A
		public IncrementalSyncIntervalCannotBeSetOnNonIncrementalRequestsException(Exception innerException) : base(Strings.ErrorIncrementalSyncIntervalCannotBeSetOnNonIncrementalRequests, innerException)
		{
		}

		// Token: 0x0600A81A RID: 43034 RVA: 0x00289738 File Offset: 0x00287938
		protected IncrementalSyncIntervalCannotBeSetOnNonIncrementalRequestsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A81B RID: 43035 RVA: 0x00289742 File Offset: 0x00287942
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
