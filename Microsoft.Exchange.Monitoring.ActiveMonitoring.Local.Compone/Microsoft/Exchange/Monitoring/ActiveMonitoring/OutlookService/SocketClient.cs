using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Office.Outlook;
using Microsoft.Office.Outlook.Network;
using Microsoft.Office.Outlook.V1.Mail;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.OutlookService
{
	// Token: 0x0200025D RID: 605
	public class SocketClient
	{
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x0007165C File Offset: 0x0006F85C
		// (set) Token: 0x060010F7 RID: 4343 RVA: 0x00071664 File Offset: 0x0006F864
		public string ExtraInfo { get; set; }

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x060010F8 RID: 4344 RVA: 0x0007166D File Offset: 0x0006F86D
		// (set) Token: 0x060010F9 RID: 4345 RVA: 0x00071675 File Offset: 0x0006F875
		public ServiceApiClient Client { get; set; }

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x0007167E File Offset: 0x0006F87E
		// (set) Token: 0x060010FB RID: 4347 RVA: 0x00071686 File Offset: 0x0006F886
		public ManualResetEvent DoneEvent { get; set; }

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x0007168F File Offset: 0x0006F88F
		// (set) Token: 0x060010FD RID: 4349 RVA: 0x00071697 File Offset: 0x0006F897
		public Semaphore HandlingResponseSemaphore { get; set; }

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x000716A0 File Offset: 0x0006F8A0
		// (set) Token: 0x060010FF RID: 4351 RVA: 0x000716A8 File Offset: 0x0006F8A8
		public SocketResponseType EventResponseType { get; set; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06001100 RID: 4352 RVA: 0x000716B1 File Offset: 0x0006F8B1
		// (set) Token: 0x06001101 RID: 4353 RVA: 0x000716B9 File Offset: 0x0006F8B9
		public string UnexpectedEventResponseMessage { get; set; }

		// Token: 0x06001102 RID: 4354 RVA: 0x000716C2 File Offset: 0x0006F8C2
		public SocketClient(ServiceApiClient client)
		{
			this.Client = client;
			this.DoneEvent = new ManualResetEvent(false);
			this.HandlingResponseSemaphore = new Semaphore(1, 1);
			this.ResetDefaultCallbacks();
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00071700 File Offset: 0x0006F900
		public TResponse PostSynchronous<TRequest, TResponse>(TRequest request, TimeSpan timeout) where TRequest : RequestBase where TResponse : ResponseBase
		{
			this.DoneEvent.Reset();
			this.Client.PostAsync<TRequest>(request, 0UL);
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			do
			{
				try
				{
					this.fSignaled = this.DoneEvent.WaitOne(timeout);
					this.DoneEvent.Reset();
					if (!this.IsEventExpected())
					{
						return default(TResponse);
					}
					TResponse tresponse = (TResponse)((object)this.EventResponse);
					if (tresponse != null || !this.fSignaled)
					{
						return tresponse;
					}
				}
				finally
				{
					this.HandlingResponseSemaphore.Release();
				}
			}
			while (stopwatch.Elapsed < timeout);
			return default(TResponse);
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x0007180E File Offset: 0x0006FA0E
		protected void SetDefaultCallback<TResponse>() where TResponse : ResponseBase, new()
		{
			this.Client.OnAsyncResponse<TResponse>(delegate(TResponse response)
			{
				this.AppendExtraInfo("Received a response");
				this.HandlingResponseSemaphore.WaitOne();
				this.EventResponse = response;
				this.EventResponseType = 0;
				this.UnexpectedEventResponseMessage = "";
				this.DoneEvent.Set();
			});
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00071990 File Offset: 0x0006FB90
		protected void ResetDefaultCallbacksBase()
		{
			this.Client.OnAsyncDisconnect(delegate(SocketResponse response)
			{
				this.AppendExtraInfo("Disconnecting");
				if (response.StatusMessage != "Goodbye: Expired" && response.StatusMessage != "OK")
				{
					this.HandlingResponseSemaphore.WaitOne();
					this.AppendExtraInfo("DisconnectStatus:" + response.StatusMessage);
					this.EventResponseType = response.ResponseType;
					this.UnexpectedEventResponseMessage = response.StatusMessage;
					this.DoneEvent.Set();
				}
			});
			this.Client.OnUnknownVersionError(delegate(List<uint> knownVersions)
			{
				this.HandlingResponseSemaphore.WaitOne();
				this.AppendExtraInfo("UnknownVersionError=" + this.UnexpectedEventResponseMessage);
				this.EventResponseType = 3;
				this.UnexpectedEventResponseMessage = "Received OnUnknownVersionError SocketResponse";
				this.DoneEvent.Set();
			});
			this.Client.OnError(delegate(SocketResponse response)
			{
				this.HandlingResponseSemaphore.WaitOne();
				this.AppendExtraInfo("ResponseType=" + response.ResponseType.ToString());
				this.AppendExtraInfo("Error=" + response.StatusCode.ToString());
				this.AppendExtraInfo("Message=" + response.StatusMessage);
				this.EventResponseType = response.ResponseType;
				this.UnexpectedEventResponseMessage = response.StatusMessage;
				this.DoneEvent.Set();
			});
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x000719E4 File Offset: 0x0006FBE4
		protected bool IsEventExpected()
		{
			bool result = true;
			switch (this.EventResponseType)
			{
			case 0:
			case 1:
			case 2:
			case 5:
				break;
			case 3:
				this.AppendExtraInfo("UnexpectedError=\r\n[start]\r\n" + this.UnexpectedEventResponseMessage + "\r\n[end]");
				result = false;
				break;
			case 4:
				this.AppendExtraInfo("UnexpectedDisconnect=\r\n[start]\r\n" + this.UnexpectedEventResponseMessage + "\r\n[end]");
				result = false;
				break;
			default:
				result = false;
				this.AppendExtraInfo("UnexpectedEvent=Unknown SocketResponseType in SocketResponse");
				break;
			}
			return result;
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x00071A68 File Offset: 0x0006FC68
		protected void CheckResponseErrors(string command, ResponseBase response)
		{
			if (!this.fSignaled)
			{
				this.AppendExtraInfo("ProbeFailure=Probe Timed Out");
				this.ExecutionSuccess = false;
				return;
			}
			if (response == null)
			{
				this.AppendExtraInfo("ProbeFailure:Probe Failed Empty Response Received");
				this.ExecutionSuccess = false;
				return;
			}
			if (command == null || command == "PingCommand" || !(command == "BeginSyncCommand"))
			{
				PingResponse pingResponse = (PingResponse)response;
				if (string.IsNullOrWhiteSpace(pingResponse.BuildNumber))
				{
					this.AppendExtraInfo("ProbeFailure= Unexpected Ping response failed to get service build version");
					this.ExecutionSuccess = false;
					return;
				}
			}
			else
			{
				BeginSyncResponse beginSyncResponse = (BeginSyncResponse)response;
				ResponseCode errorCode = beginSyncResponse.ErrorCode;
				if (errorCode != null)
				{
					this.AppendExtraInfo(string.Format("ProbeFailure= Server returned {0} response", beginSyncResponse.ErrorCode.ToString()));
					this.ExecutionSuccess = false;
				}
			}
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00071B24 File Offset: 0x0006FD24
		protected void ResetDefaultCallbacks()
		{
			this.ResetDefaultCallbacksBase();
			this.SetDefaultCallback<PingResponse>();
			this.SetDefaultCallback<BeginSyncResponse>();
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00071B38 File Offset: 0x0006FD38
		public PingResponse Ping(PingRequest pingRequest, TimeSpan timeout)
		{
			PingResponse pingResponse = this.PostSynchronous<PingRequest, PingResponse>(pingRequest, timeout);
			this.CheckResponseErrors("PingCommand", pingResponse);
			return pingResponse;
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00071B5C File Offset: 0x0006FD5C
		public BeginSyncResponse Sync(BeginSyncRequest syncRequest, TimeSpan timeout)
		{
			BeginSyncResponse beginSyncResponse = this.PostSynchronous<BeginSyncRequest, BeginSyncResponse>(syncRequest, timeout);
			this.CheckResponseErrors("BeginSyncCommand", beginSyncResponse);
			return beginSyncResponse;
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x00071B7F File Offset: 0x0006FD7F
		private void AppendExtraInfo(string str)
		{
			if (string.IsNullOrWhiteSpace(this.ExtraInfo))
			{
				this.ExtraInfo = str;
				return;
			}
			this.ExtraInfo = string.Format("{0};{1}", this.ExtraInfo, str);
		}

		// Token: 0x04000CD0 RID: 3280
		public bool ExecutionSuccess = true;

		// Token: 0x04000CD1 RID: 3281
		public ResponseBase EventResponse;

		// Token: 0x04000CD2 RID: 3282
		private bool fSignaled = true;
	}
}
