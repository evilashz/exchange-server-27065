using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000A37 RID: 2615
	[ConfigurationCollection(typeof(DenyRulePort), AddItemName = "Value")]
	public class PortCollection : ConfigurationElementCollection
	{
		// Token: 0x060038E3 RID: 14563 RVA: 0x00090FBB File Offset: 0x0008F1BB
		protected override ConfigurationElement CreateNewElement()
		{
			return new DenyRulePort();
		}

		// Token: 0x060038E4 RID: 14564 RVA: 0x00090FC2 File Offset: 0x0008F1C2
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((DenyRulePort)element).Value;
		}

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x060038E5 RID: 14565 RVA: 0x00090FD4 File Offset: 0x0008F1D4
		// (set) Token: 0x060038E6 RID: 14566 RVA: 0x00090FE6 File Offset: 0x0008F1E6
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

		// Token: 0x060038E7 RID: 14567 RVA: 0x00090FFC File Offset: 0x0008F1FC
		internal bool TryLoad()
		{
			foreach (object obj in this)
			{
				DenyRulePort denyRulePort = (DenyRulePort)obj;
				if (!this.portsSet.Contains(denyRulePort.Value))
				{
					this.portsSet.Add(denyRulePort.Value);
				}
			}
			return true;
		}

		// Token: 0x060038E8 RID: 14568 RVA: 0x00091070 File Offset: 0x0008F270
		internal bool Evaluate(HttpContext httpContext)
		{
			int item;
			if (!int.TryParse(httpContext.Request.ServerVariables["Server_Port"], out item))
			{
				ExTraceGlobals.RulesBasedHttpModuleTracer.TraceError<string>((long)this.GetHashCode(), "[PortCollection.Execute] Invalid Server Port {0}", httpContext.Request.ServerVariables["Server_Port"]);
				return this.OperatorType != DenyRuleOperator.In;
			}
			if (this.OperatorType == DenyRuleOperator.In)
			{
				return this.portsSet.Contains(item);
			}
			return !this.portsSet.Contains(item);
		}

		// Token: 0x040030DC RID: 12508
		private const string serverPortServerVariableName = "Server_Port";

		// Token: 0x040030DD RID: 12509
		private HashSet<int> portsSet = new HashSet<int>();
	}
}
