using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200022B RID: 555
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("remove", "umlanguagepackregistry")]
	public sealed class RemoveUMLanguagePackRegistry : ManageUMLanaguagePackRegistry
	{
		// Token: 0x060012D5 RID: 4821 RVA: 0x000528D8 File Offset: 0x00050AD8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				this.languagePack.RemoveProductCodesFromRegistry();
			}
			catch (RegistryInsufficientPermissionException exception)
			{
				base.WriteError(exception, ErrorCategory.PermissionDenied, null);
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}
	}
}
