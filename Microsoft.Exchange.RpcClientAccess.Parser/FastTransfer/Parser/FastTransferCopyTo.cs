using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x0200015B RID: 347
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class FastTransferCopyTo : FastTransferObject, IFastTransferProcessor<FastTransferDownloadContext>, IFastTransferProcessor<FastTransferUploadContext>, IDisposable
	{
		// Token: 0x06000664 RID: 1636 RVA: 0x00012894 File Offset: 0x00010A94
		protected FastTransferCopyTo(bool isShallowCopy, IPropertyBag propertyBag, bool isTopLevel) : base(isTopLevel)
		{
			this.isShallowCopy = isShallowCopy;
			this.propertyBag = propertyBag;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0001296C File Offset: 0x00010B6C
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferDownloadContext>.Process(FastTransferDownloadContext context)
		{
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(this.DownloadProperties(context)));
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(this.DownloadContents(context)));
			yield break;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00012A5C File Offset: 0x00010C5C
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferUploadContext>.Process(FastTransferUploadContext context)
		{
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(this.UploadProperties(context)));
			if (!context.NoMoreData)
			{
				yield return new FastTransferStateMachine?(new FastTransferStateMachine(this.UploadContents(context)));
			}
			yield break;
		}

		// Token: 0x06000667 RID: 1639
		protected abstract IPropertyFilter GetDownloadPropertiesFilter(FastTransferDownloadContext context);

		// Token: 0x06000668 RID: 1640 RVA: 0x00012B2C File Offset: 0x00010D2C
		protected virtual IEnumerator<FastTransferStateMachine?> DownloadProperties(FastTransferDownloadContext context)
		{
			IPropertyFilter filter = this.GetDownloadPropertiesFilter(context);
			yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferPropList(this.propertyBag, filter)));
			yield break;
		}

		// Token: 0x06000669 RID: 1641
		protected abstract IEnumerator<FastTransferStateMachine?> DownloadContents(FastTransferDownloadContext context);

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x00012B4F File Offset: 0x00010D4F
		protected bool IsShallowCopy
		{
			get
			{
				return this.isShallowCopy;
			}
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00012C18 File Offset: 0x00010E18
		protected virtual IEnumerator<FastTransferStateMachine?> UploadProperties(FastTransferUploadContext context)
		{
			yield return new FastTransferStateMachine?(FastTransferPropertyValue.SkipPropertyIfExists(context, PropertyTag.DNPrefix));
			yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferPropList(this.propertyBag)));
			yield break;
		}

		// Token: 0x0600066C RID: 1644
		protected abstract IEnumerator<FastTransferStateMachine?> UploadContents(FastTransferUploadContext context);

		// Token: 0x04000344 RID: 836
		private readonly IPropertyBag propertyBag;

		// Token: 0x04000345 RID: 837
		private readonly bool isShallowCopy;
	}
}
