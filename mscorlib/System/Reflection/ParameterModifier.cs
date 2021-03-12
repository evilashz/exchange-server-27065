using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005EB RID: 1515
	[ComVisible(true)]
	[Serializable]
	public struct ParameterModifier
	{
		// Token: 0x06004729 RID: 18217 RVA: 0x0010220A File Offset: 0x0010040A
		public ParameterModifier(int parameterCount)
		{
			if (parameterCount <= 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ParmArraySize"));
			}
			this._byRef = new bool[parameterCount];
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x0600472A RID: 18218 RVA: 0x0010222C File Offset: 0x0010042C
		internal bool[] IsByRefArray
		{
			get
			{
				return this._byRef;
			}
		}

		// Token: 0x17000B26 RID: 2854
		public bool this[int index]
		{
			get
			{
				return this._byRef[index];
			}
			set
			{
				this._byRef[index] = value;
			}
		}

		// Token: 0x04001D44 RID: 7492
		private bool[] _byRef;
	}
}
