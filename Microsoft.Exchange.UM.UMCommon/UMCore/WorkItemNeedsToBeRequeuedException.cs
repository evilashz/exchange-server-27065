using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000201 RID: 513
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WorkItemNeedsToBeRequeuedException : TransientException
	{
		// Token: 0x060010C6 RID: 4294 RVA: 0x00039392 File Offset: 0x00037592
		public WorkItemNeedsToBeRequeuedException() : base(Strings.WorkItemNeedsToBeRequeued)
		{
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0003939F File Offset: 0x0003759F
		public WorkItemNeedsToBeRequeuedException(Exception innerException) : base(Strings.WorkItemNeedsToBeRequeued, innerException)
		{
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x000393AD File Offset: 0x000375AD
		protected WorkItemNeedsToBeRequeuedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x000393B7 File Offset: 0x000375B7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
