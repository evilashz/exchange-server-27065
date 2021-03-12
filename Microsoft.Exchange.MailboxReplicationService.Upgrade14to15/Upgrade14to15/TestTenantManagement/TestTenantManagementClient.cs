using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15.TestTenantManagement
{
	// Token: 0x020000B8 RID: 184
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class TestTenantManagementClient : ClientBase<ITestTenantManagement>, ITestTenantManagement
	{
		// Token: 0x0600053B RID: 1339 RVA: 0x000090EE File Offset: 0x000072EE
		public TestTenantManagementClient()
		{
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x000090F6 File Offset: 0x000072F6
		public TestTenantManagementClient(string endpointConfigurationName) : base(endpointConfigurationName)
		{
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x000090FF File Offset: 0x000072FF
		public TestTenantManagementClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00009109 File Offset: 0x00007309
		public TestTenantManagementClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00009113 File Offset: 0x00007313
		public TestTenantManagementClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000540 RID: 1344 RVA: 0x00009120 File Offset: 0x00007320
		// (remove) Token: 0x06000541 RID: 1345 RVA: 0x00009158 File Offset: 0x00007358
		public event EventHandler<QueryTenantsToPopulateCompletedEventArgs> QueryTenantsToPopulateCompleted;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000542 RID: 1346 RVA: 0x00009190 File Offset: 0x00007390
		// (remove) Token: 0x06000543 RID: 1347 RVA: 0x000091C8 File Offset: 0x000073C8
		public event EventHandler<QueryTenantsToValidateCompletedEventArgs> QueryTenantsToValidateCompleted;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000544 RID: 1348 RVA: 0x00009200 File Offset: 0x00007400
		// (remove) Token: 0x06000545 RID: 1349 RVA: 0x00009238 File Offset: 0x00007438
		public event EventHandler<QueryTenantsToValidateByScenarioCompletedEventArgs> QueryTenantsToValidateByScenarioCompleted;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000546 RID: 1350 RVA: 0x00009270 File Offset: 0x00007470
		// (remove) Token: 0x06000547 RID: 1351 RVA: 0x000092A8 File Offset: 0x000074A8
		public event EventHandler<AsyncCompletedEventArgs> UpdateTenantPopulationStatusCompleted;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000548 RID: 1352 RVA: 0x000092E0 File Offset: 0x000074E0
		// (remove) Token: 0x06000549 RID: 1353 RVA: 0x00009318 File Offset: 0x00007518
		public event EventHandler<AsyncCompletedEventArgs> UpdateTenantPopulationStatusWithScenarioCompleted;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600054A RID: 1354 RVA: 0x00009350 File Offset: 0x00007550
		// (remove) Token: 0x0600054B RID: 1355 RVA: 0x00009388 File Offset: 0x00007588
		public event EventHandler<AsyncCompletedEventArgs> UpdateTenantValidationStatusCompleted;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600054C RID: 1356 RVA: 0x000093C0 File Offset: 0x000075C0
		// (remove) Token: 0x0600054D RID: 1357 RVA: 0x000093F8 File Offset: 0x000075F8
		public event EventHandler<AsyncCompletedEventArgs> UpdateTenantValidationStatusWithReasonCompleted;

		// Token: 0x0600054E RID: 1358 RVA: 0x0000942D File Offset: 0x0000762D
		public Tenant[] QueryTenantsToPopulate(PopulationStatus status)
		{
			return base.Channel.QueryTenantsToPopulate(status);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0000943B File Offset: 0x0000763B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginQueryTenantsToPopulate(PopulationStatus status, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginQueryTenantsToPopulate(status, callback, asyncState);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0000944B File Offset: 0x0000764B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Tenant[] EndQueryTenantsToPopulate(IAsyncResult result)
		{
			return base.Channel.EndQueryTenantsToPopulate(result);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0000945C File Offset: 0x0000765C
		private IAsyncResult OnBeginQueryTenantsToPopulate(object[] inValues, AsyncCallback callback, object asyncState)
		{
			PopulationStatus status = (PopulationStatus)inValues[0];
			return this.BeginQueryTenantsToPopulate(status, callback, asyncState);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0000947C File Offset: 0x0000767C
		private object[] OnEndQueryTenantsToPopulate(IAsyncResult result)
		{
			Tenant[] array = this.EndQueryTenantsToPopulate(result);
			return new object[]
			{
				array
			};
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x000094A0 File Offset: 0x000076A0
		private void OnQueryTenantsToPopulateCompleted(object state)
		{
			if (this.QueryTenantsToPopulateCompleted != null)
			{
				ClientBase<ITestTenantManagement>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<ITestTenantManagement>.InvokeAsyncCompletedEventArgs)state;
				this.QueryTenantsToPopulateCompleted(this, new QueryTenantsToPopulateCompletedEventArgs(invokeAsyncCompletedEventArgs.Results, invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x000094E5 File Offset: 0x000076E5
		public void QueryTenantsToPopulateAsync(PopulationStatus status)
		{
			this.QueryTenantsToPopulateAsync(status, null);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x000094F0 File Offset: 0x000076F0
		public void QueryTenantsToPopulateAsync(PopulationStatus status, object userState)
		{
			if (this.onBeginQueryTenantsToPopulateDelegate == null)
			{
				this.onBeginQueryTenantsToPopulateDelegate = new ClientBase<ITestTenantManagement>.BeginOperationDelegate(this.OnBeginQueryTenantsToPopulate);
			}
			if (this.onEndQueryTenantsToPopulateDelegate == null)
			{
				this.onEndQueryTenantsToPopulateDelegate = new ClientBase<ITestTenantManagement>.EndOperationDelegate(this.OnEndQueryTenantsToPopulate);
			}
			if (this.onQueryTenantsToPopulateCompletedDelegate == null)
			{
				this.onQueryTenantsToPopulateCompletedDelegate = new SendOrPostCallback(this.OnQueryTenantsToPopulateCompleted);
			}
			base.InvokeAsync(this.onBeginQueryTenantsToPopulateDelegate, new object[]
			{
				status
			}, this.onEndQueryTenantsToPopulateDelegate, this.onQueryTenantsToPopulateCompletedDelegate, userState);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00009575 File Offset: 0x00007775
		public Tenant[] QueryTenantsToValidate(ValidationStatus status)
		{
			return base.Channel.QueryTenantsToValidate(status);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00009583 File Offset: 0x00007783
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginQueryTenantsToValidate(ValidationStatus status, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginQueryTenantsToValidate(status, callback, asyncState);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00009593 File Offset: 0x00007793
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Tenant[] EndQueryTenantsToValidate(IAsyncResult result)
		{
			return base.Channel.EndQueryTenantsToValidate(result);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x000095A4 File Offset: 0x000077A4
		private IAsyncResult OnBeginQueryTenantsToValidate(object[] inValues, AsyncCallback callback, object asyncState)
		{
			ValidationStatus status = (ValidationStatus)inValues[0];
			return this.BeginQueryTenantsToValidate(status, callback, asyncState);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x000095C4 File Offset: 0x000077C4
		private object[] OnEndQueryTenantsToValidate(IAsyncResult result)
		{
			Tenant[] array = this.EndQueryTenantsToValidate(result);
			return new object[]
			{
				array
			};
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x000095E8 File Offset: 0x000077E8
		private void OnQueryTenantsToValidateCompleted(object state)
		{
			if (this.QueryTenantsToValidateCompleted != null)
			{
				ClientBase<ITestTenantManagement>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<ITestTenantManagement>.InvokeAsyncCompletedEventArgs)state;
				this.QueryTenantsToValidateCompleted(this, new QueryTenantsToValidateCompletedEventArgs(invokeAsyncCompletedEventArgs.Results, invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0000962D File Offset: 0x0000782D
		public void QueryTenantsToValidateAsync(ValidationStatus status)
		{
			this.QueryTenantsToValidateAsync(status, null);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00009638 File Offset: 0x00007838
		public void QueryTenantsToValidateAsync(ValidationStatus status, object userState)
		{
			if (this.onBeginQueryTenantsToValidateDelegate == null)
			{
				this.onBeginQueryTenantsToValidateDelegate = new ClientBase<ITestTenantManagement>.BeginOperationDelegate(this.OnBeginQueryTenantsToValidate);
			}
			if (this.onEndQueryTenantsToValidateDelegate == null)
			{
				this.onEndQueryTenantsToValidateDelegate = new ClientBase<ITestTenantManagement>.EndOperationDelegate(this.OnEndQueryTenantsToValidate);
			}
			if (this.onQueryTenantsToValidateCompletedDelegate == null)
			{
				this.onQueryTenantsToValidateCompletedDelegate = new SendOrPostCallback(this.OnQueryTenantsToValidateCompleted);
			}
			base.InvokeAsync(this.onBeginQueryTenantsToValidateDelegate, new object[]
			{
				status
			}, this.onEndQueryTenantsToValidateDelegate, this.onQueryTenantsToValidateCompletedDelegate, userState);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x000096BD File Offset: 0x000078BD
		public Tenant[] QueryTenantsToValidateByScenario(ValidationStatus status, string scenario)
		{
			return base.Channel.QueryTenantsToValidateByScenario(status, scenario);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x000096CC File Offset: 0x000078CC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginQueryTenantsToValidateByScenario(ValidationStatus status, string scenario, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginQueryTenantsToValidateByScenario(status, scenario, callback, asyncState);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x000096DE File Offset: 0x000078DE
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Tenant[] EndQueryTenantsToValidateByScenario(IAsyncResult result)
		{
			return base.Channel.EndQueryTenantsToValidateByScenario(result);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x000096EC File Offset: 0x000078EC
		private IAsyncResult OnBeginQueryTenantsToValidateByScenario(object[] inValues, AsyncCallback callback, object asyncState)
		{
			ValidationStatus status = (ValidationStatus)inValues[0];
			string scenario = (string)inValues[1];
			return this.BeginQueryTenantsToValidateByScenario(status, scenario, callback, asyncState);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00009718 File Offset: 0x00007918
		private object[] OnEndQueryTenantsToValidateByScenario(IAsyncResult result)
		{
			Tenant[] array = this.EndQueryTenantsToValidateByScenario(result);
			return new object[]
			{
				array
			};
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0000973C File Offset: 0x0000793C
		private void OnQueryTenantsToValidateByScenarioCompleted(object state)
		{
			if (this.QueryTenantsToValidateByScenarioCompleted != null)
			{
				ClientBase<ITestTenantManagement>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<ITestTenantManagement>.InvokeAsyncCompletedEventArgs)state;
				this.QueryTenantsToValidateByScenarioCompleted(this, new QueryTenantsToValidateByScenarioCompletedEventArgs(invokeAsyncCompletedEventArgs.Results, invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00009781 File Offset: 0x00007981
		public void QueryTenantsToValidateByScenarioAsync(ValidationStatus status, string scenario)
		{
			this.QueryTenantsToValidateByScenarioAsync(status, scenario, null);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0000978C File Offset: 0x0000798C
		public void QueryTenantsToValidateByScenarioAsync(ValidationStatus status, string scenario, object userState)
		{
			if (this.onBeginQueryTenantsToValidateByScenarioDelegate == null)
			{
				this.onBeginQueryTenantsToValidateByScenarioDelegate = new ClientBase<ITestTenantManagement>.BeginOperationDelegate(this.OnBeginQueryTenantsToValidateByScenario);
			}
			if (this.onEndQueryTenantsToValidateByScenarioDelegate == null)
			{
				this.onEndQueryTenantsToValidateByScenarioDelegate = new ClientBase<ITestTenantManagement>.EndOperationDelegate(this.OnEndQueryTenantsToValidateByScenario);
			}
			if (this.onQueryTenantsToValidateByScenarioCompletedDelegate == null)
			{
				this.onQueryTenantsToValidateByScenarioCompletedDelegate = new SendOrPostCallback(this.OnQueryTenantsToValidateByScenarioCompleted);
			}
			base.InvokeAsync(this.onBeginQueryTenantsToValidateByScenarioDelegate, new object[]
			{
				status,
				scenario
			}, this.onEndQueryTenantsToValidateByScenarioDelegate, this.onQueryTenantsToValidateByScenarioCompletedDelegate, userState);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00009815 File Offset: 0x00007A15
		public void UpdateTenantPopulationStatus(Guid tenantId, PopulationStatus status)
		{
			base.Channel.UpdateTenantPopulationStatus(tenantId, status);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00009824 File Offset: 0x00007A24
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginUpdateTenantPopulationStatus(Guid tenantId, PopulationStatus status, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginUpdateTenantPopulationStatus(tenantId, status, callback, asyncState);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00009836 File Offset: 0x00007A36
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void EndUpdateTenantPopulationStatus(IAsyncResult result)
		{
			base.Channel.EndUpdateTenantPopulationStatus(result);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00009844 File Offset: 0x00007A44
		private IAsyncResult OnBeginUpdateTenantPopulationStatus(object[] inValues, AsyncCallback callback, object asyncState)
		{
			Guid tenantId = (Guid)inValues[0];
			PopulationStatus status = (PopulationStatus)inValues[1];
			return this.BeginUpdateTenantPopulationStatus(tenantId, status, callback, asyncState);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0000986D File Offset: 0x00007A6D
		private object[] OnEndUpdateTenantPopulationStatus(IAsyncResult result)
		{
			this.EndUpdateTenantPopulationStatus(result);
			return null;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00009878 File Offset: 0x00007A78
		private void OnUpdateTenantPopulationStatusCompleted(object state)
		{
			if (this.UpdateTenantPopulationStatusCompleted != null)
			{
				ClientBase<ITestTenantManagement>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<ITestTenantManagement>.InvokeAsyncCompletedEventArgs)state;
				this.UpdateTenantPopulationStatusCompleted(this, new AsyncCompletedEventArgs(invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x000098B7 File Offset: 0x00007AB7
		public void UpdateTenantPopulationStatusAsync(Guid tenantId, PopulationStatus status)
		{
			this.UpdateTenantPopulationStatusAsync(tenantId, status, null);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x000098C4 File Offset: 0x00007AC4
		public void UpdateTenantPopulationStatusAsync(Guid tenantId, PopulationStatus status, object userState)
		{
			if (this.onBeginUpdateTenantPopulationStatusDelegate == null)
			{
				this.onBeginUpdateTenantPopulationStatusDelegate = new ClientBase<ITestTenantManagement>.BeginOperationDelegate(this.OnBeginUpdateTenantPopulationStatus);
			}
			if (this.onEndUpdateTenantPopulationStatusDelegate == null)
			{
				this.onEndUpdateTenantPopulationStatusDelegate = new ClientBase<ITestTenantManagement>.EndOperationDelegate(this.OnEndUpdateTenantPopulationStatus);
			}
			if (this.onUpdateTenantPopulationStatusCompletedDelegate == null)
			{
				this.onUpdateTenantPopulationStatusCompletedDelegate = new SendOrPostCallback(this.OnUpdateTenantPopulationStatusCompleted);
			}
			base.InvokeAsync(this.onBeginUpdateTenantPopulationStatusDelegate, new object[]
			{
				tenantId,
				status
			}, this.onEndUpdateTenantPopulationStatusDelegate, this.onUpdateTenantPopulationStatusCompletedDelegate, userState);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00009952 File Offset: 0x00007B52
		public void UpdateTenantPopulationStatusWithScenario(Guid tenantId, PopulationStatus status, string scenario, string comment)
		{
			base.Channel.UpdateTenantPopulationStatusWithScenario(tenantId, status, scenario, comment);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00009964 File Offset: 0x00007B64
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginUpdateTenantPopulationStatusWithScenario(Guid tenantId, PopulationStatus status, string scenario, string comment, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginUpdateTenantPopulationStatusWithScenario(tenantId, status, scenario, comment, callback, asyncState);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0000997A File Offset: 0x00007B7A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void EndUpdateTenantPopulationStatusWithScenario(IAsyncResult result)
		{
			base.Channel.EndUpdateTenantPopulationStatusWithScenario(result);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00009988 File Offset: 0x00007B88
		private IAsyncResult OnBeginUpdateTenantPopulationStatusWithScenario(object[] inValues, AsyncCallback callback, object asyncState)
		{
			Guid tenantId = (Guid)inValues[0];
			PopulationStatus status = (PopulationStatus)inValues[1];
			string scenario = (string)inValues[2];
			string comment = (string)inValues[3];
			return this.BeginUpdateTenantPopulationStatusWithScenario(tenantId, status, scenario, comment, callback, asyncState);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x000099C5 File Offset: 0x00007BC5
		private object[] OnEndUpdateTenantPopulationStatusWithScenario(IAsyncResult result)
		{
			this.EndUpdateTenantPopulationStatusWithScenario(result);
			return null;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x000099D0 File Offset: 0x00007BD0
		private void OnUpdateTenantPopulationStatusWithScenarioCompleted(object state)
		{
			if (this.UpdateTenantPopulationStatusWithScenarioCompleted != null)
			{
				ClientBase<ITestTenantManagement>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<ITestTenantManagement>.InvokeAsyncCompletedEventArgs)state;
				this.UpdateTenantPopulationStatusWithScenarioCompleted(this, new AsyncCompletedEventArgs(invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00009A0F File Offset: 0x00007C0F
		public void UpdateTenantPopulationStatusWithScenarioAsync(Guid tenantId, PopulationStatus status, string scenario, string comment)
		{
			this.UpdateTenantPopulationStatusWithScenarioAsync(tenantId, status, scenario, comment, null);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00009A20 File Offset: 0x00007C20
		public void UpdateTenantPopulationStatusWithScenarioAsync(Guid tenantId, PopulationStatus status, string scenario, string comment, object userState)
		{
			if (this.onBeginUpdateTenantPopulationStatusWithScenarioDelegate == null)
			{
				this.onBeginUpdateTenantPopulationStatusWithScenarioDelegate = new ClientBase<ITestTenantManagement>.BeginOperationDelegate(this.OnBeginUpdateTenantPopulationStatusWithScenario);
			}
			if (this.onEndUpdateTenantPopulationStatusWithScenarioDelegate == null)
			{
				this.onEndUpdateTenantPopulationStatusWithScenarioDelegate = new ClientBase<ITestTenantManagement>.EndOperationDelegate(this.OnEndUpdateTenantPopulationStatusWithScenario);
			}
			if (this.onUpdateTenantPopulationStatusWithScenarioCompletedDelegate == null)
			{
				this.onUpdateTenantPopulationStatusWithScenarioCompletedDelegate = new SendOrPostCallback(this.OnUpdateTenantPopulationStatusWithScenarioCompleted);
			}
			base.InvokeAsync(this.onBeginUpdateTenantPopulationStatusWithScenarioDelegate, new object[]
			{
				tenantId,
				status,
				scenario,
				comment
			}, this.onEndUpdateTenantPopulationStatusWithScenarioDelegate, this.onUpdateTenantPopulationStatusWithScenarioCompletedDelegate, userState);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00009AB8 File Offset: 0x00007CB8
		public void UpdateTenantValidationStatus(Guid tenantId, ValidationStatus status, int? office15BugId)
		{
			base.Channel.UpdateTenantValidationStatus(tenantId, status, office15BugId);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00009AC8 File Offset: 0x00007CC8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginUpdateTenantValidationStatus(Guid tenantId, ValidationStatus status, int? office15BugId, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginUpdateTenantValidationStatus(tenantId, status, office15BugId, callback, asyncState);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00009ADC File Offset: 0x00007CDC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void EndUpdateTenantValidationStatus(IAsyncResult result)
		{
			base.Channel.EndUpdateTenantValidationStatus(result);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00009AEC File Offset: 0x00007CEC
		private IAsyncResult OnBeginUpdateTenantValidationStatus(object[] inValues, AsyncCallback callback, object asyncState)
		{
			Guid tenantId = (Guid)inValues[0];
			ValidationStatus status = (ValidationStatus)inValues[1];
			int? office15BugId = (int?)inValues[2];
			return this.BeginUpdateTenantValidationStatus(tenantId, status, office15BugId, callback, asyncState);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00009B1F File Offset: 0x00007D1F
		private object[] OnEndUpdateTenantValidationStatus(IAsyncResult result)
		{
			this.EndUpdateTenantValidationStatus(result);
			return null;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00009B2C File Offset: 0x00007D2C
		private void OnUpdateTenantValidationStatusCompleted(object state)
		{
			if (this.UpdateTenantValidationStatusCompleted != null)
			{
				ClientBase<ITestTenantManagement>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<ITestTenantManagement>.InvokeAsyncCompletedEventArgs)state;
				this.UpdateTenantValidationStatusCompleted(this, new AsyncCompletedEventArgs(invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00009B6B File Offset: 0x00007D6B
		public void UpdateTenantValidationStatusAsync(Guid tenantId, ValidationStatus status, int? office15BugId)
		{
			this.UpdateTenantValidationStatusAsync(tenantId, status, office15BugId, null);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00009B78 File Offset: 0x00007D78
		public void UpdateTenantValidationStatusAsync(Guid tenantId, ValidationStatus status, int? office15BugId, object userState)
		{
			if (this.onBeginUpdateTenantValidationStatusDelegate == null)
			{
				this.onBeginUpdateTenantValidationStatusDelegate = new ClientBase<ITestTenantManagement>.BeginOperationDelegate(this.OnBeginUpdateTenantValidationStatus);
			}
			if (this.onEndUpdateTenantValidationStatusDelegate == null)
			{
				this.onEndUpdateTenantValidationStatusDelegate = new ClientBase<ITestTenantManagement>.EndOperationDelegate(this.OnEndUpdateTenantValidationStatus);
			}
			if (this.onUpdateTenantValidationStatusCompletedDelegate == null)
			{
				this.onUpdateTenantValidationStatusCompletedDelegate = new SendOrPostCallback(this.OnUpdateTenantValidationStatusCompleted);
			}
			base.InvokeAsync(this.onBeginUpdateTenantValidationStatusDelegate, new object[]
			{
				tenantId,
				status,
				office15BugId
			}, this.onEndUpdateTenantValidationStatusDelegate, this.onUpdateTenantValidationStatusCompletedDelegate, userState);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00009C10 File Offset: 0x00007E10
		public void UpdateTenantValidationStatusWithReason(Guid tenantId, ValidationStatus status, string failureReason)
		{
			base.Channel.UpdateTenantValidationStatusWithReason(tenantId, status, failureReason);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00009C20 File Offset: 0x00007E20
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginUpdateTenantValidationStatusWithReason(Guid tenantId, ValidationStatus status, string failureReason, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginUpdateTenantValidationStatusWithReason(tenantId, status, failureReason, callback, asyncState);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00009C34 File Offset: 0x00007E34
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void EndUpdateTenantValidationStatusWithReason(IAsyncResult result)
		{
			base.Channel.EndUpdateTenantValidationStatusWithReason(result);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00009C44 File Offset: 0x00007E44
		private IAsyncResult OnBeginUpdateTenantValidationStatusWithReason(object[] inValues, AsyncCallback callback, object asyncState)
		{
			Guid tenantId = (Guid)inValues[0];
			ValidationStatus status = (ValidationStatus)inValues[1];
			string failureReason = (string)inValues[2];
			return this.BeginUpdateTenantValidationStatusWithReason(tenantId, status, failureReason, callback, asyncState);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00009C77 File Offset: 0x00007E77
		private object[] OnEndUpdateTenantValidationStatusWithReason(IAsyncResult result)
		{
			this.EndUpdateTenantValidationStatusWithReason(result);
			return null;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00009C84 File Offset: 0x00007E84
		private void OnUpdateTenantValidationStatusWithReasonCompleted(object state)
		{
			if (this.UpdateTenantValidationStatusWithReasonCompleted != null)
			{
				ClientBase<ITestTenantManagement>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<ITestTenantManagement>.InvokeAsyncCompletedEventArgs)state;
				this.UpdateTenantValidationStatusWithReasonCompleted(this, new AsyncCompletedEventArgs(invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00009CC3 File Offset: 0x00007EC3
		public void UpdateTenantValidationStatusWithReasonAsync(Guid tenantId, ValidationStatus status, string failureReason)
		{
			this.UpdateTenantValidationStatusWithReasonAsync(tenantId, status, failureReason, null);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00009CD0 File Offset: 0x00007ED0
		public void UpdateTenantValidationStatusWithReasonAsync(Guid tenantId, ValidationStatus status, string failureReason, object userState)
		{
			if (this.onBeginUpdateTenantValidationStatusWithReasonDelegate == null)
			{
				this.onBeginUpdateTenantValidationStatusWithReasonDelegate = new ClientBase<ITestTenantManagement>.BeginOperationDelegate(this.OnBeginUpdateTenantValidationStatusWithReason);
			}
			if (this.onEndUpdateTenantValidationStatusWithReasonDelegate == null)
			{
				this.onEndUpdateTenantValidationStatusWithReasonDelegate = new ClientBase<ITestTenantManagement>.EndOperationDelegate(this.OnEndUpdateTenantValidationStatusWithReason);
			}
			if (this.onUpdateTenantValidationStatusWithReasonCompletedDelegate == null)
			{
				this.onUpdateTenantValidationStatusWithReasonCompletedDelegate = new SendOrPostCallback(this.OnUpdateTenantValidationStatusWithReasonCompleted);
			}
			base.InvokeAsync(this.onBeginUpdateTenantValidationStatusWithReasonDelegate, new object[]
			{
				tenantId,
				status,
				failureReason
			}, this.onEndUpdateTenantValidationStatusWithReasonDelegate, this.onUpdateTenantValidationStatusWithReasonCompletedDelegate, userState);
		}

		// Token: 0x0400028E RID: 654
		private ClientBase<ITestTenantManagement>.BeginOperationDelegate onBeginQueryTenantsToPopulateDelegate;

		// Token: 0x0400028F RID: 655
		private ClientBase<ITestTenantManagement>.EndOperationDelegate onEndQueryTenantsToPopulateDelegate;

		// Token: 0x04000290 RID: 656
		private SendOrPostCallback onQueryTenantsToPopulateCompletedDelegate;

		// Token: 0x04000291 RID: 657
		private ClientBase<ITestTenantManagement>.BeginOperationDelegate onBeginQueryTenantsToValidateDelegate;

		// Token: 0x04000292 RID: 658
		private ClientBase<ITestTenantManagement>.EndOperationDelegate onEndQueryTenantsToValidateDelegate;

		// Token: 0x04000293 RID: 659
		private SendOrPostCallback onQueryTenantsToValidateCompletedDelegate;

		// Token: 0x04000294 RID: 660
		private ClientBase<ITestTenantManagement>.BeginOperationDelegate onBeginQueryTenantsToValidateByScenarioDelegate;

		// Token: 0x04000295 RID: 661
		private ClientBase<ITestTenantManagement>.EndOperationDelegate onEndQueryTenantsToValidateByScenarioDelegate;

		// Token: 0x04000296 RID: 662
		private SendOrPostCallback onQueryTenantsToValidateByScenarioCompletedDelegate;

		// Token: 0x04000297 RID: 663
		private ClientBase<ITestTenantManagement>.BeginOperationDelegate onBeginUpdateTenantPopulationStatusDelegate;

		// Token: 0x04000298 RID: 664
		private ClientBase<ITestTenantManagement>.EndOperationDelegate onEndUpdateTenantPopulationStatusDelegate;

		// Token: 0x04000299 RID: 665
		private SendOrPostCallback onUpdateTenantPopulationStatusCompletedDelegate;

		// Token: 0x0400029A RID: 666
		private ClientBase<ITestTenantManagement>.BeginOperationDelegate onBeginUpdateTenantPopulationStatusWithScenarioDelegate;

		// Token: 0x0400029B RID: 667
		private ClientBase<ITestTenantManagement>.EndOperationDelegate onEndUpdateTenantPopulationStatusWithScenarioDelegate;

		// Token: 0x0400029C RID: 668
		private SendOrPostCallback onUpdateTenantPopulationStatusWithScenarioCompletedDelegate;

		// Token: 0x0400029D RID: 669
		private ClientBase<ITestTenantManagement>.BeginOperationDelegate onBeginUpdateTenantValidationStatusDelegate;

		// Token: 0x0400029E RID: 670
		private ClientBase<ITestTenantManagement>.EndOperationDelegate onEndUpdateTenantValidationStatusDelegate;

		// Token: 0x0400029F RID: 671
		private SendOrPostCallback onUpdateTenantValidationStatusCompletedDelegate;

		// Token: 0x040002A0 RID: 672
		private ClientBase<ITestTenantManagement>.BeginOperationDelegate onBeginUpdateTenantValidationStatusWithReasonDelegate;

		// Token: 0x040002A1 RID: 673
		private ClientBase<ITestTenantManagement>.EndOperationDelegate onEndUpdateTenantValidationStatusWithReasonDelegate;

		// Token: 0x040002A2 RID: 674
		private SendOrPostCallback onUpdateTenantValidationStatusWithReasonCompletedDelegate;
	}
}
