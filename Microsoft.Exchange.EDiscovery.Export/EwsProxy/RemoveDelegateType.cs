using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200035F RID: 863
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[Serializable]
	public class RemoveDelegateType : BaseDelegateType
	{
		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06001BA9 RID: 7081 RVA: 0x0002991B File Offset: 0x00027B1B
		// (set) Token: 0x06001BAA RID: 7082 RVA: 0x00029923 File Offset: 0x00027B23
		[XmlArrayItem("UserId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public UserIdType[] UserIds
		{
			get
			{
				return this.userIdsField;
			}
			set
			{
				this.userIdsField = value;
			}
		}

		// Token: 0x04001272 RID: 4722
		private UserIdType[] userIdsField;
	}
}
