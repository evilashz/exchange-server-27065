using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000D5 RID: 213
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClusResourceAlreadyExistsException : ClusCommonFailException
	{
		// Token: 0x06000760 RID: 1888 RVA: 0x0001C0BA File Offset: 0x0001A2BA
		public ClusResourceAlreadyExistsException(string resourceName) : base(Strings.ClusResourceAlreadyExistsException(resourceName))
		{
			this.resourceName = resourceName;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001C0D4 File Offset: 0x0001A2D4
		public ClusResourceAlreadyExistsException(string resourceName, Exception innerException) : base(Strings.ClusResourceAlreadyExistsException(resourceName), innerException)
		{
			this.resourceName = resourceName;
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001C0EF File Offset: 0x0001A2EF
		protected ClusResourceAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.resourceName = (string)info.GetValue("resourceName", typeof(string));
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001C119 File Offset: 0x0001A319
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("resourceName", this.resourceName);
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x0001C134 File Offset: 0x0001A334
		public string ResourceName
		{
			get
			{
				return this.resourceName;
			}
		}

		// Token: 0x0400072C RID: 1836
		private readonly string resourceName;
	}
}
