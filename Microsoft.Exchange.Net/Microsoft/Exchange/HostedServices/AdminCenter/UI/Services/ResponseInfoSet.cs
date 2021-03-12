using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services
{
	// Token: 0x02000856 RID: 2134
	[KnownType(typeof(CompanyResponseInfoSet))]
	[DataContract(Name = "ResponseInfoSet", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[KnownType(typeof(DomainResponseInfoSet))]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[DebuggerStepThrough]
	[Serializable]
	internal class ResponseInfoSet : IExtensibleDataObject
	{
		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06002D91 RID: 11665 RVA: 0x00065EC7 File Offset: 0x000640C7
		// (set) Token: 0x06002D92 RID: 11666 RVA: 0x00065ECF File Offset: 0x000640CF
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

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06002D93 RID: 11667 RVA: 0x00065ED8 File Offset: 0x000640D8
		// (set) Token: 0x06002D94 RID: 11668 RVA: 0x00065EE0 File Offset: 0x000640E0
		[DataMember]
		internal bool OperationSuccessful
		{
			get
			{
				return this.OperationSuccessfulField;
			}
			set
			{
				this.OperationSuccessfulField = value;
			}
		}

		// Token: 0x040027B0 RID: 10160
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x040027B1 RID: 10161
		[OptionalField]
		private bool OperationSuccessfulField;
	}
}
