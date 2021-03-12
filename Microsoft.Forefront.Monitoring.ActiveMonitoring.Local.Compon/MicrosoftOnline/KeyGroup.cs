using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000185 RID: 389
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class KeyGroup : DirectoryObject
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x00020956 File Offset: 0x0001EB56
		// (set) Token: 0x060008F2 RID: 2290 RVA: 0x0002095E File Offset: 0x0001EB5E
		public DirectoryPropertyXmlAsymmetricKey AsymmetricKey
		{
			get
			{
				return this.asymmetricKeyField;
			}
			set
			{
				this.asymmetricKeyField = value;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x00020967 File Offset: 0x0001EB67
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x0002096F File Offset: 0x0001EB6F
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

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x00020978 File Offset: 0x0001EB78
		// (set) Token: 0x060008F6 RID: 2294 RVA: 0x00020980 File Offset: 0x0001EB80
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

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00020989 File Offset: 0x0001EB89
		// (set) Token: 0x060008F8 RID: 2296 RVA: 0x00020991 File Offset: 0x0001EB91
		public DirectoryPropertyXmlEncryptedSecretKey EncryptedSecretKey
		{
			get
			{
				return this.encryptedSecretKeyField;
			}
			set
			{
				this.encryptedSecretKeyField = value;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x0002099A File Offset: 0x0001EB9A
		// (set) Token: 0x060008FA RID: 2298 RVA: 0x000209A2 File Offset: 0x0001EBA2
		public DirectoryPropertyXmlKeyDescription KeyDescription
		{
			get
			{
				return this.keyDescriptionField;
			}
			set
			{
				this.keyDescriptionField = value;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x000209AB File Offset: 0x0001EBAB
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x000209B3 File Offset: 0x0001EBB3
		public DirectoryPropertyGuidSingle KeyGroupId
		{
			get
			{
				return this.keyGroupIdField;
			}
			set
			{
				this.keyGroupIdField = value;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x000209BC File Offset: 0x0001EBBC
		// (set) Token: 0x060008FE RID: 2302 RVA: 0x000209C4 File Offset: 0x0001EBC4
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

		// Token: 0x04000476 RID: 1142
		private DirectoryPropertyXmlAsymmetricKey asymmetricKeyField;

		// Token: 0x04000477 RID: 1143
		private DirectoryPropertyStringSingleLength1To1024 descriptionField;

		// Token: 0x04000478 RID: 1144
		private DirectoryPropertyStringSingleLength1To256 displayNameField;

		// Token: 0x04000479 RID: 1145
		private DirectoryPropertyXmlEncryptedSecretKey encryptedSecretKeyField;

		// Token: 0x0400047A RID: 1146
		private DirectoryPropertyXmlKeyDescription keyDescriptionField;

		// Token: 0x0400047B RID: 1147
		private DirectoryPropertyGuidSingle keyGroupIdField;

		// Token: 0x0400047C RID: 1148
		private XmlAttribute[] anyAttrField;
	}
}
