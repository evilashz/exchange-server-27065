using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001DD RID: 477
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	internal class ValidateNotEmptyGuidAttribute : ValidateArgumentsAttribute
	{
		// Token: 0x0600109B RID: 4251 RVA: 0x00032400 File Offset: 0x00030600
		protected override void Validate(object arguments, EngineIntrinsics engineIntrinsics)
		{
			if (arguments == null || Guid.Empty.Equals(arguments))
			{
				throw new ArgumentException(DataStrings.CmdletParameterEmptyValidationException);
			}
		}
	}
}
