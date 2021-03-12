using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B45 RID: 2885
	[DataContract(Name = "ModernGroupMembershipRequestMessageDetailsRequest", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ModernGroupMembershipRequestMessageDetailsRequest : BaseRequest
	{
		// Token: 0x170013B7 RID: 5047
		// (get) Token: 0x060051C6 RID: 20934 RVA: 0x0010ADB8 File Offset: 0x00108FB8
		// (set) Token: 0x060051C7 RID: 20935 RVA: 0x0010ADC0 File Offset: 0x00108FC0
		[DataMember(Name = "MessageId")]
		public ItemId MessageId { get; set; }

		// Token: 0x170013B8 RID: 5048
		// (get) Token: 0x060051C8 RID: 20936 RVA: 0x0010ADC9 File Offset: 0x00108FC9
		// (set) Token: 0x060051C9 RID: 20937 RVA: 0x0010ADD1 File Offset: 0x00108FD1
		internal StoreObjectId MessageStoreId { get; private set; }

		// Token: 0x060051CA RID: 20938 RVA: 0x0010ADDC File Offset: 0x00108FDC
		internal override void Validate()
		{
			if (this.MessageId == null || string.IsNullOrEmpty(this.MessageId.Id))
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
			}
			this.MessageStoreId = ServiceIdConverter.ConvertFromConcatenatedId(this.MessageId.Id, BasicTypes.Item, null).ToStoreObjectId();
		}

		// Token: 0x060051CB RID: 20939 RVA: 0x0010AE2C File Offset: 0x0010902C
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x060051CC RID: 20940 RVA: 0x0010AE36 File Offset: 0x00109036
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}
	}
}
