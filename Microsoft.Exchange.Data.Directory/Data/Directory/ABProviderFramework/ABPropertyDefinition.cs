using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.ABProviderFramework
{
	// Token: 0x0200000B RID: 11
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ABPropertyDefinition : SimpleProviderPropertyDefinition
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00003028 File Offset: 0x00001228
		public ABPropertyDefinition(string name, Type type, PropertyDefinitionFlags flags, object defaultValue) : base(name, ExchangeObjectVersion.Exchange2010, type, flags, defaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None)
		{
			if ((flags & ABPropertyDefinition.UnsupportedFlags) != PropertyDefinitionFlags.None)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "ABPropertyDefinition '{0}' has unsupported flags '{1}'.", new object[]
				{
					name,
					flags
				}));
			}
			if ((flags & ABPropertyDefinition.MustHaveFlags) != ABPropertyDefinition.MustHaveFlags)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "ABPropertyDefinition '{0}' has flags '{1}' - '{2}' flags are required.", new object[]
				{
					name,
					flags,
					ABPropertyDefinition.MustHaveFlags
				}));
			}
		}

		// Token: 0x04000028 RID: 40
		private static readonly PropertyDefinitionFlags UnsupportedFlags = PropertyDefinitionFlags.Calculated | PropertyDefinitionFlags.FilterOnly | PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue | PropertyDefinitionFlags.WriteOnce;

		// Token: 0x04000029 RID: 41
		private static readonly PropertyDefinitionFlags MustHaveFlags = PropertyDefinitionFlags.ReadOnly;
	}
}
