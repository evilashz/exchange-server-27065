using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.Storage.GroupMailbox;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007E0 RID: 2016
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class EscalationGetter
	{
		// Token: 0x06004B8A RID: 19338 RVA: 0x0013ACC5 File Offset: 0x00138EC5
		public EscalationGetter(GroupMailboxLocator group, IMailboxSession groupSession)
		{
			ArgumentValidator.ThrowIfNull("group", group);
			ArgumentValidator.ThrowIfNull("groupSession", groupSession);
			this.group = group;
			this.groupSession = groupSession;
		}

		// Token: 0x06004B8B RID: 19339 RVA: 0x0013ACF4 File Offset: 0x00138EF4
		public List<string> Execute()
		{
			IExtensibleLogger logger = GroupEscalationGetterDiagnosticsFrameFactory.Default.CreateLogger(this.groupSession.MailboxGuid, this.groupSession.OrganizationId);
			IMailboxAssociationPerformanceTracker mailboxAssociationPerformanceTracker = GroupEscalationGetterDiagnosticsFrameFactory.Default.CreatePerformanceTracker(null);
			List<string> result;
			using (GroupEscalationGetterDiagnosticsFrameFactory.Default.CreateDiagnosticsFrame("XSO", "EscalationGetter", logger, mailboxAssociationPerformanceTracker))
			{
				StoreBuilder storeBuilder = new StoreBuilder(this.groupSession, XSOFactory.Default, logger, this.groupSession.ClientInfoString);
				using (IAssociationStore associationStore = storeBuilder.Create(this.group, mailboxAssociationPerformanceTracker))
				{
					IEnumerable<IPropertyBag> associationsByType = associationStore.GetAssociationsByType("IPM.MailboxAssociation.User", MailboxAssociationBaseSchema.ShouldEscalate, EscalationGetter.EscalateProperties);
					List<string> list = new List<string>();
					foreach (IPropertyBag propertyBag in associationsByType)
					{
						string text = propertyBag[MailboxAssociationBaseSchema.LegacyDN] as string;
						if (text != null)
						{
							list.Add(text);
						}
						else
						{
							EscalationGetter.Tracer.TraceError<string>((long)this.GetHashCode(), "EscalationGetter.Execute: Missing LegacyDn for item with Id {0}.", propertyBag[ItemSchema.Id].ToString());
						}
						bool valueOrDefault = associationStore.GetValueOrDefault<bool>(propertyBag, MailboxAssociationBaseSchema.IsAutoSubscribed, false);
						if (valueOrDefault)
						{
							mailboxAssociationPerformanceTracker.IncrementAutoSubscribedMembers();
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x040028F8 RID: 10488
		private const string OperationContext = "XSO";

		// Token: 0x040028F9 RID: 10489
		private const string Operation = "EscalationGetter";

		// Token: 0x040028FA RID: 10490
		private static readonly Trace Tracer = ExTraceGlobals.GroupMailboxAccessLayerTracer;

		// Token: 0x040028FB RID: 10491
		private readonly GroupMailboxLocator group;

		// Token: 0x040028FC RID: 10492
		private readonly IMailboxSession groupSession;

		// Token: 0x040028FD RID: 10493
		private static readonly PropertyDefinition[] EscalateProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			MailboxAssociationBaseSchema.ExternalId,
			MailboxAssociationBaseSchema.LegacyDN,
			MailboxAssociationBaseSchema.IsAutoSubscribed
		};
	}
}
