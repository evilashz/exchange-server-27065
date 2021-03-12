using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200016E RID: 366
	[ClassAccessLevel(AccessLevel.Consumer)]
	public abstract class ManageUmLanguagePack : ComponentInfoBasedTask
	{
		// Token: 0x06000DA4 RID: 3492 RVA: 0x0003F582 File Offset: 0x0003D782
		public ManageUmLanguagePack()
		{
			base.Fields["InstallationMode"] = InstallationModes.Install;
			base.ComponentInfoFileNames = new List<string>();
			this.ShouldRestartUMService = true;
			base.ImplementsResume = false;
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x0003F5B9 File Offset: 0x0003D7B9
		// (set) Token: 0x06000DA6 RID: 3494 RVA: 0x0003F5D0 File Offset: 0x0003D7D0
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

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x0003F5E3 File Offset: 0x0003D7E3
		// (set) Token: 0x06000DA8 RID: 3496 RVA: 0x0003F5FA File Offset: 0x0003D7FA
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

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x0003F60D File Offset: 0x0003D80D
		// (set) Token: 0x06000DAA RID: 3498 RVA: 0x0003F624 File Offset: 0x0003D824
		[Parameter(Mandatory = false)]
		public bool ShouldRestartUMService
		{
			get
			{
				return (bool)base.Fields["ShouldRestartUMService"];
			}
			set
			{
				base.Fields["ShouldRestartUMService"] = value;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x0003F63C File Offset: 0x0003D83C
		protected override LocalizedString Description
		{
			get
			{
				return Strings.UmLanguagePackDescription(this.Language.ToString());
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x0003F64E File Offset: 0x0003D84E
		// (set) Token: 0x06000DAD RID: 3501 RVA: 0x0003F665 File Offset: 0x0003D865
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

		// Token: 0x06000DAE RID: 3502 RVA: 0x0003F678 File Offset: 0x0003D878
		protected override void InternalProcessRecord()
		{
			if (this.ShouldRestartUMService)
			{
				base.ComponentInfoFileNames.Add("setup\\data\\UmLanguagePackInitialization.xml");
				base.ComponentInfoFileNames.Add("setup\\data\\UmLanguagePackComponent.xml");
				base.ComponentInfoFileNames.Add("setup\\data\\UnifiedMessagingFinalization.xml");
			}
			else
			{
				base.ComponentInfoFileNames.Add("setup\\data\\UmLanguagePackComponent.xml");
			}
			base.InternalProcessRecord();
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0003F6D8 File Offset: 0x0003D8D8
		protected override void InternalBeginProcessing()
		{
			RegistryKey registryKey = null;
			try
			{
				registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft Speech Server\\2.0\\Applications\\ExUM", true);
				if (registryKey != null)
				{
					registryKey.SetValue("PreloadedResourceManifestXml", string.Empty, RegistryValueKind.String);
					registryKey.SetValue("PreloadedResourceManifest", string.Empty, RegistryValueKind.String);
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
			base.InternalBeginProcessing();
		}
	}
}
