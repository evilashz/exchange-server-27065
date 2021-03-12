using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000170 RID: 368
	[ClassAccessLevel(AccessLevel.Consumer)]
	public abstract class ManageUMLanaguagePackRegistry : Task
	{
		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x0003FB23 File Offset: 0x0003DD23
		// (set) Token: 0x06000DC3 RID: 3523 RVA: 0x0003FB3A File Offset: 0x0003DD3A
		[Parameter(Mandatory = true)]
		public CultureInfo Language
		{
			get
			{
				return (CultureInfo)base.Fields["Language"];
			}
			set
			{
				base.Fields["Language"] = value;
			}
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0003FB50 File Offset: 0x0003DD50
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			try
			{
				this.languagePack = UmLanguagePackUtils.GetUmLanguagePack(this.Language);
			}
			catch (UnSupportedUMLanguageException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidData, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0400069F RID: 1695
		protected UmLanguagePack languagePack;
	}
}
