using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions
{
	// Token: 0x0200003C RID: 60
	public sealed class ExpandEventParametersSchema : TypeSchema
	{
		// Token: 0x06000135 RID: 309 RVA: 0x00003BA4 File Offset: 0x00001DA4
		public ExpandEventParametersSchema()
		{
			base.RegisterPropertyDefinition(ExpandEventParametersSchema.StaticReturnMasterProperty);
			base.RegisterPropertyDefinition(ExpandEventParametersSchema.StaticReturnRegularOccurrencesProperty);
			base.RegisterPropertyDefinition(ExpandEventParametersSchema.StaticReturnExceptionsProperty);
			base.RegisterPropertyDefinition(ExpandEventParametersSchema.StaticReturnCancellationsProperty);
			base.RegisterPropertyDefinition(ExpandEventParametersSchema.StaticWindowStartProperty);
			base.RegisterPropertyDefinition(ExpandEventParametersSchema.StaticWindowEndProperty);
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00003BF9 File Offset: 0x00001DF9
		public TypedPropertyDefinition<bool> ReturnMasterProperty
		{
			get
			{
				return ExpandEventParametersSchema.StaticReturnMasterProperty;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00003C00 File Offset: 0x00001E00
		public TypedPropertyDefinition<bool> ReturnRegularOccurrencesProperty
		{
			get
			{
				return ExpandEventParametersSchema.StaticReturnRegularOccurrencesProperty;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00003C07 File Offset: 0x00001E07
		public TypedPropertyDefinition<bool> ReturnExceptionsProperty
		{
			get
			{
				return ExpandEventParametersSchema.StaticReturnExceptionsProperty;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00003C0E File Offset: 0x00001E0E
		public TypedPropertyDefinition<bool> ReturnCancellationsProperty
		{
			get
			{
				return ExpandEventParametersSchema.StaticReturnCancellationsProperty;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00003C15 File Offset: 0x00001E15
		public TypedPropertyDefinition<ExDateTime> WindowStartProperty
		{
			get
			{
				return ExpandEventParametersSchema.StaticWindowStartProperty;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00003C1C File Offset: 0x00001E1C
		public TypedPropertyDefinition<ExDateTime> WindowEndProperty
		{
			get
			{
				return ExpandEventParametersSchema.StaticWindowEndProperty;
			}
		}

		// Token: 0x0400007F RID: 127
		private static readonly TypedPropertyDefinition<bool> StaticReturnMasterProperty = new TypedPropertyDefinition<bool>("ExpandEvent.ReturnMaster", false, true);

		// Token: 0x04000080 RID: 128
		private static readonly TypedPropertyDefinition<bool> StaticReturnRegularOccurrencesProperty = new TypedPropertyDefinition<bool>("ExpandEvent.ReturnRegularOccurrences", false, true);

		// Token: 0x04000081 RID: 129
		private static readonly TypedPropertyDefinition<bool> StaticReturnExceptionsProperty = new TypedPropertyDefinition<bool>("ExpandEvent.ReturnExceptions", false, true);

		// Token: 0x04000082 RID: 130
		private static readonly TypedPropertyDefinition<bool> StaticReturnCancellationsProperty = new TypedPropertyDefinition<bool>("ExpandEvent.ReturnCancellations", false, true);

		// Token: 0x04000083 RID: 131
		private static readonly TypedPropertyDefinition<ExDateTime> StaticWindowStartProperty = new TypedPropertyDefinition<ExDateTime>("ExpandEvent.WindowStart", default(ExDateTime), true);

		// Token: 0x04000084 RID: 132
		private static readonly TypedPropertyDefinition<ExDateTime> StaticWindowEndProperty = new TypedPropertyDefinition<ExDateTime>("ExpandEvent.WindowEnd", default(ExDateTime), true);
	}
}
