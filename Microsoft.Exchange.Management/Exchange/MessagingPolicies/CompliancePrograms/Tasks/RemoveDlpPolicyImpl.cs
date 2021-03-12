using System;
using System.Management.Automation;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x0200096C RID: 2412
	internal class RemoveDlpPolicyImpl : CmdletImplementation
	{
		// Token: 0x06005632 RID: 22066 RVA: 0x0016259F File Offset: 0x0016079F
		public RemoveDlpPolicyImpl(RemoveDlpPolicy taskObject)
		{
			this.taskObject = taskObject;
		}

		// Token: 0x06005633 RID: 22067 RVA: 0x001625AE File Offset: 0x001607AE
		public override void Validate()
		{
			if (this.taskObject.Identity == null)
			{
				this.taskObject.WriteError(new ArgumentException(Strings.ErrorInvalidDlpPolicyIdentity, RemoveDlpPolicyImpl.Identity), ErrorCategory.InvalidArgument, this.taskObject.Identity);
			}
		}

		// Token: 0x06005634 RID: 22068 RVA: 0x001625E8 File Offset: 0x001607E8
		public override void ProcessRecord()
		{
			try
			{
				DlpUtils.DeleteEtrsByDlpPolicy(this.taskObject.GetDataObject().ImmutableId, base.DataSession);
			}
			catch (ParserException ex)
			{
				this.taskObject.WriteError(new ArgumentException(Strings.RemoveDlpPolicyCorruptRule(this.taskObject.Identity.ToString(), ex.Message)), ErrorCategory.ParserError, this.taskObject.Identity);
			}
			base.DataSession.Delete(this.taskObject.GetDataObject());
		}

		// Token: 0x040031D9 RID: 12761
		public static readonly string Identity = "Identity";

		// Token: 0x040031DA RID: 12762
		private RemoveDlpPolicy taskObject;
	}
}
