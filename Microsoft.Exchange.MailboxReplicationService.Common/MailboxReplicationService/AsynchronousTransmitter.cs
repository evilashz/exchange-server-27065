using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000C7 RID: 199
	internal class AsynchronousTransmitter : DisposableWrapper<IDataImport>, IDataImport, IDisposable
	{
		// Token: 0x060007B8 RID: 1976 RVA: 0x0000C91C File Offset: 0x0000AB1C
		public AsynchronousTransmitter(IDataImport destination, bool ownsDestination) : base(destination, ownsDestination)
		{
			this.currentMessage = null;
			this.replyMessage = null;
			this.lastFailure = null;
			this.eventWakeUpTransmitter = new ManualResetEvent(false);
			this.eventBufferIsAvailableToAccept = new ManualResetEvent(true);
			this.quitting = false;
			this.transmitThread = null;
			this.mainThread = Thread.CurrentThread;
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0000C977 File Offset: 0x0000AB77
		void IDataImport.SendMessage(IDataMessage message)
		{
			this.SpinUpTransmitterThread();
			this.WaitUntilDataIsProcessed();
			this.currentMessage = message;
			this.eventBufferIsAvailableToAccept.Reset();
			this.eventWakeUpTransmitter.Set();
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0000C9A4 File Offset: 0x0000ABA4
		IDataMessage IDataImport.SendMessageAndWaitForReply(IDataMessage message)
		{
			((IDataImport)this).SendMessage(new AsynchronousTransmitter.AsyncTransmitterWaitForReplyMessage(message));
			this.WaitUntilDataIsProcessed();
			IDataMessage result = this.replyMessage;
			this.replyMessage = null;
			return result;
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0000CA10 File Offset: 0x0000AC10
		public void Close()
		{
			CommonUtils.CatchKnownExceptions(delegate
			{
				this.SpinUpTransmitterThread();
				CommonUtils.SafeWait(this.eventBufferIsAvailableToAccept, this.transmitThread);
				this.quitting = true;
				this.eventWakeUpTransmitter.Set();
				this.transmitThread.Join();
				this.transmitThread = null;
			}, null);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0000CA24 File Offset: 0x0000AC24
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				this.Close();
				this.eventWakeUpTransmitter.Close();
				this.eventBufferIsAvailableToAccept.Close();
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0000CA4C File Offset: 0x0000AC4C
		private void SpinUpTransmitterThread()
		{
			if (this.transmitThread != null)
			{
				return;
			}
			this.traceActivityID = MrsTracer.ActivityID;
			this.configContexts = SettingsContextBase.GetCurrentContexts();
			Thread thread = new Thread(new ThreadStart(this.TransmitThread));
			thread.Name = "MRS transmitter thread";
			thread.Start();
			this.transmitThread = thread;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0000CAA4 File Offset: 0x0000ACA4
		private void WaitUntilDataIsProcessed()
		{
			CommonUtils.SafeWait(this.eventBufferIsAvailableToAccept, this.transmitThread);
			if (this.lastFailure != null)
			{
				Exception ex = this.lastFailure;
				this.lastFailure = null;
				this.currentMessage = null;
				ExecutionContext.StampCurrentDataContext(ex);
				throw ex;
			}
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0000CBAE File Offset: 0x0000ADAE
		private void TransmitThread()
		{
			MrsTracer.ActivityID = this.traceActivityID;
			SettingsContextBase.RunOperationInContext(this.configContexts, delegate
			{
				for (;;)
				{
					CommonUtils.SafeWait(this.eventWakeUpTransmitter, this.mainThread);
					if (this.quitting)
					{
						break;
					}
					CommonUtils.CatchKnownExceptions(delegate
					{
						AsynchronousTransmitter.AsyncTransmitterWaitForReplyMessage asyncTransmitterWaitForReplyMessage = this.currentMessage as AsynchronousTransmitter.AsyncTransmitterWaitForReplyMessage;
						if (asyncTransmitterWaitForReplyMessage != null)
						{
							this.replyMessage = base.WrappedObject.SendMessageAndWaitForReply(asyncTransmitterWaitForReplyMessage.Request);
							return;
						}
						base.WrappedObject.SendMessage(this.currentMessage);
					}, delegate(Exception failure)
					{
						failure.PreserveExceptionStack();
						this.lastFailure = failure;
					});
					this.currentMessage = null;
					this.eventWakeUpTransmitter.Reset();
					this.eventBufferIsAvailableToAccept.Set();
				}
			});
		}

		// Token: 0x04000496 RID: 1174
		private IDataMessage currentMessage;

		// Token: 0x04000497 RID: 1175
		private IDataMessage replyMessage;

		// Token: 0x04000498 RID: 1176
		private Exception lastFailure;

		// Token: 0x04000499 RID: 1177
		private ManualResetEvent eventWakeUpTransmitter;

		// Token: 0x0400049A RID: 1178
		private ManualResetEvent eventBufferIsAvailableToAccept;

		// Token: 0x0400049B RID: 1179
		private bool quitting;

		// Token: 0x0400049C RID: 1180
		private Thread transmitThread;

		// Token: 0x0400049D RID: 1181
		private Thread mainThread;

		// Token: 0x0400049E RID: 1182
		private int traceActivityID;

		// Token: 0x0400049F RID: 1183
		private List<SettingsContextBase> configContexts;

		// Token: 0x020000C9 RID: 201
		private class AsyncTransmitterWaitForReplyMessage : IDataMessage
		{
			// Token: 0x060007C6 RID: 1990 RVA: 0x0000CBD2 File Offset: 0x0000ADD2
			public AsyncTransmitterWaitForReplyMessage(IDataMessage requestMessage)
			{
				this.requestMessage = requestMessage;
			}

			// Token: 0x170002A8 RID: 680
			// (get) Token: 0x060007C7 RID: 1991 RVA: 0x0000CBE1 File Offset: 0x0000ADE1
			public IDataMessage Request
			{
				get
				{
					return this.requestMessage;
				}
			}

			// Token: 0x060007C8 RID: 1992 RVA: 0x0000CBE9 File Offset: 0x0000ADE9
			int IDataMessage.GetSize()
			{
				return this.Request.GetSize();
			}

			// Token: 0x060007C9 RID: 1993 RVA: 0x0000CBF6 File Offset: 0x0000ADF6
			void IDataMessage.Serialize(bool useCompression, out DataMessageOpcode opcode, out byte[] data)
			{
				this.Request.Serialize(useCompression, out opcode, out data);
			}

			// Token: 0x040004A0 RID: 1184
			private IDataMessage requestMessage;
		}
	}
}
