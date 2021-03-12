using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000D8 RID: 216
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDatabaseADException : AmServerException
	{
		// Token: 0x060012B4 RID: 4788 RVA: 0x00068151 File Offset: 0x00066351
		public AmDatabaseADException(string dbName, string error) : base(ServerStrings.AmDatabaseADException(dbName, error))
		{
			this.dbName = dbName;
			this.error = error;
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x00068173 File Offset: 0x00066373
		public AmDatabaseADException(string dbName, string error, Exception innerException) : base(ServerStrings.AmDatabaseADException(dbName, error), innerException)
		{
			this.dbName = dbName;
			this.error = error;
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00068198 File Offset: 0x00066398
		protected AmDatabaseADException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x000681ED File Offset: 0x000663ED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x00068219 File Offset: 0x00066419
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x00068221 File Offset: 0x00066421
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04000966 RID: 2406
		private readonly string dbName;

		// Token: 0x04000967 RID: 2407
		private readonly string error;
	}
}
