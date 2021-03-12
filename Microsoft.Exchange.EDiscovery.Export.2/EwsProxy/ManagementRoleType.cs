using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003B2 RID: 946
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlRoot("ManagementRole", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ManagementRoleType : SoapHeader
	{
		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06001D6C RID: 7532 RVA: 0x0002A800 File Offset: 0x00028A00
		// (set) Token: 0x06001D6D RID: 7533 RVA: 0x0002A808 File Offset: 0x00028A08
		[XmlArrayItem("Role", IsNullable = false)]
		public string[] UserRoles
		{
			get
			{
				return this.userRolesField;
			}
			set
			{
				this.userRolesField = value;
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06001D6E RID: 7534 RVA: 0x0002A811 File Offset: 0x00028A11
		// (set) Token: 0x06001D6F RID: 7535 RVA: 0x0002A819 File Offset: 0x00028A19
		[XmlArrayItem("Role", IsNullable = false)]
		public string[] ApplicationRoles
		{
			get
			{
				return this.applicationRolesField;
			}
			set
			{
				this.applicationRolesField = value;
			}
		}

		// Token: 0x04001378 RID: 4984
		private string[] userRolesField;

		// Token: 0x04001379 RID: 4985
		private string[] applicationRolesField;
	}
}
