using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001D0 RID: 464
	internal class UMPartnerFaxContext : UMPartnerContext
	{
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x000317F7 File Offset: 0x0002F9F7
		// (set) Token: 0x0600103F RID: 4159 RVA: 0x0003180E File Offset: 0x0002FA0E
		public string CallId
		{
			get
			{
				return (string)base[UMPartnerFaxContext.Schema.CallId];
			}
			set
			{
				base[UMPartnerFaxContext.Schema.CallId] = value;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x00031821 File Offset: 0x0002FA21
		// (set) Token: 0x06001041 RID: 4161 RVA: 0x00031838 File Offset: 0x0002FA38
		public string CallerId
		{
			get
			{
				return (string)base[UMPartnerFaxContext.Schema.CallerId];
			}
			set
			{
				base[UMPartnerFaxContext.Schema.CallerId] = value;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x0003184B File Offset: 0x0002FA4B
		// (set) Token: 0x06001043 RID: 4163 RVA: 0x00031862 File Offset: 0x0002FA62
		public string Extension
		{
			get
			{
				return (string)base[UMPartnerFaxContext.Schema.Extension];
			}
			set
			{
				base[UMPartnerFaxContext.Schema.Extension] = value;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x00031875 File Offset: 0x0002FA75
		// (set) Token: 0x06001045 RID: 4165 RVA: 0x0003188C File Offset: 0x0002FA8C
		public string PhoneContext
		{
			get
			{
				return (string)base[UMPartnerFaxContext.Schema.PhoneContext];
			}
			set
			{
				base[UMPartnerFaxContext.Schema.PhoneContext] = value;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x0003189F File Offset: 0x0002FA9F
		// (set) Token: 0x06001047 RID: 4167 RVA: 0x000318B6 File Offset: 0x0002FAB6
		public string CallerIdDisplayName
		{
			get
			{
				return (string)base[UMPartnerFaxContext.Schema.CallerIdDisplayName];
			}
			set
			{
				base[UMPartnerFaxContext.Schema.CallerIdDisplayName] = value;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x000318C9 File Offset: 0x0002FAC9
		protected override UMPartnerContext.UMPartnerContextSchema ContextSchema
		{
			get
			{
				return UMPartnerFaxContext.Schema;
			}
		}

		// Token: 0x040009A6 RID: 2470
		private static readonly UMPartnerFaxContext.UMPartnerFaxContextSchema Schema = new UMPartnerFaxContext.UMPartnerFaxContextSchema();

		// Token: 0x020001D1 RID: 465
		private class UMPartnerFaxContextSchema : UMPartnerContext.UMPartnerContextSchema
		{
			// Token: 0x040009A7 RID: 2471
			public UMPartnerContext.UMPartnerContextPropertyDefinition CallId = new UMPartnerContext.UMPartnerContextPropertyDefinition("CallId", typeof(string), string.Empty);

			// Token: 0x040009A8 RID: 2472
			public UMPartnerContext.UMPartnerContextPropertyDefinition CallerId = new UMPartnerContext.UMPartnerContextPropertyDefinition("CallerId", typeof(string), string.Empty);

			// Token: 0x040009A9 RID: 2473
			public UMPartnerContext.UMPartnerContextPropertyDefinition Extension = new UMPartnerContext.UMPartnerContextPropertyDefinition("Extension", typeof(string), string.Empty);

			// Token: 0x040009AA RID: 2474
			public UMPartnerContext.UMPartnerContextPropertyDefinition PhoneContext = new UMPartnerContext.UMPartnerContextPropertyDefinition("PhoneContext", typeof(string), string.Empty);

			// Token: 0x040009AB RID: 2475
			public readonly UMPartnerContext.UMPartnerContextPropertyDefinition CallerIdDisplayName = new UMPartnerContext.UMPartnerContextPropertyDefinition("CallerIdDisplayName", typeof(string), string.Empty);
		}
	}
}
