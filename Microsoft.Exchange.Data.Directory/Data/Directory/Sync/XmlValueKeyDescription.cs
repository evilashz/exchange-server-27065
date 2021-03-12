using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000922 RID: 2338
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class XmlValueKeyDescription
	{
		// Token: 0x1700279D RID: 10141
		// (get) Token: 0x06006F77 RID: 28535 RVA: 0x00176C19 File Offset: 0x00174E19
		// (set) Token: 0x06006F78 RID: 28536 RVA: 0x00176C21 File Offset: 0x00174E21
		[XmlElement(Order = 0)]
		public KeyDescriptionValue KeyDescription
		{
			get
			{
				return this.keyDescriptionField;
			}
			set
			{
				this.keyDescriptionField = value;
			}
		}

		// Token: 0x04004851 RID: 18513
		private KeyDescriptionValue keyDescriptionField;
	}
}
