using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200018C RID: 396
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class TaskSet : DirectoryObject
	{
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x000216F8 File Offset: 0x0001F8F8
		// (set) Token: 0x06000A8D RID: 2701 RVA: 0x00021700 File Offset: 0x0001F900
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

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x00021709 File Offset: 0x0001F909
		// (set) Token: 0x06000A8F RID: 2703 RVA: 0x00021711 File Offset: 0x0001F911
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

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x0002171A File Offset: 0x0001F91A
		// (set) Token: 0x06000A91 RID: 2705 RVA: 0x00021722 File Offset: 0x0001F922
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

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x0002172B File Offset: 0x0001F92B
		// (set) Token: 0x06000A93 RID: 2707 RVA: 0x00021733 File Offset: 0x0001F933
		public DirectoryPropertyGuid TaskIdList
		{
			get
			{
				return this.taskIdListField;
			}
			set
			{
				this.taskIdListField = value;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x0002173C File Offset: 0x0001F93C
		// (set) Token: 0x06000A95 RID: 2709 RVA: 0x00021744 File Offset: 0x0001F944
		public DirectoryPropertyGuidSingle TaskSetId
		{
			get
			{
				return this.taskSetIdField;
			}
			set
			{
				this.taskSetIdField = value;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x0002174D File Offset: 0x0001F94D
		// (set) Token: 0x06000A97 RID: 2711 RVA: 0x00021755 File Offset: 0x0001F955
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

		// Token: 0x04000540 RID: 1344
		private DirectoryPropertyBooleanSingle builtinField;

		// Token: 0x04000541 RID: 1345
		private DirectoryPropertyStringSingleLength1To1024 descriptionField;

		// Token: 0x04000542 RID: 1346
		private DirectoryPropertyStringSingleLength1To256 displayNameField;

		// Token: 0x04000543 RID: 1347
		private DirectoryPropertyGuid taskIdListField;

		// Token: 0x04000544 RID: 1348
		private DirectoryPropertyGuidSingle taskSetIdField;

		// Token: 0x04000545 RID: 1349
		private XmlAttribute[] anyAttrField;
	}
}
