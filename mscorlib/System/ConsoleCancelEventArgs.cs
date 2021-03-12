﻿using System;

namespace System
{
	// Token: 0x020000C4 RID: 196
	[Serializable]
	public sealed class ConsoleCancelEventArgs : EventArgs
	{
		// Token: 0x06000B96 RID: 2966 RVA: 0x00024F39 File Offset: 0x00023139
		internal ConsoleCancelEventArgs(ConsoleSpecialKey type)
		{
			this._type = type;
			this._cancel = false;
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x00024F4F File Offset: 0x0002314F
		// (set) Token: 0x06000B98 RID: 2968 RVA: 0x00024F57 File Offset: 0x00023157
		public bool Cancel
		{
			get
			{
				return this._cancel;
			}
			set
			{
				this._cancel = value;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x00024F60 File Offset: 0x00023160
		public ConsoleSpecialKey SpecialKey
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x04000483 RID: 1155
		private ConsoleSpecialKey _type;

		// Token: 0x04000484 RID: 1156
		private bool _cancel;
	}
}
