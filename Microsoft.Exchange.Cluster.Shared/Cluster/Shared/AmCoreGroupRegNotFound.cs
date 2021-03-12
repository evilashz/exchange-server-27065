using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000C7 RID: 199
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmCoreGroupRegNotFound : ClusterException
	{
		// Token: 0x06000717 RID: 1815 RVA: 0x0001B89C File Offset: 0x00019A9C
		public AmCoreGroupRegNotFound(string regvalueName) : base(Strings.AmCoreGroupRegNotFound(regvalueName))
		{
			this.regvalueName = regvalueName;
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0001B8B6 File Offset: 0x00019AB6
		public AmCoreGroupRegNotFound(string regvalueName, Exception innerException) : base(Strings.AmCoreGroupRegNotFound(regvalueName), innerException)
		{
			this.regvalueName = regvalueName;
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001B8D1 File Offset: 0x00019AD1
		protected AmCoreGroupRegNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.regvalueName = (string)info.GetValue("regvalueName", typeof(string));
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001B8FB File Offset: 0x00019AFB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("regvalueName", this.regvalueName);
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0001B916 File Offset: 0x00019B16
		public string RegvalueName
		{
			get
			{
				return this.regvalueName;
			}
		}

		// Token: 0x0400071B RID: 1819
		private readonly string regvalueName;
	}
}
