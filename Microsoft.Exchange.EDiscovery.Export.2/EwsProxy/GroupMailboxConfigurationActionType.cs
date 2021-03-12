using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000380 RID: 896
	[Flags]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum GroupMailboxConfigurationActionType
	{
		// Token: 0x040012C9 RID: 4809
		SetRegionalSettings = 1,
		// Token: 0x040012CA RID: 4810
		CreateDefaultFolders = 2,
		// Token: 0x040012CB RID: 4811
		SetInitialFolderPermissions = 4,
		// Token: 0x040012CC RID: 4812
		SetAllFolderPermissions = 8,
		// Token: 0x040012CD RID: 4813
		ConfigureCalendar = 16,
		// Token: 0x040012CE RID: 4814
		SendWelcomeMessage = 32,
		// Token: 0x040012CF RID: 4815
		GenerateGroupPhoto = 64
	}
}
