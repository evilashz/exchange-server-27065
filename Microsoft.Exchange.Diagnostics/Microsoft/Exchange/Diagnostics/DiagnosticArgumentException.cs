using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000419 RID: 1049
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DiagnosticArgumentException : LocalizedException
	{
		// Token: 0x06001954 RID: 6484 RVA: 0x0005F8CB File Offset: 0x0005DACB
		public DiagnosticArgumentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x0005F8D4 File Offset: 0x0005DAD4
		public DiagnosticArgumentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x0005F8DE File Offset: 0x0005DADE
		protected DiagnosticArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x0005F8E8 File Offset: 0x0005DAE8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
