using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A35 RID: 2613
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetCalendarSharingPermissionsRequest : CalendarSharingRequestBase
	{
		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x060049B8 RID: 18872 RVA: 0x00102BBD File Offset: 0x00100DBD
		// (set) Token: 0x060049B9 RID: 18873 RVA: 0x00102BC5 File Offset: 0x00100DC5
		[DataMember]
		public CalendarSharingPermissionInfo[] Recipients
		{
			get
			{
				return this.recipients;
			}
			set
			{
				this.recipients = value;
				if (value != null)
				{
					this.CreateRecipientIndexStringDictionary();
				}
			}
		}

		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x060049BA RID: 18874 RVA: 0x00102BD7 File Offset: 0x00100DD7
		// (set) Token: 0x060049BB RID: 18875 RVA: 0x00102BDF File Offset: 0x00100DDF
		[DataMember]
		public DeliverMeetingRequestsType SelectedDeliveryOption { get; set; }

		// Token: 0x17001090 RID: 4240
		// (get) Token: 0x060049BC RID: 18876 RVA: 0x00102BE8 File Offset: 0x00100DE8
		// (set) Token: 0x060049BD RID: 18877 RVA: 0x00102BF0 File Offset: 0x00100DF0
		internal CalendarSharingPermissionInfo PublishedCalendarPermissions { get; private set; }

		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x060049BE RID: 18878 RVA: 0x00102BF9 File Offset: 0x00100DF9
		private Dictionary<string, CalendarSharingPermissionInfo> RecipientGivenIndexStringDictionary
		{
			get
			{
				return this.recipientGivenIndexStringDictionary;
			}
		}

		// Token: 0x060049BF RID: 18879 RVA: 0x00102C04 File Offset: 0x00100E04
		internal override void ValidateRequest()
		{
			base.ValidateRequest();
			this.recipients = (this.recipients ?? new CalendarSharingPermissionInfo[0]);
			foreach (CalendarSharingPermissionInfo calendarSharingPermissionInfo in this.Recipients)
			{
				if (calendarSharingPermissionInfo.FromPermissionsTable && string.IsNullOrEmpty(calendarSharingPermissionInfo.ID))
				{
					throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
				}
				if (string.IsNullOrEmpty(calendarSharingPermissionInfo.CurrentDetailLevel))
				{
					throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
				}
				CalendarSharingDetailLevel calendarSharingDetailLevel;
				if (!Enum.TryParse<CalendarSharingDetailLevel>(calendarSharingPermissionInfo.CurrentDetailLevel, out calendarSharingDetailLevel))
				{
					throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
				}
			}
		}

		// Token: 0x060049C0 RID: 18880 RVA: 0x00102C9C File Offset: 0x00100E9C
		internal CalendarSharingPermissionInfo RecipientGivenIndexString(string indexString)
		{
			CalendarSharingPermissionInfo result = null;
			if (indexString != null)
			{
				this.RecipientGivenIndexStringDictionary.TryGetValue(indexString, out result);
			}
			return result;
		}

		// Token: 0x060049C1 RID: 18881 RVA: 0x00102CC0 File Offset: 0x00100EC0
		private void CreateRecipientIndexStringDictionary()
		{
			this.recipientGivenIndexStringDictionary = new Dictionary<string, CalendarSharingPermissionInfo>(this.Recipients.Length);
			this.recipients = (this.recipients ?? new CalendarSharingPermissionInfo[0]);
			foreach (CalendarSharingPermissionInfo calendarSharingPermissionInfo in this.Recipients)
			{
				if (!calendarSharingPermissionInfo.FromPermissionsTable)
				{
					this.PublishedCalendarPermissions = calendarSharingPermissionInfo;
				}
				else
				{
					this.recipientGivenIndexStringDictionary[calendarSharingPermissionInfo.ID] = calendarSharingPermissionInfo;
				}
			}
		}

		// Token: 0x04002A17 RID: 10775
		private Dictionary<string, CalendarSharingPermissionInfo> recipientGivenIndexStringDictionary;

		// Token: 0x04002A18 RID: 10776
		private CalendarSharingPermissionInfo[] recipients;
	}
}
