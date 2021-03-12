using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.com.IPC.WSService;

// Token: 0x02000A02 RID: 2562
[DebuggerStepThrough]
[GeneratedCode("System.ServiceModel", "3.0.0.0")]
public class CertifyCompletedEventArgs : AsyncCompletedEventArgs
{
	// Token: 0x060037D6 RID: 14294 RVA: 0x0008D281 File Offset: 0x0008B481
	public CertifyCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
	{
		this.results = results;
	}

	// Token: 0x17000E36 RID: 3638
	// (get) Token: 0x060037D7 RID: 14295 RVA: 0x0008D294 File Offset: 0x0008B494
	public XrmlCertificateChain GroupIdentityCredential
	{
		get
		{
			base.RaiseExceptionIfNecessary();
			return (XrmlCertificateChain)this.results[0];
		}
	}

	// Token: 0x17000E37 RID: 3639
	// (get) Token: 0x060037D8 RID: 14296 RVA: 0x0008D2A9 File Offset: 0x0008B4A9
	public VersionData Result
	{
		get
		{
			base.RaiseExceptionIfNecessary();
			return (VersionData)this.results[1];
		}
	}

	// Token: 0x04002F58 RID: 12120
	private object[] results;
}
