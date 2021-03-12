using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000295 RID: 661
	[Cmdlet("Write", "ExchangeSetupLog", DefaultParameterSetName = "Info")]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class WriteExchangeSetupLog : Task
	{
		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x0006488A File Offset: 0x00062A8A
		// (set) Token: 0x060017D5 RID: 6101 RVA: 0x000648A1 File Offset: 0x00062AA1
		[Parameter(Mandatory = true, Position = 0, ParameterSetName = "Info")]
		public SwitchParameter Info
		{
			get
			{
				return (SwitchParameter)base.Fields["Info"];
			}
			set
			{
				base.Fields["Info"] = value;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x000648B9 File Offset: 0x00062AB9
		// (set) Token: 0x060017D7 RID: 6103 RVA: 0x000648D0 File Offset: 0x00062AD0
		[Parameter(Mandatory = true, Position = 0, ParameterSetName = "Warning")]
		public SwitchParameter Warning
		{
			get
			{
				return (SwitchParameter)base.Fields["Warning"];
			}
			set
			{
				base.Fields["Warning"] = value;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x000648E8 File Offset: 0x00062AE8
		// (set) Token: 0x060017D9 RID: 6105 RVA: 0x000648FF File Offset: 0x00062AFF
		[Parameter(Mandatory = true, Position = 0, ParameterSetName = "Error")]
		public SwitchParameter Error
		{
			get
			{
				return (SwitchParameter)base.Fields["Error"];
			}
			set
			{
				base.Fields["Error"] = value;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x00064917 File Offset: 0x00062B17
		// (set) Token: 0x060017DB RID: 6107 RVA: 0x0006492E File Offset: 0x00062B2E
		[Parameter(Mandatory = true, Position = 1, ParameterSetName = "Info")]
		[Parameter(Mandatory = true, Position = 1, ParameterSetName = "Warning")]
		public string Message
		{
			get
			{
				return (string)base.Fields["Message"];
			}
			set
			{
				base.Fields["Message"] = value;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x060017DC RID: 6108 RVA: 0x00064941 File Offset: 0x00062B41
		// (set) Token: 0x060017DD RID: 6109 RVA: 0x00064958 File Offset: 0x00062B58
		[Parameter(Mandatory = true, Position = 1, ParameterSetName = "Error")]
		public Exception Exception
		{
			get
			{
				return (Exception)base.Fields["Exception"];
			}
			set
			{
				base.Fields["Exception"] = value;
			}
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x0006496C File Offset: 0x00062B6C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			if (base.ParameterSetName == "Warning")
			{
				this.WriteWarning(new LocalizedString(this.Message));
			}
			else if (base.ParameterSetName == "Error")
			{
				base.WriteError(this.Exception, ErrorCategory.InvalidOperation, null);
			}
			else
			{
				base.WriteVerbose(new LocalizedString(this.Message));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04000A11 RID: 2577
		private const string ParameterSetInfo = "Info";

		// Token: 0x04000A12 RID: 2578
		private const string ParameterSetWarning = "Warning";

		// Token: 0x04000A13 RID: 2579
		private const string ParameterSetError = "Error";
	}
}
