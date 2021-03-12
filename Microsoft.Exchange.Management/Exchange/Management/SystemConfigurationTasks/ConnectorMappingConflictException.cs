using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F71 RID: 3953
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConnectorMappingConflictException : LocalizedException
	{
		// Token: 0x0600AC21 RID: 44065 RVA: 0x0028FF36 File Offset: 0x0028E136
		public ConnectorMappingConflictException(string receiveConnectorId) : base(Strings.ReceiveConnectorMappingConflict(receiveConnectorId))
		{
			this.receiveConnectorId = receiveConnectorId;
		}

		// Token: 0x0600AC22 RID: 44066 RVA: 0x0028FF4B File Offset: 0x0028E14B
		public ConnectorMappingConflictException(string receiveConnectorId, Exception innerException) : base(Strings.ReceiveConnectorMappingConflict(receiveConnectorId), innerException)
		{
			this.receiveConnectorId = receiveConnectorId;
		}

		// Token: 0x0600AC23 RID: 44067 RVA: 0x0028FF61 File Offset: 0x0028E161
		protected ConnectorMappingConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.receiveConnectorId = (string)info.GetValue("receiveConnectorId", typeof(string));
		}

		// Token: 0x0600AC24 RID: 44068 RVA: 0x0028FF8B File Offset: 0x0028E18B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("receiveConnectorId", this.receiveConnectorId);
		}

		// Token: 0x1700377A RID: 14202
		// (get) Token: 0x0600AC25 RID: 44069 RVA: 0x0028FFA6 File Offset: 0x0028E1A6
		public string ReceiveConnectorId
		{
			get
			{
				return this.receiveConnectorId;
			}
		}

		// Token: 0x040060E0 RID: 24800
		private readonly string receiveConnectorId;
	}
}
