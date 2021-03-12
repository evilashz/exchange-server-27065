using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000171 RID: 369
	[XmlInclude(typeof(DirectoryPropertyGuidSingle))]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class DirectoryPropertyGuid : DirectoryProperty
	{
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x000207CB File Offset: 0x0001E9CB
		// (set) Token: 0x060008C4 RID: 2244 RVA: 0x000207D3 File Offset: 0x0001E9D3
		[XmlElement("Value")]
		public string[] Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		// Token: 0x04000469 RID: 1129
		private string[] valueField;
	}
}
