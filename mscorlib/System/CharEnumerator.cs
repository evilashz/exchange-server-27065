using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000B7 RID: 183
	[ComVisible(true)]
	[Serializable]
	public sealed class CharEnumerator : IEnumerator, ICloneable, IEnumerator<char>, IDisposable
	{
		// Token: 0x06000AD7 RID: 2775 RVA: 0x000224E3 File Offset: 0x000206E3
		internal CharEnumerator(string str)
		{
			this.str = str;
			this.index = -1;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x000224F9 File Offset: 0x000206F9
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00022504 File Offset: 0x00020704
		public bool MoveNext()
		{
			if (this.index < this.str.Length - 1)
			{
				this.index++;
				this.currentElement = this.str[this.index];
				return true;
			}
			this.index = this.str.Length;
			return false;
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0002255F File Offset: 0x0002075F
		public void Dispose()
		{
			if (this.str != null)
			{
				this.index = this.str.Length;
			}
			this.str = null;
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x00022584 File Offset: 0x00020784
		object IEnumerator.Current
		{
			get
			{
				if (this.index == -1)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
				}
				if (this.index >= this.str.Length)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
				}
				return this.currentElement;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x000225D8 File Offset: 0x000207D8
		public char Current
		{
			get
			{
				if (this.index == -1)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
				}
				if (this.index >= this.str.Length)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
				}
				return this.currentElement;
			}
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00022627 File Offset: 0x00020827
		public void Reset()
		{
			this.currentElement = '\0';
			this.index = -1;
		}

		// Token: 0x04000400 RID: 1024
		private string str;

		// Token: 0x04000401 RID: 1025
		private int index;

		// Token: 0x04000402 RID: 1026
		private char currentElement;
	}
}
