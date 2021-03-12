using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000185 RID: 389
	internal sealed class VotingInformationProperty : ComplexPropertyBase, IToServiceObjectCommand, IPropertyCommand
	{
		// Token: 0x06000B12 RID: 2834 RVA: 0x00034EEA File Offset: 0x000330EA
		public VotingInformationProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00034EF3 File Offset: 0x000330F3
		public static VotingInformationProperty CreateCommand(CommandContext commandContext)
		{
			return new VotingInformationProperty(commandContext);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00034EFB File Offset: 0x000330FB
		public void ToXml()
		{
			throw new InvalidOperationException("VotingInformationProperty.ToXml should not be called.");
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00034F08 File Offset: 0x00033108
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			MessageItem messageItem = commandSettings.StoreObject as MessageItem;
			if (messageItem == null || messageItem.VotingInfo == null)
			{
				return;
			}
			VotingInfo.OptionData[] array = (VotingInfo.OptionData[])messageItem.VotingInfo.GetOptionsDataList();
			if (string.IsNullOrEmpty(messageItem.VotingInfo.Response) && (array == null || array.Length == 0))
			{
				return;
			}
			VotingInformationType votingInformationType = new VotingInformationType();
			votingInformationType.VotingResponse = messageItem.VotingInfo.Response;
			if (array != null && array.Length != 0)
			{
				votingInformationType.UserOptions = new VotingOptionDataType[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					votingInformationType.UserOptions[i] = new VotingOptionDataType();
					votingInformationType.UserOptions[i].DisplayName = array[i].DisplayName;
					votingInformationType.UserOptions[i].SendPrompt = (SendPromptType)array[i].SendPrompt;
				}
			}
			ServiceObject serviceObject = commandSettings.ServiceObject;
			serviceObject.PropertyBag[propertyInformation] = votingInformationType;
		}
	}
}
