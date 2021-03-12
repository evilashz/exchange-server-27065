using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EF0 RID: 3824
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatabaseNotMovedInServerModeException : LocalizedException
	{
		// Token: 0x0600A99A RID: 43418 RVA: 0x0028C0CD File Offset: 0x0028A2CD
		public DatabaseNotMovedInServerModeException(string database, string sourceServer) : base(Strings.DatabaseNotMovedInServerModeException(database, sourceServer))
		{
			this.database = database;
			this.sourceServer = sourceServer;
		}

		// Token: 0x0600A99B RID: 43419 RVA: 0x0028C0EA File Offset: 0x0028A2EA
		public DatabaseNotMovedInServerModeException(string database, string sourceServer, Exception innerException) : base(Strings.DatabaseNotMovedInServerModeException(database, sourceServer), innerException)
		{
			this.database = database;
			this.sourceServer = sourceServer;
		}

		// Token: 0x0600A99C RID: 43420 RVA: 0x0028C108 File Offset: 0x0028A308
		protected DatabaseNotMovedInServerModeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.database = (string)info.GetValue("database", typeof(string));
			this.sourceServer = (string)info.GetValue("sourceServer", typeof(string));
		}

		// Token: 0x0600A99D RID: 43421 RVA: 0x0028C15D File Offset: 0x0028A35D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("database", this.database);
			info.AddValue("sourceServer", this.sourceServer);
		}

		// Token: 0x170036F7 RID: 14071
		// (get) Token: 0x0600A99E RID: 43422 RVA: 0x0028C189 File Offset: 0x0028A389
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x170036F8 RID: 14072
		// (get) Token: 0x0600A99F RID: 43423 RVA: 0x0028C191 File Offset: 0x0028A391
		public string SourceServer
		{
			get
			{
				return this.sourceServer;
			}
		}

		// Token: 0x0400605D RID: 24669
		private readonly string database;

		// Token: 0x0400605E RID: 24670
		private readonly string sourceServer;
	}
}
