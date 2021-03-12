using System;
using System.Management.Automation;
using System.Reflection;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.FfoQuarantine
{
	// Token: 0x0200002C RID: 44
	[OutputType(new Type[]
	{
		typeof(bool)
	})]
	[Cmdlet("Release", "QuarantineMessage", SupportsShouldProcess = true, DefaultParameterSetName = "ReleaseToSelf")]
	public sealed class ReleaseQuarantineMessage : Task
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000127 RID: 295 RVA: 0x000058E7 File Offset: 0x00003AE7
		// (set) Token: 0x06000128 RID: 296 RVA: 0x000058EF File Offset: 0x00003AEF
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
		public string Identity { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000058F8 File Offset: 0x00003AF8
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00005900 File Offset: 0x00003B00
		[Parameter(Mandatory = false)]
		public OrganizationIdParameter Organization { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00005909 File Offset: 0x00003B09
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00005911 File Offset: 0x00003B11
		[Parameter(ParameterSetName = "OrgReleaseToUser", Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public string[] User { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0000591A File Offset: 0x00003B1A
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00005922 File Offset: 0x00003B22
		[Parameter]
		public SwitchParameter ReportFalsePositive { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600012F RID: 303 RVA: 0x0000592B File Offset: 0x00003B2B
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00005933 File Offset: 0x00003B33
		[Parameter(ParameterSetName = "OrgReleaseToAll", Mandatory = true)]
		public SwitchParameter ReleaseToAll { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000131 RID: 305 RVA: 0x0000593C File Offset: 0x00003B3C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageReleaseQuarantineMessage(this.Identity.ToString());
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00005950 File Offset: 0x00003B50
		protected override void InternalProcessRecord()
		{
			SystemProbe.Trace(ReleaseQuarantineMessage.ComponentName, SystemProbe.Status.Pass, "Entering InternalProcessRecord", new object[0]);
			try
			{
				Assembly assembly = Assembly.Load("Microsoft.Exchange.Hygiene.ManagementHelper");
				Type type = assembly.GetType("Microsoft.Exchange.Hygiene.ManagementHelper.FfoQuarantine.ReleaseQuarantineMessageHelper");
				MethodInfo method = type.GetMethod("InternalProcessRecordHelper", BindingFlags.Static | BindingFlags.Public, null, new Type[]
				{
					typeof(ReleaseQuarantineMessage)
				}, null);
				method.Invoke(null, new object[]
				{
					this
				});
			}
			catch (TargetInvocationException ex)
			{
				SystemProbe.Trace(ReleaseQuarantineMessage.ComponentName, SystemProbe.Status.Fail, "TargetInvocationException in InternalProcessRecord: {0}", new object[]
				{
					ex.ToString()
				});
				if (ex.InnerException != null)
				{
					throw ex.InnerException;
				}
				throw;
			}
			catch (Exception ex2)
			{
				SystemProbe.Trace(ReleaseQuarantineMessage.ComponentName, SystemProbe.Status.Fail, "Unhandled Exception in InternalProcessRecord: {0}", new object[]
				{
					ex2.ToString()
				});
				throw;
			}
			SystemProbe.Trace(ReleaseQuarantineMessage.ComponentName, SystemProbe.Status.Pass, "Exiting InternalProcessRecord", new object[0]);
		}

		// Token: 0x0400006B RID: 107
		private static readonly string ComponentName = "ReleaseQuarantineMessage";
	}
}
