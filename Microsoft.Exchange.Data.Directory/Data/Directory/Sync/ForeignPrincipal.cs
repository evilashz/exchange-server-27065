using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000850 RID: 2128
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class ForeignPrincipal : DirectoryObject
	{
		// Token: 0x06006A65 RID: 27237 RVA: 0x001729A0 File Offset: 0x00170BA0
		internal override void ForEachProperty(IPropertyProcessor processor)
		{
			processor.Process<DirectoryPropertyStringSingleLength1To1024>(SyncForeignPrincipalSchema.Description, ref this.descriptionField);
			processor.Process<DirectoryPropertyStringSingleLength1To256>(SyncForeignPrincipalSchema.DisplayName, ref this.displayNameField);
			processor.Process<DirectoryPropertyGuidSingle>(SyncForeignPrincipalSchema.LinkedPartnerGroupId, ref this.foreignPrincipalIdField);
			processor.Process<DirectoryPropertyGuidSingle>(SyncForeignPrincipalSchema.LinkedPartnerOrganizationId, ref this.foreignContextIdField);
		}

		// Token: 0x170025CD RID: 9677
		// (get) Token: 0x06006A66 RID: 27238 RVA: 0x001729F1 File Offset: 0x00170BF1
		// (set) Token: 0x06006A67 RID: 27239 RVA: 0x001729F9 File Offset: 0x00170BF9
		[XmlElement(Order = 0)]
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

		// Token: 0x170025CE RID: 9678
		// (get) Token: 0x06006A68 RID: 27240 RVA: 0x00172A02 File Offset: 0x00170C02
		// (set) Token: 0x06006A69 RID: 27241 RVA: 0x00172A0A File Offset: 0x00170C0A
		[XmlElement(Order = 1)]
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

		// Token: 0x170025CF RID: 9679
		// (get) Token: 0x06006A6A RID: 27242 RVA: 0x00172A13 File Offset: 0x00170C13
		// (set) Token: 0x06006A6B RID: 27243 RVA: 0x00172A1B File Offset: 0x00170C1B
		[XmlElement(Order = 2)]
		public DirectoryPropertyGuidSingle ForeignContextId
		{
			get
			{
				return this.foreignContextIdField;
			}
			set
			{
				this.foreignContextIdField = value;
			}
		}

		// Token: 0x170025D0 RID: 9680
		// (get) Token: 0x06006A6C RID: 27244 RVA: 0x00172A24 File Offset: 0x00170C24
		// (set) Token: 0x06006A6D RID: 27245 RVA: 0x00172A2C File Offset: 0x00170C2C
		[XmlElement(Order = 3)]
		public DirectoryPropertyGuidSingle ForeignPrincipalId
		{
			get
			{
				return this.foreignPrincipalIdField;
			}
			set
			{
				this.foreignPrincipalIdField = value;
			}
		}

		// Token: 0x170025D1 RID: 9681
		// (get) Token: 0x06006A6E RID: 27246 RVA: 0x00172A35 File Offset: 0x00170C35
		// (set) Token: 0x06006A6F RID: 27247 RVA: 0x00172A3D File Offset: 0x00170C3D
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

		// Token: 0x04004592 RID: 17810
		private DirectoryPropertyStringSingleLength1To1024 descriptionField;

		// Token: 0x04004593 RID: 17811
		private DirectoryPropertyStringSingleLength1To256 displayNameField;

		// Token: 0x04004594 RID: 17812
		private DirectoryPropertyGuidSingle foreignContextIdField;

		// Token: 0x04004595 RID: 17813
		private DirectoryPropertyGuidSingle foreignPrincipalIdField;

		// Token: 0x04004596 RID: 17814
		private XmlAttribute[] anyAttrField;
	}
}
