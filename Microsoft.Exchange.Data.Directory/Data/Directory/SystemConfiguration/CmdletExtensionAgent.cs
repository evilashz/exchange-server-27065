using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003B5 RID: 949
	[Serializable]
	public sealed class CmdletExtensionAgent : ADConfigurationObject, IComparable<CmdletExtensionAgent>
	{
		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06002B50 RID: 11088 RVA: 0x000B3E67 File Offset: 0x000B2067
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return CmdletExtensionAgent.lastModified;
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06002B51 RID: 11089 RVA: 0x000B3E6E File Offset: 0x000B206E
		internal override ADObjectSchema Schema
		{
			get
			{
				return CmdletExtensionAgent.schema;
			}
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06002B52 RID: 11090 RVA: 0x000B3E75 File Offset: 0x000B2075
		internal override string MostDerivedObjectClass
		{
			get
			{
				return CmdletExtensionAgent.mostDerivedClass;
			}
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06002B53 RID: 11091 RVA: 0x000B3E7C File Offset: 0x000B207C
		// (set) Token: 0x06002B54 RID: 11092 RVA: 0x000B3E8E File Offset: 0x000B208E
		public string Assembly
		{
			get
			{
				return (string)this[CmdletExtensionAgentSchema.Assembly];
			}
			internal set
			{
				this[CmdletExtensionAgentSchema.Assembly] = value;
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06002B55 RID: 11093 RVA: 0x000B3E9C File Offset: 0x000B209C
		// (set) Token: 0x06002B56 RID: 11094 RVA: 0x000B3EAE File Offset: 0x000B20AE
		public string ClassFactory
		{
			get
			{
				return (string)this[CmdletExtensionAgentSchema.ClassFactory];
			}
			internal set
			{
				this[CmdletExtensionAgentSchema.ClassFactory] = value;
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06002B57 RID: 11095 RVA: 0x000B3EBC File Offset: 0x000B20BC
		// (set) Token: 0x06002B58 RID: 11096 RVA: 0x000B3ECE File Offset: 0x000B20CE
		public bool Enabled
		{
			get
			{
				return (bool)this[CmdletExtensionAgentSchema.Enabled];
			}
			internal set
			{
				this[CmdletExtensionAgentSchema.Enabled] = value;
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06002B59 RID: 11097 RVA: 0x000B3EE1 File Offset: 0x000B20E1
		// (set) Token: 0x06002B5A RID: 11098 RVA: 0x000B3EF3 File Offset: 0x000B20F3
		public byte Priority
		{
			get
			{
				return (byte)this[CmdletExtensionAgentSchema.Priority];
			}
			internal set
			{
				this[CmdletExtensionAgentSchema.Priority] = value;
			}
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06002B5B RID: 11099 RVA: 0x000B3F06 File Offset: 0x000B2106
		// (set) Token: 0x06002B5C RID: 11100 RVA: 0x000B3F18 File Offset: 0x000B2118
		[Parameter(Mandatory = false)]
		public bool IsSystem
		{
			get
			{
				return (bool)this[CmdletExtensionAgentSchema.IsSystem];
			}
			set
			{
				this[CmdletExtensionAgentSchema.IsSystem] = value;
			}
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x000B3F2C File Offset: 0x000B212C
		internal static object PriorityGetter(IPropertyBag propertyBag)
		{
			int value = (int)propertyBag[CmdletExtensionAgentSchema.CmdletExtensionFlags];
			return BitConverter.GetBytes(value)[2];
		}

		// Token: 0x06002B5E RID: 11102 RVA: 0x000B3F58 File Offset: 0x000B2158
		internal static void PrioritySetter(object value, IPropertyBag propertyBag)
		{
			int value2 = (int)propertyBag[CmdletExtensionAgentSchema.CmdletExtensionFlags];
			byte[] bytes = BitConverter.GetBytes(value2);
			bytes[2] = (byte)value;
			propertyBag[CmdletExtensionAgentSchema.CmdletExtensionFlags] = BitConverter.ToInt32(bytes, 0);
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x000B3F9D File Offset: 0x000B219D
		int IComparable<CmdletExtensionAgent>.CompareTo(CmdletExtensionAgent other)
		{
			if (other != null)
			{
				return (int)(this.Priority - other.Priority);
			}
			return -1;
		}

		// Token: 0x04001A03 RID: 6659
		private static CmdletExtensionAgentSchema schema = ObjectSchema.GetInstance<CmdletExtensionAgentSchema>();

		// Token: 0x04001A04 RID: 6660
		private static string mostDerivedClass = "msExchCmdletExtensionAgent";

		// Token: 0x04001A05 RID: 6661
		private static ExchangeObjectVersion lastModified = new ExchangeObjectVersion(4, 0, 14, 1, 166, 0);
	}
}
