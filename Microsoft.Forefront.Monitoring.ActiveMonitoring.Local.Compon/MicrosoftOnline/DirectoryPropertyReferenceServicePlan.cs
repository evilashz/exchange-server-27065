using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000160 RID: 352
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class DirectoryPropertyReferenceServicePlan : DirectoryPropertyReference
	{
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x000206DD File Offset: 0x0001E8DD
		// (set) Token: 0x060008A7 RID: 2215 RVA: 0x000206E5 File Offset: 0x0001E8E5
		[XmlElement("Value")]
		public DirectoryReferenceServicePlan[] Value
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

		// Token: 0x04000463 RID: 1123
		private DirectoryReferenceServicePlan[] valueField;
	}
}
