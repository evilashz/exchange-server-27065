using System;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E8A RID: 3722
	internal class SimpleEwsPropertyProvider : EwsPropertyProvider
	{
		// Token: 0x060060ED RID: 24813 RVA: 0x0012E8E5 File Offset: 0x0012CAE5
		public SimpleEwsPropertyProvider(PropertyInformation propertyInformation) : base(propertyInformation)
		{
		}

		// Token: 0x060060EE RID: 24814 RVA: 0x0012E8EE File Offset: 0x0012CAEE
		public SimpleEwsPropertyProvider(ReadOnlyCollection<PropertyInformation> propertyInformationList) : base(propertyInformationList)
		{
		}

		// Token: 0x17001651 RID: 5713
		// (get) Token: 0x060060EF RID: 24815 RVA: 0x0012E8F7 File Offset: 0x0012CAF7
		// (set) Token: 0x060060F0 RID: 24816 RVA: 0x0012E8FF File Offset: 0x0012CAFF
		public Func<object, bool> DeletingPropertyPedicate { get; set; }

		// Token: 0x17001652 RID: 5714
		// (get) Token: 0x060060F1 RID: 24817 RVA: 0x0012E908 File Offset: 0x0012CB08
		// (set) Token: 0x060060F2 RID: 24818 RVA: 0x0012E910 File Offset: 0x0012CB10
		public Func<ServiceObject, PropertyUpdate> SetPropertyUpdateCreator { get; set; }

		// Token: 0x17001653 RID: 5715
		// (get) Token: 0x060060F3 RID: 24819 RVA: 0x0012E919 File Offset: 0x0012CB19
		// (set) Token: 0x060060F4 RID: 24820 RVA: 0x0012E921 File Offset: 0x0012CB21
		public Func<ServiceObject, PropertyUpdate> DeletePropertyUpdateCreator { get; set; }

		// Token: 0x17001654 RID: 5716
		// (get) Token: 0x060060F5 RID: 24821 RVA: 0x0012E92A File Offset: 0x0012CB2A
		// (set) Token: 0x060060F6 RID: 24822 RVA: 0x0012E932 File Offset: 0x0012CB32
		public Action<Entity, PropertyDefinition, ServiceObject, PropertyInformation> Getter { get; set; }

		// Token: 0x17001655 RID: 5717
		// (get) Token: 0x060060F7 RID: 24823 RVA: 0x0012E93B File Offset: 0x0012CB3B
		// (set) Token: 0x060060F8 RID: 24824 RVA: 0x0012E943 File Offset: 0x0012CB43
		public Action<Entity, PropertyDefinition, ServiceObject, PropertyInformation> Setter { get; set; }

		// Token: 0x17001656 RID: 5718
		// (get) Token: 0x060060F9 RID: 24825 RVA: 0x0012E94C File Offset: 0x0012CB4C
		// (set) Token: 0x060060FA RID: 24826 RVA: 0x0012E954 File Offset: 0x0012CB54
		public Func<object, string> QueryConstantBuilder { get; set; }

		// Token: 0x060060FB RID: 24827 RVA: 0x0012E960 File Offset: 0x0012CB60
		public override PropertyUpdate GetPropertyUpdate(ServiceObject ewsObject, object value)
		{
			PropertyUpdate propertyUpdate = null;
			if (this.IsDeletingProperty(value))
			{
				if (this.DeletePropertyUpdateCreator != null)
				{
					propertyUpdate = this.DeletePropertyUpdateCreator(ewsObject);
				}
			}
			else if (this.SetPropertyUpdateCreator != null)
			{
				propertyUpdate = this.SetPropertyUpdateCreator(ewsObject);
			}
			if (propertyUpdate != null)
			{
				propertyUpdate.PropertyPath = base.PropertyInformation.PropertyPath;
				return propertyUpdate;
			}
			return base.GetPropertyUpdate(ewsObject, value);
		}

		// Token: 0x060060FC RID: 24828 RVA: 0x0012E9C2 File Offset: 0x0012CBC2
		public override string GetQueryConstant(object value)
		{
			if (this.QueryConstantBuilder != null)
			{
				return this.QueryConstantBuilder(value);
			}
			return base.GetQueryConstant(value);
		}

		// Token: 0x060060FD RID: 24829 RVA: 0x0012E9E0 File Offset: 0x0012CBE0
		protected override bool IsDeletingProperty(object value)
		{
			if (this.DeletingPropertyPedicate != null)
			{
				return this.DeletingPropertyPedicate(value);
			}
			return base.IsDeletingProperty(value);
		}

		// Token: 0x060060FE RID: 24830 RVA: 0x0012E9FE File Offset: 0x0012CBFE
		protected override void GetProperty(Entity entity, PropertyDefinition property, ServiceObject ewsObject)
		{
			if (this.Getter != null)
			{
				this.Getter(entity, property, ewsObject, base.PropertyInformation);
				return;
			}
			base.GetProperty(entity, property, ewsObject);
		}

		// Token: 0x060060FF RID: 24831 RVA: 0x0012EA26 File Offset: 0x0012CC26
		protected override void SetProperty(Entity entity, PropertyDefinition property, ServiceObject ewsObject)
		{
			if (this.Setter != null)
			{
				this.Setter(entity, property, ewsObject, base.PropertyInformation);
				return;
			}
			base.SetProperty(entity, property, ewsObject);
		}
	}
}
