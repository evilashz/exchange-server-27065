using System;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E94 RID: 3732
	internal class MeetingMessageTypePropertyProvider : SimpleEwsPropertyProvider
	{
		// Token: 0x0600613B RID: 24891 RVA: 0x0012F0A6 File Offset: 0x0012D2A6
		public MeetingMessageTypePropertyProvider(PropertyInformation propertyInformation) : base(propertyInformation)
		{
		}

		// Token: 0x0600613C RID: 24892 RVA: 0x0012F0B0 File Offset: 0x0012D2B0
		protected override void GetProperty(Entity entity, PropertyDefinition property, ServiceObject ewsObject)
		{
			MeetingMessageType meetingMessageType = MeetingMessageType.None;
			string text = ewsObject[ItemSchema.ItemClass] as string;
			string a;
			if ((a = text) != null)
			{
				if (!(a == "IPM.Schedule.Meeting.Request"))
				{
					if (!(a == "IPM.Schedule.Meeting.Canceled"))
					{
						if (!(a == "IPM.Schedule.Meeting.Resp.Pos"))
						{
							if (!(a == "IPM.Schedule.Meeting.Resp.Neg"))
							{
								if (a == "IPM.Schedule.Meeting.Resp.Tent")
								{
									meetingMessageType = MeetingMessageType.MeetingTenativelyAccepted;
								}
							}
							else
							{
								meetingMessageType = MeetingMessageType.MeetingDeclined;
							}
						}
						else
						{
							meetingMessageType = MeetingMessageType.MeetingAccepted;
						}
					}
					else
					{
						meetingMessageType = MeetingMessageType.MeetingCancelled;
					}
				}
				else
				{
					meetingMessageType = MeetingMessageType.MeetingRequest;
				}
			}
			entity[property] = meetingMessageType;
		}

		// Token: 0x0600613D RID: 24893 RVA: 0x0012F138 File Offset: 0x0012D338
		public override string GetQueryConstant(object value)
		{
			if (value is MeetingMessageType)
			{
				switch ((MeetingMessageType)Enum.Parse(typeof(MeetingMessageType), value.ToString()))
				{
				case MeetingMessageType.None:
					return "IPM.Note";
				case MeetingMessageType.MeetingRequest:
					return "IPM.Schedule.Meeting.Request";
				case MeetingMessageType.MeetingCancelled:
					return "IPM.Schedule.Meeting.Canceled";
				case MeetingMessageType.MeetingAccepted:
					return "IPM.Schedule.Meeting.Resp.Pos";
				case MeetingMessageType.MeetingTenativelyAccepted:
					return "IPM.Schedule.Meeting.Resp.Tent";
				case MeetingMessageType.MeetingDeclined:
					return "IPM.Schedule.Meeting.Resp.Neg";
				}
			}
			return null;
		}
	}
}
