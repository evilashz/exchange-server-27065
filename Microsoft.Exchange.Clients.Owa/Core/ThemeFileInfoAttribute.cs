using System;
using System.IO;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000267 RID: 615
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class ThemeFileInfoAttribute : Attribute
	{
		// Token: 0x06001498 RID: 5272 RVA: 0x0007DD12 File Offset: 0x0007BF12
		internal ThemeFileInfoAttribute() : this(string.Empty, ThemeFileInfoFlags.None, null)
		{
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x0007DD21 File Offset: 0x0007BF21
		internal ThemeFileInfoAttribute(string name) : this(name, ThemeFileInfoFlags.None, null)
		{
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x0007DD2C File Offset: 0x0007BF2C
		internal ThemeFileInfoAttribute(string name, ThemeFileInfoFlags themeFileInfoFlags) : this(name, themeFileInfoFlags, null)
		{
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x0007DD37 File Offset: 0x0007BF37
		internal ThemeFileInfoAttribute(string name, ThemeFileInfoFlags themeFileInfoFlags, string fallbackImageName)
		{
			this.name = name;
			this.themeFileInfoFlags = themeFileInfoFlags;
			this.fallbackImageName = fallbackImageName;
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x0007DD54 File Offset: 0x0007BF54
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x0007DD5C File Offset: 0x0007BF5C
		public string FallbackImageName
		{
			get
			{
				return this.fallbackImageName;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x0007DD64 File Offset: 0x0007BF64
		public bool UseCssSprites
		{
			get
			{
				if (string.IsNullOrEmpty(this.Name))
				{
					return false;
				}
				string extension = Path.GetExtension(this.Name);
				return !string.IsNullOrEmpty(extension) && (extension.Equals(".gif", StringComparison.InvariantCultureIgnoreCase) || extension.Equals(".png", StringComparison.InvariantCultureIgnoreCase)) && !Utilities.IsFlagSet((int)this.themeFileInfoFlags, 1);
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x0007DDC1 File Offset: 0x0007BFC1
		public bool PhaseII
		{
			get
			{
				return this.UseCssSprites && Utilities.IsFlagSet((int)this.themeFileInfoFlags, 2);
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x0007DDD9 File Offset: 0x0007BFD9
		public bool IsResource
		{
			get
			{
				return Utilities.IsFlagSet((int)this.themeFileInfoFlags, 4);
			}
		}

		// Token: 0x04001081 RID: 4225
		private string name;

		// Token: 0x04001082 RID: 4226
		private ThemeFileInfoFlags themeFileInfoFlags;

		// Token: 0x04001083 RID: 4227
		private string fallbackImageName;
	}
}
