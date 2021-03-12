using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000E0 RID: 224
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbNotMountedNoViableServersException : AmServerException
	{
		// Token: 0x060012DC RID: 4828 RVA: 0x00068589 File Offset: 0x00066789
		public AmDbNotMountedNoViableServersException(string dbName) : base(ServerStrings.AmDbNotMountedNoViableServersException(dbName))
		{
			this.dbName = dbName;
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x000685A3 File Offset: 0x000667A3
		public AmDbNotMountedNoViableServersException(string dbName, Exception innerException) : base(ServerStrings.AmDbNotMountedNoViableServersException(dbName), innerException)
		{
			this.dbName = dbName;
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x000685BE File Offset: 0x000667BE
		protected AmDbNotMountedNoViableServersException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x000685E8 File Offset: 0x000667E8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060012E0 RID: 4832 RVA: 0x00068603 File Offset: 0x00066803
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x0400096E RID: 2414
		private readonly string dbName;
	}
}
