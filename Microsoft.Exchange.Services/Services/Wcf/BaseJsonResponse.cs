using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009FE RID: 2558
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public abstract class BaseJsonResponse
	{
		// Token: 0x06004849 RID: 18505 RVA: 0x00101610 File Offset: 0x000FF810
		public BaseJsonResponse()
		{
			this.Header = new JsonResponseHeaders();
		}

		// Token: 0x04002955 RID: 10581
		[DataMember(Name = "Header")]
		public JsonResponseHeaders Header;
	}
}
