using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008FF RID: 2303
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class XmlValueTakeoverAction
	{
		// Token: 0x1700275C RID: 10076
		// (get) Token: 0x06006ED6 RID: 28374 RVA: 0x001766C2 File Offset: 0x001748C2
		// (set) Token: 0x06006ED7 RID: 28375 RVA: 0x001766CA File Offset: 0x001748CA
		[XmlElement(Order = 0)]
		public TakeoverActionValue TakeoverAction
		{
			get
			{
				return this.takeoverActionField;
			}
			set
			{
				this.takeoverActionField = value;
			}
		}

		// Token: 0x040047FF RID: 18431
		private TakeoverActionValue takeoverActionField;
	}
}
