using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E9C RID: 3740
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class StartAfterOrCompleteAfterCannotBeSetWhenJobCompletingException : CannotSetCompletingPermanentException
	{
		// Token: 0x0600A7E6 RID: 42982 RVA: 0x00289348 File Offset: 0x00287548
		public StartAfterOrCompleteAfterCannotBeSetWhenJobCompletingException() : base(Strings.StartAfterOrCompleteAfterCannotBeSetWhenJobCompleting)
		{
		}

		// Token: 0x0600A7E7 RID: 42983 RVA: 0x00289355 File Offset: 0x00287555
		public StartAfterOrCompleteAfterCannotBeSetWhenJobCompletingException(Exception innerException) : base(Strings.StartAfterOrCompleteAfterCannotBeSetWhenJobCompleting, innerException)
		{
		}

		// Token: 0x0600A7E8 RID: 42984 RVA: 0x00289363 File Offset: 0x00287563
		protected StartAfterOrCompleteAfterCannotBeSetWhenJobCompletingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A7E9 RID: 42985 RVA: 0x0028936D File Offset: 0x0028756D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
