using System;
using System.Management.Automation.Remoting;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.Configuration.ObjectModel.EventLog;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.Authorization;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x02000211 RID: 529
	public sealed class PswsAuthorizationPlugin : ExchangeAuthorizationPlugin
	{
		// Token: 0x0600126C RID: 4716 RVA: 0x0003B42B File Offset: 0x0003962B
		protected override IIdentity GetExecutingUserIdentity(PSPrincipal psPrincipal, string connectionUrl, out UserToken userToken, out Microsoft.Exchange.Configuration.Core.AuthenticationType authenticationType)
		{
			userToken = HttpContext.Current.CurrentUserToken();
			authenticationType = userToken.AuthenticationType;
			return PswsAuthZHelper.GetExecutingAuthZUser(userToken);
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x0003B44A File Offset: 0x0003964A
		protected override ExchangeRunspaceConfigurationSettings BuildRunspaceConfigurationSettings(string connectionString, IIdentity identity)
		{
			return PswsAuthZHelper.BuildRunspaceConfigurationSettings(connectionString, HttpContext.Current.CurrentUserToken(), HttpContext.Current.Request.Headers);
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x0003B46C File Offset: 0x0003966C
		protected override void PreGetInitialSessionState(PSSenderInfo senderInfo)
		{
			ExTraceGlobals.PublicPluginAPITracer.TraceDebug((long)this.GetHashCode(), "[PswsAuthorizationPlugin.PreGetInitialSessionState] Enter.");
			UserToken userToken = HttpContext.Current.CurrentUserToken();
			PswsAuthZUserToken authZPluginUserToken = PswsAuthZHelper.GetAuthZPluginUserToken(userToken);
			OverBudgetException ex;
			if (PswsBudgetManager.Instance.CheckOverBudget(authZPluginUserToken, CostType.ActiveRunspace, out ex))
			{
				ExTraceGlobals.PublicPluginAPITracer.TraceError<OverBudgetException>((long)this.GetHashCode(), "[PswsAuthorizationPlugin.PreGetInitialSessionState] OverBudgetException: {0}.", ex);
				TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_PswsOverBudgetException, null, new object[]
				{
					HttpContext.Current.User.Identity.Name,
					ex.ToString(),
					PswsBudgetManager.Instance.GetConnectedUsers()
				});
				PswsErrorHandling.SendErrorToClient(PswsErrorCode.OverBudgetException, ex, ex.Snapshot);
				AuthZLogger.SafeAppendGenericError("OverBudgetException", ex.ToString(), false);
				throw ex;
			}
			ExTraceGlobals.PublicPluginAPITracer.TraceDebug((long)this.GetHashCode(), "[PswsAuthorizationPlugin.PreGetInitialSessionState] Exit.");
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x0003B544 File Offset: 0x00039744
		protected override void PostGetInitialSessionState(PSSenderInfo senderInfo)
		{
			ExTraceGlobals.PublicPluginAPITracer.TraceDebug((long)this.GetHashCode(), "[PswsAuthorizationPlugin.PostGetInitialSessionState] Enter.");
			UserToken userToken = HttpContext.Current.CurrentUserToken();
			PswsAuthZUserToken authZPluginUserToken = PswsAuthZHelper.GetAuthZPluginUserToken(userToken);
			PswsBudgetManager.Instance.StartRunspace(authZPluginUserToken);
			ExTraceGlobals.PublicPluginAPITracer.TraceDebug((long)this.GetHashCode(), "[PswsAuthorizationPlugin.PostGetInitialSessionState] Exit.");
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x0003B59A File Offset: 0x0003979A
		protected override void OnGetInitialSessionStateError(PSSenderInfo senderInfo, Exception exception)
		{
			base.OnGetInitialSessionStateError(senderInfo, exception);
			PswsErrorHandling.SendErrorToClient(PswsErrorCode.GetISSError, exception, null);
			AuthZLogger.SafeAppendGenericError(exception.GetType().FullName, exception, new Func<Exception, bool>(KnownException.IsUnhandledException));
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x0003B5CD File Offset: 0x000397CD
		protected override void OnDispose()
		{
		}
	}
}
