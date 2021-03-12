using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000A2D RID: 2605
	[ConfigurationCollection(typeof(HttpModuleAuthenticationDenyRule), AddItemName = "Rule")]
	public class HttpModuleAuthenticationDenyRulesCollection : ConfigurationElementCollection
	{
		// Token: 0x060038B7 RID: 14519 RVA: 0x0009060B File Offset: 0x0008E80B
		protected override ConfigurationElement CreateNewElement()
		{
			return new HttpModuleAuthenticationDenyRule();
		}

		// Token: 0x060038B8 RID: 14520 RVA: 0x00090612 File Offset: 0x0008E812
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((HttpModuleAuthenticationDenyRule)element).Name;
		}

		// Token: 0x060038B9 RID: 14521 RVA: 0x00090620 File Offset: 0x0008E820
		internal bool TryLoad()
		{
			if (this.isLoaded)
			{
				return this.allRulesLoadedSuccessfully;
			}
			foreach (object obj in this)
			{
				HttpModuleAuthenticationDenyRule httpModuleAuthenticationDenyRule = (HttpModuleAuthenticationDenyRule)obj;
				this.allRulesLoadedSuccessfully &= httpModuleAuthenticationDenyRule.TryLoad();
				if (httpModuleAuthenticationDenyRule.ExecuteEventType.HasFlag(DenyRuleExecuteEvent.PostAuthentication))
				{
					this.postAuthRulesCollection.Add(httpModuleAuthenticationDenyRule);
				}
				if (httpModuleAuthenticationDenyRule.ExecuteEventType.HasFlag(DenyRuleExecuteEvent.PreAuthentication))
				{
					this.preAuthRulesCollection.Add(httpModuleAuthenticationDenyRule);
				}
			}
			this.isLoaded = true;
			return this.allRulesLoadedSuccessfully;
		}

		// Token: 0x060038BA RID: 14522 RVA: 0x000906E4 File Offset: 0x0008E8E4
		internal bool EvaluatePreAuthRules(HttpContext httpContext)
		{
			if (!this.allRulesLoadedSuccessfully)
			{
				return true;
			}
			foreach (HttpModuleAuthenticationDenyRule httpModuleAuthenticationDenyRule in this.preAuthRulesCollection)
			{
				if (httpModuleAuthenticationDenyRule.Evaluate(httpContext))
				{
					ExTraceGlobals.RulesBasedHttpModuleTracer.TraceError<string>((long)this.GetHashCode(), "[HttpModuleAuthenticationDenyRulesCollection.ExecutePostAuthRules] Access denied per rule {0}.", httpModuleAuthenticationDenyRule.Name);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060038BB RID: 14523 RVA: 0x00090768 File Offset: 0x0008E968
		internal bool EvaluatePostAuthRules(HttpContext httpContext)
		{
			if (!this.allRulesLoadedSuccessfully)
			{
				return true;
			}
			foreach (HttpModuleAuthenticationDenyRule httpModuleAuthenticationDenyRule in this.postAuthRulesCollection)
			{
				if (httpModuleAuthenticationDenyRule.Evaluate(httpContext))
				{
					ExTraceGlobals.RulesBasedHttpModuleTracer.TraceError<string>((long)this.GetHashCode(), "[HttpModuleAuthenticationDenyRulesCollection.ExecutePostAuthRules] Access denied per rule {0}.", httpModuleAuthenticationDenyRule.Name);
					return true;
				}
			}
			return false;
		}

		// Token: 0x040030B7 RID: 12471
		private bool allRulesLoadedSuccessfully = true;

		// Token: 0x040030B8 RID: 12472
		private bool isLoaded;

		// Token: 0x040030B9 RID: 12473
		private List<HttpModuleAuthenticationDenyRule> preAuthRulesCollection = new List<HttpModuleAuthenticationDenyRule>();

		// Token: 0x040030BA RID: 12474
		private List<HttpModuleAuthenticationDenyRule> postAuthRulesCollection = new List<HttpModuleAuthenticationDenyRule>();
	}
}
