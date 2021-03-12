using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000120 RID: 288
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class VotingOptionDataType
	{
		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x00021DA1 File Offset: 0x0001FFA1
		// (set) Token: 0x06000D07 RID: 3335 RVA: 0x00021DA9 File Offset: 0x0001FFA9
		public string DisplayName
		{
			get
			{
				return this.displayNameField;
			}
			set
			{
				this.displayNameField = value;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x00021DB2 File Offset: 0x0001FFB2
		// (set) Token: 0x06000D09 RID: 3337 RVA: 0x00021DBA File Offset: 0x0001FFBA
		public SendPromptType SendPrompt
		{
			get
			{
				return this.sendPromptField;
			}
			set
			{
				this.sendPromptField = value;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x00021DC3 File Offset: 0x0001FFC3
		// (set) Token: 0x06000D0B RID: 3339 RVA: 0x00021DCB File Offset: 0x0001FFCB
		[XmlIgnore]
		public bool SendPromptSpecified
		{
			get
			{
				return this.sendPromptFieldSpecified;
			}
			set
			{
				this.sendPromptFieldSpecified = value;
			}
		}

		// Token: 0x0400090C RID: 2316
		private string displayNameField;

		// Token: 0x0400090D RID: 2317
		private SendPromptType sendPromptField;

		// Token: 0x0400090E RID: 2318
		private bool sendPromptFieldSpecified;
	}
}
