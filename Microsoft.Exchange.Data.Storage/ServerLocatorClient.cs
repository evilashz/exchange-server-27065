using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using www.outlook.com.highavailability.ServerLocator.v1;

// Token: 0x02000D3F RID: 3391
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
[DebuggerStepThrough]
public class ServerLocatorClient : ClientBase<ServerLocator>, ServerLocator
{
	// Token: 0x06007571 RID: 30065 RVA: 0x0020848C File Offset: 0x0020668C
	public ServerLocatorClient()
	{
	}

	// Token: 0x06007572 RID: 30066 RVA: 0x00208494 File Offset: 0x00206694
	public ServerLocatorClient(string endpointConfigurationName) : base(endpointConfigurationName)
	{
	}

	// Token: 0x06007573 RID: 30067 RVA: 0x0020849D File Offset: 0x0020669D
	public ServerLocatorClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
	{
	}

	// Token: 0x06007574 RID: 30068 RVA: 0x002084A7 File Offset: 0x002066A7
	public ServerLocatorClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
	{
	}

	// Token: 0x06007575 RID: 30069 RVA: 0x002084B1 File Offset: 0x002066B1
	public ServerLocatorClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
	{
	}

	// Token: 0x1400000D RID: 13
	// (add) Token: 0x06007576 RID: 30070 RVA: 0x002084BC File Offset: 0x002066BC
	// (remove) Token: 0x06007577 RID: 30071 RVA: 0x002084F4 File Offset: 0x002066F4
	public event EventHandler<GetVersionCompletedEventArgs> GetVersionCompleted;

	// Token: 0x1400000E RID: 14
	// (add) Token: 0x06007578 RID: 30072 RVA: 0x0020852C File Offset: 0x0020672C
	// (remove) Token: 0x06007579 RID: 30073 RVA: 0x00208564 File Offset: 0x00206764
	public event EventHandler<GetServerForDatabaseCompletedEventArgs> GetServerForDatabaseCompleted;

	// Token: 0x1400000F RID: 15
	// (add) Token: 0x0600757A RID: 30074 RVA: 0x0020859C File Offset: 0x0020679C
	// (remove) Token: 0x0600757B RID: 30075 RVA: 0x002085D4 File Offset: 0x002067D4
	public event EventHandler<GetActiveCopiesForDatabaseAvailabilityGroupCompletedEventArgs> GetActiveCopiesForDatabaseAvailabilityGroupCompleted;

	// Token: 0x14000010 RID: 16
	// (add) Token: 0x0600757C RID: 30076 RVA: 0x0020860C File Offset: 0x0020680C
	// (remove) Token: 0x0600757D RID: 30077 RVA: 0x00208644 File Offset: 0x00206844
	public event EventHandler<GetActiveCopiesForDatabaseAvailabilityGroupExtendedCompletedEventArgs> GetActiveCopiesForDatabaseAvailabilityGroupExtendedCompleted;

	// Token: 0x0600757E RID: 30078 RVA: 0x00208679 File Offset: 0x00206879
	public ServiceVersion GetVersion()
	{
		return base.Channel.GetVersion();
	}

	// Token: 0x0600757F RID: 30079 RVA: 0x00208686 File Offset: 0x00206886
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public IAsyncResult BeginGetVersion(AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginGetVersion(callback, asyncState);
	}

	// Token: 0x06007580 RID: 30080 RVA: 0x00208695 File Offset: 0x00206895
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public ServiceVersion EndGetVersion(IAsyncResult result)
	{
		return base.Channel.EndGetVersion(result);
	}

	// Token: 0x06007581 RID: 30081 RVA: 0x002086A3 File Offset: 0x002068A3
	private IAsyncResult OnBeginGetVersion(object[] inValues, AsyncCallback callback, object asyncState)
	{
		return this.BeginGetVersion(callback, asyncState);
	}

	// Token: 0x06007582 RID: 30082 RVA: 0x002086B0 File Offset: 0x002068B0
	private object[] OnEndGetVersion(IAsyncResult result)
	{
		ServiceVersion serviceVersion = this.EndGetVersion(result);
		return new object[]
		{
			serviceVersion
		};
	}

