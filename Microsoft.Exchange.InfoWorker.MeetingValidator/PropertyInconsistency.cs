using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x02000013 RID: 19
	internal sealed class PropertyInconsistency : Inconsistency
	{
		// Token: 0x06000078 RID: 120 RVA: 0x000046C7 File Offset: 0x000028C7
		private PropertyInconsistency()
		{
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000046D0 File Offset: 0x000028D0
		private PropertyInconsistency(RoleType owner, CalendarInconsistencyFlag flag, string propertyName, object expectedValue, object actualValue, CalendarValidationContext context) : base(owner, string.Empty, flag, context)
		{
			this.PropertyName = propertyName;
			this.ExpectedValue = ((expectedValue == null) ? "<NULL>" : expectedValue.ToString());
			this.ActualValue = ((actualValue == null) ? "<NULL>" : actualValue.ToString());
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004723 File Offset: 0x00002923
		internal static PropertyInconsistency CreateInstance(RoleType owner, CalendarInconsistencyFlag flag, string propertyName, object expectedValue, object actualValue, CalendarValidationContext context)
		{
			return new PropertyInconsistency(owner, flag, propertyName, expectedValue, actualValue, context);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004734 File Offset: 0x00002934
		internal override RumInfo CreateRumInfo(CalendarValidationContext context, IList<Attendee> attendees)
		{
			CalendarInconsistencyFlag flag = base.Flag;
			if (flag != CalendarInconsistencyFlag.Cancellation)
			{
				return base.CreateRumInfo(context, attendees);
			}
			bool flag2;
			if (!bool.TryParse(this.ExpectedValue, out flag2))
			{
				throw new ArgumentException("Expected value for cancellation inconsistency should be Boolean.", "inconsistency.ExpectedValue");
			}
			if (flag2)
			{
				return CancellationRumInfo.CreateMasterInstance(attendees);
			}
			return UpdateRumInfo.CreateMasterInstance(attendees, base.Flag);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600007C RID: 124 RVA: 0x0000478B File Offset: 0x0000298B
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00004793 File Offset: 0x00002993
		internal string PropertyName { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600007E RID: 126 RVA: 0x0000479C File Offset: 0x0000299C
		// (set) Token: 0x0600007F RID: 127 RVA: 0x000047A4 File Offset: 0x000029A4
		internal string ExpectedValue { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000047AD File Offset: 0x000029AD
		// (set) Token: 0x06000081 RID: 129 RVA: 0x000047B5 File Offset: 0x000029B5
		internal string ActualValue { get; private set; }

		// Token: 0x06000082 RID: 130 RVA: 0x000047C0 File Offset: 0x000029C0
		public override void WriteXml(XmlWriter writer)
		{
			writer.WriteElementString("Owner", base.Owner.ToString());
			writer.WriteElementString("PropertyName", this.PropertyName);
			writer.WriteElementString("ExpectedValue", this.ExpectedValue);
			writer.WriteElementString("ActualValue", this.ActualValue);
		}

		// Token: 0x04000024 RID: 36
		private const string NullValueString = "<NULL>";
	}
}
