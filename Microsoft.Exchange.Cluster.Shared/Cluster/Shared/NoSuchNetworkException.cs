using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000E0 RID: 224
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoSuchNetworkException : ClusCommonValidationFailedException
	{
		// Token: 0x06000799 RID: 1945 RVA: 0x0001C6DA File Offset: 0x0001A8DA
		public NoSuchNetworkException(string netName) : base(Strings.NoSuchNetwork(netName))
		{
			this.netName = netName;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0001C6F4 File Offset: 0x0001A8F4
		public NoSuchNetworkException(string netName, Exception innerException) : base(Strings.NoSuchNetwork(netName), innerException)
		{
			this.netName = netName;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0001C70F File Offset: 0x0001A90F
		protected NoSuchNetworkException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.netName = (string)info.GetValue("netName", typeof(string));
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0001C739 File Offset: 0x0001A939
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("netName", this.netName);
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x0001C754 File Offset: 0x0001A954
		public string NetName
		{
			get
			{
				return this.netName;
			}
		}

		// Token: 0x04000739 RID: 1849
		private readonly string netName;
	}
}
