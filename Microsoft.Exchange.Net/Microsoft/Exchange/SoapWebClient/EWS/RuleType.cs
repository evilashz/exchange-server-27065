using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000293 RID: 659
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class RuleType
	{
		// Token: 0x04001124 RID: 4388
		public string RuleId;

		// Token: 0x04001125 RID: 4389
		public string DisplayName;

		// Token: 0x04001126 RID: 4390
		public int Priority;

		// Token: 0x04001127 RID: 4391
		public bool IsEnabled;

		// Token: 0x04001128 RID: 4392
		public bool IsNotSupported;

		// Token: 0x04001129 RID: 4393
		[XmlIgnore]
		public bool IsNotSupportedSpecified;

		// Token: 0x0400112A RID: 4394
		public bool IsInError;

		// Token: 0x0400112B RID: 4395
		[XmlIgnore]
		public bool IsInErrorSpecified;

		// Token: 0x0400112C RID: 4396
		public RulePredicatesType Conditions;

		// Token: 0x0400112D RID: 4397
		public RulePredicatesType Exceptions;

		// Token: 0x0400112E RID: 4398
		public RuleActionsType Actions;
	}
}
