using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000E7 RID: 231
	internal sealed class MeetingRequestTypeProperty : ComplexPropertyBase, IToXmlCommand, IToServiceObjectCommand, IPropertyCommand
	{
		// Token: 0x0600064F RID: 1615 RVA: 0x00020E74 File Offset: 0x0001F074
		private MeetingRequestTypeProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00020E7D File Offset: 0x0001F07D
		public static MeetingRequestTypeProperty CreateCommand(CommandContext commandContext)
		{
			return new MeetingRequestTypeProperty(commandContext);
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00020E85 File Offset: 0x0001F085
		public void ToXml()
		{
			throw new InvalidOperationException("MeetingRequestTypeProperty.ToXml should not be called");
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00020E94 File Offset: 0x0001F094
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			ServiceObject serviceObject = commandSettings.ServiceObject;
			MeetingRequest meetingRequest = (MeetingRequest)commandSettings.StoreObject;
			string value = MeetingRequestTypeProperty.MeetingMessageTypeToString(meetingRequest.MeetingRequestType);
			if (!string.IsNullOrEmpty(value))
			{
				serviceObject[this.commandContext.PropertyInformation] = value;
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00020EE4 File Offset: 0x0001F0E4
		private static string MeetingMessageTypeToString(MeetingMessageType meetingMessageType)
		{
			if (meetingMessageType <= MeetingMessageType.FullUpdate)
			{
				if (meetingMessageType == MeetingMessageType.NewMeetingRequest)
				{
					return "NewMeetingRequest";
				}
				if (meetingMessageType == MeetingMessageType.FullUpdate)
				{
					return "FullUpdate";
				}
			}
			else
			{
				if (meetingMessageType == MeetingMessageType.InformationalUpdate)
				{
					return "InformationalUpdate";
				}
				if (meetingMessageType == MeetingMessageType.SilentUpdate)
				{
					return "SilentUpdate";
				}
				if (meetingMessageType == MeetingMessageType.Outdated)
				{
					return "Outdated";
				}
			}
			if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2007SP1))
			{
				if (meetingMessageType == MeetingMessageType.None)
				{
					return "None";
				}
				if (meetingMessageType == MeetingMessageType.PrincipalWantsCopy)
				{
					return "PrincipalWantsCopy";
				}
			}
			ExTraceGlobals.CalendarDataTracer.TraceDebug<MeetingMessageType, ExchangeVersion>(0L, "Could not emit meeting request type value for value {0}, request schema version {1}", meetingMessageType, ExchangeVersion.Current);
			return null;
		}

		// Token: 0x040006AD RID: 1709
		private const string FullUpdateValue = "FullUpdate";

		// Token: 0x040006AE RID: 1710
		private const string InformationalUpdateValue = "InformationalUpdate";

		// Token: 0x040006AF RID: 1711
		private const string NewMeetingRequestValue = "NewMeetingRequest";

		// Token: 0x040006B0 RID: 1712
		private const string NoneValue = "None";

		// Token: 0x040006B1 RID: 1713
		private const string OutdatedValue = "Outdated";

		// Token: 0x040006B2 RID: 1714
		private const string PrincipalWantsCopy = "PrincipalWantsCopy";

		// Token: 0x040006B3 RID: 1715
		private const string SilentUpdateValue = "SilentUpdate";
	}
}
