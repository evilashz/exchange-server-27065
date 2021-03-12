using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000202 RID: 514
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedQueueingWorkItemException : TransientException
	{
		// Token: 0x060010CA RID: 4298 RVA: 0x000393C1 File Offset: 0x000375C1
		public FailedQueueingWorkItemException() : base(Strings.FailedQueueingWorkItemException)
		{
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x000393CE File Offset: 0x000375CE
		public FailedQueueingWorkItemException(Exception innerException) : base(Strings.FailedQueueingWorkItemException, innerException)
		{
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x000393DC File Offset: 0x000375DC
		protected FailedQueueingWorkItemException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x000393E6 File Offset: 0x000375E6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
