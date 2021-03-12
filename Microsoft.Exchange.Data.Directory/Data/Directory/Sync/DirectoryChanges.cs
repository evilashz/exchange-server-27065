using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000863 RID: 2147
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
	[DebuggerStepThrough]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class DirectoryChanges
	{
		// Token: 0x170026DC RID: 9948
		// (get) Token: 0x06006CB9 RID: 27833 RVA: 0x00174CA6 File Offset: 0x00172EA6
		[XmlIgnore]
		public Guid BatchId
		{
			get
			{
				return this.batchId;
			}
		}

		// Token: 0x06006CBA RID: 27834 RVA: 0x00174CAE File Offset: 0x00172EAE
		public DirectoryChanges()
		{
			this.operationResultCodeField = OperationResultCode.Success;
		}

		// Token: 0x170026DD RID: 9949
		// (get) Token: 0x06006CBB RID: 27835 RVA: 0x00174CC8 File Offset: 0x00172EC8
		// (set) Token: 0x06006CBC RID: 27836 RVA: 0x00174CD0 File Offset: 0x00172ED0
		[XmlArrayItem(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11", IsNullable = false)]
		[XmlArray(Order = 0)]
		public DirectoryContext[] Contexts
		{
			get
			{
				return this.contextsField;
			}
			set
			{
				this.contextsField = value;
			}
		}

		// Token: 0x170026DE RID: 9950
		// (get) Token: 0x06006CBD RID: 27837 RVA: 0x00174CD9 File Offset: 0x00172ED9
		// (set) Token: 0x06006CBE RID: 27838 RVA: 0x00174CE1 File Offset: 0x00172EE1
		[XmlArray(Order = 1)]
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

		// Token: 0x170026DF RID: 9951
		// (get) Token: 0x06006CBF RID: 27839 RVA: 0x00174CEA File Offset: 0x00172EEA
		// (set) Token: 0x06006CC0 RID: 27840 RVA: 0x00174CF2 File Offset: 0x00172EF2
		[XmlArrayItem(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11", IsNullable = false)]
		[XmlArray(Order = 2)]
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

		// Token: 0x170026E0 RID: 9952
		// (get) Token: 0x06006CC1 RID: 27841 RVA: 0x00174CFB File Offset: 0x00172EFB
		// (set) Token: 0x06006CC2 RID: 27842 RVA: 0x00174D03 File Offset: 0x00172F03
		[XmlElement(DataType = "base64Binary", Order = 3)]
		public byte[] NextCookie
		{
			get
			{
				return this.nextCookieField;
			}
			set
			{
				this.nextCookieField = value;
			}
		}

		// Token: 0x170026E1 RID: 9953
		// (get) Token: 0x06006CC3 RID: 27843 RVA: 0x00174D0C File Offset: 0x00172F0C
		// (set) Token: 0x06006CC4 RID: 27844 RVA: 0x00174D14 File Offset: 0x00172F14
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

		// Token: 0x170026E2 RID: 9954
		// (get) Token: 0x06006CC5 RID: 27845 RVA: 0x00174D1D File Offset: 0x00172F1D
		// (set) Token: 0x06006CC6 RID: 27846 RVA: 0x00174D25 File Offset: 0x00172F25
		[DefaultValue(OperationResultCode.Success)]
		[XmlAttribute]
		public OperationResultCode OperationResultCode
		{
			get
			{
				return this.operationResultCodeField;
			}
			set
			{
				this.operationResultCodeField = value;
			}
		}

		// Token: 0x040046A1 RID: 18081
		private Guid batchId = Guid.NewGuid();

		// Token: 0x040046A2 RID: 18082
		private DirectoryContext[] contextsField;

		// Token: 0x040046A3 RID: 18083
		private DirectoryObject[] objectsField;

		// Token: 0x040046A4 RID: 18084
		private DirectoryLink[] linksField;

		// Token: 0x040046A5 RID: 18085
		private byte[] nextCookieField;

		// Token: 0x040046A6 RID: 18086
		private bool moreField;

		// Token: 0x040046A7 RID: 18087
		private OperationResultCode operationResultCodeField;
	}
}
