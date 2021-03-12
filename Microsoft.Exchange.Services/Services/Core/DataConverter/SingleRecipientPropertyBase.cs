using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000ED RID: 237
	internal abstract class SingleRecipientPropertyBase : ComplexPropertyBase, IPregatherParticipants, IToXmlCommand, IToXmlForPropertyBagCommand, IToServiceObjectCommand, IToServiceObjectForPropertyBagCommand, ISetCommand, IPropertyCommand
	{
		// Token: 0x06000670 RID: 1648 RVA: 0x0002177E File Offset: 0x0001F97E
		public SingleRecipientPropertyBase(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000671 RID: 1649
		protected abstract Participant GetParticipant(Item storeItem);

		// Token: 0x06000672 RID: 1650
		protected abstract void SetParticipant(Item storeItem, Participant participant);

		// Token: 0x06000673 RID: 1651
		protected abstract PropertyDefinition GetParticipantDisplayNamePropertyDefinition();

		// Token: 0x06000674 RID: 1652
		protected abstract PropertyDefinition GetParticipantEmailAddressPropertyDefinition();

		// Token: 0x06000675 RID: 1653
		protected abstract PropertyDefinition GetParticipantRoutingTypePropertyDefinition();

		// Token: 0x06000676 RID: 1654
		protected abstract PropertyDefinition GetParticipantSipUriPropertyDefinition();

		// Token: 0x06000677 RID: 1655 RVA: 0x00021788 File Offset: 0x0001F988
		void IPregatherParticipants.Pregather(StoreObject storeObject, List<Participant> participants)
		{
			Participant participant = this.GetParticipant(storeObject as Item);
			if (participant != null)
			{
				participants.Add(participant);
			}
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x000217B2 File Offset: 0x0001F9B2
		public void ToXml()
		{
			throw new InvalidOperationException("SingleRecipientPropertyBase.ToXml should never be called");
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x000217BE File Offset: 0x0001F9BE
		public void ToXmlForPropertyBag()
		{
			throw new InvalidOperationException("SingleRecipientPropertyBase.ToXmlForPropertyBag should never be called");
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x000217CC File Offset: 0x0001F9CC
		public void Set()
		{
			SetCommandSettings commandSettings = base.GetCommandSettings<SetCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			SingleRecipientType valueOrDefault = serviceObject.GetValueOrDefault<SingleRecipientType>(this.commandContext.PropertyInformation);
			if (valueOrDefault != null && valueOrDefault.Mailbox != null)
			{
				Item item = (Item)storeObject;
				this.SetParticipant(item, this.GetParticipantFromAddress(item, valueOrDefault.Mailbox));
			}
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0002182C File Offset: 0x0001FA2C
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			Item item = storeObject as Item;
			if (item != null)
			{
				Participant participant = this.GetParticipant(item);
				ParticipantInformation participantInformation;
				if (participant != null && EWSSettings.ParticipantInformation.TryGetParticipant(participant, out participantInformation))
				{
					serviceObject[propertyInformation] = PropertyCommand.CreateRecipientFromParticipant(participantInformation);
				}
			}
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00021898 File Offset: 0x0001FA98
		public void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			ServiceObject serviceObject = commandSettings.ServiceObject;
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			string empty = string.Empty;
			if (PropertyCommand.TryGetValueFromPropertyBag<string>(propertyBag, this.GetParticipantDisplayNamePropertyDefinition(), out empty))
			{
				string emailAddress;
				if (!PropertyCommand.TryGetValueFromPropertyBag<string>(propertyBag, this.GetParticipantEmailAddressPropertyDefinition(), out emailAddress))
				{
					emailAddress = null;
				}
				string routingType;
				if (!PropertyCommand.TryGetValueFromPropertyBag<string>(propertyBag, this.GetParticipantRoutingTypePropertyDefinition(), out routingType))
				{
					routingType = null;
				}
				string sipUri;
				if (!PropertyCommand.TryGetValueFromPropertyBag<string>(propertyBag, this.GetParticipantSipUriPropertyDefinition(), out sipUri))
				{
					sipUri = null;
				}
				ParticipantInformation participantInformation = new ParticipantInformation(empty, routingType, emailAddress, new OneOffParticipantOrigin(), null, sipUri, null);
				SingleRecipientType value = PropertyCommand.CreateRecipientFromParticipant(participantInformation);
				serviceObject[propertyInformation] = value;
			}
		}
	}
}
