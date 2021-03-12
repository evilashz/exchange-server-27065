using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200031C RID: 796
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetUserPhotoType : BaseRequestType
	{
		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06001A10 RID: 6672 RVA: 0x00028BA2 File Offset: 0x00026DA2
		// (set) Token: 0x06001A11 RID: 6673 RVA: 0x00028BAA File Offset: 0x00026DAA
		public string Email
		{
			get
			{
				return this.emailField;
			}
			set
			{
				this.emailField = value;
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06001A12 RID: 6674 RVA: 0x00028BB3 File Offset: 0x00026DB3
		// (set) Token: 0x06001A13 RID: 6675 RVA: 0x00028BBB File Offset: 0x00026DBB
		public UserPhotoSizeType SizeRequested
		{
			get
			{
				return this.sizeRequestedField;
			}
			set
			{
				this.sizeRequestedField = value;
			}
		}

		// Token: 0x0400117F RID: 4479
		private string emailField;

		// Token: 0x04001180 RID: 4480
		private UserPhotoSizeType sizeRequestedField;
	}
}
