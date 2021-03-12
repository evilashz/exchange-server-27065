using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Resources
{
	// Token: 0x02000361 RID: 865
	[ComVisible(true)]
	public interface IResourceReader : IEnumerable, IDisposable
	{
		// Token: 0x06002BCC RID: 11212
		void Close();

		// Token: 0x06002BCD RID: 11213
		IDictionaryEnumerator GetEnumerator();
	}
}
