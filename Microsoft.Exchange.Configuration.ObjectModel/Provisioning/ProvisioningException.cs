using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Provisioning
{
	// Token: 0x020002C5 RID: 709
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ProvisioningException : LocalizedException
	{
		// Token: 0x06001944 RID: 6468 RVA: 0x0005CE70 File Offset: 0x0005B070
		public ProvisioningException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x0005CE79 File Offset: 0x0005B079
		public ProvisioningException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x0005CE83 File Offset: 0x0005B083
		protected ProvisioningException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x0005CE8D File Offset: 0x0005B08D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
