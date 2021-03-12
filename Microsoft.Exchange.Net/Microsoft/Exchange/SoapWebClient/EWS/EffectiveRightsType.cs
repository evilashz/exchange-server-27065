using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000258 RID: 600
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class EffectiveRightsType
	{
		// Token: 0x04000F85 RID: 3973
		public bool CreateAssociated;

		// Token: 0x04000F86 RID: 3974
		public bool CreateContents;

		// Token: 0x04000F87 RID: 3975
		public bool CreateHierarchy;

		// Token: 0x04000F88 RID: 3976
		public bool Delete;

		// Token: 0x04000F89 RID: 3977
		public bool Modify;

		// Token: 0x04000F8A RID: 3978
		public bool Read;

		// Token: 0x04000F8B RID: 3979
		public bool ViewPrivateItems;

		// Token: 0x04000F8C RID: 3980
		[XmlIgnore]
		public bool ViewPrivateItemsSpecified;
	}
}
