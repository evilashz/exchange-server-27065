using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EE7 RID: 3815
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AddDatabaseCopyNewCopyMustBeInDagException : LocalizedException
	{
		// Token: 0x0600A961 RID: 43361 RVA: 0x0028B8A5 File Offset: 0x00289AA5
		public AddDatabaseCopyNewCopyMustBeInDagException(string serverName, string databaseName) : base(Strings.AddDatabaseCopyNewCopyMustBeInDagException(serverName, databaseName))
		{
			this.serverName = serverName;
			this.databaseName = databaseName;
		}

		// Token: 0x0600A962 RID: 43362 RVA: 0x0028B8C2 File Offset: 0x00289AC2
		public AddDatabaseCopyNewCopyMustBeInDagException(string serverName, string databaseName, Exception innerException) : base(Strings.AddDatabaseCopyNewCopyMustBeInDagException(serverName, databaseName), innerException)
		{
			this.serverName = serverName;
			this.databaseName = databaseName;
		}

		// Token: 0x0600A963 RID: 43363 RVA: 0x0028B8E0 File Offset: 0x00289AE0
		protected AddDatabaseCopyNewCopyMustBeInDagException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.databaseName = (string)info.GetValue("databaseName", typeof(string));
		}

		// Token: 0x0600A964 RID: 43364 RVA: 0x0028B935 File Offset: 0x00289B35
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
			info.AddValue("databaseName", this.databaseName);
		}

		// Token: 0x170036E2 RID: 14050
		// (get) Token: 0x0600A965 RID: 43365 RVA: 0x0028B961 File Offset: 0x00289B61
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x170036E3 RID: 14051
		// (get) Token: 0x0600A966 RID: 43366 RVA: 0x0028B969 File Offset: 0x00289B69
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
		}

		// Token: 0x04006048 RID: 24648
		private readonly string serverName;

		// Token: 0x04006049 RID: 24649
		private readonly string databaseName;
	}
}
