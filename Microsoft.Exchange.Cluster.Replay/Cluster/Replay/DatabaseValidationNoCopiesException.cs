using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004EB RID: 1259
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatabaseValidationNoCopiesException : DatabaseValidationException
	{
		// Token: 0x06002E86 RID: 11910 RVA: 0x000C38FD File Offset: 0x000C1AFD
		public DatabaseValidationNoCopiesException(string databaseName) : base(ReplayStrings.DatabaseValidationNoCopiesException(databaseName))
		{
			this.databaseName = databaseName;
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x000C3917 File Offset: 0x000C1B17
		public DatabaseValidationNoCopiesException(string databaseName, Exception innerException) : base(ReplayStrings.DatabaseValidationNoCopiesException(databaseName), innerException)
		{
			this.databaseName = databaseName;
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x000C3932 File Offset: 0x000C1B32
		protected DatabaseValidationNoCopiesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseName = (string)info.GetValue("databaseName", typeof(string));
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x000C395C File Offset: 0x000C1B5C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseName", this.databaseName);
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06002E8A RID: 11914 RVA: 0x000C3977 File Offset: 0x000C1B77
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
		}

		// Token: 0x04001581 RID: 5505
		private readonly string databaseName;
	}
}
