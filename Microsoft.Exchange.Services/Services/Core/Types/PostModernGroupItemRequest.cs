using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000B1D RID: 2845
	[XmlType("PostModernGroupItemType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class PostModernGroupItemRequest : CreateItemRequest
	{
		// Token: 0x17001353 RID: 4947
		// (get) Token: 0x060050B7 RID: 20663 RVA: 0x00109D7A File Offset: 0x00107F7A
		// (set) Token: 0x060050B8 RID: 20664 RVA: 0x00109D82 File Offset: 0x00107F82
		[XmlElement("ModernGroupEmailAddress")]
		[DataMember(Name = "ModernGroupEmailAddress", IsRequired = true)]
		public EmailAddressWrapper ModernGroupEmailAddress { get; set; }

		// Token: 0x060050B9 RID: 20665 RVA: 0x00109D8B File Offset: 0x00107F8B
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new PostModernGroupItem(callContext, this);
		}

		// Token: 0x060050BA RID: 20666 RVA: 0x00109D94 File Offset: 0x00107F94
		internal override void Validate()
		{
			base.Validate();
			if (this.ModernGroupEmailAddress == null || base.Items.Items == null || base.Items.Items.Length != 1 || !(base.Items.Items[0] is MessageType))
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException((CoreResources.IDs)3784063568U), FaultParty.Sender);
			}
			if (this.ModernGroupEmailAddress.MailboxType != MailboxHelper.MailboxTypeType.GroupMailbox.ToString())
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException((CoreResources.IDs)3784063568U), FaultParty.Sender);
			}
		}
	}
}
