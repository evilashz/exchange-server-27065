using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003E2 RID: 994
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskComponentManagerAnotherInstanceRunning : DagTaskServerException
	{
		// Token: 0x060028D7 RID: 10455 RVA: 0x000B8A29 File Offset: 0x000B6C29
		public DagTaskComponentManagerAnotherInstanceRunning() : base(ReplayStrings.DagTaskComponentManagerAnotherInstanceRunning)
		{
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x000B8A3B File Offset: 0x000B6C3B
		public DagTaskComponentManagerAnotherInstanceRunning(Exception innerException) : base(ReplayStrings.DagTaskComponentManagerAnotherInstanceRunning, innerException)
		{
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x000B8A4E File Offset: 0x000B6C4E
		protected DagTaskComponentManagerAnotherInstanceRunning(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x000B8A58 File Offset: 0x000B6C58
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
