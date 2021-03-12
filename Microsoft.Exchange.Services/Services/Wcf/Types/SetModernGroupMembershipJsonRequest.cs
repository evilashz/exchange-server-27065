using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A03 RID: 2563
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetModernGroupMembershipJsonRequest : BaseRequest
	{
		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x0600485E RID: 18526 RVA: 0x001016D9 File Offset: 0x000FF8D9
		// (set) Token: 0x0600485F RID: 18527 RVA: 0x001016E1 File Offset: 0x000FF8E1
		[DataMember(IsRequired = true)]
		public string GroupSmtpAddress { get; set; }

		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x06004860 RID: 18528 RVA: 0x001016EA File Offset: 0x000FF8EA
		// (set) Token: 0x06004861 RID: 18529 RVA: 0x001016F2 File Offset: 0x000FF8F2
		[DataMember(IsRequired = true)]
		public ModernGroupMembershipOperationType OperationType { get; set; }

		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x06004862 RID: 18530 RVA: 0x001016FB File Offset: 0x000FF8FB
		// (set) Token: 0x06004863 RID: 18531 RVA: 0x00101703 File Offset: 0x000FF903
		[DataMember(IsRequired = false)]
		public string AttachedMessage { get; set; }

		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x06004864 RID: 18532 RVA: 0x0010170C File Offset: 0x000FF90C
		// (set) Token: 0x06004865 RID: 18533 RVA: 0x00101714 File Offset: 0x000FF914
		internal ProxyAddress GroupProxyAddress { get; private set; }

		// Token: 0x06004866 RID: 18534 RVA: 0x00101720 File Offset: 0x000FF920
		internal override void Validate()
		{
			if (string.IsNullOrEmpty(this.GroupSmtpAddress) || !SmtpAddress.IsValidSmtpAddress(this.GroupSmtpAddress))
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(CoreResources.IDs.ErrorInvalidSmtpAddress), FaultParty.Sender);
			}
			if (!Enum.IsDefined(typeof(ModernGroupMembershipOperationType), this.OperationType))
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
			}
			if (this.OperationType == ModernGroupMembershipOperationType.RequestJoin && this.AttachedMessage == null)
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(CoreResources.IDs.ErrorRequiredPropertyMissing), FaultParty.Sender);
			}
			this.GroupProxyAddress = new SmtpProxyAddress(this.GroupSmtpAddress, true);
		}

		// Token: 0x06004867 RID: 18535 RVA: 0x001017BE File Offset: 0x000FF9BE
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x06004868 RID: 18536 RVA: 0x001017C8 File Offset: 0x000FF9C8
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}
	}
}
