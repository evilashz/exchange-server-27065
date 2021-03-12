using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200090B RID: 2315
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class XmlValueStrongAuthenticationRequirement
	{
		// Token: 0x17002773 RID: 10099
		// (get) Token: 0x06006F0E RID: 28430 RVA: 0x00176899 File Offset: 0x00174A99
		// (set) Token: 0x06006F0F RID: 28431 RVA: 0x001768A1 File Offset: 0x00174AA1
		[XmlElement(Order = 0)]
		public StrongAuthenticationRequirementValue StrongAuthenticationRequirement
		{
			get
			{
				return this.strongAuthenticationRequirementField;
			}
			set
			{
				this.strongAuthenticationRequirementField = value;
			}
		}

		// Token: 0x0400481F RID: 18463
		private StrongAuthenticationRequirementValue strongAuthenticationRequirementField;
	}
}
