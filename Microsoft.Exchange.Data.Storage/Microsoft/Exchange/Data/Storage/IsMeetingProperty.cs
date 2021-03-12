using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C31 RID: 3121
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class IsMeetingProperty : SmartPropertyDefinition
	{
		// Token: 0x06006ECD RID: 28365 RVA: 0x001DCCCC File Offset: 0x001DAECC
		internal IsMeetingProperty() : base("IsMeeting", typeof(bool), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.AppointmentStateInternal, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.MeetingRequestWasSent, PropertyDependencyType.NeedToReadForWrite)
		})
		{
		}

		// Token: 0x06006ECE RID: 28366 RVA: 0x001DCD18 File Offset: 0x001DAF18
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			object value = propertyBag.GetValue(InternalSchema.AppointmentStateInternal);
			if (value is int)
			{
				return ((int)value & 1) != 0;
			}
			return value;
		}

		// Token: 0x06006ECF RID: 28367 RVA: 0x001DCD50 File Offset: 0x001DAF50
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			object value2 = propertyBag.GetValue(InternalSchema.MeetingRequestWasSent);
			if (value2 is bool && (bool)value2 && !(bool)value)
			{
				throw new InvalidOperationException(ServerStrings.ExCannotRevertSentMeetingToAppointment);
			}
			int num = 0;
			object value3 = propertyBag.GetValue(InternalSchema.AppointmentStateInternal);
			if (value3 is int)
			{
				num = (int)value3;
			}
			if ((bool)value)
			{
				num |= 1;
			}
			else
			{
				num &= -2;
			}
			propertyBag.SetValueWithFixup(InternalSchema.AppointmentStateInternal, num);
		}
	}
}
