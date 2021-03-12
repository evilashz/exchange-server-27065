using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200083F RID: 2111
	[XmlInclude(typeof(DirectoryLinkInvitedBy))]
	[XmlInclude(typeof(UnauthOrig))]
	[XmlInclude(typeof(MSExchModeratedByLink))]
	[XmlInclude(typeof(DirectoryLinkAddressListObjectToGroup))]
	[XmlInclude(typeof(DirectoryLinkOwnerObjectToUserAndServicePrincipal))]
	[XmlInclude(typeof(Owner))]
	[XmlInclude(typeof(DirectoryLinkGroupBasedLicenseErrorOccuredUserToGroup))]
	[XmlInclude(typeof(DirectoryLinkServicePrincipalToServicePrincipal))]
	[XmlInclude(typeof(DelegationEntry))]
	[XmlInclude(typeof(DirectoryLinkUserToUser))]
	[XmlInclude(typeof(CloudMSExchDelegateListLink))]
	[XmlInclude(typeof(MSExchDelegateListLink))]
	[XmlInclude(typeof(DirectoryLinkAddressListObjectToGroupAndUser))]
	[XmlInclude(typeof(PublicDelegates))]
	[XmlInclude(typeof(DirectoryLinkGroupToPerson))]
	[XmlInclude(typeof(MSExchCoManagedByLink))]
	[XmlInclude(typeof(ManagedBy))]
	[XmlInclude(typeof(DirectoryLinkUserToServicePrincipal))]
	[XmlInclude(typeof(InvitedBy))]
	[XmlInclude(typeof(DirectoryLinkAllowAccessTo))]
	[XmlInclude(typeof(AllowAccessTo))]
	[XmlInclude(typeof(DirectoryLinkPendingMember))]
	[XmlInclude(typeof(PendingMember))]
	[XmlInclude(typeof(DirectoryLinkMemberObjectToAddressListObject))]
	[XmlInclude(typeof(Member))]
	[XmlInclude(typeof(DirectoryLinkPersonToPerson))]
	[XmlInclude(typeof(Manager))]
	[XmlInclude(typeof(DirectoryLinkDeviceToUser))]
	[XmlInclude(typeof(RegisteredOwner))]
	[XmlInclude(typeof(RegisteredUsers))]
	[XmlInclude(typeof(DirectoryLinkAddressListObjectToAddressListObject))]
	[XmlInclude(typeof(CloudPublicDelegates))]
	[XmlInclude(typeof(DirectoryLinkAddressListObjectToPerson))]
	[XmlInclude(typeof(MSExchBypassModerationLink))]
	[XmlInclude(typeof(AuthOrig))]
	[XmlInclude(typeof(MSExchBypassModerationFromDLMembersLink))]
	[XmlInclude(typeof(DLMemSubmitPerms))]
	[XmlInclude(typeof(DLMemRejectPerms))]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public abstract class DirectoryLink
	{
		// Token: 0x060068CA RID: 26826
		public abstract DirectoryObjectClass GetSourceClass();

		// Token: 0x060068CB RID: 26827
		public abstract void SetSourceClass(DirectoryObjectClass objectClass);

		// Token: 0x060068CC RID: 26828
		public abstract DirectoryObjectClass GetTargetClass();

		// Token: 0x060068CD RID: 26829
		public abstract void SetTargetClass(DirectoryObjectClass objectClass);

		// Token: 0x060068CE RID: 26830 RVA: 0x001713C2 File Offset: 0x0016F5C2
		protected static object ConvertEnums(Type targetType, string name)
		{
			return Enum.Parse(targetType, name);
		}

		// Token: 0x060068CF RID: 26831 RVA: 0x001713CB File Offset: 0x0016F5CB
		public DirectoryLink()
		{
			this.deletedField = false;
		}

		// Token: 0x1700251A RID: 9498
		// (get) Token: 0x060068D0 RID: 26832 RVA: 0x001713DA File Offset: 0x0016F5DA
		// (set) Token: 0x060068D1 RID: 26833 RVA: 0x001713E2 File Offset: 0x0016F5E2
		[XmlAttribute]
		public string ContextId
		{
			get
			{
				return this.contextIdField;
			}
			set
			{
				this.contextIdField = value;
			}
		}

		// Token: 0x1700251B RID: 9499
		// (get) Token: 0x060068D2 RID: 26834 RVA: 0x001713EB File Offset: 0x0016F5EB
		// (set) Token: 0x060068D3 RID: 26835 RVA: 0x001713F3 File Offset: 0x0016F5F3
		[XmlAttribute]
		public string SourceId
		{
			get
			{
				return this.sourceIdField;
			}
			set
			{
				this.sourceIdField = value;
			}
		}

		// Token: 0x1700251C RID: 9500
		// (get) Token: 0x060068D4 RID: 26836 RVA: 0x001713FC File Offset: 0x0016F5FC
		// (set) Token: 0x060068D5 RID: 26837 RVA: 0x00171404 File Offset: 0x0016F604
		[XmlAttribute]
		public string TargetId
		{
			get
			{
				return this.targetIdField;
			}
			set
			{
				this.targetIdField = value;
			}
		}

		// Token: 0x1700251D RID: 9501
		// (get) Token: 0x060068D6 RID: 26838 RVA: 0x0017140D File Offset: 0x0016F60D
		// (set) Token: 0x060068D7 RID: 26839 RVA: 0x00171415 File Offset: 0x0016F615
		[XmlAttribute]
		[DefaultValue(false)]
		public bool Deleted
		{
			get
			{
				return this.deletedField;
			}
			set
			{
				this.deletedField = value;
			}
		}

		// Token: 0x1700251E RID: 9502
		// (get) Token: 0x060068D8 RID: 26840 RVA: 0x0017141E File Offset: 0x0016F61E
		// (set) Token: 0x060068D9 RID: 26841 RVA: 0x00171426 File Offset: 0x0016F626
		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr
		{
			get
			{
				return this.anyAttrField;
			}
			set
			{
				this.anyAttrField = value;
			}
		}

		// Token: 0x040044DF RID: 17631
		private string contextIdField;

		// Token: 0x040044E0 RID: 17632
		private string sourceIdField;

		// Token: 0x040044E1 RID: 17633
		private string targetIdField;

		// Token: 0x040044E2 RID: 17634
		private bool deletedField;

		// Token: 0x040044E3 RID: 17635
		private XmlAttribute[] anyAttrField;
	}
}
