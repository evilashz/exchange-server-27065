using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001014 RID: 4116
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CMCConnectorRequiresTenantScopedInboundConnectorException : LocalizedException
	{
		// Token: 0x0600AF20 RID: 44832 RVA: 0x00293ECF File Offset: 0x002920CF
		public CMCConnectorRequiresTenantScopedInboundConnectorException() : base(Strings.CMCConnectorRequiresTenantScopedInboundConnector)
		{
		}

		// Token: 0x0600AF21 RID: 44833 RVA: 0x00293EDC File Offset: 0x002920DC
		public CMCConnectorRequiresTenantScopedInboundConnectorException(Exception innerException) : base(Strings.CMCConnectorRequiresTenantScopedInboundConnector, innerException)
		{
		}

		// Token: 0x0600AF22 RID: 44834 RVA: 0x00293EEA File Offset: 0x002920EA
		protected CMCConnectorRequiresTenantScopedInboundConnectorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AF23 RID: 44835 RVA: 0x00293EF4 File Offset: 0x002920F4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
