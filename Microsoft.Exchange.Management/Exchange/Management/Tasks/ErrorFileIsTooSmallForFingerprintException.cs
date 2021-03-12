using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FF5 RID: 4085
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorFileIsTooSmallForFingerprintException : LocalizedException
	{
		// Token: 0x0600AE8C RID: 44684 RVA: 0x002931F4 File Offset: 0x002913F4
		public ErrorFileIsTooSmallForFingerprintException() : base(Strings.ErrorFileIsTooSmallForFingerprint)
		{
		}

		// Token: 0x0600AE8D RID: 44685 RVA: 0x00293201 File Offset: 0x00291401
		public ErrorFileIsTooSmallForFingerprintException(Exception innerException) : base(Strings.ErrorFileIsTooSmallForFingerprint, innerException)
		{
		}

		// Token: 0x0600AE8E RID: 44686 RVA: 0x0029320F File Offset: 0x0029140F
		protected ErrorFileIsTooSmallForFingerprintException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AE8F RID: 44687 RVA: 0x00293219 File Offset: 0x00291419
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
