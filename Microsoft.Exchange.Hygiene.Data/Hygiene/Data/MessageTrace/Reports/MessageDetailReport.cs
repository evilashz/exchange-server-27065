using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports
{
	// Token: 0x02000194 RID: 404
	internal class MessageDetailReport : Schema
	{
		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060010A4 RID: 4260 RVA: 0x00033F5C File Offset: 0x0003215C
		// (set) Token: 0x060010A5 RID: 4261 RVA: 0x00033F6E File Offset: 0x0003216E
		public string Organization
		{
			get
			{
				return (string)this[MessageDetailReport.OrganizationDefinition];
			}
			set
			{
				this[MessageDetailReport.OrganizationDefinition] = value;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060010A6 RID: 4262 RVA: 0x00033F7C File Offset: 0x0003217C
		// (set) Token: 0x060010A7 RID: 4263 RVA: 0x00033F8E File Offset: 0x0003218E
		public string Domain
		{
			get
			{
				return (string)this[MessageDetailReport.DomainDefinition];
			}
			set
			{
				this[MessageDetailReport.DomainDefinition] = value;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060010A8 RID: 4264 RVA: 0x00033F9C File Offset: 0x0003219C
		// (set) Token: 0x060010A9 RID: 4265 RVA: 0x00033FAE File Offset: 0x000321AE
		public DateTime Date
		{
			get
			{
				return (DateTime)this[MessageDetailReport.DateDefinition];
			}
			set
			{
				this[MessageDetailReport.DateDefinition] = value;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060010AA RID: 4266 RVA: 0x00033FC1 File Offset: 0x000321C1
		// (set) Token: 0x060010AB RID: 4267 RVA: 0x00033FD3 File Offset: 0x000321D3
		public Guid InternalMessageId
		{
			get
			{
				return (Guid)this[MessageDetailReport.InternalMessageIdDefinition];
			}
			set
			{
				this[MessageDetailReport.InternalMessageIdDefinition] = value;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060010AC RID: 4268 RVA: 0x00033FE6 File Offset: 0x000321E6
		// (set) Token: 0x060010AD RID: 4269 RVA: 0x00033FF8 File Offset: 0x000321F8
		public string MessageId
		{
			get
			{
				return (string)this[MessageDetailReport.MessageIdDefinition];
			}
			set
			{
				this[MessageDetailReport.MessageIdDefinition] = value;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060010AE RID: 4270 RVA: 0x00034006 File Offset: 0x00032206
		// (set) Token: 0x060010AF RID: 4271 RVA: 0x00034018 File Offset: 0x00032218
		public string Direction
		{
			get
			{
				return (string)this[MessageDetailReport.DirectionDefinition];
			}
			set
			{
				this[MessageDetailReport.DirectionDefinition] = value;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060010B0 RID: 4272 RVA: 0x00034026 File Offset: 0x00032226
		// (set) Token: 0x060010B1 RID: 4273 RVA: 0x00034038 File Offset: 0x00032238
		public string RecipientAddress
		{
			get
			{
				return (string)this[MessageDetailReport.RecipientAddressDefinition];
			}
			set
			{
				this[MessageDetailReport.RecipientAddressDefinition] = value;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060010B2 RID: 4274 RVA: 0x00034046 File Offset: 0x00032246
		// (set) Token: 0x060010B3 RID: 4275 RVA: 0x00034058 File Offset: 0x00032258
		public string SenderAddress
		{
			get
			{
				return (string)this[MessageDetailReport.SenderAddressDefinition];
			}
			set
			{
				this[MessageDetailReport.SenderAddressDefinition] = value;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060010B4 RID: 4276 RVA: 0x00034066 File Offset: 0x00032266
		// (set) Token: 0x060010B5 RID: 4277 RVA: 0x00034078 File Offset: 0x00032278
		public string SenderIP
		{
			get
			{
				return (string)this[MessageDetailReport.SenderIPDefinition];
			}
			set
			{
				this[MessageDetailReport.SenderIPDefinition] = value;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060010B6 RID: 4278 RVA: 0x00034086 File Offset: 0x00032286
		// (set) Token: 0x060010B7 RID: 4279 RVA: 0x00034098 File Offset: 0x00032298
		public string Subject
		{
			get
			{
				return (string)this[MessageDetailReport.SubjectDefinition];
			}
			set
			{
				this[MessageDetailReport.SubjectDefinition] = value;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060010B8 RID: 4280 RVA: 0x000340A6 File Offset: 0x000322A6
		// (set) Token: 0x060010B9 RID: 4281 RVA: 0x000340B8 File Offset: 0x000322B8
		public int MessageSize
		{
			get
			{
				return (int)this[MessageDetailReport.MessageSizeDefinition];
			}
			set
			{
				this[MessageDetailReport.MessageSizeDefinition] = value;
			}
		}

		// Token: 0x040007F3 RID: 2035
		internal static readonly HygienePropertyDefinition OrganizationDefinition = new HygienePropertyDefinition("OrganizationalUnitRootId", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007F4 RID: 2036
		internal static readonly HygienePropertyDefinition DomainDefinition = new HygienePropertyDefinition("DomainName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007F5 RID: 2037
		internal static readonly HygienePropertyDefinition DateDefinition = new HygienePropertyDefinition("Date", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007F6 RID: 2038
		internal static readonly HygienePropertyDefinition InternalMessageIdDefinition = new HygienePropertyDefinition("InternalMessageId", typeof(Guid));

		// Token: 0x040007F7 RID: 2039
		internal static readonly HygienePropertyDefinition MessageIdDefinition = new HygienePropertyDefinition("nvc_ClientMessageId", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007F8 RID: 2040
		internal static readonly HygienePropertyDefinition DirectionDefinition = new HygienePropertyDefinition("Direction", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007F9 RID: 2041
		internal static readonly HygienePropertyDefinition RecipientAddressDefinition = new HygienePropertyDefinition("RecipientAddress", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007FA RID: 2042
		internal static readonly HygienePropertyDefinition SenderIPDefinition = new HygienePropertyDefinition("SenderIP", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007FB RID: 2043
		internal static readonly HygienePropertyDefinition SenderAddressDefinition = new HygienePropertyDefinition("SenderAddress", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007FC RID: 2044
		internal static readonly HygienePropertyDefinition SubjectDefinition = new HygienePropertyDefinition("Subject", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007FD RID: 2045
		internal static readonly HygienePropertyDefinition MessageSizeDefinition = new HygienePropertyDefinition("MessageSize", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
