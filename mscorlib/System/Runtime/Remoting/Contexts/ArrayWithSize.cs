using System;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x020007E4 RID: 2020
	internal class ArrayWithSize
	{
		// Token: 0x060057BB RID: 22459 RVA: 0x0013479E File Offset: 0x0013299E
		internal ArrayWithSize(IDynamicMessageSink[] sinks, int count)
		{
			this.Sinks = sinks;
			this.Count = count;
		}

		// Token: 0x040027BE RID: 10174
		internal IDynamicMessageSink[] Sinks;

		// Token: 0x040027BF RID: 10175
		internal int Count;
	}
}
