using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200050B RID: 1291
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatabasesMissingInCopyStatusLookUpTable : DatabaseVolumeInfoException
	{
		// Token: 0x06002F48 RID: 12104 RVA: 0x000C541D File Offset: 0x000C361D
		public DatabasesMissingInCopyStatusLookUpTable(string databaseNames) : base(ReplayStrings.DatabasesMissingInCopyStatusLookUpTable(databaseNames))
		{
			this.databaseNames = databaseNames;
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x000C5437 File Offset: 0x000C3637
		public DatabasesMissingInCopyStatusLookUpTable(string databaseNames, Exception innerException) : base(ReplayStrings.DatabasesMissingInCopyStatusLookUpTable(databaseNames), innerException)
		{
			this.databaseNames = databaseNames;
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x000C5452 File Offset: 0x000C3652
		protected DatabasesMissingInCopyStatusLookUpTable(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseNames = (string)info.GetValue("databaseNames", typeof(string));
		}

		// Token: 0x06002F4B RID: 12107 RVA: 0x000C547C File Offset: 0x000C367C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseNames", this.databaseNames);
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06002F4C RID: 12108 RVA: 0x000C5497 File Offset: 0x000C3697
		public string DatabaseNames
		{
			get
			{
				return this.databaseNames;
			}
		}

		// Token: 0x040015C3 RID: 5571
		private readonly string databaseNames;
	}
}
