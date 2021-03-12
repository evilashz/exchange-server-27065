using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000092 RID: 146
	public class GetFailedContentIndexDocumentsCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x060018F4 RID: 6388 RVA: 0x00037FC8 File Offset: 0x000361C8
		private GetFailedContentIndexDocumentsCommand() : base("Get-FailedContentIndexDocuments")
		{
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x00037FD5 File Offset: 0x000361D5
		public GetFailedContentIndexDocumentsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x00037FE4 File Offset: 0x000361E4
		public virtual GetFailedContentIndexDocumentsCommand SetParameters(GetFailedContentIndexDocumentsCommand.mailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x00037FEE File Offset: 0x000361EE
		public virtual GetFailedContentIndexDocumentsCommand SetParameters(GetFailedContentIndexDocumentsCommand.serverParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x00037FF8 File Offset: 0x000361F8
		public virtual GetFailedContentIndexDocumentsCommand SetParameters(GetFailedContentIndexDocumentsCommand.databaseParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x00038002 File Offset: 0x00036202
		public virtual GetFailedContentIndexDocumentsCommand SetParameters(GetFailedContentIndexDocumentsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000093 RID: 147
		public class mailboxParameters : ParametersBase
		{
			// Token: 0x17000389 RID: 905
			// (set) Token: 0x060018FA RID: 6394 RVA: 0x0003800C File Offset: 0x0003620C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700038A RID: 906
			// (set) Token: 0x060018FB RID: 6395 RVA: 0x0003802A File Offset: 0x0003622A
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x1700038B RID: 907
			// (set) Token: 0x060018FC RID: 6396 RVA: 0x00038042 File Offset: 0x00036242
			public virtual FailureMode FailureMode
			{
				set
				{
					base.PowerSharpParameters["FailureMode"] = value;
				}
			}

			// Token: 0x1700038C RID: 908
			// (set) Token: 0x060018FD RID: 6397 RVA: 0x0003805A File Offset: 0x0003625A
			public virtual int? ErrorCode
			{
				set
				{
					base.PowerSharpParameters["ErrorCode"] = value;
				}
			}

			// Token: 0x1700038D RID: 909
			// (set) Token: 0x060018FE RID: 6398 RVA: 0x00038072 File Offset: 0x00036272
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x1700038E RID: 910
			// (set) Token: 0x060018FF RID: 6399 RVA: 0x0003808A File Offset: 0x0003628A
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x1700038F RID: 911
			// (set) Token: 0x06001900 RID: 6400 RVA: 0x000380A2 File Offset: 0x000362A2
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17000390 RID: 912
			// (set) Token: 0x06001901 RID: 6401 RVA: 0x000380B5 File Offset: 0x000362B5
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17000391 RID: 913
			// (set) Token: 0x06001902 RID: 6402 RVA: 0x000380CD File Offset: 0x000362CD
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17000392 RID: 914
			// (set) Token: 0x06001903 RID: 6403 RVA: 0x000380E5 File Offset: 0x000362E5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000393 RID: 915
			// (set) Token: 0x06001904 RID: 6404 RVA: 0x000380F8 File Offset: 0x000362F8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000394 RID: 916
			// (set) Token: 0x06001905 RID: 6405 RVA: 0x00038110 File Offset: 0x00036310
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000395 RID: 917
			// (set) Token: 0x06001906 RID: 6406 RVA: 0x00038128 File Offset: 0x00036328
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000396 RID: 918
			// (set) Token: 0x06001907 RID: 6407 RVA: 0x00038140 File Offset: 0x00036340
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000397 RID: 919
			// (set) Token: 0x06001908 RID: 6408 RVA: 0x00038158 File Offset: 0x00036358
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000094 RID: 148
		public class serverParameters : ParametersBase
		{
			// Token: 0x17000398 RID: 920
			// (set) Token: 0x0600190A RID: 6410 RVA: 0x00038178 File Offset: 0x00036378
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17000399 RID: 921
			// (set) Token: 0x0600190B RID: 6411 RVA: 0x0003818B File Offset: 0x0003638B
			public virtual FailureMode FailureMode
			{
				set
				{
					base.PowerSharpParameters["FailureMode"] = value;
				}
			}

			// Token: 0x1700039A RID: 922
			// (set) Token: 0x0600190C RID: 6412 RVA: 0x000381A3 File Offset: 0x000363A3
			public virtual int? ErrorCode
			{
				set
				{
					base.PowerSharpParameters["ErrorCode"] = value;
				}
			}

			// Token: 0x1700039B RID: 923
			// (set) Token: 0x0600190D RID: 6413 RVA: 0x000381BB File Offset: 0x000363BB
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x1700039C RID: 924
			// (set) Token: 0x0600190E RID: 6414 RVA: 0x000381D3 File Offset: 0x000363D3
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x1700039D RID: 925
			// (set) Token: 0x0600190F RID: 6415 RVA: 0x000381EB File Offset: 0x000363EB
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700039E RID: 926
			// (set) Token: 0x06001910 RID: 6416 RVA: 0x000381FE File Offset: 0x000363FE
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700039F RID: 927
			// (set) Token: 0x06001911 RID: 6417 RVA: 0x00038216 File Offset: 0x00036416
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170003A0 RID: 928
			// (set) Token: 0x06001912 RID: 6418 RVA: 0x0003822E File Offset: 0x0003642E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170003A1 RID: 929
			// (set) Token: 0x06001913 RID: 6419 RVA: 0x00038241 File Offset: 0x00036441
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170003A2 RID: 930
			// (set) Token: 0x06001914 RID: 6420 RVA: 0x00038259 File Offset: 0x00036459
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170003A3 RID: 931
			// (set) Token: 0x06001915 RID: 6421 RVA: 0x00038271 File Offset: 0x00036471
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170003A4 RID: 932
			// (set) Token: 0x06001916 RID: 6422 RVA: 0x00038289 File Offset: 0x00036489
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170003A5 RID: 933
			// (set) Token: 0x06001917 RID: 6423 RVA: 0x000382A1 File Offset: 0x000364A1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000095 RID: 149
		public class databaseParameters : ParametersBase
		{
			// Token: 0x170003A6 RID: 934
			// (set) Token: 0x06001919 RID: 6425 RVA: 0x000382C1 File Offset: 0x000364C1
			public virtual DatabaseIdParameter MailboxDatabase
			{
				set
				{
					base.PowerSharpParameters["MailboxDatabase"] = value;
				}
			}

			// Token: 0x170003A7 RID: 935
			// (set) Token: 0x0600191A RID: 6426 RVA: 0x000382D4 File Offset: 0x000364D4
			public virtual FailureMode FailureMode
			{
				set
				{
					base.PowerSharpParameters["FailureMode"] = value;
				}
			}

			// Token: 0x170003A8 RID: 936
			// (set) Token: 0x0600191B RID: 6427 RVA: 0x000382EC File Offset: 0x000364EC
			public virtual int? ErrorCode
			{
				set
				{
					base.PowerSharpParameters["ErrorCode"] = value;
				}
			}

			// Token: 0x170003A9 RID: 937
			// (set) Token: 0x0600191C RID: 6428 RVA: 0x00038304 File Offset: 0x00036504
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x170003AA RID: 938
			// (set) Token: 0x0600191D RID: 6429 RVA: 0x0003831C File Offset: 0x0003651C
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x170003AB RID: 939
			// (set) Token: 0x0600191E RID: 6430 RVA: 0x00038334 File Offset: 0x00036534
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170003AC RID: 940
			// (set) Token: 0x0600191F RID: 6431 RVA: 0x00038347 File Offset: 0x00036547
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170003AD RID: 941
			// (set) Token: 0x06001920 RID: 6432 RVA: 0x0003835F File Offset: 0x0003655F
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170003AE RID: 942
			// (set) Token: 0x06001921 RID: 6433 RVA: 0x00038377 File Offset: 0x00036577
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170003AF RID: 943
			// (set) Token: 0x06001922 RID: 6434 RVA: 0x0003838A File Offset: 0x0003658A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170003B0 RID: 944
			// (set) Token: 0x06001923 RID: 6435 RVA: 0x000383A2 File Offset: 0x000365A2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170003B1 RID: 945
			// (set) Token: 0x06001924 RID: 6436 RVA: 0x000383BA File Offset: 0x000365BA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170003B2 RID: 946
			// (set) Token: 0x06001925 RID: 6437 RVA: 0x000383D2 File Offset: 0x000365D2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170003B3 RID: 947
			// (set) Token: 0x06001926 RID: 6438 RVA: 0x000383EA File Offset: 0x000365EA
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000096 RID: 150
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170003B4 RID: 948
			// (set) Token: 0x06001928 RID: 6440 RVA: 0x0003840A File Offset: 0x0003660A
			public virtual FailureMode FailureMode
			{
				set
				{
					base.PowerSharpParameters["FailureMode"] = value;
				}
			}

			// Token: 0x170003B5 RID: 949
			// (set) Token: 0x06001929 RID: 6441 RVA: 0x00038422 File Offset: 0x00036622
			public virtual int? ErrorCode
			{
				set
				{
					base.PowerSharpParameters["ErrorCode"] = value;
				}
			}

			// Token: 0x170003B6 RID: 950
			// (set) Token: 0x0600192A RID: 6442 RVA: 0x0003843A File Offset: 0x0003663A
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x170003B7 RID: 951
			// (set) Token: 0x0600192B RID: 6443 RVA: 0x00038452 File Offset: 0x00036652
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x170003B8 RID: 952
			// (set) Token: 0x0600192C RID: 6444 RVA: 0x0003846A File Offset: 0x0003666A
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170003B9 RID: 953
			// (set) Token: 0x0600192D RID: 6445 RVA: 0x0003847D File Offset: 0x0003667D
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170003BA RID: 954
			// (set) Token: 0x0600192E RID: 6446 RVA: 0x00038495 File Offset: 0x00036695
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170003BB RID: 955
			// (set) Token: 0x0600192F RID: 6447 RVA: 0x000384AD File Offset: 0x000366AD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170003BC RID: 956
			// (set) Token: 0x06001930 RID: 6448 RVA: 0x000384C0 File Offset: 0x000366C0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170003BD RID: 957
			// (set) Token: 0x06001931 RID: 6449 RVA: 0x000384D8 File Offset: 0x000366D8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170003BE RID: 958
			// (set) Token: 0x06001932 RID: 6450 RVA: 0x000384F0 File Offset: 0x000366F0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170003BF RID: 959
			// (set) Token: 0x06001933 RID: 6451 RVA: 0x00038508 File Offset: 0x00036708
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170003C0 RID: 960
			// (set) Token: 0x06001934 RID: 6452 RVA: 0x00038520 File Offset: 0x00036720
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
