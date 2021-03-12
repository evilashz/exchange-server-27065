using System;
using System.Globalization;
using System.Net.Sockets;
using System.Text;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.UMWorkerProcess
{
	// Token: 0x02000004 RID: 4
	internal sealed class ControlObject
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00003C3C File Offset: 0x00001E3C
		internal ControlObject(Socket socket, WorkerProcess workerProcess)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "Creating  ControlObject in workerprocess", new object[0]);
			this.connection = new NetworkConnection(socket, 4096);
			this.connection.MaxLineLength = 4096;
			this.connection.Timeout = 31536000;
			this.wp = workerProcess;
			this.portIsConnected = true;
			this.StartReadLine();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00003CAF File Offset: 0x00001EAF
		internal bool IsConnected
		{
			get
			{
				return this.portIsConnected;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00003CB8 File Offset: 0x00001EB8
		public void Shutdown()
		{
			NetworkConnection networkConnection = this.connection;
			if (networkConnection != null)
			{
				networkConnection.Shutdown();
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00003CD8 File Offset: 0x00001ED8
		private static void ReadLineComplete(IAsyncResult asyncResult)
		{
			ControlObject controlObject = (ControlObject)asyncResult.AsyncState;
			byte[] buffer;
			int offset;
			int num;
			object obj;
			controlObject.connection.EndReadLine(asyncResult, out buffer, out offset, out num, out obj);
			if (obj != null || num == 0)
			{
				controlObject.Disconnect();
				return;
			}
			controlObject.CommandReceived(buffer, offset, num);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00003D20 File Offset: 0x00001F20
		private static void SendHeartBeatComplete(IAsyncResult asyncResult)
		{
			ControlObject controlObject = (ControlObject)asyncResult.AsyncState;
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Sent SendHeartBeat", new object[0]);
			object obj;
			controlObject.connection.EndWrite(asyncResult, out obj);
			if (obj != null)
			{
				controlObject.Disconnect();
				return;
			}
			controlObject.StartReadLine();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003D74 File Offset: 0x00001F74
		private void CommandReceived(byte[] buffer, int offset, int size)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "In CommandReceived in workerprocess, command recvd ={0}", new object[]
			{
				Encoding.ASCII.GetString(buffer, offset, size)
			});
			char c = (char)buffer[offset];
			if (c <= 'H')
			{
				if (c == 'A')
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Received Activate Request in worker proecss", new object[0]);
					this.wp.Activate();
					this.StartReadLine();
					return;
				}
				if (c == 'H')
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Received Heartbeat Request in worker proecss", new object[0]);
					this.SendHeartBeat();
					return;
				}
			}
			else
			{
				switch (c)
				{
				case 'R':
					CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Received retire Request in worker proecss", new object[0]);
					this.Disconnect();
					return;
				case 'S':
					break;
				case 'T':
					CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Received Watson Request in worker proecss", new object[0]);
					ExceptionHandling.SendWatsonWithExtraData(new WatsoningDueToTimeout(), false);
					this.StartReadLine();
					return;
				default:
					if (c == 'W')
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Received retire with Watson Request in worker proecss", new object[0]);
						this.Disconnect(true);
						return;
					}
					break;
				}
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Received some garbage in worker process", new object[0]);
			this.StartReadLine();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00003ECA File Offset: 0x000020CA
		private void Disconnect()
		{
			this.Disconnect(false);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003ED4 File Offset: 0x000020D4
		private void Disconnect(bool requestWatson)
		{
			this.portIsConnected = false;
			this.connection.Dispose();
			this.connection = null;
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "In ControlObject.Disconnect", new object[0]);
			this.wp.Retire(requestWatson);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003F24 File Offset: 0x00002124
		private void SendHeartBeat()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "In SendHeartBeat", new object[0]);
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "SendHeartbeat: ArePerfCountersEnabled value ={0}", new object[]
			{
				UmServiceGlobals.ArePerfCountersEnabled
			});
			string text;
			if (!UmServiceGlobals.ArePerfCountersEnabled)
			{
				text = "HR 0";
			}
			else
			{
				text = "HR " + AvailabilityCounters.TotalWorkerProcessCallCount.RawValue.ToString(CultureInfo.InvariantCulture);
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "SendHeartbeat: Response value ={0}", new object[]
			{
				text
			});
			byte[] bytes = Encoding.UTF8.GetBytes(text);
			this.connection.BeginWrite(bytes, 0, bytes.Length, ControlObject.sendHeartBeatComplete, this);
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "SendHeartbeat: BeginWrite Response", new object[0]);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00004009 File Offset: 0x00002209
		private void StartReadLine()
		{
			this.connection.BeginReadLine(ControlObject.readLineComplete, this);
		}

		// Token: 0x04000014 RID: 20
		private static readonly AsyncCallback readLineComplete = new AsyncCallback(ControlObject.ReadLineComplete);

		// Token: 0x04000015 RID: 21
		private static readonly AsyncCallback sendHeartBeatComplete = new AsyncCallback(ControlObject.SendHeartBeatComplete);

		// Token: 0x04000016 RID: 22
		private NetworkConnection connection;

		// Token: 0x04000017 RID: 23
		private bool portIsConnected;

		// Token: 0x04000018 RID: 24
		private WorkerProcess wp;
	}
}
