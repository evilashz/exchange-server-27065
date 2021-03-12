using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000296 RID: 662
	[Serializable]
	public abstract class RegistryObject : ConfigurableObject
	{
		// Token: 0x060017FB RID: 6139 RVA: 0x0004B13D File Offset: 0x0004933D
		public RegistryObject() : this(null)
		{
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x0004B146 File Offset: 0x00049346
		public RegistryObject(RegistryObjectId identity) : base(new SimpleProviderPropertyBag())
		{
			if (identity != null)
			{
				this.propertyBag[SimpleProviderObjectSchema.Identity] = identity;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x060017FD RID: 6141
		internal abstract RegistryObjectSchema RegistrySchema { get; }

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x060017FE RID: 6142 RVA: 0x0004B167 File Offset: 0x00049367
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return this.RegistrySchema;
			}
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x0004B16F File Offset: 0x0004936F
		internal void AddValidationError(ValidationError error)
		{
			if (this.validationErrors == null)
			{
				this.validationErrors = new List<ValidationError>();
			}
			this.validationErrors.Add(error);
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x0004B190 File Offset: 0x00049390
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (this.validationErrors != null)
			{
				errors.AddRange(this.validationErrors);
			}
			base.ValidateRead(errors);
		}

		// Token: 0x04000E37 RID: 3639
		private List<ValidationError> validationErrors;
	}
}
