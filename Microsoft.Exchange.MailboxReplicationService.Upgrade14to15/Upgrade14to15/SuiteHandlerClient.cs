using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200009E RID: 158
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[DebuggerStepThrough]
	public class SuiteHandlerClient : ClientBase<ISuiteHandler>, ISuiteHandler
	{
		// Token: 0x060003F0 RID: 1008 RVA: 0x00005CAD File Offset: 0x00003EAD
		public SuiteHandlerClient()
		{
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00005CB5 File Offset: 0x00003EB5
		public SuiteHandlerClient(string endpointConfigurationName) : base(endpointConfigurationName)
		{
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00005CBE File Offset: 0x00003EBE
		public SuiteHandlerClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00005CC8 File Offset: 0x00003EC8
		public SuiteHandlerClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00005CD2 File Offset: 0x00003ED2
		public SuiteHandlerClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060003F5 RID: 1013 RVA: 0x00005CDC File Offset: 0x00003EDC
		// (remove) Token: 0x060003F6 RID: 1014 RVA: 0x00005D14 File Offset: 0x00003F14
		public event EventHandler<AddPilotUsersCompletedEventArgs> AddPilotUsersCompleted;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060003F7 RID: 1015 RVA: 0x00005D4C File Offset: 0x00003F4C
		// (remove) Token: 0x060003F8 RID: 1016 RVA: 0x00005D84 File Offset: 0x00003F84
		public event EventHandler<GetPilotUsersCompletedEventArgs> GetPilotUsersCompleted;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060003F9 RID: 1017 RVA: 0x00005DBC File Offset: 0x00003FBC
		// (remove) Token: 0x060003FA RID: 1018 RVA: 0x00005DF4 File Offset: 0x00003FF4
		public event EventHandler<AsyncCompletedEventArgs> PostponeTenantUpgradeCompleted;

		// Token: 0x060003FB RID: 1019 RVA: 0x00005E29 File Offset: 0x00004029
		public int AddPilotUsers(Guid tenantId, UserId[] users)
		{
			return base.Channel.AddPilotUsers(tenantId, users);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00005E38 File Offset: 0x00004038
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginAddPilotUsers(Guid tenantId, UserId[] users, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginAddPilotUsers(tenantId, users, callback, asyncState);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00005E4A File Offset: 0x0000404A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int EndAddPilotUsers(IAsyncResult result)
		{
			return base.Channel.EndAddPilotUsers(result);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00005E58 File Offset: 0x00004058
		private IAsyncResult OnBeginAddPilotUsers(object[] inValues, AsyncCallback callback, object asyncState)
		{
			Guid tenantId = (Guid)inValues[0];
			UserId[] users = (UserId[])inValues[1];
			return this.BeginAddPilotUsers(tenantId, users, callback, asyncState);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00005E84 File Offset: 0x00004084
		private object[] OnEndAddPilotUsers(IAsyncResult result)
		{
			int num = this.EndAddPilotUsers(result);
			return new object[]
			{
				num
			};
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00005EAC File Offset: 0x000040AC
		private void OnAddPilotUsersCompleted(object state)
		{
			if (this.AddPilotUsersCompleted != null)
			{
				ClientBase<ISuiteHandler>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<ISuiteHandler>.InvokeAsyncCompletedEventArgs)state;
				this.AddPilotUsersCompleted(this, new AddPilotUsersCompletedEventArgs(invokeAsyncCompletedEventArgs.Results, invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00005EF1 File Offset: 0x000040F1
		public void AddPilotUsersAsync(Guid tenantId, UserId[] users)
		{
			this.AddPilotUsersAsync(tenantId, users, null);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00005EFC File Offset: 0x000040FC
		public void AddPilotUsersAsync(Guid tenantId, UserId[] users, object userState)
		{
			if (this.onBeginAddPilotUsersDelegate == null)
			{
				this.onBeginAddPilotUsersDelegate = new ClientBase<ISuiteHandler>.BeginOperationDelegate(this.OnBeginAddPilotUsers);
			}
			if (this.onEndAddPilotUsersDelegate == null)
			{
				this.onEndAddPilotUsersDelegate = new ClientBase<ISuiteHandler>.EndOperationDelegate(this.OnEndAddPilotUsers);
			}
			if (this.onAddPilotUsersCompletedDelegate == null)
			{
				this.onAddPilotUsersCompletedDelegate = new SendOrPostCallback(this.OnAddPilotUsersCompleted);
			}
			base.InvokeAsync(this.onBeginAddPilotUsersDelegate, new object[]
			{
				tenantId,
				users
			}, this.onEndAddPilotUsersDelegate, this.onAddPilotUsersCompletedDelegate, userState);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00005F85 File Offset: 0x00004185
		public UserWorkloadStatusInfo[] GetPilotUsers(Guid tenantId)
		{
			return base.Channel.GetPilotUsers(tenantId);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00005F93 File Offset: 0x00004193
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginGetPilotUsers(Guid tenantId, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginGetPilotUsers(tenantId, callback, asyncState);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00005FA3 File Offset: 0x000041A3
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public UserWorkloadStatusInfo[] EndGetPilotUsers(IAsyncResult result)
		{
			return base.Channel.EndGetPilotUsers(result);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00005FB4 File Offset: 0x000041B4
		private IAsyncResult OnBeginGetPilotUsers(object[] inValues, AsyncCallback callback, object asyncState)
		{
			Guid tenantId = (Guid)inValues[0];
			return this.BeginGetPilotUsers(tenantId, callback, asyncState);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00005FD4 File Offset: 0x000041D4
		private object[] OnEndGetPilotUsers(IAsyncResult result)
		{
			UserWorkloadStatusInfo[] array = this.EndGetPilotUsers(result);
			return new object[]
			{
				array
			};
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00005FF8 File Offset: 0x000041F8
		private void OnGetPilotUsersCompleted(object state)
		{
			if (this.GetPilotUsersCompleted != null)
			{
				ClientBase<ISuiteHandler>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<ISuiteHandler>.InvokeAsyncCompletedEventArgs)state;
				this.GetPilotUsersCompleted(this, new GetPilotUsersCompletedEventArgs(invokeAsyncCompletedEventArgs.Results, invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000603D File Offset: 0x0000423D
		public void GetPilotUsersAsync(Guid tenantId)
		{
			this.GetPilotUsersAsync(tenantId, null);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00006048 File Offset: 0x00004248
		public void GetPilotUsersAsync(Guid tenantId, object userState)
		{
			if (this.onBeginGetPilotUsersDelegate == null)
			{
				this.onBeginGetPilotUsersDelegate = new ClientBase<ISuiteHandler>.BeginOperationDelegate(this.OnBeginGetPilotUsers);
			}
			if (this.onEndGetPilotUsersDelegate == null)
			{
				this.onEndGetPilotUsersDelegate = new ClientBase<ISuiteHandler>.EndOperationDelegate(this.OnEndGetPilotUsers);
			}
			if (this.onGetPilotUsersCompletedDelegate == null)
			{
				this.onGetPilotUsersCompletedDelegate = new SendOrPostCallback(this.OnGetPilotUsersCompleted);
			}
			base.InvokeAsync(this.onBeginGetPilotUsersDelegate, new object[]
			{
				tenantId
			}, this.onEndGetPilotUsersDelegate, this.onGetPilotUsersCompletedDelegate, userState);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000060CD File Offset: 0x000042CD
		public void PostponeTenantUpgrade(Guid tenantId, string userUpn)
		{
			base.Channel.PostponeTenantUpgrade(tenantId, userUpn);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000060DC File Offset: 0x000042DC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginPostponeTenantUpgrade(Guid tenantId, string userUpn, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginPostponeTenantUpgrade(tenantId, userUpn, callback, asyncState);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x000060EE File Offset: 0x000042EE
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void EndPostponeTenantUpgrade(IAsyncResult result)
		{
			base.Channel.EndPostponeTenantUpgrade(result);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000060FC File Offset: 0x000042FC
		private IAsyncResult OnBeginPostponeTenantUpgrade(object[] inValues, AsyncCallback callback, object asyncState)
		{
			Guid tenantId = (Guid)inValues[0];
			string userUpn = (string)inValues[1];
			return this.BeginPostponeTenantUpgrade(tenantId, userUpn, callback, asyncState);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00006125 File Offset: 0x00004325
		private object[] OnEndPostponeTenantUpgrade(IAsyncResult result)
		{
			this.EndPostponeTenantUpgrade(result);
			return null;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00006130 File Offset: 0x00004330
		private void OnPostponeTenantUpgradeCompleted(object state)
		{
			if (this.PostponeTenantUpgradeCompleted != null)
			{
				ClientBase<ISuiteHandler>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<ISuiteHandler>.InvokeAsyncCompletedEventArgs)state;
				this.PostponeTenantUpgradeCompleted(this, new AsyncCompletedEventArgs(invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000616F File Offset: 0x0000436F
		public void PostponeTenantUpgradeAsync(Guid tenantId, string userUpn)
		{
			this.PostponeTenantUpgradeAsync(tenantId, userUpn, null);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000617C File Offset: 0x0000437C
		public void PostponeTenantUpgradeAsync(Guid tenantId, string userUpn, object userState)
		{
			if (this.onBeginPostponeTenantUpgradeDelegate == null)
			{
				this.onBeginPostponeTenantUpgradeDelegate = new ClientBase<ISuiteHandler>.BeginOperationDelegate(this.OnBeginPostponeTenantUpgrade);
			}
			if (this.onEndPostponeTenantUpgradeDelegate == null)
			{
				this.onEndPostponeTenantUpgradeDelegate = new ClientBase<ISuiteHandler>.EndOperationDelegate(this.OnEndPostponeTenantUpgrade);
			}
			if (this.onPostponeTenantUpgradeCompletedDelegate == null)
			{
				this.onPostponeTenantUpgradeCompletedDelegate = new SendOrPostCallback(this.OnPostponeTenantUpgradeCompleted);
			}
			base.InvokeAsync(this.onBeginPostponeTenantUpgradeDelegate, new object[]
			{
				tenantId,
				userUpn
			}, this.onEndPostponeTenantUpgradeDelegate, this.onPostponeTenantUpgradeCompletedDelegate, userState);
		}

		// Token: 0x040001C2 RID: 450
		private ClientBase<ISuiteHandler>.BeginOperationDelegate onBeginAddPilotUsersDelegate;

		// Token: 0x040001C3 RID: 451
		private ClientBase<ISuiteHandler>.EndOperationDelegate onEndAddPilotUsersDelegate;

		// Token: 0x040001C4 RID: 452
		private SendOrPostCallback onAddPilotUsersCompletedDelegate;

		// Token: 0x040001C5 RID: 453
		private ClientBase<ISuiteHandler>.BeginOperationDelegate onBeginGetPilotUsersDelegate;

		// Token: 0x040001C6 RID: 454
		private ClientBase<ISuiteHandler>.EndOperationDelegate onEndGetPilotUsersDelegate;

		// Token: 0x040001C7 RID: 455
		private SendOrPostCallback onGetPilotUsersCompletedDelegate;

		// Token: 0x040001C8 RID: 456
		private ClientBase<ISuiteHandler>.BeginOperationDelegate onBeginPostponeTenantUpgradeDelegate;

		// Token: 0x040001C9 RID: 457
		private ClientBase<ISuiteHandler>.EndOperationDelegate onEndPostponeTenantUpgradeDelegate;

		// Token: 0x040001CA RID: 458
		private SendOrPostCallback onPostponeTenantUpgradeCompletedDelegate;
	}
}
