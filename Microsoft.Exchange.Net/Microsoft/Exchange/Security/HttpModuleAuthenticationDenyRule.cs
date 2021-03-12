using System;
using System.Configuration;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000A2E RID: 2606
	internal class HttpModuleAuthenticationDenyRule : ConfigurationElement
	{
		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x060038BD RID: 14525 RVA: 0x00090811 File Offset: 0x0008EA11
		[ConfigurationProperty("Name", IsRequired = true)]
		public string Name
		{
			get
			{
				return (string)base["Name"];
			}
		}

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x060038BE RID: 14526 RVA: 0x00090823 File Offset: 0x0008EA23
		[ConfigurationProperty("Description")]
		public string Description
		{
			get
			{
				return (string)base["Description"];
			}
		}

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x060038BF RID: 14527 RVA: 0x00090835 File Offset: 0x0008EA35
		[ConfigurationProperty("Execute")]
		public DenyRuleExecuteEvent ExecuteEventType
		{
			get
			{
				if (base["Execute"] == null)
				{
					return DenyRuleExecuteEvent.Always;
				}
				return (DenyRuleExecuteEvent)base["Execute"];
			}
		}

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x060038C0 RID: 14528 RVA: 0x00090856 File Offset: 0x0008EA56
		[ConfigurationProperty("Ports")]
		public PortCollection Ports
		{
			get
			{
				return (PortCollection)base["Ports"];
			}
		}

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x060038C1 RID: 14529 RVA: 0x00090868 File Offset: 0x0008EA68
		[ConfigurationProperty("IPRanges")]
		public DenyRuleIPRangeCollection IPRanges
		{
			get
			{
				return (DenyRuleIPRangeCollection)base["IPRanges"];
			}
		}

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x060038C2 RID: 14530 RVA: 0x0009087A File Offset: 0x0008EA7A
		[ConfigurationProperty("AuthSchemes")]
		public DenyRuleAuthSchemeCollection AuthSchemes
		{
			get
			{
				return (DenyRuleAuthSchemeCollection)base["AuthSchemes"];
			}
		}

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x060038C3 RID: 14531 RVA: 0x0009088C File Offset: 0x0008EA8C
		[ConfigurationProperty("UserPatterns")]
		public DenyRuleUserPatternCollection UserPatterns
		{
			get
			{
				return (DenyRuleUserPatternCollection)base["UserPatterns"];
			}
		}

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x060038C4 RID: 14532 RVA: 0x0009089E File Offset: 0x0008EA9E
		[ConfigurationProperty("CookiePatterns")]
		public DenyRuleCookiePatternCollection CookiePatterns
		{
			get
			{
				return (DenyRuleCookiePatternCollection)base["CookiePatterns"];
			}
		}

		// Token: 0x060038C5 RID: 14533 RVA: 0x000908B0 File Offset: 0x0008EAB0
		internal bool TryLoad()
		{
			this.isValid = (this.IPRanges.TryLoad() && this.Ports.TryLoad() && this.AuthSchemes.TryLoad() && this.UserPatterns.TryLoad() && this.CookiePatterns.TryLoad());
			return this.isValid;
		}

		// Token: 0x060038C6 RID: 14534 RVA: 0x0009090C File Offset: 0x0008EB0C
		internal bool Evaluate(HttpContext httpContext)
		{
			if (!this.isValid)
			{
				ExTraceGlobals.RulesBasedHttpModuleTracer.TraceError<string>((long)this.GetHashCode(), "[HttpModuleAuthenticationDenyRule.Execute] There was an error loading the rule {0}. Skipping it.", this.Name);
				return true;
			}
			return (this.IPRanges.Count <= 0 || this.IPRanges.Evaluate(httpContext)) && (this.Ports.Count <= 0 || this.Ports.Evaluate(httpContext)) && (this.UserPatterns.Count <= 0 || this.UserPatterns.Evaluate(httpContext)) && (this.AuthSchemes.Count <= 0 || this.AuthSchemes.Evaluate(httpContext)) && (this.CookiePatterns.Count <= 0 || this.CookiePatterns.Evaluate(httpContext));
		}

		// Token: 0x040030BB RID: 12475
		private bool isValid;
	}
}
