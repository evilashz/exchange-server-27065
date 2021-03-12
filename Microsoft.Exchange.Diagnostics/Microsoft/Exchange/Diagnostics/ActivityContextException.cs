using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000418 RID: 1048
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ActivityContextException : LocalizedException
	{
		// Token: 0x06001950 RID: 6480 RVA: 0x0005F8A4 File Offset: 0x0005DAA4
		public ActivityContextException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x0005F8AD File Offset: 0x0005DAAD
		public ActivityContextException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x0005F8B7 File Offset: 0x0005DAB7
		protected ActivityContextException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x0005F8C1 File Offset: 0x0005DAC1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
