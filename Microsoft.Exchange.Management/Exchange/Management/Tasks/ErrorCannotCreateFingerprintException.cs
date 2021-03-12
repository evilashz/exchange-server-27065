using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FFB RID: 4091
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorCannotCreateFingerprintException : LocalizedException
	{
		// Token: 0x0600AEA7 RID: 44711 RVA: 0x002933F6 File Offset: 0x002915F6
		public ErrorCannotCreateFingerprintException() : base(Strings.ErrorCannotCreateFingerprint)
		{
		}

		// Token: 0x0600AEA8 RID: 44712 RVA: 0x00293403 File Offset: 0x00291603
		public ErrorCannotCreateFingerprintException(Exception innerException) : base(Strings.ErrorCannotCreateFingerprint, innerException)
		{
		}

		// Token: 0x0600AEA9 RID: 44713 RVA: 0x00293411 File Offset: 0x00291611
		protected ErrorCannotCreateFingerprintException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AEAA RID: 44714 RVA: 0x0029341B File Offset: 0x0029161B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
