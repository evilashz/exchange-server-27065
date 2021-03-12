using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x02000163 RID: 355
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class FastTransferFolderContentWithDelProp : FastTransferFolderContentBase, IFastTransferProcessor<FastTransferDownloadContext>, IFastTransferProcessor<FastTransferUploadContext>, IDisposable
	{
		// Token: 0x060006A9 RID: 1705 RVA: 0x00014733 File Offset: 0x00012933
		public FastTransferFolderContentWithDelProp(IFolder folder, FastTransferFolderContentBase.IncludeSubObject includeSubObject) : base(folder, includeSubObject, true)
		{
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0001473E File Offset: 0x0001293E
		public FastTransferFolderContentWithDelProp(IFolder folder) : base(folder, FastTransferFolderContentBase.IncludeSubObject.All, true)
		{
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00014749 File Offset: 0x00012949
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FastTransferFolderContentWithDelProp>(this);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001484C File Offset: 0x00012A4C
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferDownloadContext>.Process(FastTransferDownloadContext context)
		{
			base.CheckDisposed();
			IPropertyFilter filter = context.PropertyFilterFactory.GetFolderCopyToFilter();
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(base.DownloadProperties(context, filter)));
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(base.DownloadSubObjects(context, true)));
			context.IncrementProgress();
			yield break;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00014A70 File Offset: 0x00012C70
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferUploadContext>.Process(FastTransferUploadContext context)
		{
			base.CheckDisposed();
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(base.UploadProperties(context)));
			while (!context.NoMoreData)
			{
				PropertyTag markerOrMetaProperty;
				if (context.DataInterface.TryPeekMarker(out markerOrMetaProperty))
				{
					if (markerOrMetaProperty == PropertyTag.StartMessage || markerOrMetaProperty == PropertyTag.StartFAIMsg)
					{
						yield return new FastTransferStateMachine?(new FastTransferStateMachine(base.UploadMessages(context)));
					}
					else
					{
						if (!(markerOrMetaProperty == PropertyTag.StartSubFld))
						{
							break;
						}
						yield return new FastTransferStateMachine?(new FastTransferStateMachine(base.UploadSubfolders(context)));
					}
				}
				else
				{
					if (!(markerOrMetaProperty == PropertyTag.FXDelProp) && !(markerOrMetaProperty == PropertyTag.DNPrefix) && !(markerOrMetaProperty == PropertyTag.NewFXFolder))
					{
						throw new RopExecutionException(string.Format("Property {0} was not expected", markerOrMetaProperty), ErrorCode.FxUnexpectedMarker);
					}
					yield return new FastTransferStateMachine?(FastTransferPropertyValue.SkipPropertyIfExists(context, markerOrMetaProperty));
				}
			}
			yield break;
		}
	}
}
