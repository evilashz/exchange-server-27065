using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000BD9 RID: 3033
	public class SetCASMailboxPlanCommand : SyntheticCommandWithPipelineInputNoOutput<CASMailboxPlan>
	{
		// Token: 0x06009293 RID: 37523 RVA: 0x000D5FEE File Offset: 0x000D41EE
		private SetCASMailboxPlanCommand() : base("Set-CASMailboxPlan")
		{
		}

		// Token: 0x06009294 RID: 37524 RVA: 0x000D5FFB File Offset: 0x000D41FB
		public SetCASMailboxPlanCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009295 RID: 37525 RVA: 0x000D600A File Offset: 0x000D420A
		public virtual SetCASMailboxPlanCommand SetParameters(SetCASMailboxPlanCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009296 RID: 37526 RVA: 0x000D6014 File Offset: 0x000D4214
		public virtual SetCASMailboxPlanCommand SetParameters(SetCASMailboxPlanCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BDA RID: 3034
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700669A RID: 26266
			// (set) Token: 0x06009297 RID: 37527 RVA: 0x000D601E File Offset: 0x000D421E
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700669B RID: 26267
			// (set) Token: 0x06009298 RID: 37528 RVA: 0x000D603C File Offset: 0x000D423C
			public virtual string OwaMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["OwaMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700669C RID: 26268
			// (set) Token: 0x06009299 RID: 37529 RVA: 0x000D605A File Offset: 0x000D425A
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700669D RID: 26269
			// (set) Token: 0x0600929A RID: 37530 RVA: 0x000D6072 File Offset: 0x000D4272
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700669E RID: 26270
			// (set) Token: 0x0600929B RID: 37531 RVA: 0x000D6085 File Offset: 0x000D4285
			public virtual bool ActiveSyncDebugLogging
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncDebugLogging"] = value;
				}
			}

			// Token: 0x1700669F RID: 26271
			// (set) Token: 0x0600929C RID: 37532 RVA: 0x000D609D File Offset: 0x000D429D
			public virtual bool ActiveSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncEnabled"] = value;
				}
			}

			// Token: 0x170066A0 RID: 26272
			// (set) Token: 0x0600929D RID: 37533 RVA: 0x000D60B5 File Offset: 0x000D42B5
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170066A1 RID: 26273
			// (set) Token: 0x0600929E RID: 37534 RVA: 0x000D60C8 File Offset: 0x000D42C8
			public virtual bool ECPEnabled
			{
				set
				{
					base.PowerSharpParameters["ECPEnabled"] = value;
				}
			}

			// Token: 0x170066A2 RID: 26274
			// (set) Token: 0x0600929F RID: 37535 RVA: 0x000D60E0 File Offset: 0x000D42E0
			public virtual bool ImapEnabled
			{
				set
				{
					base.PowerSharpParameters["ImapEnabled"] = value;
				}
			}

			// Token: 0x170066A3 RID: 26275
			// (set) Token: 0x060092A0 RID: 37536 RVA: 0x000D60F8 File Offset: 0x000D42F8
			public virtual bool ImapUseProtocolDefaults
			{
				set
				{
					base.PowerSharpParameters["ImapUseProtocolDefaults"] = value;
				}
			}

			// Token: 0x170066A4 RID: 26276
			// (set) Token: 0x060092A1 RID: 37537 RVA: 0x000D6110 File Offset: 0x000D4310
			public virtual MimeTextFormat ImapMessagesRetrievalMimeFormat
			{
				set
				{
					base.PowerSharpParameters["ImapMessagesRetrievalMimeFormat"] = value;
				}
			}

			// Token: 0x170066A5 RID: 26277
			// (set) Token: 0x060092A2 RID: 37538 RVA: 0x000D6128 File Offset: 0x000D4328
			public virtual bool ImapEnableExactRFC822Size
			{
				set
				{
					base.PowerSharpParameters["ImapEnableExactRFC822Size"] = value;
				}
			}

			// Token: 0x170066A6 RID: 26278
			// (set) Token: 0x060092A3 RID: 37539 RVA: 0x000D6140 File Offset: 0x000D4340
			public virtual bool ImapProtocolLoggingEnabled
			{
				set
				{
					base.PowerSharpParameters["ImapProtocolLoggingEnabled"] = value;
				}
			}

			// Token: 0x170066A7 RID: 26279
			// (set) Token: 0x060092A4 RID: 37540 RVA: 0x000D6158 File Offset: 0x000D4358
			public virtual bool ImapSuppressReadReceipt
			{
				set
				{
					base.PowerSharpParameters["ImapSuppressReadReceipt"] = value;
				}
			}

			// Token: 0x170066A8 RID: 26280
			// (set) Token: 0x060092A5 RID: 37541 RVA: 0x000D6170 File Offset: 0x000D4370
			public virtual bool ImapForceICalForCalendarRetrievalOption
			{
				set
				{
					base.PowerSharpParameters["ImapForceICalForCalendarRetrievalOption"] = value;
				}
			}

			// Token: 0x170066A9 RID: 26281
			// (set) Token: 0x060092A6 RID: 37542 RVA: 0x000D6188 File Offset: 0x000D4388
			public virtual bool MAPIEnabled
			{
				set
				{
					base.PowerSharpParameters["MAPIEnabled"] = value;
				}
			}

			// Token: 0x170066AA RID: 26282
			// (set) Token: 0x060092A7 RID: 37543 RVA: 0x000D61A0 File Offset: 0x000D43A0
			public virtual bool? MapiHttpEnabled
			{
				set
				{
					base.PowerSharpParameters["MapiHttpEnabled"] = value;
				}
			}

			// Token: 0x170066AB RID: 26283
			// (set) Token: 0x060092A8 RID: 37544 RVA: 0x000D61B8 File Offset: 0x000D43B8
			public virtual bool MAPIBlockOutlookNonCachedMode
			{
				set
				{
					base.PowerSharpParameters["MAPIBlockOutlookNonCachedMode"] = value;
				}
			}

			// Token: 0x170066AC RID: 26284
			// (set) Token: 0x060092A9 RID: 37545 RVA: 0x000D61D0 File Offset: 0x000D43D0
			public virtual string MAPIBlockOutlookVersions
			{
				set
				{
					base.PowerSharpParameters["MAPIBlockOutlookVersions"] = value;
				}
			}

			// Token: 0x170066AD RID: 26285
			// (set) Token: 0x060092AA RID: 37546 RVA: 0x000D61E3 File Offset: 0x000D43E3
			public virtual bool MAPIBlockOutlookRpcHttp
			{
				set
				{
					base.PowerSharpParameters["MAPIBlockOutlookRpcHttp"] = value;
				}
			}

			// Token: 0x170066AE RID: 26286
			// (set) Token: 0x060092AB RID: 37547 RVA: 0x000D61FB File Offset: 0x000D43FB
			public virtual bool MAPIBlockOutlookExternalConnectivity
			{
				set
				{
					base.PowerSharpParameters["MAPIBlockOutlookExternalConnectivity"] = value;
				}
			}

			// Token: 0x170066AF RID: 26287
			// (set) Token: 0x060092AC RID: 37548 RVA: 0x000D6213 File Offset: 0x000D4413
			public virtual bool OWAEnabled
			{
				set
				{
					base.PowerSharpParameters["OWAEnabled"] = value;
				}
			}

			// Token: 0x170066B0 RID: 26288
			// (set) Token: 0x060092AD RID: 37549 RVA: 0x000D622B File Offset: 0x000D442B
			public virtual bool OWAforDevicesEnabled
			{
				set
				{
					base.PowerSharpParameters["OWAforDevicesEnabled"] = value;
				}
			}

			// Token: 0x170066B1 RID: 26289
			// (set) Token: 0x060092AE RID: 37550 RVA: 0x000D6243 File Offset: 0x000D4443
			public virtual bool PopEnabled
			{
				set
				{
					base.PowerSharpParameters["PopEnabled"] = value;
				}
			}

			// Token: 0x170066B2 RID: 26290
			// (set) Token: 0x060092AF RID: 37551 RVA: 0x000D625B File Offset: 0x000D445B
			public virtual bool PopUseProtocolDefaults
			{
				set
				{
					base.PowerSharpParameters["PopUseProtocolDefaults"] = value;
				}
			}

			// Token: 0x170066B3 RID: 26291
			// (set) Token: 0x060092B0 RID: 37552 RVA: 0x000D6273 File Offset: 0x000D4473
			public virtual MimeTextFormat PopMessagesRetrievalMimeFormat
			{
				set
				{
					base.PowerSharpParameters["PopMessagesRetrievalMimeFormat"] = value;
				}
			}

			// Token: 0x170066B4 RID: 26292
			// (set) Token: 0x060092B1 RID: 37553 RVA: 0x000D628B File Offset: 0x000D448B
			public virtual bool PopEnableExactRFC822Size
			{
				set
				{
					base.PowerSharpParameters["PopEnableExactRFC822Size"] = value;
				}
			}

			// Token: 0x170066B5 RID: 26293
			// (set) Token: 0x060092B2 RID: 37554 RVA: 0x000D62A3 File Offset: 0x000D44A3
			public virtual bool PopProtocolLoggingEnabled
			{
				set
				{
					base.PowerSharpParameters["PopProtocolLoggingEnabled"] = value;
				}
			}

			// Token: 0x170066B6 RID: 26294
			// (set) Token: 0x060092B3 RID: 37555 RVA: 0x000D62BB File Offset: 0x000D44BB
			public virtual bool PopSuppressReadReceipt
			{
				set
				{
					base.PowerSharpParameters["PopSuppressReadReceipt"] = value;
				}
			}

			// Token: 0x170066B7 RID: 26295
			// (set) Token: 0x060092B4 RID: 37556 RVA: 0x000D62D3 File Offset: 0x000D44D3
			public virtual bool PopForceICalForCalendarRetrievalOption
			{
				set
				{
					base.PowerSharpParameters["PopForceICalForCalendarRetrievalOption"] = value;
				}
			}

			// Token: 0x170066B8 RID: 26296
			// (set) Token: 0x060092B5 RID: 37557 RVA: 0x000D62EB File Offset: 0x000D44EB
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x170066B9 RID: 26297
			// (set) Token: 0x060092B6 RID: 37558 RVA: 0x000D6303 File Offset: 0x000D4503
			public virtual bool? EwsEnabled
			{
				set
				{
					base.PowerSharpParameters["EwsEnabled"] = value;
				}
			}

			// Token: 0x170066BA RID: 26298
			// (set) Token: 0x060092B7 RID: 37559 RVA: 0x000D631B File Offset: 0x000D451B
			public virtual bool? EwsAllowOutlook
			{
				set
				{
					base.PowerSharpParameters["EwsAllowOutlook"] = value;
				}
			}

			// Token: 0x170066BB RID: 26299
			// (set) Token: 0x060092B8 RID: 37560 RVA: 0x000D6333 File Offset: 0x000D4533
			public virtual bool? EwsAllowMacOutlook
			{
				set
				{
					base.PowerSharpParameters["EwsAllowMacOutlook"] = value;
				}
			}

			// Token: 0x170066BC RID: 26300
			// (set) Token: 0x060092B9 RID: 37561 RVA: 0x000D634B File Offset: 0x000D454B
			public virtual bool? EwsAllowEntourage
			{
				set
				{
					base.PowerSharpParameters["EwsAllowEntourage"] = value;
				}
			}

			// Token: 0x170066BD RID: 26301
			// (set) Token: 0x060092BA RID: 37562 RVA: 0x000D6363 File Offset: 0x000D4563
			public virtual EwsApplicationAccessPolicy? EwsApplicationAccessPolicy
			{
				set
				{
					base.PowerSharpParameters["EwsApplicationAccessPolicy"] = value;
				}
			}

			// Token: 0x170066BE RID: 26302
			// (set) Token: 0x060092BB RID: 37563 RVA: 0x000D637B File Offset: 0x000D457B
			public virtual MultiValuedProperty<string> EwsAllowList
			{
				set
				{
					base.PowerSharpParameters["EwsAllowList"] = value;
				}
			}

			// Token: 0x170066BF RID: 26303
			// (set) Token: 0x060092BC RID: 37564 RVA: 0x000D638E File Offset: 0x000D458E
			public virtual MultiValuedProperty<string> EwsBlockList
			{
				set
				{
					base.PowerSharpParameters["EwsBlockList"] = value;
				}
			}

			// Token: 0x170066C0 RID: 26304
			// (set) Token: 0x060092BD RID: 37565 RVA: 0x000D63A1 File Offset: 0x000D45A1
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170066C1 RID: 26305
			// (set) Token: 0x060092BE RID: 37566 RVA: 0x000D63B4 File Offset: 0x000D45B4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170066C2 RID: 26306
			// (set) Token: 0x060092BF RID: 37567 RVA: 0x000D63CC File Offset: 0x000D45CC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170066C3 RID: 26307
			// (set) Token: 0x060092C0 RID: 37568 RVA: 0x000D63E4 File Offset: 0x000D45E4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170066C4 RID: 26308
			// (set) Token: 0x060092C1 RID: 37569 RVA: 0x000D63FC File Offset: 0x000D45FC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170066C5 RID: 26309
			// (set) Token: 0x060092C2 RID: 37570 RVA: 0x000D6414 File Offset: 0x000D4614
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000BDB RID: 3035
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170066C6 RID: 26310
			// (set) Token: 0x060092C4 RID: 37572 RVA: 0x000D6434 File Offset: 0x000D4634
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x170066C7 RID: 26311
			// (set) Token: 0x060092C5 RID: 37573 RVA: 0x000D6452 File Offset: 0x000D4652
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170066C8 RID: 26312
			// (set) Token: 0x060092C6 RID: 37574 RVA: 0x000D6470 File Offset: 0x000D4670
			public virtual string OwaMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["OwaMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170066C9 RID: 26313
			// (set) Token: 0x060092C7 RID: 37575 RVA: 0x000D648E File Offset: 0x000D468E
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170066CA RID: 26314
			// (set) Token: 0x060092C8 RID: 37576 RVA: 0x000D64A6 File Offset: 0x000D46A6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170066CB RID: 26315
			// (set) Token: 0x060092C9 RID: 37577 RVA: 0x000D64B9 File Offset: 0x000D46B9
			public virtual bool ActiveSyncDebugLogging
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncDebugLogging"] = value;
				}
			}

			// Token: 0x170066CC RID: 26316
			// (set) Token: 0x060092CA RID: 37578 RVA: 0x000D64D1 File Offset: 0x000D46D1
			public virtual bool ActiveSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncEnabled"] = value;
				}
			}

			// Token: 0x170066CD RID: 26317
			// (set) Token: 0x060092CB RID: 37579 RVA: 0x000D64E9 File Offset: 0x000D46E9
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170066CE RID: 26318
			// (set) Token: 0x060092CC RID: 37580 RVA: 0x000D64FC File Offset: 0x000D46FC
			public virtual bool ECPEnabled
			{
				set
				{
					base.PowerSharpParameters["ECPEnabled"] = value;
				}
			}

			// Token: 0x170066CF RID: 26319
			// (set) Token: 0x060092CD RID: 37581 RVA: 0x000D6514 File Offset: 0x000D4714
			public virtual bool ImapEnabled
			{
				set
				{
					base.PowerSharpParameters["ImapEnabled"] = value;
				}
			}

			// Token: 0x170066D0 RID: 26320
			// (set) Token: 0x060092CE RID: 37582 RVA: 0x000D652C File Offset: 0x000D472C
			public virtual bool ImapUseProtocolDefaults
			{
				set
				{
					base.PowerSharpParameters["ImapUseProtocolDefaults"] = value;
				}
			}

			// Token: 0x170066D1 RID: 26321
			// (set) Token: 0x060092CF RID: 37583 RVA: 0x000D6544 File Offset: 0x000D4744
			public virtual MimeTextFormat ImapMessagesRetrievalMimeFormat
			{
				set
				{
					base.PowerSharpParameters["ImapMessagesRetrievalMimeFormat"] = value;
				}
			}

			// Token: 0x170066D2 RID: 26322
			// (set) Token: 0x060092D0 RID: 37584 RVA: 0x000D655C File Offset: 0x000D475C
			public virtual bool ImapEnableExactRFC822Size
			{
				set
				{
					base.PowerSharpParameters["ImapEnableExactRFC822Size"] = value;
				}
			}

			// Token: 0x170066D3 RID: 26323
			// (set) Token: 0x060092D1 RID: 37585 RVA: 0x000D6574 File Offset: 0x000D4774
			public virtual bool ImapProtocolLoggingEnabled
			{
				set
				{
					base.PowerSharpParameters["ImapProtocolLoggingEnabled"] = value;
				}
			}

			// Token: 0x170066D4 RID: 26324
			// (set) Token: 0x060092D2 RID: 37586 RVA: 0x000D658C File Offset: 0x000D478C
			public virtual bool ImapSuppressReadReceipt
			{
				set
				{
					base.PowerSharpParameters["ImapSuppressReadReceipt"] = value;
				}
			}

			// Token: 0x170066D5 RID: 26325
			// (set) Token: 0x060092D3 RID: 37587 RVA: 0x000D65A4 File Offset: 0x000D47A4
			public virtual bool ImapForceICalForCalendarRetrievalOption
			{
				set
				{
					base.PowerSharpParameters["ImapForceICalForCalendarRetrievalOption"] = value;
				}
			}

			// Token: 0x170066D6 RID: 26326
			// (set) Token: 0x060092D4 RID: 37588 RVA: 0x000D65BC File Offset: 0x000D47BC
			public virtual bool MAPIEnabled
			{
				set
				{
					base.PowerSharpParameters["MAPIEnabled"] = value;
				}
			}

			// Token: 0x170066D7 RID: 26327
			// (set) Token: 0x060092D5 RID: 37589 RVA: 0x000D65D4 File Offset: 0x000D47D4
			public virtual bool? MapiHttpEnabled
			{
				set
				{
					base.PowerSharpParameters["MapiHttpEnabled"] = value;
				}
			}

			// Token: 0x170066D8 RID: 26328
			// (set) Token: 0x060092D6 RID: 37590 RVA: 0x000D65EC File Offset: 0x000D47EC
			public virtual bool MAPIBlockOutlookNonCachedMode
			{
				set
				{
					base.PowerSharpParameters["MAPIBlockOutlookNonCachedMode"] = value;
				}
			}

			// Token: 0x170066D9 RID: 26329
			// (set) Token: 0x060092D7 RID: 37591 RVA: 0x000D6604 File Offset: 0x000D4804
			public virtual string MAPIBlockOutlookVersions
			{
				set
				{
					base.PowerSharpParameters["MAPIBlockOutlookVersions"] = value;
				}
			}

			// Token: 0x170066DA RID: 26330
			// (set) Token: 0x060092D8 RID: 37592 RVA: 0x000D6617 File Offset: 0x000D4817
			public virtual bool MAPIBlockOutlookRpcHttp
			{
				set
				{
					base.PowerSharpParameters["MAPIBlockOutlookRpcHttp"] = value;
				}
			}

			// Token: 0x170066DB RID: 26331
			// (set) Token: 0x060092D9 RID: 37593 RVA: 0x000D662F File Offset: 0x000D482F
			public virtual bool MAPIBlockOutlookExternalConnectivity
			{
				set
				{
					base.PowerSharpParameters["MAPIBlockOutlookExternalConnectivity"] = value;
				}
			}

			// Token: 0x170066DC RID: 26332
			// (set) Token: 0x060092DA RID: 37594 RVA: 0x000D6647 File Offset: 0x000D4847
			public virtual bool OWAEnabled
			{
				set
				{
					base.PowerSharpParameters["OWAEnabled"] = value;
				}
			}

			// Token: 0x170066DD RID: 26333
			// (set) Token: 0x060092DB RID: 37595 RVA: 0x000D665F File Offset: 0x000D485F
			public virtual bool OWAforDevicesEnabled
			{
				set
				{
					base.PowerSharpParameters["OWAforDevicesEnabled"] = value;
				}
			}

			// Token: 0x170066DE RID: 26334
			// (set) Token: 0x060092DC RID: 37596 RVA: 0x000D6677 File Offset: 0x000D4877
			public virtual bool PopEnabled
			{
				set
				{
					base.PowerSharpParameters["PopEnabled"] = value;
				}
			}

			// Token: 0x170066DF RID: 26335
			// (set) Token: 0x060092DD RID: 37597 RVA: 0x000D668F File Offset: 0x000D488F
			public virtual bool PopUseProtocolDefaults
			{
				set
				{
					base.PowerSharpParameters["PopUseProtocolDefaults"] = value;
				}
			}

			// Token: 0x170066E0 RID: 26336
			// (set) Token: 0x060092DE RID: 37598 RVA: 0x000D66A7 File Offset: 0x000D48A7
			public virtual MimeTextFormat PopMessagesRetrievalMimeFormat
			{
				set
				{
					base.PowerSharpParameters["PopMessagesRetrievalMimeFormat"] = value;
				}
			}

			// Token: 0x170066E1 RID: 26337
			// (set) Token: 0x060092DF RID: 37599 RVA: 0x000D66BF File Offset: 0x000D48BF
			public virtual bool PopEnableExactRFC822Size
			{
				set
				{
					base.PowerSharpParameters["PopEnableExactRFC822Size"] = value;
				}
			}

			// Token: 0x170066E2 RID: 26338
			// (set) Token: 0x060092E0 RID: 37600 RVA: 0x000D66D7 File Offset: 0x000D48D7
			public virtual bool PopProtocolLoggingEnabled
			{
				set
				{
					base.PowerSharpParameters["PopProtocolLoggingEnabled"] = value;
				}
			}

			// Token: 0x170066E3 RID: 26339
			// (set) Token: 0x060092E1 RID: 37601 RVA: 0x000D66EF File Offset: 0x000D48EF
			public virtual bool PopSuppressReadReceipt
			{
				set
				{
					base.PowerSharpParameters["PopSuppressReadReceipt"] = value;
				}
			}

			// Token: 0x170066E4 RID: 26340
			// (set) Token: 0x060092E2 RID: 37602 RVA: 0x000D6707 File Offset: 0x000D4907
			public virtual bool PopForceICalForCalendarRetrievalOption
			{
				set
				{
					base.PowerSharpParameters["PopForceICalForCalendarRetrievalOption"] = value;
				}
			}

			// Token: 0x170066E5 RID: 26341
			// (set) Token: 0x060092E3 RID: 37603 RVA: 0x000D671F File Offset: 0x000D491F
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x170066E6 RID: 26342
			// (set) Token: 0x060092E4 RID: 37604 RVA: 0x000D6737 File Offset: 0x000D4937
			public virtual bool? EwsEnabled
			{
				set
				{
					base.PowerSharpParameters["EwsEnabled"] = value;
				}
			}

			// Token: 0x170066E7 RID: 26343
			// (set) Token: 0x060092E5 RID: 37605 RVA: 0x000D674F File Offset: 0x000D494F
			public virtual bool? EwsAllowOutlook
			{
				set
				{
					base.PowerSharpParameters["EwsAllowOutlook"] = value;
				}
			}

			// Token: 0x170066E8 RID: 26344
			// (set) Token: 0x060092E6 RID: 37606 RVA: 0x000D6767 File Offset: 0x000D4967
			public virtual bool? EwsAllowMacOutlook
			{
				set
				{
					base.PowerSharpParameters["EwsAllowMacOutlook"] = value;
				}
			}

			// Token: 0x170066E9 RID: 26345
			// (set) Token: 0x060092E7 RID: 37607 RVA: 0x000D677F File Offset: 0x000D497F
			public virtual bool? EwsAllowEntourage
			{
				set
				{
					base.PowerSharpParameters["EwsAllowEntourage"] = value;
				}
			}

			// Token: 0x170066EA RID: 26346
			// (set) Token: 0x060092E8 RID: 37608 RVA: 0x000D6797 File Offset: 0x000D4997
			public virtual EwsApplicationAccessPolicy? EwsApplicationAccessPolicy
			{
				set
				{
					base.PowerSharpParameters["EwsApplicationAccessPolicy"] = value;
				}
			}

			// Token: 0x170066EB RID: 26347
			// (set) Token: 0x060092E9 RID: 37609 RVA: 0x000D67AF File Offset: 0x000D49AF
			public virtual MultiValuedProperty<string> EwsAllowList
			{
				set
				{
					base.PowerSharpParameters["EwsAllowList"] = value;
				}
			}

			// Token: 0x170066EC RID: 26348
			// (set) Token: 0x060092EA RID: 37610 RVA: 0x000D67C2 File Offset: 0x000D49C2
			public virtual MultiValuedProperty<string> EwsBlockList
			{
				set
				{
					base.PowerSharpParameters["EwsBlockList"] = value;
				}
			}

			// Token: 0x170066ED RID: 26349
			// (set) Token: 0x060092EB RID: 37611 RVA: 0x000D67D5 File Offset: 0x000D49D5
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170066EE RID: 26350
			// (set) Token: 0x060092EC RID: 37612 RVA: 0x000D67E8 File Offset: 0x000D49E8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170066EF RID: 26351
			// (set) Token: 0x060092ED RID: 37613 RVA: 0x000D6800 File Offset: 0x000D4A00
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170066F0 RID: 26352
			// (set) Token: 0x060092EE RID: 37614 RVA: 0x000D6818 File Offset: 0x000D4A18
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170066F1 RID: 26353
			// (set) Token: 0x060092EF RID: 37615 RVA: 0x000D6830 File Offset: 0x000D4A30
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170066F2 RID: 26354
			// (set) Token: 0x060092F0 RID: 37616 RVA: 0x000D6848 File Offset: 0x000D4A48
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
