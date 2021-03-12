using System;
using Microsoft.BDM.Pets.DNSManagement;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x0200004B RID: 75
	[Serializable]
	public sealed class WebDnsRecord : ConfigurableObject
	{
		// Token: 0x060003A5 RID: 933 RVA: 0x00010017 File Offset: 0x0000E217
		public WebDnsRecord() : base(new SimplePropertyBag())
		{
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00010024 File Offset: 0x0000E224
		internal WebDnsRecord(ResourceRecord record) : base(new SimplePropertyBag())
		{
			this.RecordType = record.RecordType.ToString();
			this.TTL = record.TTL;
			this.DomainName = record.DomainName;
			this.Value = record.Value;
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x00010076 File Offset: 0x0000E276
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return WebDnsRecord.schema;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0001007D File Offset: 0x0000E27D
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x00010084 File Offset: 0x0000E284
		// (set) Token: 0x060003AA RID: 938 RVA: 0x00010096 File Offset: 0x0000E296
		public string RecordType
		{
			get
			{
				return (string)this[WebDnsRecordSchema.RecordType];
			}
			internal set
			{
				this[WebDnsRecordSchema.RecordType] = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060003AB RID: 939 RVA: 0x000100A4 File Offset: 0x0000E2A4
		// (set) Token: 0x060003AC RID: 940 RVA: 0x000100B6 File Offset: 0x0000E2B6
		public int TTL
		{
			get
			{
				return (int)this[WebDnsRecordSchema.TTL];
			}
			internal set
			{
				this[WebDnsRecordSchema.TTL] = value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060003AD RID: 941 RVA: 0x000100C9 File Offset: 0x0000E2C9
		// (set) Token: 0x060003AE RID: 942 RVA: 0x000100DB File Offset: 0x0000E2DB
		public string DomainName
		{
			get
			{
				return (string)this[WebDnsRecordSchema.DomainName];
			}
			internal set
			{
				this[WebDnsRecordSchema.DomainName] = value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060003AF RID: 943 RVA: 0x000100E9 File Offset: 0x0000E2E9
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x000100FB File Offset: 0x0000E2FB
		public string Value
		{
			get
			{
				return (string)this[WebDnsRecordSchema.Value];
			}
			internal set
			{
				this[WebDnsRecordSchema.Value] = value;
			}
		}

		// Token: 0x04000164 RID: 356
		private static WebDnsRecordSchema schema = ObjectSchema.GetInstance<WebDnsRecordSchema>();
	}
}
