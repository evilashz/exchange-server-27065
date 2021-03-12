using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000D8 RID: 216
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[DebuggerStepThrough]
	public class MonitorableClient : ClientBase<IMonitorable>, IMonitorable
	{
		// Token: 0x060006D1 RID: 1745 RVA: 0x0000EE54 File Offset: 0x0000D054
		public MonitorableClient()
		{
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0000EE5C File Offset: 0x0000D05C
		public MonitorableClient(string endpointConfigurationName) : base(endpointConfigurationName)
		{
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0000EE65 File Offset: 0x0000D065
		public MonitorableClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0000EE6F File Offset: 0x0000D06F
		public MonitorableClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0000EE79 File Offset: 0x0000D079
		public MonitorableClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060006D6 RID: 1750 RVA: 0x0000EE84 File Offset: 0x0000D084
		// (remove) Token: 0x060006D7 RID: 1751 RVA: 0x0000EEBC File Offset: 0x0000D0BC
		public event EventHandler<PingCompletedEventArgs> PingCompleted;

		// Token: 0x060006D8 RID: 1752 RVA: 0x0000EEF1 File Offset: 0x0000D0F1
		public string Ping()
		{
			return base.Channel.Ping();
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0000EEFE File Offset: 0x0000D0FE
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginPing(AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginPing(callback, asyncState);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0000EF0D File Offset: 0x0000D10D
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string EndPing(IAsyncResult result)
		{
			return base.Channel.EndPing(result);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0000EF1B File Offset: 0x0000D11B
		private IAsyncResult OnBeginPing(object[] inValues, AsyncCallback callback, object asyncState)
		{
			return this.BeginPing(callback, asyncState);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0000EF28 File Offset: 0x0000D128
		private object[] OnEndPing(IAsyncResult result)
		{
			string text = this.EndPing(result);
			return new object[]
			{
				text
			};
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0000EF4C File Offset: 0x0000D14C
		private void OnPingCompleted(object state)
		{
			if (this.PingCompleted != null)
			{
				ClientBase<IMonitorable>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<IMonitorable>.InvokeAsyncCompletedEventArgs)state;
				this.PingCompleted(this, new PingCompletedEventArgs(invokeAsyncCompletedEventArgs.Results, invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0000EF91 File Offset: 0x0000D191
		public void PingAsync()
		{
			this.PingAsync(null);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0000EF9C File Offset: 0x0000D19C
		public void PingAsync(object userState)
		{
			if (this.onBeginPingDelegate == null)
			{
				this.onBeginPingDelegate = new ClientBase<IMonitorable>.BeginOperationDelegate(this.OnBeginPing);
			}
			if (this.onEndPingDelegate == null)
			{
				this.onEndPingDelegate = new ClientBase<IMonitorable>.EndOperationDelegate(this.OnEndPing);
			}
			if (this.onPingCompletedDelegate == null)
			{
				this.onPingCompletedDelegate = new SendOrPostCallback(this.OnPingCompleted);
			}
			base.InvokeAsync(this.onBeginPingDelegate, null, this.onEndPingDelegate, this.onPingCompletedDelegate, userState);
		}

		// Token: 0x04000367 RID: 871
		private ClientBase<IMonitorable>.BeginOperationDelegate onBeginPingDelegate;

		// Token: 0x04000368 RID: 872
		private ClientBase<IMonitorable>.EndOperationDelegate onEndPingDelegate;

		// Token: 0x04000369 RID: 873
		private SendOrPostCallback onPingCompletedDelegate;
	}
}
