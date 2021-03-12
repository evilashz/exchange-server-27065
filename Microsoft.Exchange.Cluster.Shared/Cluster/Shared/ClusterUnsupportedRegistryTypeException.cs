using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000C4 RID: 196
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClusterUnsupportedRegistryTypeException : ClusterException
	{
		// Token: 0x06000706 RID: 1798 RVA: 0x0001B676 File Offset: 0x00019876
		public ClusterUnsupportedRegistryTypeException(string typeName) : base(Strings.ClusterUnsupportedRegistryTypeException(typeName))
		{
			this.typeName = typeName;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001B690 File Offset: 0x00019890
		public ClusterUnsupportedRegistryTypeException(string typeName, Exception innerException) : base(Strings.ClusterUnsupportedRegistryTypeException(typeName), innerException)
		{
			this.typeName = typeName;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001B6AB File Offset: 0x000198AB
		protected ClusterUnsupportedRegistryTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.typeName = (string)info.GetValue("typeName", typeof(string));
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001B6D5 File Offset: 0x000198D5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("typeName", this.typeName);
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x0001B6F0 File Offset: 0x000198F0
		public string TypeName
		{
			get
			{
				return this.typeName;
			}
		}

		// Token: 0x04000716 RID: 1814
		private readonly string typeName;
	}
}
