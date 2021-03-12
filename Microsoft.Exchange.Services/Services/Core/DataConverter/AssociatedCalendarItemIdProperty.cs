using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000D3 RID: 211
	internal sealed class AssociatedCalendarItemIdProperty : ComplexPropertyBase, IToXmlCommand, IToServiceObjectCommand, IPropertyCommand
	{
		// Token: 0x060005C3 RID: 1475 RVA: 0x0001E3FE File Offset: 0x0001C5FE
		private AssociatedCalendarItemIdProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001E407 File Offset: 0x0001C607
		public static AssociatedCalendarItemIdProperty CreateCommand(CommandContext commandContext)
		{
			return new AssociatedCalendarItemIdProperty(commandContext);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0001E40F File Offset: 0x0001C60F
		public void ToXml()
		{
			throw new InvalidOperationException("AssociatedCalendarItemIdProperty.ToXml should not be called.");
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001E41C File Offset: 0x0001C61C
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			MeetingMessage meetingMessage = (MeetingMessage)commandSettings.StoreObject;
			try
			{
				IdAndSession correlatedItemIdAndSession = AssociatedCalendarItemIdProperty.GetCorrelatedItemIdAndSession(meetingMessage);
				if (correlatedItemIdAndSession != null)
				{
					ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(correlatedItemIdAndSession.Id, correlatedItemIdAndSession, null);
					commandSettings.ServiceObject.PropertyBag[propertyInformation] = new ItemId(concatenatedId.Id, concatenatedId.ChangeKey);
				}
			}
			catch (ObjectNotFoundException arg)
			{
				ExTraceGlobals.CreateItemCallTracer.TraceDebug<bool, LogonType, ObjectNotFoundException>((long)this.GetHashCode(), "[AssosiatedCalendarItemIdProperty::ToServiceObject] meetingMessage.IsDelegated='{0}'; meetingMessage.Session.LogonType='{1}'; Exception: '{2}'", meetingMessage.IsDelegated(), meetingMessage.Session.LogonType, arg);
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0001E4C4 File Offset: 0x0001C6C4
		private static IdAndSession GetCorrelatedItemIdAndSession(MeetingMessage meetingMessage)
		{
			try
			{
				IdAndSession result = null;
				CalendarItemBase cachedCorrelatedItem = meetingMessage.GetCachedCorrelatedItem();
				if (cachedCorrelatedItem != null)
				{
					result = new IdAndSession(cachedCorrelatedItem.Id, cachedCorrelatedItem.Session);
				}
				return result;
			}
			catch (CorrelationFailedException ex)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<CorrelationFailedException>(0L, "CalendarItem associated with MeetingMessage could not be found. Exception '{0}'.", ex);
				if (ex.InnerException is NotSupportedWithServerVersionException)
				{
					throw new WrongServerVersionDelegateException(ex);
				}
			}
			catch (CorruptDataException arg)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<CorruptDataException>(0L, "CalendarItem associated with the meeting message is corrupt. Exception '{0}'.", arg);
			}
			catch (VirusException arg2)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<VirusException>(0L, "CalendarItem associated with the meeting message has a virus. Exception '{0}'.", arg2);
			}
			catch (RecurrenceException arg3)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<RecurrenceException>(0L, "CalendarItem associated with the meeting message has a recurrence problem. Exception '{0}'.", arg3);
			}
			return null;
		}
	}
}
