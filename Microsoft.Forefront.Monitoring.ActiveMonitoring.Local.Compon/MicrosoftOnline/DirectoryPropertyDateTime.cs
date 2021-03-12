using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000173 RID: 371
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[XmlInclude(typeof(DirectoryPropertyDateTimeSingle))]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class DirectoryPropertyDateTime : DirectoryProperty
	{
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x000207EC File Offset: 0x0001E9EC
		// (set) Token: 0x060008C8 RID: 2248 RVA: 0x000207F4 File Offset: 0x0001E9F4
		[XmlElement("Value")]
		public DateTime[] Value
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

		// Token: 0x0400046A RID: 1130
		private DateTime[] valueField;
	}
}
