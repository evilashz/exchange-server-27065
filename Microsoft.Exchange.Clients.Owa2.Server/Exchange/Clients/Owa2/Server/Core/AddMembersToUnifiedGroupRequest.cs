using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003D7 RID: 983
	[DataContract(Name = "AddMembersToUnifiedGroupRequest", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddMembersToUnifiedGroupRequest : BaseRequest
	{
		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06001F83 RID: 8067 RVA: 0x000776E1 File Offset: 0x000758E1
		// (set) Token: 0x06001F84 RID: 8068 RVA: 0x000776E9 File Offset: 0x000758E9
		[DataMember(Name = "ExternalDirectoryObjectId", IsRequired = true)]
		public string ExternalDirectoryObjectId { get; set; }

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06001F85 RID: 8069 RVA: 0x000776F2 File Offset: 0x000758F2
		// (set) Token: 0x06001F86 RID: 8070 RVA: 0x000776FA File Offset: 0x000758FA
		[DataMember(Name = "AddedMembers", IsRequired = false)]
		public string[] AddedMembers { get; set; }

		// Token: 0x06001F87 RID: 8071 RVA: 0x00077703 File Offset: 0x00075903
		internal override void Validate()
		{
			if (string.IsNullOrEmpty(this.ExternalDirectoryObjectId))
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
			}
			if (this.AddedMembers == null)
			{
				this.AddedMembers = new string[0];
			}
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x00077732 File Offset: 0x00075932
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x0007773C File Offset: 0x0007593C
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}
	}
}
