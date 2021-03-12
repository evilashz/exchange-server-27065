using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AA3 RID: 2723
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetCASMailboxResponse : OptionsResponseBase
	{
		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x06004CDF RID: 19679 RVA: 0x00106943 File Offset: 0x00104B43
		// (set) Token: 0x06004CE0 RID: 19680 RVA: 0x0010694B File Offset: 0x00104B4B
		[DataMember(IsRequired = true)]
		public CASMailbox Options { get; set; }

		// Token: 0x06004CE1 RID: 19681 RVA: 0x00106954 File Offset: 0x00104B54
		public override string ToString()
		{
			return string.Format("GetCASMailbox: {0}", this.Options);
		}
	}
}
