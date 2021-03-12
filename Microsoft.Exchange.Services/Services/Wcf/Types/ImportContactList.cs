using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AFB RID: 2811
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class ImportContactList
	{
		// Token: 0x17001310 RID: 4880
		// (get) Token: 0x06004FEE RID: 20462 RVA: 0x0010913C File Offset: 0x0010733C
		// (set) Token: 0x06004FEF RID: 20463 RVA: 0x00109144 File Offset: 0x00107344
		[DataMember(IsRequired = true)]
		public byte[] CSVData { get; set; }
	}
}
