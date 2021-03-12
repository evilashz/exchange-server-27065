using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000D0 RID: 208
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClusterDatabaseTransientException : ClusCommonTransientException
	{
		// Token: 0x06000742 RID: 1858 RVA: 0x0001BC9C File Offset: 0x00019E9C
		public ClusterDatabaseTransientException(string msg) : base(Strings.ClusterDatabaseTransientException(msg))
		{
			this.msg = msg;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001BCB6 File Offset: 0x00019EB6
		public ClusterDatabaseTransientException(string msg, Exception innerException) : base(Strings.ClusterDatabaseTransientException(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001BCD1 File Offset: 0x00019ED1
		protected ClusterDatabaseTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001BCFB File Offset: 0x00019EFB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x0001BD16 File Offset: 0x00019F16
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x04000722 RID: 1826
		private readonly string msg;
	}
}
