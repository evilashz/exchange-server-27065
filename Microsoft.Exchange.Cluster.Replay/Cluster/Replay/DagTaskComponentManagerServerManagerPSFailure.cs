using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003E1 RID: 993
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskComponentManagerServerManagerPSFailure : DagTaskServerException
	{
		// Token: 0x060028D2 RID: 10450 RVA: 0x000B89A7 File Offset: 0x000B6BA7
		public DagTaskComponentManagerServerManagerPSFailure(string error) : base(ReplayStrings.DagTaskComponentManagerServerManagerPSFailure(error))
		{
			this.error = error;
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x000B89C1 File Offset: 0x000B6BC1
		public DagTaskComponentManagerServerManagerPSFailure(string error, Exception innerException) : base(ReplayStrings.DagTaskComponentManagerServerManagerPSFailure(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x000B89DC File Offset: 0x000B6BDC
		protected DagTaskComponentManagerServerManagerPSFailure(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x000B8A06 File Offset: 0x000B6C06
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x060028D6 RID: 10454 RVA: 0x000B8A21 File Offset: 0x000B6C21
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x040013F5 RID: 5109
		private readonly string error;
	}
}
