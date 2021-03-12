using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000313 RID: 787
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ResetUMMailboxType : BaseRequestType
	{
		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x060019DF RID: 6623 RVA: 0x00028A05 File Offset: 0x00026C05
		// (set) Token: 0x060019E0 RID: 6624 RVA: 0x00028A0D File Offset: 0x00026C0D
		public bool KeepProperties
		{
			get
			{
				return this.keepPropertiesField;
			}
			set
			{
				this.keepPropertiesField = value;
			}
		}

		// Token: 0x04001163 RID: 4451
		private bool keepPropertiesField;
	}
}
