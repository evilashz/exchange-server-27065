using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007E2 RID: 2018
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PinnersGetter
	{
		// Token: 0x06004B9D RID: 19357 RVA: 0x0013B762 File Offset: 0x00139962
		public PinnersGetter(GroupMailboxLocator group, MailboxSession groupSession)
		{
			ArgumentValidator.ThrowIfNull("group", group);
			ArgumentValidator.ThrowIfNull("groupSession", groupSession);
			this.group = group;
			this.groupSession = groupSession;
		}

		// Token: 0x06004B9E RID: 19358 RVA: 0x0013B790 File Offset: 0x00139990
		public List<string> Execute()
		{
			IExtensibleLogger logger = MailboxAssociationDiagnosticsFrameFactory.Default.CreateLogger(this.groupSession.MailboxGuid, this.groupSession.OrganizationId);
			IMailboxAssociationPerformanceTracker performanceTracker = MailboxAssociationDiagnosticsFrameFactory.Default.CreatePerformanceTracker(null);
			List<string> result;
			using (MailboxAssociationDiagnosticsFrameFactory.Default.CreateDiagnosticsFrame("XSO", "PinnersGetter", logger, performanceTracker))
			{
				StoreBuilder storeBuilder = new StoreBuilder(this.groupSession, XSOFactory.Default, logger, this.groupSession.ClientInfoString);
				using (IAssociationStore associationStore = storeBuilder.Create(this.group, performanceTracker))
				{
					IEnumerable<IPropertyBag> associationsByType = associationStore.GetAssociationsByType("IPM.MailboxAssociation.User", MailboxAssociationBaseSchema.IsPin, PinnersGetter.PinnerProperties);
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
							PinnersGetter.Tracer.TraceError<string>((long)this.GetHashCode(), "PinnersGetter.Execute: Missing LegacyDn for item with Id {0}.", propertyBag[ItemSchema.Id].ToString());
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0400290D RID: 10509
		private const string OperationContext = "XSO";

		// Token: 0x0400290E RID: 10510
		private const string Operation = "PinnersGetter";

		// Token: 0x0400290F RID: 10511
		private static readonly Trace Tracer = ExTraceGlobals.GroupMailboxAccessLayerTracer;

		// Token: 0x04002910 RID: 10512
		private readonly GroupMailboxLocator group;

		// Token: 0x04002911 RID: 10513
		private readonly MailboxSession groupSession;

		// Token: 0x04002912 RID: 10514
		private static readonly PropertyDefinition[] PinnerProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			MailboxAssociationBaseSchema.ExternalId,
			MailboxAssociationBaseSchema.LegacyDN
		};
	}
}
