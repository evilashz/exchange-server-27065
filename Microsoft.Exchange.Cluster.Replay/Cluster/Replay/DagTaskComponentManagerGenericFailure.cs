using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003DF RID: 991
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskComponentManagerGenericFailure : DagTaskServerException
	{
		// Token: 0x060028C8 RID: 10440 RVA: 0x000B88A3 File Offset: 0x000B6AA3
		public DagTaskComponentManagerGenericFailure(int error) : base(ReplayStrings.DagTaskComponentManagerGenericFailure(error))
		{
			this.error = error;
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x000B88BD File Offset: 0x000B6ABD
		public DagTaskComponentManagerGenericFailure(int error, Exception innerException) : base(ReplayStrings.DagTaskComponentManagerGenericFailure(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x000B88D8 File Offset: 0x000B6AD8
		protected DagTaskComponentManagerGenericFailure(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (int)info.GetValue("error", typeof(int));
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x000B8902 File Offset: 0x000B6B02
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x060028CC RID: 10444 RVA: 0x000B891D File Offset: 0x000B6B1D
		public int Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x040013F3 RID: 5107
		private readonly int error;
	}
}
