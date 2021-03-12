using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensions;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000002 RID: 2
	internal class DataExport : DisposeTrackableBase, IDataExport, IDataImport, IDisposable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public DataExport(IDataMessage getDataResponseMsg, MailboxReplicationProxyService service)
		{
			this.getDataResponseMsg = getDataResponseMsg;
			this.nextOpcode = DataMessageOpcode.None;
			this.nextBuffer = null;
			this.result = DataExport.DataExportResult.Done;
			this.exportFailure = null;
			this.exportThread = null;
			this.eventDataAvailable = new AutoResetEvent(false);
			this.eventDataProcessed = new AutoResetEvent(false);
			this.cancelExport = false;
			this.service = service;
			this.lastReturnWasTimeout = false;
			this.storedOpcode = 0;
			this.storedData = null;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002148 File Offset: 0x00000348
		DataExportBatch IDataExport.ExportData()
		{
			DataExportBatch dataExportBatch = null;
			int opcode;
			byte[] data;
			for (;;)
			{
				DataExport.DataExportResult dataExportResult;
				if (this.storedOpcode != 0)
				{
					dataExportResult = DataExport.DataExportResult.MoreData;
					opcode = this.storedOpcode;
					data = this.storedData;
					this.storedOpcode = 0;
					this.storedData = null;
				}
				else
				{
					dataExportResult = this.GetNextOutput(out opcode, out data);
				}
				bool flag = dataExportBatch == null;
				if (flag)
				{
					dataExportBatch = new DataExportBatch();
				}
				switch (dataExportResult)
				{
				case DataExport.DataExportResult.Done:
					goto IL_B2;
				case DataExport.DataExportResult.MoreData:
					if (!flag)
					{
						goto Block_4;
					}
					dataExportBatch.Opcode = opcode;
					dataExportBatch.Data = data;
					break;
				case DataExport.DataExportResult.Flush:
					dataExportBatch.FlushAfterImport = true;
					break;
				case DataExport.DataExportResult.Timeout:
					goto IL_88;
				}
			}
			Block_4:
			this.storedOpcode = opcode;
			this.storedData = data;
			return dataExportBatch;
			IL_88:
			MrsTracer.ProxyService.Warning("ExportThread appears to be stuck in a call. Returning to prevent WCF call timeout. Will continue waiting on the next call.", new object[0]);
			dataExportBatch.FlushAfterImport = true;
			return dataExportBatch;
			IL_B2:
			dataExportBatch.IsLastBatch = true;
			return dataExportBatch;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002210 File Offset: 0x00000410
		void IDataExport.CancelExport()
		{
			MrsTracer.ProxyService.Function("DataExport.CancelExport", new object[0]);
			if (this.exportThread == null)
			{
				MrsTracer.ProxyService.Warning("CancelExport should not be called after export was finished.", new object[0]);
				return;
			}
			this.cancelExport = true;
			this.eventDataProcessed.Set();
			this.exportThread.Join();
			this.exportThread = null;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002278 File Offset: 0x00000478
		void IDataImport.SendMessage(IDataMessage message)
		{
			MrsTracer.ProxyService.Function("DataExport.SendMessage", new object[0]);
			this.CheckForCancel();
			message.Serialize(this.service.UseCompression, out this.nextOpcode, out this.nextBuffer);
			this.result = DataExport.DataExportResult.MoreData;
			this.eventDataAvailable.Set();
			this.WaitForTheNextCall();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000022D8 File Offset: 0x000004D8
		IDataMessage IDataImport.SendMessageAndWaitForReply(IDataMessage message)
		{
			MrsTracer.ProxyService.Function("DataExport.SendMessageAndWaitForReply", new object[0]);
			if (message is FlushMessage)
			{
				this.CheckForCancel();
				this.result = DataExport.DataExportResult.Flush;
				this.eventDataAvailable.Set();
				this.WaitForTheNextCall();
				return null;
			}
			if (message is FxProxyGetObjectDataRequestMessage && this.getDataResponseMsg is FxProxyGetObjectDataResponseMessage)
			{
				return this.getDataResponseMsg;
			}
			if (message is FxProxyPoolGetFolderDataRequestMessage && this.getDataResponseMsg is FxProxyPoolGetFolderDataResponseMessage)
			{
				return this.getDataResponseMsg;
			}
			throw new UnexpectedErrorPermanentException(-2147024809);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002430 File Offset: 0x00000630
		public void FolderExport(ISourceFolder folder, CopyPropertiesFlags flags, PropTag[] excludeTags)
		{
			this.RunExportThread(delegate
			{
				using (BufferedTransmitter bufferedTransmitter = new BufferedTransmitter(this, this.service.ExportBufferSizeFromMrsKB, false, this.service.UseBufferring, this.service.UseCompression))
				{
					using (AsynchronousTransmitter asynchronousTransmitter = new AsynchronousTransmitter(bufferedTransmitter, false))
					{
						using (FxProxyTransmitter fxProxyTransmitter = new FxProxyTransmitter(asynchronousTransmitter, false))
						{
							folder.CopyTo(fxProxyTransmitter, flags, excludeTags);
						}
					}
				}
			});
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002568 File Offset: 0x00000768
		public void FoldersExport(ISourceMailbox mailbox, List<byte[]> folderIds, ExportFoldersDataToCopyFlags exportFoldersDataToCopyFlags, GetFolderRecFlags folderRecFlags, PropTag[] additionalFolderRecProps, CopyPropertiesFlags copyPropertiesFlags, PropTag[] excludeProps, AclFlags extendedAclFlags)
		{
			this.RunExportThread(delegate
			{
				using (BufferedTransmitter bufferedTransmitter = new BufferedTransmitter(this, this.service.ExportBufferSizeFromMrsKB, false, this.service.UseBufferring, this.service.UseCompression))
				{
					using (AsynchronousTransmitter asynchronousTransmitter = new AsynchronousTransmitter(bufferedTransmitter, false))
					{
						using (FxProxyPoolTransmitter fxProxyPoolTransmitter = new FxProxyPoolTransmitter(asynchronousTransmitter, false, this.service.ClientVersion))
						{
							mailbox.ExportFolders(folderIds, fxProxyPoolTransmitter, exportFoldersDataToCopyFlags, folderRecFlags, additionalFolderRecProps, copyPropertiesFlags, excludeProps, extendedAclFlags);
						}
					}
				}
			});
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000269C File Offset: 0x0000089C
		public void FolderExportMessages(ISourceFolder folder, CopyMessagesFlags flags, byte[][] entryIds)
		{
			this.RunExportThread(delegate
			{
				using (BufferedTransmitter bufferedTransmitter = new BufferedTransmitter(this, this.service.ExportBufferSizeFromMrsKB, false, this.service.UseBufferring, this.service.UseCompression))
				{
					using (AsynchronousTransmitter asynchronousTransmitter = new AsynchronousTransmitter(bufferedTransmitter, false))
					{
						using (FxProxyTransmitter fxProxyTransmitter = new FxProxyTransmitter(asynchronousTransmitter, false))
						{
							folder.ExportMessages(fxProxyTransmitter, flags, entryIds);
						}
					}
				}
			});
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000027A0 File Offset: 0x000009A0
		public void MailboxExport(ISourceMailbox mailbox, PropTag[] excludeTags)
		{
			this.RunExportThread(delegate
			{
				using (BufferedTransmitter bufferedTransmitter = new BufferedTransmitter(this, this.service.ExportBufferSizeFromMrsKB, false, this.service.UseBufferring, this.service.UseCompression))
				{
					using (AsynchronousTransmitter asynchronousTransmitter = new AsynchronousTransmitter(bufferedTransmitter, false))
					{
						using (FxProxyTransmitter fxProxyTransmitter = new FxProxyTransmitter(asynchronousTransmitter, false))
						{
							mailbox.CopyTo(fxProxyTransmitter, excludeTags);
						}
					}
				}
			});
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000028BC File Offset: 0x00000ABC
		public void MessageExport(ISourceMailbox mailbox, List<MessageRec> messages, ExportMessagesFlags flags, PropTag[] excludeProps)
		{
			this.RunExportThread(delegate
			{
				using (BufferedTransmitter bufferedTransmitter = new BufferedTransmitter(this, this.service.ExportBufferSizeFromMrsKB, false, this.service.UseBufferring, this.service.UseCompression))
				{
					using (AsynchronousTransmitter asynchronousTransmitter = new AsynchronousTransmitter(bufferedTransmitter, false))
					{
						using (FxProxyPoolTransmitter fxProxyPoolTransmitter = new FxProxyPoolTransmitter(asynchronousTransmitter, false, this.service.ClientVersion))
						{
							mailbox.ExportMessages(messages, fxProxyPoolTransmitter, flags, null, excludeProps);
						}
					}
				}
			});
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000029F8 File Offset: 0x00000BF8
		public void MessageExportWithBadMessageDetection(ISourceMailbox mailbox, List<MessageRec> messages, ExportMessagesFlags flags, PropTag[] excludeProps, bool isDownlevelClient)
		{
			this.RunExportThread(delegate
			{
				List<BadMessageRec> list = new List<BadMessageRec>();
				MapiUtils.ExportMessagesWithBadItemDetection(mailbox, messages, delegate
				{
					BufferedTransmitter destination = new BufferedTransmitter(this, this.service.ExportBufferSizeFromMrsKB, false, this.service.UseBufferring, this.service.UseCompression);
					AsynchronousTransmitter destination2 = new AsynchronousTransmitter(destination, true);
					return new FxProxyPoolTransmitter(destination2, true, this.service.ClientVersion);
				}, flags, null, excludeProps, TestIntegration.Instance, ref list);
				if (list != null && list.Count > 0)
				{
					MessageExportResultTransmitter messageExportResultTransmitter = new MessageExportResultTransmitter(this, isDownlevelClient);
					messageExportResultTransmitter.SendMessageExportResults(list);
					((IDataImport)this).SendMessageAndWaitForReply(FlushMessage.Instance);
				}
			});
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002A4C File Offset: 0x00000C4C
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				if (this.exportThread != null)
				{
					MrsTracer.ProxyService.Warning("Disposing DataExport while ExportThread is still active.", new object[0]);
					this.cancelExport = true;
					this.eventDataProcessed.Set();
					this.exportThread.Join();
					this.exportThread = null;
				}
				this.eventDataAvailable.Close();
				this.eventDataProcessed.Close();
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002AB4 File Offset: 0x00000CB4
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<DataExport>(this);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002ABC File Offset: 0x00000CBC
		private DataExport.DataExportResult GetNextOutput(out int opcode, out byte[] data)
		{
			MrsTracer.ProxyService.Function("DataExport.GetNextOutput", new object[0]);
			opcode = 0;
			data = null;
			if (this.exportThread == null)
			{
				MrsTracer.ProxyService.Error("ExportData should not be called after the export has finished.", new object[0]);
				throw new UnexpectedErrorPermanentException(-2147024809);
			}
			if (!this.lastReturnWasTimeout)
			{
				this.nextOpcode = DataMessageOpcode.None;
				this.nextBuffer = null;
				this.result = DataExport.DataExportResult.Done;
				this.eventDataProcessed.Set();
			}
			if (!CommonUtils.SafeWaitWithTimeout(this.eventDataAvailable, this.exportThread, DataExport.maxCallLength))
			{
				this.lastReturnWasTimeout = true;
				return DataExport.DataExportResult.Timeout;
			}
			this.lastReturnWasTimeout = false;
			if (this.result == DataExport.DataExportResult.MoreData)
			{
				opcode = (int)this.nextOpcode;
				data = this.nextBuffer;
			}
			if (this.result == DataExport.DataExportResult.Done)
			{
				this.exportThread.Join();
				this.exportThread = null;
			}
			if (this.exportFailure != null)
			{
				MrsTracer.ProxyService.Warning("Export failed:\n{0}\n{1}\nContext: {2}", new object[]
				{
					CommonUtils.FullExceptionMessage(this.exportFailure),
					this.exportFailure.StackTrace,
					ExecutionContext.GetDataContext(this.exportFailure)
				});
				ExecutionContext.StampCurrentDataContext(this.exportFailure);
				throw this.exportFailure;
			}
			return this.result;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002BF6 File Offset: 0x00000DF6
		private void CheckForCancel()
		{
			if (this.cancelExport)
			{
				MrsTracer.ProxyService.Warning("Export was canceled. Throwing exception to cancel Export routine", new object[0]);
				throw new DataExportPermanentException(new DataExportCanceledPermanentException());
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002C20 File Offset: 0x00000E20
		private void WaitForTheNextCall()
		{
			if (!this.eventDataProcessed.WaitOne(MRSProxyConfiguration.Instance.DataImportTimeout))
			{
				throw new DataExportTransientException(new DataExportTimeoutTransientException());
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002C44 File Offset: 0x00000E44
		private void RunExportThread(Action threadOperation)
		{
			this.traceActivityID = MrsTracer.ActivityID;
			this.configContexts = SettingsContextBase.GetCurrentContexts();
			this.exportThread = new Thread(new ParameterizedThreadStart(this.ExportThread));
			this.exportThread.Name = "DataExport Thread";
			this.exportThread.Start(threadOperation);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002CE0 File Offset: 0x00000EE0
		private void ExportThread(object context)
		{
			MrsTracer.ActivityID = this.traceActivityID;
			Action exportOperation = (Action)context;
			CommonUtils.CatchKnownExceptions(delegate
			{
				this.WaitForTheNextCall();
				SettingsContextBase.RunOperationInContext(this.configContexts, exportOperation);
				this.exportFailure = null;
			}, delegate(Exception ex)
			{
				ex.PreserveExceptionStack();
				this.exportFailure = ex;
			});
			this.result = DataExport.DataExportResult.Done;
			this.eventDataAvailable.Set();
		}

		// Token: 0x04000001 RID: 1
		private static readonly TimeSpan maxCallLength = TimeSpan.FromSeconds(30.0);

		// Token: 0x04000002 RID: 2
		private IDataMessage getDataResponseMsg;

		// Token: 0x04000003 RID: 3
		private DataMessageOpcode nextOpcode;

		// Token: 0x04000004 RID: 4
		private byte[] nextBuffer;

		// Token: 0x04000005 RID: 5
		private DataExport.DataExportResult result;

		// Token: 0x04000006 RID: 6
		private Exception exportFailure;

		// Token: 0x04000007 RID: 7
		private Thread exportThread;

		// Token: 0x04000008 RID: 8
		private AutoResetEvent eventDataAvailable;

		// Token: 0x04000009 RID: 9
		private AutoResetEvent eventDataProcessed;

		// Token: 0x0400000A RID: 10
		private bool lastReturnWasTimeout;

		// Token: 0x0400000B RID: 11
		private bool cancelExport;

		// Token: 0x0400000C RID: 12
		private int traceActivityID;

		// Token: 0x0400000D RID: 13
		private List<SettingsContextBase> configContexts;

		// Token: 0x0400000E RID: 14
		private MailboxReplicationProxyService service;

		// Token: 0x0400000F RID: 15
		private int storedOpcode;

		// Token: 0x04000010 RID: 16
		private byte[] storedData;

		// Token: 0x02000003 RID: 3
		private enum DataExportResult
		{
			// Token: 0x04000012 RID: 18
			Done,
			// Token: 0x04000013 RID: 19
			MoreData,
			// Token: 0x04000014 RID: 20
			Flush,
			// Token: 0x04000015 RID: 21
			Timeout
		}
	}
}
