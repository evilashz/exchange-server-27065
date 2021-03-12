using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services
{
	// Token: 0x0200084F RID: 2127
	[DataContract(Name = "InboundIPListConfig", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[Serializable]
	internal class InboundIPListConfig : IExtensibleDataObject
	{
		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06002D7C RID: 11644 RVA: 0x00065E16 File Offset: 0x00064016
		// (set) Token: 0x06002D7D RID: 11645 RVA: 0x00065E1E File Offset: 0x0006401E
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

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06002D7E RID: 11646 RVA: 0x00065E27 File Offset: 0x00064027
		// (set) Token: 0x06002D7F RID: 11647 RVA: 0x00065E2F File Offset: 0x0006402F
		[DataMember]
		internal SmtpProfile[] IPList
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

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06002D80 RID: 11648 RVA: 0x00065E38 File Offset: 0x00064038
		// (set) Token: 0x06002D81 RID: 11649 RVA: 0x00065E40 File Offset: 0x00064040
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

		// Token: 0x04002794 RID: 10132
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x04002795 RID: 10133
		[OptionalField]
		private SmtpProfile[] IPListField;

		// Token: 0x04002796 RID: 10134
		[OptionalField]
		private TargetAction TargetActionField;
	}
}
