using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000FD RID: 253
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class XmlValueContextMoveWatermarks
	{
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x0001FE83 File Offset: 0x0001E083
		// (set) Token: 0x060007AE RID: 1966 RVA: 0x0001FE8B File Offset: 0x0001E08B
		public ContextMoveWatermarksValue Watermarks
		{
			get
			{
				return this.watermarksField;
			}
			set
			{
				this.watermarksField = value;
			}
		}

		// Token: 0x040003F1 RID: 1009
		private ContextMoveWatermarksValue watermarksField;
	}
}
