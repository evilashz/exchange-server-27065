using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F28 RID: 3880
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TopologyServiceMissingDC : LocalizedException
	{
		// Token: 0x0600AABD RID: 43709 RVA: 0x0028DF15 File Offset: 0x0028C115
		public TopologyServiceMissingDC(string operationType) : base(Strings.messageTopologyServiceMissingDCExceptionThrown(operationType))
		{
			this.operationType = operationType;
		}

		// Token: 0x0600AABE RID: 43710 RVA: 0x0028DF2A File Offset: 0x0028C12A
		public TopologyServiceMissingDC(string operationType, Exception innerException) : base(Strings.messageTopologyServiceMissingDCExceptionThrown(operationType), innerException)
		{
			this.operationType = operationType;
		}

		// Token: 0x0600AABF RID: 43711 RVA: 0x0028DF40 File Offset: 0x0028C140
		protected TopologyServiceMissingDC(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.operationType = (string)info.GetValue("operationType", typeof(string));
		}

		// Token: 0x0600AAC0 RID: 43712 RVA: 0x0028DF6A File Offset: 0x0028C16A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("operationType", this.operationType);
		}

		// Token: 0x1700373A RID: 14138
		// (get) Token: 0x0600AAC1 RID: 43713 RVA: 0x0028DF85 File Offset: 0x0028C185
		public string OperationType
		{
			get
			{
				return this.operationType;
			}
		}

		// Token: 0x040060A0 RID: 24736
		private readonly string operationType;
	}
}
