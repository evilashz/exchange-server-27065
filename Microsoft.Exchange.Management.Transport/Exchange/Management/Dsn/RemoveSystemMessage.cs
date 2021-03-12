using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Dsn
{
	// Token: 0x02000094 RID: 148
	[Cmdlet("Remove", "SystemMessage", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveSystemMessage : RemoveSystemConfigurationObjectTask<SystemMessageIdParameter, SystemMessage>
	{
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00014869 File Offset: 0x00012A69
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveSystemMessage(this.Identity.ToString());
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0001487C File Offset: 0x00012A7C
		protected override ObjectId RootId
		{
			get
			{
				ADObjectId orgContainerId = (base.DataSession as IConfigurationSession).GetOrgContainerId();
				return SystemMessage.GetDsnCustomizationContainer(orgContainerId);
			}
		}
	}
}
