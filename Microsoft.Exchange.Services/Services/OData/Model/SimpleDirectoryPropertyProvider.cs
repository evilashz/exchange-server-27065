using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E8C RID: 3724
	internal class SimpleDirectoryPropertyProvider : DirectoryPropertyProvider
	{
		// Token: 0x06006108 RID: 24840 RVA: 0x0012EB18 File Offset: 0x0012CD18
		public SimpleDirectoryPropertyProvider(ADPropertyDefinition adPropertyDefinition) : base(adPropertyDefinition)
		{
		}

		// Token: 0x17001658 RID: 5720
		// (get) Token: 0x06006109 RID: 24841 RVA: 0x0012EB21 File Offset: 0x0012CD21
		// (set) Token: 0x0600610A RID: 24842 RVA: 0x0012EB29 File Offset: 0x0012CD29
		public Action<Entity, PropertyDefinition, ADRawEntry, ADPropertyDefinition> Getter { get; set; }

		// Token: 0x17001659 RID: 5721
		// (get) Token: 0x0600610B RID: 24843 RVA: 0x0012EB32 File Offset: 0x0012CD32
		// (set) Token: 0x0600610C RID: 24844 RVA: 0x0012EB3A File Offset: 0x0012CD3A
		public Action<Entity, PropertyDefinition, ADRawEntry, ADPropertyDefinition> Setter { get; set; }

		// Token: 0x1700165A RID: 5722
		// (get) Token: 0x0600610D RID: 24845 RVA: 0x0012EB43 File Offset: 0x0012CD43
		// (set) Token: 0x0600610E RID: 24846 RVA: 0x0012EB4B File Offset: 0x0012CD4B
		public Func<object, object> QueryConstantBuilder { get; set; }

		// Token: 0x0600610F RID: 24847 RVA: 0x0012EB54 File Offset: 0x0012CD54
		public override object GetQueryConstant(object value)
		{
			if (this.QueryConstantBuilder != null)
			{
				return this.QueryConstantBuilder(value);
			}
			return base.GetQueryConstant(value);
		}

		// Token: 0x06006110 RID: 24848 RVA: 0x0012EB72 File Offset: 0x0012CD72
		protected override void GetProperty(Entity entity, PropertyDefinition property, ADRawEntry adObject)
		{
			if (this.Getter != null)
			{
				this.Getter(entity, property, adObject, base.ADPropertyDefinition);
				return;
			}
			base.GetProperty(entity, property, adObject);
		}

		// Token: 0x06006111 RID: 24849 RVA: 0x0012EB9A File Offset: 0x0012CD9A
		protected override void SetProperty(Entity entity, PropertyDefinition property, ADRawEntry adObject)
		{
			if (this.Setter != null)
			{
				this.Setter(entity, property, adObject, base.ADPropertyDefinition);
				return;
			}
			base.GetProperty(entity, property, adObject);
		}
	}
}
