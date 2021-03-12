using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FFC RID: 4092
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorFailedExtractTextForFingerprintException : LocalizedException
	{
		// Token: 0x0600AEAB RID: 44715 RVA: 0x00293425 File Offset: 0x00291625
		public ErrorFailedExtractTextForFingerprintException() : base(Strings.ErrorFailedExtractTextForFingerprint)
		{
		}

		// Token: 0x0600AEAC RID: 44716 RVA: 0x00293432 File Offset: 0x00291632
		public ErrorFailedExtractTextForFingerprintException(Exception innerException) : base(Strings.ErrorFailedExtractTextForFingerprint, innerException)
		{
		}

		// Token: 0x0600AEAD RID: 44717 RVA: 0x00293440 File Offset: 0x00291640
		protected ErrorFailedExtractTextForFingerprintException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AEAE RID: 44718 RVA: 0x0029344A File Offset: 0x0029164A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
