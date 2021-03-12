using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003DF RID: 991
	[KnownType(typeof(ListRoleMemberResults))]
	[KnownType(typeof(ListServicePrincipalResults))]
	[DataContract(Name = "ListResults", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[KnownType(typeof(ListGroupMemberResults))]
	[KnownType(typeof(ListGroupResults))]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[KnownType(typeof(ListUserResults))]
	[KnownType(typeof(ListPartnerContractResults))]
	[KnownType(typeof(ListContactResults))]
	public class ListResults : IExtensibleDataObject
	{
		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x0008CF39 File Offset: 0x0008B139
		// (set) Token: 0x06001836 RID: 6198 RVA: 0x0008CF41 File Offset: 0x0008B141
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

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x0008CF4A File Offset: 0x0008B14A
		// (set) Token: 0x06001838 RID: 6200 RVA: 0x0008CF52 File Offset: 0x0008B152
		[DataMember]
		public bool IsFirstPage
		{
			get
			{
				return this.IsFirstPageField;
			}
			set
			{
				this.IsFirstPageField = value;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x0008CF5B File Offset: 0x0008B15B
		// (set) Token: 0x0600183A RID: 6202 RVA: 0x0008CF63 File Offset: 0x0008B163
		[DataMember]
		public bool IsLastPage
		{
			get
			{
				return this.IsLastPageField;
			}
			set
			{
				this.IsLastPageField = value;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x0008CF6C File Offset: 0x0008B16C
		// (set) Token: 0x0600183C RID: 6204 RVA: 0x0008CF74 File Offset: 0x0008B174
		[DataMember]
		public byte[] ListContext
		{
			get
			{
				return this.ListContextField;
			}
			set
			{
				this.ListContextField = value;
			}
		}

		// Token: 0x0400111B RID: 4379
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400111C RID: 4380
		private bool IsFirstPageField;

		// Token: 0x0400111D RID: 4381
		private bool IsLastPageField;

		// Token: 0x0400111E RID: 4382
		private byte[] ListContextField;
	}
}
