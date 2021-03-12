using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000410 RID: 1040
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SystemProbeException : LocalizedException
	{
		// Token: 0x0600191A RID: 6426 RVA: 0x0005F105 File Offset: 0x0005D305
		public SystemProbeException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x0005F10E File Offset: 0x0005D30E
		public SystemProbeException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x0005F118 File Offset: 0x0005D318
		protected SystemProbeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x0005F122 File Offset: 0x0005D322
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
