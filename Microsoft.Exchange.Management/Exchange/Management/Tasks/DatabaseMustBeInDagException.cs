using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001067 RID: 4199
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatabaseMustBeInDagException : LocalizedException
	{
		// Token: 0x0600B0DA RID: 45274 RVA: 0x00296EDD File Offset: 0x002950DD
		public DatabaseMustBeInDagException(string dbName) : base(Strings.DatabaseMustBeInDagException(dbName))
		{
			this.dbName = dbName;
		}

		// Token: 0x0600B0DB RID: 45275 RVA: 0x00296EF2 File Offset: 0x002950F2
		public DatabaseMustBeInDagException(string dbName, Exception innerException) : base(Strings.DatabaseMustBeInDagException(dbName), innerException)
		{
			this.dbName = dbName;
		}

		// Token: 0x0600B0DC RID: 45276 RVA: 0x00296F08 File Offset: 0x00295108
		protected DatabaseMustBeInDagException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
		}

		// Token: 0x0600B0DD RID: 45277 RVA: 0x00296F32 File Offset: 0x00295132
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
		}

		// Token: 0x1700385B RID: 14427
		// (get) Token: 0x0600B0DE RID: 45278 RVA: 0x00296F4D File Offset: 0x0029514D
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x040061C1 RID: 25025
		private readonly string dbName;
	}
}
