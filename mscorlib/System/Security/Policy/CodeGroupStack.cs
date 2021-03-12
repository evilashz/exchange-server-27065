using System;
using System.Collections;

namespace System.Security.Policy
{
	// Token: 0x0200033A RID: 826
	internal sealed class CodeGroupStack
	{
		// Token: 0x060029F2 RID: 10738 RVA: 0x0009BF1F File Offset: 0x0009A11F
		internal CodeGroupStack()
		{
			this.m_array = new ArrayList();
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x0009BF32 File Offset: 0x0009A132
		internal void Push(CodeGroupStackFrame element)
		{
			this.m_array.Add(element);
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x0009BF44 File Offset: 0x0009A144
		internal CodeGroupStackFrame Pop()
		{
			if (this.IsEmpty())
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EmptyStack"));
			}
			int count = this.m_array.Count;
			CodeGroupStackFrame result = (CodeGroupStackFrame)this.m_array[count - 1];
			this.m_array.RemoveAt(count - 1);
			return result;
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x0009BF98 File Offset: 0x0009A198
		internal bool IsEmpty()
		{
			return this.m_array.Count == 0;
		}

		// Token: 0x040010D4 RID: 4308
		private ArrayList m_array;
	}
}
