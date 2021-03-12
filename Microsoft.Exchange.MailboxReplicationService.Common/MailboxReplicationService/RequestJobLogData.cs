using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001C8 RID: 456
	internal class RequestJobLogData : RequestJobBase
	{
		// Token: 0x060012AC RID: 4780 RVA: 0x0002ADEA File Offset: 0x00028FEA
		public RequestJobLogData(RequestJobBase request, RequestState statusDetail) : this(request)
		{
			this.SetOverride("StatusDetail", statusDetail.ToString());
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x0002AE09 File Offset: 0x00029009
		public RequestJobLogData(RequestJobBase request) : base((SimpleProviderPropertyBag)request.propertyBag)
		{
			this.overrides = new Dictionary<string, string>();
			this.Request = request;
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x0002AE2E File Offset: 0x0002902E
		// (set) Token: 0x060012AF RID: 4783 RVA: 0x0002AE36 File Offset: 0x00029036
		public RequestJobBase Request { get; private set; }

		// Token: 0x060012B0 RID: 4784 RVA: 0x0002AE3F File Offset: 0x0002903F
		internal bool TryGetOverride(string key, out string value)
		{
			return this.overrides.TryGetValue(key, out value);
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x0002AE4E File Offset: 0x0002904E
		private void SetOverride(string key, string value)
		{
			this.overrides[key] = value;
		}

		// Token: 0x040009B1 RID: 2481
		public const string StatusDetailKey = "StatusDetail";

		// Token: 0x040009B2 RID: 2482
		private Dictionary<string, string> overrides;
	}
}
