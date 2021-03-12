using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x020005BA RID: 1466
	public class AssignedLicense
	{
		// Token: 0x06001654 RID: 5716 RVA: 0x0002DEC8 File Offset: 0x0002C0C8
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

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001655 RID: 5717 RVA: 0x0002DEF1 File Offset: 0x0002C0F1
		// (set) Token: 0x06001656 RID: 5718 RVA: 0x0002DEF9 File Offset: 0x0002C0F9
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

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x0002DF02 File Offset: 0x0002C102
		// (set) Token: 0x06001658 RID: 5720 RVA: 0x0002DF0A File Offset: 0x0002C10A
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

		// Token: 0x04001A1F RID: 6687
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<Guid> _disabledPlans = new Collection<Guid>();

		// Token: 0x04001A20 RID: 6688
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _skuId;
	}
}
