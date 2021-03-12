using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;
using Microsoft.Exchange.Diagnostics.WorkloadManagement.Implementation;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement
{
	// Token: 0x020001F0 RID: 496
	internal sealed class ActivityScope : DisposeTrackableBase, IActivityScope
	{
		// Token: 0x06000E21 RID: 3617 RVA: 0x0003B22F File Offset: 0x0003942F
		internal ActivityScope(ActivityScopeImpl activityScopeImpl)
		{
			if (activityScopeImpl == null)
			{
				throw new ArgumentNullException("activityScopeImpl");
			}
			this.activityScopeImpl = activityScopeImpl;
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x0003B24C File Offset: 0x0003944C
		public Guid ActivityId
		{
			get
			{
				return this.activityScopeImpl.ActivityId;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000E23 RID: 3619 RVA: 0x0003B259 File Offset: 0x00039459
		public Guid LocalId
		{
			get
			{
				return this.activityScopeImpl.LocalId;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x0003B266 File Offset: 0x00039466
		public ActivityContextStatus Status
		{
			get
			{
				return this.activityScopeImpl.Status;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x0003B273 File Offset: 0x00039473
		// (set) Token: 0x06000E26 RID: 3622 RVA: 0x0003B280 File Offset: 0x00039480
		public ActivityType ActivityType
		{
			get
			{
				return this.activityScopeImpl.ActivityType;
			}
			internal set
			{
				this.activityScopeImpl.ActivityType = value;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x0003B28E File Offset: 0x0003948E
		public DateTime StartTime
		{
			get
			{
				return this.activityScopeImpl.StartTime;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x0003B29B File Offset: 0x0003949B
		public DateTime? EndTime
		{
			get
			{
				return this.activityScopeImpl.EndTime;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x0003B2A8 File Offset: 0x000394A8
		public double TotalMilliseconds
		{
			get
			{
				return this.activityScopeImpl.TotalMilliseconds;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x0003B2B5 File Offset: 0x000394B5
		// (set) Token: 0x06000E2B RID: 3627 RVA: 0x0003B2C2 File Offset: 0x000394C2
		public object UserState
		{
			get
			{
				return this.activityScopeImpl.UserState;
			}
			set
			{
				this.activityScopeImpl.UserState = value;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x0003B2D0 File Offset: 0x000394D0
		// (set) Token: 0x06000E2D RID: 3629 RVA: 0x0003B2DD File Offset: 0x000394DD
		public string UserId
		{
			get
			{
				return this.activityScopeImpl.UserId;
			}
			set
			{
				this.activityScopeImpl.UserId = value;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000E2E RID: 3630 RVA: 0x0003B2EB File Offset: 0x000394EB
		// (set) Token: 0x06000E2F RID: 3631 RVA: 0x0003B2F8 File Offset: 0x000394F8
		public string Puid
		{
			get
			{
				return this.activityScopeImpl.Puid;
			}
			set
			{
				this.activityScopeImpl.Puid = value;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x0003B306 File Offset: 0x00039506
		// (set) Token: 0x06000E31 RID: 3633 RVA: 0x0003B313 File Offset: 0x00039513
		public string UserEmail
		{
			get
			{
				return this.activityScopeImpl.UserEmail;
			}
			set
			{
				this.activityScopeImpl.UserEmail = value;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x0003B321 File Offset: 0x00039521
		// (set) Token: 0x06000E33 RID: 3635 RVA: 0x0003B32E File Offset: 0x0003952E
		public string AuthenticationType
		{
			get
			{
				return this.activityScopeImpl.AuthenticationType;
			}
			set
			{
				this.activityScopeImpl.AuthenticationType = value;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000E34 RID: 3636 RVA: 0x0003B33C File Offset: 0x0003953C
		// (set) Token: 0x06000E35 RID: 3637 RVA: 0x0003B349 File Offset: 0x00039549
		public string AuthenticationToken
		{
			get
			{
				return this.activityScopeImpl.AuthenticationToken;
			}
			set
			{
				this.activityScopeImpl.AuthenticationToken = value;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000E36 RID: 3638 RVA: 0x0003B357 File Offset: 0x00039557
		// (set) Token: 0x06000E37 RID: 3639 RVA: 0x0003B364 File Offset: 0x00039564
		public string TenantId
		{
			get
			{
				return this.activityScopeImpl.TenantId;
			}
			set
			{
				this.activityScopeImpl.TenantId = value;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000E38 RID: 3640 RVA: 0x0003B372 File Offset: 0x00039572
		// (set) Token: 0x06000E39 RID: 3641 RVA: 0x0003B37F File Offset: 0x0003957F
		public string TenantType
		{
			get
			{
				return this.activityScopeImpl.TenantType;
			}
			set
			{
				this.activityScopeImpl.TenantType = value;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000E3A RID: 3642 RVA: 0x0003B38D File Offset: 0x0003958D
		// (set) Token: 0x06000E3B RID: 3643 RVA: 0x0003B39A File Offset: 0x0003959A
		public string Component
		{
			get
			{
				return this.activityScopeImpl.Component;
			}
			set
			{
				this.activityScopeImpl.Component = value;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x0003B3A8 File Offset: 0x000395A8
		// (set) Token: 0x06000E3D RID: 3645 RVA: 0x0003B3B5 File Offset: 0x000395B5
		public string ComponentInstance
		{
			get
			{
				return this.activityScopeImpl.ComponentInstance;
			}
			set
			{
				this.activityScopeImpl.ComponentInstance = value;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x0003B3C3 File Offset: 0x000395C3
		// (set) Token: 0x06000E3F RID: 3647 RVA: 0x0003B3D0 File Offset: 0x000395D0
		public string Feature
		{
			get
			{
				return this.activityScopeImpl.Feature;
			}
			set
			{
				this.activityScopeImpl.Feature = value;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x0003B3DE File Offset: 0x000395DE
		// (set) Token: 0x06000E41 RID: 3649 RVA: 0x0003B3EB File Offset: 0x000395EB
		public string Protocol
		{
			get
			{
				return this.activityScopeImpl.Protocol;
			}
			set
			{
				this.activityScopeImpl.Protocol = value;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x0003B3F9 File Offset: 0x000395F9
		// (set) Token: 0x06000E43 RID: 3651 RVA: 0x0003B406 File Offset: 0x00039606
		public string ClientInfo
		{
			get
			{
				return this.activityScopeImpl.ClientInfo;
			}
			set
			{
				this.activityScopeImpl.ClientInfo = value;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000E44 RID: 3652 RVA: 0x0003B414 File Offset: 0x00039614
		// (set) Token: 0x06000E45 RID: 3653 RVA: 0x0003B421 File Offset: 0x00039621
		public string ClientRequestId
		{
			get
			{
				return this.activityScopeImpl.ClientRequestId;
			}
			set
			{
				this.activityScopeImpl.ClientRequestId = value;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x0003B42F File Offset: 0x0003962F
		// (set) Token: 0x06000E47 RID: 3655 RVA: 0x0003B43C File Offset: 0x0003963C
		public string ReturnClientRequestId
		{
			get
			{
				return this.activityScopeImpl.ReturnClientRequestId;
			}
			set
			{
				this.activityScopeImpl.ReturnClientRequestId = value;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000E48 RID: 3656 RVA: 0x0003B44A File Offset: 0x0003964A
		// (set) Token: 0x06000E49 RID: 3657 RVA: 0x0003B457 File Offset: 0x00039657
		public string Action
		{
			get
			{
				return this.activityScopeImpl.Action;
			}
			set
			{
				this.activityScopeImpl.Action = value;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x0003B465 File Offset: 0x00039665
		public IEnumerable<KeyValuePair<Enum, object>> Metadata
		{
			get
			{
				return this.activityScopeImpl.Metadata;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x0003B472 File Offset: 0x00039672
		public IEnumerable<KeyValuePair<OperationKey, OperationStatistics>> Statistics
		{
			get
			{
				return this.activityScopeImpl.Statistics;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x0003B47F File Offset: 0x0003967F
		internal ActivityScopeImpl ActivityScopeImpl
		{
			get
			{
				return this.activityScopeImpl;
			}
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0003B487 File Offset: 0x00039687
		public ActivityContextState Suspend()
		{
			return this.activityScopeImpl.Suspend();
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0003B494 File Offset: 0x00039694
		public void End()
		{
			this.Dispose();
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0003B49C File Offset: 0x0003969C
		public bool AddOperation(ActivityOperationType operation, string instance, float value = 0f, int count = 1)
		{
			return this.activityScopeImpl.AddOperation(operation, instance, value, count);
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x0003B4AE File Offset: 0x000396AE
		public void SetProperty(Enum property, string value)
		{
			this.activityScopeImpl.SetProperty(property, value);
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x0003B4BD File Offset: 0x000396BD
		public bool AppendToProperty(Enum property, string value)
		{
			return this.activityScopeImpl.AppendToProperty(property, value);
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x0003B4CC File Offset: 0x000396CC
		public string GetProperty(Enum property)
		{
			return this.activityScopeImpl.GetProperty(property);
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0003B4DA File Offset: 0x000396DA
		public List<KeyValuePair<string, object>> GetFormattableMetadata()
		{
			return this.activityScopeImpl.GetFormattableMetadata();
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x0003B4E7 File Offset: 0x000396E7
		public IEnumerable<KeyValuePair<string, object>> GetFormattableMetadata(IEnumerable<Enum> properties)
		{
			return this.activityScopeImpl.GetFormattableMetadata(properties);
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0003B4F5 File Offset: 0x000396F5
		public List<KeyValuePair<string, object>> GetFormattableStatistics()
		{
			return this.activityScopeImpl.GetFormattableStatistics();
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0003B502 File Offset: 0x00039702
		public void UpdateFromMessage(HttpRequestMessageProperty wcfMessage)
		{
			this.activityScopeImpl.UpdateFromMessage(wcfMessage);
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x0003B510 File Offset: 0x00039710
		public void UpdateFromMessage(OperationContext wcfOperationContext)
		{
			this.activityScopeImpl.UpdateFromMessage(wcfOperationContext);
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x0003B51E File Offset: 0x0003971E
		public void UpdateFromMessage(HttpRequest httpRequest)
		{
			this.activityScopeImpl.UpdateFromMessage(httpRequest);
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x0003B52C File Offset: 0x0003972C
		public void UpdateFromMessage(HttpRequestBase httpRequestBase)
		{
			this.activityScopeImpl.UpdateFromMessage(httpRequestBase);
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x0003B53A File Offset: 0x0003973A
		public void SerializeTo(HttpRequestMessageProperty wcfMessage)
		{
			this.activityScopeImpl.SerializeTo(wcfMessage);
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0003B548 File Offset: 0x00039748
		public void SerializeTo(OperationContext wcfOperationContext)
		{
			this.activityScopeImpl.SerializeTo(wcfOperationContext);
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x0003B556 File Offset: 0x00039756
		public void SerializeTo(HttpWebRequest httpWebRequest)
		{
			this.activityScopeImpl.SerializeTo(httpWebRequest);
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x0003B564 File Offset: 0x00039764
		public void SerializeTo(HttpResponse httpResponse)
		{
			this.activityScopeImpl.SerializeTo(httpResponse);
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x0003B572 File Offset: 0x00039772
		public void SerializeMinimalTo(HttpRequestBase httpRequest)
		{
			this.activityScopeImpl.SerializeMinimalTo(httpRequest);
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x0003B580 File Offset: 0x00039780
		public void SerializeMinimalTo(HttpWebRequest httpWebRequest)
		{
			this.activityScopeImpl.SerializeMinimalTo(httpWebRequest);
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x0003B58E File Offset: 0x0003978E
		public AggregatedOperationStatistics TakeStatisticsSnapshot(AggregatedOperationType type)
		{
			return this.activityScopeImpl.TakeStatisticsSnapshot(type);
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x0003B59C File Offset: 0x0003979C
		public override string ToString()
		{
			return this.ActivityScopeImpl.ToString();
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x0003B5A9 File Offset: 0x000397A9
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ActivityScope>(this);
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0003B5B1 File Offset: 0x000397B1
		protected override void InternalDispose(bool disposing)
		{
			if (!base.IsDisposed && this.activityScopeImpl != null)
			{
				this.activityScopeImpl.End();
			}
		}

		// Token: 0x04000A7A RID: 2682
		public const string ActivityIdKey = "ActID";

		// Token: 0x04000A7B RID: 2683
		private readonly ActivityScopeImpl activityScopeImpl;
	}
}
