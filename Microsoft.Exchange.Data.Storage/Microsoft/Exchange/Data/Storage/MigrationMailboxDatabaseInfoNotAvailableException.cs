using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200010F RID: 271
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationMailboxDatabaseInfoNotAvailableException : MigrationTransientException
	{
		// Token: 0x060013DA RID: 5082 RVA: 0x00069DCE File Offset: 0x00067FCE
		public MigrationMailboxDatabaseInfoNotAvailableException(string mbxid) : base(ServerStrings.MigrationMailboxDatabaseInfoNotAvailable(mbxid))
		{
			this.mbxid = mbxid;
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x00069DE3 File Offset: 0x00067FE3
		public MigrationMailboxDatabaseInfoNotAvailableException(string mbxid, Exception innerException) : base(ServerStrings.MigrationMailboxDatabaseInfoNotAvailable(mbxid), innerException)
		{
			this.mbxid = mbxid;
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x00069DF9 File Offset: 0x00067FF9
		protected MigrationMailboxDatabaseInfoNotAvailableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mbxid = (string)info.GetValue("mbxid", typeof(string));
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x00069E23 File Offset: 0x00068023
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mbxid", this.mbxid);
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x060013DE RID: 5086 RVA: 0x00069E3E File Offset: 0x0006803E
		public string Mbxid
		{
			get
			{
				return this.mbxid;
			}
		}

		// Token: 0x0400099D RID: 2461
		private readonly string mbxid;
	}
}
