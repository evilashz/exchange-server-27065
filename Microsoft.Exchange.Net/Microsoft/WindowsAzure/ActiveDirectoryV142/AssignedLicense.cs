using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005E1 RID: 1505
	public class AssignedLicense
	{
		// Token: 0x060018DF RID: 6367 RVA: 0x0002FEE0 File Offset: 0x0002E0E0
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

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060018E0 RID: 6368 RVA: 0x0002FF09 File Offset: 0x0002E109
		// (set) Token: 0x060018E1 RID: 6369 RVA: 0x0002FF11 File Offset: 0x0002E111
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

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x060018E2 RID: 6370 RVA: 0x0002FF1A File Offset: 0x0002E11A
		// (set) Token: 0x060018E3 RID: 6371 RVA: 0x0002FF22 File Offset: 0x0002E122
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

		// Token: 0x04001B49 RID: 6985
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<Guid> _disabledPlans = new Collection<Guid>();

		// Token: 0x04001B4A RID: 6986
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _skuId;
	}
}
