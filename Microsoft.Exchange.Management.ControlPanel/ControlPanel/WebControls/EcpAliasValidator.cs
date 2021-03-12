using System;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005B8 RID: 1464
	public class EcpAliasValidator : EcpRegularExpressionValidator
	{
		// Token: 0x060042BA RID: 17082 RVA: 0x000CB077 File Offset: 0x000C9277
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			base.ValidationExpression = "^[!#%&'=`~\\$\\*\\+\\-\\/\\?\\^\\{\\|\\}a-zA-Z0-9_\\u00A1-\\u00FF]+(\\.[!#%&'=`~\\$\\*\\+\\-\\/\\?\\^\\{\\|\\}a-zA-Z0-9_\\u00A1-\\u00FF]+)*$";
		}

		// Token: 0x170025E5 RID: 9701
		// (get) Token: 0x060042BB RID: 17083 RVA: 0x000CB08B File Offset: 0x000C928B
		public override string TypeId
		{
			get
			{
				return "Alias";
			}
		}
	}
}
