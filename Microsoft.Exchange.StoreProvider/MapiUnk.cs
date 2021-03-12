using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x02000018 RID: 24
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiUnk : DisposeTrackableBase
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00003452 File Offset: 0x00001652
		protected MapiUnk()
		{
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000345C File Offset: 0x0000165C
		protected MapiUnk(IExInterface iUnknown, object externalIUnknown, MapiStore mapiStore)
		{
			bool flag = false;
			try
			{
				if (externalIUnknown == null)
				{
					if (iUnknown == null || iUnknown.IsInvalid)
					{
						throw MapiExceptionHelper.ArgumentException("iUnknown", "Unable to create MapiUnk object with null or invalid interface handle.");
					}
				}
				else if (iUnknown != null)
				{
					throw MapiExceptionHelper.ArgumentException("iUnknown", "Cannot create external interace object with also an iUnknown interface.");
				}
				this.iUnknown = iUnknown;
				this.externalIUnknown = externalIUnknown;
				this.childRef = null;
				this.mapiStore = mapiStore;
				if (this.mapiStore != null)
				{
					this.childRef = this.mapiStore.AddChildReference(this);
					this.allowWarnings = this.mapiStore.AllowWarnings;
				}
				else
				{
					this.allowWarnings = false;
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.Dispose();
				}
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003510 File Offset: 0x00001710
		protected virtual void MapiInternalDispose()
		{
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003512 File Offset: 0x00001712
		protected virtual void PostMapiInternalDispose()
		{
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003514 File Offset: 0x00001714
		internal new void CheckDisposed()
		{
			if (base.IsDisposed)
			{
				throw MapiExceptionHelper.ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000352F File Offset: 0x0000172F
		internal void LockStore()
		{
			if (this.mapiStore != null)
			{
				this.mapiStore.LockConnection();
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003544 File Offset: 0x00001744
		internal void UnlockStore()
		{
			if (this.mapiStore != null)
			{
				this.mapiStore.UnlockConnection();
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003559 File Offset: 0x00001759
		internal bool IsExternal
		{
			get
			{
				return this.externalIUnknown != null;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003567 File Offset: 0x00001767
		public MapiStore MapiStore
		{
			get
			{
				this.CheckDisposed();
				return this.mapiStore;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003575 File Offset: 0x00001775
		public override void SuppressDisposeTracker()
		{
			if (this.iUnknown != null)
			{
				this.iUnknown.SuppressDisposeTracker();
			}
			base.SuppressDisposeTracker();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003590 File Offset: 0x00001790
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.LockStore();
				try
				{
					if (this.notificationCallbackIds != null)
					{
						foreach (ulong notificationCallbackId in this.notificationCallbackIds)
						{
							NotificationCallbackHelper.Instance.UnregisterNotificationHelper(notificationCallbackId);
						}
						this.notificationCallbackIds = null;
					}
					this.MapiInternalDispose();
					this.iUnknown.DisposeIfValid();
					this.iUnknown = null;
					MapiUnk.ReleaseObject(this.externalIUnknown);
					this.externalIUnknown = null;
					if (this.childRef != null)
					{
						DisposableRef.RemoveFromList(this.childRef);
						this.childRef.Dispose();
						this.childRef = null;
					}
					this.PostMapiInternalDispose();
				}
				finally
				{
					this.UnlockStore();
				}
				this.mapiStore = null;
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003674 File Offset: 0x00001874
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiUnk>(this);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600007B RID: 123 RVA: 0x0000367C File Offset: 0x0000187C
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00003684 File Offset: 0x00001884
		public bool AllowWarnings
		{
			get
			{
				return this.allowWarnings;
			}
			set
			{
				this.allowWarnings = value;
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000368D File Offset: 0x0000188D
		protected void ThrowIfError(string message, int hr)
		{
			if (this.IsExternal)
			{
				MapiExceptionHelper.ThrowIfError(message, hr, this.externalIUnknown, this.LastLowLevelException);
				return;
			}
			MapiExceptionHelper.ThrowIfError(message, hr, this.iUnknown, this.LastLowLevelException);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000036BE File Offset: 0x000018BE
		protected void ThrowIfErrorOrWarning(string message, int hr)
		{
			if (this.IsExternal)
			{
				MapiExceptionHelper.ThrowIfErrorOrWarning(message, hr, this.AllowWarnings, this.externalIUnknown, this.LastLowLevelException);
				return;
			}
			MapiExceptionHelper.ThrowIfErrorOrWarning(message, hr, this.AllowWarnings, this.iUnknown, this.LastLowLevelException);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000036FB File Offset: 0x000018FB
		protected void BlockExternalObjectCheck()
		{
			if (this.IsExternal)
			{
				throw MapiExceptionHelper.NotSupportedException("Method is not supported with external object.");
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003710 File Offset: 0x00001910
		protected static void ReleaseObject(object obj)
		{
			if (obj != null && Marshal.IsComObject(obj))
			{
				try
				{
					Marshal.ReleaseComObject(obj);
				}
				catch (InvalidComObjectException)
				{
				}
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003744 File Offset: 0x00001944
		protected ulong RegisterNotificationHelper(NotificationHelper notificationHelper)
		{
			this.LockStore();
			ulong result;
			try
			{
				if (this.notificationCallbackIds == null)
				{
					this.notificationCallbackIds = new List<ulong>(4);
				}
				ulong num = NotificationCallbackHelper.Instance.RegisterNotificationHelper(notificationHelper);
				this.notificationCallbackIds.Add(num);
				result = num;
			}
			finally
			{
				this.UnlockStore();
			}
			return result;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000037A0 File Offset: 0x000019A0
		protected void UnregisterNotificationHelper(ulong callbackId)
		{
			this.LockStore();
			if (this.notificationCallbackIds == null)
			{
				return;
			}
			try
			{
				this.notificationCallbackIds.Remove(callbackId);
				NotificationCallbackHelper.Instance.UnregisterNotificationHelper(callbackId);
			}
			finally
			{
				this.UnlockStore();
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000037F0 File Offset: 0x000019F0
		private Exception LastLowLevelException
		{
			get
			{
				if (this.mapiStore != null)
				{
					return this.mapiStore.LastLowLevelException;
				}
				return null;
			}
		}

		// Token: 0x04000085 RID: 133
		private IExInterface iUnknown;

		// Token: 0x04000086 RID: 134
		private object externalIUnknown;

		// Token: 0x04000087 RID: 135
		private MapiStore mapiStore;

		// Token: 0x04000088 RID: 136
		private DisposableRef childRef;

		// Token: 0x04000089 RID: 137
		private bool allowWarnings;

		// Token: 0x0400008A RID: 138
		private List<ulong> notificationCallbackIds;
	}
}
