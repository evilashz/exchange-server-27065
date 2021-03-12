using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007A0 RID: 1952
	[Cmdlet("Export", "MailboxDiagnosticLogs", DefaultParameterSetName = "MailboxLogParameterSet", SupportsShouldProcess = true)]
	public sealed class ExportMailboxDiagnosticLogs : GetXsoObjectWithIdentityTaskBase<MailboxDiagnosticLogs, ADRecipient>
	{
		// Token: 0x170014CE RID: 5326
		// (get) Token: 0x060044C4 RID: 17604 RVA: 0x0011A7C3 File Offset: 0x001189C3
		// (set) Token: 0x060044C5 RID: 17605 RVA: 0x0011A7D0 File Offset: 0x001189D0
		[Parameter(Mandatory = true, ParameterSetName = "MailboxLogParameterSet", ValueFromPipelineByPropertyName = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "ExtendedPropertiesParameterSet", ValueFromPipelineByPropertyName = true, Position = 0)]
		public new MailUserOrGeneralMailboxIdParameter Identity
		{
			get
			{
				return (MailUserOrGeneralMailboxIdParameter)base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x170014CF RID: 5327
		// (get) Token: 0x060044C6 RID: 17606 RVA: 0x0011A7D9 File Offset: 0x001189D9
		// (set) Token: 0x060044C7 RID: 17607 RVA: 0x0011A7F9 File Offset: 0x001189F9
		[Parameter(Mandatory = true, ParameterSetName = "MailboxLogParameterSet")]
		public string ComponentName
		{
			get
			{
				return (string)(base.Fields["ComponentName"] ?? string.Empty);
			}
			set
			{
				base.Fields["ComponentName"] = value;
			}
		}

		// Token: 0x170014D0 RID: 5328
		// (get) Token: 0x060044C8 RID: 17608 RVA: 0x0011A80C File Offset: 0x00118A0C
		// (set) Token: 0x060044C9 RID: 17609 RVA: 0x0011A832 File Offset: 0x00118A32
		[Parameter(Mandatory = true, ParameterSetName = "ExtendedPropertiesParameterSet")]
		public SwitchParameter ExtendedProperties
		{
			get
			{
				return (SwitchParameter)(base.Fields["ExtendedProperties"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ExtendedProperties"] = value;
			}
		}

		// Token: 0x170014D1 RID: 5329
		// (get) Token: 0x060044CA RID: 17610 RVA: 0x0011A84A File Offset: 0x00118A4A
		// (set) Token: 0x060044CB RID: 17611 RVA: 0x0011A870 File Offset: 0x00118A70
		[Parameter(Mandatory = false, ParameterSetName = "ExtendedPropertiesParameterSet")]
		[Parameter(Mandatory = false, ParameterSetName = "MailboxLogParameterSet")]
		public SwitchParameter Archive
		{
			get
			{
				return (SwitchParameter)(base.Fields["Archive"] ?? false);
			}
			set
			{
				base.Fields["Archive"] = value;
			}
		}

		// Token: 0x170014D2 RID: 5330
		// (get) Token: 0x060044CC RID: 17612 RVA: 0x0011A888 File Offset: 0x00118A88
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationExportMailboxDiagnosticLogs;
			}
		}

		// Token: 0x170014D3 RID: 5331
		// (get) Token: 0x060044CD RID: 17613 RVA: 0x0011A890 File Offset: 0x00118A90
		protected override bool ShouldProcessArchive
		{
			get
			{
				return this.Archive.IsPresent;
			}
		}

		// Token: 0x060044CE RID: 17614 RVA: 0x0011A8B0 File Offset: 0x00118AB0
		internal override IConfigDataProvider CreateXsoMailboxDataProvider(ExchangePrincipal principal, ISecurityAccessToken userToken)
		{
			if (this.ExtendedProperties.IsPresent)
			{
				return new MailboxDiagnosticLogsDataProvider(principal, "ExportMailboxDiagnosticLogs");
			}
			return new MailboxDiagnosticLogsDataProvider(this.ComponentName, principal, "ExportMailboxDiagnosticLogs");
		}

		// Token: 0x060044CF RID: 17615 RVA: 0x0011A8EA File Offset: 0x00118AEA
		protected override bool IsKnownException(Exception exception)
		{
			return exception is ObjectNotFoundException || base.IsKnownException(exception);
		}

		// Token: 0x04002A81 RID: 10881
		public const string Name = "MailboxDiagnosticLogs";

		// Token: 0x04002A82 RID: 10882
		public const string MailboxLogParameterSet = "MailboxLogParameterSet";

		// Token: 0x04002A83 RID: 10883
		public const string ExtendedPropertiesParameterSet = "ExtendedPropertiesParameterSet";

		// Token: 0x04002A84 RID: 10884
		public const string Export = "Export";
	}
}
