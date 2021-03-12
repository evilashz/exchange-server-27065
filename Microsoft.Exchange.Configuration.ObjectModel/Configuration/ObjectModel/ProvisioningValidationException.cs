using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x020002AC RID: 684
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ProvisioningValidationException : LocalizedException
	{
		// Token: 0x060018D1 RID: 6353 RVA: 0x0005C5A9 File Offset: 0x0005A7A9
		public ProvisioningValidationException(string description, string agentName) : base(Strings.ErrorProvisioningValidation(description, agentName))
		{
			this.description = description;
			this.agentName = agentName;
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x0005C5C6 File Offset: 0x0005A7C6
		public ProvisioningValidationException(string description, string agentName, Exception innerException) : base(Strings.ErrorProvisioningValidation(description, agentName), innerException)
		{
			this.description = description;
			this.agentName = agentName;
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x0005C5E4 File Offset: 0x0005A7E4
		protected ProvisioningValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.description = (string)info.GetValue("description", typeof(string));
			this.agentName = (string)info.GetValue("agentName", typeof(string));
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x0005C639 File Offset: 0x0005A839
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("description", this.description);
			info.AddValue("agentName", this.agentName);
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060018D5 RID: 6357 RVA: 0x0005C665 File Offset: 0x0005A865
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060018D6 RID: 6358 RVA: 0x0005C66D File Offset: 0x0005A86D
		public string AgentName
		{
			get
			{
				return this.agentName;
			}
		}

		// Token: 0x04000986 RID: 2438
		private readonly string description;

		// Token: 0x04000987 RID: 2439
		private readonly string agentName;
	}
}
