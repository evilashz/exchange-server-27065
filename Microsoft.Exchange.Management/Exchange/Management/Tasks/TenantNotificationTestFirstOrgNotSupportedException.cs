using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200113F RID: 4415
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TenantNotificationTestFirstOrgNotSupportedException : LocalizedException
	{
		// Token: 0x0600B520 RID: 46368 RVA: 0x0029DB79 File Offset: 0x0029BD79
		public TenantNotificationTestFirstOrgNotSupportedException() : base(Strings.TenantNotificationTestFirstOrgNotSupported)
		{
		}

		// Token: 0x0600B521 RID: 46369 RVA: 0x0029DB86 File Offset: 0x0029BD86
		public TenantNotificationTestFirstOrgNotSupportedException(Exception innerException) : base(Strings.TenantNotificationTestFirstOrgNotSupported, innerException)
		{
		}

		// Token: 0x0600B522 RID: 46370 RVA: 0x0029DB94 File Offset: 0x0029BD94
		protected TenantNotificationTestFirstOrgNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B523 RID: 46371 RVA: 0x0029DB9E File Offset: 0x0029BD9E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
