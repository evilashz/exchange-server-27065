using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000196 RID: 406
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class Role : DirectoryObject
	{
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00021CDC File Offset: 0x0001FEDC
		// (set) Token: 0x06000B3F RID: 2879 RVA: 0x00021CE4 File Offset: 0x0001FEE4
		public DirectoryPropertyBooleanSingle BelongsToFirstLoginObjectSet
		{
			get
			{
				return this.belongsToFirstLoginObjectSetField;
			}
			set
			{
				this.belongsToFirstLoginObjectSetField = value;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x00021CED File Offset: 0x0001FEED
		// (set) Token: 0x06000B41 RID: 2881 RVA: 0x00021CF5 File Offset: 0x0001FEF5
		public DirectoryPropertyBooleanSingle Builtin
		{
			get
			{
				return this.builtinField;
			}
			set
			{
				this.builtinField = value;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x00021CFE File Offset: 0x0001FEFE
		// (set) Token: 0x06000B43 RID: 2883 RVA: 0x00021D06 File Offset: 0x0001FF06
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

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x00021D0F File Offset: 0x0001FF0F
		// (set) Token: 0x06000B45 RID: 2885 RVA: 0x00021D17 File Offset: 0x0001FF17
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

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x00021D20 File Offset: 0x0001FF20
		// (set) Token: 0x06000B47 RID: 2887 RVA: 0x00021D28 File Offset: 0x0001FF28
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

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x00021D31 File Offset: 0x0001FF31
		// (set) Token: 0x06000B49 RID: 2889 RVA: 0x00021D39 File Offset: 0x0001FF39
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

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x00021D42 File Offset: 0x0001FF42
		// (set) Token: 0x06000B4B RID: 2891 RVA: 0x00021D4A File Offset: 0x0001FF4A
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

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x00021D53 File Offset: 0x0001FF53
		// (set) Token: 0x06000B4D RID: 2893 RVA: 0x00021D5B File Offset: 0x0001FF5B
		public DirectoryPropertyXmlServiceInfo ServiceInfo
		{
			get
			{
				return this.serviceInfoField;
			}
			set
			{
				this.serviceInfoField = value;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x00021D64 File Offset: 0x0001FF64
		// (set) Token: 0x06000B4F RID: 2895 RVA: 0x00021D6C File Offset: 0x0001FF6C
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

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x00021D75 File Offset: 0x0001FF75
		// (set) Token: 0x06000B51 RID: 2897 RVA: 0x00021D7D File Offset: 0x0001FF7D
		public DirectoryPropertyXmlValidationError ValidationError
		{
			get
			{
				return this.validationErrorField;
			}
			set
			{
				this.validationErrorField = value;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00021D86 File Offset: 0x0001FF86
		// (set) Token: 0x06000B53 RID: 2899 RVA: 0x00021D8E File Offset: 0x0001FF8E
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

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x00021D97 File Offset: 0x0001FF97
		// (set) Token: 0x06000B55 RID: 2901 RVA: 0x00021D9F File Offset: 0x0001FF9F
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

		// Token: 0x04000594 RID: 1428
		private DirectoryPropertyBooleanSingle belongsToFirstLoginObjectSetField;

		// Token: 0x04000595 RID: 1429
		private DirectoryPropertyBooleanSingle builtinField;

		// Token: 0x04000596 RID: 1430
		private DirectoryPropertyStringSingleLength1To1024 descriptionField;

		// Token: 0x04000597 RID: 1431
		private DirectoryPropertyStringSingleLength1To256 displayNameField;

		// Token: 0x04000598 RID: 1432
		private DirectoryPropertyStringSingleMailNickname mailNicknameField;

		// Token: 0x04000599 RID: 1433
		private DirectoryPropertyBooleanSingle roleDisabledField;

		// Token: 0x0400059A RID: 1434
		private DirectoryPropertyGuidSingle roleTemplateIdField;

		// Token: 0x0400059B RID: 1435
		private DirectoryPropertyXmlServiceInfo serviceInfoField;

		// Token: 0x0400059C RID: 1436
		private DirectoryPropertyXmlTaskSetScopeReference taskSetScopeReferenceField;

		// Token: 0x0400059D RID: 1437
		private DirectoryPropertyXmlValidationError validationErrorField;

		// Token: 0x0400059E RID: 1438
		private DirectoryPropertyStringSingleLength1To40 wellKnownObjectField;

		// Token: 0x0400059F RID: 1439
		private XmlAttribute[] anyAttrField;
	}
}
