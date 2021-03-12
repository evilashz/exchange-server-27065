using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x020002F6 RID: 758
	[Cmdlet("Uninstall", "Adam")]
	[LocDescription(Strings.IDs.UninstallAdamTask)]
	public sealed class UninstallAdamTask : Task
	{
		// Token: 0x06001A09 RID: 6665 RVA: 0x00073F05 File Offset: 0x00072105
		public UninstallAdamTask()
		{
			this.InstanceName = "MSExchange";
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001A0A RID: 6666 RVA: 0x00073F18 File Offset: 0x00072118
		// (set) Token: 0x06001A0B RID: 6667 RVA: 0x00073F2F File Offset: 0x0007212F
		[Parameter(Mandatory = false)]
		public string InstanceName
		{
			get
			{
				return (string)base.Fields["InstanceName"];
			}
			set
			{
				base.Fields["InstanceName"] = value;
			}
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x00073F42 File Offset: 0x00072142
		protected override void InternalValidate()
		{
			base.InternalValidate();
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x00073F4C File Offset: 0x0007214C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.InstanceName
			});
			try
			{
				ManageAdamService.UninstallAdam(this.InstanceName);
			}
			catch (AdamUninstallProcessFailureException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, null);
			}
			catch (AdamUninstallGeneralFailureWithResultException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidOperation, null);
			}
			catch (AdamUninstallErrorException exception3)
			{
				base.WriteError(exception3, ErrorCategory.InvalidOperation, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04000B63 RID: 2915
		public const string InstanceParamName = "InstanceName";
	}
}
