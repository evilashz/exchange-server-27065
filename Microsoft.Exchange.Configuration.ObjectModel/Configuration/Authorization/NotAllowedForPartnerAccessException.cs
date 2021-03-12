using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x020002D8 RID: 728
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NotAllowedForPartnerAccessException : AuthorizationException
	{
		// Token: 0x0600199D RID: 6557 RVA: 0x0005D594 File Offset: 0x0005B794
		public NotAllowedForPartnerAccessException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x0005D59D File Offset: 0x0005B79D
		public NotAllowedForPartnerAccessException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x0005D5A7 File Offset: 0x0005B7A7
		protected NotAllowedForPartnerAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x0005D5B1 File Offset: 0x0005B7B1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
