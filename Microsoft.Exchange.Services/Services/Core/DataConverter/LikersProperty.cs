using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000186 RID: 390
	internal sealed class LikersProperty : ComplexPropertyBase, IToServiceObjectCommand, IToServiceObjectForPropertyBagCommand, IPropertyCommand
	{
		// Token: 0x06000B16 RID: 2838 RVA: 0x0003500C File Offset: 0x0003320C
		public LikersProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00035015 File Offset: 0x00033215
		public static LikersProperty CreateCommand(CommandContext commandContext)
		{
			return new LikersProperty(commandContext);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00035020 File Offset: 0x00033220
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			MessageItem messageItem = commandSettings.StoreObject as MessageItem;
			if (messageItem == null || messageItem.PropertyBag == null)
			{
				return;
			}
			Likers likers = new Likers(messageItem.PropertyBag);
			commandSettings.ServiceObject.PropertyBag[propertyInformation] = LikersProperty.ToEmailAddress(likers);
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0003507C File Offset: 0x0003327C
		public void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			Likers likers = Likers.CreateInstance(propertyBag);
			if (likers != null)
			{
				serviceObject[this.commandContext.PropertyInformation] = LikersProperty.ToEmailAddress(likers);
			}
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x000350BF File Offset: 0x000332BF
		private static EmailAddressWrapper[] ToEmailAddress(IEnumerable<Participant> likers)
		{
			if (likers != null)
			{
				return likers.Select(new Func<Participant, EmailAddressWrapper>(EmailAddressWrapper.FromParticipant)).ToArray<EmailAddressWrapper>();
			}
			return null;
		}
	}
}
