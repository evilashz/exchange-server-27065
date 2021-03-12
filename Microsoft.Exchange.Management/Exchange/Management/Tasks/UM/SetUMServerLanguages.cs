using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D44 RID: 3396
	[Cmdlet("Set", "UMServerLanguages", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public class SetUMServerLanguages : SystemConfigurationObjectActionTask<UMServerIdParameter, Server>
	{
		// Token: 0x17002871 RID: 10353
		// (get) Token: 0x0600822D RID: 33325 RVA: 0x00214597 File Offset: 0x00212797
		// (set) Token: 0x0600822E RID: 33326 RVA: 0x002145AE File Offset: 0x002127AE
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<UMLanguage> Languages
		{
			get
			{
				return (MultiValuedProperty<UMLanguage>)base.Fields["Languages"];
			}
			set
			{
				base.Fields["Languages"] = value;
			}
		}

		// Token: 0x17002872 RID: 10354
		// (get) Token: 0x0600822F RID: 33327 RVA: 0x002145C1 File Offset: 0x002127C1
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetUmServer(this.Identity.ToString());
			}
		}

		// Token: 0x06008230 RID: 33328 RVA: 0x002145D4 File Offset: 0x002127D4
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!base.HasErrors)
			{
				if (!base.Fields.IsModified("Languages"))
				{
					LanguagesNotPassed exception = new LanguagesNotPassed();
					base.WriteError(exception, ErrorCategory.InvalidOperation, null);
				}
				if (this.Languages == null)
				{
					this.DataObject.Languages = new MultiValuedProperty<UMLanguage>();
				}
				else
				{
					this.DataObject.Languages = this.Languages;
				}
			}
			TaskLogger.LogExit();
		}
	}
}
