using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x0200008F RID: 143
	internal class StreamSource<T> : StreamSource where T : IDisposable
	{
		// Token: 0x060005C3 RID: 1475 RVA: 0x00028797 File Offset: 0x00026997
		public StreamSource(ReferenceCount<T> coreReference, Func<T, ICorePropertyBag> getPropertyBagFunc)
		{
			this.coreReference = coreReference;
			this.coreReference.AddRef();
			this.getPropertyBagFunc = getPropertyBagFunc;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x000287B9 File Offset: 0x000269B9
		public override ICorePropertyBag PropertyBag
		{
			get
			{
				return this.getPropertyBagFunc(this.coreReference.ReferencedObject);
			}
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x000287D1 File Offset: 0x000269D1
		public override void OnAccess()
		{
			this.PropertyBag.Load(null);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x000287DF File Offset: 0x000269DF
		public override StreamSource Duplicate()
		{
			return new StreamSource<T>(this.coreReference, this.getPropertyBagFunc);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000287F2 File Offset: 0x000269F2
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<StreamSource<T>>(this);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x000287FA File Offset: 0x000269FA
		protected override void InternalDispose()
		{
			this.coreReference.Release();
			base.InternalDispose();
		}

		// Token: 0x04000267 RID: 615
		private readonly ReferenceCount<T> coreReference;

		// Token: 0x04000268 RID: 616
		private readonly Func<T, ICorePropertyBag> getPropertyBagFunc;
	}
}
