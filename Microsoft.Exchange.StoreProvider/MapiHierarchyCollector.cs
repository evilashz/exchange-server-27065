using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x020001C7 RID: 455
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiHierarchyCollector : MapiHierarchyCollectorBase
	{
		// Token: 0x060006BF RID: 1727 RVA: 0x00018CA2 File Offset: 0x00016EA2
		internal MapiHierarchyCollector(IExImportHierarchyChanges iExchangeImportHierarchyChanges, MapiStore mapiStore) : base(iExchangeImportHierarchyChanges, mapiStore)
		{
			this.iExchangeImportHierarchyChanges = iExchangeImportHierarchyChanges;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00018CB3 File Offset: 0x00016EB3
		protected override void MapiInternalDispose()
		{
			this.iExchangeImportHierarchyChanges = null;
			base.MapiInternalDispose();
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00018CC2 File Offset: 0x00016EC2
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiHierarchyCollector>(this);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00018CCC File Offset: 0x00016ECC
		public void Config(Stream stream, CollectorConfigFlags flags)
		{
			base.CheckDisposed();
			base.LockStore();
			try
			{
				MapiIStream iStream = null;
				if (stream != null)
				{
					iStream = new MapiIStream(stream);
				}
				int num = this.iExchangeImportHierarchyChanges.Config(iStream, (int)flags);
				if (num != 0)
				{
					base.ThrowIfErrorOrWarning("Unable to configure ICS collector.", num);
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00018D28 File Offset: 0x00016F28
		public void UpdateState(Stream stream)
		{
			base.CheckDisposed();
			base.LockStore();
			try
			{
				MapiIStream iStream = null;
				if (stream != null)
				{
					iStream = new MapiIStream(stream);
				}
				int num = this.iExchangeImportHierarchyChanges.UpdateState(iStream);
				if (num != 0)
				{
					base.ThrowIfErrorOrWarning("Unable to update collector state.", num);
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x04000612 RID: 1554
		private IExImportHierarchyChanges iExchangeImportHierarchyChanges;
	}
}
