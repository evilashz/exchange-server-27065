using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002AB RID: 683
	[DataContract]
	public class PimSubscription : PimSubscriptionRow
	{
		// Token: 0x06002BC4 RID: 11204 RVA: 0x0008845D File Offset: 0x0008665D
		public PimSubscription(PimSubscriptionProxy subscription) : base(subscription)
		{
		}

		// Token: 0x17001D93 RID: 7571
		// (get) Token: 0x06002BC5 RID: 11205 RVA: 0x00088466 File Offset: 0x00086666
		// (set) Token: 0x06002BC6 RID: 11206 RVA: 0x00088473 File Offset: 0x00086673
		[DataMember]
		public string DisplayName
		{
			get
			{
				return base.PimSubscriptionProxy.DisplayName;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001D94 RID: 7572
		// (get) Token: 0x06002BC7 RID: 11207 RVA: 0x0008847C File Offset: 0x0008667C
		// (set) Token: 0x06002BC8 RID: 11208 RVA: 0x000884F6 File Offset: 0x000866F6
		[DataMember]
		public string LastSuccessfulSyncText
		{
			get
			{
				string arg;
				if (base.PimSubscriptionProxy.LastSuccessfulSync != null)
				{
					arg = base.PimSubscriptionProxy.LastSuccessfulSync.UtcToUserDateTimeString();
				}
				else
				{
					arg = OwaOptionStrings.NeverSyncText;
				}
				string result = string.Empty;
				if (base.PimSubscriptionProxy.IsSuccessStatus)
				{
					result = string.Format(OwaOptionStrings.LastSynchronization, arg);
				}
				else
				{
					result = string.Format(OwaOptionStrings.LastSuccessfulSync, arg);
				}
				return result;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001D95 RID: 7573
		// (get) Token: 0x06002BC9 RID: 11209 RVA: 0x000884FD File Offset: 0x000866FD
		// (set) Token: 0x06002BCA RID: 11210 RVA: 0x0008850A File Offset: 0x0008670A
		[DataMember]
		public string DetailedStatus
		{
			get
			{
				return base.PimSubscriptionProxy.DetailedStatus;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001D96 RID: 7574
		// (get) Token: 0x06002BCB RID: 11211 RVA: 0x00088511 File Offset: 0x00086711
		// (set) Token: 0x06002BCC RID: 11212 RVA: 0x00088526 File Offset: 0x00086726
		[DataMember]
		public string CurrentStatusClass
		{
			get
			{
				if (!base.ShowWarningIcon)
				{
					return "PropertyDiv HideSyncFailedRow";
				}
				return "PropertyDiv ShowSyncFailedRow";
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001D97 RID: 7575
		// (get) Token: 0x06002BCD RID: 11213 RVA: 0x0008852D File Offset: 0x0008672D
		// (set) Token: 0x06002BCE RID: 11214 RVA: 0x00088543 File Offset: 0x00086743
		[DataMember]
		public string WarningText
		{
			get
			{
				if (base.ShowWarningIcon)
				{
					return base.StatusDescription;
				}
				return string.Empty;
			}
			set
			{
				throw new NotSupportedException();
			}
		}
	}
}
