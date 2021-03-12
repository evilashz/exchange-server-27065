using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000F4 RID: 244
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ErrorGettingDatabasesException : MigrationTransientException
	{
		// Token: 0x06000775 RID: 1909 RVA: 0x00010239 File Offset: 0x0000E439
		public ErrorGettingDatabasesException(string server) : base(UpgradeHandlerStrings.ErrorGettingDatabases(server))
		{
			this.server = server;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0001024E File Offset: 0x0000E44E
		public ErrorGettingDatabasesException(string server, Exception innerException) : base(UpgradeHandlerStrings.ErrorGettingDatabases(server), innerException)
		{
			this.server = server;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00010264 File Offset: 0x0000E464
		protected ErrorGettingDatabasesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.server = (string)info.GetValue("server", typeof(string));
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001028E File Offset: 0x0000E48E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("server", this.server);
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x000102A9 File Offset: 0x0000E4A9
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x040003A5 RID: 933
		private readonly string server;
	}
}
