using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Protocols.FastTransfer
{
	// Token: 0x02000026 RID: 38
	internal class FastTransferRecipient : FastTransferPropertyBag, IRecipient
	{
		// Token: 0x0600019F RID: 415 RVA: 0x0000D9A0 File Offset: 0x0000BBA0
		public FastTransferRecipient(FastTransferDownloadContext downloadContext, MapiPerson mapiRecipient) : base(downloadContext, mapiRecipient, true, null)
		{
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000D9AC File Offset: 0x0000BBAC
		public FastTransferRecipient(FastTransferUploadContext uploadContext, MapiPerson mapiRecipient) : base(uploadContext, mapiRecipient)
		{
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000D9B6 File Offset: 0x0000BBB6
		public void Reinitialize(MapiPerson mapiRecipient)
		{
			base.Reinitialize(mapiRecipient);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000D9C0 File Offset: 0x0000BBC0
		protected override bool IncludeTag(StorePropTag propTag)
		{
			if (base.ForMoveUser && propTag.IsCategory(4))
			{
				return true;
			}
			ushort propId = propTag.PropId;
			return propId != 12288 && base.IncludeTag(propTag);
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000D9FA File Offset: 0x0000BBFA
		public IPropertyBag PropertyBag
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000D9FD File Offset: 0x0000BBFD
		public void Save()
		{
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000D9FF File Offset: 0x0000BBFF
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<FastTransferRecipient>(this);
		}
	}
}
