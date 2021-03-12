using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003D7 RID: 983
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskOperationFailedWithEcException : DagTaskServerException
	{
		// Token: 0x0600289D RID: 10397 RVA: 0x000B8392 File Offset: 0x000B6592
		public DagTaskOperationFailedWithEcException(int ec) : base(ReplayStrings.DagTaskOperationFailedWithEcException(ec))
		{
			this.ec = ec;
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x000B83AC File Offset: 0x000B65AC
		public DagTaskOperationFailedWithEcException(int ec, Exception innerException) : base(ReplayStrings.DagTaskOperationFailedWithEcException(ec), innerException)
		{
			this.ec = ec;
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x000B83C7 File Offset: 0x000B65C7
		protected DagTaskOperationFailedWithEcException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ec = (int)info.GetValue("ec", typeof(int));
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x000B83F1 File Offset: 0x000B65F1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ec", this.ec);
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x060028A1 RID: 10401 RVA: 0x000B840C File Offset: 0x000B660C
		public int Ec
		{
			get
			{
				return this.ec;
			}
		}

		// Token: 0x040013E8 RID: 5096
		private readonly int ec;
	}
}
