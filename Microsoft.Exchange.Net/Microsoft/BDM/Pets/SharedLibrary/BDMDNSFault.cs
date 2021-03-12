using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Microsoft.BDM.Pets.SharedLibrary
{
	// Token: 0x02000BCA RID: 3018
	[KnownType(typeof(DomainNameInUseFault))]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[KnownType(typeof(KeyNotFoundFault))]
	[KnownType(typeof(InvalidArgumentFault))]
	[DataContract(Name = "BDMDNSFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[DebuggerStepThrough]
	public class BDMDNSFault : IExtensibleDataObject
	{
		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x06004124 RID: 16676 RVA: 0x000AD439 File Offset: 0x000AB639
		// (set) Token: 0x06004125 RID: 16677 RVA: 0x000AD441 File Offset: 0x000AB641
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

		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x06004126 RID: 16678 RVA: 0x000AD44A File Offset: 0x000AB64A
		// (set) Token: 0x06004127 RID: 16679 RVA: 0x000AD452 File Offset: 0x000AB652
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

		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x06004128 RID: 16680 RVA: 0x000AD45B File Offset: 0x000AB65B
		// (set) Token: 0x06004129 RID: 16681 RVA: 0x000AD463 File Offset: 0x000AB663
		[DataMember]
		public ExceptionDetail ErrorDetails
		{
			get
			{
				return this.ErrorDetailsField;
			}
			set
			{
				this.ErrorDetailsField = value;
			}
		}

		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x0600412A RID: 16682 RVA: 0x000AD46C File Offset: 0x000AB66C
		// (set) Token: 0x0600412B RID: 16683 RVA: 0x000AD474 File Offset: 0x000AB674
		[DataMember]
		public string ReasonCode
		{
			get
			{
				return this.ReasonCodeField;
			}
			set
			{
				this.ReasonCodeField = value;
			}
		}

		// Token: 0x04003851 RID: 14417
		private ExtensionDataObject extensionDataField;

		// Token: 0x04003852 RID: 14418
		private string DescriptionField;

		// Token: 0x04003853 RID: 14419
		private ExceptionDetail ErrorDetailsField;

		// Token: 0x04003854 RID: 14420
		private string ReasonCodeField;
	}
}
