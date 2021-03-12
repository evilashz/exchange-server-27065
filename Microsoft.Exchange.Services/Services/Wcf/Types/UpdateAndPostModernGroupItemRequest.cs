using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B1E RID: 2846
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateAndPostModernGroupItemRequest : UpdateItemRequest
	{
		// Token: 0x17001354 RID: 4948
		// (get) Token: 0x060050BC RID: 20668 RVA: 0x00109E31 File Offset: 0x00108031
		// (set) Token: 0x060050BD RID: 20669 RVA: 0x00109E39 File Offset: 0x00108039
		[XmlIgnore]
		[DataMember(Name = "ModernGroupEmailAddress", IsRequired = true)]
		public EmailAddressWrapper ModernGroupEmailAddress { get; set; }

		// Token: 0x060050BE RID: 20670 RVA: 0x00109E42 File Offset: 0x00108042
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new UpdateAndPostModernGroupItem(callContext, this);
		}

		// Token: 0x060050BF RID: 20671 RVA: 0x00109E4C File Offset: 0x0010804C
		internal override void Validate()
		{
			base.Validate();
			if (this.ModernGroupEmailAddress == null || base.ItemChanges == null || base.ItemChanges.Length == 0)
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
