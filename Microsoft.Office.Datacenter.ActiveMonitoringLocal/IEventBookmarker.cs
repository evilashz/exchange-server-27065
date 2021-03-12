using System;
using System.Diagnostics.Eventing.Reader;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000074 RID: 116
	public interface IEventBookmarker
	{
		// Token: 0x060006AD RID: 1709
		void Initialize(string baseLocation);

		// Token: 0x060006AE RID: 1710
		EventBookmark Read(string bookmarkName);

		// Token: 0x060006AF RID: 1711
		void Write(string bookmarkName, EventBookmark bookmark);

		// Token: 0x060006B0 RID: 1712
		void Delete(string bookmarkName);
	}
}
