using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000911 RID: 2321
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class XmlValueStrongAuthenticationMethod
	{
		// Token: 0x1700277F RID: 10111
		// (get) Token: 0x06006F2C RID: 28460 RVA: 0x00176995 File Offset: 0x00174B95
		// (set) Token: 0x06006F2D RID: 28461 RVA: 0x0017699D File Offset: 0x00174B9D
		[XmlElement(Order = 0)]
		public StrongAuthenticationMethodValue StrongAuthenticationMethod
		{
			get
			{
				return this.strongAuthenticationMethodField;
			}
			set
			{
				this.strongAuthenticationMethodField = value;
			}
		}

		// Token: 0x0400482B RID: 18475
		private StrongAuthenticationMethodValue strongAuthenticationMethodField;
	}
}
