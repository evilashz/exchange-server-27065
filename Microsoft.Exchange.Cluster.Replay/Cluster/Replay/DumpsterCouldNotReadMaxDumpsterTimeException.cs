using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200051E RID: 1310
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DumpsterCouldNotReadMaxDumpsterTimeException : DumpsterRedeliveryException
	{
		// Token: 0x06002FAD RID: 12205 RVA: 0x000C5FB9 File Offset: 0x000C41B9
		public DumpsterCouldNotReadMaxDumpsterTimeException(string dbName) : base(ReplayStrings.DumpsterCouldNotReadMaxDumpsterTimeException(dbName))
		{
			this.dbName = dbName;
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x000C5FD3 File Offset: 0x000C41D3
		public DumpsterCouldNotReadMaxDumpsterTimeException(string dbName, Exception innerException) : base(ReplayStrings.DumpsterCouldNotReadMaxDumpsterTimeException(dbName), innerException)
		{
			this.dbName = dbName;
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x000C5FEE File Offset: 0x000C41EE
		protected DumpsterCouldNotReadMaxDumpsterTimeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x000C6018 File Offset: 0x000C4218
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06002FB1 RID: 12209 RVA: 0x000C6033 File Offset: 0x000C4233
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x040015DC RID: 5596
		private readonly string dbName;
	}
}
