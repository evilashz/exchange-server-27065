using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration
{
	// Token: 0x020002E1 RID: 737
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PswsProxyException : LocalizedException
	{
		// Token: 0x060019CA RID: 6602 RVA: 0x0005D9D6 File Offset: 0x0005BBD6
		public PswsProxyException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x0005D9DF File Offset: 0x0005BBDF
		public PswsProxyException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x0005D9E9 File Offset: 0x0005BBE9
		protected PswsProxyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x0005D9F3 File Offset: 0x0005BBF3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
