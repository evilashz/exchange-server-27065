using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000931 RID: 2353
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class XmlValueAppAddress
	{
		// Token: 0x170027B4 RID: 10164
		// (get) Token: 0x06006FB1 RID: 28593 RVA: 0x00176E00 File Offset: 0x00175000
		// (set) Token: 0x06006FB2 RID: 28594 RVA: 0x00176E08 File Offset: 0x00175008
		[XmlElement(Order = 0)]
		public AppAddressValue AppAddress
		{
			get
			{
				return this.appAddressField;
			}
			set
			{
				this.appAddressField = value;
			}
		}

		// Token: 0x04004880 RID: 18560
		private AppAddressValue appAddressField;
	}
}
