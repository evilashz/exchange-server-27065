using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x02000008 RID: 8
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LiveExternalException : LocalizedException
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002847 File Offset: 0x00000A47
		public LiveExternalException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002850 File Offset: 0x00000A50
		public LiveExternalException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000285A File Offset: 0x00000A5A
		protected LiveExternalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002864 File Offset: 0x00000A64
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
