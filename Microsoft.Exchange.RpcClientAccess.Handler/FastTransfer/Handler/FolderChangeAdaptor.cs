using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x02000066 RID: 102
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class FolderChangeAdaptor : BaseObject, IFolderChange, IDisposable
	{
		// Token: 0x0600044A RID: 1098 RVA: 0x0001F177 File Offset: 0x0001D377
		internal FolderChangeAdaptor(IPropertyBag folderPropertyBag)
		{
			this.folderPropertyBag = folderPropertyBag;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0001F186 File Offset: 0x0001D386
		public IPropertyBag FolderPropertyBag
		{
			get
			{
				base.CheckDisposed();
				return this.folderPropertyBag;
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001F194 File Offset: 0x0001D394
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FolderChangeAdaptor>(this);
		}

		// Token: 0x0400017D RID: 381
		private readonly IPropertyBag folderPropertyBag;
	}
}
