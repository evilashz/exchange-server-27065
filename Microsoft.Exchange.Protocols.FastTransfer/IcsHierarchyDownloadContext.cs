using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.FastTransfer;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.FastTransfer
{
	// Token: 0x02000009 RID: 9
	internal class IcsHierarchyDownloadContext : IcsDownloadContext
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002E74 File Offset: 0x00001074
		public ErrorCode Configure(MapiLogon logon, IHierarchySynchronizationScope scope, FastTransferSendOption sendOptions, SyncFlag syncFlags, SyncExtraFlag extraFlags, StorePropTag[] propertyTags)
		{
			ErrorCode errorCode = base.Configure(logon, sendOptions);
			if (errorCode == ErrorCode.NoError)
			{
				this.scope = scope;
				this.syncFlags = syncFlags;
				this.extraFlags = extraFlags;
				if ((ushort)(syncFlags & SyncFlag.OnlySpecifiedProps) != 0)
				{
					if (propertyTags == null || propertyTags.Length == 0)
					{
						propertyTags = IcsHierarchyDownloadContext.defaultFolderProperties;
					}
					else
					{
						HashSet<StorePropTag> hashSet = new HashSet<StorePropTag>(propertyTags);
						hashSet.UnionWith(IcsHierarchyDownloadContext.defaultFolderProperties);
						propertyTags = new StorePropTag[hashSet.Count];
						hashSet.CopyTo(propertyTags);
					}
				}
				this.propertyTags = propertyTags;
				if (this.propertyTags != null)
				{
					int num = Array.FindIndex<StorePropTag>(this.propertyTags, (StorePropTag ptag) => ptag == PropTag.Folder.AclTableAndSecurityDescriptor);
					if (num != -1)
					{
						this.propertyTags[num] = PropTag.Folder.NTSecurityDescriptor;
					}
				}
			}
			return errorCode;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002F4C File Offset: 0x0000114C
		protected override IFastTransferProcessor<FastTransferDownloadContext> GetFastTransferProcessor(MapiContext operationContext)
		{
			IcsHierarchyDownloadContext.HierarchySynchronizer hierarchySynchronizer = new IcsHierarchyDownloadContext.HierarchySynchronizer(operationContext, this, this.scope, base.IcsState, this.syncFlags, this.extraFlags, this.propertyTags);
			IcsHierarchySynchronizer.Options options = IcsHierarchySynchronizer.Options.None;
			if ((this.extraFlags & SyncExtraFlag.Eid) != SyncExtraFlag.None)
			{
				options |= IcsHierarchySynchronizer.Options.IncludeFid;
			}
			if ((this.extraFlags & SyncExtraFlag.Cn) != SyncExtraFlag.None)
			{
				options |= IcsHierarchySynchronizer.Options.IncludeChangeNumber;
			}
			return new IcsHierarchySynchronizer(hierarchySynchronizer, options);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002FA4 File Offset: 0x000011A4
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<IcsHierarchyDownloadContext>(this);
		}

		// Token: 0x0400001D RID: 29
		private static StorePropTag[] defaultFolderProperties = new StorePropTag[]
		{
			PropTag.Folder.ParentSourceKey,
			PropTag.Folder.SourceKey,
			PropTag.Folder.LastModificationTime,
			PropTag.Folder.ChangeKey,
			PropTag.Folder.PredecessorChangeList,
			PropTag.Folder.DisplayName,
			PropTag.Folder.Fid,
			PropTag.Folder.ChangeNumber
		};

		// Token: 0x0400001E RID: 30
		private IHierarchySynchronizationScope scope;

		// Token: 0x0400001F RID: 31
		private SyncFlag syncFlags;

		// Token: 0x04000020 RID: 32
		private SyncExtraFlag extraFlags;

		// Token: 0x04000021 RID: 33
		private StorePropTag[] propertyTags;

		// Token: 0x0200000A RID: 10
		internal class HierarchySynchronizer : DisposableBase, IHierarchySynchronizer, IDisposable
		{
			// Token: 0x06000051 RID: 81 RVA: 0x00003058 File Offset: 0x00001258
			public HierarchySynchronizer(MapiContext operationContext, IcsHierarchyDownloadContext context, IHierarchySynchronizationScope scope, IcsState state, SyncFlag syncFlags, SyncExtraFlag extraFlags, StorePropTag[] propertyTags)
			{
				state.ReloadIfNecessary();
				if (ExTraceGlobals.IcsDownloadStateTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					StringBuilder stringBuilder = new StringBuilder(100);
					stringBuilder.Append("InitialState=[");
					stringBuilder.Append(state.ToString());
					stringBuilder.Append("]");
					ExTraceGlobals.IcsDownloadStateTracer.TraceDebug(0L, stringBuilder.ToString());
				}
				this.context = context;
				this.scope = scope;
				this.state = state;
				this.syncFlags = syncFlags;
				this.extraFlags = extraFlags;
				this.propertyTags = new HashSet<StorePropTag>(propertyTags);
				this.cnsetSeenServer = scope.GetServerCnsetSeen(operationContext);
				scope.GetChangedAndDeletedFolders(operationContext, this.syncFlags, state.CnsetSeen, state.IdsetGiven, out this.changedFolders, out this.idsetDeletes);
			}

			// Token: 0x06000052 RID: 82 RVA: 0x00003464 File Offset: 0x00001664
			public IEnumerator<IFolderChange> GetChanges()
			{
				if (this.changedFolders != null)
				{
					ushort lastCnReplid = 0;
					Guid lastCnGuid = default(Guid);
					IReplidGuidMap replidGuidMap = this.context.Logon.StoreMailbox.ReplidGuidMap;
					foreach (FolderChangeEntry folderHeader in this.changedFolders)
					{
						ExchangeId fid = ExchangeId.CreateFromInternalShortId(this.context.Logon.StoreMailbox.CurrentOperationContext, replidGuidMap, folderHeader.FolderId);
						bool skipFolder = false;
						if ((ushort)(this.syncFlags & SyncFlag.CatchUp) == 0)
						{
							using (MapiFolder folder = this.scope.OpenFolder(fid))
							{
								if (folder != null)
								{
									yield return new IcsHierarchyDownloadContext.FolderChange(this.context, this.scope, this.syncFlags, this.extraFlags, this.propertyTags, folder);
								}
								else
								{
									skipFolder = true;
								}
							}
						}
						if (!skipFolder)
						{
							ushort num;
							ulong counter;
							ExchangeIdHelpers.FromLong(folderHeader.Cn, out num, out counter);
							if (num != lastCnReplid)
							{
								lastCnGuid = this.scope.ReplidToGuid(new ReplId(num));
								lastCnReplid = num;
							}
							this.state.CnsetSeen.Insert(lastCnGuid, counter);
							this.state.IdsetGiven.Insert(fid);
						}
					}
				}
				this.state.CnsetSeen.Insert(this.cnsetSeenServer);
				yield break;
			}

			// Token: 0x06000053 RID: 83 RVA: 0x00003480 File Offset: 0x00001680
			public IPropertyBag GetDeletions()
			{
				MemoryPropertyBag memoryPropertyBag = new MemoryPropertyBag(this.context);
				if ((ushort)(this.syncFlags & SyncFlag.CatchUp) == 0 && (ushort)(this.syncFlags & SyncFlag.NoDeletions) == 0 && this.idsetDeletes != null && !this.idsetDeletes.IsEmpty)
				{
					byte[] value = this.idsetDeletes.Serialize(new Func<Guid, ReplId>(this.scope.GuidToReplid));
					memoryPropertyBag.SetProperty(new PropertyValue(PropertyTag.IdsetDeleted, value));
				}
				return memoryPropertyBag;
			}

			// Token: 0x06000054 RID: 84 RVA: 0x000034F8 File Offset: 0x000016F8
			public IIcsState GetFinalState()
			{
				if (this.idsetDeletes != null)
				{
					this.state.IdsetGiven.Remove(this.idsetDeletes);
				}
				if (ExTraceGlobals.IcsDownloadStateTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					StringBuilder stringBuilder = new StringBuilder(100);
					stringBuilder.Append("FinalState=[");
					stringBuilder.Append(this.state.ToString());
					stringBuilder.Append("]");
					ExTraceGlobals.IcsDownloadStateTracer.TraceDebug(0L, stringBuilder.ToString());
				}
				return this.state;
			}

			// Token: 0x06000055 RID: 85 RVA: 0x0000357B File Offset: 0x0000177B
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<IcsHierarchyDownloadContext.HierarchySynchronizer>(this);
			}

			// Token: 0x06000056 RID: 86 RVA: 0x00003583 File Offset: 0x00001783
			protected override void InternalDispose(bool isCalledFromDispose)
			{
			}

			// Token: 0x04000023 RID: 35
			private IcsHierarchyDownloadContext context;

			// Token: 0x04000024 RID: 36
			private IHierarchySynchronizationScope scope;

			// Token: 0x04000025 RID: 37
			private IcsState state;

			// Token: 0x04000026 RID: 38
			private SyncFlag syncFlags;

			// Token: 0x04000027 RID: 39
			private SyncExtraFlag extraFlags;

			// Token: 0x04000028 RID: 40
			private HashSet<StorePropTag> propertyTags;

			// Token: 0x04000029 RID: 41
			private IdSet cnsetSeenServer;

			// Token: 0x0400002A RID: 42
			private IdSet idsetDeletes;

			// Token: 0x0400002B RID: 43
			private IList<FolderChangeEntry> changedFolders;
		}

		// Token: 0x0200000D RID: 13
		internal class FolderChange : FastTransferPropertyBag, IFolderChange, IDisposable
		{
			// Token: 0x0600008E RID: 142 RVA: 0x00004937 File Offset: 0x00002B37
			public FolderChange(IcsDownloadContext context, IHierarchySynchronizationScope scope, SyncFlag syncFlags, SyncExtraFlag extraFlags, HashSet<StorePropTag> propList, MapiFolder folder) : base(context, folder, (ushort)(syncFlags & SyncFlag.OnlySpecifiedProps) == 0, propList)
			{
				this.scope = scope;
				this.syncFlags = syncFlags;
				this.extraFlags = extraFlags;
			}

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x0600008F RID: 143 RVA: 0x00004965 File Offset: 0x00002B65
			// (set) Token: 0x06000090 RID: 144 RVA: 0x00004972 File Offset: 0x00002B72
			private MapiFolder MapiFolder
			{
				get
				{
					return (MapiFolder)base.MapiPropBag;
				}
				set
				{
					base.MapiPropBag = value;
				}
			}

			// Token: 0x06000091 RID: 145 RVA: 0x0000497C File Offset: 0x00002B7C
			protected override List<Property> LoadAllPropertiesImp()
			{
				List<Property> list = base.LoadAllPropertiesImp();
				if (base.ForMoveUser)
				{
					FastTransferPropertyBag.AddNullPropertyIfNotPresent(list, PropTag.Folder.CnExport);
					FastTransferPropertyBag.AddNullPropertyIfNotPresent(list, PropTag.Folder.PclExport);
					FastTransferPropertyBag.AddNullPropertyIfNotPresent(list, PropTag.Folder.CnMvExport);
					FastTransferPropertyBag.AddNullPropertyIfNotPresent(list, PropTag.Folder.LastModificationTime);
					FastTransferPropertyBag.AddNullPropertyIfNotPresent(list, PropTag.Folder.MidsetDeletedExport);
					FastTransferPropertyBag.AddNullPropertyIfNotPresent(list, PropTag.Folder.ChangeKey);
					FastTransferPropertyBag.AddNullPropertyIfNotPresent(list, PropTag.Folder.PredecessorChangeList);
					FastTransferPropertyBag.AddNullPropertyIfNotPresent(list, PropTag.Folder.SourceKey);
					FastTransferPropertyBag.AddNullPropertyIfNotPresent(list, PropTag.Folder.LastConflict);
					FastTransferPropertyBag.AddNullPropertyIfNotPresent(list, PropTag.Folder.ArticleNumNext);
					FastTransferPropertyBag.AddNullPropertyIfNotPresent(list, PropTag.Folder.FolderAdminFlags);
					FastTransferPropertyBag.AddNullPropertyIfNotPresent(list, PropTag.Folder.ELCPolicyComment);
					FastTransferPropertyBag.AddNullPropertyIfNotPresent(list, PropTag.Folder.ELCPolicyId);
					FastTransferPropertyBag.AddNullPropertyIfNotPresent(list, PropTag.Folder.ELCFolderQuota);
				}
				FastTransferPropertyBag.ResetPropertyIfPresent(list, PropTag.Folder.SourceKey);
				FastTransferPropertyBag.ResetPropertyIfPresent(list, PropTag.Folder.ParentSourceKey);
				ValueHelper.SortAndRemoveDuplicates<Property>(list, PropertyComparerByTag.Comparer);
				return list;
			}

			// Token: 0x06000092 RID: 146 RVA: 0x00004A58 File Offset: 0x00002C58
			protected override Property GetPropertyImp(StorePropTag propTag)
			{
				if (propTag == PropTag.Folder.SourceKey)
				{
					if ((ushort)(this.syncFlags & SyncFlag.NoForeignKeys) != 0)
					{
						object value = this.MapiFolder.Fid.To22ByteArray();
						return new Property(PropTag.Folder.SourceKey, value);
					}
				}
				else if (propTag == PropTag.Folder.ParentSourceKey)
				{
					if (this.MapiFolder.GetParentFid(base.Context.CurrentOperationContext) == this.scope.GetRootFid())
					{
						object empty = Array<byte>.Empty;
						return new Property(PropTag.Folder.ParentSourceKey, empty);
					}
					if ((ushort)(this.syncFlags & SyncFlag.NoForeignKeys) != 0)
					{
						object value2 = this.MapiFolder.GetParentFid(base.Context.CurrentOperationContext).To22ByteArray();
						return new Property(PropTag.Folder.ParentSourceKey, value2);
					}
				}
				return base.GetPropertyImp(propTag);
			}

			// Token: 0x06000093 RID: 147 RVA: 0x00004B2D File Offset: 0x00002D2D
			protected override void SetPropertyImp(Property property)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000094 RID: 148 RVA: 0x00004B34 File Offset: 0x00002D34
			protected override void DeleteImp(StorePropTag propTag)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000095 RID: 149 RVA: 0x00004B3B File Offset: 0x00002D3B
			public override Stream GetPropertyStreamImp(StorePropTag propTag)
			{
				return base.GetPropertyStreamImp(propTag);
			}

			// Token: 0x06000096 RID: 150 RVA: 0x00004B44 File Offset: 0x00002D44
			public override Stream SetPropertyStreamImp(StorePropTag propTag, long dataSize)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000097 RID: 151 RVA: 0x00004B4C File Offset: 0x00002D4C
			protected override bool IncludeTag(StorePropTag propTag)
			{
				if (base.ForMoveUser && propTag.IsCategory(4))
				{
					ushort propId = propTag.PropId;
					if (propId != 26514)
					{
						switch (propId)
						{
						case 26532:
						case 26533:
						case 26534:
							break;
						default:
							return true;
						}
					}
					return false;
				}
				return IcsHierarchyDownloadContext.FolderChange.specialFolderProps.Contains(propTag) || (propTag == PropTag.Folder.Fid && (this.extraFlags & SyncExtraFlag.Eid) != SyncExtraFlag.None) || (propTag == PropTag.Folder.ChangeNumber && (this.extraFlags & SyncExtraFlag.Cn) != SyncExtraFlag.None) || (propTag == PropTag.Folder.ParentFid && ((this.extraFlags & SyncExtraFlag.ManifestMode) != SyncExtraFlag.None || (ushort)(this.syncFlags & SyncFlag.NoForeignKeys) != 0 || (this.extraFlags & SyncExtraFlag.Eid) != SyncExtraFlag.None)) || ((26112 > propTag.PropId || propTag.PropId > 26623) && !propTag.IsNamedProperty && (!(propTag == PropTag.Folder.FreeBusyNTSD) || !base.ExcludeProps) && base.IncludeTag(propTag));
			}

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x06000098 RID: 152 RVA: 0x00004C54 File Offset: 0x00002E54
			public IPropertyBag FolderPropertyBag
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06000099 RID: 153 RVA: 0x00004C57 File Offset: 0x00002E57
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<IcsHierarchyDownloadContext.FolderChange>(this);
			}

			// Token: 0x0600009A RID: 154 RVA: 0x00004C5F File Offset: 0x00002E5F
			protected override void InternalDispose(bool isCalledFromDispose)
			{
				if (isCalledFromDispose && this.MapiFolder != null)
				{
					this.MapiFolder.Dispose();
					this.MapiFolder = null;
				}
				base.InternalDispose(isCalledFromDispose);
			}

			// Token: 0x0400003B RID: 59
			private static HashSet<StorePropTag> specialFolderProps = new HashSet<StorePropTag>
			{
				PropTag.Folder.ParentSourceKey,
				PropTag.Folder.SourceKey,
				PropTag.Folder.LastModificationTime,
				PropTag.Folder.ChangeKey,
				PropTag.Folder.PredecessorChangeList,
				PropTag.Folder.DisplayName,
				PropTag.Folder.ELCFolderQuota,
				PropTag.Folder.ELCFolderSize,
				PropTag.Folder.FolderAdminFlags,
				PropTag.Folder.ELCPolicyComment,
				PropTag.Folder.ELCPolicyId
			};

			// Token: 0x0400003C RID: 60
			private IHierarchySynchronizationScope scope;

			// Token: 0x0400003D RID: 61
			private SyncFlag syncFlags;

			// Token: 0x0400003E RID: 62
			private SyncExtraFlag extraFlags;
		}
	}
}
