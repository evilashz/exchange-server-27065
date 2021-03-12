using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020010BE RID: 4286
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ProvisioningFederatedExchangeException : FederationException
	{
		// Token: 0x0600B2A2 RID: 45730 RVA: 0x00299F2A File Offset: 0x0029812A
		public ProvisioningFederatedExchangeException(string details) : base(Strings.ErrorProvisioningFederatedExchange(details))
		{
			this.details = details;
		}

		// Token: 0x0600B2A3 RID: 45731 RVA: 0x00299F3F File Offset: 0x0029813F
		public ProvisioningFederatedExchangeException(string details, Exception innerException) : base(Strings.ErrorProvisioningFederatedExchange(details), innerException)
		{
			this.details = details;
		}

		// Token: 0x0600B2A4 RID: 45732 RVA: 0x00299F55 File Offset: 0x00298155
		protected ProvisioningFederatedExchangeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.details = (string)info.GetValue("details", typeof(string));
		}

		// Token: 0x0600B2A5 RID: 45733 RVA: 0x00299F7F File Offset: 0x0029817F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("details", this.details);
		}

		// Token: 0x170038C7 RID: 14535
		// (get) Token: 0x0600B2A6 RID: 45734 RVA: 0x00299F9A File Offset: 0x0029819A
		public string Details
		{
			get
			{
				return this.details;
			}
		}

		// Token: 0x0400622D RID: 25133
		private readonly string details;
	}
}
