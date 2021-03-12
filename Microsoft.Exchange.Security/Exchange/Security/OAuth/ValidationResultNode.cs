using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x02000108 RID: 264
	[Serializable]
	public sealed class ValidationResultNode : ConfigurableObject
	{
		// Token: 0x060008AA RID: 2218 RVA: 0x000390C4 File Offset: 0x000372C4
		public ValidationResultNode(LocalizedString task, LocalizedString result, ResultType type) : base(new SimplePropertyBag(SimpleProviderObjectSchema.Identity, SimpleProviderObjectSchema.ObjectState, SimpleProviderObjectSchema.ExchangeVersion))
		{
			this[this.propertyBag.ObjectIdentityPropertyDefinition] = new ValidationResultNodeId();
			this.propertyBag.ResetChangeTracking();
			this.Task = task;
			this.Detail = result;
			this.ResultType = type;
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x00039121 File Offset: 0x00037321
		// (set) Token: 0x060008AC RID: 2220 RVA: 0x00039129 File Offset: 0x00037329
		private new ObjectId Identity { get; set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x00039132 File Offset: 0x00037332
		// (set) Token: 0x060008AE RID: 2222 RVA: 0x0003913A File Offset: 0x0003733A
		private new bool IsValid { get; set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x00039143 File Offset: 0x00037343
		// (set) Token: 0x060008B0 RID: 2224 RVA: 0x00039155 File Offset: 0x00037355
		public LocalizedString Task
		{
			get
			{
				return (LocalizedString)this[ValidationResultNodeSchema.Task];
			}
			internal set
			{
				this[ValidationResultNodeSchema.Task] = value;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x00039168 File Offset: 0x00037368
		// (set) Token: 0x060008B2 RID: 2226 RVA: 0x0003917A File Offset: 0x0003737A
		public LocalizedString Detail
		{
			get
			{
				return (LocalizedString)this[ValidationResultNodeSchema.Detail];
			}
			internal set
			{
				this[ValidationResultNodeSchema.Detail] = value;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x0003918D File Offset: 0x0003738D
		// (set) Token: 0x060008B4 RID: 2228 RVA: 0x0003919F File Offset: 0x0003739F
		public ResultType ResultType
		{
			get
			{
				return (ResultType)this[ValidationResultNodeSchema.ResultType];
			}
			internal set
			{
				this[ValidationResultNodeSchema.ResultType] = value;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x000391B2 File Offset: 0x000373B2
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ValidationResultNode.schema;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x000391B9 File Offset: 0x000373B9
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x000391C0 File Offset: 0x000373C0
		public override string ToString()
		{
			return string.Format("{0} {1} - {2}", this.Task, this.ResultType, this.Detail);
		}

		// Token: 0x040007EE RID: 2030
		private static readonly ValidationResultNodeSchema schema = ObjectSchema.GetInstance<ValidationResultNodeSchema>();
	}
}
