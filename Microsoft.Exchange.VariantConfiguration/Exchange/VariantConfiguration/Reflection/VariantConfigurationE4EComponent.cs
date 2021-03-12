using System;
using Microsoft.Exchange.Flighting;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000104 RID: 260
	public sealed class VariantConfigurationE4EComponent : VariantConfigurationComponent
	{
		// Token: 0x06000BDF RID: 3039 RVA: 0x0001C2D8 File Offset: 0x0001A4D8
		internal VariantConfigurationE4EComponent() : base("E4E")
		{
			base.Add(new VariantConfigurationSection("E4E.settings.ini", "OTP", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("E4E.settings.ini", "Version", typeof(IVersion), false));
			base.Add(new VariantConfigurationSection("E4E.settings.ini", "LogoffViaOwa", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("E4E.settings.ini", "MsodsGraphQuery", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("E4E.settings.ini", "E4E", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("E4E.settings.ini", "TouchLayout", typeof(IFeature), false));
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x0001C3B0 File Offset: 0x0001A5B0
		public VariantConfigurationSection OTP
		{
			get
			{
				return base["OTP"];
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x0001C3BD File Offset: 0x0001A5BD
		public VariantConfigurationSection Version
		{
			get
			{
				return base["Version"];
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x0001C3CA File Offset: 0x0001A5CA
		public VariantConfigurationSection LogoffViaOwa
		{
			get
			{
				return base["LogoffViaOwa"];
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0001C3D7 File Offset: 0x0001A5D7
		public VariantConfigurationSection MsodsGraphQuery
		{
			get
			{
				return base["MsodsGraphQuery"];
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x0001C3E4 File Offset: 0x0001A5E4
		public VariantConfigurationSection E4E
		{
			get
			{
				return base["E4E"];
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x0001C3F1 File Offset: 0x0001A5F1
		public VariantConfigurationSection TouchLayout
		{
			get
			{
				return base["TouchLayout"];
			}
		}
	}
}
