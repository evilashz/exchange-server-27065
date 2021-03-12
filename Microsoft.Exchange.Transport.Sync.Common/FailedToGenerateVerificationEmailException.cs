using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000015 RID: 21
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FailedToGenerateVerificationEmailException : LocalizedException
	{
		// Token: 0x06000117 RID: 279 RVA: 0x00004F25 File Offset: 0x00003125
		public FailedToGenerateVerificationEmailException() : base(Strings.FailedToGenerateVerificationEmail)
		{
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004F32 File Offset: 0x00003132
		public FailedToGenerateVerificationEmailException(Exception innerException) : base(Strings.FailedToGenerateVerificationEmail, innerException)
		{
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004F40 File Offset: 0x00003140
		protected FailedToGenerateVerificationEmailException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004F4A File Offset: 0x0000314A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
