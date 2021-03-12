using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000913 RID: 2323
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class XmlValueStrongAuthenticationPolicy
	{
		// Token: 0x17002782 RID: 10114
		// (get) Token: 0x06006F34 RID: 28468 RVA: 0x001769DF File Offset: 0x00174BDF
		// (set) Token: 0x06006F35 RID: 28469 RVA: 0x001769E7 File Offset: 0x00174BE7
		[XmlElement(Order = 0)]
		public StrongAuthenticationPolicyValue StrongAuthenticationPolicy
		{
			get
			{
				return this.strongAuthenticationPolicyField;
			}
			set
			{
				this.strongAuthenticationPolicyField = value;
			}
		}

		// Token: 0x0400482E RID: 18478
		private StrongAuthenticationPolicyValue strongAuthenticationPolicyField;
	}
}
