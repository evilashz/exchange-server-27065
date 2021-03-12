using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x02000162 RID: 354
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class FastTransferFolderContent : FastTransferFolderContentBase, IFastTransferProcessor<FastTransferDownloadContext>, IFastTransferProcessor<FastTransferUploadContext>, IDisposable
	{
		// Token: 0x060006A3 RID: 1699 RVA: 0x000141ED File Offset: 0x000123ED
		public FastTransferFolderContent(IFolder folder, FastTransferFolderContentBase.IncludeSubObject includeSubObject, bool isTopLevel) : base(folder, includeSubObject, isTopLevel)
		{
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x000141F8 File Offset: 0x000123F8
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FastTransferFolderContent>(this);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00014358 File Offset: 0x00012558
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferDownloadContext>.Process(FastTransferDownloadContext context)
		{
			base.CheckDisposed();
			if (!base.IsTopLevel)
			{
				yield return new FastTransferStateMachine?(new FastTransferStateMachine(this.DownloadRequiredProperties(context)));
			}
			IPropertyFilter filter = base.IsTopLevel ? context.PropertyFilterFactory.GetCopyFolderFilter() : context.PropertyFilterFactory.GetCopySubfolderFilter();
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(base.DownloadProperties(context, filter)));
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(base.DownloadSubObjects(context, false)));
			context.IncrementProgress();
			yield break;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x000144C0 File Offset: 0x000126C0
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferUploadContext>.Process(FastTransferUploadContext context)
		{
			base.CheckDisposed();
			if (!base.IsTopLevel)
			{
				yield return new FastTransferStateMachine?(new FastTransferStateMachine(this.UploadRequiredProperties(context)));
			}
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(base.UploadProperties(context)));
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(base.UploadMessages(context)));
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(base.UploadSubfolders(context)));
			yield break;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00014608 File Offset: 0x00012808
		private IEnumerator<FastTransferStateMachine?> DownloadRequiredProperties(FastTransferDownloadContext context)
		{
			yield return new FastTransferStateMachine?(FastTransferPropertyValue.Serialize(context, base.Folder.PropertyBag, base.Folder.PropertyBag.GetAnnotatedProperty(PropertyTag.Fid)));
			Feature.Stubbed(15505, "Support code page and string conversion.");
			yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferPropList(base.Folder.PropertyBag, new PropertyTag[]
			{
				PropertyTag.DisplayName,
				PropertyTag.Comment
			})));
			yield break;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00014710 File Offset: 0x00012910
		private IEnumerator<FastTransferStateMachine?> UploadRequiredProperties(FastTransferUploadContext context)
		{
			yield return new FastTransferStateMachine?(FastTransferPropertyValue.DeserializeInto(context, base.Folder.PropertyBag));
			PropertyValue fid = base.Folder.PropertyBag.GetAnnotatedProperty(PropertyTag.Fid).PropertyValue;
			if (fid.IsError)
			{
				throw new RopExecutionException(string.Format("The FID should have been downloaded in the first place. FID = {0}.", fid), (ErrorCode)2147942487U);
			}
			yield break;
		}
	}
}
