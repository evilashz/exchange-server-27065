using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008BF RID: 2239
	public interface ITuple
	{
		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x06005CEB RID: 23787
		int Length { get; }

		// Token: 0x17001010 RID: 4112
		object this[int index]
		{
			get;
		}
	}
}
