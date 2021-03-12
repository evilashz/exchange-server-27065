using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000936 RID: 2358
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class XmlValueValidationError
	{
		// Token: 0x170027BA RID: 10170
		// (get) Token: 0x06006FC1 RID: 28609 RVA: 0x00176E86 File Offset: 0x00175086
		// (set) Token: 0x06006FC2 RID: 28610 RVA: 0x00176E8E File Offset: 0x0017508E
		[XmlElement(Order = 0)]
		public ValidationErrorValue ErrorInfo
		{
			get
			{
				return this.errorInfoField;
			}
			set
			{
				this.errorInfoField = value;
			}
		}

		// Token: 0x0400488C RID: 18572
		private ValidationErrorValue errorInfoField;
	}
}
