using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000509 RID: 1289
	[DataContract]
	public class OwaMailboxPolicyFeatureInfo : MailboxFeatureInfo
	{
		// Token: 0x06003DF4 RID: 15860 RVA: 0x000BA0CC File Offset: 0x000B82CC
		public OwaMailboxPolicyFeatureInfo(string parameter, bool enabled, string featureName) : base(new Identity(parameter, featureName))
		{
			base.UseModalDialogForEdit = false;
			base.Name = featureName;
			this.Status = (enabled ? ClientStrings.EnabledDisplayText : ClientStrings.DisabledDisplayText);
			base.EnableCommandUrl = null;
			base.EditCommandUrl = null;
			this.CanChangeStatus = RbacPrincipal.Current.IsInRole("Set-OwaMailboxPolicy?Identity&" + parameter + "@W:Organization");
		}
	}
}
