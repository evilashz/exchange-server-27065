using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001FE RID: 510
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoHubTransportServerAvailableException : TransientException
	{
		// Token: 0x060010BA RID: 4282 RVA: 0x00039305 File Offset: 0x00037505
		public NoHubTransportServerAvailableException() : base(Strings.SmtpSubmissionFailed)
		{
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x00039312 File Offset: 0x00037512
		public NoHubTransportServerAvailableException(Exception innerException) : base(Strings.SmtpSubmissionFailed, innerException)
		{
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x00039320 File Offset: 0x00037520
		protected NoHubTransportServerAvailableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x0003932A File Offset: 0x0003752A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
