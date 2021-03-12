using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000263 RID: 611
	internal class FirstTimeFolderSync : FolderSync
	{
		// Token: 0x0600169B RID: 5787 RVA: 0x000885ED File Offset: 0x000867ED
		public FirstTimeFolderSync(ISyncProvider syncProvider, IFolderSyncState syncState, ConflictResolutionPolicy policy, bool deferStateModifications) : base(syncProvider, syncState, policy, deferStateModifications)
		{
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x00088606 File Offset: 0x00086806
		// (set) Token: 0x0600169D RID: 5789 RVA: 0x0008860E File Offset: 0x0008680E
		public string CollectionId { get; set; }

		// Token: 0x0600169E RID: 5790 RVA: 0x00088617 File Offset: 0x00086817
		public override void UndoServerOperations()
		{
			base.UndoServerOperations();
			if (this.backupCurFTSMaxWatermarkHasBeenSet)
			{
				if (this.backupCurFTSMaxWatermark != null)
				{
					this.CurFTSMaxWatermark = this.backupCurFTSMaxWatermark;
					return;
				}
				this.syncState.Remove(SyncStateProp.CurFTSMaxWatermark);
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x0600169F RID: 5791 RVA: 0x0008864C File Offset: 0x0008684C
		// (set) Token: 0x060016A0 RID: 5792 RVA: 0x000886B0 File Offset: 0x000868B0
		private ISyncWatermark CurFTSMaxWatermark
		{
			get
			{
				ISyncWatermark syncWatermark = this.syncState.Contains(SyncStateProp.CurFTSMaxWatermark) ? ((ISyncWatermark)this.syncState[SyncStateProp.CurFTSMaxWatermark]) : null;
				if (!this.backupCurFTSMaxWatermarkHasBeenSet && this.shouldBackUpState)
				{
					if (syncWatermark != null)
					{
						this.backupCurFTSMaxWatermark = (ISyncWatermark)syncWatermark.Clone();
					}
					this.backupCurFTSMaxWatermarkHasBeenSet = true;
				}
				return syncWatermark;
			}
			set
			{
				if (!this.backupCurFTSMaxWatermarkHasBeenSet && this.shouldBackUpState)
				{
					if (this.syncState.Contains(SyncStateProp.CurFTSMaxWatermark))
					{
						this.backupCurFTSMaxWatermark = (ISyncWatermark)this.syncState[SyncStateProp.CurFTSMaxWatermark];
					}
					this.backupCurFTSMaxWatermarkHasBeenSet = true;
				}
				this.syncState[SyncStateProp.CurFTSMaxWatermark] = value;
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x00088712 File Offset: 0x00086912
		// (set) Token: 0x060016A2 RID: 5794 RVA: 0x00088749 File Offset: 0x00086949
		private ISyncWatermark PrevFTSMaxWatermark
		{
			get
			{
				base.CommitModifyState(this.prevMaxFTSWatermarkModifiers);
				if (!this.syncState.Contains(SyncStateProp.PrevFTSMaxWatermark))
				{
					return null;
				}
				return (ISyncWatermark)this.syncState[SyncStateProp.PrevFTSMaxWatermark];
			}
			set
			{
				base.CommitModifyState(this.prevMaxFTSWatermarkModifiers);
				this.syncState[SyncStateProp.PrevFTSMaxWatermark] = value;
			}
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x00088768 File Offset: 0x00086968
		protected override void InitializeAllItemsInFilter(QueryBasedSyncFilter queryBasedSyncFilter)
		{
			FirstTimeSyncProvider firstTimeSyncProvider = this.syncProvider as FirstTimeSyncProvider;
			if (firstTimeSyncProvider != null)
			{
				ExTraceGlobals.SyncProcessTracer.Information((long)this.GetHashCode(), "FirstTimeFolderSync.InitializeAllItemsInFilter.  Using FTS Provider");
				queryBasedSyncFilter.EntriesInFilter.Clear();
				FirstTimeSyncWatermark minWatermark = firstTimeSyncProvider.GetNewFirstTimeSyncWatermark() as FirstTimeSyncWatermark;
				if (Command.CurrentCommand != null)
				{
					Command.CurrentCommand.ProtocolLogger.SetProviderSyncType(this.CollectionId, ProviderSyncType.FCS);
					Command.CurrentCommand.Context.SetDiagnosticValue(AirSyncConditionalHandlerSchema.FilterChangeSync, true);
					if (base.IsFirstSyncScenario)
					{
						Command.CurrentCommand.Context.SetDiagnosticValue(AirSyncConditionalHandlerSchema.InitialSync, true);
					}
				}
				firstTimeSyncProvider.FirstTimeSync(null, minWatermark, queryBasedSyncFilter.FilterQuery, -1, queryBasedSyncFilter.EntriesInFilter);
				return;
			}
			ExTraceGlobals.SyncProcessTracer.Information((long)this.GetHashCode(), "FirstTimeFolderSync.InitializeAllItemsInFilter.  Using Old Provider");
			base.InitializeAllItemsInFilter(queryBasedSyncFilter);
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x00088844 File Offset: 0x00086A44
		protected override void UpdateWatermarkFromMinReceivedDate(ExDateTime minReceivedDate)
		{
			FirstTimeSyncWatermark firstTimeSyncWatermark = this.CurFTSMaxWatermark as FirstTimeSyncWatermark;
			if (firstTimeSyncWatermark == null)
			{
				this.CurFTSMaxWatermark = FirstTimeSyncWatermark.Create(minReceivedDate, 0);
				return;
			}
			if (firstTimeSyncWatermark.ReceivedDateUtc == ExDateTime.MinValue || firstTimeSyncWatermark.ReceivedDateUtc > minReceivedDate)
			{
				firstTimeSyncWatermark.Update(0, false, minReceivedDate);
			}
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x00088898 File Offset: 0x00086A98
		protected override bool GetNewOperations(int windowSize, Dictionary<ISyncItemId, ServerManifestEntry> tempServerManifest)
		{
			if (base.CurSnapShotWatermark == null)
			{
				if (Command.CurrentCommand != null)
				{
					Command.CurrentCommand.ProtocolLogger.SetProviderSyncType(this.CollectionId, ProviderSyncType.ICS);
					Command.CurrentCommand.Context.SetDiagnosticValue(AirSyncConditionalHandlerSchema.IcsSync, true);
				}
				return this.syncProvider.GetNewOperations(base.CurMaxWatermark, null, true, windowSize, null, tempServerManifest);
			}
			FirstTimeSyncProvider firstTimeSyncProvider = this.syncProvider as FirstTimeSyncProvider;
			if (this.CurFTSMaxWatermark == null)
			{
				this.CurFTSMaxWatermark = firstTimeSyncProvider.GetNewFirstTimeSyncWatermark();
			}
			ExTraceGlobals.SyncProcessTracer.Information<ExDateTime>((long)this.GetHashCode(), "FirstTimeFolderSync.GetNewOperations.  Performing FirstTimeSync with MaxWatermark '{0}'", (this.CurFTSMaxWatermark as FirstTimeSyncWatermark).ReceivedDateUtc);
			if (Command.CurrentCommand != null)
			{
				Command.CurrentCommand.ProtocolLogger.SetProviderSyncType(this.CollectionId, ProviderSyncType.IQ);
				Command.CurrentCommand.Context.SetDiagnosticValue(AirSyncConditionalHandlerSchema.ItemQuerySync, true);
			}
			return firstTimeSyncProvider.FirstTimeSync(base.ClientState, this.CurFTSMaxWatermark as FirstTimeSyncWatermark, FolderSync.ComputeQueryHint(this.filters), windowSize, tempServerManifest);
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x0008899C File Offset: 0x00086B9C
		protected override void MarkFirstTimeSyncAsComplete()
		{
			base.MarkFirstTimeSyncAsComplete();
			this.CurFTSMaxWatermark = null;
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x000889AC File Offset: 0x00086BAC
		protected override void CommitPreviousState()
		{
			ISyncWatermark prevMaxWatermark;
			ISyncWatermark prevFTSMaxWatermark;
			Dictionary<ISyncItemId, ServerManifestEntry> prevServerManifest;
			string prevFilterId;
			Dictionary<ISyncItemId, ServerManifestEntry> prevDelayedServerOperationQueue;
			ISyncWatermark prevSnapShotWatermark;
			bool prevLastSyncConversationMode;
			(this.acknowledgeModifications as FirstTimeFolderSync.FirstTimeAcknowledgeModifications).CommitPreviousState(out prevMaxWatermark, out prevFTSMaxWatermark, out prevServerManifest, out prevFilterId, out prevDelayedServerOperationQueue, out prevSnapShotWatermark, out prevLastSyncConversationMode);
			base.PrevMaxWatermark = prevMaxWatermark;
			this.PrevFTSMaxWatermark = prevFTSMaxWatermark;
			base.PrevServerManifest = prevServerManifest;
			base.PrevFilterId = prevFilterId;
			base.PrevDelayedServerOperationQueue = prevDelayedServerOperationQueue;
			base.PrevSnapShotWatermark = prevSnapShotWatermark;
			base.PrevLastSyncConversationMode = prevLastSyncConversationMode;
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x00088A0C File Offset: 0x00086C0C
		protected override void SavePreviousState()
		{
			(this.acknowledgeModifications as FirstTimeFolderSync.FirstTimeAcknowledgeModifications).SavePreviousState((ISyncWatermark)base.CurMaxWatermark.Clone(), (this.CurFTSMaxWatermark != null) ? ((ISyncWatermark)this.CurFTSMaxWatermark.Clone()) : null, FolderSync.CloneDictionary(base.CurServerManifest), base.CurFilterId, FolderSync.CloneDictionary(base.CurDelayedServerOperationQueue), (base.CurSnapShotWatermark != null) ? ((ISyncWatermark)base.CurSnapShotWatermark.Clone()) : null, base.CurLastSyncConversationMode);
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x00088A91 File Offset: 0x00086C91
		protected override FolderSync.AcknowledgeModifications CreateAcknowledgeModifications()
		{
			return new FirstTimeFolderSync.FirstTimeAcknowledgeModifications();
		}

		// Token: 0x04000E06 RID: 3590
		private List<FolderSync.StateModifier> prevMaxFTSWatermarkModifiers = new List<FolderSync.StateModifier>(5);

		// Token: 0x04000E07 RID: 3591
		private ISyncWatermark backupCurFTSMaxWatermark;

		// Token: 0x04000E08 RID: 3592
		private bool backupCurFTSMaxWatermarkHasBeenSet;

		// Token: 0x02000264 RID: 612
		private class FirstTimeAcknowledgeModifications : FolderSync.AcknowledgeModifications
		{
			// Token: 0x060016AA RID: 5802 RVA: 0x00088A98 File Offset: 0x00086C98
			public void CommitPreviousState(out ISyncWatermark prevMaxWatermark, out ISyncWatermark prevFTSMaxWatermark, out Dictionary<ISyncItemId, ServerManifestEntry> prevServerManifest, out string prevFilterId, out Dictionary<ISyncItemId, ServerManifestEntry> prevDelayedServerOperationQueue, out ISyncWatermark prevSnapShotWatermark, out bool prevLastSyncConversationMode)
			{
				base.CommitPreviousState(out prevMaxWatermark, out prevServerManifest, out prevFilterId, out prevDelayedServerOperationQueue, out prevSnapShotWatermark, out prevLastSyncConversationMode);
				prevFTSMaxWatermark = this.prevFTSMaxWatermark;
			}

			// Token: 0x060016AB RID: 5803 RVA: 0x00088AB2 File Offset: 0x00086CB2
			public void SavePreviousState(ISyncWatermark prevMaxWatermark, ISyncWatermark prevFTSMaxWatermark, Dictionary<ISyncItemId, ServerManifestEntry> prevServerManifest, string prevFilterId, Dictionary<ISyncItemId, ServerManifestEntry> prevDelayedServerOperationQueue, ISyncWatermark prevSnapShotWatermark, bool prevLastSyncConversationMode)
			{
				base.SavePreviousState(prevMaxWatermark, prevServerManifest, prevFilterId, prevDelayedServerOperationQueue, prevSnapShotWatermark, prevLastSyncConversationMode);
				this.prevFTSMaxWatermark = prevFTSMaxWatermark;
			}

			// Token: 0x04000E0A RID: 3594
			private ISyncWatermark prevFTSMaxWatermark;
		}
	}
}
