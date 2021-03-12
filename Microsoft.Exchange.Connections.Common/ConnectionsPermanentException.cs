using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000031 RID: 49
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ConnectionsPermanentException : LocalizedException
	{
		// Token: 0x060000ED RID: 237 RVA: 0x0000375B File Offset: 0x0000195B
		public ConnectionsPermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003764 File Offset: 0x00001964
		public ConnectionsPermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000376E File Offset: 0x0000196E
		protected ConnectionsPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003778 File Offset: 0x00001978
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
