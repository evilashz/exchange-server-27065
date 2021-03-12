using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000F2A RID: 3882
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToReadDatabaseSchemaVersionInformationForServerException : LocalizedException
	{
		// Token: 0x0600AAC7 RID: 43719 RVA: 0x0028E005 File Offset: 0x0028C205
		public FailedToReadDatabaseSchemaVersionInformationForServerException(string serverName) : base(Strings.FailedToReadDatabaseSchemaVersionInformationForServer(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x0600AAC8 RID: 43720 RVA: 0x0028E01A File Offset: 0x0028C21A
		public FailedToReadDatabaseSchemaVersionInformationForServerException(string serverName, Exception innerException) : base(Strings.FailedToReadDatabaseSchemaVersionInformationForServer(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600AAC9 RID: 43721 RVA: 0x0028E030 File Offset: 0x0028C230
		protected FailedToReadDatabaseSchemaVersionInformationForServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600AACA RID: 43722 RVA: 0x0028E05A File Offset: 0x0028C25A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x1700373C RID: 14140
		// (get) Token: 0x0600AACB RID: 43723 RVA: 0x0028E075 File Offset: 0x0028C275
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x040060A2 RID: 24738
		private readonly string serverName;
	}
}
