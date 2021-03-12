using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x0200096E RID: 2414
	internal class RemoveDlpPolicyTemplateImpl : CmdletImplementation
	{
		// Token: 0x0600563C RID: 22076 RVA: 0x0016271F File Offset: 0x0016091F
		public RemoveDlpPolicyTemplateImpl(RemoveDlpPolicyTemplate taskObject)
		{
			this.taskObject = taskObject;
		}

		// Token: 0x0600563D RID: 22077 RVA: 0x00162730 File Offset: 0x00160930
		public override void Validate()
		{
			if (this.taskObject.Identity == null)
			{
				this.taskObject.WriteError(new ArgumentException(Strings.ErrorInvalidDlpPolicyTemplateIdentity, RemoveDlpPolicyImpl.Identity), ErrorCategory.InvalidArgument, this.taskObject.Identity);
				return;
			}
			if (!DlpUtils.GetOutOfBoxDlpTemplates(base.DataSession, this.taskObject.Identity.ToString()).Any<ADComplianceProgram>())
			{
				this.taskObject.WriteError(new ArgumentException(Strings.ErrorDlpPolicyTemplateIsNotInstalled(this.taskObject.Identity.ToString())), ErrorCategory.InvalidArgument, this.taskObject.Identity);
			}
		}

		// Token: 0x0600563E RID: 22078 RVA: 0x001627CE File Offset: 0x001609CE
		public override void ProcessRecord()
		{
			DlpUtils.DeleteOutOfBoxDlpPolicy(base.DataSession, this.taskObject.Identity.ToString());
		}

		// Token: 0x040031DC RID: 12764
		public static readonly string Identity = "Identity";

		// Token: 0x040031DD RID: 12765
		private RemoveDlpPolicyTemplate taskObject;
	}
}
