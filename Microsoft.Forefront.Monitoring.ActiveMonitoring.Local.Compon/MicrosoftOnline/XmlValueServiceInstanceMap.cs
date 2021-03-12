using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000D6 RID: 214
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[Serializable]
	public class XmlValueServiceInstanceMap
	{
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x0001F5A6 File Offset: 0x0001D7A6
		// (set) Token: 0x060006AD RID: 1709 RVA: 0x0001F5AE File Offset: 0x0001D7AE
		public ServiceInstanceMapValue Maps
		{
			get
			{
				return this.mapsField;
			}
			set
			{
				this.mapsField = value;
			}
		}

		// Token: 0x0400036A RID: 874
		private ServiceInstanceMapValue mapsField;
	}
}
