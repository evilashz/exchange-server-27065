using System;
using System.Configuration;
using System.Web;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000A39 RID: 2617
	[ConfigurationCollection(typeof(DenyRuleUserPattern), AddItemName = "Value")]
	public class DenyRuleUserPatternCollection : ConfigurationElementCollection
	{
		// Token: 0x060038EE RID: 14574 RVA: 0x000911A5 File Offset: 0x0008F3A5
		protected override ConfigurationElement CreateNewElement()
		{
			return new DenyRuleUserPattern();
		}

		// Token: 0x060038EF RID: 14575 RVA: 0x000911AC File Offset: 0x0008F3AC
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((DenyRuleUserPattern)element).Value;
		}

		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x060038F0 RID: 14576 RVA: 0x000911B9 File Offset: 0x0008F3B9
		// (set) Token: 0x060038F1 RID: 14577 RVA: 0x000911CB File Offset: 0x0008F3CB
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

		// Token: 0x060038F2 RID: 14578 RVA: 0x000911E0 File Offset: 0x0008F3E0
		internal bool TryLoad()
		{
			foreach (object obj in this)
			{
				DenyRuleUserPattern denyRuleUserPattern = (DenyRuleUserPattern)obj;
				if (!denyRuleUserPattern.TryLoad())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060038F3 RID: 14579 RVA: 0x0009123C File Offset: 0x0008F43C
		internal bool Evaluate(HttpContext httpContext)
		{
			if (string.IsNullOrEmpty(httpContext.Request.ServerVariables["LOGON_USER"]))
			{
				return this.OperatorType != DenyRuleOperator.In;
			}
			if (this.OperatorType == DenyRuleOperator.In)
			{
				foreach (object obj in this)
				{
					DenyRuleUserPattern denyRuleUserPattern = (DenyRuleUserPattern)obj;
					if (denyRuleUserPattern.Evaluate(httpContext.Request.ServerVariables["LOGON_USER"]))
					{
						return true;
					}
				}
				return false;
			}
			foreach (object obj2 in this)
			{
				DenyRuleUserPattern denyRuleUserPattern2 = (DenyRuleUserPattern)obj2;
				if (denyRuleUserPattern2.Evaluate(httpContext.Request.ServerVariables["LOGON_USER"]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040030DE RID: 12510
		private const string requestHeaderLogonUserFieldName = "LOGON_USER";
	}
}
