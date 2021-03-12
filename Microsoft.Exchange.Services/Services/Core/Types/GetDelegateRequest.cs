using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000431 RID: 1073
	[XmlType("GetDelegateType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetDelegateRequest : BaseDelegateRequest
	{
		// Token: 0x06001F7E RID: 8062 RVA: 0x000A0DB3 File Offset: 0x0009EFB3
		public GetDelegateRequest() : base(false)
		{
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001F7F RID: 8063 RVA: 0x000A0DBC File Offset: 0x0009EFBC
		// (set) Token: 0x06001F80 RID: 8064 RVA: 0x000A0DC4 File Offset: 0x0009EFC4
		[XmlArrayItem("UserId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public UserId[] UserIds
		{
			get
			{
				return this.userIds;
			}
			set
			{
				this.userIds = value;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001F81 RID: 8065 RVA: 0x000A0DCD File Offset: 0x0009EFCD
		// (set) Token: 0x06001F82 RID: 8066 RVA: 0x000A0DD5 File Offset: 0x0009EFD5
		[XmlAttribute("IncludePermissions")]
		public bool IncludePermissions
		{
			get
			{
				return this.includePermissions;
			}
			set
			{
				this.includePermissions = value;
			}
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x000A0DDE File Offset: 0x0009EFDE
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetDelegate(callContext, this);
		}

		// Token: 0x040013E7 RID: 5095
		private UserId[] userIds;

		// Token: 0x040013E8 RID: 5096
		private bool includePermissions;
	}
}
