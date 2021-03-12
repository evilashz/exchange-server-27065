using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F7F RID: 3967
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidTransportRoleOnReceiveConnectorException : LocalizedException
	{
		// Token: 0x0600AC5D RID: 44125 RVA: 0x002902F7 File Offset: 0x0028E4F7
		public InvalidTransportRoleOnReceiveConnectorException() : base(Strings.InvalidTransportRoleOnReceiveConnector)
		{
		}

		// Token: 0x0600AC5E RID: 44126 RVA: 0x00290304 File Offset: 0x0028E504
		public InvalidTransportRoleOnReceiveConnectorException(Exception innerException) : base(Strings.InvalidTransportRoleOnReceiveConnector, innerException)
		{
		}

		// Token: 0x0600AC5F RID: 44127 RVA: 0x00290312 File Offset: 0x0028E512
		protected InvalidTransportRoleOnReceiveConnectorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AC60 RID: 44128 RVA: 0x0029031C File Offset: 0x0028E51C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
