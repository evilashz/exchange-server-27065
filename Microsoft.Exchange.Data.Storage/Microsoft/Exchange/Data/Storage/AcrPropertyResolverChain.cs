using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AE4 RID: 2788
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AcrPropertyResolverChain : AcrPropertyResolver
	{
		// Token: 0x0600651D RID: 25885 RVA: 0x001AD5DC File Offset: 0x001AB7DC
		internal AcrPropertyResolverChain(AcrPropertyResolverChain.ResolutionFunction[] resolutionFunctions, PropertyDefinition[] dependencies, bool ignoreMissingDependencies)
		{
			this.resolutionFunctions = Array.FindAll<AcrPropertyResolverChain.ResolutionFunction>(resolutionFunctions, (AcrPropertyResolverChain.ResolutionFunction f) => f != null);
			this.dependencies = (dependencies ?? Array<PropertyDefinition>.Empty);
			this.ignoreMissingDependencies = ignoreMissingDependencies;
		}

		// Token: 0x17001BE9 RID: 7145
		// (get) Token: 0x0600651E RID: 25886 RVA: 0x001AD62F File Offset: 0x001AB82F
		internal override PropertyDefinition[] Dependencies
		{
			get
			{
				return this.dependencies;
			}
		}

		// Token: 0x0600651F RID: 25887 RVA: 0x001AD638 File Offset: 0x001AB838
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AcrPropertyResolverChain(");
			foreach (AcrPropertyResolverChain.ResolutionFunction del in this.resolutionFunctions)
			{
				stringBuilder.Append("{");
				stringBuilder.Append(del.GetMethodInfo().Name);
				stringBuilder.Append("}");
			}
			stringBuilder.Append(")[");
			foreach (PropertyDefinition propertyDefinition in this.dependencies)
			{
				stringBuilder.Append("{");
				stringBuilder.Append(propertyDefinition.ToString());
				stringBuilder.Append("}");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06006520 RID: 25888 RVA: 0x001AD704 File Offset: 0x001AB904
		protected override object[] InternalResolve(IList<AcrPropertyProfile.ValuesToResolve> valuesToResolve, IList<AcrPropertyProfile.ValuesToResolve> dependencies)
		{
			base.CheckValuesToResolveArePresent(valuesToResolve);
			if (!this.ignoreMissingDependencies)
			{
				base.CheckDependenciesArePresent(valuesToResolve);
			}
			foreach (AcrPropertyResolverChain.ResolutionFunction resolutionFunction in this.resolutionFunctions)
			{
				object[] array2 = null;
				try
				{
					array2 = resolutionFunction(valuesToResolve, dependencies);
				}
				catch (InvalidCastException)
				{
				}
				catch (NullReferenceException)
				{
				}
				if (array2 != null)
				{
					return array2;
				}
			}
			return null;
		}

		// Token: 0x04003995 RID: 14741
		private readonly PropertyDefinition[] dependencies;

		// Token: 0x04003996 RID: 14742
		private readonly AcrPropertyResolverChain.ResolutionFunction[] resolutionFunctions;

		// Token: 0x04003997 RID: 14743
		private readonly bool ignoreMissingDependencies;

		// Token: 0x02000AE5 RID: 2789
		// (Invoke) Token: 0x06006523 RID: 25891
		internal delegate object[] ResolutionFunction(IList<AcrPropertyProfile.ValuesToResolve> valuesToResolve, IList<AcrPropertyProfile.ValuesToResolve> dependencies);
	}
}
