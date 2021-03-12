using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000036 RID: 54
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DisposableRef : DisposeTrackableBase
	{
		// Token: 0x06000132 RID: 306 RVA: 0x00007572 File Offset: 0x00005772
		internal DisposableRef(IForceReportDisposeTrackable childObject)
		{
			this.childObject = childObject;
			this.nextRef = this;
			this.prevRef = this;
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000133 RID: 307 RVA: 0x0000758F File Offset: 0x0000578F
		internal IForceReportDisposeTrackable Child
		{
			get
			{
				return this.childObject;
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00007597 File Offset: 0x00005797
		internal DisposableRef NextRef(DisposableRef item)
		{
			if (item.nextRef == this)
			{
				return null;
			}
			return this.nextRef;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000075AC File Offset: 0x000057AC
		internal DisposableRef AddToList(IForceReportDisposeTrackable iObject)
		{
			if (ComponentTrace<MapiNetTags>.CheckEnabled(33))
			{
				ComponentTrace<MapiNetTags>.Trace<string>(13106, 33, (long)this.GetHashCode(), "DisposableRef.AddToList: iObject={0}", TraceUtils.MakeHash(iObject));
			}
			DisposableRef disposableRef = new DisposableRef(iObject);
			disposableRef.nextRef = this.nextRef;
			disposableRef.prevRef = this;
			this.nextRef.prevRef = disposableRef;
			this.nextRef = disposableRef;
			if (ComponentTrace<MapiNetTags>.CheckEnabled(33))
			{
				ComponentTrace<MapiNetTags>.Trace<string>(11058, 33, (long)this.GetHashCode(), "DisposableRef.AddToList results: childRef={0}", TraceUtils.MakeHash(disposableRef));
			}
			return disposableRef;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00007638 File Offset: 0x00005838
		internal static void RemoveFromList(DisposableRef childRef)
		{
			if (ComponentTrace<MapiNetTags>.CheckEnabled(33))
			{
				ComponentTrace<MapiNetTags>.Trace<string>(15154, 33, 0L, "DisposableRef.RemoveFromList params: childRef={0}", TraceUtils.MakeHash(childRef));
			}
			if (childRef != null)
			{
				childRef.prevRef.nextRef = childRef.nextRef;
				childRef.nextRef.prevRef = childRef.prevRef;
				childRef.prevRef = childRef;
				childRef.nextRef = childRef;
				childRef.childObject = null;
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000076A3 File Offset: 0x000058A3
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.childObject != null)
			{
				this.childObject.Dispose();
				this.childObject = null;
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000076C2 File Offset: 0x000058C2
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<DisposableRef>(this);
		}

		// Token: 0x040003B6 RID: 950
		private IForceReportDisposeTrackable childObject;

		// Token: 0x040003B7 RID: 951
		private DisposableRef prevRef;

		// Token: 0x040003B8 RID: 952
		private DisposableRef nextRef;
	}
}
