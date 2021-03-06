using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C00 RID: 3072
	[Cmdlet("Disable", "OutlookAnywhere", SupportsShouldProcess = true, DefaultParameterSetName = "Identity", ConfirmImpact = ConfirmImpact.High)]
	public sealed class DisableRpcHttp : RemoveExchangeVirtualDirectory<ADRpcHttpVirtualDirectory>
	{
		// Token: 0x170023A4 RID: 9124
		// (get) Token: 0x060073DA RID: 29658 RVA: 0x001D7F0D File Offset: 0x001D610D
		// (set) Token: 0x060073DB RID: 29659 RVA: 0x001D7F24 File Offset: 0x001D6124
		[Parameter(ParameterSetName = "Server", ValueFromPipeline = true)]
		public ServerIdParameter Server
		{
			get
			{
				return (ServerIdParameter)base.Fields["Server"];
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x170023A5 RID: 9125
		// (get) Token: 0x060073DC RID: 29660 RVA: 0x001D7F37 File Offset: 0x001D6137
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageDisableRpcHttp(this.Identity.ToString());
			}
		}

		// Token: 0x060073DD RID: 29661 RVA: 0x001D7F4C File Offset: 0x001D614C
		protected override void InternalValidate()
		{
			if (this.Server != null)
			{
				Server server = (Server)base.GetDataObject<Server>(this.Server, base.DataSession, null, new LocalizedString?(Strings.ErrorServerNotFound(this.Server.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(this.Server.ToString())));
				if (base.HasErrors)
				{
					return;
				}
				VirtualDirectoryIdParameter id = VirtualDirectoryIdParameter.Parse(this.Server + "\\*");
				IEnumerable<ADRpcHttpVirtualDirectory> dataObjects = base.GetDataObjects<ADRpcHttpVirtualDirectory>(id, base.DataSession, server.Identity);
				IEnumerator<ADRpcHttpVirtualDirectory> enumerator = dataObjects.GetEnumerator();
				if (!enumerator.MoveNext())
				{
					base.WriteError(new ArgumentException(Strings.ErrorRpcHttpNotEnabled(server.Fqdn), string.Empty), ErrorCategory.InvalidArgument, this.Server);
					return;
				}
				this.Identity = new VirtualDirectoryIdParameter(enumerator.Current);
				if (enumerator.MoveNext())
				{
					base.WriteError(new ArgumentException(Strings.ErrorRpcHttpNotUnique(server.Fqdn), string.Empty), ErrorCategory.InvalidArgument, this.Server);
					return;
				}
			}
			base.InternalValidate();
		}

		// Token: 0x060073DE RID: 29662 RVA: 0x001D8058 File Offset: 0x001D6258
		protected override void DeleteFromMetabase()
		{
		}
	}
}
