using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001076 RID: 4214
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RemoveDagFailedToDestroyClusterException : LocalizedException
	{
		// Token: 0x0600B131 RID: 45361 RVA: 0x002979BD File Offset: 0x00295BBD
		public RemoveDagFailedToDestroyClusterException(string clusterName, string dagName, uint status) : base(Strings.RemoveDagFailedToDestroyClusterException(clusterName, dagName, status))
		{
			this.clusterName = clusterName;
			this.dagName = dagName;
			this.status = status;
		}

		// Token: 0x0600B132 RID: 45362 RVA: 0x002979E2 File Offset: 0x00295BE2
		public RemoveDagFailedToDestroyClusterException(string clusterName, string dagName, uint status, Exception innerException) : base(Strings.RemoveDagFailedToDestroyClusterException(clusterName, dagName, status), innerException)
		{
			this.clusterName = clusterName;
			this.dagName = dagName;
			this.status = status;
		}

		// Token: 0x0600B133 RID: 45363 RVA: 0x00297A0C File Offset: 0x00295C0C
		protected RemoveDagFailedToDestroyClusterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.clusterName = (string)info.GetValue("clusterName", typeof(string));
			this.dagName = (string)info.GetValue("dagName", typeof(string));
			this.status = (uint)info.GetValue("status", typeof(uint));
		}

		// Token: 0x0600B134 RID: 45364 RVA: 0x00297A81 File Offset: 0x00295C81
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("clusterName", this.clusterName);
			info.AddValue("dagName", this.dagName);
			info.AddValue("status", this.status);
		}

		// Token: 0x17003876 RID: 14454
		// (get) Token: 0x0600B135 RID: 45365 RVA: 0x00297ABE File Offset: 0x00295CBE
		public string ClusterName
		{
			get
			{
				return this.clusterName;
			}
		}

		// Token: 0x17003877 RID: 14455
		// (get) Token: 0x0600B136 RID: 45366 RVA: 0x00297AC6 File Offset: 0x00295CC6
		public string DagName
		{
			get
			{
				return this.dagName;
			}
		}

		// Token: 0x17003878 RID: 14456
		// (get) Token: 0x0600B137 RID: 45367 RVA: 0x00297ACE File Offset: 0x00295CCE
		public uint Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x040061DC RID: 25052
		private readonly string clusterName;

		// Token: 0x040061DD RID: 25053
		private readonly string dagName;

		// Token: 0x040061DE RID: 25054
		private readonly uint status;
	}
}
