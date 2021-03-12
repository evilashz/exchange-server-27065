using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x02000186 RID: 390
	internal sealed class IcsHierarchySynchronizer : FastTransferObject, IFastTransferProcessor<FastTransferDownloadContext>, IFastTransferProcessor<FastTransferUploadContext>, IDisposable
	{
		// Token: 0x060007B0 RID: 1968 RVA: 0x0001B4A0 File Offset: 0x000196A0
		public IcsHierarchySynchronizer(IHierarchySynchronizer hierarchySynchronizer, IHierarchySynchronizerClient hierarchySynchronizerClient, IcsHierarchySynchronizer.Options options) : base(true)
		{
			this.hierarchySynchronizer = hierarchySynchronizer;
			this.hierarchySynchronizerClient = hierarchySynchronizerClient;
			this.options = options;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0001B4BE File Offset: 0x000196BE
		public IcsHierarchySynchronizer(IHierarchySynchronizer hierarchySynchronizer, IcsHierarchySynchronizer.Options options) : this(hierarchySynchronizer, null, options)
		{
			Util.ThrowOnNullArgument(hierarchySynchronizer, "hierarchySynchronizer");
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001B4D4 File Offset: 0x000196D4
		internal IcsHierarchySynchronizer(IHierarchySynchronizerClient hierarchySynchronizerClient, IcsHierarchySynchronizer.Options options) : this(null, hierarchySynchronizerClient, options)
		{
			Util.ThrowOnNullArgument(hierarchySynchronizerClient, "hierarchySynchronizerClient");
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0001B8E8 File Offset: 0x00019AE8
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferDownloadContext>.Process(FastTransferDownloadContext context)
		{
			using (IEnumerator<IFolderChange> changes = this.hierarchySynchronizer.GetChanges())
			{
				bool includeFid = (this.options & IcsHierarchySynchronizer.Options.IncludeFid) == IcsHierarchySynchronizer.Options.IncludeFid;
				bool includeChangeNumber = (this.options & IcsHierarchySynchronizer.Options.IncludeChangeNumber) == IcsHierarchySynchronizer.Options.IncludeChangeNumber;
				IPropertyFilter propertyFilter = context.PropertyFilterFactory.GetIcsHierarchyFilter(includeFid, includeChangeNumber);
				while (changes.MoveNext())
				{
					using (IFolderChange currentFolderChange = changes.Current)
					{
						context.DataInterface.PutMarker(PropertyTag.IncrSyncChg);
						yield return null;
						using (IEnumerator<PropertyTag> enumerator = currentFolderChange.FolderPropertyBag.WithNoValue(this.FilterPropertyList(IcsHierarchySynchronizer.RequiredFolderChangeProperties, propertyFilter)).GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								PropertyTag propertyTag = enumerator.Current;
								throw new RopExecutionException(string.Format("Required folder change property {0} is missing", propertyTag), (ErrorCode)2147942487U);
							}
						}
						yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferPropList(currentFolderChange.FolderPropertyBag, propertyFilter)));
					}
				}
			}
			IPropertyBag deletions = this.hierarchySynchronizer.GetDeletions();
			AnnotatedPropertyValue idSetDeletedValue = deletions.GetAnnotatedProperty(PropertyTag.IdsetDeleted);
			if (!idSetDeletedValue.PropertyValue.IsNotFound)
			{
				context.DataInterface.PutMarker(PropertyTag.IncrSyncDel);
				yield return null;
				yield return new FastTransferStateMachine?(FastTransferPropertyValue.Serialize(context, deletions, idSetDeletedValue));
			}
			FastTransferIcsState state = this.CreateDownloadFinalState();
			yield return new FastTransferStateMachine?(context.CreateStateMachine(state));
			context.DataInterface.PutMarker(PropertyTag.IncrSyncEnd);
			yield break;
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0001B90C File Offset: 0x00019B0C
		private FastTransferIcsState CreateDownloadFinalState()
		{
			FastTransferIcsState result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				IIcsState finalState = this.hierarchySynchronizer.GetFinalState();
				disposeGuard.Add<IIcsState>(finalState);
				FastTransferIcsState fastTransferIcsState = new FastTransferIcsState(finalState);
				disposeGuard.Add<FastTransferIcsState>(fastTransferIcsState);
				disposeGuard.Success();
				result = fastTransferIcsState;
			}
			return result;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0001BB54 File Offset: 0x00019D54
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferUploadContext>.Process(FastTransferUploadContext context)
		{
			PropertyTag propertyTag;
			context.DataInterface.TryPeekMarker(out propertyTag);
			while (propertyTag == PropertyTag.IncrSyncChg)
			{
				context.DataInterface.ReadMarker(PropertyTag.IncrSyncChg);
				yield return null;
				IPropertyBag folderChanges = this.hierarchySynchronizerClient.LoadFolderChanges();
				do
				{
					yield return new FastTransferStateMachine?(FastTransferPropertyValue.DeserializeInto(context, folderChanges));
				}
				while (!context.DataInterface.TryPeekMarker(out propertyTag));
			}
			IPropertyBag folderDeletion = this.hierarchySynchronizerClient.LoadFolderDeletion();
			if (propertyTag == PropertyTag.IncrSyncDel)
			{
				context.DataInterface.ReadMarker(PropertyTag.IncrSyncDel);
				yield return new FastTransferStateMachine?(FastTransferPropertyValue.DeserializeInto(context, folderDeletion));
			}
			FastTransferIcsState state = this.CreateUploadFinalState();
			yield return new FastTransferStateMachine?(context.CreateStateMachine(state));
			context.DataInterface.ReadMarker(PropertyTag.IncrSyncEnd);
			yield break;
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0001BB78 File Offset: 0x00019D78
		private FastTransferIcsState CreateUploadFinalState()
		{
			FastTransferIcsState result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				IIcsState icsState = this.hierarchySynchronizerClient.LoadFinalState();
				disposeGuard.Add<IIcsState>(icsState);
				FastTransferIcsState fastTransferIcsState = new FastTransferIcsState(icsState);
				disposeGuard.Add<FastTransferIcsState>(fastTransferIcsState);
				disposeGuard.Success();
				result = fastTransferIcsState;
			}
			return result;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0001BBF4 File Offset: 0x00019DF4
		private IEnumerable<PropertyTag> FilterPropertyList(IEnumerable<PropertyTag> propertyTags, IPropertyFilter propertyFilter)
		{
			return from propertyTag in propertyTags
			where propertyFilter.IncludeProperty(propertyTag)
			select propertyTag;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0001BC20 File Offset: 0x00019E20
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.hierarchySynchronizer);
			Util.DisposeIfPresent(this.hierarchySynchronizerClient);
			base.InternalDispose();
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0001BC3E File Offset: 0x00019E3E
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<IcsHierarchySynchronizer>(this);
		}

		// Token: 0x040003D1 RID: 977
		private readonly IHierarchySynchronizer hierarchySynchronizer;

		// Token: 0x040003D2 RID: 978
		private readonly IHierarchySynchronizerClient hierarchySynchronizerClient;

		// Token: 0x040003D3 RID: 979
		private readonly IcsHierarchySynchronizer.Options options;

		// Token: 0x040003D4 RID: 980
		public static readonly ReadOnlyCollection<PropertyTag> RequiredFolderChangeProperties = new ReadOnlyCollection<PropertyTag>(new PropertyTag[]
		{
			PropertyTag.ExternalParentFid,
			PropertyTag.ExternalFid,
			PropertyTag.LastModificationTime,
			PropertyTag.ExternalChangeNumber,
			PropertyTag.ExternalPredecessorChangeList,
			PropertyTag.DisplayName,
			PropertyTag.Fid
		});

		// Token: 0x02000187 RID: 391
		[Flags]
		internal enum Options
		{
			// Token: 0x040003D6 RID: 982
			None = 0,
			// Token: 0x040003D7 RID: 983
			IncludeFid = 1,
			// Token: 0x040003D8 RID: 984
			IncludeChangeNumber = 2
		}
	}
}
