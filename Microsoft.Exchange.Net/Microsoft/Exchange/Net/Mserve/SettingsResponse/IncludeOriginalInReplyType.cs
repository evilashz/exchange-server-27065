using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008DE RID: 2270
	[XmlType(Namespace = "HMSETTINGS:")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[Serializable]
	public enum IncludeOriginalInReplyType
	{
		// Token: 0x04002A31 RID: 10801
		Auto,
		// Token: 0x04002A32 RID: 10802
		Manual
	}
}
