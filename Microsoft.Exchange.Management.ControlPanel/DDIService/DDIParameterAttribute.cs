using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000175 RID: 373
	[AttributeUsage(AttributeTargets.Class)]
	public class DDIParameterAttribute : DDIValidateAttribute
	{
		// Token: 0x0600222D RID: 8749 RVA: 0x00067100 File Offset: 0x00065300
		public DDIParameterAttribute() : base("DDIParameterAttribute")
		{
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x00067110 File Offset: 0x00065310
		public override List<string> Validate(object target, Service profile)
		{
			List<string> list = new List<string>();
			if (target != null)
			{
				Parameter parameter = target as Parameter;
				if (parameter == null)
				{
					throw new ArgumentException("DDIParameterAttribute can only be applied to Parameter object");
				}
				if (!string.IsNullOrWhiteSpace(parameter.Reference) && parameter.Value != null)
				{
					list.Add(string.Format("Cannot both specify Reference and Value at the same time", target));
				}
			}
			return list;
		}
	}
}
