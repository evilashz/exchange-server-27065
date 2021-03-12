﻿using System;
using System.Globalization;
using System.Threading;
using Microsoft.Rtc.Collaboration.AudioVideo;
using Microsoft.Rtc.Signaling;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D6E RID: 3438
	internal class RemoteConnectivityTester : BaseUMconnectivityTester
	{
		// Token: 0x060083FB RID: 33787 RVA: 0x0021B9CC File Offset: 0x00219BCC
		internal RemoteConnectivityTester(string hostToCall, int portToCall)
		{
			base.DebugTrace("Inside RemoteConnectivityTester()", new object[0]);
			this.alldtmfString = string.Empty;
			this.proxyAddress = hostToCall;
			this.proxyPort = portToCall;
		}

		// Token: 0x1700290A RID: 10506
		// (get) Token: 0x060083FC RID: 33788 RVA: 0x0021BA1F File Offset: 0x00219C1F
		internal bool AnyDTMFsReceived
		{
			get
			{
				return this.anyDTMFsReceived;
			}
		}

		// Token: 0x1700290B RID: 10507
		// (get) Token: 0x060083FD RID: 33789 RVA: 0x0021BA29 File Offset: 0x00219C29
		internal string AllDTMFsReceived
		{
			get
			{
				return this.alldtmfString;
			}
		}

		// Token: 0x1700290C RID: 10508
		// (get) Token: 0x060083FE RID: 33790 RVA: 0x0021BA31 File Offset: 0x00219C31
		protected override string SignalingHeaderValue
		{
			get
			{
				return "remote";
			}
		}

		// Token: 0x060083FF RID: 33791 RVA: 0x0021BA38 File Offset: 0x00219C38
		protected override void HandleToneReceived(ToneControllerEventArgs e)
		{
			if (!this.notInterestedInDtmf)
			{
				if (!this.anyDTMFsReceived)
				{
					this.anyDTMFsReceived = true;
					this.firstDtmfEvent.Set();
				}
				if (this.runningInMOMMode)
				{
					this.notInterestedInDtmf = true;
					this.dtmfRecvdEvent.Set();
					return;
				}
				if (e.Tone >= 0 && e.Tone <= 15)
				{
					this.HandleTheReceivedDTMF(e.Tone);
					return;
				}
				base.DebugTrace("Received a bad tone :{0}", new object[]
				{
					e.Tone
				});
			}
		}

		// Token: 0x06008400 RID: 33792 RVA: 0x0021BACD File Offset: 0x00219CCD
		internal override bool ExecuteTest(TestParameters testparams)
		{
			Thread.Sleep(1000);
			return this.ExecuteSendDTMFTest(testparams);
		}

		// Token: 0x06008401 RID: 33793 RVA: 0x0021BAE8 File Offset: 0x00219CE8
		protected override void HandleConnectionEstablished()
		{
			base.DebugTrace("Inside RemoteconnectivityTester InitializeDTMFProperties()", new object[0]);
			this.firstDemarcatorRecvd = false;
			this.numofDigitsToReceive = 0;
			this.dtmfString = null;
			this.currNumDigitsRecvd = 0;
			this.notInterestedInDtmf = false;
			this.dtmfRecvdEvent = new ManualResetEvent(false);
			this.firstDtmfEvent = new ManualResetEvent(false);
			this.runningInMOMMode = false;
		}

		// Token: 0x06008402 RID: 33794 RVA: 0x0021BB4A File Offset: 0x00219D4A
		protected override ConnectionContext GetConnectionContext()
		{
			return new ConnectionContext(this.proxyAddress, this.proxyPort);
		}

		// Token: 0x06008403 RID: 33795 RVA: 0x0021BB60 File Offset: 0x00219D60
		private bool SendDiagnosticSequence(TestParameters testparams)
		{
			base.DebugTrace("Inside RemoteconnectivityTester SendDiagnosticSequence()", new object[0]);
			int millisecondsTimeout = 0;
			if (testparams.DiagInitialSilenceInMilisecs != 0)
			{
				millisecondsTimeout = testparams.DiagInitialSilenceInMilisecs;
			}
			if (!string.IsNullOrEmpty(testparams.DiagDtmfSequence))
			{
				this.diagnosticSequence = testparams.DiagDtmfSequence;
			}
			int num = 100;
			if (testparams.DiagInterDtmfGapInMilisecs != 0)
			{
				num = testparams.DiagInterDtmfGapInMilisecs;
			}
			int duration = 100;
			if (testparams.DiagDtmfDurationInMilisecs != 0)
			{
				duration = testparams.DiagDtmfDurationInMilisecs;
			}
			int num2 = 0;
			string[] array = null;
			if (!string.IsNullOrEmpty(testparams.DiagInterDtmfGapDiffInMilisecs.Trim()))
			{
				array = testparams.DiagInterDtmfGapDiffInMilisecs.Split(new char[]
				{
					','
				});
				num2 = array.Length;
			}
			bool result;
			lock (this)
			{
				this.digitError = false;
				try
				{
					int num3 = 0;
					Thread.Sleep(millisecondsTimeout);
					foreach (char c in this.diagnosticSequence)
					{
						base.DebugTrace("Inside RemoteconnectivityTester SendDiagnosticSequenceAdvanced, sending digit {0}", new object[]
						{
							c
						});
						int num4 = num;
						if (num2 == 0)
						{
							if (num3 == 0)
							{
								num4 += 2000;
							}
						}
						else if (num3 < num2)
						{
							int num5 = string.IsNullOrEmpty(array[num3]) ? 0 : Convert.ToInt32(array[num3], CultureInfo.InvariantCulture);
							num4 += ((num5 > 0) ? num5 : 0);
						}
						if (!base.SendDTMFTone(c, duration))
						{
							return false;
						}
						num3++;
						Thread.Sleep(num4);
					}
					result = true;
				}
				catch (Exception ex)
				{
					if (!(ex is RealTimeException) && !(ex is InvalidOperationException))
					{
						throw;
					}
					base.Error = new TUC_SendSequenceError(ex.Message, ex);
					base.ErrorTrace("Inside RemoteconnectivityTester SendDiagnosticSequence, error ={0}", new object[]
					{
						ex.ToString()
					});
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06008404 RID: 33796 RVA: 0x0021BD84 File Offset: 0x00219F84
		private void HandleTheReceivedDTMF(int tone)
		{
			base.DebugTrace("Inside RemoteconnectivityTester HandleTheReceivedDTMF()for tone ={0}", new object[]
			{
				tone
			});
			switch (tone)
			{
			case 10:
				this.GotOneMoreDigit("*");
				this.alldtmfString += "*";
				return;
			case 11:
				this.alldtmfString += "#";
				if (!this.firstDemarcatorRecvd)
				{
					this.firstDemarcatorRecvd = true;
					return;
				}
				if (this.numofDigitsToReceive == 0)
				{
					try
					{
						if (this.dtmfString != null)
						{
							this.numofDigitsToReceive = int.Parse(this.dtmfString, CultureInfo.InvariantCulture);
							base.DebugTrace("Inside RemoteconnectivityTester HandleTheReceivedDTMF(), numofDigitsToReceive={0}", new object[]
							{
								this.numofDigitsToReceive
							});
						}
						else
						{
							this.digitError = true;
							base.ErrorTrace("Inside RemoteconnectivityTester HandleTheReceivedDTMF(), dtmfString=null", new object[0]);
						}
					}
					catch (FormatException ex)
					{
						this.digitError = true;
						base.ErrorTrace("Inside RemoteconnectivityTester HandleTheReceivedDTMF(), trying to parse dtmfString={0} ex={1}", new object[]
						{
							this.dtmfString,
							ex.ToString()
						});
					}
					catch (OverflowException ex2)
					{
						this.digitError = true;
						base.ErrorTrace("Inside RemoteconnectivityTester HandleTheReceivedDTMF(), trying to parse dtmfString={0} ex={1}", new object[]
						{
							this.dtmfString,
							ex2.ToString()
						});
					}
					this.dtmfString = null;
					this.currNumDigitsRecvd = 0;
					return;
				}
				this.GotOneMoreDigit("#");
				return;
			case 12:
				this.GotOneMoreDigit("A");
				this.alldtmfString += "A";
				return;
			case 13:
				this.GotOneMoreDigit("B");
				this.alldtmfString += "B";
				return;
			case 14:
				this.GotOneMoreDigit("C");
				this.alldtmfString += "C";
				return;
			case 15:
				this.GotOneMoreDigit("D");
				this.alldtmfString += "D";
				return;
			default:
				this.GotOneMoreDigit(tone.ToString(CultureInfo.InvariantCulture));
				this.alldtmfString += tone.ToString(CultureInfo.InvariantCulture);
				return;
			}
		}

		// Token: 0x06008405 RID: 33797 RVA: 0x0021BFDC File Offset: 0x0021A1DC
		private void GotOneMoreDigit(string digit)
		{
			base.DebugTrace("Inside RemoteconnectivityTester GotOneMoreDigit", new object[0]);
			if (this.dtmfString != null)
			{
				this.dtmfString += digit;
			}
			else
			{
				this.dtmfString = digit;
			}
			this.currNumDigitsRecvd++;
			if (this.digitError || this.currNumDigitsRecvd == this.numofDigitsToReceive)
			{
				this.notInterestedInDtmf = true;
				this.dtmfRecvdEvent.Set();
			}
		}

		// Token: 0x06008406 RID: 33798 RVA: 0x0021C058 File Offset: 0x0021A258
		private bool AnalyzeDtmfsRecvd()
		{
			base.DebugTrace("Inside RemoteconnectivityTester AnalyzeDtmfsRecvd, dtmfString = {0}", new object[]
			{
				this.dtmfString
			});
			if (this.dtmfString == null)
			{
				return false;
			}
			if (!this.dtmfString.StartsWith(this.diagnosticSequence, StringComparison.InvariantCulture))
			{
				return false;
			}
			this.dtmfString = this.dtmfString.Remove(0, this.diagnosticSequence.Length + 1);
			int num = this.dtmfString.IndexOf("#", StringComparison.InvariantCulture);
			if (num == -1)
			{
				return false;
			}
			base.UmIP = this.dtmfString.Substring(0, num).Replace("*", ".");
			base.DebugTrace("Inside RemoteconnectivityTester AnalyzeDtmfsRecvd, UMIP = {0}", new object[]
			{
				base.UmIP
			});
			base.CurrCalls = this.dtmfString.Substring(num + 1);
			base.DebugTrace("Inside RemoteconnectivityTester AnalyzeDtmfsRecvd, currcalls = {0}", new object[]
			{
				base.CurrCalls
			});
			return true;
		}

		// Token: 0x06008407 RID: 33799 RVA: 0x0021C147 File Offset: 0x0021A347
		private bool ExecuteSendDTMFTest(TestParameters testparams)
		{
			if (base.IsCallGone())
			{
				base.Error = new TUC_RemoteEndDisconnected();
				base.IsCallEstablished = false;
				return false;
			}
			return this.HandleDtmfTest(testparams);
		}

		// Token: 0x06008408 RID: 33800 RVA: 0x0021C16C File Offset: 0x0021A36C
		private bool HandleDtmfTest(TestParameters testparams)
		{
			this.runningInMOMMode = testparams.IsMOMTest;
			return this.SendDiagnosticSequence(testparams) && this.WaitForFirstDTMFResponse() && this.DiagnosticResponseAnalyzer(testparams.IsMOMTest);
		}

		// Token: 0x06008409 RID: 33801 RVA: 0x0021C1A0 File Offset: 0x0021A3A0
		private bool WaitForFirstDTMFResponse()
		{
			base.DebugTrace("Inside WaitForFirstDTMFResponse()", new object[0]);
			int num = WaitHandle.WaitAny(new ManualResetEvent[]
			{
				base.CallEndedEvent,
				this.firstDtmfEvent
			}, TimeSpan.FromSeconds(30.0), false);
			bool flag = false;
			int num2 = num;
			switch (num2)
			{
			case 0:
				base.Error = new TUC_RemoteEndDisconnected();
				break;
			case 1:
				flag = true;
				break;
			default:
				if (num2 == 258)
				{
					base.Error = new TUC_OperationTimedOutInTestUMConnectivityTask(Strings.WaitForFirstDiagnosticResponse, 30.ToString(CultureInfo.InvariantCulture));
				}
				break;
			}
			if (!flag)
			{
				this.notInterestedInDtmf = true;
			}
			return flag;
		}

		// Token: 0x0600840A RID: 33802 RVA: 0x0021C24C File Offset: 0x0021A44C
		private bool DiagnosticResponseAnalyzer(bool isMOMTest)
		{
			base.DebugTrace("Inside DiagnosticResponseAnalyzer()", new object[0]);
			int num = WaitHandle.WaitAny(new ManualResetEvent[]
			{
				base.CallEndedEvent,
				this.dtmfRecvdEvent
			}, TimeSpan.FromSeconds(270.0), false);
			int num2 = num;
			switch (num2)
			{
			case 0:
				base.Error = new TUC_RemoteEndDisconnected();
				return false;
			case 1:
				if (isMOMTest && this.anyDTMFsReceived)
				{
					return true;
				}
				if (!this.AnalyzeDtmfsRecvd())
				{
					base.Error = new TUC_InvalidDTMFSequenceReceived();
					return false;
				}
				return true;
			default:
				if (num2 == 258)
				{
					base.Error = new TUC_OperationTimedOutInTestUMConnectivityTask(Strings.WaitForDiagnosticResponse, 270.ToString(CultureInfo.InvariantCulture));
					return false;
				}
				base.Error = new TUC_OperationTimedOutInTestUMConnectivityTask(Strings.WaitForDiagnosticResponse, 270.ToString(CultureInfo.InvariantCulture));
				return false;
			}
		}

		// Token: 0x04003FE8 RID: 16360
		private string dtmfString;

		// Token: 0x04003FE9 RID: 16361
		private string diagnosticSequence = "ABCD*#0123456789";

		// Token: 0x04003FEA RID: 16362
		private string alldtmfString;

		// Token: 0x04003FEB RID: 16363
		private int numofDigitsToReceive;

		// Token: 0x04003FEC RID: 16364
		private bool firstDemarcatorRecvd;

		// Token: 0x04003FED RID: 16365
		private volatile bool notInterestedInDtmf;

		// Token: 0x04003FEE RID: 16366
		private bool runningInMOMMode;

		// Token: 0x04003FEF RID: 16367
		private volatile bool anyDTMFsReceived;

		// Token: 0x04003FF0 RID: 16368
		private int currNumDigitsRecvd;

		// Token: 0x04003FF1 RID: 16369
		private bool digitError;

		// Token: 0x04003FF2 RID: 16370
		private ManualResetEvent dtmfRecvdEvent;

		// Token: 0x04003FF3 RID: 16371
		private ManualResetEvent firstDtmfEvent;

		// Token: 0x04003FF4 RID: 16372
		private readonly string proxyAddress = string.Empty;

		// Token: 0x04003FF5 RID: 16373
		private readonly int proxyPort;
	}
}
