using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mime;
using System.Threading;
using Microsoft.Rtc.Collaboration;
using Microsoft.Rtc.Signaling;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D69 RID: 3433
	internal class LocalConnectivityTester : BaseUMconnectivityTester
	{
		// Token: 0x060083C7 RID: 33735 RVA: 0x0021B15E File Offset: 0x0021935E
		internal LocalConnectivityTester()
		{
			base.DebugTrace("Inside LocalConnectivityTester()", new object[0]);
		}

		// Token: 0x170028F2 RID: 10482
		// (get) Token: 0x060083C8 RID: 33736 RVA: 0x0021B177 File Offset: 0x00219377
		internal bool AcceptingCalls
		{
			get
			{
				return this.acceptingCalls != null && this.acceptingCalls.Equals("1");
			}
		}

		// Token: 0x170028F3 RID: 10483
		// (get) Token: 0x060083C9 RID: 33737 RVA: 0x0021B196 File Offset: 0x00219396
		internal string TotalQueueLength
		{
			get
			{
				return this.totalQueueLength;
			}
		}

		// Token: 0x060083CA RID: 33738 RVA: 0x0021B19E File Offset: 0x0021939E
		internal override bool ExecuteTest(TestParameters testparams)
		{
			base.DebugTrace("Inside LocalConnectivityTester ExecuteTest()", new object[0]);
			return this.ExecuteSIPINFOTest(testparams);
		}

		// Token: 0x060083CB RID: 33739 RVA: 0x0021B1C0 File Offset: 0x002193C0
		protected static bool IsDiagnosticHeaderPresent(MessageReceivedEventArgs e)
		{
			foreach (SignalingHeader signalingHeader in e.RequestData.SignalingHeaders)
			{
				if (signalingHeader.Name.Equals("UMTUCFirstResponse"))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060083CC RID: 33740 RVA: 0x0021B224 File Offset: 0x00219424
		protected override void HandleMessageReceived(MessageReceivedEventArgs e)
		{
			base.DebugTrace("Inside LocalConnectivityTester HandleMessageReceived", new object[0]);
			if (e.MessageType != 1 || !e.HasTextBody || !LocalConnectivityTester.IsDiagnosticHeaderPresent(e))
			{
				return;
			}
			this.InfoHandling(e);
		}

		// Token: 0x060083CD RID: 33741 RVA: 0x0021B258 File Offset: 0x00219458
		protected virtual bool SendLocalLoopInfoMesg(string dpname)
		{
			base.DebugTrace("Inside LocalConnectivityTester SendLocalLoopInfoMesg()", new object[0]);
			return this.SendInfo("UM Operation Check", null);
		}

		// Token: 0x060083CE RID: 33742 RVA: 0x0021B278 File Offset: 0x00219478
		protected bool SendInfo(string data, List<SignalingHeader> headers)
		{
			base.DebugTrace("Inside LocalConnectivityTester SendInfo()", new object[0]);
			bool result;
			lock (this)
			{
				try
				{
					if (base.IsCallGone())
					{
						base.Error = new TUC_RemoteEndDisconnected();
						result = false;
					}
					else
					{
						this.sipInfoEvent = new ManualResetEvent(false);
						ContentType contentType = new ContentType("text/plain");
						ContentDescription contentDescription = new ContentDescription(contentType, data);
						CallSendMessageRequestOptions callSendMessageRequestOptions = new CallSendMessageRequestOptions();
						if (headers != null)
						{
							CollectionExtensions.AddRange<SignalingHeader>(callSendMessageRequestOptions.Headers, headers);
						}
						base.AudioCall.EndSendMessage(base.AudioCall.BeginSendMessage(1, contentDescription, callSendMessageRequestOptions, null, null));
						base.DebugTrace("Inside LocalConnectivityTester SendInfo: sent SIPINFO", new object[0]);
						result = true;
					}
				}
				catch (Exception ex)
				{
					if (!(ex is RealTimeException) && !(ex is InvalidOperationException))
					{
						throw;
					}
					base.Error = new TUC_SendSequenceError(ex.Message, ex);
					base.ErrorTrace("Inside LocalConnectivityTester SendInfo, error ={0}", new object[]
					{
						ex.ToString()
					});
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060083CF RID: 33743 RVA: 0x0021B398 File Offset: 0x00219598
		protected void InfoHandling(MessageReceivedEventArgs e)
		{
			string textBody = e.TextBody;
			base.DebugTrace("Inside LocalConnectivityTester InfoHandling, text recvd = {0}", new object[]
			{
				textBody
			});
			string[] array = textBody.Split(new char[]
			{
				':'
			});
			if (array.Length != 4 || !array[0].Equals("OK"))
			{
				return;
			}
			base.CurrCalls = array[1];
			this.totalQueueLength = array[2];
			this.acceptingCalls = array[3];
			this.sipInfoEvent.Set();
		}

		// Token: 0x060083D0 RID: 33744 RVA: 0x0021B414 File Offset: 0x00219614
		protected bool ExecuteSIPINFOTest(TestParameters testparams)
		{
			base.DebugTrace("Inside LocalConnectivityTester ExecuteSIPINFOTest()", new object[0]);
			if (base.IsCallGone())
			{
				base.Error = new TUC_RemoteEndDisconnected();
				return false;
			}
			if (!this.SendLocalLoopInfoMesg(testparams.DpName))
			{
				return false;
			}
			int num = WaitHandle.WaitAny(new ManualResetEvent[]
			{
				base.CallEndedEvent,
				this.sipInfoEvent
			}, 20000, false);
			int num2 = num;
			switch (num2)
			{
			case 0:
				base.Error = new TUC_RemoteEndDisconnected();
				return false;
			case 1:
				return true;
			default:
				if (num2 == 258)
				{
					base.Error = new TUC_OperationTimedOutInTestUMConnectivityTask(Strings.WaitForDiagnosticResponse, 20.ToString(CultureInfo.InvariantCulture));
					return false;
				}
				base.Error = new TUC_OperationTimedOutInTestUMConnectivityTask(Strings.WaitForDiagnosticResponse, 20.ToString(CultureInfo.InvariantCulture));
				return false;
			}
		}

		// Token: 0x04003FDE RID: 16350
		private ManualResetEvent sipInfoEvent;

		// Token: 0x04003FDF RID: 16351
		private string totalQueueLength;

		// Token: 0x04003FE0 RID: 16352
		private string acceptingCalls;
	}
}
