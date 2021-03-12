using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004F0 RID: 1264
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AutoReseedNoCopiesException : AutoReseedException
	{
		// Token: 0x06002E9E RID: 11934 RVA: 0x000C3B2A File Offset: 0x000C1D2A
		public AutoReseedNoCopiesException(string databaseName) : base(ReplayStrings.AutoReseedNoCopiesException(databaseName))
		{
			this.databaseName = databaseName;
		}

		// Token: 0x06002E9F RID: 11935 RVA: 0x000C3B44 File Offset: 0x000C1D44
		public AutoReseedNoCopiesException(string databaseName, Exception innerException) : base(ReplayStrings.AutoReseedNoCopiesException(databaseName), innerException)
		{
			this.databaseName = databaseName;
		}

		// Token: 0x06002EA0 RID: 11936 RVA: 0x000C3B5F File Offset: 0x000C1D5F
		protected AutoReseedNoCopiesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseName = (string)info.GetValue("databaseName", typeof(string));
		}

		// Token: 0x06002EA1 RID: 11937 RVA: 0x000C3B89 File Offset: 0x000C1D89
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseName", this.databaseName);
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06002EA2 RID: 11938 RVA: 0x000C3BA4 File Offset: 0x000C1DA4
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
		}

		// Token: 0x04001585 RID: 5509
		private readonly string databaseName;
	}
}
