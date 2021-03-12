using System;
using System.Configuration;
using System.Web;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000A3B RID: 2619
	[ConfigurationCollection(typeof(DenyRuleCookiePattern), AddItemName = "Value")]
	public class DenyRuleCookiePatternCollection : ConfigurationElementCollection
	{
		// Token: 0x060038FB RID: 14587 RVA: 0x00091486 File Offset: 0x0008F686
		protected override ConfigurationElement CreateNewElement()
		{
			return new DenyRuleCookiePattern();
		}

		// Token: 0x060038FC RID: 14588 RVA: 0x0009148D File Offset: 0x0008F68D
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((DenyRuleCookiePattern)element).Value;
		}

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x060038FD RID: 14589 RVA: 0x0009149A File Offset: 0x0008F69A
		// (set) Token: 0x060038FE RID: 14590 RVA: 0x000914AC File Offset: 0x0008F6AC
		[ConfigurationProperty("Operator", IsRequired = true)]
		public DenyRuleOperator OperatorType
		{
			get
			{
				return (DenyRuleOperator)base["Operator"];
			}
			set
			{
				base["Operator"] = value;
			}
		}

		// Token: 0x060038FF RID: 14591 RVA: 0x000914C0 File Offset: 0x0008F6C0
		internal bool TryLoad()
		{
			foreach (object obj in this)
			{
				DenyRuleCookiePattern denyRuleCookiePattern = (DenyRuleCookiePattern)obj;
				if (!denyRuleCookiePattern.TryLoad())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003900 RID: 14592 RVA: 0x0009151C File Offset: 0x0008F71C
		internal bool Evaluate(HttpContext httpContext)
		{
			if (httpContext.Request.Headers == null || httpContext.Request.Headers["Cookie"] == null)
			{
				return this.OperatorType != DenyRuleOperator.In;
			}
			if (this.OperatorType == DenyRuleOperator.In)
			{
				foreach (object obj in this)
				{
					DenyRuleCookiePattern denyRuleCookiePattern = (DenyRuleCookiePattern)obj;
					if (denyRuleCookiePattern.Evaluate(httpContext.Request.Headers["Cookie"]))
					{
						return true;
					}
				}
				return false;
			}
			foreach (object obj2 in this)
			{
				DenyRuleCookiePattern denyRuleCookiePattern2 = (DenyRuleCookiePattern)obj2;
				if (denyRuleCookiePattern2.Evaluate(httpContext.Request.Headers["Cookie"]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040030E0 RID: 12512
		private const string requestHeaderCookieFieldName = "Cookie";
	}
}
