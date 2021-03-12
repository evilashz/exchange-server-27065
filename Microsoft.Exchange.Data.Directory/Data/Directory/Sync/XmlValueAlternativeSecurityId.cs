using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000918 RID: 2328
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class XmlValueAlternativeSecurityId
	{
		// Token: 0x1700278B RID: 10123
		// (get) Token: 0x06006F4B RID: 28491 RVA: 0x00176AA7 File Offset: 0x00174CA7
		// (set) Token: 0x06006F4C RID: 28492 RVA: 0x00176AAF File Offset: 0x00174CAF
		[XmlElement(Order = 0)]
		public AlternativeSecurityIdValue Id
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		// Token: 0x04004837 RID: 18487
		private AlternativeSecurityIdValue idField;
	}
}
