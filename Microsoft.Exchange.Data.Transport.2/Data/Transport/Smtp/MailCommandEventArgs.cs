using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x02000037 RID: 55
	public class MailCommandEventArgs : ReceiveCommandEventArgs
	{
		// Token: 0x06000144 RID: 324 RVA: 0x00005F86 File Offset: 0x00004186
		internal MailCommandEventArgs()
		{
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00005F99 File Offset: 0x00004199
		internal MailCommandEventArgs(SmtpSession smtpSession) : base(smtpSession)
		{
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00005FAD File Offset: 0x000041AD
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00005FB5 File Offset: 0x000041B5
		public string Auth
		{
			get
			{
				return this.auth;
			}
			set
			{
				this.auth = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00005FBE File Offset: 0x000041BE
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00005FC6 File Offset: 0x000041C6
		public BodyType BodyType
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00005FCF File Offset: 0x000041CF
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00005FD7 File Offset: 0x000041D7
		public string EnvelopeId
		{
			get
			{
				return this.envId;
			}
			set
			{
				this.envId = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00005FE0 File Offset: 0x000041E0
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00005FE8 File Offset: 0x000041E8
		public RoutingAddress FromAddress
		{
			get
			{
				return this.fromAddress;
			}
			set
			{
				if (!value.IsValid)
				{
					throw new ArgumentException(string.Format("The specified address is an invalid SMTP address - {0}", value));
				}
				this.fromAddress = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00006010 File Offset: 0x00004210
		public IDictionary<string, object> MailItemProperties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00006018 File Offset: 0x00004218
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00006020 File Offset: 0x00004220
		public DsnFormatRequested DsnFormatRequested
		{
			get
			{
				return this.ret;
			}
			set
			{
				this.ret = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00006029 File Offset: 0x00004229
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00006031 File Offset: 0x00004231
		public long Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000603A File Offset: 0x0000423A
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00006042 File Offset: 0x00004242
		public bool SmtpUtf8
		{
			get
			{
				return this.smtpUtf8;
			}
			set
			{
				this.smtpUtf8 = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000604B File Offset: 0x0000424B
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00006053 File Offset: 0x00004253
		public string Oorg
		{
			get
			{
				return this.oorg;
			}
			set
			{
				if (!string.IsNullOrEmpty(value) && !RoutingAddress.IsValidDomain(value))
				{
					throw new ArgumentException("Invalid originator organization value '{0}'. Originator organizations should be valid SMTP domains, like 'contoso.com'", value);
				}
				this.oorg = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00006078 File Offset: 0x00004278
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00006080 File Offset: 0x00004280
		internal Guid SystemProbeId
		{
			get
			{
				return this.systemProbeId;
			}
			set
			{
				this.systemProbeId = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00006089 File Offset: 0x00004289
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00006091 File Offset: 0x00004291
		internal RoutingAddress OriginalFromAddress
		{
			get
			{
				return this.originalFromAddress;
			}
			set
			{
				this.originalFromAddress = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000609A File Offset: 0x0000429A
		// (set) Token: 0x0600015C RID: 348 RVA: 0x000060A2 File Offset: 0x000042A2
		internal Dictionary<string, string> ConsumerMailOptionalArguments { get; set; }

		// Token: 0x0400014B RID: 331
		private string auth;

		// Token: 0x0400014C RID: 332
		private BodyType body;

		// Token: 0x0400014D RID: 333
		private string envId;

		// Token: 0x0400014E RID: 334
		private RoutingAddress fromAddress;

		// Token: 0x0400014F RID: 335
		private readonly IDictionary<string, object> properties = new Dictionary<string, object>();

		// Token: 0x04000150 RID: 336
		private DsnFormatRequested ret;

		// Token: 0x04000151 RID: 337
		private long size;

		// Token: 0x04000152 RID: 338
		private string oorg;

		// Token: 0x04000153 RID: 339
		private Guid systemProbeId;

		// Token: 0x04000154 RID: 340
		private bool smtpUtf8;

		// Token: 0x04000155 RID: 341
		private RoutingAddress originalFromAddress;
	}
}
