using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000A5 RID: 165
	internal class DestinationMailboxWrapper : MailboxWrapper, IDestinationMailbox, IMailbox, IDisposable
	{
		// Token: 0x0600088D RID: 2189 RVA: 0x0003A520 File Offset: 0x00038720
		public DestinationMailboxWrapper(IDestinationMailbox mailbox, MailboxWrapperFlags flags, LocalizedString tracingId, params Guid[] syncStateKeyPrefixGuids) : base(mailbox, flags, tracingId)
		{
			Guid[] array = new Guid[syncStateKeyPrefixGuids.Length + 1];
			syncStateKeyPrefixGuids.CopyTo(array, 0);
			array[syncStateKeyPrefixGuids.Length] = DestinationMailboxWrapper.SyncStateKeySuffix;
			this.syncStateKey = DestinationMailboxWrapper.GetSyncStateKey(array);
			array[syncStateKeyPrefixGuids.Length] = DestinationMailboxWrapper.ICSSyncStateKeySuffix;
			this.icsSyncStateKey = DestinationMailboxWrapper.GetSyncStateKey(array);
			array[syncStateKeyPrefixGuids.Length] = DestinationMailboxWrapper.ReplaySyncStateKeySuffix;
			this.replaySyncStateKey = DestinationMailboxWrapper.GetSyncStateKey(array);
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x0003A5AB File Offset: 0x000387AB
		public IDestinationMailbox DestinationMailbox
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x0003A5AE File Offset: 0x000387AE
		// (set) Token: 0x06000890 RID: 2192 RVA: 0x0003A5B6 File Offset: 0x000387B6
		public PersistedSyncData SyncState { get; set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x0003A5BF File Offset: 0x000387BF
		// (set) Token: 0x06000892 RID: 2194 RVA: 0x0003A5C7 File Offset: 0x000387C7
		public MailboxMapiSyncState ICSSyncState { get; set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x0003A5D0 File Offset: 0x000387D0
		// (set) Token: 0x06000894 RID: 2196 RVA: 0x0003A5D8 File Offset: 0x000387D8
		public ReplaySyncState ReplaySyncState { get; set; }

		// Token: 0x06000895 RID: 2197 RVA: 0x0003A5E4 File Offset: 0x000387E4
		public SyncStateError LoadSyncState(Guid expectedRequestGuid, ReportData report, SyncStateFlags flags)
		{
			MrsTracer.Service.Debug("Attempting to load saved synchronization state.", new object[0]);
			string text = this.DestinationMailbox.LoadSyncState(this.syncStateKey);
			if (new TestIntegration(false).InjectCorruptSyncState)
			{
				text = "Some corrupt sync state";
			}
			PersistedSyncData persistedSyncData;
			try
			{
				persistedSyncData = PersistedSyncData.Deserialize(text);
			}
			catch (UnableToDeserializeXMLException failure)
			{
				DestinationMailboxWrapper.StringSummary stringSummary = new DestinationMailboxWrapper.StringSummary(text);
				report.Append(MrsStrings.ReportSyncStateCorrupt(expectedRequestGuid, stringSummary.Length, stringSummary.Start, stringSummary.End), failure, ReportEntryFlags.Target);
				return SyncStateError.CorruptSyncState;
			}
			if (persistedSyncData == null)
			{
				MrsTracer.Service.Debug("Saved sync state does not exist.", new object[0]);
				report.Append(MrsStrings.ReportSyncStateNull(expectedRequestGuid));
				return SyncStateError.NullSyncState;
			}
			if (persistedSyncData.MoveRequestGuid != expectedRequestGuid)
			{
				MrsTracer.Service.Warning("Saved state message contains invalid data (move request guid does not match). Discarding saved state.", new object[0]);
				report.Append(MrsStrings.ReportSyncStateWrongRequestGuid(expectedRequestGuid, persistedSyncData.MoveRequestGuid));
				return SyncStateError.WrongRequestGuid;
			}
			MrsTracer.Service.Debug("Successfully parsed saved state: stage={0}", new object[]
			{
				persistedSyncData.SyncStage
			});
			this.SyncState = persistedSyncData;
			string text2 = this.DestinationMailbox.LoadSyncState(this.icsSyncStateKey);
			try
			{
				this.ICSSyncState = MailboxMapiSyncState.Deserialize(text2);
			}
			catch (UnableToDeserializeXMLException failure2)
			{
				DestinationMailboxWrapper.StringSummary stringSummary2 = new DestinationMailboxWrapper.StringSummary(text2);
				report.Append(MrsStrings.ReportIcsSyncStateCorrupt(expectedRequestGuid, stringSummary2.Length, stringSummary2.Start, stringSummary2.End), failure2, ReportEntryFlags.Target);
				return SyncStateError.CorruptIcsSyncState;
			}
			if (this.ICSSyncState == null)
			{
				report.Append(MrsStrings.ReportIcsSyncStateNull(expectedRequestGuid));
				return SyncStateError.NullIcsSyncState;
			}
			if (flags.HasFlag(SyncStateFlags.Replay))
			{
				string text3 = this.DestinationMailbox.LoadSyncState(this.replaySyncStateKey);
				try
				{
					this.ReplaySyncState = ReplaySyncState.Deserialize(text3);
				}
				catch (UnableToDeserializeXMLException failure3)
				{
					DestinationMailboxWrapper.StringSummary stringSummary3 = new DestinationMailboxWrapper.StringSummary(text3);
					report.Append(MrsStrings.ReportReplaySyncStateCorrupt(expectedRequestGuid, stringSummary3.Length, stringSummary3.Start, stringSummary3.End), failure3, ReportEntryFlags.Target);
					return SyncStateError.CorruptReplaySyncState;
				}
				if (this.ReplaySyncState == null)
				{
					report.Append(MrsStrings.ReportReplaySyncStateNull(expectedRequestGuid));
					return SyncStateError.NullReplaySyncState;
				}
				report.AppendDebug(MrsStrings.ReportSyncStateLoaded2(expectedRequestGuid, text.Length, text2.Length, text3.Length));
			}
			else
			{
				report.AppendDebug(MrsStrings.ReportSyncStateLoaded(expectedRequestGuid, text.Length, text2.Length));
			}
			return SyncStateError.None;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0003A860 File Offset: 0x00038A60
		public void SaveSyncState()
		{
			this.DestinationMailbox.SaveSyncState(this.syncStateKey, this.SyncState.Serialize(false));
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0003A880 File Offset: 0x00038A80
		public void SaveICSSyncState(bool force)
		{
			ExDateTime utcNow = ExDateTime.UtcNow;
			if (!force && utcNow < this.icsSyncStateUpdateTimestamp + BaseJob.FlushInterval)
			{
				return;
			}
			this.DestinationMailbox.SaveSyncState(this.icsSyncStateKey, this.ICSSyncState.Serialize(false));
			this.icsSyncStateUpdateTimestamp = utcNow;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0003A8D4 File Offset: 0x00038AD4
		public void SaveReplaySyncState()
		{
			this.DestinationMailbox.SaveSyncState(this.replaySyncStateKey, this.ReplaySyncState.Serialize(false));
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0003A8F4 File Offset: 0x00038AF4
		public void ClearSyncState()
		{
			this.DestinationMailbox.SaveSyncState(this.syncStateKey, null);
			this.DestinationMailbox.SaveSyncState(this.icsSyncStateKey, null);
			this.DestinationMailbox.SaveSyncState(this.replaySyncStateKey, null);
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x0003A92F File Offset: 0x00038B2F
		protected override OperationSideDataContext SideOperationContext
		{
			get
			{
				return OperationSideDataContext.Target;
			}
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0003A936 File Offset: 0x00038B36
		public override IFolder GetFolder(byte[] folderId)
		{
			return this.DestinationMailbox.GetFolder(folderId);
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0003A944 File Offset: 0x00038B44
		public override void Clear()
		{
			base.Clear();
			this.SyncState = null;
			this.ICSSyncState = null;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0003A980 File Offset: 0x00038B80
		bool IDestinationMailbox.MailboxExists()
		{
			bool result = false;
			base.CreateContext("IDestinationMailbox.MailboxExists", new DataContext[0]).Execute(delegate
			{
				result = ((IDestinationMailbox)this.WrappedObject).MailboxExists();
			}, true);
			return result;
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0003AA00 File Offset: 0x00038C00
		CreateMailboxResult IDestinationMailbox.CreateMailbox(byte[] mailboxData, MailboxSignatureFlags sourceSignatureFlags)
		{
			CreateMailboxResult result = CreateMailboxResult.Success;
			base.CreateContext("IDestinationMailbox.CreateMailbox", new DataContext[]
			{
				new SimpleValueDataContext("MailboxData", TraceUtils.DumpBytes(mailboxData)),
				new SimpleValueDataContext("sourceSignatureFlags", sourceSignatureFlags)
			}).Execute(delegate
			{
				result = ((IDestinationMailbox)this.WrappedObject).CreateMailbox(mailboxData, sourceSignatureFlags);
			}, true);
			return result;
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0003AAB4 File Offset: 0x00038CB4
		void IDestinationMailbox.ProcessMailboxSignature(byte[] mailboxData)
		{
			base.CreateContext("IDestinationMailbox.ProcessMailbox", new DataContext[]
			{
				new SimpleValueDataContext("MailboxData", TraceUtils.DumpBytes(mailboxData))
			}).Execute(delegate
			{
				((IDestinationMailbox)this.WrappedObject).ProcessMailboxSignature(mailboxData);
			}, true);
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0003AB44 File Offset: 0x00038D44
		IDestinationFolder IDestinationMailbox.GetFolder(byte[] entryId)
		{
			IDestinationFolder result = null;
			base.CreateContext("IDestinationMailbox.GetFolder", new DataContext[]
			{
				new EntryIDsDataContext(entryId)
			}).Execute(delegate
			{
				result = ((IDestinationMailbox)this.WrappedObject).GetFolder(entryId);
			}, true);
			if (result == null)
			{
				return null;
			}
			return new DestinationFolderWrapper(result, base.CreateContext);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0003ABE4 File Offset: 0x00038DE4
		IFxProxy IDestinationMailbox.GetFxProxy()
		{
			IFxProxy result = null;
			base.CreateContext("IDestinationMailbox.GetFxProxy", new DataContext[0]).Execute(delegate
			{
				result = ((IDestinationMailbox)this.WrappedObject).GetFxProxy();
			}, true);
			if (result == null)
			{
				return null;
			}
			return new FxProxyWrapper(result, base.CreateContext);
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0003AC74 File Offset: 0x00038E74
		PropProblemData[] IDestinationMailbox.SetProps(PropValueData[] pva)
		{
			PropProblemData[] result = null;
			base.CreateContext("IDestinationMailbox.SetProps", new DataContext[]
			{
				new PropValuesDataContext(pva)
			}).Execute(delegate
			{
				result = ((IDestinationMailbox)this.WrappedObject).SetProps(pva);
			}, true);
			return result;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0003AD08 File Offset: 0x00038F08
		IFxProxyPool IDestinationMailbox.GetFxProxyPool(ICollection<byte[]> folderIds)
		{
			IFxProxyPool result = null;
			base.CreateContext("IDestinationMailbox.GetFxProxyPool", new DataContext[]
			{
				new EntryIDsDataContext(new List<byte[]>(folderIds))
			}).Execute(delegate
			{
				result = ((IDestinationMailbox)this.WrappedObject).GetFxProxyPool(folderIds);
			}, true);
			if (result == null)
			{
				return null;
			}
			return new FxProxyPoolWrapper(result, base.CreateContext);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0003ADBC File Offset: 0x00038FBC
		void IDestinationMailbox.CreateFolder(FolderRec sourceFolder, CreateFolderFlags createFolderFlags, out byte[] newFolderId)
		{
			byte[] newFolderIdInt = null;
			base.CreateContext("IDestinationMailbox.CreateFolder", new DataContext[]
			{
				new FolderRecDataContext(sourceFolder),
				new SimpleValueDataContext("CreateFolderFlags", createFolderFlags)
			}).Execute(delegate
			{
				((IDestinationMailbox)this.WrappedObject).CreateFolder(sourceFolder, createFolderFlags, out newFolderIdInt);
			}, true);
			newFolderId = newFolderIdInt;
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0003AE74 File Offset: 0x00039074
		void IDestinationMailbox.MoveFolder(byte[] folderId, byte[] oldParentId, byte[] newParentId)
		{
			base.CreateContext("IDestinationMailbox.MoveFolder", new DataContext[]
			{
				new EntryIDsDataContext(folderId),
				new SimpleValueDataContext("OldParentId", TraceUtils.DumpEntryId(oldParentId)),
				new SimpleValueDataContext("NewParentId", TraceUtils.DumpEntryId(newParentId))
			}).Execute(delegate
			{
				((IDestinationMailbox)this.WrappedObject).MoveFolder(folderId, oldParentId, newParentId);
			}, true);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0003AF30 File Offset: 0x00039130
		void IDestinationMailbox.DeleteFolder(FolderRec folderRec)
		{
			base.CreateContext("IDestinationMailbox.DeleteFolder", new DataContext[]
			{
				new FolderRecDataContext(folderRec)
			}).Execute(delegate
			{
				((IDestinationMailbox)this.WrappedObject).DeleteFolder(folderRec);
			}, true);
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0003AFB0 File Offset: 0x000391B0
		void IDestinationMailbox.SetMailboxSecurityDescriptor(RawSecurityDescriptor sd)
		{
			base.CreateContext("IDestinationMailbox.SetMailboxSecurityDescriptor", new DataContext[]
			{
				new SimpleValueDataContext("SD", CommonUtils.GetSDDLString(sd))
			}).Execute(delegate
			{
				((IDestinationMailbox)this.WrappedObject).SetMailboxSecurityDescriptor(sd);
			}, true);
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0003B038 File Offset: 0x00039238
		void IDestinationMailbox.SetUserSecurityDescriptor(RawSecurityDescriptor sd)
		{
			base.CreateContext("IDestinationMailbox.SetUserSecurityDescriptor", new DataContext[]
			{
				new SimpleValueDataContext("SD", CommonUtils.GetSDDLString(sd))
			}).Execute(delegate
			{
				((IDestinationMailbox)this.WrappedObject).SetUserSecurityDescriptor(sd);
			}, true);
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0003B0C0 File Offset: 0x000392C0
		void IDestinationMailbox.PreFinalSyncDataProcessing(int? sourceMailboxVersion)
		{
			base.CreateContext("IDestinationMailbox.PreFinalSyncDataProcessing", new DataContext[0]).Execute(delegate
			{
				((IDestinationMailbox)this.WrappedObject).PreFinalSyncDataProcessing(sourceMailboxVersion);
			}, true);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0003B13C File Offset: 0x0003933C
		ConstraintCheckResultType IDestinationMailbox.CheckDataGuarantee(DateTime commitTimestamp, out LocalizedString failureReason)
		{
			ConstraintCheckResultType result = ConstraintCheckResultType.Satisfied;
			LocalizedString failureReasonInt = LocalizedString.Empty;
			base.CreateContext("IDestinationMailbox.CheckDataGuarantee", new DataContext[]
			{
				new SimpleValueDataContext("CommitTimestamp", commitTimestamp)
			}).Execute(delegate
			{
				result = ((IDestinationMailbox)this.WrappedObject).CheckDataGuarantee(commitTimestamp, out failureReasonInt);
			}, true);
			failureReason = failureReasonInt;
			return result;
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0003B1D5 File Offset: 0x000393D5
		void IDestinationMailbox.ForceLogRoll()
		{
			base.CreateContext("IDestinationMailbox.ForceLogRoll", new DataContext[0]).Execute(delegate
			{
				((IDestinationMailbox)base.WrappedObject).ForceLogRoll();
			}, true);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0003B230 File Offset: 0x00039430
		List<ReplayAction> IDestinationMailbox.GetActions(string replaySyncState, int maxNumberOfActions)
		{
			List<ReplayAction> result = null;
			base.CreateContext("IDestinationMailbox.GetActions", new DataContext[]
			{
				new SimpleValueDataContext("ReplaySyncState", replaySyncState),
				new SimpleValueDataContext("MaxNumberOfActions", maxNumberOfActions)
			}).Execute(delegate
			{
				result = ((IDestinationMailbox)this.WrappedObject).GetActions(replaySyncState, maxNumberOfActions);
			}, true);
			return result;
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0003B2E0 File Offset: 0x000394E0
		void IDestinationMailbox.SetMailboxSettings(ItemPropertiesBase item)
		{
			base.CreateContext("IDestinationMailbox.SetMailboxSettings", new DataContext[]
			{
				new SimpleValueDataContext("SetMailboxSettings", item.GetType())
			}).Execute(delegate
			{
				((IDestinationMailbox)this.WrappedObject).SetMailboxSettings(item);
			}, true);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0003B344 File Offset: 0x00039544
		private static byte[] GetSyncStateKey(Guid[] syncStateKeyGuids)
		{
			byte[] array = new byte[16 * syncStateKeyGuids.Length];
			for (int i = 0; i < syncStateKeyGuids.Length; i++)
			{
				syncStateKeyGuids[i].ToByteArray().CopyTo(array, 16 * i);
			}
			return array;
		}

		// Token: 0x0400032D RID: 813
		private static readonly Guid SyncStateKeySuffix = new Guid("4f91cae0-9566-4c62-9889-2fa527aaa91e");

		// Token: 0x0400032E RID: 814
		private static readonly Guid ICSSyncStateKeySuffix = new Guid("b440559b-375b-4c77-ab84-806c1964b598");

		// Token: 0x0400032F RID: 815
		private static readonly Guid ReplaySyncStateKeySuffix = new Guid("2F9A4D65-3CA2-46CB-BE2C-59190E21D8F1");

		// Token: 0x04000330 RID: 816
		private byte[] syncStateKey;

		// Token: 0x04000331 RID: 817
		private byte[] icsSyncStateKey;

		// Token: 0x04000332 RID: 818
		private byte[] replaySyncStateKey;

		// Token: 0x04000333 RID: 819
		private ExDateTime icsSyncStateUpdateTimestamp;

		// Token: 0x020000A6 RID: 166
		private class StringSummary
		{
			// Token: 0x170001E2 RID: 482
			// (get) Token: 0x060008B1 RID: 2225 RVA: 0x0003B3B1 File Offset: 0x000395B1
			// (set) Token: 0x060008B2 RID: 2226 RVA: 0x0003B3B9 File Offset: 0x000395B9
			public int Length { get; private set; }

			// Token: 0x170001E3 RID: 483
			// (get) Token: 0x060008B3 RID: 2227 RVA: 0x0003B3C2 File Offset: 0x000395C2
			// (set) Token: 0x060008B4 RID: 2228 RVA: 0x0003B3CA File Offset: 0x000395CA
			public string Start { get; private set; }

			// Token: 0x170001E4 RID: 484
			// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0003B3D3 File Offset: 0x000395D3
			// (set) Token: 0x060008B6 RID: 2230 RVA: 0x0003B3DB File Offset: 0x000395DB
			public string End { get; private set; }

			// Token: 0x060008B7 RID: 2231 RVA: 0x0003B3E4 File Offset: 0x000395E4
			public StringSummary(string str)
			{
				if (str == null)
				{
					return;
				}
				this.Length = str.Length;
				if (str.Length <= 100)
				{
					this.End = str;
					this.Start = str;
					return;
				}
				this.Start = str.Substring(0, 100);
				this.End = str.Substring(str.Length - 100 - 1, 100);
			}

			// Token: 0x04000337 RID: 823
			private const int StartEndLength = 100;
		}
	}
}
