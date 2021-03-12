using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200075A RID: 1882
	[Serializable]
	public class SyncDeletedRecipient : ADPresentationObject
	{
		// Token: 0x06005B54 RID: 23380 RVA: 0x0013F9BC File Offset: 0x0013DBBC
		public SyncDeletedRecipient()
		{
		}

		// Token: 0x06005B55 RID: 23381 RVA: 0x0013F9C4 File Offset: 0x0013DBC4
		public SyncDeletedRecipient(DeletedRecipient deleteRecipient) : base(deleteRecipient)
		{
		}

		// Token: 0x17001FBB RID: 8123
		// (get) Token: 0x06005B56 RID: 23382 RVA: 0x0013F9CD File Offset: 0x0013DBCD
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return SyncDeletedRecipient.schema;
			}
		}

		// Token: 0x17001FBC RID: 8124
		// (get) Token: 0x06005B57 RID: 23383 RVA: 0x0013F9D4 File Offset: 0x0013DBD4
		private new bool IsValid
		{
			get
			{
				return base.IsValid;
			}
		}

		// Token: 0x17001FBD RID: 8125
		// (get) Token: 0x06005B58 RID: 23384 RVA: 0x0013F9DC File Offset: 0x0013DBDC
		private new string OriginatingServer
		{
			get
			{
				return base.OriginatingServer;
			}
		}

		// Token: 0x17001FBE RID: 8126
		// (get) Token: 0x06005B59 RID: 23385 RVA: 0x0013F9E4 File Offset: 0x0013DBE4
		private new ADObjectId ObjectCategory
		{
			get
			{
				return base.ObjectCategory;
			}
		}

		// Token: 0x17001FBF RID: 8127
		// (get) Token: 0x06005B5A RID: 23386 RVA: 0x0013F9EC File Offset: 0x0013DBEC
		private new ExchangeObjectVersion ExchangeVersion
		{
			get
			{
				return base.ExchangeVersion;
			}
		}

		// Token: 0x17001FC0 RID: 8128
		// (get) Token: 0x06005B5B RID: 23387 RVA: 0x0013F9F4 File Offset: 0x0013DBF4
		// (set) Token: 0x06005B5C RID: 23388 RVA: 0x0013FA06 File Offset: 0x0013DC06
		public bool EndOfList
		{
			get
			{
				return (bool)this[SyncDeletedObjectSchema.EndOfList];
			}
			internal set
			{
				this[SyncDeletedObjectSchema.EndOfList] = value;
			}
		}

		// Token: 0x17001FC1 RID: 8129
		// (get) Token: 0x06005B5D RID: 23389 RVA: 0x0013FA19 File Offset: 0x0013DC19
		// (set) Token: 0x06005B5E RID: 23390 RVA: 0x0013FA2B File Offset: 0x0013DC2B
		public byte[] Cookie
		{
			get
			{
				return (byte[])this[SyncDeletedObjectSchema.Cookie];
			}
			internal set
			{
				this[SyncDeletedObjectSchema.Cookie] = value;
			}
		}

		// Token: 0x04003E2B RID: 15915
		private static SyncDeletedObjectSchema schema = ObjectSchema.GetInstance<SyncDeletedObjectSchema>();
	}
}
