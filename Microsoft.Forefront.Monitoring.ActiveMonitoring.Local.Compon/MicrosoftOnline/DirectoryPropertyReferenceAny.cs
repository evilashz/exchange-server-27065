using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000165 RID: 357
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[XmlInclude(typeof(DirectoryPropertyReferenceAnySingle))]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class DirectoryPropertyReferenceAny : DirectoryPropertyReference
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x00020738 File Offset: 0x0001E938
		// (set) Token: 0x060008B2 RID: 2226 RVA: 0x00020740 File Offset: 0x0001E940
		[XmlElement("Value")]
		public DirectoryReferenceAny[] Value
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

		// Token: 0x04000466 RID: 1126
		private DirectoryReferenceAny[] valueField;
	}
}
