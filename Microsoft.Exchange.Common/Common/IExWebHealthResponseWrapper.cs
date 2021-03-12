using System;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000051 RID: 81
	public interface IExWebHealthResponseWrapper
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001A1 RID: 417
		// (set) Token: 0x060001A2 RID: 418
		int StatusCode { get; set; }

		// Token: 0x060001A3 RID: 419
		void AddHeader(string name, string value);

		// Token: 0x060001A4 RID: 420
		string GetHeaderValue(string name);
	}
}
