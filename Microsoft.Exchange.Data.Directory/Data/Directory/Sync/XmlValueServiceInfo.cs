using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200093A RID: 2362
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class XmlValueServiceInfo
	{
		// Token: 0x170027C5 RID: 10181
		// (get) Token: 0x06006FDB RID: 28635 RVA: 0x00176F61 File Offset: 0x00175161
		// (set) Token: 0x06006FDC RID: 28636 RVA: 0x00176F69 File Offset: 0x00175169
		[XmlElement(Order = 0)]
		public ServiceInfoValue Info
		{
			get
			{
				return this.infoField;
			}
			set
			{
				this.infoField = value;
			}
		}

		// Token: 0x04004897 RID: 18583
		private ServiceInfoValue infoField;
	}
}
