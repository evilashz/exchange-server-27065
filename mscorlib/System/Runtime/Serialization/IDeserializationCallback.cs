using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000704 RID: 1796
	[ComVisible(true)]
	public interface IDeserializationCallback
	{
		// Token: 0x06005086 RID: 20614
		void OnDeserialization(object sender);
	}
}
