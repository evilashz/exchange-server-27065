using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200037F RID: 895
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class UpdateGroupMailboxType : BaseRequestType
	{
		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06001C4B RID: 7243 RVA: 0x00029E74 File Offset: 0x00028074
		// (set) Token: 0x06001C4C RID: 7244 RVA: 0x00029E7C File Offset: 0x0002807C
		public string GroupSmtpAddress
		{
			get
			{
				return this.groupSmtpAddressField;
			}
			set
			{
				this.groupSmtpAddressField = value;
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06001C4D RID: 7245 RVA: 0x00029E85 File Offset: 0x00028085
		// (set) Token: 0x06001C4E RID: 7246 RVA: 0x00029E8D File Offset: 0x0002808D
		public string ExecutingUserSmtpAddress
		{
			get
			{
				return this.executingUserSmtpAddressField;
			}
			set
			{
				this.executingUserSmtpAddressField = value;
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06001C4F RID: 7247 RVA: 0x00029E96 File Offset: 0x00028096
		// (set) Token: 0x06001C50 RID: 7248 RVA: 0x00029E9E File Offset: 0x0002809E
		public string DomainController
		{
			get
			{
				return this.domainControllerField;
			}
			set
			{
				this.domainControllerField = value;
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06001C51 RID: 7249 RVA: 0x00029EA7 File Offset: 0x000280A7
		// (set) Token: 0x06001C52 RID: 7250 RVA: 0x00029EAF File Offset: 0x000280AF
		public GroupMailboxConfigurationActionType ForceConfigurationAction
		{
			get
			{
				return this.forceConfigurationActionField;
			}
			set
			{
				this.forceConfigurationActionField = value;
			}
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06001C53 RID: 7251 RVA: 0x00029EB8 File Offset: 0x000280B8
		// (set) Token: 0x06001C54 RID: 7252 RVA: 0x00029EC0 File Offset: 0x000280C0
		public GroupMemberIdentifierType MemberIdentifierType
		{
			get
			{
				return this.memberIdentifierTypeField;
			}
			set
			{
				this.memberIdentifierTypeField = value;
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06001C55 RID: 7253 RVA: 0x00029EC9 File Offset: 0x000280C9
		// (set) Token: 0x06001C56 RID: 7254 RVA: 0x00029ED1 File Offset: 0x000280D1
		[XmlIgnore]
		public bool MemberIdentifierTypeSpecified
		{
			get
			{
				return this.memberIdentifierTypeFieldSpecified;
			}
			set
			{
				this.memberIdentifierTypeFieldSpecified = value;
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06001C57 RID: 7255 RVA: 0x00029EDA File Offset: 0x000280DA
		// (set) Token: 0x06001C58 RID: 7256 RVA: 0x00029EE2 File Offset: 0x000280E2
		[XmlArrayItem("String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] AddedMembers
		{
			get
			{
				return this.addedMembersField;
			}
			set
			{
				this.addedMembersField = value;
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06001C59 RID: 7257 RVA: 0x00029EEB File Offset: 0x000280EB
		// (set) Token: 0x06001C5A RID: 7258 RVA: 0x00029EF3 File Offset: 0x000280F3
		[XmlArrayItem("String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] RemovedMembers
		{
			get
			{
				return this.removedMembersField;
			}
			set
			{
				this.removedMembersField = value;
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06001C5B RID: 7259 RVA: 0x00029EFC File Offset: 0x000280FC
		// (set) Token: 0x06001C5C RID: 7260 RVA: 0x00029F04 File Offset: 0x00028104
		[XmlArrayItem("String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] AddedPendingMembers
		{
			get
			{
				return this.addedPendingMembersField;
			}
			set
			{
				this.addedPendingMembersField = value;
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06001C5D RID: 7261 RVA: 0x00029F0D File Offset: 0x0002810D
		// (set) Token: 0x06001C5E RID: 7262 RVA: 0x00029F15 File Offset: 0x00028115
		[XmlArrayItem("String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] RemovedPendingMembers
		{
			get
			{
				return this.removedPendingMembersField;
			}
			set
			{
				this.removedPendingMembersField = value;
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06001C5F RID: 7263 RVA: 0x00029F1E File Offset: 0x0002811E
		// (set) Token: 0x06001C60 RID: 7264 RVA: 0x00029F26 File Offset: 0x00028126
		public int PermissionsVersion
		{
			get
			{
				return this.permissionsVersionField;
			}
			set
			{
				this.permissionsVersionField = value;
			}
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06001C61 RID: 7265 RVA: 0x00029F2F File Offset: 0x0002812F
		// (set) Token: 0x06001C62 RID: 7266 RVA: 0x00029F37 File Offset: 0x00028137
		[XmlIgnore]
		public bool PermissionsVersionSpecified
		{
			get
			{
				return this.permissionsVersionFieldSpecified;
			}
			set
			{
				this.permissionsVersionFieldSpecified = value;
			}
		}

		// Token: 0x040012BC RID: 4796
		private string groupSmtpAddressField;

		// Token: 0x040012BD RID: 4797
		private string executingUserSmtpAddressField;

		// Token: 0x040012BE RID: 4798
		private string domainControllerField;

		// Token: 0x040012BF RID: 4799
		private GroupMailboxConfigurationActionType forceConfigurationActionField;

		// Token: 0x040012C0 RID: 4800
		private GroupMemberIdentifierType memberIdentifierTypeField;

		// Token: 0x040012C1 RID: 4801
		private bool memberIdentifierTypeFieldSpecified;

		// Token: 0x040012C2 RID: 4802
		private string[] addedMembersField;

		// Token: 0x040012C3 RID: 4803
		private string[] removedMembersField;

		// Token: 0x040012C4 RID: 4804
		private string[] addedPendingMembersField;

		// Token: 0x040012C5 RID: 4805
		private string[] removedPendingMembersField;

		// Token: 0x040012C6 RID: 4806
		private int permissionsVersionField;

		// Token: 0x040012C7 RID: 4807
		private bool permissionsVersionFieldSpecified;
	}
}
