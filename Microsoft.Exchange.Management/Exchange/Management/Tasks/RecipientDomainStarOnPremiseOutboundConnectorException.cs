using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001015 RID: 4117
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RecipientDomainStarOnPremiseOutboundConnectorException : LocalizedException
	{
		// Token: 0x0600AF24 RID: 44836 RVA: 0x00293EFE File Offset: 0x002920FE
		public RecipientDomainStarOnPremiseOutboundConnectorException() : base(Strings.RecipientDomainStarOnPremiseOutboundConnector)
		{
		}

		// Token: 0x0600AF25 RID: 44837 RVA: 0x00293F0B File Offset: 0x0029210B
		public RecipientDomainStarOnPremiseOutboundConnectorException(Exception innerException) : base(Strings.RecipientDomainStarOnPremiseOutboundConnector, innerException)
		{
		}

		// Token: 0x0600AF26 RID: 44838 RVA: 0x00293F19 File Offset: 0x00292119
		protected RecipientDomainStarOnPremiseOutboundConnectorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AF27 RID: 44839 RVA: 0x00293F23 File Offset: 0x00292123
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
