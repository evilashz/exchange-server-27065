using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Management.Automation;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.MapiTasks.Presentation;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x0200017A RID: 378
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class PublicFolderSplitHelper
	{
		// Token: 0x06000F15 RID: 3861 RVA: 0x00059E04 File Offset: 0x00058004
		public static Unlimited<ByteQuantifiedSize> GetTotalItemSize(IPublicFolderMailboxLoggerBase logger, IAssistantRunspaceFactory powershellFactory, Guid mailboxGuid, OrganizationId organizationId, ISplitOperationState splitOperationState)
		{
			Unlimited<ByteQuantifiedSize> totalItemSize = ByteQuantifiedSize.Zero;
			string value = string.Format("{0}\\{1}", organizationId.OrganizationalUnit.Name, mailboxGuid.ToString());
			PSCommand cmd = new PSCommand();
			cmd.AddCommand("Get-MailboxStatistics");
			cmd.AddParameter("NoADLookup");
			cmd.AddParameter("Identity", value);
			MailboxStatistics publicFolderMailboxStatistics = null;
			PublicFolderSplitHelper.PowerShellExceptionHandler(delegate(out string originOfException, out ErrorRecord error)
			{
				error = null;
				originOfException = "GetTotalItemSize - RunPSCommand Get-MailboxStatistics";
				IAssistantRunspaceProxy assistantRunspaceProxy = powershellFactory.CreateRunspaceForDatacenterAdmin(organizationId);
				publicFolderMailboxStatistics = assistantRunspaceProxy.RunPSCommand<MailboxStatistics>(cmd, out error, logger);
				if (error == null && publicFolderMailboxStatistics != null)
				{
					totalItemSize = publicFolderMailboxStatistics.TotalItemSize;
				}
			}, splitOperationState);
			return totalItemSize;
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x00059F68 File Offset: 0x00058168
		public static Unlimited<ByteQuantifiedSize> GetActualItemSize(IPublicFolderMailboxLoggerBase logger, IAssistantRunspaceFactory powershellFactory, Guid mailboxGuid, OrganizationId organizationId, ISplitOperationState splitOperationState)
		{
			Unlimited<ByteQuantifiedSize> actualItemSize = ByteQuantifiedSize.Zero;
			Collection<PSObject> output = null;
			Collection<ErrorRecord> errors = null;
			Dictionary<string, string> paramDictionary = new Dictionary<string, string>
			{
				{
					"Mailbox",
					mailboxGuid.ToString()
				},
				{
					"Organization",
					organizationId.OrganizationalUnit.Name
				}
			};
			string text = Path.Combine(ExchangeSetupContext.InstallPath, "Scripts");
			if (string.IsNullOrEmpty(text))
			{
				throw new SplitProcessorException("PublicFolderSplitHelper::GetActualItemSize - GetExchangeScriptsPath", new Exception("ExchangeScriptsPath is null or empty"));
			}
			string scriptFullPath = Path.Combine(text, "Get-PublicFolderMailboxSize.ps1");
			PublicFolderSplitHelper.PowerShellExceptionHandler(delegate(out string originOfException, out ErrorRecord error)
			{
				error = null;
				originOfException = "GetActualItemSize - RunPowershellScript Get-PublicFolderMailboxSize.ps1";
				IAssistantRunspaceProxy assistantRunspaceProxy = powershellFactory.CreateRunspaceForDatacenterAdmin(organizationId);
				output = assistantRunspaceProxy.RunPowershellScript(scriptFullPath, paramDictionary, out errors, logger);
				if (errors == null && output != null && output.Count > 0)
				{
					actualItemSize = (ByteQuantifiedSize)output[0].BaseObject;
					return;
				}
				error = errors[0];
			}, splitOperationState);
			return actualItemSize;
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0005A154 File Offset: 0x00058354
		public static Unlimited<ByteQuantifiedSize> GetMailboxQuota(IPublicFolderMailboxLoggerBase logger, IAssistantRunspaceFactory powershellFactory, Guid mailboxGuid, OrganizationId organizationId, ISplitOperationState splitOperationState)
		{
			ADObjectId mailboxDatabase = null;
			bool useDatabaseQuotaDefaults = false;
			Unlimited<ByteQuantifiedSize> mailboxQuota = Unlimited<ByteQuantifiedSize>.UnlimitedValue;
			string value = string.Format("{0}\\{1}", organizationId.OrganizationalUnit.Name, mailboxGuid.ToString());
			PSCommand psCommand = new PSCommand();
			psCommand.AddCommand("Get-Mailbox");
			psCommand.AddParameter("PublicFolder");
			psCommand.AddParameter("Identity", value);
			Mailbox publicFolderMailbox = null;
			PublicFolderSplitHelper.PowerShellExceptionHandler(delegate(out string originOfException, out ErrorRecord error)
			{
				error = null;
				originOfException = "GetMailboxQuota - RunPSCommand: Get-Mailbox -PublicFolder";
				IAssistantRunspaceProxy assistantRunspaceProxy = powershellFactory.CreateRunspaceForDatacenterAdmin(organizationId);
				publicFolderMailbox = assistantRunspaceProxy.RunPSCommand<Mailbox>(psCommand, out error, logger);
				if (error == null)
				{
					mailboxQuota = publicFolderMailbox.ProhibitSendQuota;
					useDatabaseQuotaDefaults = (publicFolderMailbox.UseDatabaseQuotaDefaults ?? false);
					MailboxDatabase mailboxDatabase = publicFolderMailbox.Database;
					if (useDatabaseQuotaDefaults && mailboxDatabase != null)
					{
						psCommand.Clear();
						psCommand.AddCommand("Get-MailboxDatabase");
						psCommand.AddParameter("Identity", mailboxDatabase.Name);
						originOfException = "GetMailboxQuota - RunPSCommand: Get-MailboxDatabase";
						mailboxDatabase = assistantRunspaceProxy.RunPSCommand<MailboxDatabase>(psCommand, out error, logger);
						if (error == null)
						{
							mailboxQuota = mailboxDatabase.ProhibitSendQuota;
						}
					}
				}
			}, splitOperationState);
			return mailboxQuota;
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0005A21C File Offset: 0x0005841C
		public static bool IsPrimaryHierarchy(Guid mailboxGuid, OrganizationId organizationId)
		{
			Guid g;
			if (PublicFolderSession.TryGetPrimaryHierarchyMailboxGuid(organizationId, out g))
			{
				return mailboxGuid.Equals(g);
			}
			throw new ObjectNotFoundException(PublicFolderSession.GetNoPublicFoldersProvisionedError(organizationId));
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0005A248 File Offset: 0x00058448
		public static void SyncAndWaitForCompletion(Guid mailboxGuid, OrganizationId organizationId, IXSOFactory xsoFactory, IPublicFolderMailboxLoggerBase logger, ISplitOperationState splitOperationState)
		{
			ExchangePrincipal contentMailboxPrincipal;
			if (!PublicFolderSession.TryGetPublicFolderMailboxPrincipal(organizationId, mailboxGuid, false, out contentMailboxPrincipal))
			{
				throw new ObjectNotFoundException(ServerStrings.PublicFolderMailboxNotFound);
			}
			PublicFolderSyncJobState publicFolderSyncJobState = PublicFolderSyncJobRpc.StartSyncHierarchy(contentMailboxPrincipal, true);
			int num = 0;
			while (publicFolderSyncJobState.JobStatus != PublicFolderSyncJobState.Status.Completed && publicFolderSyncJobState.LastError == null && (double)num++ < PublicFolderSplitConfig.Instance.TimeoutForSynchronousOperation.TotalMilliseconds / PublicFolderSplitConfig.Instance.QuerySyncStatusInterval.TotalMilliseconds)
			{
				Thread.Sleep(PublicFolderSplitConfig.Instance.QuerySyncStatusInterval.Milliseconds);
				publicFolderSyncJobState = PublicFolderSyncJobRpc.QueryStatusSyncHierarchy(contentMailboxPrincipal);
			}
			bool flag;
			if (publicFolderSyncJobState.JobStatus != PublicFolderSyncJobState.Status.Completed || PublicFolderSplitHelper.IsSyncRequired(mailboxGuid, organizationId, out flag, xsoFactory, logger))
			{
				splitOperationState.Error = new SyncInProgressException(organizationId.OrganizationalUnit.Name, mailboxGuid.ToString());
				return;
			}
			if (PublicFolderSplitHelper.HasSyncFailure(publicFolderSyncJobState))
			{
				splitOperationState.Error = publicFolderSyncJobState.LastError;
			}
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0005A327 File Offset: 0x00058527
		public static bool HasSyncFailure(PublicFolderSyncJobState syncJobState)
		{
			return syncJobState.LastError != null;
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0005A338 File Offset: 0x00058538
		public static bool IsSyncRequired(Guid mailboxGuid, OrganizationId organizationId, out bool isLongRunningOp, IXSOFactory xsoFactory, IPublicFolderMailboxLoggerBase logger)
		{
			isLongRunningOp = false;
			bool flag = true;
			using (PublicFolderSession publicFolderSession = PublicFolderSession.OpenAsAdmin(organizationId, null, mailboxGuid, null, CultureInfo.CurrentCulture, string.Format("{0};Action={1}", "Client=TBA", "PublicFolderSplitHelper"), null))
			{
				using (Folder folder = xsoFactory.BindToFolder(publicFolderSession, publicFolderSession.GetTombstonesRootFolderId()) as Folder)
				{
					using (UserConfiguration configuration = UserConfiguration.GetConfiguration(folder, new UserConfigurationName("PublicFolderSyncInfo", ConfigurationNameKind.Name), UserConfigurationTypes.Dictionary))
					{
						IDictionary dictionary = configuration.GetDictionary();
						int? num = dictionary.Contains("NumberOfFoldersToBeSynced") ? ((int?)dictionary["NumberOfFoldersToBeSynced"]) : null;
						int? num2 = dictionary.Contains("NumberOfFoldersSynced") ? ((int?)dictionary["NumberOfFoldersSynced"]) : null;
						int? num3 = (num == null) ? null : (num - (num2 ?? 0));
						flag = (num3 == null || num3 > 0);
						isLongRunningOp = (flag && (num3 == null || num3 > PublicFolderSplitConfig.Instance.LongRunningSyncChangeCount));
						logger.LogEvent(LogEventType.Statistics, string.Format("PublicFolderSplitHelper::IsSyncRequired - FTBS={0},FS={1},FTBSN={2}", num, num2, num3));
					}
				}
			}
			return flag;
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0005A544 File Offset: 0x00058744
		public static void PowerShellExceptionHandler(PublicFolderSplitHelper.PowerShellMethod method, ISplitOperationState splitOperationState)
		{
			if (splitOperationState == null)
			{
				PublicFolderSplitHelper.PowerShellExceptionHandler(method);
				return;
			}
			ErrorRecord errorRecord = null;
			string errorDetails = null;
			try
			{
				method(out errorDetails, out errorRecord);
			}
			catch (ParameterBindingException error)
			{
				splitOperationState.Error = error;
				splitOperationState.ErrorDetails = errorDetails;
			}
			catch (CmdletInvocationException error2)
			{
				splitOperationState.Error = error2;
				splitOperationState.ErrorDetails = errorDetails;
			}
			if (errorRecord != null)
			{
				splitOperationState.Error = errorRecord.Exception;
				splitOperationState.ErrorDetails = errorDetails;
			}
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x0005A5C0 File Offset: 0x000587C0
		private static void PowerShellExceptionHandler(PublicFolderSplitHelper.PowerShellMethod method)
		{
			ErrorRecord errorRecord = null;
			string arg = null;
			try
			{
				method(out arg, out errorRecord);
			}
			catch (ParameterBindingException innerException)
			{
				throw new SplitProcessorException(string.Format("PublicFolderSplitHelper::{0}", arg), innerException);
			}
			catch (CmdletInvocationException innerException2)
			{
				throw new SplitProcessorException(string.Format("PublicFolderSplitHelper::{0}", arg), innerException2);
			}
			if (errorRecord != null)
			{
				throw new SplitProcessorException(string.Format("PublicFolderSplitHelper::{0} - {1}", arg, (errorRecord.Exception == null) ? null : errorRecord.Exception.Message), errorRecord.Exception);
			}
		}

		// Token: 0x0400099C RID: 2460
		private const string GetActualSizeScriptName = "Get-PublicFolderMailboxSize.ps1";

		// Token: 0x0200017B RID: 379
		// (Invoke) Token: 0x06000F1F RID: 3871
		public delegate void PowerShellMethod(out string originOfException, out ErrorRecord error);
	}
}
