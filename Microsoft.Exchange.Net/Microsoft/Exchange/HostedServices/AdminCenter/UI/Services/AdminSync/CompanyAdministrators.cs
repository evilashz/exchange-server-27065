using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync
{
	// Token: 0x0200086D RID: 2157
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[DataContract(Name = "CompanyAdministrators", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync")]
	[Serializable]
	internal class CompanyAdministrators : IExtensibleDataObject
	{
		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06002E2C RID: 11820 RVA: 0x00066397 File Offset: 0x00064597
		// (set) Token: 0x06002E2D RID: 11821 RVA: 0x0006639F File Offset: 0x0006459F
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

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06002E2E RID: 11822 RVA: 0x000663A8 File Offset: 0x000645A8
		// (set) Token: 0x06002E2F RID: 11823 RVA: 0x000663B0 File Offset: 0x000645B0
		[DataMember]
		internal Dictionary<Role, Guid[]> AdminGroups
		{
			get
			{
				return this.AdminGroupsField;
			}
			set
			{
				this.AdminGroupsField = value;
			}
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06002E30 RID: 11824 RVA: 0x000663B9 File Offset: 0x000645B9
		// (set) Token: 0x06002E31 RID: 11825 RVA: 0x000663C1 File Offset: 0x000645C1
		[DataMember]
		internal Dictionary<Role, string[]> AdminUsers
		{
			get
			{
				return this.AdminUsersField;
			}
			set
			{
				this.AdminUsersField = value;
			}
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06002E32 RID: 11826 RVA: 0x000663CA File Offset: 0x000645CA
		// (set) Token: 0x06002E33 RID: 11827 RVA: 0x000663D2 File Offset: 0x000645D2
		[DataMember]
		internal int CompanyId
		{
			get
			{
				return this.CompanyIdField;
			}
			set
			{
				this.CompanyIdField = value;
			}
		}

		// Token: 0x0400282E RID: 10286
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400282F RID: 10287
		[OptionalField]
		private Dictionary<Role, Guid[]> AdminGroupsField;

		// Token: 0x04002830 RID: 10288
		[OptionalField]
		private Dictionary<Role, string[]> AdminUsersField;

		// Token: 0x04002831 RID: 10289
		[OptionalField]
		private int CompanyIdField;
	}
}
