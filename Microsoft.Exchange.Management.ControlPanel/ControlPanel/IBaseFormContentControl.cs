using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200006B RID: 107
	public interface IBaseFormContentControl
	{
		// Token: 0x17001851 RID: 6225
		// (get) Token: 0x06001AA5 RID: 6821
		WebServiceMethod RefreshWebServiceMethod { get; }

		// Token: 0x17001852 RID: 6226
		// (get) Token: 0x06001AA6 RID: 6822
		WebServiceMethod SaveWebServiceMethod { get; }

		// Token: 0x17001853 RID: 6227
		// (get) Token: 0x06001AA7 RID: 6823
		bool ReadOnly { get; }

		// Token: 0x17001854 RID: 6228
		// (get) Token: 0x06001AA8 RID: 6824
		bool HasSaveMethod { get; }

		// Token: 0x17001855 RID: 6229
		// (get) Token: 0x06001AA9 RID: 6825
		SectionCollection Sections { get; }
	}
}
