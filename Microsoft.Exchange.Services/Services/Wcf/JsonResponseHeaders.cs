using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B6B RID: 2923
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class JsonResponseHeaders
	{
		// Token: 0x060052C6 RID: 21190 RVA: 0x0010BE8D File Offset: 0x0010A08D
		public JsonResponseHeaders()
		{
			this.ServerVersionInfo = ServerVersionInfo.CurrentAssemblyVersion;
		}

		// Token: 0x04002E16 RID: 11798
		[DataMember]
		public ServerVersionInfo ServerVersionInfo;
	}
}
