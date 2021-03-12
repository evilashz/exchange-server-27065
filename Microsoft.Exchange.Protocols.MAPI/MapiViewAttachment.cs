using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.Mapi;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200008B RID: 139
	public sealed class MapiViewAttachment : MapiViewTableBase
	{
		// Token: 0x060004DB RID: 1243 RVA: 0x00021839 File Offset: 0x0001FA39
		public MapiViewAttachment() : base(MapiObjectType.AttachmentView)
		{
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00021844 File Offset: 0x0001FA44
		public void Configure(MapiContext context, MapiLogon mapiLogon, MapiMessage message)
		{
			if (base.IsDisposed)
			{
				ExTraceGlobals.GeneralTracer.TraceError(0L, "Configure called on a Dispose'd MapiViewAttachment!  Throwing ExExceptionInvalidObject!");
				throw new ExExceptionInvalidObject((LID)52216U, "Configure cannot be invoked after Dispose.");
			}
			if (base.IsValid)
			{
				ExTraceGlobals.GeneralTracer.TraceError(0L, "Configure called on already configured MapiViewAttachment!  Throwing ExExceptionInvalidObject!");
				throw new ExExceptionInvalidObject((LID)46072U, "Object has already been Configure'd");
			}
			if (message.IsDisposed)
			{
				ExTraceGlobals.GeneralTracer.TraceError(0L, "Configure called on a Dispose'd MapiMessage!  Throwing ExExceptionInvalidObject!");
				throw new ExExceptionInvalidObject((LID)62456U, "Configure cannot be invoked after Dispose.");
			}
			AttachmentViewTable storeViewTable = new AttachmentViewTable(context, mapiLogon.StoreMailbox, message.StoreMessage);
			base.Configure(context, mapiLogon, Array<StorePropTag>.Empty, storeViewTable, uint.MaxValue, null);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x000218FE File Offset: 0x0001FAFE
		protected override bool CanSortOnProperty(StorePropTag propTag)
		{
			return true;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00021901 File Offset: 0x0001FB01
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiViewAttachment>(this);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00021909 File Offset: 0x0001FB09
		protected override void InternalDispose(bool calledFromDispose)
		{
			base.InternalDispose(calledFromDispose);
		}
	}
}
