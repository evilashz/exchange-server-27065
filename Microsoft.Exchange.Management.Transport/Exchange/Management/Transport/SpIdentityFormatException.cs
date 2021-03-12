using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000186 RID: 390
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SpIdentityFormatException : LocalizedException
	{
		// Token: 0x06000F8F RID: 3983 RVA: 0x000365C0 File Offset: 0x000347C0
		public SpIdentityFormatException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x000365C9 File Offset: 0x000347C9
		public SpIdentityFormatException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x000365D3 File Offset: 0x000347D3
		protected SpIdentityFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x000365DD File Offset: 0x000347DD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
