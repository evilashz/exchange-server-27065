using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000EE3 RID: 3811
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotEnableRoleGroupException : LocalizedException
	{
		// Token: 0x0600A94C RID: 43340 RVA: 0x0028B666 File Offset: 0x00289866
		public CannotEnableRoleGroupException() : base(Strings.ErrorEnableRoleGroup)
		{
		}

		// Token: 0x0600A94D RID: 43341 RVA: 0x0028B673 File Offset: 0x00289873
		public CannotEnableRoleGroupException(Exception innerException) : base(Strings.ErrorEnableRoleGroup, innerException)
		{
		}

		// Token: 0x0600A94E RID: 43342 RVA: 0x0028B681 File Offset: 0x00289881
		protected CannotEnableRoleGroupException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A94F RID: 43343 RVA: 0x0028B68B File Offset: 0x0028988B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
