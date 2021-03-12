using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002FE RID: 766
	internal abstract class PlatformSipUri
	{
		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x0600174E RID: 5966
		// (set) Token: 0x0600174F RID: 5967
		public abstract string Host { get; set; }

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001750 RID: 5968
		// (set) Token: 0x06001751 RID: 5969
		public abstract int Port { get; set; }

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001752 RID: 5970
		// (set) Token: 0x06001753 RID: 5971
		public abstract string User { get; set; }

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001754 RID: 5972
		// (set) Token: 0x06001755 RID: 5973
		public abstract UserParameter UserParameter { get; set; }

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001756 RID: 5974
		// (set) Token: 0x06001757 RID: 5975
		public abstract TransportParameter TransportParameter { get; set; }

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001758 RID: 5976
		public abstract string SimplifiedUri { get; }

		// Token: 0x06001759 RID: 5977
		public abstract void AddParameter(string name, string value);

		// Token: 0x0600175A RID: 5978
		public abstract string FindParameter(string name);

		// Token: 0x0600175B RID: 5979
		public abstract void RemoveParameter(string name);

		// Token: 0x0600175C RID: 5980
		public abstract IEnumerable<PlatformSipUriParameter> GetParametersThatHaveValues();

		// Token: 0x0600175D RID: 5981
		public abstract override string ToString();
	}
}
