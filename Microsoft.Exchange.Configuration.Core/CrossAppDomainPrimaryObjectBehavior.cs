using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Exchange.Configuration.Core.EventLog;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x02000006 RID: 6
	internal class CrossAppDomainPrimaryObjectBehavior : CrossAppDomainObjectBehavior
	{
		// Token: 0x06000018 RID: 24 RVA: 0x0000256C File Offset: 0x0000076C
		internal CrossAppDomainPrimaryObjectBehavior(string namedPipeName, BehaviorDirection direction, CrossAppDomainPrimaryObjectBehavior.OnMessageReceived onMessagereceived) : base(namedPipeName, direction)
		{
			CrossAppDomainPrimaryObjectBehavior <>4__this = this;
			CoreLogger.ExecuteAndLog("CrossAppDomainPrimaryObjectBehavior.Ctor", true, null, null, delegate()
			{
				if (direction == BehaviorDirection.In)
				{
					<>4__this.onMessageReceived = onMessagereceived;
					<>4__this.serverStream = new NamedPipeServerStream(<>4__this.NamedPipeName, PipeDirection.In, 5);
					<>4__this.receiveMessageThread = new Thread(new ThreadStart(<>4__this.ReceiveMessage));
					<>4__this.receiveMessageThread.Start();
					return;
				}
				<>4__this.serverStream = new NamedPipeServerStream(<>4__this.NamedPipeName, PipeDirection.Out, 5);
				<>4__this.sendMessageThread = new Thread(new ThreadStart(<>4__this.SendMessage));
				<>4__this.sendMessageThread.Start();
			});
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000025D7 File Offset: 0x000007D7
		internal override bool IsActive
		{
			get
			{
				if (this.isShutDown || this.serverStream == null)
				{
					return false;
				}
				if (base.Direction == BehaviorDirection.In)
				{
					return this.receiveMessageThread.IsAlive;
				}
				return this.sendMessageThread.IsAlive;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002684 File Offset: 0x00000884
		public void SendMessage(string message)
		{
			CoreLogger.ExecuteAndLog("CrossAppDomainPrimaryObjectBehavior.SendMessage", true, null, null, delegate()
			{
				if (this.IsActive)
				{
					lock (this.sendMessageQueueLocker)
					{
						if (this.IsActive)
						{
							this.sendMessageQueue.Enqueue(message);
						}
					}
				}
			});
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002880 File Offset: 0x00000A80
		protected override void Dispose(bool isDisposing)
		{
			CoreLogger.ExecuteAndLog("CrossAppDomainPrimaryObjectBehavior.Dispose", false, null, null, delegate()
			{
				try
				{
					if (isDisposing && this.serverStream != null)
					{
						this.isShutDown = true;
						bool flag = false;
						if (this.Direction == BehaviorDirection.In && this.receiveMessageThread.IsAlive)
						{
							using (NamedPipeClientStream namedPipeClientStream = new NamedPipeClientStream(".", this.NamedPipeName, PipeDirection.Out))
							{
								flag = CrossAppDomainObjectBehavior.ConnectClientStream(namedPipeClientStream, 3000, this.NamedPipeName, true);
							}
							if (flag)
							{
								this.receiveMessageThread.Join();
							}
						}
						else if (this.Direction == BehaviorDirection.Out && this.sendMessageThread.IsAlive)
						{
							using (NamedPipeClientStream namedPipeClientStream2 = new NamedPipeClientStream(".", this.NamedPipeName, PipeDirection.In))
							{
								flag = CrossAppDomainObjectBehavior.ConnectClientStream(namedPipeClientStream2, 3000, this.NamedPipeName, true);
							}
							if (flag)
							{
								this.sendMessageThread.Join();
							}
						}
					}
				}
				finally
				{
					if (this.serverStream != null)
					{
						this.serverStream.Dispose();
					}
					this.serverStream = null;
					this.receiveMessageThread = null;
					this.sendMessageThread = null;
					this.<>n__FabricatedMethodb(isDisposing);
				}
			});
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002954 File Offset: 0x00000B54
		private void ReceiveMessage()
		{
			CoreLogger.ExecuteAndLog("CrossAppDomainPrimaryObjectBehavior.ReceiveMessage", false, null, null, delegate()
			{
				while (!this.isShutDown && this.serverStream != null)
				{
					this.serverStream.WaitForConnection();
					try
					{
						CoreLogger.TraceDebug("Server stream connected.", new object[0]);
						if (!this.isShutDown)
						{
							byte[] array = CrossAppDomainObjectBehavior.LoopReadData((byte[] buffer, int offset, int count) => this.serverStream.Read(buffer, offset, count));
							this.onMessageReceived(array, array.Length);
						}
					}
					finally
					{
						this.serverStream.Disconnect();
					}
				}
			});
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002AD4 File Offset: 0x00000CD4
		private void SendMessage()
		{
			CoreLogger.ExecuteAndLog("CrossAppDomainPrimaryObjectBehavior.SendMessage", false, null, null, delegate()
			{
				while (!this.isShutDown && this.serverStream != null)
				{
					int num = 0;
					try
					{
						this.serverStream.WaitForConnection();
						try
						{
							if (this.sendMessageQueue.Count != 0)
							{
								lock (this.sendMessageQueueLocker)
								{
									List<string> list = this.sendMessageQueue.ToList<string>();
									if (list != null && list.Count != 0)
									{
										byte[] array = CrossAppDomainObjectBehavior.PackMessages(list);
										this.serverStream.Write(array, 0, array.Length);
										this.sendMessageQueue.Clear();
									}
								}
							}
						}
						finally
						{
							this.serverStream.Disconnect();
						}
					}
					catch (Exception ex)
					{
						if (num >= 4)
						{
							throw;
						}
						ServiceController serviceController = new ServiceController("W3SVC");
						bool flag2 = serviceController.Status == ServiceControllerStatus.Running;
						if (flag2)
						{
							CoreLogger.LogEvent(TaskEventLogConstants.Tuple_CrossAppDomainPrimaryObjectBehaviorException, null, new object[]
							{
								base.NamedPipeName,
								"SendMessage",
								ex.ToString()
							});
						}
						if (this.serverStream != null)
						{
							this.serverStream.Dispose();
						}
						this.serverStream = new NamedPipeServerStream(base.NamedPipeName, PipeDirection.Out, 5);
						num++;
					}
				}
			});
		}

		// Token: 0x04000009 RID: 9
		private const int MaxNumberOfNamedPipeInstances = 5;

		// Token: 0x0400000A RID: 10
		private const int DummyStreamWatingTimeInMilliseconds = 3000;

		// Token: 0x0400000B RID: 11
		private readonly Queue<string> sendMessageQueue = new Queue<string>();

		// Token: 0x0400000C RID: 12
		private readonly object sendMessageQueueLocker = new object();

		// Token: 0x0400000D RID: 13
		private CrossAppDomainPrimaryObjectBehavior.OnMessageReceived onMessageReceived;

		// Token: 0x0400000E RID: 14
		private NamedPipeServerStream serverStream;

		// Token: 0x0400000F RID: 15
		private Thread receiveMessageThread;

		// Token: 0x04000010 RID: 16
		private Thread sendMessageThread;

		// Token: 0x04000011 RID: 17
		private bool isShutDown;

		// Token: 0x02000007 RID: 7
		// (Invoke) Token: 0x06000023 RID: 35
		internal delegate void OnMessageReceived(byte[] message, int receivedSize);
	}
}
