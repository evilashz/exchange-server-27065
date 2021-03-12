using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F97 RID: 3991
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SendConnectorDashdashAddressSpaceNotOffEdgeException : LocalizedException
	{
		// Token: 0x0600ACC9 RID: 44233 RVA: 0x00290ACB File Offset: 0x0028ECCB
		public SendConnectorDashdashAddressSpaceNotOffEdgeException() : base(Strings.SendConnectorDashdashAddressSpaceNotOffEdge)
		{
		}

		// Token: 0x0600ACCA RID: 44234 RVA: 0x00290AD8 File Offset: 0x0028ECD8
		public SendConnectorDashdashAddressSpaceNotOffEdgeException(Exception innerException) : base(Strings.SendConnectorDashdashAddressSpaceNotOffEdge, innerException)
		{
		}

		// Token: 0x0600ACCB RID: 44235 RVA: 0x00290AE6 File Offset: 0x0028ECE6
		protected SendConnectorDashdashAddressSpaceNotOffEdgeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ACCC RID: 44236 RVA: 0x00290AF0 File Offset: 0x0028ECF0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
