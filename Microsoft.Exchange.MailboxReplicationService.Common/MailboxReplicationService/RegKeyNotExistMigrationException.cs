using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002FC RID: 764
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class RegKeyNotExistMigrationException : MailboxReplicationTransientException
	{
		// Token: 0x060024A5 RID: 9381 RVA: 0x00050477 File Offset: 0x0004E677
		public RegKeyNotExistMigrationException(string keyPath) : base(MrsStrings.ErrorRegKeyNotExist(keyPath))
		{
			this.keyPath = keyPath;
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x0005048C File Offset: 0x0004E68C
		public RegKeyNotExistMigrationException(string keyPath, Exception innerException) : base(MrsStrings.ErrorRegKeyNotExist(keyPath), innerException)
		{
			this.keyPath = keyPath;
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x000504A2 File Offset: 0x0004E6A2
		protected RegKeyNotExistMigrationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.keyPath = (string)info.GetValue("keyPath", typeof(string));
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x000504CC File Offset: 0x0004E6CC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("keyPath", this.keyPath);
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x060024A9 RID: 9385 RVA: 0x000504E7 File Offset: 0x0004E6E7
		public string KeyPath
		{
			get
			{
				return this.keyPath;
			}
		}

		// Token: 0x04000FFE RID: 4094
		private readonly string keyPath;
	}
}
