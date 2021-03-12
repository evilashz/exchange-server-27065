using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001084 RID: 4228
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskNodeNotFoundTryConfigOnlyException : LocalizedException
	{
		// Token: 0x0600B180 RID: 45440 RVA: 0x00298331 File Offset: 0x00296531
		public DagTaskNodeNotFoundTryConfigOnlyException(string machineName, string clusterName) : base(Strings.DagTaskNodeNotFoundTryConfigOnlyException(machineName, clusterName))
		{
			this.machineName = machineName;
			this.clusterName = clusterName;
		}

		// Token: 0x0600B181 RID: 45441 RVA: 0x0029834E File Offset: 0x0029654E
		public DagTaskNodeNotFoundTryConfigOnlyException(string machineName, string clusterName, Exception innerException) : base(Strings.DagTaskNodeNotFoundTryConfigOnlyException(machineName, clusterName), innerException)
		{
			this.machineName = machineName;
			this.clusterName = clusterName;
		}

		// Token: 0x0600B182 RID: 45442 RVA: 0x0029836C File Offset: 0x0029656C
		protected DagTaskNodeNotFoundTryConfigOnlyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.machineName = (string)info.GetValue("machineName", typeof(string));
			this.clusterName = (string)info.GetValue("clusterName", typeof(string));
		}

		// Token: 0x0600B183 RID: 45443 RVA: 0x002983C1 File Offset: 0x002965C1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("machineName", this.machineName);
			info.AddValue("clusterName", this.clusterName);
		}

		// Token: 0x1700388D RID: 14477
		// (get) Token: 0x0600B184 RID: 45444 RVA: 0x002983ED File Offset: 0x002965ED
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		// Token: 0x1700388E RID: 14478
		// (get) Token: 0x0600B185 RID: 45445 RVA: 0x002983F5 File Offset: 0x002965F5
		public string ClusterName
		{
			get
			{
				return this.clusterName;
			}
		}

		// Token: 0x040061F3 RID: 25075
		private readonly string machineName;

		// Token: 0x040061F4 RID: 25076
		private readonly string clusterName;
	}
}
