using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002D8 RID: 728
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AbstractStoreSession : IStoreSession, IDisposable
	{
		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06001F17 RID: 7959 RVA: 0x000863D8 File Offset: 0x000845D8
		public bool IsMoveUser
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06001F18 RID: 7960 RVA: 0x000863DB File Offset: 0x000845DB
		public virtual IExchangePrincipal MailboxOwner
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06001F19 RID: 7961 RVA: 0x000863E2 File Offset: 0x000845E2
		public virtual IActivitySession ActivitySession
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06001F1A RID: 7962 RVA: 0x000863E9 File Offset: 0x000845E9
		public virtual CultureInfo Culture
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06001F1B RID: 7963 RVA: 0x000863F0 File Offset: 0x000845F0
		public virtual string DisplayAddress
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06001F1C RID: 7964 RVA: 0x000863F7 File Offset: 0x000845F7
		public virtual OrganizationId OrganizationId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06001F1D RID: 7965 RVA: 0x000863FE File Offset: 0x000845FE
		public virtual IdConverter IdConverter
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x00086405 File Offset: 0x00084605
		public virtual AggregateOperationResult Delete(DeleteItemFlags deleteFlags, params StoreId[] ids)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x0008640C File Offset: 0x0008460C
		public virtual void Dispose()
		{
			throw new NotImplementedException();
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06001F20 RID: 7968 RVA: 0x00086413 File Offset: 0x00084613
		public virtual IXSOMailbox Mailbox
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06001F21 RID: 7969 RVA: 0x0008641A File Offset: 0x0008461A
		public virtual Guid MailboxGuid
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06001F22 RID: 7970 RVA: 0x00086421 File Offset: 0x00084621
		public virtual LogonType LogonType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06001F23 RID: 7971 RVA: 0x00086428 File Offset: 0x00084628
		public virtual SessionCapabilities Capabilities
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06001F24 RID: 7972 RVA: 0x0008642F File Offset: 0x0008462F
		public virtual Guid MdbGuid
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06001F25 RID: 7973 RVA: 0x00086436 File Offset: 0x00084636
		// (set) Token: 0x06001F26 RID: 7974 RVA: 0x0008643D File Offset: 0x0008463D
		public virtual ExTimeZone ExTimeZone
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x00086444 File Offset: 0x00084644
		public virtual StoreObjectId GetParentFolderId(StoreObjectId objectId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x0008644B File Offset: 0x0008464B
		public IRecipientSession GetADRecipientSession(bool isReadOnly, ConsistencyMode consistencyMode)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x00086452 File Offset: 0x00084652
		public IConfigurationSession GetADConfigurationSession(bool isReadOnly, ConsistencyMode consistencyMode)
		{
			throw new NotImplementedException();
		}
	}
}
