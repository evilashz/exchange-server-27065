using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x02000006 RID: 6
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LiveConfigurationException : LocalizedException
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000027F9 File Offset: 0x000009F9
		public LiveConfigurationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002802 File Offset: 0x00000A02
		public LiveConfigurationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000280C File Offset: 0x00000A0C
		protected LiveConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002816 File Offset: 0x00000A16
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
