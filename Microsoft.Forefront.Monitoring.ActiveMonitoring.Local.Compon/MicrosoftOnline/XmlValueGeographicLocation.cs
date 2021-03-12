using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000F2 RID: 242
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class XmlValueGeographicLocation
	{
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x0001FD1A File Offset: 0x0001DF1A
		// (set) Token: 0x06000783 RID: 1923 RVA: 0x0001FD22 File Offset: 0x0001DF22
		public GeographicLocationValue Location
		{
			get
			{
				return this.locationField;
			}
			set
			{
				this.locationField = value;
			}
		}

		// Token: 0x040003D7 RID: 983
		private GeographicLocationValue locationField;
	}
}
