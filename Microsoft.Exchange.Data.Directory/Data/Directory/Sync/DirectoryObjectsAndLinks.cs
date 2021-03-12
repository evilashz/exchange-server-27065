using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000868 RID: 2152
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryObjectsAndLinks
	{
		// Token: 0x170026E7 RID: 9959
		// (get) Token: 0x06006CD6 RID: 27862 RVA: 0x00174DD5 File Offset: 0x00172FD5
		[XmlIgnore]
		public Guid BatchId
		{
			get
			{
				return this.batchId;
			}
		}

		// Token: 0x170026E8 RID: 9960
		// (get) Token: 0x06006CD7 RID: 27863 RVA: 0x00174DDD File Offset: 0x00172FDD
		// (set) Token: 0x06006CD8 RID: 27864 RVA: 0x00174DE5 File Offset: 0x00172FE5
		[XmlArray(Order = 0)]
		[XmlArrayItem(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11", IsNullable = false)]
		public DirectoryObject[] Objects
		{
			get
			{
				return this.objectsField;
			}
			set
			{
				this.objectsField = value;
			}
		}

		// Token: 0x170026E9 RID: 9961
		// (get) Token: 0x06006CD9 RID: 27865 RVA: 0x00174DEE File Offset: 0x00172FEE
		// (set) Token: 0x06006CDA RID: 27866 RVA: 0x00174DF6 File Offset: 0x00172FF6
		[XmlArrayItem(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11", IsNullable = false)]
		[XmlArray(Order = 1)]
		public DirectoryLink[] Links
		{
			get
			{
				return this.linksField;
			}
			set
			{
				this.linksField = value;
			}
		}

		// Token: 0x170026EA RID: 9962
		// (get) Token: 0x06006CDB RID: 27867 RVA: 0x00174DFF File Offset: 0x00172FFF
		// (set) Token: 0x06006CDC RID: 27868 RVA: 0x00174E07 File Offset: 0x00173007
		[XmlArray(Order = 2)]
		[XmlArrayItem(IsNullable = false)]
		public DirectoryObjectError[] Errors
		{
			get
			{
				return this.errorsField;
			}
			set
			{
				this.errorsField = value;
			}
		}

		// Token: 0x170026EB RID: 9963
		// (get) Token: 0x06006CDD RID: 27869 RVA: 0x00174E10 File Offset: 0x00173010
		// (set) Token: 0x06006CDE RID: 27870 RVA: 0x00174E18 File Offset: 0x00173018
		[XmlElement(DataType = "base64Binary", Order = 3)]
		public byte[] NextPageToken
		{
			get
			{
				return this.nextPageTokenField;
			}
			set
			{
				this.nextPageTokenField = value;
			}
		}

		// Token: 0x170026EC RID: 9964
		// (get) Token: 0x06006CDF RID: 27871 RVA: 0x00174E21 File Offset: 0x00173021
		// (set) Token: 0x06006CE0 RID: 27872 RVA: 0x00174E29 File Offset: 0x00173029
		[XmlElement(Order = 4)]
		public bool More
		{
			get
			{
				return this.moreField;
			}
			set
			{
				this.moreField = value;
			}
		}

		// Token: 0x040046C4 RID: 18116
		private Guid batchId = Guid.NewGuid();

		// Token: 0x040046C5 RID: 18117
		private DirectoryObject[] objectsField;

		// Token: 0x040046C6 RID: 18118
		private DirectoryLink[] linksField;

		// Token: 0x040046C7 RID: 18119
		private DirectoryObjectError[] errorsField;

		// Token: 0x040046C8 RID: 18120
		private byte[] nextPageTokenField;

		// Token: 0x040046C9 RID: 18121
		private bool moreField;
	}
}
