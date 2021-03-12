﻿using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.ProcessManager
{
	// Token: 0x02000805 RID: 2053
	internal abstract class WorkerControlObject
	{
		// Token: 0x06002B1D RID: 11037 RVA: 0x0005E50D File Offset: 0x0005C70D
		public WorkerControlObject(PipeStream readPipeStream, IWorkerProcess workerArg, Trace tracer)
		{
			this.readPipeStream = readPipeStream;
			this.workerProcess = workerArg;
			this.tracer = tracer;
			this.messageMemoryStream = new MemoryStream(this.pipeStreamMessageBuffer, false);
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x0005E54C File Offset: 0x0005C74C
		public bool Initialize()
		{
			bool result;
			try
			{
				this.readPipeStream.BeginRead(this.pipeStreamMessageBuffer, 0, this.pipeStreamMessageBuffer.Length, WorkerControlObject.readMessageComplete, this);
				result = true;
			}
			catch (IOException arg)
			{
				this.Disconnect();
				this.tracer.TraceDebug<IOException>(0L, "WorkerControlObject.Initialize: IOException on BeginRead {0}", arg);
				result = false;
			}
			return result;
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06002B1F RID: 11039 RVA: 0x0005E5B0 File Offset: 0x0005C7B0
		public bool SeenRetireCommand
		{
			get
			{
				return this.seenRetireCommand;
			}
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x0005E5B8 File Offset: 0x0005C7B8
		private static void ReadMessageComplete(IAsyncResult asyncResult)
		{
			WorkerControlObject workerControlObject = (WorkerControlObject)asyncResult.AsyncState;
			workerControlObject.ReadMessageCompleteInternal(asyncResult);
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x0005E5D8 File Offset: 0x0005C7D8
		private void ReadMessageCompleteInternal(IAsyncResult asyncResult)
		{
			try
			{
				int num = this.readPipeStream.EndRead(asyncResult);
				if (num == 0)
				{
					this.tracer.TraceDebug(0L, "WorkerControlObject.ReadMessageCompleteInternal: reading from control pipe returned 0 bytes");
					this.Disconnect();
					this.workerProcess.Stop();
				}
				else
				{
					this.CommandReceived(num);
					if (this.readPipeStream != null)
					{
						this.readPipeStream.BeginRead(this.pipeStreamMessageBuffer, 0, this.pipeStreamMessageBuffer.Length, WorkerControlObject.readMessageComplete, this);
					}
				}
			}
			catch (IOException arg)
			{
				this.tracer.TraceDebug<IOException>(0L, "WorkerControlObject.ReadMessageCompleteInternal: IOException {0}", arg);
				this.Disconnect();
				this.workerProcess.Stop();
			}
			catch (ObjectDisposedException arg2)
			{
				this.tracer.TraceDebug<ObjectDisposedException>(0L, "WorkerControlObject.ReadMessageCompleteInternal: ObjectDisposedException {0}", arg2);
			}
			catch (OperationCanceledException arg3)
			{
				this.tracer.TraceDebug<OperationCanceledException>(0L, "WorkerControlObject.ReadMessageCompleteInternal: OperationCanceledException {0}", arg3);
			}
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x0005E6CC File Offset: 0x0005C8CC
		private void Disconnect()
		{
			if (this.readPipeStream != null)
			{
				try
				{
					this.readPipeStream.Flush();
					this.readPipeStream.Dispose();
				}
				catch (IOException arg)
				{
					this.tracer.TraceDebug<IOException>(0L, "WorkerControlObject.Disconnect: IOException {0}", arg);
				}
				catch (ObjectDisposedException arg2)
				{
					this.tracer.TraceDebug<ObjectDisposedException>(0L, "WorkerControlObject.Disconnect on ObjectDisposedException {0}", arg2);
				}
				this.readPipeStream = null;
			}
		}

		// Token: 0x06002B23 RID: 11043 RVA: 0x0005E748 File Offset: 0x0005C948
		private void CommandReceived(int size)
		{
			char c = (char)this.pipeStreamMessageBuffer[0];
			if (c != 'C')
			{
				switch (c)
				{
				case 'P':
					this.workerProcess.Pause();
					return;
				case 'R':
					this.seenRetireCommand = true;
					this.Disconnect();
					this.workerProcess.Retire();
					return;
				}
				this.messageMemoryStream.Position = 1L;
				if (!this.ProcessCommand((char)this.pipeStreamMessageBuffer[0], size - 1, (size > 1) ? this.messageMemoryStream : null))
				{
					this.tracer.TraceError(0L, "Unknown message received by worker, forcing Retire to cause worker exit");
					this.Disconnect();
					this.workerProcess.Retire();
				}
				return;
			}
			this.workerProcess.Continue();
		}

		// Token: 0x06002B24 RID: 11044
		protected abstract bool ProcessCommand(char command, int size, Stream data);

		// Token: 0x04002583 RID: 9603
		private readonly IWorkerProcess workerProcess;

		// Token: 0x04002584 RID: 9604
		private static readonly AsyncCallback readMessageComplete = new AsyncCallback(WorkerControlObject.ReadMessageComplete);

		// Token: 0x04002585 RID: 9605
		private PipeStream readPipeStream;

		// Token: 0x04002586 RID: 9606
		private bool seenRetireCommand;

		// Token: 0x04002587 RID: 9607
		private byte[] pipeStreamMessageBuffer = new byte[2048];

		// Token: 0x04002588 RID: 9608
		private MemoryStream messageMemoryStream;

		// Token: 0x04002589 RID: 9609
		private readonly Trace tracer;

		// Token: 0x02000806 RID: 2054
		public static class StandardWorkerCommands
		{
			// Token: 0x0400258A RID: 9610
			public const char Retire = 'R';

			// Token: 0x0400258B RID: 9611
			public const char Pause = 'P';

			// Token: 0x0400258C RID: 9612
			public const char Continue = 'C';
		}
	}
}
