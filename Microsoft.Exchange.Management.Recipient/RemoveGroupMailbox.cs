using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000055 RID: 85
	[Cmdlet("Remove", "GroupMailbox", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveGroupMailbox : RemoveMailboxBase<MailboxIdParameter>
	{
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0001799B File Offset: 0x00015B9B
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x000179B2 File Offset: 0x00015BB2
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		[ValidateNotNullOrEmpty]
		public RecipientIdParameter ExecutingUser
		{
			get
			{
				return (RecipientIdParameter)base.Fields["ExecutingUser"];
			}
			set
			{
				base.Fields["ExecutingUser"] = value;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x000179C5 File Offset: 0x00015BC5
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x000179DC File Offset: 0x00015BDC
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public RecipientIdType RecipientIdType
		{
			get
			{
				return (RecipientIdType)base.Fields["RecipientIdType"];
			}
			set
			{
				base.Fields["RecipientIdType"] = value;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x000179F4 File Offset: 0x00015BF4
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x00017A1A File Offset: 0x00015C1A
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public SwitchParameter FromSyncClient
		{
			get
			{
				return (SwitchParameter)(base.Fields["FromSyncClient"] ?? false);
			}
			set
			{
				base.Fields["FromSyncClient"] = value;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x00017A32 File Offset: 0x00015C32
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x00017A3A File Offset: 0x00015C3A
		[Parameter(Mandatory = false)]
		public new SwitchParameter ForReconciliation
		{
			get
			{
				return base.ForReconciliation;
			}
			set
			{
				base.ForReconciliation = value;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x00017A43 File Offset: 0x00015C43
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x00017A4B File Offset: 0x00015C4B
		private new SwitchParameter Arbitration { get; set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x00017A54 File Offset: 0x00015C54
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x00017A5C File Offset: 0x00015C5C
		private new DatabaseIdParameter Database { get; set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x00017A65 File Offset: 0x00015C65
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x00017A6D File Offset: 0x00015C6D
		private new SwitchParameter Disconnect { get; set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x00017A76 File Offset: 0x00015C76
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x00017A7E File Offset: 0x00015C7E
		private new SwitchParameter IgnoreDefaultScope { get; set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x00017A87 File Offset: 0x00015C87
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x00017A8F File Offset: 0x00015C8F
		private new SwitchParameter IgnoreLegalHold { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x00017A98 File Offset: 0x00015C98
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x00017AA0 File Offset: 0x00015CA0
		private new SwitchParameter KeepWindowsLiveID { get; set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x00017AA9 File Offset: 0x00015CA9
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x00017AB1 File Offset: 0x00015CB1
		private new SwitchParameter PublicFolder { get; set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x00017ABA File Offset: 0x00015CBA
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x00017AC2 File Offset: 0x00015CC2
		private new SwitchParameter RemoveLastArbitrationMailboxAllowed { get; set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x00017ACB File Offset: 0x00015CCB
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x00017AD3 File Offset: 0x00015CD3
		private new StoreMailboxIdParameter StoreMailboxIdentity { get; set; }

		// Token: 0x06000549 RID: 1353 RVA: 0x00017ADC File Offset: 0x00015CDC
		protected override IConfigurable ResolveDataObject()
		{
			ADRecipient adrecipient = (ADRecipient)base.ResolveDataObject();
			if (adrecipient.RecipientTypeDetails != RecipientTypeDetails.GroupMailbox)
			{
				base.WriteError(new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound(this.Identity.ToString(), typeof(ADUser).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), ExchangeErrorCategory.Client, this.Identity);
			}
			return adrecipient;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00017B53 File Offset: 0x00015D53
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new RemoveGroupMailboxTaskModuleFactory();
		}

		// Token: 0x0400015B RID: 347
		private const string ParameterExecutingUser = "ExecutingUser";

		// Token: 0x0400015C RID: 348
		private const string ParameterRecipientIdType = "RecipientIdType";

		// Token: 0x0400015D RID: 349
		private const string ParameterFromSyncClient = "FromSyncClient";
	}
}
