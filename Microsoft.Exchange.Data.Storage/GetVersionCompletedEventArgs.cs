using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using www.outlook.com.highavailability.ServerLocator.v1;

// Token: 0x02000D3B RID: 3387
[DebuggerStepThrough]
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public class GetVersionCompletedEventArgs : AsyncCompletedEventArgs
{
	// Token: 0x06007569 RID: 30057 RVA: 0x002083EC File Offset: 0x002065EC
	public GetVersionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
	{
		this.results = results;
	}

	// Token: 0x17001F83 RID: 8067
	// (get) Token: 0x0600756A RID: 30058 RVA: 0x002083FF File Offset: 0x002065FF
	public ServiceVersion Result
	{
		get
		{
			base.RaiseExceptionIfNecessary();
			return (ServiceVersion)this.results[0];
		}
	}

	// Token: 0x04005198 RID: 20888
	private object[] results;
}
