using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000941 RID: 2369
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public enum ProvisioningStatus
	{
		// Token: 0x040048AD RID: 18605
		Success,
		// Token: 0x040048AE RID: 18606
		Error,
		// Token: 0x040048AF RID: 18607
		PendingInput
	}
}
