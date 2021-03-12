using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000769 RID: 1897
	internal sealed class SerStack
	{
		// Token: 0x0600531C RID: 21276 RVA: 0x001242A6 File Offset: 0x001224A6
		internal SerStack()
		{
			this.stackId = "System";
		}

		// Token: 0x0600531D RID: 21277 RVA: 0x001242CC File Offset: 0x001224CC
		internal SerStack(string stackId)
		{
			this.stackId = stackId;
		}

		// Token: 0x0600531E RID: 21278 RVA: 0x001242F0 File Offset: 0x001224F0
		internal void Push(object obj)
		{
			if (this.top == this.objects.Length - 1)
			{
				this.IncreaseCapacity();
			}
			object[] array = this.objects;
			int num = this.top + 1;
			this.top = num;
			array[num] = obj;
		}

		// Token: 0x0600531F RID: 21279 RVA: 0x00124330 File Offset: 0x00122530
		internal object Pop()
		{
			if (this.top < 0)
			{
				return null;
			}
			object result = this.objects[this.top];
			object[] array = this.objects;
			int num = this.top;
			this.top = num - 1;
			array[num] = null;
			return result;
		}

		// Token: 0x06005320 RID: 21280 RVA: 0x00124370 File Offset: 0x00122570
		internal void IncreaseCapacity()
		{
			int num = this.objects.Length * 2;
			object[] destinationArray = new object[num];
			Array.Copy(this.objects, 0, destinationArray, 0, this.objects.Length);
			this.objects = destinationArray;
		}

		// Token: 0x06005321 RID: 21281 RVA: 0x001243AC File Offset: 0x001225AC
		internal object Peek()
		{
			if (this.top < 0)
			{
				return null;
			}
			return this.objects[this.top];
		}

		// Token: 0x06005322 RID: 21282 RVA: 0x001243C6 File Offset: 0x001225C6
		internal object PeekPeek()
		{
			if (this.top < 1)
			{
				return null;
			}
			return this.objects[this.top - 1];
		}

		// Token: 0x06005323 RID: 21283 RVA: 0x001243E2 File Offset: 0x001225E2
		internal int Count()
		{
			return this.top + 1;
		}

		// Token: 0x06005324 RID: 21284 RVA: 0x001243EC File Offset: 0x001225EC
		internal bool IsEmpty()
		{
			return this.top <= 0;
		}

		// Token: 0x06005325 RID: 21285 RVA: 0x001243FC File Offset: 0x001225FC
		[Conditional("SER_LOGGING")]
		internal void Dump()
		{
			for (int i = 0; i < this.Count(); i++)
			{
				object obj = this.objects[i];
			}
		}

		// Token: 0x04002593 RID: 9619
		internal object[] objects = new object[5];

		// Token: 0x04002594 RID: 9620
		internal string stackId;

		// Token: 0x04002595 RID: 9621
		internal int top = -1;

		// Token: 0x04002596 RID: 9622
		internal int next;
	}
}
