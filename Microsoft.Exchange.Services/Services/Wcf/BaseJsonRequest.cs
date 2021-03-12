using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000A6F RID: 2671
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public abstract class BaseJsonRequest
	{
		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x06004BD1 RID: 19409 RVA: 0x00105DB5 File Offset: 0x00103FB5
		// (set) Token: 0x06004BD2 RID: 19410 RVA: 0x00105DBD File Offset: 0x00103FBD
		[DataMember(Name = "Header", IsRequired = true)]
		public JsonRequestHeaders Header { get; set; }
	}
}
