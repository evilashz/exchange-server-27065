using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000CC RID: 204
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class UpgradeHandlerClient : ClientBase<IUpgradeHandler>, IUpgradeHandler
	{
		// Token: 0x06000618 RID: 1560 RVA: 0x0000D727 File Offset: 0x0000B927
		public UpgradeHandlerClient()
		{
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0000D72F File Offset: 0x0000B92F
		public UpgradeHandlerClient(string endpointConfigurationName) : base(endpointConfigurationName)
		{
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0000D738 File Offset: 0x0000B938
		public UpgradeHandlerClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0000D742 File Offset: 0x0000B942
		public UpgradeHandlerClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0000D74C File Offset: 0x0000B94C
		public UpgradeHandlerClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600061D RID: 1565 RVA: 0x0000D758 File Offset: 0x0000B958
		// (remove) Token: 0x0600061E RID: 1566 RVA: 0x0000D790 File Offset: 0x0000B990
		public event EventHandler<QueryWorkItemsCompletedEventArgs> QueryWorkItemsCompleted;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600061F RID: 1567 RVA: 0x0000D7C8 File Offset: 0x0000B9C8
		// (remove) Token: 0x06000620 RID: 1568 RVA: 0x0000D800 File Offset: 0x0000BA00
		public event EventHandler<QueryTenantWorkItemsCompletedEventArgs> QueryTenantWorkItemsCompleted;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000621 RID: 1569 RVA: 0x0000D838 File Offset: 0x0000BA38
		// (remove) Token: 0x06000622 RID: 1570 RVA: 0x0000D870 File Offset: 0x0000BA70
		public event EventHandler<AsyncCompletedEventArgs> UpdateWorkItemCompleted;

		// Token: 0x06000623 RID: 1571 RVA: 0x0000D8A5 File Offset: 0x0000BAA5
		public WorkItemQueryResult QueryWorkItems(string groupName, string tenantTier, string workItemType, WorkItemStatus status, int pageSize, byte[] bookmark)
		{
			return base.Channel.QueryWorkItems(groupName, tenantTier, workItemType, status, pageSize, bookmark);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0000D8BC File Offset: 0x0000BABC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginQueryWorkItems(string groupName, string tenantTier, string workItemType, WorkItemStatus status, int pageSize, byte[] bookmark, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginQueryWorkItems(groupName, tenantTier, workItemType, status, pageSize, bookmark, callback, asyncState);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0000D8E1 File Offset: 0x0000BAE1
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public WorkItemQueryResult EndQueryWorkItems(IAsyncResult result)
		{
			return base.Channel.EndQueryWorkItems(result);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0000D8F0 File Offset: 0x0000BAF0
		private IAsyncResult OnBeginQueryWorkItems(object[] inValues, AsyncCallback callback, object asyncState)
		{
			string groupName = (string)inValues[0];
			string tenantTier = (string)inValues[1];
			string workItemType = (string)inValues[2];
			WorkItemStatus status = (WorkItemStatus)inValues[3];
			int pageSize = (int)inValues[4];
			byte[] bookmark = (byte[])inValues[5];
			return this.BeginQueryWorkItems(groupName, tenantTier, workItemType, status, pageSize, bookmark, callback, asyncState);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0000D948 File Offset: 0x0000BB48
		private object[] OnEndQueryWorkItems(IAsyncResult result)
		{
			WorkItemQueryResult workItemQueryResult = this.EndQueryWorkItems(result);
			return new object[]
			{
				workItemQueryResult
			};
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0000D96C File Offset: 0x0000BB6C
		private void OnQueryWorkItemsCompleted(object state)
		{
			if (this.QueryWorkItemsCompleted != null)
			{
				ClientBase<IUpgradeHandler>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<IUpgradeHandler>.InvokeAsyncCompletedEventArgs)state;
				this.QueryWorkItemsCompleted(this, new QueryWorkItemsCompletedEventArgs(invokeAsyncCompletedEventArgs.Results, invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0000D9B1 File Offset: 0x0000BBB1
		public void QueryWorkItemsAsync(string groupName, string tenantTier, string workItemType, WorkItemStatus status, int pageSize, byte[] bookmark)
		{
			this.QueryWorkItemsAsync(groupName, tenantTier, workItemType, status, pageSize, bookmark, null);
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0000D9C4 File Offset: 0x0000BBC4
		public void QueryWorkItemsAsync(string groupName, string tenantTier, string workItemType, WorkItemStatus status, int pageSize, byte[] bookmark, object userState)
		{
			if (this.onBeginQueryWorkItemsDelegate == null)
			{
				this.onBeginQueryWorkItemsDelegate = new ClientBase<IUpgradeHandler>.BeginOperationDelegate(this.OnBeginQueryWorkItems);
			}
			if (this.onEndQueryWorkItemsDelegate == null)
			{
				this.onEndQueryWorkItemsDelegate = new ClientBase<IUpgradeHandler>.EndOperationDelegate(this.OnEndQueryWorkItems);
			}
			if (this.onQueryWorkItemsCompletedDelegate == null)
			{
				this.onQueryWorkItemsCompletedDelegate = new SendOrPostCallback(this.OnQueryWorkItemsCompleted);
			}
			base.InvokeAsync(this.onBeginQueryWorkItemsDelegate, new object[]
			{
				groupName,
				tenantTier,
				workItemType,
				status,
				pageSize,
				bookmark
			}, this.onEndQueryWorkItemsDelegate, this.onQueryWorkItemsCompletedDelegate, userState);
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0000DA66 File Offset: 0x0000BC66
		public WorkItemInfo[] QueryTenantWorkItems(Guid tenantId)
		{
			return base.Channel.QueryTenantWorkItems(tenantId);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0000DA74 File Offset: 0x0000BC74
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginQueryTenantWorkItems(Guid tenantId, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginQueryTenantWorkItems(tenantId, callback, asyncState);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0000DA84 File Offset: 0x0000BC84
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public WorkItemInfo[] EndQueryTenantWorkItems(IAsyncResult result)
		{
			return base.Channel.EndQueryTenantWorkItems(result);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0000DA94 File Offset: 0x0000BC94
		private IAsyncResult OnBeginQueryTenantWorkItems(object[] inValues, AsyncCallback callback, object asyncState)
		{
			Guid tenantId = (Guid)inValues[0];
			return this.BeginQueryTenantWorkItems(tenantId, callback, asyncState);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0000DAB4 File Offset: 0x0000BCB4
		private object[] OnEndQueryTenantWorkItems(IAsyncResult result)
		{
			WorkItemInfo[] array = this.EndQueryTenantWorkItems(result);
			return new object[]
			{
				array
			};
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0000DAD8 File Offset: 0x0000BCD8
		private void OnQueryTenantWorkItemsCompleted(object state)
		{
			if (this.QueryTenantWorkItemsCompleted != null)
			{
				ClientBase<IUpgradeHandler>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<IUpgradeHandler>.InvokeAsyncCompletedEventArgs)state;
				this.QueryTenantWorkItemsCompleted(this, new QueryTenantWorkItemsCompletedEventArgs(invokeAsyncCompletedEventArgs.Results, invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0000DB1D File Offset: 0x0000BD1D
		public void QueryTenantWorkItemsAsync(Guid tenantId)
		{
			this.QueryTenantWorkItemsAsync(tenantId, null);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0000DB28 File Offset: 0x0000BD28
		public void QueryTenantWorkItemsAsync(Guid tenantId, object userState)
		{
			if (this.onBeginQueryTenantWorkItemsDelegate == null)
			{
				this.onBeginQueryTenantWorkItemsDelegate = new ClientBase<IUpgradeHandler>.BeginOperationDelegate(this.OnBeginQueryTenantWorkItems);
			}
			if (this.onEndQueryTenantWorkItemsDelegate == null)
			{
				this.onEndQueryTenantWorkItemsDelegate = new ClientBase<IUpgradeHandler>.EndOperationDelegate(this.OnEndQueryTenantWorkItems);
			}
			if (this.onQueryTenantWorkItemsCompletedDelegate == null)
			{
				this.onQueryTenantWorkItemsCompletedDelegate = new SendOrPostCallback(this.OnQueryTenantWorkItemsCompleted);
			}
			base.InvokeAsync(this.onBeginQueryTenantWorkItemsDelegate, new object[]
			{
				tenantId
			}, this.onEndQueryTenantWorkItemsDelegate, this.onQueryTenantWorkItemsCompletedDelegate, userState);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0000DBAD File Offset: 0x0000BDAD
		public void UpdateWorkItem(string workItemId, WorkItemStatusInfo status)
		{
			base.Channel.UpdateWorkItem(workItemId, status);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0000DBBC File Offset: 0x0000BDBC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginUpdateWorkItem(string workItemId, WorkItemStatusInfo status, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginUpdateWorkItem(workItemId, status, callback, asyncState);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0000DBCE File Offset: 0x0000BDCE
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void EndUpdateWorkItem(IAsyncResult result)
		{
			base.Channel.EndUpdateWorkItem(result);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0000DBDC File Offset: 0x0000BDDC
		private IAsyncResult OnBeginUpdateWorkItem(object[] inValues, AsyncCallback callback, object asyncState)
		{
			string workItemId = (string)inValues[0];
			WorkItemStatusInfo status = (WorkItemStatusInfo)inValues[1];
			return this.BeginUpdateWorkItem(workItemId, status, callback, asyncState);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0000DC05 File Offset: 0x0000BE05
		private object[] OnEndUpdateWorkItem(IAsyncResult result)
		{
			this.EndUpdateWorkItem(result);
			return null;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0000DC10 File Offset: 0x0000BE10
		private void OnUpdateWorkItemCompleted(object state)
		{
			if (this.UpdateWorkItemCompleted != null)
			{
				ClientBase<IUpgradeHandler>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<IUpgradeHandler>.InvokeAsyncCompletedEventArgs)state;
				this.UpdateWorkItemCompleted(this, new AsyncCompletedEventArgs(invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0000DC4F File Offset: 0x0000BE4F
		public void UpdateWorkItemAsync(string workItemId, WorkItemStatusInfo status)
		{
			this.UpdateWorkItemAsync(workItemId, status, null);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0000DC5C File Offset: 0x0000BE5C
		public void UpdateWorkItemAsync(string workItemId, WorkItemStatusInfo status, object userState)
		{
			if (this.onBeginUpdateWorkItemDelegate == null)
			{
				this.onBeginUpdateWorkItemDelegate = new ClientBase<IUpgradeHandler>.BeginOperationDelegate(this.OnBeginUpdateWorkItem);
			}
			if (this.onEndUpdateWorkItemDelegate == null)
			{
				this.onEndUpdateWorkItemDelegate = new ClientBase<IUpgradeHandler>.EndOperationDelegate(this.OnEndUpdateWorkItem);
			}
			if (this.onUpdateWorkItemCompletedDelegate == null)
			{
				this.onUpdateWorkItemCompletedDelegate = new SendOrPostCallback(this.OnUpdateWorkItemCompleted);
			}
			base.InvokeAsync(this.onBeginUpdateWorkItemDelegate, new object[]
			{
				workItemId,
				status
			}, this.onEndUpdateWorkItemDelegate, this.onUpdateWorkItemCompletedDelegate, userState);
		}

		// Token: 0x0400032D RID: 813
		private ClientBase<IUpgradeHandler>.BeginOperationDelegate onBeginQueryWorkItemsDelegate;

		// Token: 0x0400032E RID: 814
		private ClientBase<IUpgradeHandler>.EndOperationDelegate onEndQueryWorkItemsDelegate;

		// Token: 0x0400032F RID: 815
		private SendOrPostCallback onQueryWorkItemsCompletedDelegate;

		// Token: 0x04000330 RID: 816
		private ClientBase<IUpgradeHandler>.BeginOperationDelegate onBeginQueryTenantWorkItemsDelegate;

		// Token: 0x04000331 RID: 817
		private ClientBase<IUpgradeHandler>.EndOperationDelegate onEndQueryTenantWorkItemsDelegate;

		// Token: 0x04000332 RID: 818
		private SendOrPostCallback onQueryTenantWorkItemsCompletedDelegate;

		// Token: 0x04000333 RID: 819
		private ClientBase<IUpgradeHandler>.BeginOperationDelegate onBeginUpdateWorkItemDelegate;

		// Token: 0x04000334 RID: 820
		private ClientBase<IUpgradeHandler>.EndOperationDelegate onEndUpdateWorkItemDelegate;

		// Token: 0x04000335 RID: 821
		private SendOrPostCallback onUpdateWorkItemCompletedDelegate;
	}
}
