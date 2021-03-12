using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003E9 RID: 1001
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "Role", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public class Role : IExtensibleDataObject
	{
		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001889 RID: 6281 RVA: 0x0008D1FE File Offset: 0x0008B3FE
		// (set) Token: 0x0600188A RID: 6282 RVA: 0x0008D206 File Offset: 0x0008B406
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x0008D20F File Offset: 0x0008B40F
		// (set) Token: 0x0600188C RID: 6284 RVA: 0x0008D217 File Offset: 0x0008B417
		[DataMember]
		public string Description
		{
			get
			{
				return this.DescriptionField;
			}
			set
			{
				this.DescriptionField = value;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x0600188D RID: 6285 RVA: 0x0008D220 File Offset: 0x0008B420
		// (set) Token: 0x0600188E RID: 6286 RVA: 0x0008D228 File Offset: 0x0008B428
		[DataMember]
		public bool? IsEnabled
		{
			get
			{
				return this.IsEnabledField;
			}
			set
			{
				this.IsEnabledField = value;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x0008D231 File Offset: 0x0008B431
		// (set) Token: 0x06001890 RID: 6288 RVA: 0x0008D239 File Offset: 0x0008B439
		[DataMember]
		public bool? IsSystem
		{
			get
			{
				return this.IsSystemField;
			}
			set
			{
				this.IsSystemField = value;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x0008D242 File Offset: 0x0008B442
		// (set) Token: 0x06001892 RID: 6290 RVA: 0x0008D24A File Offset: 0x0008B44A
		[DataMember]
		public string Name
		{
			get
			{
				return this.NameField;
			}
			set
			{
				this.NameField = value;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001893 RID: 6291 RVA: 0x0008D253 File Offset: 0x0008B453
		// (set) Token: 0x06001894 RID: 6292 RVA: 0x0008D25B File Offset: 0x0008B45B
		[DataMember]
		public Guid? ObjectId
		{
			get
			{
				return this.ObjectIdField;
			}
			set
			{
				this.ObjectIdField = value;
			}
		}

		// Token: 0x04001140 RID: 4416
		private ExtensionDataObject extensionDataField;

		// Token: 0x04001141 RID: 4417
		private string DescriptionField;

		// Token: 0x04001142 RID: 4418
		private bool? IsEnabledField;

		// Token: 0x04001143 RID: 4419
		private bool? IsSystemField;

		// Token: 0x04001144 RID: 4420
		private string NameField;

		// Token: 0x04001145 RID: 4421
		private Guid? ObjectIdField;
	}
}
