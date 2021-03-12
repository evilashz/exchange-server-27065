using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FFA RID: 4090
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorCannotLoadFingerprintCreatorException : LocalizedException
	{
		// Token: 0x0600AEA3 RID: 44707 RVA: 0x002933C7 File Offset: 0x002915C7
		public ErrorCannotLoadFingerprintCreatorException() : base(Strings.ErrorCannotLoadFingerprintCreator)
		{
		}

		// Token: 0x0600AEA4 RID: 44708 RVA: 0x002933D4 File Offset: 0x002915D4
		public ErrorCannotLoadFingerprintCreatorException(Exception innerException) : base(Strings.ErrorCannotLoadFingerprintCreator, innerException)
		{
		}

		// Token: 0x0600AEA5 RID: 44709 RVA: 0x002933E2 File Offset: 0x002915E2
		protected ErrorCannotLoadFingerprintCreatorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AEA6 RID: 44710 RVA: 0x002933EC File Offset: 0x002915EC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
