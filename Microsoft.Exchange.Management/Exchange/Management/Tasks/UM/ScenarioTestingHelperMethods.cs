using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Rtc.Signaling;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D6F RID: 3439
	internal class ScenarioTestingHelperMethods
	{
		// Token: 0x02000D70 RID: 3440
		internal abstract class ServiceTestScenarios
		{
			// Token: 0x0600840C RID: 33804
			internal abstract bool Execute(LocalTUILogonConnectivityTester umConn);
		}

		// Token: 0x02000D71 RID: 3441
		internal class SendDigits : ScenarioTestingHelperMethods.ServiceTestScenarios
		{
			// Token: 0x0600840E RID: 33806 RVA: 0x0021C346 File Offset: 0x0021A546
			internal SendDigits(string digits)
			{
				this.digitsToSend = digits;
			}

			// Token: 0x0600840F RID: 33807 RVA: 0x0021C358 File Offset: 0x0021A558
			internal override bool Execute(LocalTUILogonConnectivityTester umConn)
			{
				umConn.DebugTrace("Inside SendDigits Execute()", new object[0]);
				if (umConn.IsCallGone())
				{
					umConn.Error = new TUC_RemoteEndDisconnected();
					return false;
				}
				if (string.IsNullOrEmpty(this.digitsToSend))
				{
					return true;
				}
				bool result;
				try
				{
					foreach (char c in this.digitsToSend)
					{
						umConn.DebugTrace("Inside SendDigits , sending digit {0}", new object[]
						{
							c
						});
						if (!umConn.SendDTMFTone(c))
						{
							return false;
						}
						Thread.Sleep(500);
					}
					result = true;
				}
				catch (Exception ex)
				{
					if (!(ex is RealTimeException) && !(ex is InvalidOperationException))
					{
						throw;
					}
					umConn.Error = new TUC_SendSequenceError(ex.Message, ex);
					umConn.ErrorTrace("Inside SendDigits Execute, error ={0}", new object[]
					{
						ex
					});
					result = false;
				}
				return result;
			}

			// Token: 0x04003FF6 RID: 16374
			private readonly string digitsToSend;
		}

		// Token: 0x02000D72 RID: 3442
		internal class WaitForStates : ScenarioTestingHelperMethods.ServiceTestScenarios
		{
			// Token: 0x06008410 RID: 33808 RVA: 0x0021C450 File Offset: 0x0021A650
			internal WaitForStates(double timeoutInsecs, LocalizedString stateName, params string[] list)
			{
				if (list != null)
				{
					this.state = new List<string>();
					for (int i = 0; i < list.Length; i++)
					{
						this.state.Add(list[i]);
					}
				}
				this.secsTimeout = timeoutInsecs;
				this.stateName = stateName;
			}

			// Token: 0x06008411 RID: 33809 RVA: 0x0021C49B File Offset: 0x0021A69B
			internal WaitForStates(LocalizedString stateName, params string[] list) : this(60.0, stateName, list)
			{
			}

			// Token: 0x06008412 RID: 33810 RVA: 0x0021C4AE File Offset: 0x0021A6AE
			internal WaitForStates(double timeoutInsecs, LocalizedString stateName, List<string> stateToSearch, LocalizedString errorMesg, List<string> stateToExclude)
			{
				this.state = stateToSearch;
				this.secsTimeout = timeoutInsecs;
				this.stateName = stateName;
				this.errorMesg = errorMesg;
				this.statesToExclude = stateToExclude;
				this.isPreemptable = (stateToExclude != null);
			}

			// Token: 0x06008413 RID: 33811 RVA: 0x0021C4E9 File Offset: 0x0021A6E9
			internal WaitForStates(LocalizedString stateName, List<string> stateToSearch, LocalizedString errorMesg, List<string> stateToExclude) : this(60.0, stateName, stateToSearch, errorMesg, stateToExclude)
			{
			}

			// Token: 0x06008414 RID: 33812 RVA: 0x0021C500 File Offset: 0x0021A700
			internal override bool Execute(LocalTUILogonConnectivityTester umConnection)
			{
				this.umConn = umConnection;
				this.umConn.DebugTrace("Inside WaitForState Execute()", new object[0]);
				if (this.umConn.IsCallGone())
				{
					this.umConn.Error = new TUC_RemoteEndDisconnected();
					return false;
				}
				if (this.state == null)
				{
					return true;
				}
				if (this.secsTimeout <= 0.0)
				{
					this.umConn.Error = new TUC_OperationTimedOutInTestUMConnectivityTask(this.stateName, this.secsTimeout.ToString(CultureInfo.InvariantCulture));
					return false;
				}
				ManualResetEvent[] waitHandles = new ManualResetEvent[]
				{
					this.umConn.CallEndedEvent,
					this.umConn.SIPMessageQueuedEvent
				};
				double num = this.secsTimeout * 1000.0;
				bool flag = false;
				bool flag2 = false;
				while (!flag && !flag2 && num > 0.0)
				{
					ExDateTime utcNow = ExDateTime.UtcNow;
					int num2 = WaitHandle.WaitAny(waitHandles, TimeSpan.FromMilliseconds(num), false);
					num -= ExDateTime.UtcNow.Subtract(utcNow).TotalMilliseconds;
					int num3 = num2;
					switch (num3)
					{
					case 0:
						this.umConn.Error = new TUC_RemoteEndDisconnected();
						flag = true;
						break;
					case 1:
						switch (this.GotMyExpectedMessage(this.state, this.isPreemptable, this.statesToExclude))
						{
						case ScenarioTestingHelperMethods.WaitForStates.MatchedState.Expected:
							this.umConn.DebugTrace("Inside WaitForState Got one of the expected states", new object[0]);
							flag2 = true;
							break;
						case ScenarioTestingHelperMethods.WaitForStates.MatchedState.Excluded:
							this.umConn.Error = new TUC_OperationFailed(this.errorMesg);
							flag = true;
							break;
						default:
							if (num <= 0.0)
							{
								this.umConn.Error = new TUC_OperationTimedOutInTestUMConnectivityTask(this.stateName, this.secsTimeout.ToString(CultureInfo.InvariantCulture));
								flag = true;
							}
							else
							{
								flag = false;
							}
							break;
						}
						break;
					default:
						if (num3 == 258)
						{
							this.umConn.Error = new TUC_OperationTimedOutInTestUMConnectivityTask(this.stateName, this.secsTimeout.ToString(CultureInfo.InvariantCulture));
							flag = true;
						}
						else
						{
							this.umConn.Error = new TUC_OperationTimedOutInTestUMConnectivityTask(this.stateName, this.secsTimeout.ToString(CultureInfo.InvariantCulture));
							flag = true;
						}
						break;
					}
				}
				return flag2;
			}

			// Token: 0x06008415 RID: 33813 RVA: 0x0021C768 File Offset: 0x0021A968
			private ScenarioTestingHelperMethods.WaitForStates.MatchedState GotMyExpectedMessage(List<string> state, bool matchInExclusionList, List<string> exclusionList)
			{
				this.umConn.DebugTrace("Inside WaitForState GotMyExpectedMessage()", new object[0]);
				ScenarioTestingHelperMethods.WaitForStates.MatchedState result = ScenarioTestingHelperMethods.WaitForStates.MatchedState.NotMatched;
				bool flag = false;
				lock (this.umConn.SIPMessageQueue.SyncRoot)
				{
					while (!flag && this.umConn.SIPMessageQueue.Count > 0)
					{
						string mesg = (string)this.umConn.SIPMessageQueue.Dequeue();
						flag = this.IsThisMesgWhatIWant(mesg, state);
						if (flag)
						{
							result = ScenarioTestingHelperMethods.WaitForStates.MatchedState.Expected;
						}
						else if (matchInExclusionList)
						{
							flag = this.IsThisMesgWhatIWant(mesg, exclusionList);
							result = (flag ? ScenarioTestingHelperMethods.WaitForStates.MatchedState.Excluded : ScenarioTestingHelperMethods.WaitForStates.MatchedState.NotMatched);
						}
					}
					if (this.umConn.SIPMessageQueue.Count == 0)
					{
						this.umConn.SIPMessageQueuedEvent.Reset();
					}
				}
				return result;
			}

			// Token: 0x06008416 RID: 33814 RVA: 0x0021C840 File Offset: 0x0021AA40
			private bool IsThisMesgWhatIWant(string mesg, List<string> state)
			{
				this.umConn.DebugTrace("Inside WaitForState IsThisMesgWhatIWant() with mesg = {0}", new object[]
				{
					mesg
				});
				if (mesg == null)
				{
					return false;
				}
				mesg = mesg.Trim();
				foreach (string str in state)
				{
					string text = "Call-State: " + str;
					this.umConn.DebugTrace("Inside WaitForState Looking for a match of {0} in {1}", new object[]
					{
						text,
						mesg
					});
					if (mesg.Contains(text))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x04003FF7 RID: 16375
			private const string CallStatePrefix = "Call-State: ";

			// Token: 0x04003FF8 RID: 16376
			private List<string> state;

			// Token: 0x04003FF9 RID: 16377
			private List<string> statesToExclude;

			// Token: 0x04003FFA RID: 16378
			private readonly double secsTimeout;

			// Token: 0x04003FFB RID: 16379
			private LocalizedString stateName;

			// Token: 0x04003FFC RID: 16380
			private LocalTUILogonConnectivityTester umConn;

			// Token: 0x04003FFD RID: 16381
			private readonly bool isPreemptable;

			// Token: 0x04003FFE RID: 16382
			private LocalizedString errorMesg;

			// Token: 0x02000D73 RID: 3443
			private enum MatchedState
			{
				// Token: 0x04004000 RID: 16384
				Expected,
				// Token: 0x04004001 RID: 16385
				Excluded,
				// Token: 0x04004002 RID: 16386
				NotMatched
			}
		}
	}
}