	// Token: 0x06007583 RID: 30083 RVA: 0x002086D4 File Offset: 0x002068D4
	private void OnGetVersionCompleted(object state)
	{
		if (this.GetVersionCompleted != null)
		{
			ClientBase<ServerLocator>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<ServerLocator>.InvokeAsyncCompletedEventArgs)state;
			this.GetVersionCompleted(this, new GetVersionCompletedEventArgs(invokeAsyncCompletedEventArgs.Results, invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
		}
	}

	// Token: 0x06007584 RID: 30084 RVA: 0x00208719 File Offset: 0x00206919
	public void GetVersionAsync()
	{
		this.GetVersionAsync(null);
	}

	// Token: 0x06007585 RID: 30085 RVA: 0x00208724 File Offset: 0x00206924
	public void GetVersionAsync(object userState)
	{
		if (this.onBeginGetVersionDelegate == null)
		{
			this.onBeginGetVersionDelegate = new ClientBase<ServerLocator>.BeginOperationDelegate(this.OnBeginGetVersion);
		}
		if (this.onEndGetVersionDelegate == null)
		{
			this.onEndGetVersionDelegate = new ClientBase<ServerLocator>.EndOperationDelegate(this.OnEndGetVersion);
		}
		if (this.onGetVersionCompletedDelegate == null)
		{
			this.onGetVersionCompletedDelegate = new SendOrPostCallback(this.OnGetVersionCompleted);
		}
		base.InvokeAsync(this.onBeginGetVersionDelegate, null, this.onEndGetVersionDelegate, this.onGetVersionCompletedDelegate, userState);
	}

	// Token: 0x06007586 RID: 30086 RVA: 0x00208799 File Offset: 0x00206999
	public DatabaseServerInformation GetServerForDatabase(DatabaseServerInformation database)
	{
		return base.Channel.GetServerForDatabase(database);
	}

	// Token: 0x06007587 RID: 30087 RVA: 0x002087A7 File Offset: 0x002069A7
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public IAsyncResult BeginGetServerForDatabase(DatabaseServerInformation database, AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginGetServerForDatabase(database, callback, asyncState);
	}

	// Token: 0x06007588 RID: 30088 RVA: 0x002087B7 File Offset: 0x002069B7
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public DatabaseServerInformation EndGetServerForDatabase(IAsyncResult result)
	{
		return base.Channel.EndGetServerForDatabase(result);
	}

	// Token: 0x06007589 RID: 30089 RVA: 0x002087C8 File Offset: 0x002069C8
	private IAsyncResult OnBeginGetServerForDatabase(object[] inValues, AsyncCallback callback, object asyncState)
	{
		DatabaseServerInformation database = (DatabaseServerInformation)inValues[0];
		return this.BeginGetServerForDatabase(database, callback, asyncState);
	}

	// Token: 0x0600758A RID: 30090 RVA: 0x002087E8 File Offset: 0x002069E8
	private object[] OnEndGetServerForDatabase(IAsyncResult result)
	{
		DatabaseServerInformation databaseServerInformation = this.EndGetServerForDatabase(result);
		return new object[]
		{
			databaseServerInformation
		};
	}

	// Token: 0x0600758B RID: 30091 RVA: 0x0020880C File Offset: 0x00206A0C
	private void OnGetServerForDatabaseCompleted(object state)
	{
		if (this.GetServerForDatabaseCompleted != null)
		{
			ClientBase<ServerLocator>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<ServerLocator>.InvokeAsyncCompletedEventArgs)state;
			this.GetServerForDatabaseCompleted(this, new GetServerForDatabaseCompletedEventArgs(invokeAsyncCompletedEventArgs.Results, invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
		}
	}

	// Token: 0x0600758C RID: 30092 RVA: 0x00208851 File Offset: 0x00206A51
	public void GetServerForDatabaseAsync(DatabaseServerInformation database)
	{
		this.GetServerForDatabaseAsync(database, null);
	}

	// Token: 0x0600758D RID: 30093 RVA: 0x0020885C File Offset: 0x00206A5C
	public void GetServerForDatabaseAsync(DatabaseServerInformation database, object userState)
	{
		if (this.onBeginGetServerForDatabaseDelegate == null)
		{
			this.onBeginGetServerForDatabaseDelegate = new ClientBase<ServerLocator>.BeginOperationDelegate(this.OnBeginGetServerForDatabase);
		}
		if (this.onEndGetServerForDatabaseDelegate == null)
		{
			this.onEndGetServerForDatabaseDelegate = new ClientBase<ServerLocator>.EndOperationDelegate(this.OnEndGetServerForDatabase);
		}
		if (this.onGetServerForDatabaseCompletedDelegate == null)
		{
			this.onGetServerForDatabaseCompletedDelegate = new SendOrPostCallback(this.OnGetServerForDatabaseCompleted);
		}
		base.InvokeAsync(this.onBeginGetServerForDatabaseDelegate, new object[]
		{
			database
		}, this.onEndGetServerForDatabaseDelegate, this.onGetServerForDatabaseCompletedDelegate, userState);
	}

	// Token: 0x0600758E RID: 30094 RVA: 0x002088DC File Offset: 0x00206ADC
	public DatabaseServerInformation[] GetActiveCopiesForDatabaseAvailabilityGroup()
	{
		return base.Channel.GetActiveCopiesForDatabaseAvailabilityGroup();
	}

	// Token: 0x0600758F RID: 30095 RVA: 0x002088E9 File Offset: 0x00206AE9
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public IAsyncResult BeginGetActiveCopiesForDatabaseAvailabilityGroup(AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginGetActiveCopiesForDatabaseAvailabilityGroup(callback, asyncState);
	}

	// Token: 0x06007590 RID: 30096 RVA: 0x002088F8 File Offset: 0x00206AF8
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public DatabaseServerInformation[] EndGetActiveCopiesForDatabaseAvailabilityGroup(IAsyncResult result)
	{
		return base.Channel.EndGetActiveCopiesForDatabaseAvailabilityGroup(result);
	}

	// Token: 0x06007591 RID: 30097 RVA: 0x00208906 File Offset: 0x00206B06
	private IAsyncResult OnBeginGetActiveCopiesForDatabaseAvailabilityGroup(object[] inValues, AsyncCallback callback, object asyncState)
	{
		return this.BeginGetActiveCopiesForDatabaseAvailabilityGroup(callback, asyncState);
	}

	// Token: 0x06007592 RID: 30098 RVA: 0x00208910 File Offset: 0x00206B10
	private object[] OnEndGetActiveCopiesForDatabaseAvailabilityGroup(IAsyncResult result)
	{
		DatabaseServerInformation[] array = this.EndGetActiveCopiesForDatabaseAvailabilityGroup(result);
		return new object[]
		{
			array
		};
	}

	// Token: 0x06007593 RID: 30099 RVA: 0x00208934 File Offset: 0x00206B34
	private void OnGetActiveCopiesForDatabaseAvailabilityGroupCompleted(object state)
	{
		if (this.GetActiveCopiesForDatabaseAvailabilityGroupCompleted != null)
		{
			ClientBase<ServerLocator>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<ServerLocator>.InvokeAsyncCompletedEventArgs)state;
			this.GetActiveCopiesForDatabaseAvailabilityGroupCompleted(this, new GetActiveCopiesForDatabaseAvailabilityGroupCompletedEventArgs(invokeAsyncCompletedEventArgs.Results, invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
		}
	}

	// Token: 0x06007594 RID: 30100 RVA: 0x00208979 File Offset: 0x00206B79
	public void GetActiveCopiesForDatabaseAvailabilityGroupAsync()
	{
		this.GetActiveCopiesForDatabaseAvailabilityGroupAsync(null);
	}

	// Token: 0x06007595 RID: 30101 RVA: 0x00208984 File Offset: 0x00206B84
	public void GetActiveCopiesForDatabaseAvailabilityGroupAsync(object userState)
	{
		if (this.onBeginGetActiveCopiesForDatabaseAvailabilityGroupDelegate == null)
		{
			this.onBeginGetActiveCopiesForDatabaseAvailabilityGroupDelegate = new ClientBase<ServerLocator>.BeginOperationDelegate(this.OnBeginGetActiveCopiesForDatabaseAvailabilityGroup);
		}
		if (this.onEndGetActiveCopiesForDatabaseAvailabilityGroupDelegate == null)
		{
			this.onEndGetActiveCopiesForDatabaseAvailabilityGroupDelegate = new ClientBase<ServerLocator>.EndOperationDelegate(this.OnEndGetActiveCopiesForDatabaseAvailabilityGroup);
		}
		if (this.onGetActiveCopiesForDatabaseAvailabilityGroupCompletedDelegate == null)
		{
			this.onGetActiveCopiesForDatabaseAvailabilityGroupCompletedDelegate = new SendOrPostCallback(this.OnGetActiveCopiesForDatabaseAvailabilityGroupCompleted);
		}
		base.InvokeAsync(this.onBeginGetActiveCopiesForDatabaseAvailabilityGroupDelegate, null, this.onEndGetActiveCopiesForDatabaseAvailabilityGroupDelegate, this.onGetActiveCopiesForDatabaseAvailabilityGroupCompletedDelegate, userState);
	}

	// Token: 0x06007596 RID: 30102 RVA: 0x002089F9 File Offset: 0x00206BF9
	public DatabaseServerInformation[] GetActiveCopiesForDatabaseAvailabilityGroupExtended(GetActiveCopiesForDatabaseAvailabilityGroupParameters parameters)
	{
		return base.Channel.GetActiveCopiesForDatabaseAvailabilityGroupExtended(parameters);
	}

	// Token: 0x06007597 RID: 30103 RVA: 0x00208A07 File Offset: 0x00206C07
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public IAsyncResult BeginGetActiveCopiesForDatabaseAvailabilityGroupExtended(GetActiveCopiesForDatabaseAvailabilityGroupParameters parameters, AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginGetActiveCopiesForDatabaseAvailabilityGroupExtended(parameters, callback, asyncState);
	}

	// Token: 0x06007598 RID: 30104 RVA: 0x00208A17 File Offset: 0x00206C17
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public DatabaseServerInformation[] EndGetActiveCopiesForDatabaseAvailabilityGroupExtended(IAsyncResult result)
	{
		return base.Channel.EndGetActiveCopiesForDatabaseAvailabilityGroupExtended(result);
	}

	// Token: 0x06007599 RID: 30105 RVA: 0x00208A28 File Offset: 0x00206C28
	private IAsyncResult OnBeginGetActiveCopiesForDatabaseAvailabilityGroupExtended(object[] inValues, AsyncCallback callback, object asyncState)
	{
		GetActiveCopiesForDatabaseAvailabilityGroupParameters parameters = (GetActiveCopiesForDatabaseAvailabilityGroupParameters)inValues[0];
		return this.BeginGetActiveCopiesForDatabaseAvailabilityGroupExtended(parameters, callback, asyncState);
	}

	// Token: 0x0600759A RID: 30106 RVA: 0x00208A48 File Offset: 0x00206C48
	private object[] OnEndGetActiveCopiesForDatabaseAvailabilityGroupExtended(IAsyncResult result)
	{
		DatabaseServerInformation[] array = this.EndGetActiveCopiesForDatabaseAvailabilityGroupExtended(result);
		return new object[]
		{
			array
		};
	}

	// Token: 0x0600759B RID: 30107 RVA: 0x00208A6C File Offset: 0x00206C6C
	private void OnGetActiveCopiesForDatabaseAvailabilityGroupExtendedCompleted(object state)
	{
		if (this.GetActiveCopiesForDatabaseAvailabilityGroupExtendedCompleted != null)
		{
			ClientBase<ServerLocator>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<ServerLocator>.InvokeAsyncCompletedEventArgs)state;
			this.GetActiveCopiesForDatabaseAvailabilityGroupExtendedCompleted(this, new GetActiveCopiesForDatabaseAvailabilityGroupExtendedCompletedEventArgs(invokeAsyncCompletedEventArgs.Results, invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
		}
	}

	// Token: 0x0600759C RID: 30108 RVA: 0x00208AB1 File Offset: 0x00206CB1
	public void GetActiveCopiesForDatabaseAvailabilityGroupExtendedAsync(GetActiveCopiesForDatabaseAvailabilityGroupParameters parameters)
	{
		this.GetActiveCopiesForDatabaseAvailabilityGroupExtendedAsync(parameters, null);
	}

	// Token: 0x0600759D RID: 30109 RVA: 0x00208ABC File Offset: 0x00206CBC
	public void GetActiveCopiesForDatabaseAvailabilityGroupExtendedAsync(GetActiveCopiesForDatabaseAvailabilityGroupParameters parameters, object userState)
	{
		if (this.onBeginGetActiveCopiesForDatabaseAvailabilityGroupExtendedDelegate == null)
		{
			this.onBeginGetActiveCopiesForDatabaseAvailabilityGroupExtendedDelegate = new ClientBase<ServerLocator>.BeginOperationDelegate(this.OnBeginGetActiveCopiesForDatabaseAvailabilityGroupExtended);
		}
		if (this.onEndGetActiveCopiesForDatabaseAvailabilityGroupExtendedDelegate == null)
		{
			this.onEndGetActiveCopiesForDatabaseAvailabilityGroupExtendedDelegate = new ClientBase<ServerLocator>.EndOperationDelegate(this.OnEndGetActiveCopiesForDatabaseAvailabilityGroupExtended);
		}
		if (this.onGetActiveCopiesForDatabaseAvailabilityGroupExtendedCompletedDelegate == null)
		{
			this.onGetActiveCopiesForDatabaseAvailabilityGroupExtendedCompletedDelegate = new SendOrPostCallback(this.OnGetActiveCopiesForDatabaseAvailabilityGroupExtendedCompleted);
		}
		base.InvokeAsync(this.onBeginGetActiveCopiesForDatabaseAvailabilityGroupExtendedDelegate, new object[]
		{
			parameters
		}, this.onEndGetActiveCopiesForDatabaseAvailabilityGroupExtendedDelegate, this.onGetActiveCopiesForDatabaseAvailabilityGroupExtendedCompletedDelegate, userState);
	}

	// Token: 0x0400519C RID: 20892
	private ClientBase<ServerLocator>.BeginOperationDelegate onBeginGetVersionDelegate;

	// Token: 0x0400519D RID: 20893
	private ClientBase<ServerLocator>.EndOperationDelegate onEndGetVersionDelegate;

	// Token: 0x0400519E RID: 20894
	private SendOrPostCallback onGetVersionCompletedDelegate;

	// Token: 0x0400519F RID: 20895
	private ClientBase<ServerLocator>.BeginOperationDelegate onBeginGetServerForDatabaseDelegate;

	// Token: 0x040051A0 RID: 20896
	private ClientBase<ServerLocator>.EndOperationDelegate onEndGetServerForDatabaseDelegate;

	// Token: 0x040051A1 RID: 20897
	private SendOrPostCallback onGetServerForDatabaseCompletedDelegate;

	// Token: 0x040051A2 RID: 20898
	private ClientBase<ServerLocator>.BeginOperationDelegate onBeginGetActiveCopiesForDatabaseAvailabilityGroupDelegate;

	// Token: 0x040051A3 RID: 20899
	private ClientBase<ServerLocator>.EndOperationDelegate onEndGetActiveCopiesForDatabaseAvailabilityGroupDelegate;

	// Token: 0x040051A4 RID: 20900
	private SendOrPostCallback onGetActiveCopiesForDatabaseAvailabilityGroupCompletedDelegate;

	// Token: 0x040051A5 RID: 20901
	private ClientBase<ServerLocator>.BeginOperationDelegate onBeginGetActiveCopiesForDatabaseAvailabilityGroupExtendedDelegate;

	// Token: 0x040051A6 RID: 20902
	private ClientBase<ServerLocator>.EndOperationDelegate onEndGetActiveCopiesForDatabaseAvailabilityGroupExtendedDelegate;

	// Token: 0x040051A7 RID: 20903
	private SendOrPostCallback onGetActiveCopiesForDatabaseAvailabilityGroupExtendedCompletedDelegate;
}
