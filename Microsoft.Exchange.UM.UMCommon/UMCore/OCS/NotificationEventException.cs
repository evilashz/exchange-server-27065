using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.UM.UMCore.OCS
{
	// Token: 0x020001FA RID: 506
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NotificationEventException : LocalizedException
	{
		// Token: 0x060010A9 RID: 4265 RVA: 0x00039208 File Offset: 0x00037408
		public NotificationEventException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00039211 File Offset: 0x00037411
		public NotificationEventException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x0003921B File Offset: 0x0003741B
		protected NotificationEventException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x00039225 File Offset: 0x00037425
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
