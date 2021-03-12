using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000461 RID: 1121
	[Flags]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum GroupMailboxConfigurationActionType
	{
		// Token: 0x0400171B RID: 5915
		SetRegionalSettings = 1,
		// Token: 0x0400171C RID: 5916
		CreateDefaultFolders = 2,
		// Token: 0x0400171D RID: 5917
		SetInitialFolderPermissions = 4,
		// Token: 0x0400171E RID: 5918
		SetAllFolderPermissions = 8,
		// Token: 0x0400171F RID: 5919
		ConfigureCalendar = 16,
		// Token: 0x04001720 RID: 5920
		SendWelcomeMessage = 32,
		// Token: 0x04001721 RID: 5921
		GenerateGroupPhoto = 64
	}
}
