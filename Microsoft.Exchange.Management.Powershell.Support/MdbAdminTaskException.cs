using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000053 RID: 83
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MdbAdminTaskException : LocalizedException
	{
		// Token: 0x06000415 RID: 1045 RVA: 0x00011342 File Offset: 0x0000F542
		public MdbAdminTaskException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001134B File Offset: 0x0000F54B
		public MdbAdminTaskException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00011355 File Offset: 0x0000F555
		protected MdbAdminTaskException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001135F File Offset: 0x0000F55F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
