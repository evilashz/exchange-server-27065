using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x02000007 RID: 7
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LiveTransientException : TransientException
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002820 File Offset: 0x00000A20
		public LiveTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002829 File Offset: 0x00000A29
		public LiveTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002833 File Offset: 0x00000A33
		protected LiveTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000283D File Offset: 0x00000A3D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
