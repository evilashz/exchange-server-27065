using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000CA RID: 202
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OperationValidOnlyOnLonghornException : ClusCommonFailException
	{
		// Token: 0x06000726 RID: 1830 RVA: 0x0001BA22 File Offset: 0x00019C22
		public OperationValidOnlyOnLonghornException(string resName) : base(Strings.OperationValidOnlyOnLonghornException(resName))
		{
			this.resName = resName;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001BA3C File Offset: 0x00019C3C
		public OperationValidOnlyOnLonghornException(string resName, Exception innerException) : base(Strings.OperationValidOnlyOnLonghornException(resName), innerException)
		{
			this.resName = resName;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001BA57 File Offset: 0x00019C57
		protected OperationValidOnlyOnLonghornException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.resName = (string)info.GetValue("resName", typeof(string));
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001BA81 File Offset: 0x00019C81
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("resName", this.resName);
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0001BA9C File Offset: 0x00019C9C
		public string ResName
		{
			get
			{
				return this.resName;
			}
		}

		// Token: 0x0400071E RID: 1822
		private readonly string resName;
	}
}
