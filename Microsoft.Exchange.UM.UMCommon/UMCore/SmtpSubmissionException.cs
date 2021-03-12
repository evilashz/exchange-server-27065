using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001FF RID: 511
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SmtpSubmissionException : TransientException
	{
		// Token: 0x060010BE RID: 4286 RVA: 0x00039334 File Offset: 0x00037534
		public SmtpSubmissionException() : base(Strings.SmtpSubmissionFailed)
		{
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x00039341 File Offset: 0x00037541
		public SmtpSubmissionException(Exception innerException) : base(Strings.SmtpSubmissionFailed, innerException)
		{
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x0003934F File Offset: 0x0003754F
		protected SmtpSubmissionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x00039359 File Offset: 0x00037559
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
