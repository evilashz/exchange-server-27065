using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using www.outlook.com.highavailability.ServerLocator.v1;

// Token: 0x02000D3C RID: 3388
[DebuggerStepThrough]
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public class GetServerForDatabaseCompletedEventArgs : AsyncCompletedEventArgs
{
	// Token: 0x0600756B RID: 30059 RVA: 0x00208414 File Offset: 0x00206614
	public GetServerForDatabaseCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
	{
		this.results = results;
	}

	// Token: 0x17001F84 RID: 8068
	// (get) Token: 0x0600756C RID: 30060 RVA: 0x00208427 File Offset: 0x00206627
	public DatabaseServerInformation Result
	{
		get
		{
			base.RaiseExceptionIfNecessary();
			return (DatabaseServerInformation)this.results[0];
		}
	}

	// Token: 0x04005199 RID: 20889
	private object[] results;
}
