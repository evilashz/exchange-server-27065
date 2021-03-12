using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync
{
	// Token: 0x0200086F RID: 2159
	[DataContract(Name = "FailedAdminAccounts", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[Serializable]
	internal class FailedAdminAccounts : IExtensibleDataObject
	{
		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06002E35 RID: 11829 RVA: 0x000663E3 File Offset: 0x000645E3
		// (set) Token: 0x06002E36 RID: 11830 RVA: 0x000663EB File Offset: 0x000645EB
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

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06002E37 RID: 11831 RVA: 0x000663F4 File Offset: 0x000645F4
		// (set) Token: 0x06002E38 RID: 11832 RVA: 0x000663FC File Offset: 0x000645FC
		[DataMember]
		internal Dictionary<Guid, ErrorInfo> FailedGroups
		{
			get
			{
				return this.FailedGroupsField;
			}
			set
			{
				this.FailedGroupsField = value;
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06002E39 RID: 11833 RVA: 0x00066405 File Offset: 0x00064605
		// (set) Token: 0x06002E3A RID: 11834 RVA: 0x0006640D File Offset: 0x0006460D
		[DataMember]
		internal Dictionary<string, ErrorInfo> FailedUsers
		{
			get
			{
				return this.FailedUsersField;
			}
			set
			{
				this.FailedUsersField = value;
			}
		}

		// Token: 0x04002835 RID: 10293
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x04002836 RID: 10294
		[OptionalField]
		private Dictionary<Guid, ErrorInfo> FailedGroupsField;

		// Token: 0x04002837 RID: 10295
		[OptionalField]
		private Dictionary<string, ErrorInfo> FailedUsersField;
	}
}
