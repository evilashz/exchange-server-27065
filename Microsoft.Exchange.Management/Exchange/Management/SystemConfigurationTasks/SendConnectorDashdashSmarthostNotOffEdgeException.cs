using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F99 RID: 3993
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SendConnectorDashdashSmarthostNotOffEdgeException : LocalizedException
	{
		// Token: 0x0600ACD1 RID: 44241 RVA: 0x00290B29 File Offset: 0x0028ED29
		public SendConnectorDashdashSmarthostNotOffEdgeException() : base(Strings.SendConnectorDashdashSmarthostNotOffEdge)
		{
		}

		// Token: 0x0600ACD2 RID: 44242 RVA: 0x00290B36 File Offset: 0x0028ED36
		public SendConnectorDashdashSmarthostNotOffEdgeException(Exception innerException) : base(Strings.SendConnectorDashdashSmarthostNotOffEdge, innerException)
		{
		}

		// Token: 0x0600ACD3 RID: 44243 RVA: 0x00290B44 File Offset: 0x0028ED44
		protected SendConnectorDashdashSmarthostNotOffEdgeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ACD4 RID: 44244 RVA: 0x00290B4E File Offset: 0x0028ED4E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
