using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000E6 RID: 230
	internal sealed class IsOutOfDateProperty : ComplexPropertyBase, IToXmlCommand, IToServiceObjectCommand, IPropertyCommand
	{
		// Token: 0x0600064A RID: 1610 RVA: 0x00020DCD File Offset: 0x0001EFCD
		private IsOutOfDateProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00020DD6 File Offset: 0x0001EFD6
		public static IsOutOfDateProperty CreateCommand(CommandContext commandContext)
		{
			return new IsOutOfDateProperty(commandContext);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00020DDE File Offset: 0x0001EFDE
		public void ToXml()
		{
			throw new InvalidOperationException("IsOutOfDateProperty.ToXml should not be called");
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x00020DEA File Offset: 0x0001EFEA
		public override bool ToServiceObjectRequiresMailboxAccess
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00020DF0 File Offset: 0x0001EFF0
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			MeetingMessage meetingMessage = (MeetingMessage)commandSettings.StoreObject;
			try
			{
				serviceObject[propertyInformation] = meetingMessage.IsOutOfDate();
			}
			catch (ObjectNotFoundException arg)
			{
				ExTraceGlobals.CreateItemCallTracer.TraceDebug<bool, LogonType, ObjectNotFoundException>((long)this.GetHashCode(), "[IsOutOfDate::ToServiceObject] meetingMessage.IsDelegated='{0}'; meetingMessage.Session.LogonType='{1}'; Exception: '{2}'", meetingMessage.IsDelegated(), meetingMessage.Session.LogonType, arg);
			}
		}
	}
}
