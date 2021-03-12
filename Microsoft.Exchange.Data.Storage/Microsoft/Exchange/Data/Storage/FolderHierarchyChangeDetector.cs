using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E6B RID: 3691
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class FolderHierarchyChangeDetector
	{
		// Token: 0x06007FF8 RID: 32760 RVA: 0x00230378 File Offset: 0x0022E578
		public static FolderHierarchyChangeDetector.MailboxChangesManifest RunICSManifestSync(bool catchup, FolderHierarchyChangeDetector.SyncHierarchyManifestState hierState, MailboxSession mailboxSession, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			syncLogger.TraceDebug<SmtpAddress, bool>(ExTraceGlobals.SyncProcessTracer, 0L, "[FolderHierarchyChangeDetector.RunICSManifestSync] Checking for folder hierarhcy changes for Mailbox: {0}.  Catchup? {1}", mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress, catchup);
			MapiStore _ContainedMapiStore = mailboxSession.__ContainedMapiStore;
			FolderHierarchyChangeDetector.MailboxChangesManifest mailboxChangesManifest = new FolderHierarchyChangeDetector.MailboxChangesManifest();
			FolderHierarchyChangeDetector.ManifestHierarchyCallback iMapiManifestCallback = new FolderHierarchyChangeDetector.ManifestHierarchyCallback(catchup, mailboxChangesManifest);
			try
			{
				using (MapiFolder ipmSubtreeFolder = _ContainedMapiStore.GetIpmSubtreeFolder())
				{
					SyncConfigFlags syncConfigFlags = SyncConfigFlags.ManifestHierReturnDeletedEntryIds;
					int serverVersion = mailboxSession.MailboxOwner.MailboxInfo.Location.ServerVersion;
					if ((serverVersion >= Server.E14MinVersion && serverVersion < Server.E15MinVersion) || (long)serverVersion >= FolderHierarchyChangeDetector.E15MinVersionSupportsOnlySpecifiedPropsForHierarchy)
					{
						syncConfigFlags |= SyncConfigFlags.OnlySpecifiedProps;
					}
					using (MapiHierarchyManifestEx mapiHierarchyManifestEx = ipmSubtreeFolder.CreateExportHierarchyManifestEx(syncConfigFlags, hierState.IdsetGiven, hierState.CnsetSeen, iMapiManifestCallback, FolderHierarchyChangeDetector.PropsToFetch, null))
					{
						while (mapiHierarchyManifestEx.Synchronize() != ManifestStatus.Done)
						{
						}
						byte[] idSetGiven;
						byte[] cnetSeen;
						mapiHierarchyManifestEx.GetState(out idSetGiven, out cnetSeen);
						syncLogger.TraceDebug<SmtpAddress, int, int>(ExTraceGlobals.SyncProcessTracer, 0L, "[FolderHierarchyChangeDetector.RunICSManifestSync] Updating ICS state for mailbox: '{0}'.  Change Count: {1}, Delete Count: {2}", mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress, mailboxChangesManifest.ChangedFolders.Count, mailboxChangesManifest.DeletedFolders.Count);
						hierState.Update(idSetGiven, cnetSeen);
					}
				}
			}
			catch (MapiPermanentException arg)
			{
				syncLogger.TraceDebug<SmtpAddress, MapiPermanentException>(ExTraceGlobals.SyncProcessTracer, 0L, "[FolderHierarchyChangeDetector.RunICSManifestSync] Caught MapiPermanentException when determining folder ICS changes for mailbox: {0}.  Exception: {1}", mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress, arg);
				return null;
			}
			catch (MapiRetryableException arg2)
			{
				syncLogger.TraceDebug<SmtpAddress, MapiRetryableException>(ExTraceGlobals.SyncProcessTracer, 0L, "[FolderHierarchyChangeDetector.RunICSManifestSync] Caught MapiRetryableException when determining folder ICS changes for mailbox: {0}.  Exception: {1}", mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress, arg2);
				return null;
			}
			return mailboxChangesManifest;
		}

		// Token: 0x04005672 RID: 22130
		private static readonly long E15MinVersionSupportsOnlySpecifiedPropsForHierarchy = (long)new ServerVersion(15, 0, 922, 0).ToInt();

		// Token: 0x04005673 RID: 22131
		private static readonly PropTag[] PropsToFetch = new PropTag[]
		{
			PropTag.EntryId,
			PropTag.DisplayName
		};

		// Token: 0x02000E6C RID: 3692
		private class ManifestHierarchyCallback : IMapiHierarchyManifestCallback
		{
			// Token: 0x06007FFA RID: 32762 RVA: 0x00230577 File Offset: 0x0022E777
			public ManifestHierarchyCallback(bool catchup, FolderHierarchyChangeDetector.MailboxChangesManifest changes)
			{
				this.catchup = catchup;
				this.changes = changes;
				this.changes.ChangedFolders = new Dictionary<StoreObjectId, string>();
				this.changes.DeletedFolders = new List<StoreObjectId>();
			}

			// Token: 0x06007FFB RID: 32763 RVA: 0x002305B0 File Offset: 0x0022E7B0
			ManifestCallbackStatus IMapiHierarchyManifestCallback.Change(PropValue[] props)
			{
				if (this.catchup)
				{
					return ManifestCallbackStatus.Continue;
				}
				StoreObjectId key = null;
				string value = null;
				foreach (PropValue propValue in props)
				{
					if (propValue.PropTag == PropTag.EntryId)
					{
						byte[] bytes = propValue.GetBytes();
						key = StoreObjectId.FromProviderSpecificId(bytes);
					}
					else if (propValue.PropTag == PropTag.DisplayName)
					{
						value = propValue.GetString();
					}
				}
				if (!this.changes.ChangedFolders.ContainsKey(key))
				{
					this.changes.ChangedFolders[key] = value;
				}
				return ManifestCallbackStatus.Continue;
			}

			// Token: 0x06007FFC RID: 32764 RVA: 0x0023064C File Offset: 0x0022E84C
			ManifestCallbackStatus IMapiHierarchyManifestCallback.Delete(byte[] entryId)
			{
				if (this.catchup)
				{
					return ManifestCallbackStatus.Continue;
				}
				StoreObjectId item = StoreObjectId.FromProviderSpecificId(entryId);
				if (!this.changes.DeletedFolders.Contains(item))
				{
					this.changes.DeletedFolders.Add(item);
				}
				return ManifestCallbackStatus.Continue;
			}

			// Token: 0x04005674 RID: 22132
			private FolderHierarchyChangeDetector.MailboxChangesManifest changes;

			// Token: 0x04005675 RID: 22133
			private readonly bool catchup;
		}

		// Token: 0x02000E6D RID: 3693
		internal class MailboxChangesManifest
		{
			// Token: 0x06007FFD RID: 32765 RVA: 0x0023068F File Offset: 0x0022E88F
			public MailboxChangesManifest()
			{
				this.changedFolders = null;
				this.deletedFolders = null;
			}

			// Token: 0x17002215 RID: 8725
			// (get) Token: 0x06007FFE RID: 32766 RVA: 0x002306A5 File Offset: 0x0022E8A5
			// (set) Token: 0x06007FFF RID: 32767 RVA: 0x002306AD File Offset: 0x0022E8AD
			public Dictionary<StoreObjectId, string> ChangedFolders
			{
				get
				{
					return this.changedFolders;
				}
				set
				{
					this.changedFolders = value;
				}
			}

			// Token: 0x17002216 RID: 8726
			// (get) Token: 0x06008000 RID: 32768 RVA: 0x002306B6 File Offset: 0x0022E8B6
			// (set) Token: 0x06008001 RID: 32769 RVA: 0x002306BE File Offset: 0x0022E8BE
			public List<StoreObjectId> DeletedFolders
			{
				get
				{
					return this.deletedFolders;
				}
				set
				{
					this.deletedFolders = value;
				}
			}

			// Token: 0x17002217 RID: 8727
			// (get) Token: 0x06008002 RID: 32770 RVA: 0x002306C7 File Offset: 0x0022E8C7
			public bool HasChanges
			{
				get
				{
					return (this.changedFolders != null && this.changedFolders.Count > 0) || (this.deletedFolders != null && this.deletedFolders.Count > 0);
				}
			}

			// Token: 0x04005676 RID: 22134
			private Dictionary<StoreObjectId, string> changedFolders;

			// Token: 0x04005677 RID: 22135
			private List<StoreObjectId> deletedFolders;
		}

		// Token: 0x02000E6E RID: 3694
		public sealed class SyncHierarchyManifestState
		{
			// Token: 0x06008003 RID: 32771 RVA: 0x002306F9 File Offset: 0x0022E8F9
			public SyncHierarchyManifestState()
			{
				this.Update(Array<byte>.Empty, Array<byte>.Empty);
			}

			// Token: 0x17002218 RID: 8728
			// (get) Token: 0x06008004 RID: 32772 RVA: 0x0023071C File Offset: 0x0022E91C
			// (set) Token: 0x06008005 RID: 32773 RVA: 0x00230724 File Offset: 0x0022E924
			public byte[] IdsetGiven { get; private set; }

			// Token: 0x17002219 RID: 8729
			// (get) Token: 0x06008006 RID: 32774 RVA: 0x0023072D File Offset: 0x0022E92D
			// (set) Token: 0x06008007 RID: 32775 RVA: 0x00230735 File Offset: 0x0022E935
			public byte[] CnsetSeen { get; private set; }

			// Token: 0x06008008 RID: 32776 RVA: 0x00230740 File Offset: 0x0022E940
			public void Update(byte[] idSetGiven, byte[] cnetSeen)
			{
				lock (this.instanceLock)
				{
					this.IdsetGiven = idSetGiven;
					this.CnsetSeen = cnetSeen;
				}
			}

			// Token: 0x06008009 RID: 32777 RVA: 0x00230788 File Offset: 0x0022E988
			public FolderHierarchyChangeDetector.SyncHierarchyManifestState Clone()
			{
				FolderHierarchyChangeDetector.SyncHierarchyManifestState result;
				lock (this.instanceLock)
				{
					result = new FolderHierarchyChangeDetector.SyncHierarchyManifestState
					{
						IdsetGiven = (byte[])this.IdsetGiven.Clone(),
						CnsetSeen = (byte[])this.CnsetSeen.Clone()
					};
				}
				return result;
			}

			// Token: 0x04005678 RID: 22136
			private object instanceLock = new object();
		}
	}
}
