using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000300 RID: 768
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatabaseCouldNotBeMappedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060024B9 RID: 9401 RVA: 0x00050666 File Offset: 0x0004E866
		public DatabaseCouldNotBeMappedPermanentException(string databaseName) : base(MrsStrings.DatabaseCouldNotBeMapped(databaseName))
		{
			this.databaseName = databaseName;
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x0005067B File Offset: 0x0004E87B
		public DatabaseCouldNotBeMappedPermanentException(string databaseName, Exception innerException) : base(MrsStrings.DatabaseCouldNotBeMapped(databaseName), innerException)
		{
			this.databaseName = databaseName;
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x00050691 File Offset: 0x0004E891
		protected DatabaseCouldNotBeMappedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseName = (string)info.GetValue("databaseName", typeof(string));
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x000506BB File Offset: 0x0004E8BB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseName", this.databaseName);
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x060024BD RID: 9405 RVA: 0x000506D6 File Offset: 0x0004E8D6
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
		}

		// Token: 0x04001002 RID: 4098
		private readonly string databaseName;
	}
}
