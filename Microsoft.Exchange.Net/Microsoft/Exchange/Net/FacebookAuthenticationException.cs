using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net
{
	// Token: 0x020000F7 RID: 247
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FacebookAuthenticationException : LocalizedException
	{
		// Token: 0x0600066E RID: 1646 RVA: 0x00016896 File Offset: 0x00014A96
		public FacebookAuthenticationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001689F File Offset: 0x00014A9F
		public FacebookAuthenticationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x000168A9 File Offset: 0x00014AA9
		protected FacebookAuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x000168B3 File Offset: 0x00014AB3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
