using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000171 RID: 369
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("add", "umlanguagepackregistry")]
	public sealed class AddUMLanguagePackRegistry : ManageUMLanaguagePackRegistry
	{
		// Token: 0x06000DC6 RID: 3526 RVA: 0x0003FBA4 File Offset: 0x0003DDA4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				this.languagePack.AddProductCodesToRegistry();
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
