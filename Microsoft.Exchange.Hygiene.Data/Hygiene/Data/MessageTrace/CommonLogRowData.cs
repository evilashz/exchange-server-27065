using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200013E RID: 318
	internal class CommonLogRowData : ConfigurablePropertyBag
	{
		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x0002682C File Offset: 0x00024A2C
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(Guid.NewGuid().ToString());
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x00026851 File Offset: 0x00024A51
		// (set) Token: 0x06000C57 RID: 3159 RVA: 0x00026863 File Offset: 0x00024A63
		public string Machine
		{
			get
			{
				return (string)this[CommonLogRowData.MachineProperty];
			}
			set
			{
				this[CommonLogRowData.MachineProperty] = value;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x00026871 File Offset: 0x00024A71
		// (set) Token: 0x06000C59 RID: 3161 RVA: 0x00026883 File Offset: 0x00024A83
		public DateTime LogTime
		{
			get
			{
				return (DateTime)this[CommonLogRowData.LogTimeProperty];
			}
			set
			{
				this[CommonLogRowData.LogTimeProperty] = value;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x00026896 File Offset: 0x00024A96
		// (set) Token: 0x06000C5B RID: 3163 RVA: 0x000268A8 File Offset: 0x00024AA8
		public DateTime ItemTime
		{
			get
			{
				return (DateTime)this[CommonLogRowData.ItemTimeProperty];
			}
			set
			{
				this[CommonLogRowData.ItemTimeProperty] = value;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x000268BB File Offset: 0x00024ABB
		// (set) Token: 0x06000C5D RID: 3165 RVA: 0x000268CD File Offset: 0x00024ACD
		public Guid TenantId
		{
			get
			{
				return (Guid)this[CommonLogRowData.TenantIdProperty];
			}
			set
			{
				this[CommonLogRowData.TenantIdProperty] = value;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x000268E0 File Offset: 0x00024AE0
		// (set) Token: 0x06000C5F RID: 3167 RVA: 0x000268F2 File Offset: 0x00024AF2
		public string Agent
		{
			get
			{
				return (string)this[CommonLogRowData.AgentProperty];
			}
			set
			{
				this[CommonLogRowData.AgentProperty] = value;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x00026900 File Offset: 0x00024B00
		// (set) Token: 0x06000C61 RID: 3169 RVA: 0x00026912 File Offset: 0x00024B12
		public string Source
		{
			get
			{
				return (string)this[CommonLogRowData.SourceProperty];
			}
			set
			{
				this[CommonLogRowData.SourceProperty] = value;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x00026920 File Offset: 0x00024B20
		// (set) Token: 0x06000C63 RID: 3171 RVA: 0x00026932 File Offset: 0x00024B32
		public Guid Scope
		{
			get
			{
				return (Guid)this[CommonLogRowData.ScopeProperty];
			}
			set
			{
				this[CommonLogRowData.ScopeProperty] = value;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x00026945 File Offset: 0x00024B45
		// (set) Token: 0x06000C65 RID: 3173 RVA: 0x00026957 File Offset: 0x00024B57
		public Guid ItemId
		{
			get
			{
				return (Guid)this[CommonLogRowData.ItemIdProperty];
			}
			set
			{
				this[CommonLogRowData.ItemIdProperty] = value;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x0002696A File Offset: 0x00024B6A
		// (set) Token: 0x06000C67 RID: 3175 RVA: 0x0002697C File Offset: 0x00024B7C
		public string EventType
		{
			get
			{
				return (string)this[CommonLogRowData.EventTypeProperty];
			}
			set
			{
				this[CommonLogRowData.EventTypeProperty] = value;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x0002698A File Offset: 0x00024B8A
		// (set) Token: 0x06000C69 RID: 3177 RVA: 0x0002699C File Offset: 0x00024B9C
		public int LineId
		{
			get
			{
				return (int)this[CommonLogRowData.LineIdProperty];
			}
			set
			{
				this[CommonLogRowData.LineIdProperty] = value;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x000269AF File Offset: 0x00024BAF
		// (set) Token: 0x06000C6B RID: 3179 RVA: 0x000269C1 File Offset: 0x00024BC1
		public string CustomData
		{
			get
			{
				return (string)this[CommonLogRowData.CustomDataProperty];
			}
			set
			{
				this[CommonLogRowData.CustomDataProperty] = value;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x000269CF File Offset: 0x00024BCF
		// (set) Token: 0x06000C6D RID: 3181 RVA: 0x000269E1 File Offset: 0x00024BE1
		public string PIICustomData
		{
			get
			{
				return (string)this[CommonLogRowData.PIICustomDataProperty];
			}
			set
			{
				this[CommonLogRowData.PIICustomDataProperty] = value;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x000269EF File Offset: 0x00024BEF
		// (set) Token: 0x06000C6F RID: 3183 RVA: 0x00026A01 File Offset: 0x00024C01
		public Guid ObjectId
		{
			get
			{
				return (Guid)this[CommonLogRowData.ObjectIdProperty];
			}
			set
			{
				this[CommonLogRowData.ObjectIdProperty] = value;
			}
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00026A14 File Offset: 0x00024C14
		public override IEnumerable<PropertyDefinition> GetPropertyDefinitions(bool isChangedOnly)
		{
			return CommonLogRowData.propertydefinitions;
		}

		// Token: 0x04000615 RID: 1557
		private static readonly HygienePropertyDefinition MachineProperty = new HygienePropertyDefinition("Machine", typeof(string));

		// Token: 0x04000616 RID: 1558
		internal static readonly HygienePropertyDefinition LogTimeProperty = new HygienePropertyDefinition("LogTime", typeof(DateTime), SqlDateTime.MinValue.Value, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000617 RID: 1559
		private static readonly HygienePropertyDefinition ItemTimeProperty = new HygienePropertyDefinition("ItemTime", typeof(DateTime), SqlDateTime.MinValue.Value, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000618 RID: 1560
		internal static readonly HygienePropertyDefinition TenantIdProperty = new HygienePropertyDefinition("TenantId", typeof(Guid));

		// Token: 0x04000619 RID: 1561
		internal static readonly HygienePropertyDefinition AgentProperty = new HygienePropertyDefinition("Agent", typeof(string));

		// Token: 0x0400061A RID: 1562
		internal static readonly HygienePropertyDefinition SourceProperty = new HygienePropertyDefinition("Source", typeof(string));

		// Token: 0x0400061B RID: 1563
		private static readonly HygienePropertyDefinition ScopeProperty = new HygienePropertyDefinition("Scope", typeof(Guid));

		// Token: 0x0400061C RID: 1564
		private static readonly HygienePropertyDefinition ItemIdProperty = new HygienePropertyDefinition("ItemId", typeof(Guid));

		// Token: 0x0400061D RID: 1565
		internal static readonly HygienePropertyDefinition EventTypeProperty = new HygienePropertyDefinition("EventType", typeof(string));

		// Token: 0x0400061E RID: 1566
		internal static readonly HygienePropertyDefinition LineIdProperty = new HygienePropertyDefinition("LineId", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400061F RID: 1567
		private static readonly HygienePropertyDefinition CustomDataProperty = new HygienePropertyDefinition("CustomData", typeof(string));

		// Token: 0x04000620 RID: 1568
		private static readonly HygienePropertyDefinition PIICustomDataProperty = new HygienePropertyDefinition("PIICustomData", typeof(string));

		// Token: 0x04000621 RID: 1569
		private static readonly HygienePropertyDefinition ObjectIdProperty = new HygienePropertyDefinition("ObjectId", typeof(Guid));

		// Token: 0x04000622 RID: 1570
		private static readonly PropertyDefinition[] propertydefinitions = new PropertyDefinition[]
		{
			CommonLogRowData.MachineProperty,
			CommonLogRowData.LogTimeProperty,
			CommonLogRowData.ItemTimeProperty,
			CommonLogRowData.TenantIdProperty,
			CommonLogRowData.AgentProperty,
			CommonLogRowData.SourceProperty,
			CommonLogRowData.ScopeProperty,
			CommonLogRowData.ItemIdProperty,
			CommonLogRowData.EventTypeProperty,
			CommonLogRowData.LineIdProperty,
			CommonLogRowData.CustomDataProperty,
			CommonLogRowData.PIICustomDataProperty,
			CommonLogRowData.ObjectIdProperty
		};
	}
}
