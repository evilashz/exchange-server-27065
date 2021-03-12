using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001A7 RID: 423
	internal class SortLCIDContext : DisposeTrackableBase
	{
		// Token: 0x06000FBF RID: 4031 RVA: 0x000254B8 File Offset: 0x000236B8
		public SortLCIDContext(MapiStore mapiStore, int lcidValue)
		{
			this.mapiStore = mapiStore;
			this.SetNewLcid(lcidValue);
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x000254CE File Offset: 0x000236CE
		public SortLCIDContext(StoreSession storeSession, int lcidValue)
		{
			this.storeSession = storeSession;
			this.SetNewLcid(lcidValue);
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x000254E4 File Offset: 0x000236E4
		private void SetNewLcid(int lcidValue)
		{
			if (lcidValue == 0)
			{
				this.originalLCIDValue = -1;
				return;
			}
			this.originalLCIDValue = this.ReadLcid();
			if (this.originalLCIDValue == lcidValue)
			{
				this.originalLCIDValue = -1;
				return;
			}
			this.SetLcid(lcidValue);
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x00025518 File Offset: 0x00023718
		private int ReadLcid()
		{
			if (this.mapiStore != null)
			{
				PropValue prop = this.mapiStore.GetProp(PropTag.SortLocaleId);
				if (!prop.IsNull() && !prop.IsError())
				{
					return prop.GetInt();
				}
				return 0;
			}
			else
			{
				this.storeSession.Mailbox.Load(new PropertyDefinition[]
				{
					SortLCIDContext.SortLciPropertyDefinition
				});
				object obj = this.storeSession.Mailbox[SortLCIDContext.SortLciPropertyDefinition];
				if (obj == null || obj is PropertyError)
				{
					return 0;
				}
				return (int)obj;
			}
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x000255A4 File Offset: 0x000237A4
		private void SetLcid(int lcidValue)
		{
			if (this.mapiStore != null)
			{
				this.mapiStore.SetProps(new PropValue[]
				{
					new PropValue(PropTag.SortLocaleId, lcidValue)
				});
				return;
			}
			this.storeSession.Mailbox[SortLCIDContext.SortLciPropertyDefinition] = lcidValue;
			this.storeSession.Mailbox.Save();
			this.storeSession.Mailbox.Load();
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x00025634 File Offset: 0x00023834
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.originalLCIDValue != -1)
			{
				CommonUtils.CatchKnownExceptions(delegate
				{
					this.SetLcid(this.originalLCIDValue);
				}, null);
			}
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x00025666 File Offset: 0x00023866
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SortLCIDContext>(this);
		}

		// Token: 0x040008F1 RID: 2289
		private static readonly PropertyDefinition SortLciPropertyDefinition = PropertyTagPropertyDefinition.CreateCustom(PropTag.SortLocaleId.ToString(), 1728380931U);

		// Token: 0x040008F2 RID: 2290
		private MapiStore mapiStore;

		// Token: 0x040008F3 RID: 2291
		private StoreSession storeSession;

		// Token: 0x040008F4 RID: 2292
		private int originalLCIDValue;
	}
}
