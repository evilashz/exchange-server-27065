using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001B1 RID: 433
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class RetentionPolicyTagType
	{
		// Token: 0x04000A08 RID: 2568
		public string DisplayName;

		// Token: 0x04000A09 RID: 2569
		public string RetentionId;

		// Token: 0x04000A0A RID: 2570
		public int RetentionPeriod;

		// Token: 0x04000A0B RID: 2571
		public ElcFolderType Type;

		// Token: 0x04000A0C RID: 2572
		public RetentionActionType RetentionAction;

		// Token: 0x04000A0D RID: 2573
		public string Description;

		// Token: 0x04000A0E RID: 2574
		public bool IsVisible;

		// Token: 0x04000A0F RID: 2575
		public bool OptedInto;

		// Token: 0x04000A10 RID: 2576
		public bool IsArchive;
	}
}
