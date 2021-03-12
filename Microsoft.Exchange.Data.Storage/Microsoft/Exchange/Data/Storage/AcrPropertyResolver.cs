using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AE3 RID: 2787
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class AcrPropertyResolver
	{
		// Token: 0x17001BE8 RID: 7144
		// (get) Token: 0x06006517 RID: 25879
		internal abstract PropertyDefinition[] Dependencies { get; }

		// Token: 0x06006518 RID: 25880 RVA: 0x001AD504 File Offset: 0x001AB704
		protected void CheckDependenciesArePresent(IList<AcrPropertyProfile.ValuesToResolve> dependencies)
		{
			for (int i = 0; i < dependencies.Count; i++)
			{
				if (dependencies[i] == null)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "ACR Resolver \"{0}\" has a mandatory dependency missing: {1}", new object[]
					{
						this,
						this.Dependencies[i]
					}), "dependencies");
				}
			}
		}

		// Token: 0x06006519 RID: 25881 RVA: 0x001AD55C File Offset: 0x001AB75C
		protected void CheckValuesToResolveArePresent(IList<AcrPropertyProfile.ValuesToResolve> valuesToResolve)
		{
			for (int i = 0; i < valuesToResolve.Count; i++)
			{
				if (valuesToResolve[i] == null)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "ACR Resolver \"{0}\" has a valueToResolve #{1} missing", new object[]
					{
						this,
						i
					}), "valuesToResolve");
				}
			}
		}

		// Token: 0x0600651A RID: 25882 RVA: 0x001AD5B4 File Offset: 0x001AB7B4
		internal object[] Resolve(IList<AcrPropertyProfile.ValuesToResolve> valuesToResolve, IList<AcrPropertyProfile.ValuesToResolve> dependencies)
		{
			return this.InternalResolve(valuesToResolve, dependencies);
		}

		// Token: 0x0600651B RID: 25883
		protected abstract object[] InternalResolve(IList<AcrPropertyProfile.ValuesToResolve> valuesToResolve, IList<AcrPropertyProfile.ValuesToResolve> dependencies);
	}
}
