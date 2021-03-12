using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003CD RID: 973
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ValidationError", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public class ValidationError : IExtensibleDataObject
	{
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x0008CA19 File Offset: 0x0008AC19
		// (set) Token: 0x0600179B RID: 6043 RVA: 0x0008CA21 File Offset: 0x0008AC21
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

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x0008CA2A File Offset: 0x0008AC2A
		// (set) Token: 0x0600179D RID: 6045 RVA: 0x0008CA32 File Offset: 0x0008AC32
		[DataMember]
		public XmlElement ErrorDetail
		{
			get
			{
				return this.ErrorDetailField;
			}
			set
			{
				this.ErrorDetailField = value;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x0600179E RID: 6046 RVA: 0x0008CA3B File Offset: 0x0008AC3B
		// (set) Token: 0x0600179F RID: 6047 RVA: 0x0008CA43 File Offset: 0x0008AC43
		[DataMember]
		public bool Resolved
		{
			get
			{
				return this.ResolvedField;
			}
			set
			{
				this.ResolvedField = value;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x0008CA4C File Offset: 0x0008AC4C
		// (set) Token: 0x060017A1 RID: 6049 RVA: 0x0008CA54 File Offset: 0x0008AC54
		[DataMember]
		public string ServiceInstance
		{
			get
			{
				return this.ServiceInstanceField;
			}
			set
			{
				this.ServiceInstanceField = value;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060017A2 RID: 6050 RVA: 0x0008CA5D File Offset: 0x0008AC5D
		// (set) Token: 0x060017A3 RID: 6051 RVA: 0x0008CA65 File Offset: 0x0008AC65
		[DataMember]
		public DateTime Timestamp
		{
			get
			{
				return this.TimestampField;
			}
			set
			{
				this.TimestampField = value;
			}
		}

		// Token: 0x040010B4 RID: 4276
		private ExtensionDataObject extensionDataField;

		// Token: 0x040010B5 RID: 4277
		private XmlElement ErrorDetailField;

		// Token: 0x040010B6 RID: 4278
		private bool ResolvedField;

		// Token: 0x040010B7 RID: 4279
		private string ServiceInstanceField;

		// Token: 0x040010B8 RID: 4280
		private DateTime TimestampField;
	}
}
