using System;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200081C RID: 2076
	internal class SyncAccount : SyncObject
	{
		// Token: 0x06006697 RID: 26263 RVA: 0x0016B355 File Offset: 0x00169555
		public SyncAccount(SyncDirection syncDirection) : base(syncDirection)
		{
		}

		// Token: 0x17002429 RID: 9257
		// (get) Token: 0x06006698 RID: 26264 RVA: 0x0016B35E File Offset: 0x0016955E
		public override SyncObjectSchema Schema
		{
			get
			{
				return SyncAccount.schema;
			}
		}

		// Token: 0x1700242A RID: 9258
		// (get) Token: 0x06006699 RID: 26265 RVA: 0x0016B365 File Offset: 0x00169565
		internal override DirectoryObjectClass ObjectClass
		{
			get
			{
				return DirectoryObjectClass.Contact;
			}
		}

		// Token: 0x0600669A RID: 26266 RVA: 0x0016B368 File Offset: 0x00169568
		protected override DirectoryObject CreateDirectoryObject()
		{
			return new Account();
		}

		// Token: 0x1700242B RID: 9259
		// (get) Token: 0x0600669B RID: 26267 RVA: 0x0016B36F File Offset: 0x0016956F
		// (set) Token: 0x0600669C RID: 26268 RVA: 0x0016B381 File Offset: 0x00169581
		public SyncProperty<string> DisplayName
		{
			get
			{
				return (SyncProperty<string>)base[SyncAccountSchema.DisplayName];
			}
			set
			{
				base[SyncAccountSchema.DisplayName] = value;
			}
		}

		// Token: 0x040043BB RID: 17339
		private static readonly SyncAccountSchema schema = ObjectSchema.GetInstance<SyncAccountSchema>();
	}
}
