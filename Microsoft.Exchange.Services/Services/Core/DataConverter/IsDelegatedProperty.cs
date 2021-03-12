using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000E5 RID: 229
	internal sealed class IsDelegatedProperty : ComplexPropertyBase, IToXmlCommand, IToServiceObjectCommand, IPropertyCommand
	{
		// Token: 0x06000646 RID: 1606 RVA: 0x00020D6B File Offset: 0x0001EF6B
		private IsDelegatedProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00020D74 File Offset: 0x0001EF74
		public static IsDelegatedProperty CreateCommand(CommandContext commandContext)
		{
			return new IsDelegatedProperty(commandContext);
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00020D7C File Offset: 0x0001EF7C
		public void ToXml()
		{
			throw new InvalidOperationException("IsDelegatedProperty.ToXml should not be called.");
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00020D88 File Offset: 0x0001EF88
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			MeetingMessage meetingMessage = (MeetingMessage)commandSettings.StoreObject;
			serviceObject[propertyInformation] = meetingMessage.IsDelegated();
		}
	}
}
