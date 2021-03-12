using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000952 RID: 2386
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IEnumerator instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("496B0ABF-CDEE-11d3-88E8-00902754C43A")]
	internal interface UCOMIEnumerator
	{
		// Token: 0x0600616C RID: 24940
		bool MoveNext();

		// Token: 0x170010FF RID: 4351
		// (get) Token: 0x0600616D RID: 24941
		object Current { get; }

		// Token: 0x0600616E RID: 24942
		void Reset();
	}
}
