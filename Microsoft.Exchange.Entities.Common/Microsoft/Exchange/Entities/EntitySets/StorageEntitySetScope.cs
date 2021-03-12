using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.TypeConversion.Converters;

namespace Microsoft.Exchange.Entities.EntitySets
{
	// Token: 0x0200003A RID: 58
	internal class StorageEntitySetScope<TStoreSession> : IStorageEntitySetScope<TStoreSession> where TStoreSession : class, IStoreSession
	{
		// Token: 0x0600012C RID: 300 RVA: 0x00004970 File Offset: 0x00002B70
		public StorageEntitySetScope(TStoreSession storeSession, IRecipientSession recipientSession, IXSOFactory xsoFactory, IdConverter idConverter = null)
		{
			this.StoreSession = storeSession;
			this.RecipientSession = recipientSession;
			this.XsoFactory = xsoFactory;
			this.IdConverter = (idConverter ?? IdConverter.Instance);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000499E File Offset: 0x00002B9E
		public StorageEntitySetScope(IStorageEntitySetScope<TStoreSession> scope) : this(scope.StoreSession, scope.RecipientSession, scope.XsoFactory, scope.IdConverter)
		{
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600012E RID: 302 RVA: 0x000049BE File Offset: 0x00002BBE
		// (set) Token: 0x0600012F RID: 303 RVA: 0x000049C6 File Offset: 0x00002BC6
		public IRecipientSession RecipientSession { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000049CF File Offset: 0x00002BCF
		// (set) Token: 0x06000131 RID: 305 RVA: 0x000049D7 File Offset: 0x00002BD7
		public TStoreSession StoreSession { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000049E0 File Offset: 0x00002BE0
		// (set) Token: 0x06000133 RID: 307 RVA: 0x000049E8 File Offset: 0x00002BE8
		public IXSOFactory XsoFactory { get; private set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000049F1 File Offset: 0x00002BF1
		// (set) Token: 0x06000135 RID: 309 RVA: 0x000049F9 File Offset: 0x00002BF9
		public IdConverter IdConverter { get; private set; }

		// Token: 0x06000136 RID: 310 RVA: 0x00004A04 File Offset: 0x00002C04
		public override string ToString()
		{
			TStoreSession storeSession = this.StoreSession;
			return storeSession.MailboxGuid.ToString();
		}
	}
}
