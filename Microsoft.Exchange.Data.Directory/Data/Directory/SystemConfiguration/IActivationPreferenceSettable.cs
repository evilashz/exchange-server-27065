using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002FB RID: 763
	internal interface IActivationPreferenceSettable<T> : IComparable<T>
	{
		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06002388 RID: 9096
		// (set) Token: 0x06002389 RID: 9097
		int ActualValue { get; set; }

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x0600238A RID: 9098
		int DesiredValue { get; }

		// Token: 0x17000907 RID: 2311
		// (set) Token: 0x0600238B RID: 9099
		bool InvalidHostServerAllowed { set; }

		// Token: 0x0600238C RID: 9100
		bool Matches(T other);
	}
}
