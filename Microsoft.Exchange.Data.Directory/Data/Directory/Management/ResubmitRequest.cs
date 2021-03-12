using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000748 RID: 1864
	[Serializable]
	public class ResubmitRequest : ConfigurableObject
	{
		// Token: 0x06005ABB RID: 23227 RVA: 0x0013DF6F File Offset: 0x0013C16F
		public ResubmitRequest() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x06005ABC RID: 23228 RVA: 0x0013DF7C File Offset: 0x0013C17C
		private ResubmitRequest(ResubmitRequestId identity) : base(new SimpleProviderPropertyBag())
		{
			this.propertyBag.SetObjectVersion(ExchangeObjectVersion.Exchange2010);
			this[ResubmitRequestSchema.ResubmitRequestIdentity] = identity.ToString();
		}

		// Token: 0x17001F74 RID: 8052
		// (get) Token: 0x06005ABD RID: 23229 RVA: 0x0013DFAA File Offset: 0x0013C1AA
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17001F75 RID: 8053
		// (get) Token: 0x06005ABE RID: 23230 RVA: 0x0013DFB1 File Offset: 0x0013C1B1
		public override ObjectId Identity
		{
			get
			{
				return ResubmitRequestId.Parse((string)this[ResubmitRequestSchema.ResubmitRequestIdentity]);
			}
		}

		// Token: 0x17001F76 RID: 8054
		// (get) Token: 0x06005ABF RID: 23231 RVA: 0x0013DFC8 File Offset: 0x0013C1C8
		// (set) Token: 0x06005AC0 RID: 23232 RVA: 0x0013DFDA File Offset: 0x0013C1DA
		public string Server
		{
			get
			{
				return (string)this[ResubmitRequestSchema.Server];
			}
			internal set
			{
				this[ResubmitRequestSchema.Server] = value;
			}
		}

		// Token: 0x17001F77 RID: 8055
		// (get) Token: 0x06005AC1 RID: 23233 RVA: 0x0013DFE8 File Offset: 0x0013C1E8
		// (set) Token: 0x06005AC2 RID: 23234 RVA: 0x0013DFFA File Offset: 0x0013C1FA
		public string Destination
		{
			get
			{
				return (string)this[ResubmitRequestSchema.Destination];
			}
			internal set
			{
				this[ResubmitRequestSchema.Destination] = value;
			}
		}

		// Token: 0x17001F78 RID: 8056
		// (get) Token: 0x06005AC3 RID: 23235 RVA: 0x0013E008 File Offset: 0x0013C208
		// (set) Token: 0x06005AC4 RID: 23236 RVA: 0x0013E01A File Offset: 0x0013C21A
		public DateTime StartTime
		{
			get
			{
				return (DateTime)this[ResubmitRequestSchema.StartTime];
			}
			internal set
			{
				this[ResubmitRequestSchema.StartTime] = value;
			}
		}

		// Token: 0x17001F79 RID: 8057
		// (get) Token: 0x06005AC5 RID: 23237 RVA: 0x0013E02D File Offset: 0x0013C22D
		// (set) Token: 0x06005AC6 RID: 23238 RVA: 0x0013E03F File Offset: 0x0013C23F
		public DateTime EndTime
		{
			get
			{
				return (DateTime)this[ResubmitRequestSchema.EndTime];
			}
			internal set
			{
				this[ResubmitRequestSchema.EndTime] = value;
			}
		}

		// Token: 0x17001F7A RID: 8058
		// (get) Token: 0x06005AC7 RID: 23239 RVA: 0x0013E052 File Offset: 0x0013C252
		// (set) Token: 0x06005AC8 RID: 23240 RVA: 0x0013E064 File Offset: 0x0013C264
		public string DiagnosticInformation
		{
			get
			{
				return (string)this[ResubmitRequestSchema.DiagnosticInformation];
			}
			internal set
			{
				this[ResubmitRequestSchema.DiagnosticInformation] = value;
			}
		}

		// Token: 0x17001F7B RID: 8059
		// (get) Token: 0x06005AC9 RID: 23241 RVA: 0x0013E072 File Offset: 0x0013C272
		// (set) Token: 0x06005ACA RID: 23242 RVA: 0x0013E084 File Offset: 0x0013C284
		public DateTime CreationTime
		{
			get
			{
				return (DateTime)this[ResubmitRequestSchema.CreationTime];
			}
			internal set
			{
				this[ResubmitRequestSchema.CreationTime] = value;
			}
		}

		// Token: 0x17001F7C RID: 8060
		// (get) Token: 0x06005ACB RID: 23243 RVA: 0x0013E097 File Offset: 0x0013C297
		// (set) Token: 0x06005ACC RID: 23244 RVA: 0x0013E0A9 File Offset: 0x0013C2A9
		public ResubmitRequestState State
		{
			get
			{
				return (ResubmitRequestState)this[ResubmitRequestSchema.State];
			}
			internal set
			{
				this[ResubmitRequestSchema.State] = value;
			}
		}

		// Token: 0x06005ACD RID: 23245 RVA: 0x0013E0BC File Offset: 0x0013C2BC
		internal static ResubmitRequest Create(long rowId, string server, DateTime starttime, string destination, string diagnosticInformation, DateTime endTime, DateTime dateCreated, int state)
		{
			return new ResubmitRequest(new ResubmitRequestId(rowId))
			{
				Server = server,
				StartTime = starttime,
				Destination = destination,
				DiagnosticInformation = diagnosticInformation,
				EndTime = endTime,
				CreationTime = dateCreated,
				State = (ResubmitRequestState)state
			};
		}

		// Token: 0x17001F7D RID: 8061
		// (get) Token: 0x06005ACE RID: 23246 RVA: 0x0013E10B File Offset: 0x0013C30B
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ResubmitRequest.schema;
			}
		}

		// Token: 0x17001F7E RID: 8062
		// (get) Token: 0x06005ACF RID: 23247 RVA: 0x0013E112 File Offset: 0x0013C312
		internal ResubmitRequestId ResubmitRequestId
		{
			get
			{
				return ResubmitRequestId.Parse((string)this[ResubmitRequestSchema.ResubmitRequestIdentity]);
			}
		}

		// Token: 0x04003CDD RID: 15581
		public const string IdentityParameterName = "ResubmitRequestIdentity";

		// Token: 0x04003CDE RID: 15582
		public const string DumpsterRequestEnabledName = "DumpsterRequestEnabled";

		// Token: 0x04003CDF RID: 15583
		private static ResubmitRequestSchema schema = ObjectSchema.GetInstance<ResubmitRequestSchema>();
	}
}
