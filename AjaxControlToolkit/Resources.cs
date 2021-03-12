using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace AjaxControlToolkit
{
	// Token: 0x02000022 RID: 34
	[DebuggerNonUserCode]
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
	internal class Resources
	{
		// Token: 0x06000108 RID: 264 RVA: 0x00004570 File Offset: 0x00002770
		internal Resources()
		{
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00004578 File Offset: 0x00002778
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("AjaxControlToolkit.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600010A RID: 266 RVA: 0x000045B7 File Offset: 0x000027B7
		// (set) Token: 0x0600010B RID: 267 RVA: 0x000045BE File Offset: 0x000027BE
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600010C RID: 268 RVA: 0x000045C6 File Offset: 0x000027C6
		internal static string E_NoScriptManager
		{
			get
			{
				return Resources.ResourceManager.GetString("E_NoScriptManager", Resources.resourceCulture);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000045DC File Offset: 0x000027DC
		internal static string E_SciptPathNotAllowed
		{
			get
			{
				return Resources.ResourceManager.GetString("E_SciptPathNotAllowed", Resources.resourceCulture);
			}
		}

		// Token: 0x04000043 RID: 67
		private static ResourceManager resourceMan;

		// Token: 0x04000044 RID: 68
		private static CultureInfo resourceCulture;
	}
}
