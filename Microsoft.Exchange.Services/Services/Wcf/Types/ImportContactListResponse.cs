using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AFD RID: 2813
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class ImportContactListResponse : OptionsResponseBase
	{
		// Token: 0x17001312 RID: 4882
		// (get) Token: 0x06004FF5 RID: 20469 RVA: 0x0010918B File Offset: 0x0010738B
		// (set) Token: 0x06004FF6 RID: 20470 RVA: 0x00109193 File Offset: 0x00107393
		[DataMember(IsRequired = true)]
		public int NumberOfContactsImported { get; set; }

		// Token: 0x06004FF7 RID: 20471 RVA: 0x0010919C File Offset: 0x0010739C
		public override string ToString()
		{
			return string.Format("ImportContactListResponse: {0}", this.NumberOfContactsImported);
		}
	}
}
