using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000161 RID: 353
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[XmlInclude(typeof(DirectoryPropertyReferenceContactSingle))]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class DirectoryPropertyReferenceContact : DirectoryPropertyReference
	{
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x000206F6 File Offset: 0x0001E8F6
		// (set) Token: 0x060008AA RID: 2218 RVA: 0x000206FE File Offset: 0x0001E8FE
		[XmlElement("Value")]
		public DirectoryReferenceContact[] Value
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

		// Token: 0x04000464 RID: 1124
		private DirectoryReferenceContact[] valueField;
	}
}
