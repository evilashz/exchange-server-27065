using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000209 RID: 521
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class OutboundCallCancelledException : LocalizedException
	{
		// Token: 0x060010E9 RID: 4329 RVA: 0x000395E5 File Offset: 0x000377E5
		public OutboundCallCancelledException() : base(Strings.OutboundCallCancelled)
		{
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x000395F2 File Offset: 0x000377F2
		public OutboundCallCancelledException(Exception innerException) : base(Strings.OutboundCallCancelled, innerException)
		{
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00039600 File Offset: 0x00037800
		protected OutboundCallCancelledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0003960A File Offset: 0x0003780A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
