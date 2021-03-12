using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x02000039 RID: 57
	internal class ToneAccumulator
	{
		// Token: 0x0600027C RID: 636 RVA: 0x0000AA42 File Offset: 0x00008C42
		public ToneAccumulator()
		{
			this.accumulator = new List<byte>(16);
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000AA64 File Offset: 0x00008C64
		public bool IsEmpty
		{
			get
			{
				bool result;
				lock (this.locker)
				{
					result = (0 == this.accumulator.Count);
				}
				return result;
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000AAB0 File Offset: 0x00008CB0
		public void Add(byte b)
		{
			lock (this.locker)
			{
				this.accumulator.Add(b);
				CallIdTracer.TraceDebug(ExTraceGlobals.UCMATracer, this, "Accumulator count increased to = '{0}'", new object[]
				{
					this.accumulator.Count
				});
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000AB24 File Offset: 0x00008D24
		public void Clear()
		{
			lock (this.locker)
			{
				this.accumulator.Clear();
				CallIdTracer.TraceDebug(ExTraceGlobals.UCMATracer, this, "Accumulator cleared.", new object[0]);
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000AB80 File Offset: 0x00008D80
		public void RebufferDigits(byte[] dtmfDigits)
		{
			if (dtmfDigits != null)
			{
				lock (this.locker)
				{
					this.accumulator.InsertRange(0, dtmfDigits);
					CallIdTracer.TraceDebug(ExTraceGlobals.UCMATracer, this, "Rebuffered input, accumulator count increased to = '{0}'", new object[]
					{
						this.accumulator.Count
					});
				}
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000ABF8 File Offset: 0x00008DF8
		public byte[] ConsumeAccumulatedDigits(int minDigits, int maxDigits, StopPatterns stopPatterns)
		{
			stopPatterns = (stopPatterns ?? StopPatterns.Empty);
			byte[] result;
			lock (this.locker)
			{
				int i = Math.Min(maxDigits, this.accumulator.Count);
				List<byte> list = null;
				int num = 0;
				int num2 = 0;
				while (i > 0)
				{
					list = this.accumulator.GetRange(0, i);
					ToneAccumulator.FindStopPatternMatches(list, stopPatterns, out num, out num2);
					if (num2 > 0 || (stopPatterns.ContainsAnyKey && StopPatterns.IsAnyKeyInput(new ReadOnlyCollection<byte>(list))))
					{
						break;
					}
					i--;
				}
				if (num2 == 0)
				{
					i = Math.Min(maxDigits, this.accumulator.Count);
					list = this.accumulator.GetRange(0, i);
				}
				this.accumulator.RemoveRange(0, i);
				CallIdTracer.TraceDebug(ExTraceGlobals.UCMATracer, this, "Accumulator removed '{0}' digits.  '{1}' remain.", new object[]
				{
					i,
					this.accumulator.Count
				});
				result = list.ToArray();
			}
			return result;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000AD08 File Offset: 0x00008F08
		public InputState ComputeInputState(int minDigits, int maxDigits, StopPatterns stopPatterns, string stopTones)
		{
			InputState result;
			lock (this.locker)
			{
				InputState inputState;
				if (maxDigits == 0)
				{
					inputState = InputState.NotAllowed;
				}
				else if (this.accumulator.Count == 0)
				{
					inputState = InputState.NotStarted;
				}
				else if (this.accumulator.Count >= maxDigits)
				{
					inputState = InputState.StartedCompleteNotAmbiguous;
				}
				else if (this.EndsWithAStopTone(stopTones))
				{
					inputState = InputState.StartedCompleteNotAmbiguous;
				}
				else
				{
					inputState = this.ComputeStopPatternState(minDigits, stopPatterns);
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.UCMATracer, this, "Accumulator state: '{0}'", new object[]
				{
					inputState
				});
				result = inputState;
			}
			return result;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000ADAC File Offset: 0x00008FAC
		public bool ContainsBargeInPattern(StopPatterns patterns)
		{
			bool flag = patterns.IsBargeInPattern(new ReadOnlyCollection<byte>(this.accumulator));
			CallIdTracer.TraceDebug(ExTraceGlobals.UCMATracer, this, "Accumulator contains barge-in: '{0}'", new object[]
			{
				flag
			});
			return flag;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000ADF0 File Offset: 0x00008FF0
		private static void FindStopPatternMatches(List<byte> digits, StopPatterns stopPatterns, out int partialMatches, out int completeMatches)
		{
			partialMatches = (completeMatches = 0);
			stopPatterns.CountMatches(new ReadOnlyCollection<byte>(digits), out partialMatches, out completeMatches);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000AE14 File Offset: 0x00009014
		private InputState ComputeStopPatternState(int min, StopPatterns stopPatterns)
		{
			foreach (string text in stopPatterns)
			{
				min = Math.Min(min, text.Length);
			}
			InputState result;
			if (this.accumulator.Count < min)
			{
				result = InputState.StartedNotComplete;
			}
			else
			{
				ExAssert.RetailAssert(this.accumulator.Count > 0, "no digits in accumulator!");
				int num;
				int num2;
				ToneAccumulator.FindStopPatternMatches(this.accumulator, stopPatterns, out num, out num2);
				if (num2 == 0)
				{
					if (num == 0)
					{
						result = InputState.StartedCompleteNotAmbiguous;
					}
					else
					{
						result = InputState.StartedNotComplete;
					}
				}
				else if (num > 1)
				{
					result = InputState.StartedCompleteAmbiguous;
				}
				else
				{
					result = InputState.StartedCompleteNotAmbiguous;
				}
			}
			return result;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000AEBC File Offset: 0x000090BC
		private bool EndsWithAStopTone(string stopTones)
		{
			bool result = false;
			if (this.accumulator.Count > 0)
			{
				char value = Convert.ToChar(this.accumulator[this.accumulator.Count - 1]);
				result = (stopTones.IndexOf(value) != -1);
			}
			return result;
		}

		// Token: 0x040000DB RID: 219
		private List<byte> accumulator;

		// Token: 0x040000DC RID: 220
		private object locker = new object();
	}
}
