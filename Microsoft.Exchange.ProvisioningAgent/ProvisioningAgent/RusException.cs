using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000068 RID: 104
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RusException : ProvisioningException
	{
		// Token: 0x060002FC RID: 764 RVA: 0x0001158F File Offset: 0x0000F78F
		public RusException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00011598 File Offset: 0x0000F798
		public RusException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000115A2 File Offset: 0x0000F7A2
		protected RusException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000115AC File Offset: 0x0000F7AC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
