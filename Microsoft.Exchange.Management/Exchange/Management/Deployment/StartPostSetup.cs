using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000263 RID: 611
	[Cmdlet("Start", "PostSetup", SupportsShouldProcess = true)]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class StartPostSetup : ManageSetupBindingTasks
	{
		// Token: 0x060016C5 RID: 5829 RVA: 0x0006127D File Offset: 0x0005F47D
		public StartPostSetup()
		{
			base.ImplementsResume = false;
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x060016C6 RID: 5830 RVA: 0x0006128C File Offset: 0x0005F48C
		protected override LocalizedString Description
		{
			get
			{
				return Strings.StartPostSetupDescription;
			}
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x00061293 File Offset: 0x0005F493
		protected override void PopulateContextVariables()
		{
			base.PopulateContextVariables();
			base.Fields["SetupAssemblyPath"] = ConfigurationContext.Setup.AssemblyPath;
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x000612B0 File Offset: 0x0005F4B0
		protected override void InternalValidate()
		{
			base.InternalValidate();
			base.ComponentInfoFileNames.Reverse();
		}
	}
}
