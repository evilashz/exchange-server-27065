using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Rtc.Signaling;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D6D RID: 3437
	internal class LocalTUILogonConnectivityTester : LocalConnectivityTester
	{
		// Token: 0x060083F3 RID: 33779 RVA: 0x0021B737 File Offset: 0x00219937
		internal LocalTUILogonConnectivityTester()
		{
			base.DebugTrace("Inside LocalTUILogonConnectivityTester()", new object[0]);
			this.sipMessagesQueue = new Queue();
			this.sipMessageQueuedEvent = new ManualResetEvent(false);
		}

		// Token: 0x17002908 RID: 10504
		// (get) Token: 0x060083F4 RID: 33780 RVA: 0x0021B767 File Offset: 0x00219967
		internal ManualResetEvent SIPMessageQueuedEvent
		{
			get
			{
				return this.sipMessageQueuedEvent;
			}
		}

		// Token: 0x17002909 RID: 10505
		// (get) Token: 0x060083F5 RID: 33781 RVA: 0x0021B76F File Offset: 0x0021996F
		internal Queue SIPMessageQueue
		{
			get
			{
				return this.sipMessagesQueue;
			}
		}

		// Token: 0x060083F6 RID: 33782 RVA: 0x0021B778 File Offset: 0x00219978
		internal override bool ExecuteTest(TestParameters testparams)
		{
			base.DebugTrace("Inside LocalTUILogonConnectivityTester ExecuteTest()", new object[0]);
			if (!base.ExecuteSIPINFOTest(testparams))
			{
				return false;
			}
			ScenarioTestingHelperMethods.ServiceTestScenarios[] tests = new ScenarioTestingHelperMethods.ServiceTestScenarios[]
			{
				new ScenarioTestingHelperMethods.WaitForStates(Strings.PilotNumberState, new string[]
				{
					"0102"
				}),
				new ScenarioTestingHelperMethods.SendDigits(testparams.Phone),
				new ScenarioTestingHelperMethods.WaitForStates(Strings.PINEnterState, new string[]
				{
					"0104"
				}),
				new ScenarioTestingHelperMethods.SendDigits(testparams.PIN + "#"),
				new ScenarioTestingHelperMethods.WaitForStates(Strings.SuccessfulLogonState, new List<string>(new string[]
				{
					"0170",
					"0600",
					"0MainMenuQA"
				}), Strings.LogonError, new List<string>(new string[]
				{
					"0140",
					"0141",
					"0142"
				}))
			};
			return this.ExecuteScenarioTests(tests);
		}

		// Token: 0x060083F7 RID: 33783 RVA: 0x0021B879 File Offset: 0x00219A79
		protected override void HandleMessageReceived(MessageReceivedEventArgs e)
		{
			if (e.MessageType != 1 || !e.HasTextBody)
			{
				return;
			}
			if (LocalConnectivityTester.IsDiagnosticHeaderPresent(e))
			{
				base.InfoHandling(e);
				return;
			}
			this.HandleStateTransitionInfoMsgs(e);
		}

		// Token: 0x060083F8 RID: 33784 RVA: 0x0021B8A4 File Offset: 0x00219AA4
		protected override bool SendLocalLoopInfoMesg(string dpname)
		{
			base.DebugTrace("Inside LocalTUILogonConnectivityTester SendTUILocalLoopInfoMesg()", new object[0]);
			List<SignalingHeader> list = new List<SignalingHeader>();
			SignalingHeader item = new SignalingHeader("UMDialPlan", dpname);
			list.Add(item);
			return base.SendInfo("UM Operation Check", list);
		}

		// Token: 0x060083F9 RID: 33785 RVA: 0x0021B8E8 File Offset: 0x00219AE8
		private void HandleStateTransitionInfoMsgs(MessageReceivedEventArgs e)
		{
			base.DebugTrace("Inside LocalTUILogonConnectivityTester HandleStateTransitionInfoMsgs(), received ={0}", new object[]
			{
				e.TextBody
			});
			lock (this.sipMessagesQueue.SyncRoot)
			{
				base.DebugTrace("Inside LocalTUILogonConnectivityTester HandleStateTransitionInfoMsgs() enqeuing the mesg", new object[0]);
				if (this.sipMessagesQueue.Count == 0)
				{
					this.sipMessageQueuedEvent.Set();
				}
				this.sipMessagesQueue.Enqueue(e.TextBody);
			}
		}

		// Token: 0x060083FA RID: 33786 RVA: 0x0021B980 File Offset: 0x00219B80
		private bool ExecuteScenarioTests(ScenarioTestingHelperMethods.ServiceTestScenarios[] tests)
		{
			base.DebugTrace("Inside LocalTUILogonConnectivityTester ExecuteScenarioTests()", new object[0]);
			foreach (ScenarioTestingHelperMethods.ServiceTestScenarios serviceTestScenarios in tests)
			{
				if (!serviceTestScenarios.Execute(this))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04003FE6 RID: 16358
		private Queue sipMessagesQueue;

		// Token: 0x04003FE7 RID: 16359
		private ManualResetEvent sipMessageQueuedEvent;
	}
}
