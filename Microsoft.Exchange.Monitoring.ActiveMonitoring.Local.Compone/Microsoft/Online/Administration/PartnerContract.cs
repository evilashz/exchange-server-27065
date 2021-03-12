using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003E7 RID: 999
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "PartnerContract", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public class PartnerContract : IExtensibleDataObject
	{
		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001853 RID: 6227 RVA: 0x0008D034 File Offset: 0x0008B234
		// (set) Token: 0x06001854 RID: 6228 RVA: 0x0008D03C File Offset: 0x0008B23C
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

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001855 RID: 6229 RVA: 0x0008D045 File Offset: 0x0008B245
		// (set) Token: 0x06001856 RID: 6230 RVA: 0x0008D04D File Offset: 0x0008B24D
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

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001857 RID: 6231 RVA: 0x0008D056 File Offset: 0x0008B256
		// (set) Token: 0x06001858 RID: 6232 RVA: 0x0008D05E File Offset: 0x0008B25E
		[DataMember]
		public Guid ObjectId
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

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001859 RID: 6233 RVA: 0x0008D067 File Offset: 0x0008B267
		// (set) Token: 0x0600185A RID: 6234 RVA: 0x0008D06F File Offset: 0x0008B26F
		[DataMember]
		public Guid PartnerContext
		{
			get
			{
				return this.PartnerContextField;
			}
			set
			{
				this.PartnerContextField = value;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x0008D078 File Offset: 0x0008B278
		// (set) Token: 0x0600185C RID: 6236 RVA: 0x0008D080 File Offset: 0x0008B280
		[DataMember]
		public Guid TenantId
		{
			get
			{
				return this.TenantIdField;
			}
			set
			{
				this.TenantIdField = value;
			}
		}

		// Token: 0x04001126 RID: 4390
		private ExtensionDataObject extensionDataField;

		// Token: 0x04001127 RID: 4391
		private string NameField;

		// Token: 0x04001128 RID: 4392
		private Guid ObjectIdField;

		// Token: 0x04001129 RID: 4393
		private Guid PartnerContextField;

		// Token: 0x0400112A RID: 4394
		private Guid TenantIdField;
	}
}
