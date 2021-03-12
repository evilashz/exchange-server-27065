using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x0200059C RID: 1436
	public class AssignedLicense
	{
		// Token: 0x060013F9 RID: 5113 RVA: 0x0002C13C File Offset: 0x0002A33C
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static AssignedLicense CreateAssignedLicense(Collection<Guid> disabledPlans)
		{
			AssignedLicense assignedLicense = new AssignedLicense();
			if (disabledPlans == null)
			{
				throw new ArgumentNullException("disabledPlans");
			}
			assignedLicense.disabledPlans = disabledPlans;
			return assignedLicense;
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060013FA RID: 5114 RVA: 0x0002C165 File Offset: 0x0002A365
		// (set) Token: 0x060013FB RID: 5115 RVA: 0x0002C16D File Offset: 0x0002A36D
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<Guid> disabledPlans
		{
			get
			{
				return this._disabledPlans;
			}
			set
			{
				this._disabledPlans = value;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x0002C176 File Offset: 0x0002A376
		// (set) Token: 0x060013FD RID: 5117 RVA: 0x0002C17E File Offset: 0x0002A37E
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Guid? skuId
		{
			get
			{
				return this._skuId;
			}
			set
			{
				this._skuId = value;
			}
		}

		// Token: 0x04001907 RID: 6407
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<Guid> _disabledPlans = new Collection<Guid>();

		// Token: 0x04001908 RID: 6408
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _skuId;
	}
}
