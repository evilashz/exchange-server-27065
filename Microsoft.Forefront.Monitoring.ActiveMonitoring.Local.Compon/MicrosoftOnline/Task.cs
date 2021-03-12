using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200018D RID: 397
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[Serializable]
	public class Task : DirectoryObject
	{
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x00021766 File Offset: 0x0001F966
		// (set) Token: 0x06000A9A RID: 2714 RVA: 0x0002176E File Offset: 0x0001F96E
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

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x00021777 File Offset: 0x0001F977
		// (set) Token: 0x06000A9C RID: 2716 RVA: 0x0002177F File Offset: 0x0001F97F
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

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x00021788 File Offset: 0x0001F988
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x00021790 File Offset: 0x0001F990
		public DirectoryPropertyInt32SingleMin0 TaskName
		{
			get
			{
				return this.taskNameField;
			}
			set
			{
				this.taskNameField = value;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x00021799 File Offset: 0x0001F999
		// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x000217A1 File Offset: 0x0001F9A1
		public DirectoryPropertyGuidSingle TaskId
		{
			get
			{
				return this.taskIdField;
			}
			set
			{
				this.taskIdField = value;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x000217AA File Offset: 0x0001F9AA
		// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x000217B2 File Offset: 0x0001F9B2
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

		// Token: 0x04000546 RID: 1350
		private DirectoryPropertyStringSingleLength1To1024 descriptionField;

		// Token: 0x04000547 RID: 1351
		private DirectoryPropertyStringSingleLength1To256 displayNameField;

		// Token: 0x04000548 RID: 1352
		private DirectoryPropertyInt32SingleMin0 taskNameField;

		// Token: 0x04000549 RID: 1353
		private DirectoryPropertyGuidSingle taskIdField;

		// Token: 0x0400054A RID: 1354
		private XmlAttribute[] anyAttrField;
	}
}
