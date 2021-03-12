using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Exchange.RpcClientAccess.Handler;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x0200005C RID: 92
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ContentsSynchronizer : SynchronizerBase, IContentsSynchronizer, IDisposable
	{
		// Token: 0x060003DA RID: 986 RVA: 0x0001CB24 File Offset: 0x0001AD24
		internal ContentsSynchronizer(ReferenceCount<CoreFolder> referenceCoreFolder, SyncFlag syncFlags, Restriction restriction, SyncExtraFlag extraFlags, IcsState icsState, Encoding string8Encoding, bool wantUnicode, params PropertyTag[] referencePropertyTags) : base(referenceCoreFolder, syncFlags, extraFlags, icsState)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.string8Encoding = string8Encoding;
				this.wantUnicode = wantUnicode;
				ManifestConfigFlags manifestFlags = ContentsSynchronizer.GetManifestFlags(syncFlags, extraFlags);
				QueryFilter filter = (restriction != null) ? new FilterRestrictionTranslator(referenceCoreFolder.ReferencedObject.Session).Translate(restriction) : null;
				PropertyDefinition[] mustRequestPropertyDefinitions = ContentsSynchronizer.MustRequestPropertyDefinitions;
				IPropertyBag propertyBag = new MemoryPropertyBag(this.SessionAdaptor);
				icsState.Checkpoint(propertyBag);
				IcsStateStream icsStateStream = new IcsStateStream(propertyBag);
				CoreFolder referencedObject = this.SyncRootFolder.ReferencedObject;
				this.manifestProvider = referencedObject.GetContentManifest(manifestFlags, filter, icsStateStream.ToXsoState(), mustRequestPropertyDefinitions);
				disposeGuard.Success();
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0001CBEC File Offset: 0x0001ADEC
		public ProgressInformation ProgressInformation
		{
			get
			{
				return new ProgressInformation(0, 10, 10, 1048576UL, 1048576UL);
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0001CC04 File Offset: 0x0001AE04
		public static void CheckFlags(SyncFlag syncFlag, SyncExtraFlag extraFlags)
		{
			ContentsSynchronizer.GetManifestFlags(syncFlag, extraFlags);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0001CD50 File Offset: 0x0001AF50
		public IEnumerator<IMessageChange> GetChanges()
		{
			base.CheckDisposed();
			ManifestItemChange change;
			while (this.manifestProvider.TryGetNextChange(out change))
			{
				IPropertyBag messageHeaderPropertyBag = new MemoryPropertyBag(this.SessionAdaptor);
				SynchronizerBase.SetPropertyValuesFromServer(messageHeaderPropertyBag, this.SyncRootFolder.ReferencedObject.Session, change.PropertyValues);
				StoreObjectId itemId = StoreObjectId.FromProviderSpecificId(messageHeaderPropertyBag.GetAnnotatedProperty(PropertyTag.EntryId).PropertyValue.GetValue<byte[]>());
				this.CheckAndFixMessageChanges(messageHeaderPropertyBag);
				IMessageChange messageChange = null;
				if (this.TryGetMessageChange(itemId, messageHeaderPropertyBag, out messageChange))
				{
					yield return messageChange;
				}
			}
			yield break;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0001CD6C File Offset: 0x0001AF6C
		public IPropertyBag GetDeletions()
		{
			base.CheckDisposed();
			IPropertyBag propertyBag = new MemoryPropertyBag(this.SessionAdaptor);
			ManifestItemDeletion manifestItemDeletion;
			while (this.manifestProvider.TryGetDeletion(out manifestItemDeletion))
			{
				PropertyTag propertyTag;
				if (manifestItemDeletion.IsExpired)
				{
					propertyTag = PropertyTag.IdsetExpired;
				}
				else if (manifestItemDeletion.IsSoftDeleted)
				{
					propertyTag = PropertyTag.IdsetSoftDeleted;
				}
				else
				{
					propertyTag = PropertyTag.IdsetDeleted;
				}
				propertyBag.SetProperty(new PropertyValue(propertyTag, manifestItemDeletion.IdsetDeleted));
			}
			return propertyBag;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0001CDD8 File Offset: 0x0001AFD8
		public IPropertyBag GetReadUnreadStateChanges()
		{
			base.CheckDisposed();
			IPropertyBag propertyBag = new MemoryPropertyBag(this.SessionAdaptor);
			ManifestItemReadUnread manifestItemReadUnread;
			while (this.manifestProvider.TryGetReadUnread(out manifestItemReadUnread))
			{
				PropertyTag propertyTag = manifestItemReadUnread.IsRead ? PropertyTag.IdsetRead : PropertyTag.IdsetUnread;
				propertyBag.SetProperty(new PropertyValue(propertyTag, manifestItemReadUnread.IdsetReadUnread));
			}
			return propertyBag;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0001CE30 File Offset: 0x0001B030
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

		// Token: 0x060003E1 RID: 993 RVA: 0x0001CE8F File Offset: 0x0001B08F
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.manifestProvider);
			base.InternalDispose();
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0001CEA2 File Offset: 0x0001B0A2
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ContentsSynchronizer>(this);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0001CEAC File Offset: 0x0001B0AC
		private static ManifestConfigFlags GetManifestFlags(SyncFlag syncFlag, SyncExtraFlag extraFlags)
		{
			ManifestConfigFlags manifestConfigFlags = ManifestConfigFlags.None;
			if ((ushort)(syncFlag & (SyncFlag.NoConflicts | SyncFlag.Conversations | SyncFlag.MessageSelective)) != 0)
			{
				throw new RopExecutionException(string.Format("Unsupported SyncFlag present: {0}", syncFlag), (ErrorCode)2147746050U);
			}
			ushort num = (ushort)(syncFlag & SyncFlag.CatchUp);
			if ((ushort)(syncFlag & (SyncFlag.Associated | SyncFlag.Normal)) == 0)
			{
				throw new RopExecutionException(string.Format("One or both Normal or Associated should be specified: {0}", syncFlag), (ErrorCode)2147942487U);
			}
			SynchronizerBase.TranslateFlag(SyncFlag.NoDeletions, ManifestConfigFlags.NoDeletions, syncFlag, ref manifestConfigFlags);
			SynchronizerBase.TranslateFlag(SyncFlag.NoSoftDeletions, ManifestConfigFlags.NoSoftDeletions, syncFlag, ref manifestConfigFlags);
			SynchronizerBase.TranslateFlag(SyncFlag.ReadState, ManifestConfigFlags.ReadState, syncFlag, ref manifestConfigFlags);
			SynchronizerBase.TranslateFlag(SyncFlag.Associated, ManifestConfigFlags.Associated, syncFlag, ref manifestConfigFlags);
			SynchronizerBase.TranslateFlag(SyncFlag.Normal, ManifestConfigFlags.Normal, syncFlag, ref manifestConfigFlags);
			SynchronizerBase.TranslateFlag(SyncFlag.CatchUp, ManifestConfigFlags.Catchup, syncFlag, ref manifestConfigFlags);
			if ((extraFlags & (SyncExtraFlag.ManifestMode | SyncExtraFlag.CatchUpFull)) != SyncExtraFlag.None)
			{
				throw new RopExecutionException(string.Format("Unsupported SyncExtraFlag present: {0}", extraFlags), (ErrorCode)2147746050U);
			}
			if ((extraFlags & SyncExtraFlag.OrderByDeliveryTime) != SyncExtraFlag.None)
			{
				manifestConfigFlags |= ManifestConfigFlags.OrderByDeliveryTime;
			}
			if ((ushort)(syncFlag & SyncFlag.OnlySpecifiedProps) != 0)
			{
				Feature.Stubbed(65991, "return only specified properties");
			}
			if ((ushort)(syncFlag & SyncFlag.Unicode) == 0)
			{
				throw Feature.NotImplemented(212753, "codepage conversion");
			}
			if ((ushort)(syncFlag & SyncFlag.LimitedIMessage) != 0)
			{
				throw Feature.NotImplemented(62197, "don't return RTF bodies");
			}
			return manifestConfigFlags;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0001CFC8 File Offset: 0x0001B1C8
		private void HonorOptionNoForeignKeys(IPropertyBag propertyBag)
		{
			if ((ushort)(this.SyncFlags & SyncFlag.NoForeignKeys) == 256)
			{
				propertyBag.SetProperty(new PropertyValue(PropertyTag.ExternalMid, base.ConvertIdToLongTermId(propertyBag, PropertyTag.Mid)));
			}
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0001CFFA File Offset: 0x0001B1FA
		private void CheckAndFixMessageChanges(IPropertyBag propertyBag)
		{
			SynchronizerBase.CheckRequiredProperties(propertyBag, ContentsSynchronizer.RequiredInputProperties);
			this.HonorOptionNoForeignKeys(propertyBag);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0001D010 File Offset: 0x0001B210
		private bool TryGetMessageChange(StoreObjectId itemId, IPropertyBag messageHeaderPropertyBag, out IMessageChange messageChange)
		{
			messageChange = null;
			bool result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				CoreItem referencedObject;
				try
				{
					referencedObject = disposeGuard.Add<CoreItem>(CoreItem.Bind(this.SyncRootFolder.ReferencedObject.Session, itemId, CoreObjectSchema.AllPropertiesOnStore));
				}
				catch (ObjectNotFoundException)
				{
					return false;
				}
				DownloadBodyOption downloadBodyOption = ((ushort)(this.SyncFlags & SyncFlag.BestBody) == 8192) ? DownloadBodyOption.BestBodyOnly : DownloadBodyOption.RtfOnly;
				ReferenceCount<CoreItem> referenceCount = new ReferenceCount<CoreItem>(referencedObject);
				try
				{
					MessageAdaptor message = disposeGuard.Add<MessageAdaptor>(new MessageAdaptor(referenceCount, new MessageAdaptor.Options
					{
						IsReadOnly = true,
						IsEmbedded = false,
						DownloadBodyOption = downloadBodyOption
					}, this.string8Encoding, this.wantUnicode, null));
					MessageChangeAdaptor messageChangeAdaptor = new MessageChangeAdaptor(messageHeaderPropertyBag, message);
					disposeGuard.Add<MessageChangeAdaptor>(messageChangeAdaptor);
					disposeGuard.Success();
					messageChange = messageChangeAdaptor;
					result = true;
				}
				finally
				{
					referenceCount.Release();
				}
			}
			return result;
		}

		// Token: 0x04000144 RID: 324
		private static readonly PropertyTag[] MustRequestProperties = new PropertyTag[]
		{
			PropertyTag.ChangeNumber,
			PropertyTag.MessageSize
		};

		// Token: 0x04000145 RID: 325
		private static readonly PropertyDefinition[] MustRequestPropertyDefinitions = new PropertyDefinition[]
		{
			PropertyTagPropertyDefinition.CreateCustom("ChangeNumber", PropertyTag.ChangeNumber),
			CoreItemSchema.Size
		};

		// Token: 0x04000146 RID: 326
		private static readonly PropertyTag[] RequiredInputProperties = Util.MergeArrays<PropertyTag>(new ICollection<PropertyTag>[]
		{
			FastTransferMessageChange.AllMessageHeaderProperties,
			ContentsSynchronizer.MustRequestProperties,
			new PropertyTag[]
			{
				PropertyTag.Mid,
				PropertyTag.EntryId
			}
		});

		// Token: 0x04000147 RID: 327
		private readonly ContentManifestProvider manifestProvider;

		// Token: 0x04000148 RID: 328
		private readonly Encoding string8Encoding;

		// Token: 0x04000149 RID: 329
		private readonly bool wantUnicode;
	}
}
