using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000460 RID: 1120
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[Serializable]
	public class UpdateGroupMailboxType : BaseRequestType
	{
		// Token: 0x0400170E RID: 5902
		public string GroupSmtpAddress;

		// Token: 0x0400170F RID: 5903
		public string ExecutingUserSmtpAddress;

		// Token: 0x04001710 RID: 5904
		public string DomainController;

		// Token: 0x04001711 RID: 5905
		public GroupMailboxConfigurationActionType ForceConfigurationAction;

		// Token: 0x04001712 RID: 5906
		public GroupMemberIdentifierType MemberIdentifierType;

		// Token: 0x04001713 RID: 5907
		[XmlIgnore]
		public bool MemberIdentifierTypeSpecified;

		// Token: 0x04001714 RID: 5908
		[XmlArrayItem("String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] AddedMembers;

		// Token: 0x04001715 RID: 5909
		[XmlArrayItem("String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] RemovedMembers;

		// Token: 0x04001716 RID: 5910
		[XmlArrayItem("String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] AddedPendingMembers;

		// Token: 0x04001717 RID: 5911
		[XmlArrayItem("String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] RemovedPendingMembers;

		// Token: 0x04001718 RID: 5912
		public int PermissionsVersion;

		// Token: 0x04001719 RID: 5913
		[XmlIgnore]
		public bool PermissionsVersionSpecified;
	}
}
