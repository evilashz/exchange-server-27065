using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EE9 RID: 3817
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AddDatabaseCopyAllCopiesMustBeInTheDagException : LocalizedException
	{
		// Token: 0x0600A970 RID: 43376 RVA: 0x0028BB32 File Offset: 0x00289D32
		public AddDatabaseCopyAllCopiesMustBeInTheDagException(string databaseName, string server1, string dag1, string server2) : base(Strings.AddDatabaseCopyAllCopiesMustBeInTheDagException(databaseName, server1, dag1, server2))
		{
			this.databaseName = databaseName;
			this.server1 = server1;
			this.dag1 = dag1;
			this.server2 = server2;
		}

		// Token: 0x0600A971 RID: 43377 RVA: 0x0028BB61 File Offset: 0x00289D61
		public AddDatabaseCopyAllCopiesMustBeInTheDagException(string databaseName, string server1, string dag1, string server2, Exception innerException) : base(Strings.AddDatabaseCopyAllCopiesMustBeInTheDagException(databaseName, server1, dag1, server2), innerException)
		{
			this.databaseName = databaseName;
			this.server1 = server1;
			this.dag1 = dag1;
			this.server2 = server2;
		}

		// Token: 0x0600A972 RID: 43378 RVA: 0x0028BB94 File Offset: 0x00289D94
		protected AddDatabaseCopyAllCopiesMustBeInTheDagException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseName = (string)info.GetValue("databaseName", typeof(string));
			this.server1 = (string)info.GetValue("server1", typeof(string));
			this.dag1 = (string)info.GetValue("dag1", typeof(string));
			this.server2 = (string)info.GetValue("server2", typeof(string));
		}

		// Token: 0x0600A973 RID: 43379 RVA: 0x0028BC2C File Offset: 0x00289E2C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseName", this.databaseName);
			info.AddValue("server1", this.server1);
			info.AddValue("dag1", this.dag1);
			info.AddValue("server2", this.server2);
		}

		// Token: 0x170036E9 RID: 14057
		// (get) Token: 0x0600A974 RID: 43380 RVA: 0x0028BC85 File Offset: 0x00289E85
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
		}

		// Token: 0x170036EA RID: 14058
		// (get) Token: 0x0600A975 RID: 43381 RVA: 0x0028BC8D File Offset: 0x00289E8D
		public string Server1
		{
			get
			{
				return this.server1;
			}
		}

		// Token: 0x170036EB RID: 14059
		// (get) Token: 0x0600A976 RID: 43382 RVA: 0x0028BC95 File Offset: 0x00289E95
		public string Dag1
		{
			get
			{
				return this.dag1;
			}
		}

		// Token: 0x170036EC RID: 14060
		// (get) Token: 0x0600A977 RID: 43383 RVA: 0x0028BC9D File Offset: 0x00289E9D
		public string Server2
		{
			get
			{
				return this.server2;
			}
		}

		// Token: 0x0400604F RID: 24655
		private readonly string databaseName;

		// Token: 0x04006050 RID: 24656
		private readonly string server1;

		// Token: 0x04006051 RID: 24657
		private readonly string dag1;

		// Token: 0x04006052 RID: 24658
		private readonly string server2;
	}
}
