using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.CompliancePolicy.ComplianceTask;
using Microsoft.Office.CompliancePolicy.Dar;

namespace Microsoft.Exchange.Data.Storage.UnifiedPolicy
{
	// Token: 0x02000E83 RID: 3715
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExPolicyConfigChangeHandler : PolicyConfigChangeEventHandler
	{
		// Token: 0x060080BC RID: 32956 RVA: 0x0023359C File Offset: 0x0023179C
		private ExPolicyConfigChangeHandler() : base(Activator.CreateInstance(ExPolicyConfigChangeHandler.serviceProviderType) as DarServiceProvider)
		{
		}

		// Token: 0x17002244 RID: 8772
		// (get) Token: 0x060080BD RID: 32957 RVA: 0x002335B3 File Offset: 0x002317B3
		public static ExPolicyConfigChangeHandler Current
		{
			get
			{
				return ExPolicyConfigChangeHandler.current;
			}
		}

		// Token: 0x040056EE RID: 22254
		private static Type serviceProviderType = Type.GetType("Microsoft.Office.CompliancePolicy.Exchange.Dar.ExDarServiceProvider, Microsoft.Office.CompliancePolicy.Exchange.Dar");

		// Token: 0x040056EF RID: 22255
		private static ExPolicyConfigChangeHandler current = new ExPolicyConfigChangeHandler();
	}
}
