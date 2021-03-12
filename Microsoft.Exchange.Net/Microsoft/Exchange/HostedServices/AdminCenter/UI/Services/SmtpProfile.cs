using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services
{
	// Token: 0x02000854 RID: 2132
	[DebuggerStepThrough]
	[DataContract(Name = "SmtpProfile", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[Serializable]
	internal class SmtpProfile : IExtensibleDataObject
	{
		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06002D83 RID: 11651 RVA: 0x00065E51 File Offset: 0x00064051
		// (set) Token: 0x06002D84 RID: 11652 RVA: 0x00065E59 File Offset: 0x00064059
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

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06002D85 RID: 11653 RVA: 0x00065E62 File Offset: 0x00064062
		// (set) Token: 0x06002D86 RID: 11654 RVA: 0x00065E6A File Offset: 0x0006406A
		[DataMember]
		internal SmtpProfileEntry[] IPList
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

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06002D87 RID: 11655 RVA: 0x00065E73 File Offset: 0x00064073
		// (set) Token: 0x06002D88 RID: 11656 RVA: 0x00065E7B File Offset: 0x0006407B
		[DataMember]
		internal string ProfileName
		{
			get
			{
				return this.ProfileNameField;
			}
			set
			{
				this.ProfileNameField = value;
			}
		}

		// Token: 0x040027AA RID: 10154
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x040027AB RID: 10155
		[OptionalField]
		private SmtpProfileEntry[] IPListField;

		// Token: 0x040027AC RID: 10156
		[OptionalField]
		private string ProfileNameField;
	}
}
