﻿using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x0200005E RID: 94
	internal interface ITupleInternal : ITuple
	{
		// Token: 0x06000347 RID: 839
		string ToString(StringBuilder sb);

		// Token: 0x06000348 RID: 840
		int GetHashCode(IEqualityComparer comparer);
	}
}
