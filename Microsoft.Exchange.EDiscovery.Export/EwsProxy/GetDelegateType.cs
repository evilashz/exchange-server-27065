using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000361 RID: 865
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetDelegateType : BaseDelegateType
	{
		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06001BB3 RID: 7091 RVA: 0x0002996F File Offset: 0x00027B6F
		// (set) Token: 0x06001BB4 RID: 7092 RVA: 0x00029977 File Offset: 0x00027B77
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

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06001BB5 RID: 7093 RVA: 0x00029980 File Offset: 0x00027B80
		// (set) Token: 0x06001BB6 RID: 7094 RVA: 0x00029988 File Offset: 0x00027B88
		[XmlAttribute]
		public bool IncludePermissions
		{
			get
			{
				return this.includePermissionsField;
			}
			set
			{
				this.includePermissionsField = value;
			}
		}

		// Token: 0x04001276 RID: 4726
		private UserIdType[] userIdsField;

		// Token: 0x04001277 RID: 4727
		private bool includePermissionsField;
	}
}
