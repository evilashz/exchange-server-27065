using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000C2 RID: 194
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClusterEvictWithoutCleanupException : ClusterException
	{
		// Token: 0x060006FC RID: 1788 RVA: 0x0001B572 File Offset: 0x00019772
		public ClusterEvictWithoutCleanupException(string nodeName) : base(Strings.ClusterEvictWithoutCleanupException(nodeName))
		{
			this.nodeName = nodeName;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001B58C File Offset: 0x0001978C
		public ClusterEvictWithoutCleanupException(string nodeName, Exception innerException) : base(Strings.ClusterEvictWithoutCleanupException(nodeName), innerException)
		{
			this.nodeName = nodeName;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001B5A7 File Offset: 0x000197A7
		protected ClusterEvictWithoutCleanupException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001B5D1 File Offset: 0x000197D1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("nodeName", this.nodeName);
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x0001B5EC File Offset: 0x000197EC
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x04000714 RID: 1812
		private readonly string nodeName;
	}
}
