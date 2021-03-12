using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync
{
	// Token: 0x02000870 RID: 2160
	[KnownType(typeof(InvalidCompanyFault))]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[DataContract(Name = "AdminServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync")]
	[KnownType(typeof(InvalidContractFault))]
	[DebuggerStepThrough]
	[KnownType(typeof(InternalFault))]
	[KnownType(typeof(AuthorizationFault))]
	[KnownType(typeof(InvalidGroupFault))]
	[KnownType(typeof(RemoveGroupErrorInfo))]
	[KnownType(typeof(InvalidUserFault))]
	[KnownType(typeof(ErrorInfo))]
	[Serializable]
	internal class AdminServiceFault : IExtensibleDataObject
	{
		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06002E3C RID: 11836 RVA: 0x0006641E File Offset: 0x0006461E
		// (set) Token: 0x06002E3D RID: 11837 RVA: 0x00066426 File Offset: 0x00064626
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

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06002E3E RID: 11838 RVA: 0x0006642F File Offset: 0x0006462F
		// (set) Token: 0x06002E3F RID: 11839 RVA: 0x00066437 File Offset: 0x00064637
		[DataMember]
		internal Dictionary<string, string> Data
		{
			get
			{
				return this.DataField;
			}
			set
			{
				this.DataField = value;
			}
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06002E40 RID: 11840 RVA: 0x00066440 File Offset: 0x00064640
		// (set) Token: 0x06002E41 RID: 11841 RVA: 0x00066448 File Offset: 0x00064648
		[DataMember]
		internal ErrorType ErrorType
		{
			get
			{
				return this.ErrorTypeField;
			}
			set
			{
				this.ErrorTypeField = value;
			}
		}

		// Token: 0x04002838 RID: 10296
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x04002839 RID: 10297
		[OptionalField]
		private Dictionary<string, string> DataField;

		// Token: 0x0400283A RID: 10298
		[OptionalField]
		private ErrorType ErrorTypeField;
	}
}
