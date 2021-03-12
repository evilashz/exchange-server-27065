using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001EA RID: 490
	internal sealed class AdminAuditLogEventFacade : ConfigurableObject
	{
		// Token: 0x060010EA RID: 4330 RVA: 0x000338E6 File Offset: 0x00031AE6
		public AdminAuditLogEventFacade() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x000338F3 File Offset: 0x00031AF3
		public AdminAuditLogEventFacade(ConfigObjectId identity) : base(new SimpleProviderPropertyBag())
		{
			this.propertyBag[SimpleProviderObjectSchema.Identity] = identity;
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x00033911 File Offset: 0x00031B11
		// (set) Token: 0x060010ED RID: 4333 RVA: 0x00033928 File Offset: 0x00031B28
		public string ObjectModified
		{
			get
			{
				return this.propertyBag[AdminAuditLogFacadeSchema.ObjectModified] as string;
			}
			set
			{
				this.propertyBag[AdminAuditLogFacadeSchema.ObjectModified] = value;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x0003393B File Offset: 0x00031B3B
		// (set) Token: 0x060010EF RID: 4335 RVA: 0x00033952 File Offset: 0x00031B52
		internal string ModifiedObjectResolvedName
		{
			get
			{
				return this.propertyBag[AdminAuditLogFacadeSchema.ModifiedObjectResolvedName] as string;
			}
			set
			{
				this.propertyBag[AdminAuditLogFacadeSchema.ModifiedObjectResolvedName] = value;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x00033965 File Offset: 0x00031B65
		// (set) Token: 0x060010F1 RID: 4337 RVA: 0x0003397C File Offset: 0x00031B7C
		public string CmdletName
		{
			get
			{
				return this.propertyBag[AdminAuditLogFacadeSchema.CmdletName] as string;
			}
			set
			{
				this.propertyBag[AdminAuditLogFacadeSchema.CmdletName] = value;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x0003398F File Offset: 0x00031B8F
		// (set) Token: 0x060010F3 RID: 4339 RVA: 0x000339A6 File Offset: 0x00031BA6
		public MultiValuedProperty<AdminAuditLogCmdletParameter> CmdletParameters
		{
			get
			{
				return this.propertyBag[AdminAuditLogFacadeSchema.CmdletParameters] as MultiValuedProperty<AdminAuditLogCmdletParameter>;
			}
			set
			{
				this.propertyBag[AdminAuditLogFacadeSchema.CmdletParameters] = value;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060010F4 RID: 4340 RVA: 0x000339B9 File Offset: 0x00031BB9
		// (set) Token: 0x060010F5 RID: 4341 RVA: 0x000339D0 File Offset: 0x00031BD0
		public MultiValuedProperty<AdminAuditLogModifiedProperty> ModifiedProperties
		{
			get
			{
				return this.propertyBag[AdminAuditLogFacadeSchema.ModifiedProperties] as MultiValuedProperty<AdminAuditLogModifiedProperty>;
			}
			set
			{
				this.propertyBag[AdminAuditLogFacadeSchema.ModifiedProperties] = value;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x000339E3 File Offset: 0x00031BE3
		// (set) Token: 0x060010F7 RID: 4343 RVA: 0x000339FA File Offset: 0x00031BFA
		public string Caller
		{
			get
			{
				return this.propertyBag[AdminAuditLogFacadeSchema.Caller] as string;
			}
			set
			{
				this.propertyBag[AdminAuditLogFacadeSchema.Caller] = value;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060010F8 RID: 4344 RVA: 0x00033A0D File Offset: 0x00031C0D
		// (set) Token: 0x060010F9 RID: 4345 RVA: 0x00033A29 File Offset: 0x00031C29
		public bool? Succeeded
		{
			get
			{
				return this.propertyBag[AdminAuditLogFacadeSchema.Succeeded] as bool?;
			}
			set
			{
				this.propertyBag[AdminAuditLogFacadeSchema.Succeeded] = value;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x00033A41 File Offset: 0x00031C41
		// (set) Token: 0x060010FB RID: 4347 RVA: 0x00033A58 File Offset: 0x00031C58
		public string Error
		{
			get
			{
				return this.propertyBag[AdminAuditLogFacadeSchema.Error] as string;
			}
			set
			{
				this.propertyBag[AdminAuditLogFacadeSchema.Error] = value;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x00033A6B File Offset: 0x00031C6B
		// (set) Token: 0x060010FD RID: 4349 RVA: 0x00033A87 File Offset: 0x00031C87
		public DateTime? RunDate
		{
			get
			{
				return this.propertyBag[AdminAuditLogFacadeSchema.RunDate] as DateTime?;
			}
			set
			{
				this.propertyBag[AdminAuditLogFacadeSchema.RunDate] = value;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x00033A9F File Offset: 0x00031C9F
		// (set) Token: 0x060010FF RID: 4351 RVA: 0x00033AB6 File Offset: 0x00031CB6
		public string OriginatingServer
		{
			get
			{
				return this.propertyBag[AdminAuditLogFacadeSchema.OriginatingServer] as string;
			}
			set
			{
				this.propertyBag[AdminAuditLogFacadeSchema.OriginatingServer] = value;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001100 RID: 4352 RVA: 0x00033AC9 File Offset: 0x00031CC9
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return AdminAuditLogEventFacade.schema;
			}
		}

		// Token: 0x04000A7C RID: 2684
		private static readonly ObjectSchema schema = ObjectSchema.GetInstance<AdminAuditLogFacadeSchema>();
	}
}
