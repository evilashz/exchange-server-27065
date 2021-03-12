using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000462 RID: 1122
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmClusterNoServerToConnectException : ClusterException
	{
		// Token: 0x06002B7F RID: 11135 RVA: 0x000BD802 File Offset: 0x000BBA02
		public AmClusterNoServerToConnectException(string dagName) : base(ReplayStrings.AmClusterNoServerToConnect(dagName))
		{
			this.dagName = dagName;
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x000BD81C File Offset: 0x000BBA1C
		public AmClusterNoServerToConnectException(string dagName, Exception innerException) : base(ReplayStrings.AmClusterNoServerToConnect(dagName), innerException)
		{
			this.dagName = dagName;
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x000BD837 File Offset: 0x000BBA37
		protected AmClusterNoServerToConnectException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dagName = (string)info.GetValue("dagName", typeof(string));
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x000BD861 File Offset: 0x000BBA61
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dagName", this.dagName);
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06002B83 RID: 11139 RVA: 0x000BD87C File Offset: 0x000BBA7C
		public string DagName
		{
			get
			{
				return this.dagName;
			}
		}

		// Token: 0x0400149E RID: 5278
		private readonly string dagName;
	}
}
