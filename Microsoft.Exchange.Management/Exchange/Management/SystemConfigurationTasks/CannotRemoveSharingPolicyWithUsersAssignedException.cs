using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000FFD RID: 4093
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotRemoveSharingPolicyWithUsersAssignedException : LocalizedException
	{
		// Token: 0x0600AEAF RID: 44719 RVA: 0x00293454 File Offset: 0x00291654
		public CannotRemoveSharingPolicyWithUsersAssignedException() : base(Strings.CannotRemoveSharingPolicyWithUsersAssigned)
		{
		}

		// Token: 0x0600AEB0 RID: 44720 RVA: 0x00293461 File Offset: 0x00291661
		public CannotRemoveSharingPolicyWithUsersAssignedException(Exception innerException) : base(Strings.CannotRemoveSharingPolicyWithUsersAssigned, innerException)
		{
		}

		// Token: 0x0600AEB1 RID: 44721 RVA: 0x0029346F File Offset: 0x0029166F
		protected CannotRemoveSharingPolicyWithUsersAssignedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AEB2 RID: 44722 RVA: 0x00293479 File Offset: 0x00291679
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
