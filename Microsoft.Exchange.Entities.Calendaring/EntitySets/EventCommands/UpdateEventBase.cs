using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x02000055 RID: 85
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract]
	internal abstract class UpdateEventBase : UpdateStorageEntityCommand<Events, Event>
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00008D45 File Offset: 0x00006F45
		// (set) Token: 0x06000227 RID: 551 RVA: 0x00008D4D File Offset: 0x00006F4D
		[DataMember]
		public UpdateEventParameters UpdateEventParameters { get; set; }

		// Token: 0x06000228 RID: 552 RVA: 0x00008D56 File Offset: 0x00006F56
		protected override void UpdateCustomLoggingData()
		{
			base.UpdateCustomLoggingData();
			this.SetCustomLoggingData("UpdateEventParameters", this.UpdateEventParameters);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00008D70 File Offset: 0x00006F70
		protected void MergeAttendeesList(IList<Attendee> originalAttendees)
		{
			if (this.UpdateEventParameters != null)
			{
				if (base.Entity.Attendees != null)
				{
					throw new ArgumentException(CalendaringStrings.UpdateEventParametersAndAttendeesCantBeSpecified);
				}
				IList<Attendee> list = this.UpdateEventParameters.AttendeesToAdd ?? UpdateEventBase.EmptyAttendeeList;
				bool[] array = new bool[list.Count];
				Dictionary<string, int> dictionary = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
				for (int i = 0; i < list.Count; i++)
				{
					dictionary[list[i].EmailAddress] = i;
					array[i] = false;
				}
				HashSet<string> hashSet = new HashSet<string>(this.UpdateEventParameters.AttendeesToRemove ?? UpdateEventBase.EmptyStringList, StringComparer.OrdinalIgnoreCase);
				base.Entity.Attendees = new List<Attendee>();
				foreach (Attendee attendee in originalAttendees)
				{
					if (!hashSet.Contains(attendee.EmailAddress))
					{
						int num;
						if (dictionary.TryGetValue(attendee.EmailAddress, out num))
						{
							base.Entity.Attendees.Add(list[num]);
							array[num] = true;
						}
						else
						{
							base.Entity.Attendees.Add(attendee);
						}
					}
				}
				for (int j = 0; j < list.Count; j++)
				{
					if (!array[j])
					{
						base.Entity.Attendees.Add(list[j]);
					}
				}
			}
		}

		// Token: 0x0400009A RID: 154
		private static readonly List<Attendee> EmptyAttendeeList = new List<Attendee>();

		// Token: 0x0400009B RID: 155
		private static readonly List<string> EmptyStringList = new List<string>();
	}
}
