using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003F7 RID: 1015
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DomainDnsTxtRecord", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public class DomainDnsTxtRecord : DomainDnsRecord
	{
		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x0600192F RID: 6447 RVA: 0x0008D77C File Offset: 0x0008B97C
		// (set) Token: 0x06001930 RID: 6448 RVA: 0x0008D784 File Offset: 0x0008B984
		[DataMember]
		public string Text
		{
			get
			{
				return this.TextField;
			}
			set
			{
				this.TextField = value;
			}
		}

		// Token: 0x040011A1 RID: 4513
		private string TextField;
	}
}
