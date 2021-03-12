using System;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009B0 RID: 2480
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class MailboxManagementDataAdapter<TObject> : IDisposeTrackable, IDisposable where TObject : UserConfigurationObject, new()
	{
		// Token: 0x06005B72 RID: 23410 RVA: 0x0017D9C7 File Offset: 0x0017BBC7
		public MailboxManagementDataAdapter(MailboxSession session, string configuration)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			this.Session = session;
			this.Configuration = configuration;
			this.DisposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x17001913 RID: 6419
		// (get) Token: 0x06005B73 RID: 23411 RVA: 0x0017D9F7 File Offset: 0x0017BBF7
		// (set) Token: 0x06005B74 RID: 23412 RVA: 0x0017D9FF File Offset: 0x0017BBFF
		protected MailboxSession Session { get; set; }

		// Token: 0x17001914 RID: 6420
		// (get) Token: 0x06005B75 RID: 23413 RVA: 0x0017DA08 File Offset: 0x0017BC08
		// (set) Token: 0x06005B76 RID: 23414 RVA: 0x0017DA10 File Offset: 0x0017BC10
		protected string Configuration { get; set; }

		// Token: 0x17001915 RID: 6421
		// (get) Token: 0x06005B77 RID: 23415 RVA: 0x0017DA19 File Offset: 0x0017BC19
		// (set) Token: 0x06005B78 RID: 23416 RVA: 0x0017DA21 File Offset: 0x0017BC21
		private bool Disposed { get; set; }

		// Token: 0x17001916 RID: 6422
		// (get) Token: 0x06005B79 RID: 23417 RVA: 0x0017DA2A File Offset: 0x0017BC2A
		// (set) Token: 0x06005B7A RID: 23418 RVA: 0x0017DA32 File Offset: 0x0017BC32
		private DisposeTracker DisposeTracker { get; set; }

		// Token: 0x06005B7B RID: 23419 RVA: 0x0017DA3B File Offset: 0x0017BC3B
		public TObject Read(IExchangePrincipal principal)
		{
			this.CheckDisposed();
			MailboxManagementDataAdapter<TObject>.CheckPrincipal(principal);
			return this.InternalRead(principal);
		}

		// Token: 0x06005B7C RID: 23420 RVA: 0x0017DA50 File Offset: 0x0017BC50
		protected virtual TObject InternalRead(IExchangePrincipal principal)
		{
			TObject tobject = Activator.CreateInstance<TObject>();
			tobject.Principal = principal;
			this.InternalFill(tobject);
			tobject.ResetChangeTracking();
			return tobject;
		}

		// Token: 0x06005B7D RID: 23421 RVA: 0x0017DA86 File Offset: 0x0017BC86
		public void Fill(TObject configObject)
		{
			this.CheckDisposed();
			MailboxManagementDataAdapter<TObject>.CheckPrincipal(configObject.Principal);
			this.InternalFill(configObject);
		}

		// Token: 0x06005B7E RID: 23422 RVA: 0x0017DAA7 File Offset: 0x0017BCA7
		protected virtual void InternalFill(TObject configObject)
		{
		}

		// Token: 0x06005B7F RID: 23423 RVA: 0x0017DAA9 File Offset: 0x0017BCA9
		public void Delete(IExchangePrincipal principal)
		{
			this.CheckDisposed();
			MailboxManagementDataAdapter<TObject>.CheckPrincipal(principal);
			this.InternalDelete(principal);
		}

		// Token: 0x06005B80 RID: 23424 RVA: 0x0017DABE File Offset: 0x0017BCBE
		protected virtual void InternalDelete(IExchangePrincipal principal)
		{
		}

		// Token: 0x06005B81 RID: 23425 RVA: 0x0017DAC0 File Offset: 0x0017BCC0
		public void Save(TObject configObj)
		{
			this.CheckDisposed();
			if (configObj == null)
			{
				throw new ArgumentNullException("configObj");
			}
			MailboxManagementDataAdapter<TObject>.CheckPrincipal(configObj.Principal);
			this.InternalSave(configObj);
		}

		// Token: 0x06005B82 RID: 23426 RVA: 0x0017DAF4 File Offset: 0x0017BCF4
		protected virtual void InternalSave(TObject configObj)
		{
		}

		// Token: 0x06005B83 RID: 23427 RVA: 0x0017DAF8 File Offset: 0x0017BCF8
		private static void CheckPrincipal(IExchangePrincipal principal)
		{
			if (principal == null)
			{
				throw new ArgumentNullException("principal");
			}
			if (principal.ObjectId == null)
			{
				throw new ArgumentNullException("principal", "null == principal.ADObjectId");
			}
			if (Guid.Empty == principal.ObjectId.ObjectGuid)
			{
				throw new ArgumentOutOfRangeException("principal", "Guid.Empty == principal.ADObjectId.ObjectGuid");
			}
		}

		// Token: 0x06005B84 RID: 23428 RVA: 0x0017DB52 File Offset: 0x0017BD52
		private void CheckDisposed()
		{
			if (this.Disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x06005B85 RID: 23429 RVA: 0x0017DB6D File Offset: 0x0017BD6D
		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			if (this.Session != null)
			{
				this.Session = null;
			}
			if (this.DisposeTracker != null)
			{
				this.DisposeTracker.Dispose();
			}
			this.Disposed = true;
		}

		// Token: 0x06005B86 RID: 23430 RVA: 0x0017DB9C File Offset: 0x0017BD9C
		public void Dispose()
		{
			if (!this.Disposed)
			{
				this.Dispose(true);
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005B87 RID: 23431 RVA: 0x0017DBB3 File Offset: 0x0017BDB3
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MailboxManagementDataAdapter<TObject>>(this);
		}

		// Token: 0x06005B88 RID: 23432 RVA: 0x0017DBBB File Offset: 0x0017BDBB
		public void SuppressDisposeTracker()
		{
			if (this.DisposeTracker != null)
			{
				this.DisposeTracker.Suppress();
			}
		}
	}
}
