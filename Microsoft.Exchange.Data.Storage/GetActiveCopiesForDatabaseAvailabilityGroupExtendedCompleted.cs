using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using www.outlook.com.highavailability.ServerLocator.v1;

// Token: 0x02000D3E RID: 3390
[DebuggerStepThrough]
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public class GetActiveCopiesForDatabaseAvailabilityGroupExtendedCompletedEventArgs : AsyncCompletedEventArgs
{
	// Token: 0x0600756F RID: 30063 RVA: 0x00208464 File Offset: 0x00206664
	public GetActiveCopiesForDatabaseAvailabilityGroupExtendedCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
	{
		this.results = results;
	}

	// Token: 0x17001F86 RID: 8070
	// (get) Token: 0x06007570 RID: 30064 RVA: 0x00208477 File Offset: 0x00206677
	public DatabaseServerInformation[] Result
	{
		get
		{
			base.RaiseExceptionIfNecessary();
			return (DatabaseServerInformation[])this.results[0];
		}
	}

	// Token: 0x0400519B RID: 20891
	private object[] results;
}
