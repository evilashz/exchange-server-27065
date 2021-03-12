using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Approval.Applications.Resources;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Approval;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Approval.Applications
{
	// Token: 0x02000006 RID: 6
	internal class ApprovalApplication
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000024D4 File Offset: 0x000006D4
		private static ApprovalApplication CreateApprovalApplication(ApprovalApplicationId applicationId)
		{
			switch (applicationId)
			{
			case ApprovalApplicationId.AutoGroup:
				return new AutoGroupApplication();
			case ApprovalApplicationId.ModeratedRecipient:
				return new ModeratedDLApplication();
			default:
				return null;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002500 File Offset: 0x00000700
		internal static ApprovalApplication[] CreateApprovalApplications()
		{
			ApprovalApplication[] array = new ApprovalApplication[2];
			for (ApprovalApplicationId approvalApplicationId = ApprovalApplicationId.AutoGroup; approvalApplicationId < ApprovalApplicationId.Count; approvalApplicationId++)
			{
				array[(int)approvalApplicationId] = ApprovalApplication.CreateApprovalApplication(approvalApplicationId);
			}
			return array;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000252C File Offset: 0x0000072C
		internal static Collection<PSObject> ExecuteCommandsInRunspace(SmtpAddress user, PSCommand command, CultureInfo executingCulture, out string errorMessage, out string warningMessage)
		{
			Collection<PSObject> result = null;
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			errorMessage = string.Empty;
			warningMessage = string.Empty;
			IRecipientSession recipientSession = ApprovalProcessor.CreateRecipientSessionFromSmtpAddress(user);
			ADUser aduser = recipientSession.FindByProxyAddress(ProxyAddress.Parse((string)user)) as ADUser;
			if (aduser == null)
			{
				errorMessage = Strings.ErrorUserNotFound((string)user);
				return null;
			}
			GenericIdentity identity = new GenericIdentity(aduser.Sid.ToString());
			InitialSessionState initialSessionState = null;
			try
			{
				initialSessionState = new ExchangeRunspaceConfiguration(identity).CreateInitialSessionState();
				initialSessionState.LanguageMode = PSLanguageMode.FullLanguage;
			}
			catch (CmdletAccessDeniedException)
			{
				errorMessage = Strings.ErrorNoRBACRoleAssignment((string)user);
				return null;
			}
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
			try
			{
				if (executingCulture != null)
				{
					Thread.CurrentThread.CurrentCulture = executingCulture;
					Thread.CurrentThread.CurrentUICulture = executingCulture;
				}
				using (RunspaceProxy runspaceProxy = new RunspaceProxy(new RunspaceMediator(new ForestScopeRunspaceFactory(new BasicInitialSessionStateFactory(initialSessionState), new BasicPSHostFactory(typeof(RunspaceHost), true)), new EmptyRunspaceCache())))
				{
					try
					{
						PowerShellProxy powerShellProxy = new PowerShellProxy(runspaceProxy, command);
						result = powerShellProxy.Invoke<PSObject>();
						if (powerShellProxy.Errors != null)
						{
							foreach (ErrorRecord errorRecord in powerShellProxy.Errors)
							{
								stringBuilder.Append(errorRecord.ToString());
							}
						}
						if (powerShellProxy.Warnings != null)
						{
							foreach (WarningRecord warningRecord in powerShellProxy.Warnings)
							{
								stringBuilder2.Append(warningRecord.ToString());
							}
						}
					}
					catch (CmdletInvocationException)
					{
						stringBuilder.Append(Strings.ErrorTaskInvocationFailed((string)user).ToString(executingCulture));
					}
					catch (ParameterBindingException)
					{
						stringBuilder.Append(Strings.ErrorTaskInvocationFailed((string)user).ToString(executingCulture));
					}
					catch (CommandNotFoundException)
					{
						stringBuilder.Append(Strings.ErrorTaskInvocationFailed((string)user).ToString(executingCulture));
					}
					catch (RuntimeException)
					{
						stringBuilder.Append(Strings.ErrorTaskInvocationFailed((string)user).ToString(executingCulture));
					}
					finally
					{
						errorMessage = stringBuilder.ToString();
						warningMessage = stringBuilder2.ToString();
					}
				}
			}
			finally
			{
				if (executingCulture != null)
				{
					Thread.CurrentThread.CurrentCulture = currentCulture;
					Thread.CurrentThread.CurrentUICulture = currentUICulture;
				}
			}
			return result;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000288C File Offset: 0x00000A8C
		internal ApprovalApplication()
		{
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002894 File Offset: 0x00000A94
		internal virtual bool OnApprove(MessageItem message)
		{
			return true;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002897 File Offset: 0x00000A97
		internal virtual bool OnReject(MessageItem message)
		{
			return true;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000289A File Offset: 0x00000A9A
		internal virtual void OnAllDecisionMakersNdred(MessageItem message)
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000289C File Offset: 0x00000A9C
		internal virtual void OnAllDecisionMakersOof(MessageItem message)
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000289E File Offset: 0x00000A9E
		internal virtual bool OnExpire(MessageItem message, out bool sendUpdate)
		{
			sendUpdate = true;
			return true;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000028A4 File Offset: 0x00000AA4
		internal virtual void OnStart()
		{
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000028A6 File Offset: 0x00000AA6
		internal virtual void OnStop()
		{
		}
	}
}
