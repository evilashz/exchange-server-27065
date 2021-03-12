using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore.OCS
{
	// Token: 0x020001FB RID: 507
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NotificationEventFormatException : NotificationEventException
	{
		// Token: 0x060010AD RID: 4269 RVA: 0x0003922F File Offset: 0x0003742F
		public NotificationEventFormatException() : base(Strings.NotificationEventFormatException)
		{
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x0003923C File Offset: 0x0003743C
		public NotificationEventFormatException(Exception innerException) : base(Strings.NotificationEventFormatException, innerException)
		{
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0003924A File Offset: 0x0003744A
		protected NotificationEventFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00039254 File Offset: 0x00037454
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
