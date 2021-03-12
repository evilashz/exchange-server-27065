using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001085 RID: 4229
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskNodeNotUpExceptionTryConfigOnlyException : LocalizedException
	{
		// Token: 0x0600B186 RID: 45446 RVA: 0x002983FD File Offset: 0x002965FD
		public DagTaskNodeNotUpExceptionTryConfigOnlyException(string machineName, string clusterName, string machineState) : base(Strings.DagTaskNodeNotUpExceptionTryConfigOnlyException(machineName, clusterName, machineState))
		{
			this.machineName = machineName;
			this.clusterName = clusterName;
			this.machineState = machineState;
		}

		// Token: 0x0600B187 RID: 45447 RVA: 0x00298422 File Offset: 0x00296622
		public DagTaskNodeNotUpExceptionTryConfigOnlyException(string machineName, string clusterName, string machineState, Exception innerException) : base(Strings.DagTaskNodeNotUpExceptionTryConfigOnlyException(machineName, clusterName, machineState), innerException)
		{
			this.machineName = machineName;
			this.clusterName = clusterName;
			this.machineState = machineState;
		}

		// Token: 0x0600B188 RID: 45448 RVA: 0x0029844C File Offset: 0x0029664C
		protected DagTaskNodeNotUpExceptionTryConfigOnlyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.machineName = (string)info.GetValue("machineName", typeof(string));
			this.clusterName = (string)info.GetValue("clusterName", typeof(string));
			this.machineState = (string)info.GetValue("machineState", typeof(string));
		}

		// Token: 0x0600B189 RID: 45449 RVA: 0x002984C1 File Offset: 0x002966C1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("machineName", this.machineName);
			info.AddValue("clusterName", this.clusterName);
			info.AddValue("machineState", this.machineState);
		}

		// Token: 0x1700388F RID: 14479
		// (get) Token: 0x0600B18A RID: 45450 RVA: 0x002984FE File Offset: 0x002966FE
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		// Token: 0x17003890 RID: 14480
		// (get) Token: 0x0600B18B RID: 45451 RVA: 0x00298506 File Offset: 0x00296706
		public string ClusterName
		{
			get
			{
				return this.clusterName;
			}
		}

		// Token: 0x17003891 RID: 14481
		// (get) Token: 0x0600B18C RID: 45452 RVA: 0x0029850E File Offset: 0x0029670E
		public string MachineState
		{
			get
			{
				return this.machineState;
			}
		}

		// Token: 0x040061F5 RID: 25077
		private readonly string machineName;

		// Token: 0x040061F6 RID: 25078
		private readonly string clusterName;

		// Token: 0x040061F7 RID: 25079
		private readonly string machineState;
	}
}
