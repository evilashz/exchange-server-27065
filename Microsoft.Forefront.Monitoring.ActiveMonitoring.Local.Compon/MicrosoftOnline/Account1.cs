using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200019D RID: 413
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[XmlType(TypeName = "Account", Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class Account1 : DirectoryObject
	{
		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x00022EE0 File Offset: 0x000210E0
		// (set) Token: 0x06000D5E RID: 3422 RVA: 0x00022EE8 File Offset: 0x000210E8
		public DirectoryPropertyGuidSingle AccountId
		{
			get
			{
				return this.accountIdField;
			}
			set
			{
				this.accountIdField = value;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x00022EF1 File Offset: 0x000210F1
		// (set) Token: 0x06000D60 RID: 3424 RVA: 0x00022EF9 File Offset: 0x000210F9
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

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x00022F02 File Offset: 0x00021102
		// (set) Token: 0x06000D62 RID: 3426 RVA: 0x00022F0A File Offset: 0x0002110A
		public DirectoryPropertyStringLength1To256 BillingNotificationEmails
		{
			get
			{
				return this.billingNotificationEmailsField;
			}
			set
			{
				this.billingNotificationEmailsField = value;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x00022F13 File Offset: 0x00021113
		// (set) Token: 0x06000D64 RID: 3428 RVA: 0x00022F1B File Offset: 0x0002111B
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

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x00022F24 File Offset: 0x00021124
		// (set) Token: 0x06000D66 RID: 3430 RVA: 0x00022F2C File Offset: 0x0002112C
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

		// Token: 0x040006A0 RID: 1696
		private DirectoryPropertyGuidSingle accountIdField;

		// Token: 0x040006A1 RID: 1697
		private DirectoryPropertyBooleanSingle belongsToFirstLoginObjectSetField;

		// Token: 0x040006A2 RID: 1698
		private DirectoryPropertyStringLength1To256 billingNotificationEmailsField;

		// Token: 0x040006A3 RID: 1699
		private DirectoryPropertyStringSingleLength1To256 displayNameField;

		// Token: 0x040006A4 RID: 1700
		private XmlAttribute[] anyAttrField;
	}
}
