using System;
using System.Configuration;
using System.Net;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000A36 RID: 2614
	internal class DenyRuleIPRange : ConfigurationElement
	{
		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x060038DC RID: 14556 RVA: 0x00090DB0 File Offset: 0x0008EFB0
		internal long StartIPAddress
		{
			get
			{
				return (long)((ulong)this.startIPAddress);
			}
		}

		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x060038DD RID: 14557 RVA: 0x00090DB9 File Offset: 0x0008EFB9
		internal long EndIPAddress
		{
			get
			{
				return (long)((ulong)this.endIPAddress);
			}
		}

		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x060038DE RID: 14558 RVA: 0x00090DC2 File Offset: 0x0008EFC2
		// (set) Token: 0x060038DF RID: 14559 RVA: 0x00090DD4 File Offset: 0x0008EFD4
		[ConfigurationProperty("Value", IsRequired = true)]
		public string Value
		{
			get
			{
				return (string)base["Value"];
			}
			set
			{
				base["Value"] = value;
			}
		}

		// Token: 0x060038E0 RID: 14560 RVA: 0x00090DE2 File Offset: 0x0008EFE2
		protected override void DeserializeElement(XmlReader reader, bool s)
		{
			this.Value = (reader.ReadElementContentAs(typeof(string), null) as string);
		}

		// Token: 0x060038E1 RID: 14561 RVA: 0x00090E00 File Offset: 0x0008F000
		internal bool TryLoad()
		{
			if (string.IsNullOrEmpty(this.Value))
			{
				return false;
			}
			string[] array = this.Value.Split(new char[]
			{
				'/'
			});
			IPAddress ipaddress = null;
			if (array.Length != 2)
			{
				if (!IPAddress.TryParse(this.Value, out ipaddress))
				{
					Trace rulesBasedHttpModuleTracer = ExTraceGlobals.RulesBasedHttpModuleTracer;
					long id = (long)this.GetHashCode();
					string formatString = "[DenyRuleIPRange.TryLoad] Invalid IP Address notation {0}: {1}";
					object[] array2 = new object[2];
					array2[0] = this.Value;
					rulesBasedHttpModuleTracer.TraceError(id, formatString, array2);
					ExEventLog eventLogger = RulesBasedHttpModule.EventLogger;
					ExEventLog.EventTuple tuple_RulesBasedHttpModule_InvalidRuleConfigured = CommonEventLogConstants.Tuple_RulesBasedHttpModule_InvalidRuleConfigured;
					string value = this.Value;
					object[] array3 = new object[2];
					array3[0] = "IP Range";
					eventLogger.LogEvent(tuple_RulesBasedHttpModule_InvalidRuleConfigured, value, array3);
					return false;
				}
				uint num = (uint)IPAddress.NetworkToHostOrder(BitConverter.ToInt32(ipaddress.GetAddressBytes(), 0));
				this.startIPAddress = num;
				this.endIPAddress = num;
				return true;
			}
			else
			{
				if (!IPAddress.TryParse(array[0], out ipaddress))
				{
					ExTraceGlobals.RulesBasedHttpModuleTracer.TraceError<string>((long)this.GetHashCode(), "[DenyRuleIPRange.TryLoad] Invalid IP Address notation {0}", this.Value);
					ExEventLog eventLogger2 = RulesBasedHttpModule.EventLogger;
					ExEventLog.EventTuple tuple_RulesBasedHttpModule_InvalidRuleConfigured2 = CommonEventLogConstants.Tuple_RulesBasedHttpModule_InvalidRuleConfigured;
					string value2 = this.Value;
					object[] array4 = new object[2];
					array4[0] = "IP Range";
					eventLogger2.LogEvent(tuple_RulesBasedHttpModule_InvalidRuleConfigured2, value2, array4);
					return false;
				}
				int num2;
				if (!int.TryParse(array[1], out num2))
				{
					ExTraceGlobals.RulesBasedHttpModuleTracer.TraceError<string>((long)this.GetHashCode(), "[DenyRuleIPRange.TryLoad] Invalid Network Address Bit notation {0}", this.Value);
					ExEventLog eventLogger3 = RulesBasedHttpModule.EventLogger;
					ExEventLog.EventTuple tuple_RulesBasedHttpModule_InvalidRuleConfigured3 = CommonEventLogConstants.Tuple_RulesBasedHttpModule_InvalidRuleConfigured;
					string value3 = this.Value;
					object[] array5 = new object[2];
					array5[0] = "IP Range";
					eventLogger3.LogEvent(tuple_RulesBasedHttpModule_InvalidRuleConfigured3, value3, array5);
					return false;
				}
				uint num = (uint)IPAddress.NetworkToHostOrder(BitConverter.ToInt32(ipaddress.GetAddressBytes(), 0));
				uint num3 = uint.MaxValue;
				if (num2 == 0)
				{
					num3 = 0U;
				}
				else if (num2 < 32)
				{
					num3 <<= 32 - num2;
				}
				this.startIPAddress = (num & num3);
				this.endIPAddress = (num | ~num3);
				return true;
			}
		}

		// Token: 0x040030DA RID: 12506
		private uint startIPAddress;

		// Token: 0x040030DB RID: 12507
		private uint endIPAddress;
	}
}
