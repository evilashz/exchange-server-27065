using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003E5 RID: 997
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskRemoveNodeNotUpException : DagTaskServerException
	{
		// Token: 0x060028E5 RID: 10469 RVA: 0x000B8B66 File Offset: 0x000B6D66
		public DagTaskRemoveNodeNotUpException(string machineName, string clusterName, string machineState) : base(ReplayStrings.DagTaskRemoveNodeNotUpException(machineName, clusterName, machineState))
		{
			this.machineName = machineName;
			this.clusterName = clusterName;
			this.machineState = machineState;
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x000B8B90 File Offset: 0x000B6D90
		public DagTaskRemoveNodeNotUpException(string machineName, string clusterName, string machineState, Exception innerException) : base(ReplayStrings.DagTaskRemoveNodeNotUpException(machineName, clusterName, machineState), innerException)
		{
			this.machineName = machineName;
			this.clusterName = clusterName;
			this.machineState = machineState;
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x000B8BBC File Offset: 0x000B6DBC
		protected DagTaskRemoveNodeNotUpException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.machineName = (string)info.GetValue("machineName", typeof(string));
			this.clusterName = (string)info.GetValue("clusterName", typeof(string));
			this.machineState = (string)info.GetValue("machineState", typeof(string));
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x000B8C31 File Offset: 0x000B6E31
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("machineName", this.machineName);
			info.AddValue("clusterName", this.clusterName);
			info.AddValue("machineState", this.machineState);
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x060028E9 RID: 10473 RVA: 0x000B8C6E File Offset: 0x000B6E6E
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x060028EA RID: 10474 RVA: 0x000B8C76 File Offset: 0x000B6E76
		public string ClusterName
		{
			get
			{
				return this.clusterName;
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x060028EB RID: 10475 RVA: 0x000B8C7E File Offset: 0x000B6E7E
		public string MachineState
		{
			get
			{
				return this.machineState;
			}
		}

		// Token: 0x040013F8 RID: 5112
		private readonly string machineName;

		// Token: 0x040013F9 RID: 5113
		private readonly string clusterName;

		// Token: 0x040013FA RID: 5114
		private readonly string machineState;
	}
}
