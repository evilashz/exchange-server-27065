using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000EA9 RID: 3753
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoMatchingUMMailboxPolicyInTargetForestException : LocalizedException
	{
		// Token: 0x0600A820 RID: 43040 RVA: 0x0028977B File Offset: 0x0028797B
		public NoMatchingUMMailboxPolicyInTargetForestException() : base(Strings.NoMatchingUMMailboxPolicyInTargetForest)
		{
		}

		// Token: 0x0600A821 RID: 43041 RVA: 0x00289788 File Offset: 0x00287988
		public NoMatchingUMMailboxPolicyInTargetForestException(Exception innerException) : base(Strings.NoMatchingUMMailboxPolicyInTargetForest, innerException)
		{
		}

		// Token: 0x0600A822 RID: 43042 RVA: 0x00289796 File Offset: 0x00287996
		protected NoMatchingUMMailboxPolicyInTargetForestException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A823 RID: 43043 RVA: 0x002897A0 File Offset: 0x002879A0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
