using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000A33 RID: 2611
	[ConfigurationCollection(typeof(DenyRuleAuthScheme), AddItemName = "Value")]
	public class DenyRuleAuthSchemeCollection : ConfigurationElementCollection
	{
		// Token: 0x060038C8 RID: 14536 RVA: 0x000909DE File Offset: 0x0008EBDE
		protected override ConfigurationElement CreateNewElement()
		{
			return new DenyRuleAuthScheme();
		}

		// Token: 0x060038C9 RID: 14537 RVA: 0x000909E5 File Offset: 0x0008EBE5
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((DenyRuleAuthScheme)element).Value;
		}

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x060038CA RID: 14538 RVA: 0x000909F2 File Offset: 0x0008EBF2
		// (set) Token: 0x060038CB RID: 14539 RVA: 0x00090A04 File Offset: 0x0008EC04
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

		// Token: 0x060038CC RID: 14540 RVA: 0x00090A18 File Offset: 0x0008EC18
		internal bool TryLoad()
		{
			foreach (object obj in this)
			{
				DenyRuleAuthScheme denyRuleAuthScheme = (DenyRuleAuthScheme)obj;
				if (!denyRuleAuthScheme.TryLoad())
				{
					return false;
				}
				if (!this.DenyRuleAuthSchemeSet.Contains(denyRuleAuthScheme.AuthenticationType))
				{
					this.DenyRuleAuthSchemeSet.Add(denyRuleAuthScheme.AuthenticationType);
				}
			}
			return true;
		}

		// Token: 0x060038CD RID: 14541 RVA: 0x00090A9C File Offset: 0x0008EC9C
		internal bool Evaluate(HttpContext httpContext)
		{
			DenyRuleAuthenticationType item;
			if (!Enum.TryParse<DenyRuleAuthenticationType>(httpContext.User.Identity.AuthenticationType, true, out item))
			{
				ExTraceGlobals.RulesBasedHttpModuleTracer.TraceError<string>((long)this.GetHashCode(), "[DenyRuleAuthScheme.Execute] Authentication type {0} is not supported", httpContext.User.Identity.AuthenticationType);
				return this.OperatorType != DenyRuleOperator.In;
			}
			if (this.OperatorType == DenyRuleOperator.In)
			{
				return this.DenyRuleAuthSchemeSet.Contains(item);
			}
			return !this.DenyRuleAuthSchemeSet.Contains(item);
		}

		// Token: 0x040030D8 RID: 12504
		private HashSet<DenyRuleAuthenticationType> DenyRuleAuthSchemeSet = new HashSet<DenyRuleAuthenticationType>();
	}
}
