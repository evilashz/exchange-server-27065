using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002C5 RID: 709
	[XmlInclude(typeof(CreateRuleOperationType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlInclude(typeof(SetRuleOperationType))]
	[XmlInclude(typeof(DeleteRuleOperationType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public abstract class RuleOperationType
	{
	}
}
