using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using www.outlook.com.highavailability.ServerLocator.v1;

// Token: 0x02000D3D RID: 3389
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
[DebuggerStepThrough]
public class GetActiveCopiesForDatabaseAvailabilityGroupCompletedEventArgs : AsyncCompletedEventArgs
{
	// Token: 0x0600756D RID: 30061 RVA: 0x0020843C File Offset: 0x0020663C
	public GetActiveCopiesForDatabaseAvailabilityGroupCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
	{
		this.results = results;
	}

	// Token: 0x17001F85 RID: 8069
	// (get) Token: 0x0600756E RID: 30062 RVA: 0x0020844F File Offset: 0x0020664F
	public DatabaseServerInformation[] Result
	{
		get
		{
			base.RaiseExceptionIfNecessary();
			return (DatabaseServerInformation[])this.results[0];
		}
	}

	// Token: 0x0400519A RID: 20890
	private object[] results;
}
