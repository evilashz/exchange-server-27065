using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003D9 RID: 985
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskNetFtProblemException : DagTaskServerException
	{
		// Token: 0x060028A8 RID: 10408 RVA: 0x000B84ED File Offset: 0x000B66ED
		public DagTaskNetFtProblemException(int specificErrorCode) : base(ReplayStrings.DagTaskNetFtProblem(specificErrorCode))
		{
			this.specificErrorCode = specificErrorCode;
		}

		// Token: 0x060028A9 RID: 10409 RVA: 0x000B8507 File Offset: 0x000B6707
		public DagTaskNetFtProblemException(int specificErrorCode, Exception innerException) : base(ReplayStrings.DagTaskNetFtProblem(specificErrorCode), innerException)
		{
			this.specificErrorCode = specificErrorCode;
		}

		// Token: 0x060028AA RID: 10410 RVA: 0x000B8522 File Offset: 0x000B6722
		protected DagTaskNetFtProblemException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.specificErrorCode = (int)info.GetValue("specificErrorCode", typeof(int));
		}

		// Token: 0x060028AB RID: 10411 RVA: 0x000B854C File Offset: 0x000B674C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("specificErrorCode", this.specificErrorCode);
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x060028AC RID: 10412 RVA: 0x000B8567 File Offset: 0x000B6767
		public int SpecificErrorCode
		{
			get
			{
				return this.specificErrorCode;
			}
		}

		// Token: 0x040013EB RID: 5099
		private readonly int specificErrorCode;
	}
}
