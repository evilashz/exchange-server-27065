using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F96 RID: 3990
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SendConnectorRgcNotFoundException : LocalizedException
	{
		// Token: 0x0600ACC4 RID: 44228 RVA: 0x00290A53 File Offset: 0x0028EC53
		public SendConnectorRgcNotFoundException(string connectorDn) : base(Strings.SendConnectorRgcNotFound(connectorDn))
		{
			this.connectorDn = connectorDn;
		}

		// Token: 0x0600ACC5 RID: 44229 RVA: 0x00290A68 File Offset: 0x0028EC68
		public SendConnectorRgcNotFoundException(string connectorDn, Exception innerException) : base(Strings.SendConnectorRgcNotFound(connectorDn), innerException)
		{
			this.connectorDn = connectorDn;
		}

		// Token: 0x0600ACC6 RID: 44230 RVA: 0x00290A7E File Offset: 0x0028EC7E
		protected SendConnectorRgcNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.connectorDn = (string)info.GetValue("connectorDn", typeof(string));
		}

		// Token: 0x0600ACC7 RID: 44231 RVA: 0x00290AA8 File Offset: 0x0028ECA8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("connectorDn", this.connectorDn);
		}

		// Token: 0x17003789 RID: 14217
		// (get) Token: 0x0600ACC8 RID: 44232 RVA: 0x00290AC3 File Offset: 0x0028ECC3
		public string ConnectorDn
		{
			get
			{
				return this.connectorDn;
			}
		}

		// Token: 0x040060EF RID: 24815
		private readonly string connectorDn;
	}
}
