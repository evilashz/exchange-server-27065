using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000612 RID: 1554
	internal sealed class __ExceptionInfo
	{
		// Token: 0x060049BF RID: 18879 RVA: 0x0010AA14 File Offset: 0x00108C14
		private __ExceptionInfo()
		{
			this.m_startAddr = 0;
			this.m_filterAddr = null;
			this.m_catchAddr = null;
			this.m_catchEndAddr = null;
			this.m_endAddr = 0;
			this.m_currentCatch = 0;
			this.m_type = null;
			this.m_endFinally = -1;
			this.m_currentState = 0;
		}

		// Token: 0x060049C0 RID: 18880 RVA: 0x0010AA68 File Offset: 0x00108C68
		internal __ExceptionInfo(int startAddr, Label endLabel)
		{
			this.m_startAddr = startAddr;
			this.m_endAddr = -1;
			this.m_filterAddr = new int[4];
			this.m_catchAddr = new int[4];
			this.m_catchEndAddr = new int[4];
			this.m_catchClass = new Type[4];
			this.m_currentCatch = 0;
			this.m_endLabel = endLabel;
			this.m_type = new int[4];
			this.m_endFinally = -1;
			this.m_currentState = 0;
		}

		// Token: 0x060049C1 RID: 18881 RVA: 0x0010AAE4 File Offset: 0x00108CE4
		private static Type[] EnlargeArray(Type[] incoming)
		{
			Type[] array = new Type[incoming.Length * 2];
			Array.Copy(incoming, array, incoming.Length);
			return array;
		}

		// Token: 0x060049C2 RID: 18882 RVA: 0x0010AB08 File Offset: 0x00108D08
		private void MarkHelper(int catchorfilterAddr, int catchEndAddr, Type catchClass, int type)
		{
			if (this.m_currentCatch >= this.m_catchAddr.Length)
			{
				this.m_filterAddr = ILGenerator.EnlargeArray(this.m_filterAddr);
				this.m_catchAddr = ILGenerator.EnlargeArray(this.m_catchAddr);
				this.m_catchEndAddr = ILGenerator.EnlargeArray(this.m_catchEndAddr);
				this.m_catchClass = __ExceptionInfo.EnlargeArray(this.m_catchClass);
				this.m_type = ILGenerator.EnlargeArray(this.m_type);
			}
			if (type == 1)
			{
				this.m_type[this.m_currentCatch] = type;
				this.m_filterAddr[this.m_currentCatch] = catchorfilterAddr;
				this.m_catchAddr[this.m_currentCatch] = -1;
				if (this.m_currentCatch > 0)
				{
					this.m_catchEndAddr[this.m_currentCatch - 1] = catchorfilterAddr;
				}
			}
			else
			{
				this.m_catchClass[this.m_currentCatch] = catchClass;
				if (this.m_type[this.m_currentCatch] != 1)
				{
					this.m_type[this.m_currentCatch] = type;
				}
				this.m_catchAddr[this.m_currentCatch] = catchorfilterAddr;
				if (this.m_currentCatch > 0 && this.m_type[this.m_currentCatch] != 1)
				{
					this.m_catchEndAddr[this.m_currentCatch - 1] = catchEndAddr;
				}
				this.m_catchEndAddr[this.m_currentCatch] = -1;
				this.m_currentCatch++;
			}
			if (this.m_endAddr == -1)
			{
				this.m_endAddr = catchorfilterAddr;
			}
		}

		// Token: 0x060049C3 RID: 18883 RVA: 0x0010AC5B File Offset: 0x00108E5B
		internal void MarkFilterAddr(int filterAddr)
		{
			this.m_currentState = 1;
			this.MarkHelper(filterAddr, filterAddr, null, 1);
		}

		// Token: 0x060049C4 RID: 18884 RVA: 0x0010AC6E File Offset: 0x00108E6E
		internal void MarkFaultAddr(int faultAddr)
		{
			this.m_currentState = 4;
			this.MarkHelper(faultAddr, faultAddr, null, 4);
		}

		// Token: 0x060049C5 RID: 18885 RVA: 0x0010AC81 File Offset: 0x00108E81
		internal void MarkCatchAddr(int catchAddr, Type catchException)
		{
			this.m_currentState = 2;
			this.MarkHelper(catchAddr, catchAddr, catchException, 0);
		}

		// Token: 0x060049C6 RID: 18886 RVA: 0x0010AC94 File Offset: 0x00108E94
		internal void MarkFinallyAddr(int finallyAddr, int endCatchAddr)
		{
			if (this.m_endFinally != -1)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_TooManyFinallyClause"));
			}
			this.m_currentState = 3;
			this.m_endFinally = finallyAddr;
			this.MarkHelper(finallyAddr, endCatchAddr, null, 2);
		}

		// Token: 0x060049C7 RID: 18887 RVA: 0x0010ACC7 File Offset: 0x00108EC7
		internal void Done(int endAddr)
		{
			this.m_catchEndAddr[this.m_currentCatch - 1] = endAddr;
			this.m_currentState = 5;
		}

		// Token: 0x060049C8 RID: 18888 RVA: 0x0010ACE0 File Offset: 0x00108EE0
		internal int GetStartAddress()
		{
			return this.m_startAddr;
		}

		// Token: 0x060049C9 RID: 18889 RVA: 0x0010ACE8 File Offset: 0x00108EE8
		internal int GetEndAddress()
		{
			return this.m_endAddr;
		}

		// Token: 0x060049CA RID: 18890 RVA: 0x0010ACF0 File Offset: 0x00108EF0
		internal int GetFinallyEndAddress()
		{
			return this.m_endFinally;
		}

		// Token: 0x060049CB RID: 18891 RVA: 0x0010ACF8 File Offset: 0x00108EF8
		internal Label GetEndLabel()
		{
			return this.m_endLabel;
		}

		// Token: 0x060049CC RID: 18892 RVA: 0x0010AD00 File Offset: 0x00108F00
		internal int[] GetFilterAddresses()
		{
			return this.m_filterAddr;
		}

		// Token: 0x060049CD RID: 18893 RVA: 0x0010AD08 File Offset: 0x00108F08
		internal int[] GetCatchAddresses()
		{
			return this.m_catchAddr;
		}

		// Token: 0x060049CE RID: 18894 RVA: 0x0010AD10 File Offset: 0x00108F10
		internal int[] GetCatchEndAddresses()
		{
			return this.m_catchEndAddr;
		}

		// Token: 0x060049CF RID: 18895 RVA: 0x0010AD18 File Offset: 0x00108F18
		internal Type[] GetCatchClass()
		{
			return this.m_catchClass;
		}

		// Token: 0x060049D0 RID: 18896 RVA: 0x0010AD20 File Offset: 0x00108F20
		internal int GetNumberOfCatches()
		{
			return this.m_currentCatch;
		}

		// Token: 0x060049D1 RID: 18897 RVA: 0x0010AD28 File Offset: 0x00108F28
		internal int[] GetExceptionTypes()
		{
			return this.m_type;
		}

		// Token: 0x060049D2 RID: 18898 RVA: 0x0010AD30 File Offset: 0x00108F30
		internal void SetFinallyEndLabel(Label lbl)
		{
			this.m_finallyEndLabel = lbl;
		}

		// Token: 0x060049D3 RID: 18899 RVA: 0x0010AD39 File Offset: 0x00108F39
		internal Label GetFinallyEndLabel()
		{
			return this.m_finallyEndLabel;
		}

		// Token: 0x060049D4 RID: 18900 RVA: 0x0010AD44 File Offset: 0x00108F44
		internal bool IsInner(__ExceptionInfo exc)
		{
			int num = exc.m_currentCatch - 1;
			int num2 = this.m_currentCatch - 1;
			return exc.m_catchEndAddr[num] < this.m_catchEndAddr[num2] || (exc.m_catchEndAddr[num] == this.m_catchEndAddr[num2] && exc.GetEndAddress() > this.GetEndAddress());
		}

		// Token: 0x060049D5 RID: 18901 RVA: 0x0010AD9A File Offset: 0x00108F9A
		internal int GetCurrentState()
		{
			return this.m_currentState;
		}

		// Token: 0x04001E1E RID: 7710
		internal const int None = 0;

		// Token: 0x04001E1F RID: 7711
		internal const int Filter = 1;

		// Token: 0x04001E20 RID: 7712
		internal const int Finally = 2;

		// Token: 0x04001E21 RID: 7713
		internal const int Fault = 4;

		// Token: 0x04001E22 RID: 7714
		internal const int PreserveStack = 4;

		// Token: 0x04001E23 RID: 7715
		internal const int State_Try = 0;

		// Token: 0x04001E24 RID: 7716
		internal const int State_Filter = 1;

		// Token: 0x04001E25 RID: 7717
		internal const int State_Catch = 2;

		// Token: 0x04001E26 RID: 7718
		internal const int State_Finally = 3;

		// Token: 0x04001E27 RID: 7719
		internal const int State_Fault = 4;

		// Token: 0x04001E28 RID: 7720
		internal const int State_Done = 5;

		// Token: 0x04001E29 RID: 7721
		internal int m_startAddr;

		// Token: 0x04001E2A RID: 7722
		internal int[] m_filterAddr;

		// Token: 0x04001E2B RID: 7723
		internal int[] m_catchAddr;

		// Token: 0x04001E2C RID: 7724
		internal int[] m_catchEndAddr;

		// Token: 0x04001E2D RID: 7725
		internal int[] m_type;

		// Token: 0x04001E2E RID: 7726
		internal Type[] m_catchClass;

		// Token: 0x04001E2F RID: 7727
		internal Label m_endLabel;

		// Token: 0x04001E30 RID: 7728
		internal Label m_finallyEndLabel;

		// Token: 0x04001E31 RID: 7729
		internal int m_endAddr;

		// Token: 0x04001E32 RID: 7730
		internal int m_endFinally;

		// Token: 0x04001E33 RID: 7731
		internal int m_currentCatch;

		// Token: 0x04001E34 RID: 7732
		private int m_currentState;
	}
}
