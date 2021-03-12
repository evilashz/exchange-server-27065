using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000185 RID: 389
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SpValidatorException : LocalizedException
	{
		// Token: 0x06000F8B RID: 3979 RVA: 0x00036599 File Offset: 0x00034799
		public SpValidatorException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x000365A2 File Offset: 0x000347A2
		public SpValidatorException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x000365AC File Offset: 0x000347AC
		protected SpValidatorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x000365B6 File Offset: 0x000347B6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
