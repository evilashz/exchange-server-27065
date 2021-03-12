using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000269 RID: 617
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Test", "NewOrganizationDomainName")]
	public sealed class TestNewOrganizationDomainName : Task
	{
		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x060016E4 RID: 5860 RVA: 0x0006178A File Offset: 0x0005F98A
		// (set) Token: 0x060016E5 RID: 5861 RVA: 0x000617A1 File Offset: 0x0005F9A1
		[Parameter(Mandatory = true)]
		public SmtpDomain Value
		{
			get
			{
				return (SmtpDomain)base.Fields["Value"];
			}
			set
			{
				base.Fields["Value"] = value;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x060016E6 RID: 5862 RVA: 0x000617B4 File Offset: 0x0005F9B4
		private string ParameterName
		{
			get
			{
				return "DomainName";
			}
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x000617BC File Offset: 0x0005F9BC
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			if (this.Value.ToString().Length > 64)
			{
				base.WriteError(new ArgumentException(Strings.ErrorNameValueStringTooLong(this.ParameterName, 64, this.Value.ToString().Length)), ErrorCategory.InvalidArgument, null);
			}
		}
	}
}
