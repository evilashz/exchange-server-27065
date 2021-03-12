using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000854 RID: 2132
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class KeyGroup : DirectoryObject
	{
		// Token: 0x06006B01 RID: 27393 RVA: 0x001732B8 File Offset: 0x001714B8
		internal override void ForEachProperty(IPropertyProcessor processor)
		{
		}

		// Token: 0x17002616 RID: 9750
		// (get) Token: 0x06006B02 RID: 27394 RVA: 0x001732BA File Offset: 0x001714BA
		// (set) Token: 0x06006B03 RID: 27395 RVA: 0x001732C2 File Offset: 0x001714C2
		[XmlElement(Order = 0)]
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

		// Token: 0x17002617 RID: 9751
		// (get) Token: 0x06006B04 RID: 27396 RVA: 0x001732CB File Offset: 0x001714CB
		// (set) Token: 0x06006B05 RID: 27397 RVA: 0x001732D3 File Offset: 0x001714D3
		[XmlElement(Order = 1)]
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

		// Token: 0x17002618 RID: 9752
		// (get) Token: 0x06006B06 RID: 27398 RVA: 0x001732DC File Offset: 0x001714DC
		// (set) Token: 0x06006B07 RID: 27399 RVA: 0x001732E4 File Offset: 0x001714E4
		[XmlElement(Order = 2)]
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

		// Token: 0x17002619 RID: 9753
		// (get) Token: 0x06006B08 RID: 27400 RVA: 0x001732ED File Offset: 0x001714ED
		// (set) Token: 0x06006B09 RID: 27401 RVA: 0x001732F5 File Offset: 0x001714F5
		[XmlElement(Order = 3)]
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

		// Token: 0x1700261A RID: 9754
		// (get) Token: 0x06006B0A RID: 27402 RVA: 0x001732FE File Offset: 0x001714FE
		// (set) Token: 0x06006B0B RID: 27403 RVA: 0x00173306 File Offset: 0x00171506
		[XmlElement(Order = 4)]
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

		// Token: 0x1700261B RID: 9755
		// (get) Token: 0x06006B0C RID: 27404 RVA: 0x0017330F File Offset: 0x0017150F
		// (set) Token: 0x06006B0D RID: 27405 RVA: 0x00173317 File Offset: 0x00171517
		[XmlElement(Order = 5)]
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

		// Token: 0x1700261C RID: 9756
		// (get) Token: 0x06006B0E RID: 27406 RVA: 0x00173320 File Offset: 0x00171520
		// (set) Token: 0x06006B0F RID: 27407 RVA: 0x00173328 File Offset: 0x00171528
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

		// Token: 0x040045DB RID: 17883
		private DirectoryPropertyXmlAsymmetricKey asymmetricKeyField;

		// Token: 0x040045DC RID: 17884
		private DirectoryPropertyStringSingleLength1To1024 descriptionField;

		// Token: 0x040045DD RID: 17885
		private DirectoryPropertyStringSingleLength1To256 displayNameField;

		// Token: 0x040045DE RID: 17886
		private DirectoryPropertyXmlEncryptedSecretKey encryptedSecretKeyField;

		// Token: 0x040045DF RID: 17887
		private DirectoryPropertyXmlKeyDescription keyDescriptionField;

		// Token: 0x040045E0 RID: 17888
		private DirectoryPropertyGuidSingle keyGroupIdField;

		// Token: 0x040045E1 RID: 17889
		private XmlAttribute[] anyAttrField;
	}
}
