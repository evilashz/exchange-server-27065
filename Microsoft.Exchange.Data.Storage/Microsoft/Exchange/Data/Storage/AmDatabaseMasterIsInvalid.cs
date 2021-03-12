using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000D9 RID: 217
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDatabaseMasterIsInvalid : AmServerException
	{
		// Token: 0x060012BA RID: 4794 RVA: 0x00068229 File Offset: 0x00066429
		public AmDatabaseMasterIsInvalid(string dbName) : base(ServerStrings.AmDatabaseMasterIsInvalid(dbName))
		{
			this.dbName = dbName;
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00068243 File Offset: 0x00066443
		public AmDatabaseMasterIsInvalid(string dbName, Exception innerException) : base(ServerStrings.AmDatabaseMasterIsInvalid(dbName), innerException)
		{
			this.dbName = dbName;
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0006825E File Offset: 0x0006645E
		protected AmDatabaseMasterIsInvalid(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x00068288 File Offset: 0x00066488
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x000682A3 File Offset: 0x000664A3
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x04000968 RID: 2408
		private readonly string dbName;
	}
}
