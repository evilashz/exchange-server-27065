using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001087 RID: 4231
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RemoveDagServerDatabaseIsReplicatedException : LocalizedException
	{
		// Token: 0x0600B193 RID: 45459 RVA: 0x002985E5 File Offset: 0x002967E5
		public RemoveDagServerDatabaseIsReplicatedException(string mailboxServer, string databaseName) : base(Strings.RemoveDagServerDatabaseIsReplicatedException(mailboxServer, databaseName))
		{
			this.mailboxServer = mailboxServer;
			this.databaseName = databaseName;
		}

		// Token: 0x0600B194 RID: 45460 RVA: 0x00298602 File Offset: 0x00296802
		public RemoveDagServerDatabaseIsReplicatedException(string mailboxServer, string databaseName, Exception innerException) : base(Strings.RemoveDagServerDatabaseIsReplicatedException(mailboxServer, databaseName), innerException)
		{
			this.mailboxServer = mailboxServer;
			this.databaseName = databaseName;
		}

		// Token: 0x0600B195 RID: 45461 RVA: 0x00298620 File Offset: 0x00296820
		protected RemoveDagServerDatabaseIsReplicatedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailboxServer = (string)info.GetValue("mailboxServer", typeof(string));
			this.databaseName = (string)info.GetValue("databaseName", typeof(string));
		}

		// Token: 0x0600B196 RID: 45462 RVA: 0x00298675 File Offset: 0x00296875
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailboxServer", this.mailboxServer);
			info.AddValue("databaseName", this.databaseName);
		}

		// Token: 0x17003894 RID: 14484
		// (get) Token: 0x0600B197 RID: 45463 RVA: 0x002986A1 File Offset: 0x002968A1
		public string MailboxServer
		{
			get
			{
				return this.mailboxServer;
			}
		}

		// Token: 0x17003895 RID: 14485
		// (get) Token: 0x0600B198 RID: 45464 RVA: 0x002986A9 File Offset: 0x002968A9
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
		}

		// Token: 0x040061FA RID: 25082
		private readonly string mailboxServer;

		// Token: 0x040061FB RID: 25083
		private readonly string databaseName;
	}
}
