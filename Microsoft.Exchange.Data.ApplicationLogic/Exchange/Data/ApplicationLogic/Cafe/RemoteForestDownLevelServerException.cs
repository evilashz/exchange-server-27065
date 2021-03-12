using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data.ApplicationLogic.Cafe
{
	// Token: 0x020000BE RID: 190
	[Serializable]
	internal class RemoteForestDownLevelServerException : MailboxServerLocatorException
	{
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x0002196B File Offset: 0x0001FB6B
		// (set) Token: 0x0600082F RID: 2095 RVA: 0x00021973 File Offset: 0x0001FB73
		public string ResourceForest { get; private set; }

		// Token: 0x06000830 RID: 2096 RVA: 0x0002197C File Offset: 0x0001FB7C
		public RemoteForestDownLevelServerException(string databaseId, string resourceForest) : base(databaseId)
		{
			this.ResourceForest = resourceForest;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0002198C File Offset: 0x0001FB8C
		public RemoteForestDownLevelServerException(string databaseId, string resourceForest, Exception innerException) : base(databaseId, innerException)
		{
			this.ResourceForest = resourceForest;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0002199D File Offset: 0x0001FB9D
		protected RemoteForestDownLevelServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
