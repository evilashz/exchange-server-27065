using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000D4 RID: 212
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[DebuggerStepThrough]
	public class UpgradeSchedulingConstraintsClient : ClientBase<IUpgradeSchedulingConstraints>, IUpgradeSchedulingConstraints
	{
		// Token: 0x06000663 RID: 1635 RVA: 0x0000DDA8 File Offset: 0x0000BFA8
		public UpgradeSchedulingConstraintsClient()
		{
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0000DDB0 File Offset: 0x0000BFB0
		public UpgradeSchedulingConstraintsClient(string endpointConfigurationName) : base(endpointConfigurationName)
		{
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0000DDB9 File Offset: 0x0000BFB9
		public UpgradeSchedulingConstraintsClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0000DDC3 File Offset: 0x0000BFC3
		public UpgradeSchedulingConstraintsClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0000DDCD File Offset: 0x0000BFCD
		public UpgradeSchedulingConstraintsClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000668 RID: 1640 RVA: 0x0000DDD8 File Offset: 0x0000BFD8
		// (remove) Token: 0x06000669 RID: 1641 RVA: 0x0000DE10 File Offset: 0x0000C010
		public event EventHandler<AsyncCompletedEventArgs> UpdateTenantReadinessCompleted;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600066A RID: 1642 RVA: 0x0000DE48 File Offset: 0x0000C048
		// (remove) Token: 0x0600066B RID: 1643 RVA: 0x0000DE80 File Offset: 0x0000C080
		public event EventHandler<AsyncCompletedEventArgs> UpdateGroupCompleted;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600066C RID: 1644 RVA: 0x0000DEB8 File Offset: 0x0000C0B8
		// (remove) Token: 0x0600066D RID: 1645 RVA: 0x0000DEF0 File Offset: 0x0000C0F0
		public event EventHandler<AsyncCompletedEventArgs> UpdateCapacityCompleted;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600066E RID: 1646 RVA: 0x0000DF28 File Offset: 0x0000C128
		// (remove) Token: 0x0600066F RID: 1647 RVA: 0x0000DF60 File Offset: 0x0000C160
		public event EventHandler<AsyncCompletedEventArgs> UpdateBlackoutCompleted;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000670 RID: 1648 RVA: 0x0000DF98 File Offset: 0x0000C198
		// (remove) Token: 0x06000671 RID: 1649 RVA: 0x0000DFD0 File Offset: 0x0000C1D0
		public event EventHandler<AsyncCompletedEventArgs> UpdateConstraintCompleted;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000672 RID: 1650 RVA: 0x0000E008 File Offset: 0x0000C208
		// (remove) Token: 0x06000673 RID: 1651 RVA: 0x0000E040 File Offset: 0x0000C240
		public event EventHandler<QueryTenantReadinessCompletedEventArgs> QueryTenantReadinessCompleted;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000674 RID: 1652 RVA: 0x0000E078 File Offset: 0x0000C278
		// (remove) Token: 0x06000675 RID: 1653 RVA: 0x0000E0B0 File Offset: 0x0000C2B0
		public event EventHandler<QueryGroupCompletedEventArgs> QueryGroupCompleted;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000676 RID: 1654 RVA: 0x0000E0E8 File Offset: 0x0000C2E8
		// (remove) Token: 0x06000677 RID: 1655 RVA: 0x0000E120 File Offset: 0x0000C320
		public event EventHandler<QueryCapacityCompletedEventArgs> QueryCapacityCompleted;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000678 RID: 1656 RVA: 0x0000E158 File Offset: 0x0000C358
		// (remove) Token: 0x06000679 RID: 1657 RVA: 0x0000E190 File Offset: 0x0000C390
		public event EventHandler<QueryBlackoutCompletedEventArgs> QueryBlackoutCompleted;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x0600067A RID: 1658 RVA: 0x0000E1C8 File Offset: 0x0000C3C8
		// (remove) Token: 0x0600067B RID: 1659 RVA: 0x0000E200 File Offset: 0x0000C400
		public event EventHandler<QueryConstraintCompletedEventArgs> QueryConstraintCompleted;

		// Token: 0x0600067C RID: 1660 RVA: 0x0000E235 File Offset: 0x0000C435
		public void UpdateTenantReadiness(TenantReadiness[] tenantReadiness)
		{
			base.Channel.UpdateTenantReadiness(tenantReadiness);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0000E243 File Offset: 0x0000C443
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginUpdateTenantReadiness(TenantReadiness[] tenantReadiness, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginUpdateTenantReadiness(tenantReadiness, callback, asyncState);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0000E253 File Offset: 0x0000C453
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void EndUpdateTenantReadiness(IAsyncResult result)
		{
			base.Channel.EndUpdateTenantReadiness(result);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0000E264 File Offset: 0x0000C464
		private IAsyncResult OnBeginUpdateTenantReadiness(object[] inValues, AsyncCallback callback, object asyncState)
		{
			TenantReadiness[] tenantReadiness = (TenantReadiness[])inValues[0];
			return this.BeginUpdateTenantReadiness(tenantReadiness, callback, asyncState);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0000E283 File Offset: 0x0000C483
		private object[] OnEndUpdateTenantReadiness(IAsyncResult result)
		{
			this.EndUpdateTenantReadiness(result);
			return null;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0000E290 File Offset: 0x0000C490
		private void OnUpdateTenantReadinessCompleted(object state)
		{
			if (this.UpdateTenantReadinessCompleted != null)
			{
				ClientBase<IUpgradeSchedulingConstraints>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<IUpgradeSchedulingConstraints>.InvokeAsyncCompletedEventArgs)state;
				this.UpdateTenantReadinessCompleted(this, new AsyncCompletedEventArgs(invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0000E2CF File Offset: 0x0000C4CF
		public void UpdateTenantReadinessAsync(TenantReadiness[] tenantReadiness)
		{
			this.UpdateTenantReadinessAsync(tenantReadiness, null);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0000E2DC File Offset: 0x0000C4DC
		public void UpdateTenantReadinessAsync(TenantReadiness[] tenantReadiness, object userState)
		{
			if (this.onBeginUpdateTenantReadinessDelegate == null)
			{
				this.onBeginUpdateTenantReadinessDelegate = new ClientBase<IUpgradeSchedulingConstraints>.BeginOperationDelegate(this.OnBeginUpdateTenantReadiness);
			}
			if (this.onEndUpdateTenantReadinessDelegate == null)
			{
				this.onEndUpdateTenantReadinessDelegate = new ClientBase<IUpgradeSchedulingConstraints>.EndOperationDelegate(this.OnEndUpdateTenantReadiness);
			}
			if (this.onUpdateTenantReadinessCompletedDelegate == null)
			{
				this.onUpdateTenantReadinessCompletedDelegate = new SendOrPostCallback(this.OnUpdateTenantReadinessCompleted);
			}
			base.InvokeAsync(this.onBeginUpdateTenantReadinessDelegate, new object[]
			{
				tenantReadiness
			}, this.onEndUpdateTenantReadinessDelegate, this.onUpdateTenantReadinessCompletedDelegate, userState);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0000E35C File Offset: 0x0000C55C
		public void UpdateGroup(Group[] group)
		{
			base.Channel.UpdateGroup(group);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0000E36A File Offset: 0x0000C56A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginUpdateGroup(Group[] group, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginUpdateGroup(group, callback, asyncState);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0000E37A File Offset: 0x0000C57A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void EndUpdateGroup(IAsyncResult result)
		{
			base.Channel.EndUpdateGroup(result);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0000E388 File Offset: 0x0000C588
		private IAsyncResult OnBeginUpdateGroup(object[] inValues, AsyncCallback callback, object asyncState)
		{
			Group[] group = (Group[])inValues[0];
			return this.BeginUpdateGroup(group, callback, asyncState);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0000E3A7 File Offset: 0x0000C5A7
		private object[] OnEndUpdateGroup(IAsyncResult result)
		{
			this.EndUpdateGroup(result);
			return null;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0000E3B4 File Offset: 0x0000C5B4
		private void OnUpdateGroupCompleted(object state)
		{
			if (this.UpdateGroupCompleted != null)
			{
				ClientBase<IUpgradeSchedulingConstraints>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<IUpgradeSchedulingConstraints>.InvokeAsyncCompletedEventArgs)state;
				this.UpdateGroupCompleted(this, new AsyncCompletedEventArgs(invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0000E3F3 File Offset: 0x0000C5F3
		public void UpdateGroupAsync(Group[] group)
		{
			this.UpdateGroupAsync(group, null);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0000E400 File Offset: 0x0000C600
		public void UpdateGroupAsync(Group[] group, object userState)
		{
			if (this.onBeginUpdateGroupDelegate == null)
			{
				this.onBeginUpdateGroupDelegate = new ClientBase<IUpgradeSchedulingConstraints>.BeginOperationDelegate(this.OnBeginUpdateGroup);
			}
			if (this.onEndUpdateGroupDelegate == null)
			{
				this.onEndUpdateGroupDelegate = new ClientBase<IUpgradeSchedulingConstraints>.EndOperationDelegate(this.OnEndUpdateGroup);
			}
			if (this.onUpdateGroupCompletedDelegate == null)
			{
				this.onUpdateGroupCompletedDelegate = new SendOrPostCallback(this.OnUpdateGroupCompleted);
			}
			base.InvokeAsync(this.onBeginUpdateGroupDelegate, new object[]
			{
				group
			}, this.onEndUpdateGroupDelegate, this.onUpdateGroupCompletedDelegate, userState);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0000E480 File Offset: 0x0000C680
		public void UpdateCapacity(GroupCapacity[] capacities)
		{
			base.Channel.UpdateCapacity(capacities);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0000E48E File Offset: 0x0000C68E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginUpdateCapacity(GroupCapacity[] capacities, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginUpdateCapacity(capacities, callback, asyncState);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0000E49E File Offset: 0x0000C69E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void EndUpdateCapacity(IAsyncResult result)
		{
			base.Channel.EndUpdateCapacity(result);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0000E4AC File Offset: 0x0000C6AC
		private IAsyncResult OnBeginUpdateCapacity(object[] inValues, AsyncCallback callback, object asyncState)
		{
			GroupCapacity[] capacities = (GroupCapacity[])inValues[0];
			return this.BeginUpdateCapacity(capacities, callback, asyncState);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0000E4CB File Offset: 0x0000C6CB
		private object[] OnEndUpdateCapacity(IAsyncResult result)
		{
			this.EndUpdateCapacity(result);
			return null;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0000E4D8 File Offset: 0x0000C6D8
		private void OnUpdateCapacityCompleted(object state)
		{
			if (this.UpdateCapacityCompleted != null)
			{
				ClientBase<IUpgradeSchedulingConstraints>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<IUpgradeSchedulingConstraints>.InvokeAsyncCompletedEventArgs)state;
				this.UpdateCapacityCompleted(this, new AsyncCompletedEventArgs(invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0000E517 File Offset: 0x0000C717
		public void UpdateCapacityAsync(GroupCapacity[] capacities)
		{
			this.UpdateCapacityAsync(capacities, null);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0000E524 File Offset: 0x0000C724
		public void UpdateCapacityAsync(GroupCapacity[] capacities, object userState)
		{
			if (this.onBeginUpdateCapacityDelegate == null)
			{
				this.onBeginUpdateCapacityDelegate = new ClientBase<IUpgradeSchedulingConstraints>.BeginOperationDelegate(this.OnBeginUpdateCapacity);
			}
			if (this.onEndUpdateCapacityDelegate == null)
			{
				this.onEndUpdateCapacityDelegate = new ClientBase<IUpgradeSchedulingConstraints>.EndOperationDelegate(this.OnEndUpdateCapacity);
			}
			if (this.onUpdateCapacityCompletedDelegate == null)
			{
				this.onUpdateCapacityCompletedDelegate = new SendOrPostCallback(this.OnUpdateCapacityCompleted);
			}
			base.InvokeAsync(this.onBeginUpdateCapacityDelegate, new object[]
			{
				capacities
			}, this.onEndUpdateCapacityDelegate, this.onUpdateCapacityCompletedDelegate, userState);
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0000E5A4 File Offset: 0x0000C7A4
		public void UpdateBlackout(GroupBlackout[] blackouts)
		{
			base.Channel.UpdateBlackout(blackouts);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0000E5B2 File Offset: 0x0000C7B2
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginUpdateBlackout(GroupBlackout[] blackouts, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginUpdateBlackout(blackouts, callback, asyncState);
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0000E5C2 File Offset: 0x0000C7C2
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void EndUpdateBlackout(IAsyncResult result)
		{
			base.Channel.EndUpdateBlackout(result);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0000E5D0 File Offset: 0x0000C7D0
		private IAsyncResult OnBeginUpdateBlackout(object[] inValues, AsyncCallback callback, object asyncState)
		{
			GroupBlackout[] blackouts = (GroupBlackout[])inValues[0];
			return this.BeginUpdateBlackout(blackouts, callback, asyncState);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0000E5EF File Offset: 0x0000C7EF
		private object[] OnEndUpdateBlackout(IAsyncResult result)
		{
			this.EndUpdateBlackout(result);
			return null;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0000E5FC File Offset: 0x0000C7FC
		private void OnUpdateBlackoutCompleted(object state)
		{
			if (this.UpdateBlackoutCompleted != null)
			{
				ClientBase<IUpgradeSchedulingConstraints>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<IUpgradeSchedulingConstraints>.InvokeAsyncCompletedEventArgs)state;
				this.UpdateBlackoutCompleted(this, new AsyncCompletedEventArgs(invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0000E63B File Offset: 0x0000C83B
		public void UpdateBlackoutAsync(GroupBlackout[] blackouts)
		{
			this.UpdateBlackoutAsync(blackouts, null);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0000E648 File Offset: 0x0000C848
		public void UpdateBlackoutAsync(GroupBlackout[] blackouts, object userState)
		{
			if (this.onBeginUpdateBlackoutDelegate == null)
			{
				this.onBeginUpdateBlackoutDelegate = new ClientBase<IUpgradeSchedulingConstraints>.BeginOperationDelegate(this.OnBeginUpdateBlackout);
			}
			if (this.onEndUpdateBlackoutDelegate == null)
			{
				this.onEndUpdateBlackoutDelegate = new ClientBase<IUpgradeSchedulingConstraints>.EndOperationDelegate(this.OnEndUpdateBlackout);
			}
			if (this.onUpdateBlackoutCompletedDelegate == null)
			{
				this.onUpdateBlackoutCompletedDelegate = new SendOrPostCallback(this.OnUpdateBlackoutCompleted);
			}
			base.InvokeAsync(this.onBeginUpdateBlackoutDelegate, new object[]
			{
				blackouts
			}, this.onEndUpdateBlackoutDelegate, this.onUpdateBlackoutCompletedDelegate, userState);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0000E6C8 File Offset: 0x0000C8C8
		public void UpdateConstraint(Constraint[] constraint)
		{
			base.Channel.UpdateConstraint(constraint);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0000E6D6 File Offset: 0x0000C8D6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginUpdateConstraint(Constraint[] constraint, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginUpdateConstraint(constraint, callback, asyncState);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0000E6E6 File Offset: 0x0000C8E6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void EndUpdateConstraint(IAsyncResult result)
		{
			base.Channel.EndUpdateConstraint(result);
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0000E6F4 File Offset: 0x0000C8F4
		private IAsyncResult OnBeginUpdateConstraint(object[] inValues, AsyncCallback callback, object asyncState)
		{
			Constraint[] constraint = (Constraint[])inValues[0];
			return this.BeginUpdateConstraint(constraint, callback, asyncState);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0000E713 File Offset: 0x0000C913
		private object[] OnEndUpdateConstraint(IAsyncResult result)
		{
			this.EndUpdateConstraint(result);
			return null;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0000E720 File Offset: 0x0000C920
		private void OnUpdateConstraintCompleted(object state)
		{
			if (this.UpdateConstraintCompleted != null)
			{
				ClientBase<IUpgradeSchedulingConstraints>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<IUpgradeSchedulingConstraints>.InvokeAsyncCompletedEventArgs)state;
				this.UpdateConstraintCompleted(this, new AsyncCompletedEventArgs(invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0000E75F File Offset: 0x0000C95F
		public void UpdateConstraintAsync(Constraint[] constraint)
		{
			this.UpdateConstraintAsync(constraint, null);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0000E76C File Offset: 0x0000C96C
		public void UpdateConstraintAsync(Constraint[] constraint, object userState)
		{
			if (this.onBeginUpdateConstraintDelegate == null)
			{
				this.onBeginUpdateConstraintDelegate = new ClientBase<IUpgradeSchedulingConstraints>.BeginOperationDelegate(this.OnBeginUpdateConstraint);
			}
			if (this.onEndUpdateConstraintDelegate == null)
			{
				this.onEndUpdateConstraintDelegate = new ClientBase<IUpgradeSchedulingConstraints>.EndOperationDelegate(this.OnEndUpdateConstraint);
			}
			if (this.onUpdateConstraintCompletedDelegate == null)
			{
				this.onUpdateConstraintCompletedDelegate = new SendOrPostCallback(this.OnUpdateConstraintCompleted);
			}
			base.InvokeAsync(this.onBeginUpdateConstraintDelegate, new object[]
			{
				constraint
			}, this.onEndUpdateConstraintDelegate, this.onUpdateConstraintCompletedDelegate, userState);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0000E7EC File Offset: 0x0000C9EC
		public TenantReadiness[] QueryTenantReadiness(Guid[] tenantIds)
		{
			return base.Channel.QueryTenantReadiness(tenantIds);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0000E7FA File Offset: 0x0000C9FA
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginQueryTenantReadiness(Guid[] tenantIds, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginQueryTenantReadiness(tenantIds, callback, asyncState);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0000E80A File Offset: 0x0000CA0A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public TenantReadiness[] EndQueryTenantReadiness(IAsyncResult result)
		{
			return base.Channel.EndQueryTenantReadiness(result);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0000E818 File Offset: 0x0000CA18
		private IAsyncResult OnBeginQueryTenantReadiness(object[] inValues, AsyncCallback callback, object asyncState)
		{
			Guid[] tenantIds = (Guid[])inValues[0];
			return this.BeginQueryTenantReadiness(tenantIds, callback, asyncState);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0000E838 File Offset: 0x0000CA38
		private object[] OnEndQueryTenantReadiness(IAsyncResult result)
		{
			TenantReadiness[] array = this.EndQueryTenantReadiness(result);
			return new object[]
			{
				array
			};
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0000E85C File Offset: 0x0000CA5C
		private void OnQueryTenantReadinessCompleted(object state)
		{
			if (this.QueryTenantReadinessCompleted != null)
			{
				ClientBase<IUpgradeSchedulingConstraints>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<IUpgradeSchedulingConstraints>.InvokeAsyncCompletedEventArgs)state;
				this.QueryTenantReadinessCompleted(this, new QueryTenantReadinessCompletedEventArgs(invokeAsyncCompletedEventArgs.Results, invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0000E8A1 File Offset: 0x0000CAA1
		public void QueryTenantReadinessAsync(Guid[] tenantIds)
		{
			this.QueryTenantReadinessAsync(tenantIds, null);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0000E8AC File Offset: 0x0000CAAC
		public void QueryTenantReadinessAsync(Guid[] tenantIds, object userState)
		{
			if (this.onBeginQueryTenantReadinessDelegate == null)
			{
				this.onBeginQueryTenantReadinessDelegate = new ClientBase<IUpgradeSchedulingConstraints>.BeginOperationDelegate(this.OnBeginQueryTenantReadiness);
			}
			if (this.onEndQueryTenantReadinessDelegate == null)
			{
				this.onEndQueryTenantReadinessDelegate = new ClientBase<IUpgradeSchedulingConstraints>.EndOperationDelegate(this.OnEndQueryTenantReadiness);
			}
			if (this.onQueryTenantReadinessCompletedDelegate == null)
			{
				this.onQueryTenantReadinessCompletedDelegate = new SendOrPostCallback(this.OnQueryTenantReadinessCompleted);
			}
			base.InvokeAsync(this.onBeginQueryTenantReadinessDelegate, new object[]
			{
				tenantIds
			}, this.onEndQueryTenantReadinessDelegate, this.onQueryTenantReadinessCompletedDelegate, userState);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0000E92C File Offset: 0x0000CB2C
		public Group[] QueryGroup(string[] groupNames)
		{
			return base.Channel.QueryGroup(groupNames);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0000E93A File Offset: 0x0000CB3A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginQueryGroup(string[] groupNames, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginQueryGroup(groupNames, callback, asyncState);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0000E94A File Offset: 0x0000CB4A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Group[] EndQueryGroup(IAsyncResult result)
		{
			return base.Channel.EndQueryGroup(result);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0000E958 File Offset: 0x0000CB58
		private IAsyncResult OnBeginQueryGroup(object[] inValues, AsyncCallback callback, object asyncState)
		{
			string[] groupNames = (string[])inValues[0];
			return this.BeginQueryGroup(groupNames, callback, asyncState);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0000E978 File Offset: 0x0000CB78
		private object[] OnEndQueryGroup(IAsyncResult result)
		{
			Group[] array = this.EndQueryGroup(result);
			return new object[]
			{
				array
			};
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0000E99C File Offset: 0x0000CB9C
		private void OnQueryGroupCompleted(object state)
		{
			if (this.QueryGroupCompleted != null)
			{
				ClientBase<IUpgradeSchedulingConstraints>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<IUpgradeSchedulingConstraints>.InvokeAsyncCompletedEventArgs)state;
				this.QueryGroupCompleted(this, new QueryGroupCompletedEventArgs(invokeAsyncCompletedEventArgs.Results, invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0000E9E1 File Offset: 0x0000CBE1
		public void QueryGroupAsync(string[] groupNames)
		{
			this.QueryGroupAsync(groupNames, null);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0000E9EC File Offset: 0x0000CBEC
		public void QueryGroupAsync(string[] groupNames, object userState)
		{
			if (this.onBeginQueryGroupDelegate == null)
			{
				this.onBeginQueryGroupDelegate = new ClientBase<IUpgradeSchedulingConstraints>.BeginOperationDelegate(this.OnBeginQueryGroup);
			}
			if (this.onEndQueryGroupDelegate == null)
			{
				this.onEndQueryGroupDelegate = new ClientBase<IUpgradeSchedulingConstraints>.EndOperationDelegate(this.OnEndQueryGroup);
			}
			if (this.onQueryGroupCompletedDelegate == null)
			{
				this.onQueryGroupCompletedDelegate = new SendOrPostCallback(this.OnQueryGroupCompleted);
			}
			base.InvokeAsync(this.onBeginQueryGroupDelegate, new object[]
			{
				groupNames
			}, this.onEndQueryGroupDelegate, this.onQueryGroupCompletedDelegate, userState);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0000EA6C File Offset: 0x0000CC6C
		public GroupCapacity[] QueryCapacity(string[] groupNames)
		{
			return base.Channel.QueryCapacity(groupNames);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0000EA7A File Offset: 0x0000CC7A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginQueryCapacity(string[] groupNames, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginQueryCapacity(groupNames, callback, asyncState);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0000EA8A File Offset: 0x0000CC8A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public GroupCapacity[] EndQueryCapacity(IAsyncResult result)
		{
			return base.Channel.EndQueryCapacity(result);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0000EA98 File Offset: 0x0000CC98
		private IAsyncResult OnBeginQueryCapacity(object[] inValues, AsyncCallback callback, object asyncState)
		{
			string[] groupNames = (string[])inValues[0];
			return this.BeginQueryCapacity(groupNames, callback, asyncState);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0000EAB8 File Offset: 0x0000CCB8
		private object[] OnEndQueryCapacity(IAsyncResult result)
		{
			GroupCapacity[] array = this.EndQueryCapacity(result);
			return new object[]
			{
				array
			};
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0000EADC File Offset: 0x0000CCDC
		private void OnQueryCapacityCompleted(object state)
		{
			if (this.QueryCapacityCompleted != null)
			{
				ClientBase<IUpgradeSchedulingConstraints>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<IUpgradeSchedulingConstraints>.InvokeAsyncCompletedEventArgs)state;
				this.QueryCapacityCompleted(this, new QueryCapacityCompletedEventArgs(invokeAsyncCompletedEventArgs.Results, invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0000EB21 File Offset: 0x0000CD21
		public void QueryCapacityAsync(string[] groupNames)
		{
			this.QueryCapacityAsync(groupNames, null);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0000EB2C File Offset: 0x0000CD2C
		public void QueryCapacityAsync(string[] groupNames, object userState)
		{
			if (this.onBeginQueryCapacityDelegate == null)
			{
				this.onBeginQueryCapacityDelegate = new ClientBase<IUpgradeSchedulingConstraints>.BeginOperationDelegate(this.OnBeginQueryCapacity);
			}
			if (this.onEndQueryCapacityDelegate == null)
			{
				this.onEndQueryCapacityDelegate = new ClientBase<IUpgradeSchedulingConstraints>.EndOperationDelegate(this.OnEndQueryCapacity);
			}
			if (this.onQueryCapacityCompletedDelegate == null)
			{
				this.onQueryCapacityCompletedDelegate = new SendOrPostCallback(this.OnQueryCapacityCompleted);
			}
			base.InvokeAsync(this.onBeginQueryCapacityDelegate, new object[]
			{
				groupNames
			}, this.onEndQueryCapacityDelegate, this.onQueryCapacityCompletedDelegate, userState);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0000EBAC File Offset: 0x0000CDAC
		public GroupBlackout[] QueryBlackout(string[] groupNames)
		{
			return base.Channel.QueryBlackout(groupNames);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0000EBBA File Offset: 0x0000CDBA
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginQueryBlackout(string[] groupNames, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginQueryBlackout(groupNames, callback, asyncState);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0000EBCA File Offset: 0x0000CDCA
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public GroupBlackout[] EndQueryBlackout(IAsyncResult result)
		{
			return base.Channel.EndQueryBlackout(result);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0000EBD8 File Offset: 0x0000CDD8
		private IAsyncResult OnBeginQueryBlackout(object[] inValues, AsyncCallback callback, object asyncState)
		{
			string[] groupNames = (string[])inValues[0];
			return this.BeginQueryBlackout(groupNames, callback, asyncState);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0000EBF8 File Offset: 0x0000CDF8
		private object[] OnEndQueryBlackout(IAsyncResult result)
		{
			GroupBlackout[] array = this.EndQueryBlackout(result);
			return new object[]
			{
				array
			};
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0000EC1C File Offset: 0x0000CE1C
		private void OnQueryBlackoutCompleted(object state)
		{
			if (this.QueryBlackoutCompleted != null)
			{
				ClientBase<IUpgradeSchedulingConstraints>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<IUpgradeSchedulingConstraints>.InvokeAsyncCompletedEventArgs)state;
				this.QueryBlackoutCompleted(this, new QueryBlackoutCompletedEventArgs(invokeAsyncCompletedEventArgs.Results, invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0000EC61 File Offset: 0x0000CE61
		public void QueryBlackoutAsync(string[] groupNames)
		{
			this.QueryBlackoutAsync(groupNames, null);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		public void QueryBlackoutAsync(string[] groupNames, object userState)
		{
			if (this.onBeginQueryBlackoutDelegate == null)
			{
				this.onBeginQueryBlackoutDelegate = new ClientBase<IUpgradeSchedulingConstraints>.BeginOperationDelegate(this.OnBeginQueryBlackout);
			}
			if (this.onEndQueryBlackoutDelegate == null)
			{
				this.onEndQueryBlackoutDelegate = new ClientBase<IUpgradeSchedulingConstraints>.EndOperationDelegate(this.OnEndQueryBlackout);
			}
			if (this.onQueryBlackoutCompletedDelegate == null)
			{
				this.onQueryBlackoutCompletedDelegate = new SendOrPostCallback(this.OnQueryBlackoutCompleted);
			}
			base.InvokeAsync(this.onBeginQueryBlackoutDelegate, new object[]
			{
				groupNames
			}, this.onEndQueryBlackoutDelegate, this.onQueryBlackoutCompletedDelegate, userState);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0000ECEC File Offset: 0x0000CEEC
		public Constraint[] QueryConstraint(string[] constraintName)
		{
			return base.Channel.QueryConstraint(constraintName);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0000ECFA File Offset: 0x0000CEFA
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginQueryConstraint(string[] constraintName, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginQueryConstraint(constraintName, callback, asyncState);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0000ED0A File Offset: 0x0000CF0A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Constraint[] EndQueryConstraint(IAsyncResult result)
		{
			return base.Channel.EndQueryConstraint(result);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0000ED18 File Offset: 0x0000CF18
		private IAsyncResult OnBeginQueryConstraint(object[] inValues, AsyncCallback callback, object asyncState)
		{
			string[] constraintName = (string[])inValues[0];
			return this.BeginQueryConstraint(constraintName, callback, asyncState);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0000ED38 File Offset: 0x0000CF38
		private object[] OnEndQueryConstraint(IAsyncResult result)
		{
			Constraint[] array = this.EndQueryConstraint(result);
			return new object[]
			{
				array
			};
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0000ED5C File Offset: 0x0000CF5C
		private void OnQueryConstraintCompleted(object state)
		{
			if (this.QueryConstraintCompleted != null)
			{
				ClientBase<IUpgradeSchedulingConstraints>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<IUpgradeSchedulingConstraints>.InvokeAsyncCompletedEventArgs)state;
				this.QueryConstraintCompleted(this, new QueryConstraintCompletedEventArgs(invokeAsyncCompletedEventArgs.Results, invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0000EDA1 File Offset: 0x0000CFA1
		public void QueryConstraintAsync(string[] constraintName)
		{
			this.QueryConstraintAsync(constraintName, null);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0000EDAC File Offset: 0x0000CFAC
		public void QueryConstraintAsync(string[] constraintName, object userState)
		{
			if (this.onBeginQueryConstraintDelegate == null)
			{
				this.onBeginQueryConstraintDelegate = new ClientBase<IUpgradeSchedulingConstraints>.BeginOperationDelegate(this.OnBeginQueryConstraint);
			}
			if (this.onEndQueryConstraintDelegate == null)
			{
				this.onEndQueryConstraintDelegate = new ClientBase<IUpgradeSchedulingConstraints>.EndOperationDelegate(this.OnEndQueryConstraint);
			}
			if (this.onQueryConstraintCompletedDelegate == null)
			{
				this.onQueryConstraintCompletedDelegate = new SendOrPostCallback(this.OnQueryConstraintCompleted);
			}
			base.InvokeAsync(this.onBeginQueryConstraintDelegate, new object[]
			{
				constraintName
			}, this.onEndQueryConstraintDelegate, this.onQueryConstraintCompletedDelegate, userState);
		}

		// Token: 0x0400033E RID: 830
		private ClientBase<IUpgradeSchedulingConstraints>.BeginOperationDelegate onBeginUpdateTenantReadinessDelegate;

		// Token: 0x0400033F RID: 831
		private ClientBase<IUpgradeSchedulingConstraints>.EndOperationDelegate onEndUpdateTenantReadinessDelegate;

		// Token: 0x04000340 RID: 832
		private SendOrPostCallback onUpdateTenantReadinessCompletedDelegate;

		// Token: 0x04000341 RID: 833
		private ClientBase<IUpgradeSchedulingConstraints>.BeginOperationDelegate onBeginUpdateGroupDelegate;

		// Token: 0x04000342 RID: 834
		private ClientBase<IUpgradeSchedulingConstraints>.EndOperationDelegate onEndUpdateGroupDelegate;

		// Token: 0x04000343 RID: 835
		private SendOrPostCallback onUpdateGroupCompletedDelegate;

		// Token: 0x04000344 RID: 836
		private ClientBase<IUpgradeSchedulingConstraints>.BeginOperationDelegate onBeginUpdateCapacityDelegate;

		// Token: 0x04000345 RID: 837
		private ClientBase<IUpgradeSchedulingConstraints>.EndOperationDelegate onEndUpdateCapacityDelegate;

		// Token: 0x04000346 RID: 838
		private SendOrPostCallback onUpdateCapacityCompletedDelegate;

		// Token: 0x04000347 RID: 839
		private ClientBase<IUpgradeSchedulingConstraints>.BeginOperationDelegate onBeginUpdateBlackoutDelegate;

		// Token: 0x04000348 RID: 840
		private ClientBase<IUpgradeSchedulingConstraints>.EndOperationDelegate onEndUpdateBlackoutDelegate;

		// Token: 0x04000349 RID: 841
		private SendOrPostCallback onUpdateBlackoutCompletedDelegate;

		// Token: 0x0400034A RID: 842
		private ClientBase<IUpgradeSchedulingConstraints>.BeginOperationDelegate onBeginUpdateConstraintDelegate;

		// Token: 0x0400034B RID: 843
		private ClientBase<IUpgradeSchedulingConstraints>.EndOperationDelegate onEndUpdateConstraintDelegate;

		// Token: 0x0400034C RID: 844
		private SendOrPostCallback onUpdateConstraintCompletedDelegate;

		// Token: 0x0400034D RID: 845
		private ClientBase<IUpgradeSchedulingConstraints>.BeginOperationDelegate onBeginQueryTenantReadinessDelegate;

		// Token: 0x0400034E RID: 846
		private ClientBase<IUpgradeSchedulingConstraints>.EndOperationDelegate onEndQueryTenantReadinessDelegate;

		// Token: 0x0400034F RID: 847
		private SendOrPostCallback onQueryTenantReadinessCompletedDelegate;

		// Token: 0x04000350 RID: 848
		private ClientBase<IUpgradeSchedulingConstraints>.BeginOperationDelegate onBeginQueryGroupDelegate;

		// Token: 0x04000351 RID: 849
		private ClientBase<IUpgradeSchedulingConstraints>.EndOperationDelegate onEndQueryGroupDelegate;

		// Token: 0x04000352 RID: 850
		private SendOrPostCallback onQueryGroupCompletedDelegate;

		// Token: 0x04000353 RID: 851
		private ClientBase<IUpgradeSchedulingConstraints>.BeginOperationDelegate onBeginQueryCapacityDelegate;

		// Token: 0x04000354 RID: 852
		private ClientBase<IUpgradeSchedulingConstraints>.EndOperationDelegate onEndQueryCapacityDelegate;

		// Token: 0x04000355 RID: 853
		private SendOrPostCallback onQueryCapacityCompletedDelegate;

		// Token: 0x04000356 RID: 854
		private ClientBase<IUpgradeSchedulingConstraints>.BeginOperationDelegate onBeginQueryBlackoutDelegate;

		// Token: 0x04000357 RID: 855
		private ClientBase<IUpgradeSchedulingConstraints>.EndOperationDelegate onEndQueryBlackoutDelegate;

		// Token: 0x04000358 RID: 856
		private SendOrPostCallback onQueryBlackoutCompletedDelegate;

		// Token: 0x04000359 RID: 857
		private ClientBase<IUpgradeSchedulingConstraints>.BeginOperationDelegate onBeginQueryConstraintDelegate;

		// Token: 0x0400035A RID: 858
		private ClientBase<IUpgradeSchedulingConstraints>.EndOperationDelegate onEndQueryConstraintDelegate;

		// Token: 0x0400035B RID: 859
		private SendOrPostCallback onQueryConstraintCompletedDelegate;
	}
}
