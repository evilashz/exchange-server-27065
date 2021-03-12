using System;
using System.IO;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200006D RID: 109
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class ThemeFileInfoAttribute : Attribute
	{
		// Token: 0x06000354 RID: 852 RVA: 0x0001405B File Offset: 0x0001225B
		internal ThemeFileInfoAttribute() : this(string.Empty, ThemeFileInfoFlags.None, null)
		{
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0001406A File Offset: 0x0001226A
		internal ThemeFileInfoAttribute(string name) : this(name, ThemeFileInfoFlags.None, null)
		{
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00014075 File Offset: 0x00012275
		internal ThemeFileInfoAttribute(string name, ThemeFileInfoFlags themeFileInfoFlags) : this(name, themeFileInfoFlags, null)
		{
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00014080 File Offset: 0x00012280
		internal ThemeFileInfoAttribute(string name, ThemeFileInfoFlags themeFileInfoFlags, string fallbackImageName)
		{
			this.name = name;
			this.themeFileInfoFlags = themeFileInfoFlags;
			this.fallbackImageName = fallbackImageName;
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0001409D File Offset: 0x0001229D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000359 RID: 857 RVA: 0x000140A5 File Offset: 0x000122A5
		public string FallbackImageName
		{
			get
			{
				return this.fallbackImageName;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600035A RID: 858 RVA: 0x000140B0 File Offset: 0x000122B0
		public bool UseCssSprites
		{
			get
			{
				if (string.IsNullOrEmpty(this.Name))
				{
					return false;
				}
				string extension = Path.GetExtension(this.Name);
				return !string.IsNullOrEmpty(extension) && (extension.Equals(".gif", StringComparison.OrdinalIgnoreCase) || extension.Equals(".png", StringComparison.OrdinalIgnoreCase)) && !ThemeFileInfoAttribute.IsFlagSet(this.themeFileInfoFlags, ThemeFileInfoFlags.LooseImage);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0001410D File Offset: 0x0001230D
		public bool PhaseII
		{
			get
			{
				return this.UseCssSprites && ThemeFileInfoAttribute.IsFlagSet(this.themeFileInfoFlags, ThemeFileInfoFlags.PhaseII);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00014125 File Offset: 0x00012325
		public bool IsResource
		{
			get
			{
				return ThemeFileInfoAttribute.IsFlagSet(this.themeFileInfoFlags, ThemeFileInfoFlags.Resource);
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00014133 File Offset: 0x00012333
		private static bool IsFlagSet(ThemeFileInfoFlags valueToTest, ThemeFileInfoFlags flag)
		{
			return (valueToTest & flag) == flag;
		}

		// Token: 0x04000259 RID: 601
		private string name;

		// Token: 0x0400025A RID: 602
		private ThemeFileInfoFlags themeFileInfoFlags;

		// Token: 0x0400025B RID: 603
		private string fallbackImageName;
	}
}
