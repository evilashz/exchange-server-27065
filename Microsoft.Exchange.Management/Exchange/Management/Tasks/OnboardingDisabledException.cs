using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E8F RID: 3727
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OnboardingDisabledException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A7A5 RID: 42917 RVA: 0x00288CFA File Offset: 0x00286EFA
		public OnboardingDisabledException() : base(Strings.ErrorOnboardingDisabled)
		{
		}

		// Token: 0x0600A7A6 RID: 42918 RVA: 0x00288D07 File Offset: 0x00286F07
		public OnboardingDisabledException(Exception innerException) : base(Strings.ErrorOnboardingDisabled, innerException)
		{
		}

		// Token: 0x0600A7A7 RID: 42919 RVA: 0x00288D15 File Offset: 0x00286F15
		protected OnboardingDisabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A7A8 RID: 42920 RVA: 0x00288D1F File Offset: 0x00286F1F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
