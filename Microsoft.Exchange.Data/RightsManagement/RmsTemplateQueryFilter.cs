using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.RightsManagement
{
	// Token: 0x0200029F RID: 671
	internal sealed class RmsTemplateQueryFilter : QueryFilter
	{
		// Token: 0x0600184E RID: 6222 RVA: 0x0004C690 File Offset: 0x0004A890
		public RmsTemplateQueryFilter(Guid id, string name)
		{
			this.id = id;
			this.name = name;
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x0004C6A8 File Offset: 0x0004A8A8
		public override void ToString(StringBuilder sb)
		{
			string arg = string.IsNullOrEmpty(this.name) ? "*" : this.name;
			sb.AppendFormat("(Id={0}) || (Name={1})", this.id, arg);
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x0004C6E8 File Offset: 0x0004A8E8
		internal bool Match(RmsTemplate template)
		{
			if (template == null)
			{
				return false;
			}
			if (this.id.Equals(template.Id) || string.IsNullOrEmpty(this.name) || string.Equals(this.name, template.Name, StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			foreach (CultureInfo locale in RmsTemplate.SupportedClientLanguages)
			{
				if (string.Equals(this.name, template.GetName(locale), StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000E49 RID: 3657
		internal static readonly RmsTemplateQueryFilter MatchAll = new RmsTemplateQueryFilter(Guid.Empty, null);

		// Token: 0x04000E4A RID: 3658
		private Guid id;

		// Token: 0x04000E4B RID: 3659
		private string name;
	}
}
