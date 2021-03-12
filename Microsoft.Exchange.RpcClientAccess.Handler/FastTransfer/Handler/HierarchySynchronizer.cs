using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Exchange.RpcClientAccess.Handler;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x02000068 RID: 104
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class HierarchySynchronizer : SynchronizerBase, IHierarchySynchronizer, IDisposable
	{
		// Token: 0x06000451 RID: 1105 RVA: 0x0001F2F4 File Offset: 0x0001D4F4
		internal HierarchySynchronizer(ReferenceCount<CoreFolder> referenceCoreFolder, SyncFlag syncFlags, SyncExtraFlag extraFlags, IcsState icsState, params PropertyTag[] referencePropertyTags) : base(referenceCoreFolder, syncFlags, extraFlags, icsState)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.excludePropertyTags = new HashSet<PropertyTag>(referencePropertyTags);
				ManifestConfigFlags manifestFlags = HierarchySynchronizer.GetManifestFlags(syncFlags, extraFlags, ref this.excludePropertyTags);
				this.syncRootFolderLongTermId = referenceCoreFolder.ReferencedObject.Session.IdConverter.GetLongTermIdFromId(referenceCoreFolder.ReferencedObject.Session.IdConverter.GetFidFromId(referenceCoreFolder.ReferencedObject.Id.ObjectId));
				NativeStorePropertyDefinition[] propertyDefinitionsIgnoreTypeChecking = MEDSPropertyTranslator.GetPropertyDefinitionsIgnoreTypeChecking(referenceCoreFolder.ReferencedObject.Session, referenceCoreFolder.ReferencedObject.PropertyBag, this.excludePropertyTags.ToArray<PropertyTag>());
				IPropertyBag propertyBag = new MemoryPropertyBag(this.SessionAdaptor);
				icsState.Checkpoint(propertyBag);
				IcsStateStream icsStateStream = new IcsStateStream(propertyBag);
				this.manifestProvider = this.SyncRootFolder.ReferencedObject.GetHierarchyManifest(manifestFlags, icsStateStream.ToXsoState(), Array<StorePropertyDefinition>.Empty, propertyDefinitionsIgnoreTypeChecking);
				disposeGuard.Success();
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0001F3FC File Offset: 0x0001D5FC
		public static void CheckFlags(SyncFlag syncFlag, SyncExtraFlag extraFlags)
		{
			HashSet<PropertyTag> hashSet = new HashSet<PropertyTag>();
			HierarchySynchronizer.GetManifestFlags(syncFlag, extraFlags, ref hashSet);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001F510 File Offset: 0x0001D710
		public IEnumerator<IFolderChange> GetChanges()
		{
			base.CheckDisposed();
			ManifestFolderChange change;
			while (this.manifestProvider.TryGetNextChange(out change))
			{
				IPropertyBag propertyBag = new MemoryPropertyBag(this.SessionAdaptor);
				SynchronizerBase.SetPropertyValuesFromServer(propertyBag, this.SyncRootFolder.ReferencedObject.Session, change.PropertyValues);
				this.CheckAndFixFolderChanges(propertyBag);
				yield return new FolderChangeAdaptor(propertyBag);
			}
			yield break;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0001F52C File Offset: 0x0001D72C
		public IPropertyBag GetDeletions()
		{
			base.CheckDisposed();
			IPropertyBag propertyBag = new MemoryPropertyBag(this.SessionAdaptor);
			ManifestFolderDeletion manifestFolderDeletion;
			if (this.manifestProvider.TryGetDeletion(out manifestFolderDeletion))
			{
				propertyBag.SetProperty(new PropertyValue(PropertyTag.IdsetDeleted, manifestFolderDeletion.IdsetDeleted));
			}
			else
			{
				propertyBag.Delete(PropertyTag.IdsetDeleted);
			}
			return propertyBag;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0001F580 File Offset: 0x0001D780
		public IIcsState GetFinalState()
		{
			base.CheckDisposed();
			IPropertyBag propertyBag = new MemoryPropertyBag(this.SessionAdaptor);
			IcsStateStream icsStateStream = new IcsStateStream(propertyBag);
			StorageIcsState state = icsStateStream.ToXsoState();
			this.manifestProvider.GetFinalState(ref state);
			icsStateStream.FromXsoState(state);
			this.IcsState.Load(IcsStateOrigin.ServerFinal, propertyBag);
			return new IcsStateAdaptor(this.IcsState, this.SyncRootFolder);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0001F5DF File Offset: 0x0001D7DF
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.manifestProvider);
			base.InternalDispose();
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0001F5F2 File Offset: 0x0001D7F2
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<HierarchySynchronizer>(this);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0001F5FC File Offset: 0x0001D7FC
		private static ManifestConfigFlags GetManifestFlags(SyncFlag syncFlag, SyncExtraFlag extraFlags, ref HashSet<PropertyTag> excludePropertyTags)
		{
			ManifestConfigFlags result = ManifestConfigFlags.None;
			if ((ushort)(syncFlag & ~(SyncFlag.Unicode | SyncFlag.NoDeletions | SyncFlag.NoForeignKeys)) != 0)
			{
				throw new RopExecutionException(string.Format("Unsupported SyncFlag present: {0}", syncFlag), (ErrorCode)2147746050U);
			}
			SynchronizerBase.TranslateFlag(SyncFlag.NoDeletions, ManifestConfigFlags.NoDeletions, syncFlag, ref result);
			if ((extraFlags & (SyncExtraFlag.MessageSize | SyncExtraFlag.OrderByDeliveryTime | SyncExtraFlag.ManifestMode | SyncExtraFlag.CatchUpFull)) != SyncExtraFlag.None)
			{
				throw new RopExecutionException(string.Format("Unsupported SyncExtraFlag present: {0}", extraFlags), (ErrorCode)2147746050U);
			}
			excludePropertyTags.UnionWith(HierarchySynchronizer.MustExcludeProperties);
			excludePropertyTags.ExceptWith(IcsHierarchySynchronizer.RequiredFolderChangeProperties);
			if ((ushort)(syncFlag & SyncFlag.Unicode) == 0)
			{
				throw Feature.NotImplemented(212753, "codepage conversion");
			}
			return result;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0001F68C File Offset: 0x0001D88C
		private void HonorOptionNoForeignKeys(IPropertyBag propertyBag)
		{
			if ((ushort)(this.SyncFlags & SyncFlag.NoForeignKeys) == 256)
			{
				propertyBag.SetProperty(new PropertyValue(PropertyTag.ExternalFid, base.ConvertIdToLongTermId(propertyBag, PropertyTag.Fid)));
				propertyBag.SetProperty(new PropertyValue(PropertyTag.ExternalParentFid, base.ConvertIdToLongTermId(propertyBag, PropertyTag.ParentFid)));
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0001F6E5 File Offset: 0x0001D8E5
		private void CheckAndFixFolderChanges(IPropertyBag propertyBag)
		{
			SynchronizerBase.CheckRequiredProperties(propertyBag, HierarchySynchronizer.RequiredInputProperties);
			this.HonorOptionNoForeignKeys(propertyBag);
			if (this.IsChildOfSyncRoot(propertyBag))
			{
				propertyBag.SetProperty(new PropertyValue(PropertyTag.ExternalParentFid, Array<byte>.Empty));
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0001F718 File Offset: 0x0001D918
		private bool IsChildOfSyncRoot(IPropertyBag propertyBag)
		{
			return ArrayComparer<byte>.Comparer.Equals(this.syncRootFolderLongTermId, propertyBag.GetAnnotatedProperty(PropertyTag.ExternalParentFid).PropertyValue.GetValue<byte[]>());
		}

		// Token: 0x04000180 RID: 384
		private static readonly PropertyTag[] MustExcludeProperties = new PropertyTag[]
		{
			PropertyTag.FreeBusyNTSD
		};

		// Token: 0x04000181 RID: 385
		private static readonly PropertyTag[] RequiredInputProperties = Util.MergeArrays<PropertyTag>(new ICollection<PropertyTag>[]
		{
			IcsHierarchySynchronizer.RequiredFolderChangeProperties,
			new PropertyTag[]
			{
				PropertyTag.Fid,
				PropertyTag.ParentFid
			}
		});

		// Token: 0x04000182 RID: 386
		private readonly HierarchyManifestProvider manifestProvider;

		// Token: 0x04000183 RID: 387
		private readonly byte[] syncRootFolderLongTermId;

		// Token: 0x04000184 RID: 388
		private readonly HashSet<PropertyTag> excludePropertyTags;
	}
}
