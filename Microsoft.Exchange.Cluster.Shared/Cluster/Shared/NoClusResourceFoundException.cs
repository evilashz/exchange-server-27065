using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000D2 RID: 210
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoClusResourceFoundException : ClusCommonFailException
	{
		// Token: 0x0600074C RID: 1868 RVA: 0x0001BDA0 File Offset: 0x00019FA0
		public NoClusResourceFoundException(string groupName, string resourceName) : base(Strings.NoClusResourceFoundException(groupName, resourceName))
		{
			this.groupName = groupName;
			this.resourceName = resourceName;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0001BDC2 File Offset: 0x00019FC2
		public NoClusResourceFoundException(string groupName, string resourceName, Exception innerException) : base(Strings.NoClusResourceFoundException(groupName, resourceName), innerException)
		{
			this.groupName = groupName;
			this.resourceName = resourceName;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001BDE8 File Offset: 0x00019FE8
		protected NoClusResourceFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.groupName = (string)info.GetValue("groupName", typeof(string));
			this.resourceName = (string)info.GetValue("resourceName", typeof(string));
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001BE3D File Offset: 0x0001A03D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("groupName", this.groupName);
			info.AddValue("resourceName", this.resourceName);
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x0001BE69 File Offset: 0x0001A069
		public string GroupName
		{
			get
			{
				return this.groupName;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x0001BE71 File Offset: 0x0001A071
		public string ResourceName
		{
			get
			{
				return this.resourceName;
			}
		}

		// Token: 0x04000724 RID: 1828
		private readonly string groupName;

		// Token: 0x04000725 RID: 1829
		private readonly string resourceName;
	}
}
