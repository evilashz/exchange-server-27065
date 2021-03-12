using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Provisioning
{
	// Token: 0x020002C6 RID: 710
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ProvisioningBrokerException : ProvisioningException
	{
		// Token: 0x06001948 RID: 6472 RVA: 0x0005CE97 File Offset: 0x0005B097
		public ProvisioningBrokerException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x0005CEA0 File Offset: 0x0005B0A0
		public ProvisioningBrokerException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x0005CEAA File Offset: 0x0005B0AA
		protected ProvisioningBrokerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x0005CEB4 File Offset: 0x0005B0B4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
