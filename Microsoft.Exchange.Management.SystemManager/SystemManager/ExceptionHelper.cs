using System;
using System.Management.Automation;
using System.Management.Automation.Remoting;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Configuration.MonadDataProvider;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200010E RID: 270
	public class ExceptionHelper
	{
		// Token: 0x060009F8 RID: 2552 RVA: 0x00022844 File Offset: 0x00020A44
		public static bool IsUICriticalException(Exception ex)
		{
			return ex is NullReferenceException || ex is StackOverflowException || ex is OutOfMemoryException || ex is ThreadAbortException || ex is IndexOutOfRangeException || ex is AccessViolationException || ex is SecurityException;
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0002288F File Offset: 0x00020A8F
		public static bool IsWellknownExceptionFromServer(Exception ex)
		{
			return ex != null && (ex is RemoteException || ex is PSRemotingTransportException || ex is CommandNotFoundException || ex is ParameterBindingException || ex is InvalidOperationException || ex is PSRemotingDataStructureException);
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x000228C9 File Offset: 0x00020AC9
		public static bool IsWellknownCommandExecutionException(Exception ex)
		{
			return ex is CommandExecutionException && ExceptionHelper.IsWellknownExceptionFromServer(ex.InnerException);
		}
	}
}
