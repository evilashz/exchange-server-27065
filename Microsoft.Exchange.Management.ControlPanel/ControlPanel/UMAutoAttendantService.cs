using System;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004AB RID: 1195
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class UMAutoAttendantService : UMBasePromptService, IUMAutoAttendantService, IUploadHandler
	{
	}
}
