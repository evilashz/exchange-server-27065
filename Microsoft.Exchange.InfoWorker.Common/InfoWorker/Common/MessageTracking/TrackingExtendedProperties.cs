using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002FB RID: 763
	[Serializable]
	internal class TrackingExtendedProperties
	{
		// Token: 0x0600168E RID: 5774 RVA: 0x00069148 File Offset: 0x00067348
		internal TrackingExtendedProperties() : this(false, false, null, false, string.Empty, string.Empty, string.Empty, false)
		{
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x00069178 File Offset: 0x00067378
		internal TrackingExtendedProperties(bool expandTree, bool searchAsRecip, TimeSpan? timeout, bool getAdditionalRecords, bool searchForModerationResult) : this(expandTree, searchAsRecip, timeout, getAdditionalRecords, string.Empty, string.Empty, string.Empty, searchForModerationResult)
		{
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x000691A4 File Offset: 0x000673A4
		internal TrackingExtendedProperties(string messageTrackingReportId) : this(false, false, null, false, messageTrackingReportId, string.Empty, string.Empty, false)
		{
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x000691D0 File Offset: 0x000673D0
		internal TrackingExtendedProperties(bool expandTree, bool searchAsRecip, TimeSpan? timeout, bool getAdditionalRecords, string messageTrackingReportId, string arbitrationMailboxAddress, string initMessageId, bool searchForModerationResult)
		{
			this.ExpandTree = expandTree;
			this.SearchAsRecip = searchAsRecip;
			this.Timeout = timeout;
			this.GetAdditionalRecords = getAdditionalRecords;
			this.MessageTrackingReportId = messageTrackingReportId;
			this.ArbitrationMailboxAddress = arbitrationMailboxAddress;
			this.InitMessageId = initMessageId;
			this.SearchForModerationResult = searchForModerationResult;
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x00069220 File Offset: 0x00067420
		// (set) Token: 0x06001693 RID: 5779 RVA: 0x00069228 File Offset: 0x00067428
		internal bool ExpandTree { get; private set; }

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x00069231 File Offset: 0x00067431
		// (set) Token: 0x06001695 RID: 5781 RVA: 0x00069239 File Offset: 0x00067439
		internal bool SearchAsRecip { get; private set; }

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001696 RID: 5782 RVA: 0x00069242 File Offset: 0x00067442
		// (set) Token: 0x06001697 RID: 5783 RVA: 0x0006924A File Offset: 0x0006744A
		internal TimeSpan? Timeout { get; private set; }

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x00069253 File Offset: 0x00067453
		// (set) Token: 0x06001699 RID: 5785 RVA: 0x0006925B File Offset: 0x0006745B
		internal bool GetAdditionalRecords { get; private set; }

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x00069264 File Offset: 0x00067464
		// (set) Token: 0x0600169B RID: 5787 RVA: 0x0006926C File Offset: 0x0006746C
		internal bool SearchForModerationResult { get; private set; }

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x00069275 File Offset: 0x00067475
		// (set) Token: 0x0600169D RID: 5789 RVA: 0x0006927D File Offset: 0x0006747D
		internal string MessageTrackingReportId { get; set; }

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x00069286 File Offset: 0x00067486
		// (set) Token: 0x0600169F RID: 5791 RVA: 0x0006928E File Offset: 0x0006748E
		internal string ArbitrationMailboxAddress { get; private set; }

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060016A0 RID: 5792 RVA: 0x00069297 File Offset: 0x00067497
		// (set) Token: 0x060016A1 RID: 5793 RVA: 0x0006929F File Offset: 0x0006749F
		internal string InitMessageId { get; private set; }

		// Token: 0x060016A2 RID: 5794 RVA: 0x000692A8 File Offset: 0x000674A8
		internal static TrackingExtendedProperties CreateFromTrackingPropertyArray(TrackingPropertyType[] properties)
		{
			bool expandTree = false;
			bool searchAsRecip = false;
			bool searchForModerationResult = false;
			TimeSpan? timeout = null;
			bool getAdditionalRecords = false;
			string messageTrackingReportId = string.Empty;
			string arbitrationMailboxAddress = string.Empty;
			string initMessageId = string.Empty;
			if (properties == null)
			{
				return new TrackingExtendedProperties();
			}
			foreach (TrackingPropertyType trackingPropertyType in properties)
			{
				if ("ExpandTree".Equals(trackingPropertyType.Name, StringComparison.Ordinal))
				{
					expandTree = true;
				}
				else if ("SearchAsRecip".Equals(trackingPropertyType.Name, StringComparison.Ordinal))
				{
					searchAsRecip = true;
				}
				else if ("Timeout".Equals(trackingPropertyType.Name, StringComparison.Ordinal))
				{
					timeout = Parse.ParseFromMilliseconds(trackingPropertyType.Value);
				}
				else if ("GetAdditionalRecords".Equals(trackingPropertyType.Name, StringComparison.Ordinal))
				{
					getAdditionalRecords = true;
				}
				else if ("SearchForModerationResult".Equals(trackingPropertyType.Name, StringComparison.Ordinal))
				{
					searchForModerationResult = true;
				}
				else if ("MessageTrackingReportId".Equals(trackingPropertyType.Name, StringComparison.Ordinal))
				{
					messageTrackingReportId = trackingPropertyType.Value;
				}
				else if ("ArbitrationMailboxAddress".Equals(trackingPropertyType.Name, StringComparison.Ordinal))
				{
					arbitrationMailboxAddress = trackingPropertyType.Value;
				}
				else if ("InitiationMessageId".Equals(trackingPropertyType.Name, StringComparison.Ordinal))
				{
					initMessageId = trackingPropertyType.Value;
				}
			}
			return new TrackingExtendedProperties(expandTree, searchAsRecip, timeout, getAdditionalRecords, messageTrackingReportId, arbitrationMailboxAddress, initMessageId, searchForModerationResult);
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x00069400 File Offset: 0x00067600
		internal TrackingPropertyType[] ToTrackingPropertyArray()
		{
			List<TrackingPropertyType> list = new List<TrackingPropertyType>(4);
			if (this.ExpandTree)
			{
				list.Add(new TrackingPropertyType
				{
					Name = "ExpandTree"
				});
			}
			if (this.SearchAsRecip)
			{
				list.Add(new TrackingPropertyType
				{
					Name = "SearchAsRecip"
				});
			}
			if (this.Timeout != null)
			{
				list.Add(new TrackingPropertyType
				{
					Name = "Timeout",
					Value = this.Timeout.Value.TotalMilliseconds.ToString(CultureInfo.InvariantCulture)
				});
			}
			if (this.GetAdditionalRecords)
			{
				list.Add(new TrackingPropertyType
				{
					Name = "GetAdditionalRecords"
				});
			}
			if (this.SearchForModerationResult)
			{
				list.Add(new TrackingPropertyType
				{
					Name = "SearchForModerationResult"
				});
			}
			if (!string.IsNullOrEmpty(this.MessageTrackingReportId))
			{
				list.Add(new TrackingPropertyType
				{
					Name = "MessageTrackingReportId",
					Value = this.MessageTrackingReportId
				});
			}
			if (!string.IsNullOrEmpty(this.ArbitrationMailboxAddress))
			{
				list.Add(new TrackingPropertyType
				{
					Name = "ArbitrationMailboxAddress",
					Value = this.ArbitrationMailboxAddress
				});
			}
			if (!string.IsNullOrEmpty(this.InitMessageId))
			{
				list.Add(new TrackingPropertyType
				{
					Name = "InitiationMessageId",
					Value = this.InitMessageId
				});
			}
			return list.ToArray();
		}
	}
}
