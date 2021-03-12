using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E9E RID: 3742
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CalendarOriginatorIdConstraint : StoreObjectConstraint
	{
		// Token: 0x06008208 RID: 33288 RVA: 0x002388A0 File Offset: 0x00236AA0
		internal CalendarOriginatorIdConstraint() : base(new PropertyDefinition[]
		{
			InternalSchema.CalendarOriginatorId
		})
		{
		}

		// Token: 0x06008209 RID: 33289 RVA: 0x002388C4 File Offset: 0x00236AC4
		internal override StoreObjectValidationError Validate(ValidationContext context, IValidatablePropertyBag validatablePropertyBag)
		{
			string text = validatablePropertyBag.TryGetProperty(InternalSchema.ItemClass) as string;
			if (!string.IsNullOrEmpty(text) && (ObjectClass.IsCalendarItem(text) || ObjectClass.IsRecurrenceException(text)) && validatablePropertyBag.IsPropertyDirty(InternalSchema.CalendarOriginatorId))
			{
				object obj = validatablePropertyBag.TryGetProperty(InternalSchema.CalendarOriginatorId);
				if (obj is string)
				{
					string text2 = (string)obj;
					PropertyValueTrackingData originalPropertyInformation = validatablePropertyBag.GetOriginalPropertyInformation(InternalSchema.CalendarOriginatorId);
					if (originalPropertyInformation.PropertyValueState == PropertyTrackingInformation.Modified)
					{
						MailboxSession mailboxSession = context.Session as MailboxSession;
						if (mailboxSession != null && originalPropertyInformation.OriginalPropertyValue != null && !PropertyError.IsPropertyNotFound(originalPropertyInformation.OriginalPropertyValue))
						{
							string text3 = (string)originalPropertyInformation.OriginalPropertyValue;
							int? num = CalendarOriginatorIdProperty.Compare(text2, text3);
							if (num != null && num < 0 && !mailboxSession.Capabilities.CanSetCalendarAPIProperties && mailboxSession.LogonType != LogonType.Transport)
							{
								ExTraceGlobals.MeetingMessageTracer.TraceError<string, string>((long)this.GetHashCode(), "CalendarOriginatorIdConstraint::Validate. calendar originator value changed. Original = {0}, Current = {1}", text3, text2);
								return new StoreObjectValidationError(context, InternalSchema.CalendarOriginatorId, text2, this);
							}
						}
					}
				}
			}
			return null;
		}
	}
}
