using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003CA RID: 970
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FileCheckInvalidDatabaseStateException : FileCheckException
	{
		// Token: 0x06002856 RID: 10326 RVA: 0x000B7B0B File Offset: 0x000B5D0B
		public FileCheckInvalidDatabaseStateException(string database, string state) : base(ReplayStrings.FileCheckInvalidDatabaseState(database, state))
		{
			this.database = database;
			this.state = state;
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x000B7B2D File Offset: 0x000B5D2D
		public FileCheckInvalidDatabaseStateException(string database, string state, Exception innerException) : base(ReplayStrings.FileCheckInvalidDatabaseState(database, state), innerException)
		{
			this.database = database;
			this.state = state;
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x000B7B50 File Offset: 0x000B5D50
		protected FileCheckInvalidDatabaseStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.database = (string)info.GetValue("database", typeof(string));
			this.state = (string)info.GetValue("state", typeof(string));
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x000B7BA5 File Offset: 0x000B5DA5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("database", this.database);
			info.AddValue("state", this.state);
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x0600285A RID: 10330 RVA: 0x000B7BD1 File Offset: 0x000B5DD1
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x0600285B RID: 10331 RVA: 0x000B7BD9 File Offset: 0x000B5DD9
		public string State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x040013D5 RID: 5077
		private readonly string database;

		// Token: 0x040013D6 RID: 5078
		private readonly string state;
	}
}
