using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.Mapi;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200008C RID: 140
	public sealed class MapiViewFolder : MapiViewTableBase
	{
		// Token: 0x060004E0 RID: 1248 RVA: 0x00021914 File Offset: 0x0001FB14
		public MapiViewFolder() : base(MapiObjectType.FolderView)
		{
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x0002191E File Offset: 0x0001FB1E
		internal ExchangeId FolderId
		{
			get
			{
				base.ThrowIfNotValid(null);
				return this.folderId;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x0002192D File Offset: 0x0001FB2D
		internal FolderViewTable FolderViewTable
		{
			get
			{
				base.ThrowIfNotValid(null);
				return base.StoreViewTable as FolderViewTable;
			}
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00021941 File Offset: 0x0001FB41
		public void Configure(MapiContext context, MapiLogon mapiLogon, MapiFolder mapiFolder, FolderViewTable.ConfigureFlags flags)
		{
			this.Configure(context, mapiLogon, mapiFolder.Fid, flags, uint.MaxValue);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00021954 File Offset: 0x0001FB54
		public void Configure(MapiContext context, MapiLogon mapiLogon, MapiFolder mapiFolder, FolderViewTable.ConfigureFlags flags, uint hsot)
		{
			this.Configure(context, mapiLogon, mapiFolder.Fid, flags, hsot);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00021968 File Offset: 0x0001FB68
		public void Configure(MapiContext context, MapiLogon mapiLogon, ExchangeId folderId, FolderViewTable.ConfigureFlags flags, uint hsot)
		{
			if (base.IsDisposed)
			{
				ExTraceGlobals.GeneralTracer.TraceError(0L, "Configure called on a Dispose'd MapiViewFolder!  Throwing ExExceptionInvalidObject!");
				throw new ExExceptionInvalidObject((LID)37880U, "Configure cannot be invoked after Dispose.");
			}
			if (base.IsValid)
			{
				ExTraceGlobals.GeneralTracer.TraceError(0L, "Configure called on already configured MapiViewFolder!  Throwing ExExceptionInvalidObject!");
				throw new ExExceptionInvalidObject((LID)54264U, "Object has already been Configure'd");
			}
			this.folderId = folderId;
			FolderViewTable folderViewTable = null;
			NotificationSubscription subscription = null;
			this.suppressNotifications = ((flags & (FolderViewTable.ConfigureFlags.Recursive | FolderViewTable.ConfigureFlags.SuppressNotifications)) == (FolderViewTable.ConfigureFlags.Recursive | FolderViewTable.ConfigureFlags.SuppressNotifications));
			if ((flags & FolderViewTable.ConfigureFlags.EmptyTable) == FolderViewTable.ConfigureFlags.None)
			{
				Func<Context, IFolderInformation, bool> isVisiblePredicate;
				FolderInformationType folderInformationType;
				if (context.HasMailboxFullRights)
				{
					isVisiblePredicate = new Func<Context, IFolderInformation, bool>(MapiViewFolder.IsInternalAccessFolderVisible);
					folderInformationType = FolderInformationType.Basic;
				}
				else
				{
					isVisiblePredicate = new Func<Context, IFolderInformation, bool>(MapiViewFolder.IsFolderVisible);
					folderInformationType = FolderInformationType.Extended;
				}
				folderViewTable = new FolderViewTable(context, mapiLogon.StoreMailbox, this.folderId, flags, folderInformationType, isVisiblePredicate);
				if ((flags & FolderViewTable.ConfigureFlags.Recursive) == FolderViewTable.ConfigureFlags.None)
				{
					SortOrder sortOrder = (SortOrder)new SortOrderBuilder(1)
					{
						{
							base.MapColumn(PropTag.Folder.DisplayName, mapiLogon.MapiMailbox.Database, MapiObjectType.Folder),
							true
						}
					};
					folderViewTable.SortTable(sortOrder);
				}
				if (hsot != 4294967295U && (flags & FolderViewTable.ConfigureFlags.NoNotifications) == FolderViewTable.ConfigureFlags.None)
				{
					if ((flags & FolderViewTable.ConfigureFlags.Recursive) == FolderViewTable.ConfigureFlags.None)
					{
						subscription = new FolderChildrenSubscription(SubscriptionKind.PostCommit, mapiLogon.Session.NotificationContext, mapiLogon.MapiMailbox.Database, mapiLogon.MapiMailbox.MailboxNumber, EventType.ObjectCreated | EventType.ObjectDeleted | EventType.ObjectModified | EventType.ObjectMoved | EventType.ObjectCopied, new NotificationCallback(this.OnNotification), this.folderId);
					}
					else if (this.folderId == mapiLogon.FidC.FidRoot)
					{
						subscription = new MailboxFoldersSubscription(SubscriptionKind.PostCommit, mapiLogon.Session.NotificationContext, mapiLogon.MapiMailbox.Database, mapiLogon.MapiMailbox.MailboxNumber, EventType.ObjectCreated | EventType.ObjectDeleted | EventType.ObjectModified | EventType.ObjectMoved | EventType.ObjectCopied, new NotificationCallback(this.OnNotification));
					}
				}
			}
			base.Configure(context, mapiLogon, MapiViewFolder.notificationColumns, folderViewTable, hsot, subscription);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00021B31 File Offset: 0x0001FD31
		protected override bool CanSortOnProperty(StorePropTag propTag)
		{
			return true;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00021B58 File Offset: 0x0001FD58
		protected override IList<StorePropTag> AdjustColumnsToQuery(IList<StorePropTag> columns)
		{
			if (columns.All((StorePropTag property) => !MapiFolder.IsFolderSecurityRelatedProperty(property.PropTag) && property.PropTag != 1723138079U))
			{
				return columns;
			}
			List<StorePropTag> list = new List<StorePropTag>(columns.Count + 5);
			bool flag = false;
			foreach (StorePropTag item in columns)
			{
				if (MapiFolder.IsFolderSecurityRelatedProperty(item.PropTag))
				{
					flag = true;
				}
				else if (item.PropTag == 1723138079U)
				{
					list.Add(PropTag.Folder.Fid);
				}
				else
				{
					list.Add(item);
				}
			}
			if (flag)
			{
				list.Add(PropTag.Folder.NTSecurityDescriptor);
			}
			return list;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00021C14 File Offset: 0x0001FE14
		protected override object GetPropertyValue(MapiContext context, Reader reader, StorePropTag propertyTag, Column column)
		{
			if (MapiFolder.IsFolderSecurityRelatedProperty(propertyTag.PropTag))
			{
				byte[] array = (byte[])base.GetPropertyValue(context, reader, PropTag.Folder.NTSecurityDescriptor, null);
				if (array == null)
				{
					return null;
				}
				SecurityDescriptor folderSecurityDescriptor = PropertyValueHelpers.UnformatSdFromTransfer(array);
				return MapiFolder.CalculateSecurityRelatedProperty(context, propertyTag, new AccessCheckState(context, folderSecurityDescriptor));
			}
			else
			{
				uint propTag = propertyTag.PropTag;
				if (propTag == 1721237762U)
				{
					byte[] replicaBytes = (byte[])base.GetPropertyValue(context, reader, PropTag.Folder.ReplicaList, null);
					Guid[] replicaGuids = Folder.ConvertStorageReplicaListBlobToGuidArray(replicaBytes);
					return MapiFolder.ConvertGuidArrayToWireReplicaListBlob(replicaGuids);
				}
				if (propTag == 1723138079U)
				{
					ExchangeId exchangeId = ExchangeId.CreateFromInt64(context, base.Logon.StoreMailbox.ReplidGuidMap, (long)base.GetPropertyValue(context, reader, PropTag.Folder.Fid, null));
					return Folder.GetFolderPathName(context, base.Logon.StoreMailbox, exchangeId, '\\');
				}
				return base.GetPropertyValue(context, reader, propertyTag, column);
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00021CE8 File Offset: 0x0001FEE8
		internal void OnNotification(NotificationPublishPhase phase, Context transactionContext, NotificationEvent nev)
		{
			MapiContext mapiContext = transactionContext as MapiContext;
			using (mapiContext.SetMapiLogonForNotificationContext(base.Logon))
			{
				if (ExTraceGlobals.NotificationTracer.IsTraceEnabled(TraceType.FunctionTrace))
				{
					ExTraceGlobals.NotificationTracer.TraceFunction<uint>(0L, "ENTER MapiViewFolder.OnNotification: hsot:[{0}]", base.Hsot);
				}
				try
				{
					if (base.ConfigurationError.HasConfigurationError)
					{
						base.TraceNotificationIgnored(nev, "not configured");
					}
					else
					{
						ObjectNotificationEvent objectNotificationEvent = nev as ObjectNotificationEvent;
						if (objectNotificationEvent == null || !objectNotificationEvent.IsFolderEvent || objectNotificationEvent.Fid == this.folderId)
						{
							base.TraceNotificationIgnored(nev, "out of scope");
						}
						else if (objectNotificationEvent.ExtendedEventFlags != null && (objectNotificationEvent.ExtendedEventFlags.Value & ExtendedEventFlags.InternalAccessFolder) != ExtendedEventFlags.None)
						{
							base.TraceNotificationIgnored(nev, "InternalAccess");
						}
						else if (base.Logon.IsThereTableChangedNotification(base.Hsot))
						{
							Statistics.MiscelaneousNotifications.SkippedFolderTableNotifications.Bump();
							ExTraceGlobals.NotificationTracer.TraceDebug(0L, "Notification dropped: duplicate notification.");
						}
						else
						{
							TableEventType tableEventType = TableEventType.Error;
							ExchangeId fid = ExchangeId.Null;
							EventType eventType = objectNotificationEvent.EventType;
							if (eventType <= EventType.ObjectDeleted)
							{
								if (eventType != EventType.ObjectCreated)
								{
									if (eventType != EventType.ObjectDeleted)
									{
										goto IL_276;
									}
									if (this.FolderViewTable.Recursive || objectNotificationEvent.ParentFid == this.folderId)
									{
										tableEventType = TableEventType.RowDeleted;
										fid = objectNotificationEvent.Fid;
										goto IL_276;
									}
									goto IL_276;
								}
							}
							else if (eventType != EventType.ObjectModified)
							{
								if (eventType != EventType.ObjectMoved)
								{
									if (eventType != EventType.ObjectCopied)
									{
										goto IL_276;
									}
								}
								else
								{
									ObjectMovedCopiedNotificationEvent objectMovedCopiedNotificationEvent = objectNotificationEvent as ObjectMovedCopiedNotificationEvent;
									if (objectMovedCopiedNotificationEvent == null)
									{
										return;
									}
									if (this.FolderViewTable.Recursive)
									{
										tableEventType = TableEventType.RowModified;
										fid = objectMovedCopiedNotificationEvent.Fid;
										goto IL_276;
									}
									if (objectMovedCopiedNotificationEvent.OldParentFid == this.folderId)
									{
										tableEventType = TableEventType.RowDeleted;
										fid = objectMovedCopiedNotificationEvent.OldFid;
										goto IL_276;
									}
									if (objectMovedCopiedNotificationEvent.ParentFid == this.folderId)
									{
										tableEventType = TableEventType.RowAdded;
										fid = objectMovedCopiedNotificationEvent.Fid;
										goto IL_276;
									}
									goto IL_276;
								}
							}
							else
							{
								if (this.suppressNotifications && phase == NotificationPublishPhase.PostCommit && !transactionContext.ForceNotificationPublish)
								{
									base.TraceNotificationIgnored(nev, "ignoring events from the same session");
									return;
								}
								if ((this.FolderViewTable.Recursive && objectNotificationEvent.Fid != this.folderId) || objectNotificationEvent.ParentFid == this.folderId)
								{
									tableEventType = TableEventType.RowModified;
									fid = objectNotificationEvent.Fid;
									goto IL_276;
								}
								goto IL_276;
							}
							if (this.FolderViewTable.Recursive || objectNotificationEvent.ParentFid == this.folderId)
							{
								tableEventType = TableEventType.RowAdded;
								fid = objectNotificationEvent.Fid;
							}
							IL_276:
							if (tableEventType != TableEventType.Error)
							{
								ExchangeId previousFid = ExchangeId.Zero;
								Properties empty = Properties.Empty;
								this.FolderViewTable.ForceReload(transactionContext, true);
								if ((TableEventType.RowAdded == tableEventType || TableEventType.RowModified == tableEventType) && this.ViewColumns != null)
								{
									FolderTable folderTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.FolderTable(base.Logon.StoreMailbox.Database);
									SearchCriteria findRowCriteria = Factory.CreateSearchCriteriaCompare(folderTable.FolderId, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(fid.To26ByteArray(), folderTable.FolderId));
									object[] array = null;
									if (!this.GetTableNotificationInfo(transactionContext, findRowCriteria, out empty, out array))
									{
										ExTraceGlobals.NotificationTracer.TraceDebug(0L, "Notification dropped: row deleted.");
										return;
									}
									if (array != null)
									{
										previousFid = ExchangeId.CreateFromInt64(transactionContext, base.Logon.StoreMailbox.ReplidGuidMap, (long)array[0]);
									}
								}
								TableModifiedNotificationEvent nev2 = MapiViewTableBase.CreateTableModifiedEvent(base.Logon.MapiMailbox.Database, base.Logon.MapiMailbox.StoreMailbox.MailboxNumber, transactionContext.ClientType, EventFlags.None, tableEventType, fid, ExchangeId.Null, 0, previousFid, ExchangeId.Null, 0, empty);
								base.Logon.AddPendingNotification(nev2, this, base.Hsot);
							}
							else
							{
								base.TraceNotificationIgnored(nev, "Error event type");
							}
						}
					}
				}
				finally
				{
					if (ExTraceGlobals.NotificationTracer.IsTraceEnabled(TraceType.FunctionTrace))
					{
						ExTraceGlobals.NotificationTracer.TraceFunction<uint>(0L, "EXIT MapiViewFolder.OnNotification: hsot:[{0}]", base.Hsot);
					}
				}
			}
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x000220FC File Offset: 0x000202FC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiViewFolder>(this);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00022104 File Offset: 0x00020304
		protected override void InternalDispose(bool calledFromDispose)
		{
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00022110 File Offset: 0x00020310
		private static bool IsInternalAccessFolderVisible(Context context, IFolderInformation folderInformation)
		{
			MapiContext mapiContext = context as MapiContext;
			if (folderInformation.IsInternalAccess && !mapiContext.HasInternalAccessRights)
			{
				DiagnosticContext.TraceDwordAndString((LID)53068U, 0U, folderInformation.Fid.ToString());
				return false;
			}
			return true;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0002215C File Offset: 0x0002035C
		private static bool IsFolderVisible(Context context, IFolderInformation folderInformation)
		{
			MapiContext context2 = context as MapiContext;
			if (folderInformation.ParentFid.IsZero)
			{
				return true;
			}
			if (!MapiViewFolder.IsInternalAccessFolderVisible(context, folderInformation))
			{
				DiagnosticContext.TraceDwordAndString((LID)38604U, 0U, folderInformation.Fid.ToString());
				return false;
			}
			AccessCheckState accessCheckState = new AccessCheckState(context2, folderInformation.SecurityDescriptor);
			bool flag = MapiFolder.HasFolderAccessRights(context2, folderInformation.Fid, folderInformation.IsSearchFolder, accessCheckState, FolderSecurity.ExchangeSecurityDescriptorFolderRights.ViewItem, true, AccessCheckOperation.FolderViewHierarchy, (LID)41831U);
			if (!flag)
			{
				DiagnosticContext.TraceDwordAndString((LID)33671U, 0U, folderInformation.DisplayName ?? string.Empty);
			}
			return flag;
		}

		// Token: 0x040002DC RID: 732
		private static StorePropTag[] notificationColumns = new StorePropTag[]
		{
			PropTag.Folder.Fid
		};

		// Token: 0x040002DD RID: 733
		private ExchangeId folderId;

		// Token: 0x040002DE RID: 734
		private bool suppressNotifications;
	}
}
