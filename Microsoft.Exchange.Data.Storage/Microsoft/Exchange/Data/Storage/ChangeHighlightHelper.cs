using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003BB RID: 955
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ChangeHighlightHelper
	{
		// Token: 0x06002B95 RID: 11157 RVA: 0x000ADCC8 File Offset: 0x000ABEC8
		private static Dictionary<StorePropertyDefinition, ChangeHighlightProperties> BuildPropertyGroupings(Dictionary<StorePropertyDefinition, ChangeHighlightProperties> propertyGroupings)
		{
			return new Dictionary<StorePropertyDefinition, ChangeHighlightProperties>(30)
			{
				{
					InternalSchema.MapiStartTime,
					ChangeHighlightProperties.MapiStartTime
				},
				{
					InternalSchema.MapiEndTime,
					ChangeHighlightProperties.MapiEndTime
				},
				{
					InternalSchema.Duration,
					ChangeHighlightProperties.Duration
				},
				{
					InternalSchema.AppointmentRecurring,
					ChangeHighlightProperties.RecurrenceProps
				},
				{
					InternalSchema.AppointmentRecurrenceBlob,
					ChangeHighlightProperties.RecurrenceProps
				},
				{
					InternalSchema.MapiRecurrenceType,
					ChangeHighlightProperties.RecurrenceProps
				},
				{
					InternalSchema.RecurrencePattern,
					ChangeHighlightProperties.RecurrenceProps
				},
				{
					InternalSchema.Location,
					ChangeHighlightProperties.Location
				},
				{
					InternalSchema.SubjectPrefixInternal,
					ChangeHighlightProperties.Subject
				},
				{
					InternalSchema.NormalizedSubjectInternal,
					ChangeHighlightProperties.Subject
				},
				{
					InternalSchema.TextBody,
					ChangeHighlightProperties.BodyProps
				},
				{
					InternalSchema.HtmlBody,
					ChangeHighlightProperties.BodyProps
				},
				{
					InternalSchema.RtfBody,
					ChangeHighlightProperties.BodyProps
				},
				{
					InternalSchema.RtfInSync,
					ChangeHighlightProperties.BodyProps
				},
				{
					InternalSchema.MeetingWorkspaceUrl,
					ChangeHighlightProperties.BodyProps
				},
				{
					InternalSchema.BillingInformation,
					ChangeHighlightProperties.BillMilesCompany
				},
				{
					InternalSchema.Mileage,
					ChangeHighlightProperties.BillMilesCompany
				},
				{
					InternalSchema.Companies,
					ChangeHighlightProperties.BillMilesCompany
				},
				{
					InternalSchema.MapiInternetCpid,
					ChangeHighlightProperties.BillMilesCompany
				},
				{
					InternalSchema.IsSilent,
					ChangeHighlightProperties.IsSilent
				},
				{
					InternalSchema.DisallowNewTimeProposal,
					ChangeHighlightProperties.DisallowNewTimeProposal
				},
				{
					InternalSchema.IsOnlineMeeting,
					ChangeHighlightProperties.NetMeetingProps
				},
				{
					InternalSchema.OnlineMeetingChanged,
					ChangeHighlightProperties.NetMeetingProps
				},
				{
					InternalSchema.ConferenceType,
					ChangeHighlightProperties.NetMeetingProps
				},
				{
					InternalSchema.NetShowURL,
					ChangeHighlightProperties.NetShowProps
				},
				{
					InternalSchema.NetMeetingServer,
					ChangeHighlightProperties.NetShowProps
				}
			};
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x000ADE59 File Offset: 0x000AC059
		public ChangeHighlightHelper(int highlight, bool isUpdate)
		{
			this.changeHighlight = highlight;
			this.IsUpdate = isUpdate;
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x000ADE6F File Offset: 0x000AC06F
		public ChangeHighlightHelper(int highlight) : this(highlight, false)
		{
		}

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x06002B98 RID: 11160 RVA: 0x000ADE79 File Offset: 0x000AC079
		public MeetingMessageType SuggestedMeetingType
		{
			get
			{
				if (this.changeHighlight == 0 && !this.IsUpdate)
				{
					return MeetingMessageType.NewMeetingRequest;
				}
				if ((this.changeHighlight & 7) != 0)
				{
					return MeetingMessageType.FullUpdate;
				}
				return MeetingMessageType.InformationalUpdate;
			}
		}

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x06002B99 RID: 11161 RVA: 0x000ADEA2 File Offset: 0x000AC0A2
		// (set) Token: 0x06002B9A RID: 11162 RVA: 0x000ADEAA File Offset: 0x000AC0AA
		private bool IsUpdate { get; set; }

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x06002B9B RID: 11163 RVA: 0x000ADEB3 File Offset: 0x000AC0B3
		public int HighlightFlags
		{
			get
			{
				return this.changeHighlight;
			}
		}

		// Token: 0x17000E3B RID: 3643
		public bool this[StorePropertyDefinition def]
		{
			get
			{
				return ChangeHighlightHelper.propertyGroupings.ContainsKey(def) && 0 != (this.changeHighlight & (int)ChangeHighlightHelper.propertyGroupings[def]);
			}
			set
			{
				if (!ChangeHighlightHelper.propertyGroupings.ContainsKey(def))
				{
					this.changeHighlight |= 134217728;
					return;
				}
				if (value)
				{
					this.changeHighlight |= (int)ChangeHighlightHelper.propertyGroupings[def];
					return;
				}
				if ((this.changeHighlight & 3) != 0 && ChangeHighlightHelper.propertyGroupings[def] == ChangeHighlightProperties.Duration)
				{
					return;
				}
				this.changeHighlight &= (int)(~(int)ChangeHighlightHelper.propertyGroupings[def]);
			}
		}

		// Token: 0x0400185C RID: 6236
		private static Dictionary<StorePropertyDefinition, ChangeHighlightProperties> propertyGroupings = ChangeHighlightHelper.BuildPropertyGroupings(ChangeHighlightHelper.propertyGroupings);

		// Token: 0x0400185D RID: 6237
		private int changeHighlight;

		// Token: 0x0400185E RID: 6238
		public static readonly StorePropertyDefinition[] HighlightProperties = new StorePropertyDefinition[]
		{
			InternalSchema.MapiStartTime,
			InternalSchema.MapiEndTime,
			InternalSchema.Duration,
			InternalSchema.AppointmentRecurring,
			InternalSchema.AppointmentRecurrenceBlob,
			InternalSchema.MapiRecurrenceType,
			InternalSchema.RecurrencePattern,
			InternalSchema.Location,
			InternalSchema.SubjectPrefixInternal,
			InternalSchema.NormalizedSubjectInternal,
			InternalSchema.RtfInSync,
			InternalSchema.MeetingWorkspaceUrl,
			InternalSchema.BillingInformation,
			InternalSchema.Mileage,
			InternalSchema.Companies,
			InternalSchema.MapiInternetCpid,
			InternalSchema.IsSilent,
			InternalSchema.DisallowNewTimeProposal,
			InternalSchema.IsOnlineMeeting,
			InternalSchema.OnlineMeetingChanged,
			InternalSchema.ConferenceType,
			InternalSchema.NetShowURL,
			InternalSchema.NetMeetingServer
		};
	}
}
