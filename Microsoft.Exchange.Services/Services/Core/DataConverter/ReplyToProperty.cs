using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000181 RID: 385
	internal sealed class ReplyToProperty : ComplexPropertyBase, IPregatherParticipants, IToXmlCommand, IToServiceObjectCommand, ISetCommand, IAppendUpdateCommand, IDeleteUpdateCommand, ISetUpdateCommand, IUpdateCommand, IPropertyCommand
	{
		// Token: 0x06000AF8 RID: 2808 RVA: 0x00034B27 File Offset: 0x00032D27
		public ReplyToProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00034B30 File Offset: 0x00032D30
		public static ReplyToProperty CreateCommand(CommandContext commandContext)
		{
			return new ReplyToProperty(commandContext);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00034B38 File Offset: 0x00032D38
		void IPregatherParticipants.Pregather(StoreObject storeObject, List<Participant> participants)
		{
			MessageItem messageItem = storeObject as MessageItem;
			if (messageItem != null)
			{
				try
				{
					foreach (Participant item in messageItem.ReplyTo)
					{
						participants.Add(item);
					}
				}
				catch (PropertyErrorException innerException)
				{
					throw new PropertyRequestFailedException(CoreResources.IDs.ErrorItemPropertyRequestFailed, this.commandContext.PropertyInformation.PropertyPath, innerException);
				}
			}
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00034BC0 File Offset: 0x00032DC0
		public void ToXml()
		{
			throw new InvalidOperationException("ReplyToProperty.ToXml should not be called");
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00034BCC File Offset: 0x00032DCC
		public void Set()
		{
			SetCommandSettings commandSettings = base.GetCommandSettings<SetCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			this.SetProperty(serviceObject, storeObject, false);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00034BF8 File Offset: 0x00032DF8
		public override void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			StoreObject storeObject = updateCommandSettings.StoreObject;
			this.SetProperty(setPropertyUpdate.ServiceObject, storeObject, false);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00034C1C File Offset: 0x00032E1C
		public override void DeleteUpdate(DeletePropertyUpdate deletePropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			MessageItem messageItem = updateCommandSettings.StoreObject as MessageItem;
			if (messageItem == null)
			{
				throw new InvalidPropertyRequestException(this.commandContext.PropertyInformation.PropertyPath);
			}
			messageItem.ReplyTo.Clear();
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00034C5C File Offset: 0x00032E5C
		public override void AppendUpdate(AppendPropertyUpdate appendPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			StoreObject storeObject = updateCommandSettings.StoreObject;
			this.SetProperty(appendPropertyUpdate.ServiceObject, storeObject, true);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00034C80 File Offset: 0x00032E80
		private void SetProperty(ServiceObject serviceObject, StoreObject storeObject, bool isAppend)
		{
			MessageItem messageItem = storeObject as MessageItem;
			if (messageItem == null)
			{
				throw new InvalidPropertyRequestException(this.commandContext.PropertyInformation.PropertyPath);
			}
			if (!isAppend)
			{
				messageItem.ReplyTo.Clear();
			}
			EmailAddressWrapper[] valueOrDefault = serviceObject.GetValueOrDefault<EmailAddressWrapper[]>(this.commandContext.PropertyInformation);
			if (valueOrDefault != null)
			{
				foreach (EmailAddressWrapper emailAddressWrapper in valueOrDefault)
				{
					if (emailAddressWrapper != null)
					{
						messageItem.ReplyTo.Add(this.GetParticipantFromAddress(messageItem, emailAddressWrapper));
					}
				}
			}
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00034D00 File Offset: 0x00032F00
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			MessageItem messageItem = storeObject as MessageItem;
			if (messageItem != null)
			{
				IList<Participant> replyTo = messageItem.ReplyTo;
				List<EmailAddressWrapper> list = ReplyToProperty.Render(replyTo);
				serviceObject[propertyInformation] = ((list.Count > 0) ? list.ToArray() : null);
			}
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00034D67 File Offset: 0x00032F67
		internal static List<EmailAddressWrapper> Render(IList<Participant> replyToProperty)
		{
			return ReplyToProperty.Render(replyToProperty.Cast<IParticipant>().ToList<IParticipant>());
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00034D7C File Offset: 0x00032F7C
		internal static List<EmailAddressWrapper> Render(IList<IParticipant> replyToProperty)
		{
			List<EmailAddressWrapper> list = new List<EmailAddressWrapper>();
			ParticipantInformationDictionary participantInformation = EWSSettings.ParticipantInformation;
			foreach (IParticipant participant in replyToProperty)
			{
				ParticipantInformation participantInformationOrCreateNew = participantInformation.GetParticipantInformationOrCreateNew(participant);
				list.Add(EmailAddressWrapper.FromParticipantInformation(participantInformationOrCreateNew));
			}
			return list;
		}
	}
}
