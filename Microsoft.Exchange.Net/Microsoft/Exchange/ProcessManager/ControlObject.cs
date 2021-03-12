using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.ProcessManager
{
	// Token: 0x02000807 RID: 2055
	internal sealed class ControlObject : WorkerControlObject
	{
		// Token: 0x06002B26 RID: 11046 RVA: 0x0005E810 File Offset: 0x0005CA10
		public ControlObject(PipeStream readPipeStream, ControlObject.TransportWorker transportWorkerArg) : base(readPipeStream, transportWorkerArg, ExTraceGlobals.ProcessManagerTracer)
		{
			this.transportWorker = transportWorkerArg;
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06002B27 RID: 11047 RVA: 0x0005E826 File Offset: 0x0005CA26
		public bool SeenActivateCommand
		{
			get
			{
				return this.seenActivateCommand;
			}
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x0005E830 File Offset: 0x0005CA30
		protected override bool ProcessCommand(char command, int size, Stream data)
		{
			if (command <= 'F')
			{
				if (command == 'A')
				{
					this.seenActivateCommand = true;
					this.transportWorker.Activate();
					return true;
				}
				if (command == 'F')
				{
					this.transportWorker.HandleLogFlush();
					return true;
				}
			}
			else
			{
				switch (command)
				{
				case 'L':
					this.transportWorker.ClearConfigCache();
					return true;
				case 'M':
					this.transportWorker.HandleMemoryPressure();
					return true;
				case 'N':
				{
					if (size < 1)
					{
						this.socketCreationErrors += 1L;
						ExTraceGlobals.ProcessManagerTracer.TraceError<int, long>(0L, "A unexpected new connection message with size {0} received. Connection error count = {1}", size, this.socketCreationErrors);
						return true;
					}
					Socket socket = null;
					SocketInformation socketInformation;
					try
					{
						socketInformation = (SocketInformation)ControlObject.socketInfoFormatter.Deserialize(data);
					}
					catch (SerializationException arg)
					{
						this.socketCreationErrors += 1L;
						ExTraceGlobals.ProcessManagerTracer.TraceError<SerializationException, long>(0L, "ControlObject.CommandReceived: SerializationException: {0}. Connection error count = {1}", arg, this.socketCreationErrors);
						return true;
					}
					try
					{
						socket = new Socket(socketInformation);
					}
					catch (SocketException arg2)
					{
						this.socketCreationErrors += 1L;
						ExTraceGlobals.ProcessManagerTracer.TraceError<SocketException, long>(0L, "ControlObject.CommandReceived: SocketException: {0}. Connection error count = {1}", arg2, this.socketCreationErrors);
						return true;
					}
					if (socket != null)
					{
						this.transportWorker.HandleConnection(socket);
						return true;
					}
					return true;
				}
				case 'O':
				case 'P':
					break;
				case 'Q':
					this.transportWorker.HandleBlockedSubmissionQueue();
					return true;
				default:
					switch (command)
					{
					case 'U':
						this.transportWorker.ConfigUpdate();
						return true;
					case 'W':
						this.transportWorker.HandleForceCrash();
						return true;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x0400258D RID: 9613
		private readonly ControlObject.TransportWorker transportWorker;

		// Token: 0x0400258E RID: 9614
		private static IFormatter socketInfoFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);

		// Token: 0x0400258F RID: 9615
		private bool seenActivateCommand;

		// Token: 0x04002590 RID: 9616
		private long socketCreationErrors;

		// Token: 0x02000808 RID: 2056
		public interface TransportWorker : IWorkerProcess
		{
			// Token: 0x06002B2A RID: 11050
			void Activate();

			// Token: 0x06002B2B RID: 11051
			void ConfigUpdate();

			// Token: 0x06002B2C RID: 11052
			void HandleMemoryPressure();

			// Token: 0x06002B2D RID: 11053
			void HandleLogFlush();

			// Token: 0x06002B2E RID: 11054
			void HandleBlockedSubmissionQueue();

			// Token: 0x06002B2F RID: 11055
			void ClearConfigCache();

			// Token: 0x06002B30 RID: 11056
			void HandleForceCrash();

			// Token: 0x06002B31 RID: 11057
			void HandleConnection(Socket clientConnection);
		}

		// Token: 0x02000809 RID: 2057
		private static class ServiceToWorkerCommands
		{
			// Token: 0x04002591 RID: 9617
			public const char NewConnection = 'N';

			// Token: 0x04002592 RID: 9618
			public const char Activate = 'A';

			// Token: 0x04002593 RID: 9619
			public const char ConfigUpdate = 'U';

			// Token: 0x04002594 RID: 9620
			public const char HandleMemoryPressure = 'M';

			// Token: 0x04002595 RID: 9621
			public const char ClearConfigCache = 'L';

			// Token: 0x04002596 RID: 9622
			public const char HandleLogFlush = 'F';

			// Token: 0x04002597 RID: 9623
			public const char HandleBlockedSubmissionQueue = 'Q';

			// Token: 0x04002598 RID: 9624
			public const char HandleForceCrash = 'W';
		}
	}
}
