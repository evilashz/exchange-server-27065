using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x02000077 RID: 119
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ManifestExCallbackHelper : ContentsManifestCallbackHelperBase<IMapiManifestExCallback>, IExchangeManifestExCallback
	{
		// Token: 0x060002FE RID: 766 RVA: 0x0000CF3B File Offset: 0x0000B13B
		public ManifestExCallbackHelper() : base(false)
		{
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000CF44 File Offset: 0x0000B144
		unsafe int IExchangeManifestExCallback.Change(ExchangeManifestCallbackChangeFlags flags, int cpvalHeader, SPropValue* ppvalHeader, int cpvalProps, SPropValue* ppvalProps)
		{
			return base.Change(flags, cpvalHeader, ppvalHeader, cpvalProps, ppvalProps);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000CF80 File Offset: 0x0000B180
		int IExchangeManifestExCallback.Delete(ExchangeManifestCallbackDeleteFlags flags, int cbIdsetDeleted, IntPtr pbIdsetDeleted)
		{
			if (cbIdsetDeleted > 0)
			{
				byte[] idsetDeleted = new byte[cbIdsetDeleted];
				Marshal.Copy(pbIdsetDeleted, idsetDeleted, 0, cbIdsetDeleted);
				ExchangeManifestCallbackDeleteFlags localFlags = flags;
				base.Deletes.Enqueue((IMapiManifestExCallback callback) => callback.Delete(idsetDeleted, (localFlags & ExchangeManifestCallbackDeleteFlags.SoftDelete) == ExchangeManifestCallbackDeleteFlags.SoftDelete, (localFlags & ExchangeManifestCallbackDeleteFlags.Expiry) == ExchangeManifestCallbackDeleteFlags.Expiry));
			}
			return 0;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000CFEC File Offset: 0x0000B1EC
		int IExchangeManifestExCallback.Read(ExchangeManifestCallbackReadFlags flags, int cbIdsetReadUnread, IntPtr pbIdsetReadUnread)
		{
			if (cbIdsetReadUnread > 0)
			{
				byte[] idsetReadUnread = new byte[cbIdsetReadUnread];
				Marshal.Copy(pbIdsetReadUnread, idsetReadUnread, 0, cbIdsetReadUnread);
				bool isRead = (flags & ExchangeManifestCallbackReadFlags.Read) == ExchangeManifestCallbackReadFlags.Read;
				base.Reads.Enqueue((IMapiManifestExCallback callback) => callback.ReadUnread(idsetReadUnread, isRead));
			}
			return 0;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000D041 File Offset: 0x0000B241
		protected override ManifestCallbackStatus DoChangeCallback(IMapiManifestExCallback callback, ManifestChangeType changeType, PropValue[] headerProps, PropValue[] messageProps)
		{
			return callback.Change(changeType == ManifestChangeType.Add, headerProps, messageProps);
		}
	}
}
