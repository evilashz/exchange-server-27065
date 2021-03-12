using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E90 RID: 3728
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OffboardingDisabledException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A7A9 RID: 42921 RVA: 0x00288D29 File Offset: 0x00286F29
		public OffboardingDisabledException() : base(Strings.ErrorOffboardingDisabled)
		{
		}

		// Token: 0x0600A7AA RID: 42922 RVA: 0x00288D36 File Offset: 0x00286F36
		public OffboardingDisabledException(Exception innerException) : base(Strings.ErrorOffboardingDisabled, innerException)
		{
		}

		// Token: 0x0600A7AB RID: 42923 RVA: 0x00288D44 File Offset: 0x00286F44
		protected OffboardingDisabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A7AC RID: 42924 RVA: 0x00288D4E File Offset: 0x00286F4E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
