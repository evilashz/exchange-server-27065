using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000195 RID: 405
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class RoleTemplate : DirectoryObject
	{
		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x00021C4C File Offset: 0x0001FE4C
		// (set) Token: 0x06000B2E RID: 2862 RVA: 0x00021C54 File Offset: 0x0001FE54
		public DirectoryPropertyStringSingleLength1To1024 Description
		{
			get
			{
				return this.descriptionField;
			}
			set
			{
				this.descriptionField = value;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x00021C5D File Offset: 0x0001FE5D
		// (set) Token: 0x06000B30 RID: 2864 RVA: 0x00021C65 File Offset: 0x0001FE65
		public DirectoryPropertyStringSingleLength1To256 DisplayName
		{
			get
			{
				return this.displayNameField;
			}
			set
			{
				this.displayNameField = value;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x00021C6E File Offset: 0x0001FE6E
		// (set) Token: 0x06000B32 RID: 2866 RVA: 0x00021C76 File Offset: 0x0001FE76
		public DirectoryPropertyStringSingleMailNickname MailNickname
		{
			get
			{
				return this.mailNicknameField;
			}
			set
			{
				this.mailNicknameField = value;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x00021C7F File Offset: 0x0001FE7F
		// (set) Token: 0x06000B34 RID: 2868 RVA: 0x00021C87 File Offset: 0x0001FE87
		public DirectoryPropertyGuidSingle RoleTemplateId
		{
			get
			{
				return this.roleTemplateIdField;
			}
			set
			{
				this.roleTemplateIdField = value;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x00021C90 File Offset: 0x0001FE90
		// (set) Token: 0x06000B36 RID: 2870 RVA: 0x00021C98 File Offset: 0x0001FE98
		public DirectoryPropertyBooleanSingle RoleDisabled
		{
			get
			{
				return this.roleDisabledField;
			}
			set
			{
				this.roleDisabledField = value;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x00021CA1 File Offset: 0x0001FEA1
		// (set) Token: 0x06000B38 RID: 2872 RVA: 0x00021CA9 File Offset: 0x0001FEA9
		public DirectoryPropertyStringSingleLength1To40 WellKnownObject
		{
			get
			{
				return this.wellKnownObjectField;
			}
			set
			{
				this.wellKnownObjectField = value;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x00021CB2 File Offset: 0x0001FEB2
		// (set) Token: 0x06000B3A RID: 2874 RVA: 0x00021CBA File Offset: 0x0001FEBA
		public DirectoryPropertyXmlTaskSetScopeReference TaskSetScopeReference
		{
			get
			{
				return this.taskSetScopeReferenceField;
			}
			set
			{
				this.taskSetScopeReferenceField = value;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x00021CC3 File Offset: 0x0001FEC3
		// (set) Token: 0x06000B3C RID: 2876 RVA: 0x00021CCB File Offset: 0x0001FECB
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

		// Token: 0x0400058C RID: 1420
		private DirectoryPropertyStringSingleLength1To1024 descriptionField;

		// Token: 0x0400058D RID: 1421
		private DirectoryPropertyStringSingleLength1To256 displayNameField;

		// Token: 0x0400058E RID: 1422
		private DirectoryPropertyStringSingleMailNickname mailNicknameField;

		// Token: 0x0400058F RID: 1423
		private DirectoryPropertyGuidSingle roleTemplateIdField;

		// Token: 0x04000590 RID: 1424
		private DirectoryPropertyBooleanSingle roleDisabledField;

		// Token: 0x04000591 RID: 1425
		private DirectoryPropertyStringSingleLength1To40 wellKnownObjectField;

		// Token: 0x04000592 RID: 1426
		private DirectoryPropertyXmlTaskSetScopeReference taskSetScopeReferenceField;

		// Token: 0x04000593 RID: 1427
		private XmlAttribute[] anyAttrField;
	}
}
