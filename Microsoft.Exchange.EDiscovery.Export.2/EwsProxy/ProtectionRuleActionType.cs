using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001CD RID: 461
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class ProtectionRuleActionType
	{
		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x0002551C File Offset: 0x0002371C
		// (set) Token: 0x06001396 RID: 5014 RVA: 0x00025524 File Offset: 0x00023724
		[XmlElement("Argument")]
		public ProtectionRuleArgumentType[] Argument
		{
			get
			{
				return this.argumentField;
			}
			set
			{
				this.argumentField = value;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001397 RID: 5015 RVA: 0x0002552D File Offset: 0x0002372D
		// (set) Token: 0x06001398 RID: 5016 RVA: 0x00025535 File Offset: 0x00023735
		[XmlAttribute]
		public ProtectionRuleActionKindType Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		// Token: 0x04000D8F RID: 3471
		private ProtectionRuleArgumentType[] argumentField;

		// Token: 0x04000D90 RID: 3472
		private ProtectionRuleActionKindType nameField;
	}
}
