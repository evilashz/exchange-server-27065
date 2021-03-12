using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000C6 RID: 198
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClusterNoServerToConnectException : ClusterException
	{
		// Token: 0x06000712 RID: 1810 RVA: 0x0001B81A File Offset: 0x00019A1A
		public ClusterNoServerToConnectException(string dagName) : base(Strings.ClusterNoServerToConnect(dagName))
		{
			this.dagName = dagName;
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001B834 File Offset: 0x00019A34
		public ClusterNoServerToConnectException(string dagName, Exception innerException) : base(Strings.ClusterNoServerToConnect(dagName), innerException)
		{
			this.dagName = dagName;
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0001B84F File Offset: 0x00019A4F
		protected ClusterNoServerToConnectException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dagName = (string)info.GetValue("dagName", typeof(string));
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0001B879 File Offset: 0x00019A79
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dagName", this.dagName);
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0001B894 File Offset: 0x00019A94
		public string DagName
		{
			get
			{
				return this.dagName;
			}
		}

		// Token: 0x0400071A RID: 1818
		private readonly string dagName;
	}
}
