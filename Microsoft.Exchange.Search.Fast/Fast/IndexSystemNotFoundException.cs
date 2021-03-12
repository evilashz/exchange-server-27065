using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x02000038 RID: 56
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class IndexSystemNotFoundException : ComponentFailedPermanentException
	{
		// Token: 0x060002AD RID: 685 RVA: 0x0000F436 File Offset: 0x0000D636
		public IndexSystemNotFoundException(string flowName) : base(Strings.IndexSystemForFlowDoesNotExist(flowName))
		{
			this.flowName = flowName;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000F44B File Offset: 0x0000D64B
		public IndexSystemNotFoundException(string flowName, Exception innerException) : base(Strings.IndexSystemForFlowDoesNotExist(flowName), innerException)
		{
			this.flowName = flowName;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000F461 File Offset: 0x0000D661
		protected IndexSystemNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.flowName = (string)info.GetValue("flowName", typeof(string));
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000F48B File Offset: 0x0000D68B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("flowName", this.flowName);
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000F4A6 File Offset: 0x0000D6A6
		public string FlowName
		{
			get
			{
				return this.flowName;
			}
		}

		// Token: 0x04000148 RID: 328
		private readonly string flowName;
	}
}
