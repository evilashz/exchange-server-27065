using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F72 RID: 3954
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReceiveConnectorRoleConflictException : LocalizedException
	{
		// Token: 0x0600AC26 RID: 44070 RVA: 0x0028FFAE File Offset: 0x0028E1AE
		public ReceiveConnectorRoleConflictException(string receiveConnectorId) : base(Strings.ReceiveConnectorRoleConflict(receiveConnectorId))
		{
			this.receiveConnectorId = receiveConnectorId;
		}

		// Token: 0x0600AC27 RID: 44071 RVA: 0x0028FFC3 File Offset: 0x0028E1C3
		public ReceiveConnectorRoleConflictException(string receiveConnectorId, Exception innerException) : base(Strings.ReceiveConnectorRoleConflict(receiveConnectorId), innerException)
		{
			this.receiveConnectorId = receiveConnectorId;
		}

		// Token: 0x0600AC28 RID: 44072 RVA: 0x0028FFD9 File Offset: 0x0028E1D9
		protected ReceiveConnectorRoleConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.receiveConnectorId = (string)info.GetValue("receiveConnectorId", typeof(string));
		}

		// Token: 0x0600AC29 RID: 44073 RVA: 0x00290003 File Offset: 0x0028E203
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("receiveConnectorId", this.receiveConnectorId);
		}

		// Token: 0x1700377B RID: 14203
		// (get) Token: 0x0600AC2A RID: 44074 RVA: 0x0029001E File Offset: 0x0028E21E
		public string ReceiveConnectorId
		{
			get
			{
				return this.receiveConnectorId;
			}
		}

		// Token: 0x040060E1 RID: 24801
		private readonly string receiveConnectorId;
	}
}
