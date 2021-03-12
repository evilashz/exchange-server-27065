using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x02000182 RID: 386
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PrepareSplitPlanOperation : SplitOperationBase
	{
		// Token: 0x06000F79 RID: 3961 RVA: 0x0005BAC0 File Offset: 0x00059CC0
		internal PrepareSplitPlanOperation(IPublicFolderSplitState splitState, IPublicFolderSession session, IPublicFolderMailboxLoggerBase logger, IAssistantRunspaceFactory powershellFactory, string splitScriptPath) : base(PrepareSplitPlanOperation.OperationName, session, splitState, logger, powershellFactory, splitState.PrepareSplitPlanState, SplitProgressState.PrepareSplitPlanStarted, SplitProgressState.PrepareSplitPlanCompleted)
		{
			this.splitPlanScriptPath = splitScriptPath;
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0005BAED File Offset: 0x00059CED
		protected override void InvokeInternal()
		{
			this.CalculateSplitPlan();
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0005BAF8 File Offset: 0x00059CF8
		private void CalculateSplitPlan()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("SourcePublicFolderMailbox", base.CurrentPublicFolderSession.MailboxGuid.ToString());
			dictionary.Add("TargetPublicFolderMailbox", this.splitState.TargetMailboxGuid.ToString());
			dictionary.Add("Organization", base.CurrentPublicFolderSession.OrganizationId.OrganizationalUnit.Name);
			dictionary.Add("MinimumPercentageToSplit", "1");
			dictionary.Add("SplitPlan", "SwitchValue");
			Collection<ErrorRecord> collection = null;
			try
			{
				IAssistantRunspaceProxy assistantRunspaceProxy = this.powershellFactory.CreateRunspaceForDatacenterAdmin(base.CurrentPublicFolderSession.OrganizationId);
				Collection<PSObject> collection2 = assistantRunspaceProxy.RunPowershellScript(this.splitPlanScriptPath, dictionary, out collection, this.logger);
				PSObject psobject = null;
				StringBuilder stringBuilder = new StringBuilder("Split-PublicFolderMailbox.ps1 output:\n");
				if (collection2 != null && collection2.Count > 0)
				{
					foreach (PSObject psobject2 in collection2)
					{
						stringBuilder.Append(psobject2.ToString());
						psobject = psobject2;
					}
				}
				this.logger.LogEvent(LogEventType.Verbose, stringBuilder.ToString());
				if (collection == null || collection.Count < 1)
				{
					if (psobject != null)
					{
						IPublicFolderSplitPlan publicFolderSplitPlan = psobject.BaseObject as IPublicFolderSplitPlan;
						if (publicFolderSplitPlan != null && publicFolderSplitPlan.FoldersToSplit != null && publicFolderSplitPlan.FoldersToSplit.Count > 0)
						{
							this.splitState.SplitPlan = publicFolderSplitPlan;
							return;
						}
					}
					base.OperationState.Error = new ApplicationException("No split plan determined when there should be one");
				}
				else
				{
					base.OperationState.Error = collection[0].Exception;
				}
			}
			catch (ParameterBindingException error)
			{
				base.OperationState.Error = error;
			}
			catch (CmdletInvocationException error2)
			{
				base.OperationState.Error = error2;
			}
		}

		// Token: 0x040009C9 RID: 2505
		private static string OperationName = "PrepareSplitPlan";

		// Token: 0x040009CA RID: 2506
		private readonly string splitPlanScriptPath;
	}
}
