using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000E1 RID: 225
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class PathToExceptionFieldType : BasePathToElementType
	{
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x00020675 File Offset: 0x0001E875
		// (set) Token: 0x06000A4A RID: 2634 RVA: 0x0002067D File Offset: 0x0001E87D
		[XmlAttribute]
		public ExceptionPropertyURIType FieldURI
		{
			get
			{
				return this.fieldURIField;
			}
			set
			{
				this.fieldURIField = value;
			}
		}

		// Token: 0x04000616 RID: 1558
		private ExceptionPropertyURIType fieldURIField;
	}
}
