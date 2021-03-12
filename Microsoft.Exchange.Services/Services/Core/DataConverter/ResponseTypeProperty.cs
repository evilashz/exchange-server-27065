using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000F0 RID: 240
	internal sealed class ResponseTypeProperty : SimpleProperty, IToXmlCommand, IToXmlForPropertyBagCommand, IToServiceObjectCommand, IToServiceObjectForPropertyBagCommand, IPropertyCommand
	{
		// Token: 0x060006A2 RID: 1698 RVA: 0x0002218A File Offset: 0x0002038A
		private ResponseTypeProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00022193 File Offset: 0x00020393
		public new static ResponseTypeProperty CreateCommand(CommandContext commandContext)
		{
			return new ResponseTypeProperty(commandContext);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0002219C File Offset: 0x0002039C
		public override void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			StoreObject storeObject = commandSettings.StoreObject;
			ResponseType? responseType = null;
			CalendarItemBase calendarItemBase = commandSettings.StoreObject as CalendarItemBase;
			if (calendarItemBase != null)
			{
				responseType = new ResponseType?(calendarItemBase.ResponseType);
				return;
			}
			MeetingRequest meetingRequest = commandSettings.StoreObject as MeetingRequest;
			if (meetingRequest != null)
			{
				try
				{
					responseType = meetingRequest.GetCalendarItemResponseType();
				}
				catch (CorruptDataException arg)
				{
					ExTraceGlobals.CalendarDataTracer.TraceDebug<StoreObjectId, CorruptDataException>((long)this.GetHashCode(), "[ResponseTypeProperty::ToServiceObject] Failed in correlation for item {0} with corruptdata; Exception: '{1}'", meetingRequest.StoreObjectId, arg);
				}
				catch (CorrelationFailedException arg2)
				{
					ExTraceGlobals.CalendarDataTracer.TraceDebug<StoreObjectId, CorrelationFailedException>((long)this.GetHashCode(), "[ResponseTypeProperty::ToServiceObject] Failed in correlation for item {0} with correlationfailed; Exception: '{1}'", meetingRequest.StoreObjectId, arg2);
				}
				if (responseType != null)
				{
					this.WriteServiceProperty(responseType.Value, serviceObject, propertyInformation);
					return;
				}
			}
			else
			{
				base.ToServiceObject();
			}
		}
	}
}
