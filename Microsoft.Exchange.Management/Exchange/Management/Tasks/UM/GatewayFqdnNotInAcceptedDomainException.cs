using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011EC RID: 4588
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GatewayFqdnNotInAcceptedDomainException : LocalizedException
	{
		// Token: 0x0600B979 RID: 47481 RVA: 0x002A5FDA File Offset: 0x002A41DA
		public GatewayFqdnNotInAcceptedDomainException() : base(Strings.GatewayFqdnNotInAcceptedDomain)
		{
		}

		// Token: 0x0600B97A RID: 47482 RVA: 0x002A5FE7 File Offset: 0x002A41E7
		public GatewayFqdnNotInAcceptedDomainException(Exception innerException) : base(Strings.GatewayFqdnNotInAcceptedDomain, innerException)
		{
		}

		// Token: 0x0600B97B RID: 47483 RVA: 0x002A5FF5 File Offset: 0x002A41F5
		protected GatewayFqdnNotInAcceptedDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B97C RID: 47484 RVA: 0x002A5FFF File Offset: 0x002A41FF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
