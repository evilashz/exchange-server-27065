using System;
using System.Threading;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Inference.Common;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Inference.Mdb
{
	// Token: 0x0200000D RID: 13
	internal sealed class MdbDocument : Document, IDisposableDocument, IDocument, IPropertyBag, IReadOnlyPropertyBag, IDisposeTrackable, IDisposable
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00003373 File Offset: 0x00001573
		internal MdbDocument(MdbCompositeItemIdentity identity, PropertyDefinition[] propertiesToPreload, MailboxSession session, MdbPropertyMap propertyMap, DocumentOperation operation) : this(identity, propertiesToPreload, null, session, propertyMap, operation)
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003383 File Offset: 0x00001583
		internal MdbDocument(MdbCompositeItemIdentity identity, PropertyDefinition[] propertiesToPreload, Item item, MdbPropertyMap propertyMap, DocumentOperation operation) : this(identity, propertiesToPreload, item, null, propertyMap, operation)
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003394 File Offset: 0x00001594
		internal MdbDocument(MdbCompositeItemIdentity identity, PropertyDefinition[] propertiesToPreload, Item item, MailboxSession session, MdbPropertyMap propertyMap, DocumentOperation operation) : this(identity, operation, new MdbDocumentAdapter(identity, propertiesToPreload, item, session, propertyMap, true))
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000033B7 File Offset: 0x000015B7
		internal MdbDocument(MdbCompositeItemIdentity identity, DocumentOperation operation, MdbDocumentAdapter documentAdapter) : base(identity, operation, documentAdapter)
		{
			Util.ThrowOnNullArgument(documentAdapter, "documentAdapter");
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000033D9 File Offset: 0x000015D9
		public bool IsDisposed
		{
			get
			{
				return Interlocked.CompareExchange(ref this.isDisposedFlag, 0, 0) != 0;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000033EE File Offset: 0x000015EE
		public bool IsDisposing
		{
			get
			{
				return Interlocked.CompareExchange(ref this.isDisposingFlag, 0, 0) != 0;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00003403 File Offset: 0x00001603
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00003416 File Offset: 0x00001616
		internal MdbDocumentAdapter MdbDocumentAdapter
		{
			get
			{
				this.CheckDisposed();
				return base.DocumentAdapter as MdbDocumentAdapter;
			}
			private set
			{
				base.DocumentAdapter = value;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000341F File Offset: 0x0000161F
		public override bool TryGetProperty(PropertyDefinition property, out object value)
		{
			return base.TryGetProperty(property, out value);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003429 File Offset: 0x00001629
		public override void SetProperty(PropertyDefinition property, object value)
		{
			base.SetProperty(property, value);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003434 File Offset: 0x00001634
		public override string ToString()
		{
			this.CheckDisposed();
			object obj = null;
			this.TryGetProperty(DocumentSchema.MailboxId, out obj);
			return string.Format("OP:{0}, DID:{1}, MID:{2}", base.Operation, base.Identity, (obj == null) ? string.Empty : ((string)obj));
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003482 File Offset: 0x00001682
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003491 File Offset: 0x00001691
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MdbDocument>(this);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003499 File Offset: 0x00001699
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000034AE File Offset: 0x000016AE
		private void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.MdbDocumentAdapter != null)
			{
				this.MdbDocumentAdapter.Dispose();
				this.MdbDocumentAdapter = null;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000034CD File Offset: 0x000016CD
		private void CheckDisposed()
		{
			if (this.IsDisposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000034E8 File Offset: 0x000016E8
		private void Dispose(bool calledFromDispose)
		{
			if (Interlocked.Exchange(ref this.isDisposingFlag, 1) == 0)
			{
				try
				{
					if (!this.IsDisposed)
					{
						if (calledFromDispose && this.disposeTracker != null)
						{
							this.disposeTracker.Dispose();
							this.disposeTracker = null;
						}
						this.InternalDispose(calledFromDispose);
						Interlocked.Exchange(ref this.isDisposedFlag, 1);
					}
				}
				finally
				{
					Interlocked.Exchange(ref this.isDisposingFlag, 0);
				}
			}
		}

		// Token: 0x04000033 RID: 51
		private int isDisposedFlag;

		// Token: 0x04000034 RID: 52
		private int isDisposingFlag;

		// Token: 0x04000035 RID: 53
		private DisposeTracker disposeTracker;
	}
}
