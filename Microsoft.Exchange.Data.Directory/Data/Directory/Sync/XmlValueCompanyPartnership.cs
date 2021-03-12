using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000949 RID: 2377
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class XmlValueCompanyPartnership
	{
		// Token: 0x170027DE RID: 10206
		// (get) Token: 0x06007018 RID: 28696 RVA: 0x00177170 File Offset: 0x00175370
		// (set) Token: 0x06007019 RID: 28697 RVA: 0x00177178 File Offset: 0x00175378
		[XmlArrayItem("Partnership", IsNullable = false)]
		[XmlArray(Order = 0)]
		public PartnershipValue[] Partnerships
		{
			get
			{
				return this.partnershipsField;
			}
			set
			{
				this.partnershipsField = value;
			}
		}

		// Token: 0x040048C1 RID: 18625
		private PartnershipValue[] partnershipsField;
	}
}
