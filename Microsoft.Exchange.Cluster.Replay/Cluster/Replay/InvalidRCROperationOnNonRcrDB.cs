using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200048B RID: 1163
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidRCROperationOnNonRcrDB : LocalizedException
	{
		// Token: 0x06002C63 RID: 11363 RVA: 0x000BF486 File Offset: 0x000BD686
		public InvalidRCROperationOnNonRcrDB(string dbName) : base(ReplayStrings.InvalidRCROperationOnNonRcrDB(dbName))
		{
			this.dbName = dbName;
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x000BF49B File Offset: 0x000BD69B
		public InvalidRCROperationOnNonRcrDB(string dbName, Exception innerException) : base(ReplayStrings.InvalidRCROperationOnNonRcrDB(dbName), innerException)
		{
			this.dbName = dbName;
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x000BF4B1 File Offset: 0x000BD6B1
		protected InvalidRCROperationOnNonRcrDB(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x000BF4DB File Offset: 0x000BD6DB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06002C67 RID: 11367 RVA: 0x000BF4F6 File Offset: 0x000BD6F6
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x040014DE RID: 5342
		private readonly string dbName;
	}
}
