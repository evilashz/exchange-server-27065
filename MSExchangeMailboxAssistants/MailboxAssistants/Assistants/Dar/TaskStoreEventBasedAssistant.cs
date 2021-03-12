using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Dar;
using Microsoft.Exchange.Management.CompliancePolicy.DarTasks;
using Microsoft.Mapi;
using Microsoft.Office.CompliancePolicy.Exchange.Dar.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Dar
{
	// Token: 0x02000263 RID: 611
	internal class TaskStoreEventBasedAssistant : EventBasedAssistant, IEventBasedAssistant, IAssistantBase
	{
		// Token: 0x060016A9 RID: 5801 RVA: 0x000802FA File Offset: 0x0007E4FA
		public TaskStoreEventBasedAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x00080305 File Offset: 0x0007E505
		public bool IsEventInteresting(MapiEvent mapiEvent)
		{
			return ObjectClass.IsOfClass(mapiEvent.ObjectClass, "IPM.Configuration.DarTask") && (mapiEvent.EventMask & MapiEventTypeFlags.ObjectCreated) == MapiEventTypeFlags.ObjectCreated;
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x00080390 File Offset: 0x0007E590
		protected override void HandleEventInternal(MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item, List<KeyValuePair<string, object>> customDataToLog)
		{
			if (ObjectClass.IsOfClass(mapiEvent.ObjectClass, "IPM.Configuration.DarTask") && itemStore != null && item != null)
			{
				try
				{
					ExceptionHandler.Handle(delegate
					{
						using (HostRpcClient hostRpcClient = new HostRpcClient(itemStore.ServerFullyQualifiedDomainName))
						{
							hostRpcClient.NotifyTaskStoreChange(itemStore.MailboxOwner.MailboxInfo.OrganizationId.GetBytes(Encoding.UTF8));
						}
					}, new ExceptionGroupHandler(ExceptionGroupHandlers.Rpc), new ExceptionHandlingOptions(TimeSpan.FromMinutes(1.0))
					{
						ClientId = "TaskStoreEventBasedAssistant",
						Operation = "NotifyTaskStoreChange"
					});
				}
				catch (AggregateException ex)
				{
					ExTraceGlobals.GeneralTracer.TraceError<string>((long)this.GetHashCode(), "Error during call to TaskStoreEventBasedAssistant {0}", ex.ToString());
				}
			}
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x00080448 File Offset: 0x0007E648
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x00080450 File Offset: 0x0007E650
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x00080458 File Offset: 0x0007E658
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}
	}
}
