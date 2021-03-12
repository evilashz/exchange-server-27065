using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000467 RID: 1127
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class MailboxAssociationContext
	{
		// Token: 0x060027B2 RID: 10162 RVA: 0x0009CB7C File Offset: 0x0009AD7C
		public MailboxAssociationContext(IRecipientSession adSession, ADUser mailbox, string cmdletName, MailboxAssociationIdParameter associationId, bool includeNotPromotedProperties = false)
		{
			ArgumentValidator.ThrowIfNull("adSession", adSession);
			ArgumentValidator.ThrowIfNull("mailbox", mailbox);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("cmdletName", cmdletName);
			this.adSession = adSession;
			this.mailbox = mailbox;
			this.associationId = associationId;
			this.clientInfoString = string.Format("Client=Management;Action={0}", cmdletName);
			this.cmdletName = cmdletName;
			this.includeNotPromotedProperties = includeNotPromotedProperties;
			this.groupMailboxAccessLayerFactory = GroupMailboxAccessLayerEntityFactory.Instantiate(adSession, mailbox);
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x0009CBF4 File Offset: 0x0009ADF4
		public void Execute(Action<MailboxAssociationFromStore, IAssociationAdaptor, ADUser, IExtensibleLogger> task)
		{
			IExtensibleLogger logger = MailboxAssociationDiagnosticsFrameFactory.Default.CreateLogger(this.mailbox.ExchangeGuid, this.mailbox.OrganizationId);
			IMailboxAssociationPerformanceTracker performanceTracker = MailboxAssociationDiagnosticsFrameFactory.Default.CreatePerformanceTracker(null);
			using (MailboxAssociationDiagnosticsFrameFactory.Default.CreateDiagnosticsFrame("MailboxAssociationContext.Execute", this.clientInfoString, logger, performanceTracker))
			{
				StoreBuilder storeBuilder = new StoreBuilder(null, XSOFactory.Default, logger, this.clientInfoString);
				GroupMailboxAccessLayer groupMailboxAccessLayer = new GroupMailboxAccessLayer(this.adSession, storeBuilder, performanceTracker, logger, this.clientInfoString);
				MailboxLocator mailboxLocator = this.groupMailboxAccessLayerFactory.CreateMasterLocator();
				using (IAssociationStore associationStore = storeBuilder.Create(mailboxLocator, groupMailboxAccessLayer.PerformanceTracker))
				{
					BaseAssociationAdaptor associationAdaptor = this.groupMailboxAccessLayerFactory.CreateAssociationAdaptor(mailboxLocator, associationStore);
					if (this.associationId.AssociationIdType == null)
					{
						this.ExecuteForAllAssociations(task, associationAdaptor, logger);
					}
					else
					{
						this.ExecuteForSingleAssociation(task, associationAdaptor, logger);
					}
				}
			}
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x0009CCF8 File Offset: 0x0009AEF8
		private void ExecuteForSingleAssociation(Action<MailboxAssociationFromStore, IAssociationAdaptor, ADUser, IExtensibleLogger> task, BaseAssociationAdaptor associationAdaptor, IExtensibleLogger logger)
		{
			MailboxAssociation mailboxAssociation;
			if (this.associationId.AssociationIdType == MailboxAssociationIdParameter.IdTypeItemId)
			{
				mailboxAssociation = this.GetAssociationByItemId(associationAdaptor, this.associationId.AssociationIdValue);
			}
			else
			{
				mailboxAssociation = this.GetAssociationByLocator(associationAdaptor);
			}
			MailboxAssociationContext.Tracer.TraceDebug<string, MailboxAssociation>((long)this.GetHashCode(), "MailboxAssociationContext.ExecuteForSingleAssociation [{0}]: Found association {1}", this.cmdletName, mailboxAssociation);
			task(mailboxAssociation as MailboxAssociationFromStore, associationAdaptor, this.mailbox, logger);
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x0009CD6C File Offset: 0x0009AF6C
		private MailboxAssociation GetAssociationByLocator(BaseAssociationAdaptor associationAdaptor)
		{
			IMailboxLocator mailboxLocator = this.groupMailboxAccessLayerFactory.CreateSlaveLocator(this.associationId);
			MailboxAssociationContext.Tracer.TraceDebug<string, IMailboxLocator>((long)this.GetHashCode(), "MailboxAssociationContext.GetAssociationByLocator [{0}]: Querying association with locator {1}", this.cmdletName, mailboxLocator);
			return associationAdaptor.GetAssociation(mailboxLocator);
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x0009CDB4 File Offset: 0x0009AFB4
		private MailboxAssociation GetAssociationByItemId(BaseAssociationAdaptor associationAdaptor, string base64ItemId)
		{
			MailboxAssociationContext.Tracer.TraceDebug<string>((long)this.GetHashCode(), "MailboxAssociationContext.GetAssociationByItemId [{0}]: Querying association by item id parameter", this.cmdletName);
			StoreObjectId itemId = StoreObjectId.Deserialize(base64ItemId);
			VersionedId itemId2 = new VersionedId(itemId, new byte[0]);
			return associationAdaptor.GetAssociation(itemId2);
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x0009CDFC File Offset: 0x0009AFFC
		private MailboxAssociation GetAssociationByItemId(BaseAssociationAdaptor associationAdaptor, MailboxAssociation association)
		{
			MailboxAssociationContext.Tracer.TraceDebug<string>((long)this.GetHashCode(), "MailboxAssociationContext.GetAssociationByItemId [{0}]: Querying association by its item id", this.cmdletName);
			MailboxAssociationFromStore mailboxAssociationFromStore = association as MailboxAssociationFromStore;
			if (mailboxAssociationFromStore != null)
			{
				association = associationAdaptor.GetAssociation(mailboxAssociationFromStore.ItemId);
			}
			return association;
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x0009CE40 File Offset: 0x0009B040
		private void ExecuteForAllAssociations(Action<MailboxAssociationFromStore, IAssociationAdaptor, ADUser, IExtensibleLogger> task, BaseAssociationAdaptor associationAdaptor, IExtensibleLogger logger)
		{
			MailboxAssociationContext.Tracer.TraceDebug<string>((long)this.GetHashCode(), "MailboxAssociationContext.ExecuteForAllAssociations [{0}]: Querying all associations for the mailbox.", this.cmdletName);
			IEnumerable<MailboxAssociation> allAssociations = associationAdaptor.GetAllAssociations();
			foreach (MailboxAssociation mailboxAssociation in allAssociations)
			{
				MailboxAssociation mailboxAssociation2 = mailboxAssociation;
				if (this.includeNotPromotedProperties)
				{
					MailboxAssociationContext.Tracer.TraceDebug<string>((long)this.GetHashCode(), "MailboxAssociationContext.ExecuteForAllAssociations [{0}]: Querying association by ItemId to retrieve not promoted properties.", this.cmdletName);
					mailboxAssociation2 = this.GetAssociationByItemId(associationAdaptor, mailboxAssociation);
				}
				MailboxAssociationContext.Tracer.TraceDebug<string, MailboxAssociation>((long)this.GetHashCode(), "MailboxAssociationContext.ExecuteForAllAssociations [{0}]: Found association {1}", this.cmdletName, mailboxAssociation);
				task(mailboxAssociation2 as MailboxAssociationFromStore, associationAdaptor, this.mailbox, logger);
			}
		}

		// Token: 0x04001DAA RID: 7594
		private static readonly Trace Tracer = ExTraceGlobals.CmdletsTracer;

		// Token: 0x04001DAB RID: 7595
		private readonly IRecipientSession adSession;

		// Token: 0x04001DAC RID: 7596
		private readonly ADUser mailbox;

		// Token: 0x04001DAD RID: 7597
		private readonly MailboxAssociationIdParameter associationId;

		// Token: 0x04001DAE RID: 7598
		private readonly GroupMailboxAccessLayerEntityFactory groupMailboxAccessLayerFactory;

		// Token: 0x04001DAF RID: 7599
		private readonly string clientInfoString;

		// Token: 0x04001DB0 RID: 7600
		private readonly string cmdletName;

		// Token: 0x04001DB1 RID: 7601
		private readonly bool includeNotPromotedProperties;
	}
}
