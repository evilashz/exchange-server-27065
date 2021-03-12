using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FBD RID: 4029
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class VerificationCodeTooManyFailedException : LocalizedException
	{
		// Token: 0x0600AD88 RID: 44424 RVA: 0x00291D6F File Offset: 0x0028FF6F
		public VerificationCodeTooManyFailedException() : base(Strings.ErrorVerificationCodeTooManyFailed)
		{
		}

		// Token: 0x0600AD89 RID: 44425 RVA: 0x00291D7C File Offset: 0x0028FF7C
		public VerificationCodeTooManyFailedException(Exception innerException) : base(Strings.ErrorVerificationCodeTooManyFailed, innerException)
		{
		}

		// Token: 0x0600AD8A RID: 44426 RVA: 0x00291D8A File Offset: 0x0028FF8A
		protected VerificationCodeTooManyFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AD8B RID: 44427 RVA: 0x00291D94 File Offset: 0x0028FF94
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
