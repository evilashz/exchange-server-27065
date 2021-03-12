using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000B9 RID: 185
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DxStoreKeyInvalidKeyException : ClusterApiException
	{
		// Token: 0x060006CF RID: 1743 RVA: 0x0001B0F3 File Offset: 0x000192F3
		public DxStoreKeyInvalidKeyException(string keyName) : base(Strings.DxStoreKeyInvalidKeyException(keyName))
		{
			this.keyName = keyName;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001B10D File Offset: 0x0001930D
		public DxStoreKeyInvalidKeyException(string keyName, Exception innerException) : base(Strings.DxStoreKeyInvalidKeyException(keyName), innerException)
		{
			this.keyName = keyName;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001B128 File Offset: 0x00019328
		protected DxStoreKeyInvalidKeyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.keyName = (string)info.GetValue("keyName", typeof(string));
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001B152 File Offset: 0x00019352
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("keyName", this.keyName);
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x0001B16D File Offset: 0x0001936D
		public string KeyName
		{
			get
			{
				return this.keyName;
			}
		}

		// Token: 0x0400070B RID: 1803
		private readonly string keyName;
	}
}
