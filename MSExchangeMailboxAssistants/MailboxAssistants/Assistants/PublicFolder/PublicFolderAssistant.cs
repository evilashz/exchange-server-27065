using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Assistants.Diagnostics;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.PublicFolder;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x02000171 RID: 369
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublicFolderAssistant : TimeBasedAssistant, ITimeBasedAssistant, IAssistantBase
	{
		// Token: 0x06000ED2 RID: 3794 RVA: 0x000585CF File Offset: 0x000567CF
		public PublicFolderAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x000585DC File Offset: 0x000567DC
		public static XElement GetPublicFolderAssistantDiagnosticInfo(DiagnosticsArgument arguments)
		{
			XElement xelement = new XElement("PublicFolderAssistant");
			if (arguments.HasArgument("split"))
			{
				xelement.Add(PublicFolderSplitProcessor.GetDiagnosticInfo(arguments));
			}
			return xelement;
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00058613 File Offset: 0x00056813
		public void OnWorkCycleCheckpoint()
		{
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x00058618 File Offset: 0x00056818
		protected override void InvokeInternal(InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog)
		{
			PublicFolderSession publicFolderSession = invokeArgs.StoreSession as PublicFolderSession;
			if (publicFolderSession == null)
			{
				return;
			}
			PublicFolderAssistant.Tracer.TraceDebug<string>((long)this.GetHashCode(), "PublicFolderAssistant::InvokeInternal::{0} - Begin processing of public folder mailbox", publicFolderSession.DisplayAddress);
			new PublicFolderHierarchySyncProcessor(publicFolderSession, PublicFolderAssistant.Tracer).Invoke();
			new PublicFolderItemProcessor(publicFolderSession, PublicFolderAssistant.Tracer).Invoke();
			using (PublicFolderSplitProcessor publicFolderSplitProcessor = new PublicFolderSplitProcessor(publicFolderSession, PublicFolderAssistant.Tracer))
			{
				publicFolderSplitProcessor.Invoke();
			}
			PublicFolderAssistant.Tracer.TraceDebug<string>((long)this.GetHashCode(), "PublicFolderAssistant::InvokeInternal::{0} - End processing of public folder mailbox", publicFolderSession.DisplayAddress);
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x000586C8 File Offset: 0x000568C8
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x000586D0 File Offset: 0x000568D0
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x000586D8 File Offset: 0x000568D8
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x04000957 RID: 2391
		private static readonly Trace Tracer = ExTraceGlobals.AssistantTracer;
	}
}
