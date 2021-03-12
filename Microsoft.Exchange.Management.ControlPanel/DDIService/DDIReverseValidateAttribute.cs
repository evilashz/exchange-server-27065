using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000177 RID: 375
	public class DDIReverseValidateAttribute : DDIDecoratorAttribute
	{
		// Token: 0x06002231 RID: 8753 RVA: 0x00067281 File Offset: 0x00065481
		public DDIReverseValidateAttribute() : base("DDIReverseValidateAttribute")
		{
		}

		// Token: 0x17001A92 RID: 6802
		// (get) Token: 0x06002232 RID: 8754 RVA: 0x0006728E File Offset: 0x0006548E
		// (set) Token: 0x06002233 RID: 8755 RVA: 0x00067296 File Offset: 0x00065496
		public string ErrorMessage { get; set; }

		// Token: 0x06002234 RID: 8756 RVA: 0x000672A0 File Offset: 0x000654A0
		public override List<string> Validate(object target, Service profile)
		{
			List<string> result = new List<string>();
			if (target == null || string.IsNullOrEmpty(target.ToString()))
			{
				return result;
			}
			DDIValidateAttribute ddiattribute = base.GetDDIAttribute();
			if (ddiattribute == null)
			{
				throw new ArgumentException(string.Format("{0} is not a valid DDIAttribute", base.AttributeType));
			}
			List<string> list = ddiattribute.Validate(target, profile);
			if (list.Count <= 0)
			{
				return new List<string>
				{
					string.Format("{0} {1}", target, this.ErrorMessage)
				};
			}
			return result;
		}
	}
}
