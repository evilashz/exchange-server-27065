using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000184 RID: 388
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SpCsomCallException : LocalizedException
	{
		// Token: 0x06000F87 RID: 3975 RVA: 0x00036572 File Offset: 0x00034772
		public SpCsomCallException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0003657B File Offset: 0x0003477B
		public SpCsomCallException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x00036585 File Offset: 0x00034785
		protected SpCsomCallException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0003658F File Offset: 0x0003478F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
