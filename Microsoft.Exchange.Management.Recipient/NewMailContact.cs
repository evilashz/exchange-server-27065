using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200007D RID: 125
	[Cmdlet("New", "MailContact", SupportsShouldProcess = true)]
	public sealed class NewMailContact : NewMailContactBase
	{
		// Token: 0x0600090E RID: 2318 RVA: 0x00026C7C File Offset: 0x00024E7C
		protected override void WriteResult(ADObject result)
		{
			TaskLogger.LogEnter(new object[]
			{
				result.Identity
			});
			MailContact result2 = new MailContact((ADContact)result);
			base.WriteResult(result2);
			TaskLogger.LogExit();
		}
	}
}
