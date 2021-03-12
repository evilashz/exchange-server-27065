using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001012 RID: 4114
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TenantScopedInboundConnectorRequiredForCMCConnectorException : LocalizedException
	{
		// Token: 0x0600AF16 RID: 44822 RVA: 0x00293DDF File Offset: 0x00291FDF
		public TenantScopedInboundConnectorRequiredForCMCConnectorException(string name) : base(Strings.TenantScopedInboundConnectorRequiredForCMCConnector(name))
		{
			this.name = name;
		}

		// Token: 0x0600AF17 RID: 44823 RVA: 0x00293DF4 File Offset: 0x00291FF4
		public TenantScopedInboundConnectorRequiredForCMCConnectorException(string name, Exception innerException) : base(Strings.TenantScopedInboundConnectorRequiredForCMCConnector(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600AF18 RID: 44824 RVA: 0x00293E0A File Offset: 0x0029200A
		protected TenantScopedInboundConnectorRequiredForCMCConnectorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600AF19 RID: 44825 RVA: 0x00293E34 File Offset: 0x00292034
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x170037EB RID: 14315
		// (get) Token: 0x0600AF1A RID: 44826 RVA: 0x00293E4F File Offset: 0x0029204F
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04006151 RID: 24913
		private readonly string name;
	}
}
