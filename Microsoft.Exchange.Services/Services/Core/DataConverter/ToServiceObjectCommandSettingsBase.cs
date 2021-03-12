using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000F7 RID: 247
	internal abstract class ToServiceObjectCommandSettingsBase : CommandSettings
	{
		// Token: 0x060006D6 RID: 1750 RVA: 0x00022AB6 File Offset: 0x00020CB6
		public ToServiceObjectCommandSettingsBase()
		{
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00022ABE File Offset: 0x00020CBE
		public ToServiceObjectCommandSettingsBase(PropertyPath propertyPath)
		{
			this.PropertyPath = propertyPath;
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00022ACD File Offset: 0x00020CCD
		// (set) Token: 0x060006D9 RID: 1753 RVA: 0x00022AD5 File Offset: 0x00020CD5
		public ServiceObject ServiceObject { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x00022ADE File Offset: 0x00020CDE
		// (set) Token: 0x060006DB RID: 1755 RVA: 0x00022AE6 File Offset: 0x00020CE6
		public PropertyPath PropertyPath { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x00022AEF File Offset: 0x00020CEF
		// (set) Token: 0x060006DD RID: 1757 RVA: 0x00022AF7 File Offset: 0x00020CF7
		public IdAndSession IdAndSession { get; set; }
	}
}
