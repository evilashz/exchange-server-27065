using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002D9 RID: 729
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AbstractMailboxSession : AbstractStoreSession, IMailboxSession, IStoreSession, IDisposable
	{
		// Token: 0x06001F2B RID: 7979 RVA: 0x00086461 File Offset: 0x00084661
		public virtual DefaultFolderType IsDefaultFolderType(StoreId folderId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06001F2C RID: 7980 RVA: 0x00086468 File Offset: 0x00084668
		public virtual IUserConfigurationManager UserConfigurationManager
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x0008646F File Offset: 0x0008466F
		public virtual StoreObjectId GetDefaultFolderId(DefaultFolderType defaultFolderType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x00086476 File Offset: 0x00084676
		public virtual StoreObjectId CreateDefaultFolder(DefaultFolderType defaultFolderType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06001F2F RID: 7983 RVA: 0x0008647D File Offset: 0x0008467D
		public virtual string ClientInfoString
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06001F30 RID: 7984 RVA: 0x00086484 File Offset: 0x00084684
		public virtual CultureInfo PreferedCulture
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x0008648B File Offset: 0x0008468B
		public virtual bool IsMailboxOof()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x00086492 File Offset: 0x00084692
		public virtual bool IsGroupMailbox()
		{
			throw new NotImplementedException();
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06001F33 RID: 7987 RVA: 0x00086499 File Offset: 0x00084699
		public ContactFolders ContactFolders
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x000864A0 File Offset: 0x000846A0
		public void DeleteDefaultFolder(DefaultFolderType defaultFolderType, DeleteItemFlags deleteItemFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x000864A7 File Offset: 0x000846A7
		public CumulativeRPCPerformanceStatistics GetStoreCumulativeRPCStats()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x000864AE File Offset: 0x000846AE
		public virtual bool TryFixDefaultFolderId(DefaultFolderType defaultFolderType, out StoreObjectId id)
		{
			throw new NotImplementedException();
		}
	}
}
