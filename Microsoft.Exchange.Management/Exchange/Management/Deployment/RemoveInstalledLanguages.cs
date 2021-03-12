using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000222 RID: 546
	[Cmdlet("remove", "installedlanguages", SupportsShouldProcess = true)]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class RemoveInstalledLanguages : ComponentInfoBasedTask
	{
		// Token: 0x06001286 RID: 4742 RVA: 0x00051730 File Offset: 0x0004F930
		public RemoveInstalledLanguages()
		{
			base.ComponentInfoFileNames = new List<string>();
			base.ImplementsResume = false;
			base.Fields["InstallationMode"] = InstallationModes.Uninstall;
			base.ComponentInfoFileNames.Add("setup\\data\\LanguagePackUninstallationComponent.xml");
			DateTime dateTime = (DateTime)ExDateTime.Now;
			base.Fields["LogDateTime"] = dateTime.ToString("yyyyMMdd-HHmmss");
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x000517A2 File Offset: 0x0004F9A2
		// (set) Token: 0x06001288 RID: 4744 RVA: 0x000517AA File Offset: 0x0004F9AA
		[Parameter(Mandatory = true)]
		public InstallationModes InstallMode
		{
			get
			{
				return this.InstallationMode;
			}
			set
			{
				this.InstallationMode = value;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x000517B3 File Offset: 0x0004F9B3
		// (set) Token: 0x0600128A RID: 4746 RVA: 0x000517CA File Offset: 0x0004F9CA
		[Parameter(Mandatory = false)]
		public LocalLongFullPath LogFilePath
		{
			get
			{
				return (LocalLongFullPath)base.Fields["LogFilePath"];
			}
			set
			{
				base.Fields["LogFilePath"] = value;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x000517DD File Offset: 0x0004F9DD
		protected override LocalizedString Description
		{
			get
			{
				return Strings.LanguagePackDescription;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x000517E4 File Offset: 0x0004F9E4
		// (set) Token: 0x0600128D RID: 4749 RVA: 0x000517FB File Offset: 0x0004F9FB
		[Parameter(Mandatory = false)]
		public string PropertyValues
		{
			get
			{
				return (string)base.Fields["PropertyValues"];
			}
			set
			{
				base.Fields["PropertyValues"] = value;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x0005180E File Offset: 0x0004FA0E
		// (set) Token: 0x0600128F RID: 4751 RVA: 0x00051825 File Offset: 0x0004FA25
		[Parameter(Mandatory = false)]
		public string[] LanguagePacksToRemove
		{
			get
			{
				return (string[])base.Fields["LanguagePacksToRemove"];
			}
			set
			{
				base.Fields["LanguagePacksToRemove"] = value;
			}
		}
	}
}
