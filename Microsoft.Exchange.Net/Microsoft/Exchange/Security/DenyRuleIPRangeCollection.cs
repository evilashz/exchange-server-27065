using System;
using System.Configuration;
using System.Net;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000A35 RID: 2613
	[ConfigurationCollection(typeof(DenyRuleIPRange), AddItemName = "Value")]
	public class DenyRuleIPRangeCollection : ConfigurationElementCollection
	{
		// Token: 0x060038D5 RID: 14549 RVA: 0x00090BE4 File Offset: 0x0008EDE4
		protected override ConfigurationElement CreateNewElement()
		{
			return new DenyRuleIPRange();
		}

		// Token: 0x060038D6 RID: 14550 RVA: 0x00090BEB File Offset: 0x0008EDEB
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((DenyRuleIPRange)element).Value;
		}

		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x060038D7 RID: 14551 RVA: 0x00090BF8 File Offset: 0x0008EDF8
		// (set) Token: 0x060038D8 RID: 14552 RVA: 0x00090C0A File Offset: 0x0008EE0A
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

		// Token: 0x060038D9 RID: 14553 RVA: 0x00090C20 File Offset: 0x0008EE20
		internal bool TryLoad()
		{
			foreach (object obj in this)
			{
				DenyRuleIPRange denyRuleIPRange = (DenyRuleIPRange)obj;
				if (!denyRuleIPRange.TryLoad())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060038DA RID: 14554 RVA: 0x00090C7C File Offset: 0x0008EE7C
		internal bool Evaluate(HttpContext httpContext)
		{
			string userHostAddress = httpContext.Request.UserHostAddress;
			IPAddress ipaddress;
			if (!IPAddress.TryParse(userHostAddress, out ipaddress))
			{
				ExTraceGlobals.RulesBasedHttpModuleTracer.TraceError<string>((long)this.GetHashCode(), "[DenyRuleIPRange.Evaluate] Invalid User Host Address {0}", httpContext.Request.UserHostAddress);
				return this.OperatorType != DenyRuleOperator.In;
			}
			long num = (long)((ulong)IPAddress.NetworkToHostOrder(BitConverter.ToInt32(ipaddress.GetAddressBytes(), 0)));
			if (this.OperatorType == DenyRuleOperator.In)
			{
				foreach (object obj in this)
				{
					DenyRuleIPRange denyRuleIPRange = (DenyRuleIPRange)obj;
					if (num <= denyRuleIPRange.EndIPAddress && num >= denyRuleIPRange.StartIPAddress)
					{
						return true;
					}
				}
				return false;
			}
			foreach (object obj2 in this)
			{
				DenyRuleIPRange denyRuleIPRange2 = (DenyRuleIPRange)obj2;
				if (num <= denyRuleIPRange2.EndIPAddress && num >= denyRuleIPRange2.StartIPAddress)
				{
					return false;
				}
			}
			return true;
		}
	}
}
