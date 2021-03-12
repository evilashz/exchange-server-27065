using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003E3 RID: 995
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskSetDagNeedsAllNodesUpToChangeQuorumException : DagTaskServerException
	{
		// Token: 0x060028DB RID: 10459 RVA: 0x000B8A62 File Offset: 0x000B6C62
		public DagTaskSetDagNeedsAllNodesUpToChangeQuorumException(string machineNames) : base(ReplayStrings.DagTaskSetDagNeedsAllNodesUpToChangeQuorumException(machineNames))
		{
			this.machineNames = machineNames;
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x000B8A7C File Offset: 0x000B6C7C
		public DagTaskSetDagNeedsAllNodesUpToChangeQuorumException(string machineNames, Exception innerException) : base(ReplayStrings.DagTaskSetDagNeedsAllNodesUpToChangeQuorumException(machineNames), innerException)
		{
			this.machineNames = machineNames;
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x000B8A97 File Offset: 0x000B6C97
		protected DagTaskSetDagNeedsAllNodesUpToChangeQuorumException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.machineNames = (string)info.GetValue("machineNames", typeof(string));
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x000B8AC1 File Offset: 0x000B6CC1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("machineNames", this.machineNames);
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x060028DF RID: 10463 RVA: 0x000B8ADC File Offset: 0x000B6CDC
		public string MachineNames
		{
			get
			{
				return this.machineNames;
			}
		}

		// Token: 0x040013F6 RID: 5110
		private readonly string machineNames;
	}
}
