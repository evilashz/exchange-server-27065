using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000B3 RID: 179
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class ResponseMessage : ResourceBase, IResponseMessage, IResourceBase<ResponseMessageConfiguration, SetResponseMessageConfiguration>, IEditObjectService<ResponseMessageConfiguration, SetResponseMessageConfiguration>, IGetObjectService<ResponseMessageConfiguration>
	{
		// Token: 0x06001C52 RID: 7250 RVA: 0x00058775 File Offset: 0x00056975
		[PrincipalPermission(SecurityAction.Demand, Role = "Resource+Get-CalendarProcessing?Identity@R:Self")]
		public PowerShellResults<ResponseMessageConfiguration> GetObject(Identity identity)
		{
			return base.GetObject<ResponseMessageConfiguration>(identity);
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x0005877E File Offset: 0x0005697E
		[PrincipalPermission(SecurityAction.Demand, Role = "Resource+Get-CalendarProcessing?Identity@R:Self+Set-CalendarProcessing?Identity@W:Self")]
		public PowerShellResults<ResponseMessageConfiguration> SetObject(Identity identity, SetResponseMessageConfiguration properties)
		{
			return base.SetObject<ResponseMessageConfiguration, SetResponseMessageConfiguration>(identity, properties);
		}
	}
}
