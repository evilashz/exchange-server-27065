using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services
{
	// Token: 0x0200084C RID: 2124
	[DataContract(Name = "IPListConfig", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[Serializable]
	internal class IPListConfig : IExtensibleDataObject
	{
		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06002D68 RID: 11624 RVA: 0x00065D6D File Offset: 0x00063F6D
		// (set) Token: 0x06002D69 RID: 11625 RVA: 0x00065D75 File Offset: 0x00063F75
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

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06002D6A RID: 11626 RVA: 0x00065D7E File Offset: 0x00063F7E
		// (set) Token: 0x06002D6B RID: 11627 RVA: 0x00065D86 File Offset: 0x00063F86
		[DataMember]
		internal string[] IPList
		{
			get
			{
				return this.IPListField;
			}
			set
			{
				this.IPListField = value;
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06002D6C RID: 11628 RVA: 0x00065D8F File Offset: 0x00063F8F
		// (set) Token: 0x06002D6D RID: 11629 RVA: 0x00065D97 File Offset: 0x00063F97
		[DataMember]
		internal TargetAction TargetAction
		{
			get
			{
				return this.TargetActionField;
			}
			set
			{
				this.TargetActionField = value;
			}
		}

		// Token: 0x04002787 RID: 10119
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x04002788 RID: 10120
		[OptionalField]
		private string[] IPListField;

		// Token: 0x04002789 RID: 10121
		[OptionalField]
		private TargetAction TargetActionField;
	}
}
