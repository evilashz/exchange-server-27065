using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace www.microsoft.com.bdm.pets
{
	// Token: 0x02000BC9 RID: 3017
	[DataContract(Name = "BDMHeader", Namespace = "http://www.microsoft.com/bdm/pets")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	public class BDMHeader : IExtensibleDataObject
	{
		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x0600411F RID: 16671 RVA: 0x000AD40F File Offset: 0x000AB60F
		// (set) Token: 0x06004120 RID: 16672 RVA: 0x000AD417 File Offset: 0x000AB617
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

		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x06004121 RID: 16673 RVA: 0x000AD420 File Offset: 0x000AB620
		// (set) Token: 0x06004122 RID: 16674 RVA: 0x000AD428 File Offset: 0x000AB628
		[DataMember]
		public Guid CorrelationId
		{
			get
			{
				return this.CorrelationIdField;
			}
			set
			{
				this.CorrelationIdField = value;
			}
		}

		// Token: 0x0400384F RID: 14415
		private ExtensionDataObject extensionDataField;

		// Token: 0x04003850 RID: 14416
		private Guid CorrelationIdField;
	}
}
