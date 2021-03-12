using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.EdgeSync.Mserve;
using Microsoft.Exchange.EdgeSync.Validation.Mserv;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net.Mserve;

namespace Microsoft.Exchange.Management.EdgeSync
{
	// Token: 0x02000045 RID: 69
	[Cmdlet("Test", "EdgeSyncMserv", DefaultParameterSetName = "Health", SupportsShouldProcess = true)]
	public sealed class TestEdgeSyncMserv : TestEdgeSyncBase
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00009A34 File Offset: 0x00007C34
		// (set) Token: 0x0600022D RID: 557 RVA: 0x00009A4B File Offset: 0x00007C4B
		[Parameter(Mandatory = false, ParameterSetName = "ValidateAddress")]
		public SmtpAddress[] EmailAddresses
		{
			get
			{
				return (SmtpAddress[])base.Fields["EmailAddresses"];
			}
			set
			{
				base.Fields["EmailAddresses"] = value;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00009A5E File Offset: 0x00007C5E
		// (set) Token: 0x0600022F RID: 559 RVA: 0x00009A75 File Offset: 0x00007C75
		[Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = "ValidateAddress")]
		public MailboxIdParameter[] Mailboxes
		{
			get
			{
				return (MailboxIdParameter[])base.Fields["Mailboxes"];
			}
			set
			{
				base.Fields["Mailboxes"] = value;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00009A88 File Offset: 0x00007C88
		protected override string CmdletMonitoringEventSource
		{
			get
			{
				return "MSExchange Monitoring EdgeSyncMserv";
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00009A8F File Offset: 0x00007C8F
		protected override string Service
		{
			get
			{
				return "Hotmail MSERV";
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00009A96 File Offset: 0x00007C96
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageTestEdgeSyncMserv;
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00009AA0 File Offset: 0x00007CA0
		internal override bool ReadConnectorLeasePath(IConfigurationSession session, ADObjectId rootId, out string primaryLeasePath, out string backupLeasePath, out bool hasOneConnectorEnabledInCurrentForest)
		{
			string text;
			backupLeasePath = (text = null);
			primaryLeasePath = text;
			hasOneConnectorEnabledInCurrentForest = false;
			EdgeSyncMservConnector edgeSyncMservConnector = base.FindSiteEdgeSyncConnector<EdgeSyncMservConnector>(session, rootId, out hasOneConnectorEnabledInCurrentForest);
			if (edgeSyncMservConnector == null)
			{
				return false;
			}
			primaryLeasePath = Path.Combine(edgeSyncMservConnector.PrimaryLeaseLocation, "mserv.lease");
			backupLeasePath = Path.Combine(edgeSyncMservConnector.BackupLeaseLocation, "mserv.lease");
			return true;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00009AF0 File Offset: 0x00007CF0
		internal override ADObjectId GetCookieContainerId(IConfigurationSession session)
		{
			return MserveTargetConnection.GetCookieContainerId(session);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00009AF8 File Offset: 0x00007CF8
		protected override EnhancedTimeSpan GetSyncInterval(EdgeSyncServiceConfig config)
		{
			return config.RecipientSyncInterval;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00009B00 File Offset: 0x00007D00
		protected override void InternalProcessRecord()
		{
			try
			{
				if (this.EmailAddresses != null || this.Mailboxes != null)
				{
					if (this.EmailAddresses != null)
					{
						this.PerformLookup(base.DomainController, this.EmailAddresses);
					}
					if (this.Mailboxes != null)
					{
						this.PerformLookup(base.DomainController, this.Mailboxes);
					}
					return;
				}
			}
			catch (InvalidOperationException exception)
			{
				base.WriteError(exception, ErrorCategory.ReadError, null);
			}
			catch (MserveException exception2)
			{
				base.WriteError(exception2, ErrorCategory.ReadError, null);
			}
			catch (TransientException exception3)
			{
				base.WriteError(exception3, ErrorCategory.ReadError, null);
			}
			catch (ADOperationException exception4)
			{
				base.WriteError(exception4, ErrorCategory.ReadError, null);
			}
			base.TestGeneralSyncHealth();
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00009BD0 File Offset: 0x00007DD0
		private void PerformLookup(string domainController, SmtpAddress[] addresses)
		{
			List<string> list = new List<string>();
			for (int i = 0; i < addresses.Length; i++)
			{
				string item = (string)addresses[i];
				list.Add(item);
			}
			this.PerformLookup(domainController, list);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00009C14 File Offset: 0x00007E14
		private void PerformLookup(string domainController, MailboxIdParameter[] mailboxes)
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(domainController, true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 234, "PerformLookup", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\EdgeSync\\TestEdgeSyncMserv.cs");
			List<string> list = new List<string>();
			foreach (MailboxIdParameter mailboxIdParameter in mailboxes)
			{
				IEnumerable<ADRecipient> objects = mailboxIdParameter.GetObjects<ADRecipient>(null, tenantOrRootOrgRecipientSession);
				using (IEnumerator<ADRecipient> enumerator = objects.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						foreach (string item in enumerator.Current.EmailAddresses.ToStringArray())
						{
							list.Add(item);
						}
					}
				}
			}
			this.PerformLookup(domainController, list);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00009CDC File Offset: 0x00007EDC
		private void PerformLookup(string domainController, List<string> addresses)
		{
			MserveWebService mserveWebService = EdgeSyncMservConnector.CreateDefaultMserveWebService(domainController);
			if (mserveWebService == null)
			{
				throw new InvalidOperationException("Invalid MServ configuration.");
			}
			List<RecipientSyncOperation> list;
			foreach (string text in addresses)
			{
				RecipientSyncOperation recipientSyncOperation = new RecipientSyncOperation();
				if (text.StartsWith("smtp:", StringComparison.OrdinalIgnoreCase))
				{
					recipientSyncOperation.ReadEntries.Add(text.Substring(5));
				}
				else
				{
					recipientSyncOperation.ReadEntries.Add(text);
				}
				list = mserveWebService.Synchronize(recipientSyncOperation);
				foreach (RecipientSyncOperation recipientSyncOperation2 in list)
				{
					base.WriteObject(new MservRecipientRecord(recipientSyncOperation2.ReadEntries[0], recipientSyncOperation2.PartnerId));
				}
			}
			list = mserveWebService.Synchronize();
			foreach (RecipientSyncOperation recipientSyncOperation3 in list)
			{
				base.WriteObject(new MservRecipientRecord(recipientSyncOperation3.ReadEntries[0], recipientSyncOperation3.PartnerId));
			}
		}

		// Token: 0x040000CB RID: 203
		private const string CmdletNoun = "EdgeSyncMserv";

		// Token: 0x040000CC RID: 204
		private const string ServiceName = "Hotmail MSERV";
	}
}
