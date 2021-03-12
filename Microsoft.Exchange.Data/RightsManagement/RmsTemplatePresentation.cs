using System;
using System.Globalization;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.RightsManagement
{
	// Token: 0x0200029E RID: 670
	[Serializable]
	public sealed class RmsTemplatePresentation : ConfigurableObject
	{
		// Token: 0x06001843 RID: 6211 RVA: 0x0004C4F3 File Offset: 0x0004A6F3
		public RmsTemplatePresentation() : this(new RmsTemplateIdentity())
		{
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x0004C500 File Offset: 0x0004A700
		public RmsTemplatePresentation(RmsTemplateIdentity identity) : base(new SimpleProviderPropertyBag())
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.SetIsReadOnly(true);
			this.propertyBag.SetField(SimpleProviderObjectSchema.Identity, identity);
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x0004C534 File Offset: 0x0004A734
		internal RmsTemplatePresentation(RmsTemplate template) : base(new SimpleProviderPropertyBag())
		{
			if (template == null)
			{
				throw new ArgumentNullException("template");
			}
			this.propertyBag.SetField(RmsTemplatePresentationSchema.Name, template.GetName(CultureInfo.CurrentUICulture));
			this.propertyBag.SetField(RmsTemplatePresentationSchema.Description, template.GetDescription(CultureInfo.CurrentUICulture));
			this.propertyBag.SetField(RmsTemplatePresentationSchema.Type, template.Type);
			this.propertyBag.SetField(RmsTemplatePresentationSchema.TemplateGuid, template.Id);
			this.propertyBag.SetField(SimpleProviderObjectSchema.Identity, new RmsTemplateIdentity(template.Id, template.Name, template.Type));
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x0004C5F2 File Offset: 0x0004A7F2
		public string Name
		{
			get
			{
				return (string)this[RmsTemplatePresentationSchema.Name];
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06001847 RID: 6215 RVA: 0x0004C604 File Offset: 0x0004A804
		public string Description
		{
			get
			{
				return (string)this[RmsTemplatePresentationSchema.Description];
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06001848 RID: 6216 RVA: 0x0004C616 File Offset: 0x0004A816
		// (set) Token: 0x06001849 RID: 6217 RVA: 0x0004C628 File Offset: 0x0004A828
		public RmsTemplateType Type
		{
			get
			{
				return (RmsTemplateType)this[RmsTemplatePresentationSchema.Type];
			}
			set
			{
				this[RmsTemplatePresentationSchema.Type] = value;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x0600184A RID: 6218 RVA: 0x0004C63B File Offset: 0x0004A83B
		public Guid TemplateGuid
		{
			get
			{
				return (Guid)this[RmsTemplatePresentationSchema.TemplateGuid];
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x0004C650 File Offset: 0x0004A850
		public override ObjectId Identity
		{
			get
			{
				ObjectId objectId = base.Identity;
				if (SuppressingPiiContext.NeedPiiSuppression)
				{
					objectId = (ObjectId)SuppressingPiiProperty.TryRedact(SimpleProviderObjectSchema.Identity, objectId);
				}
				return objectId;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x0600184C RID: 6220 RVA: 0x0004C67D File Offset: 0x0004A87D
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return RmsTemplatePresentation.schema;
			}
		}

		// Token: 0x04000E48 RID: 3656
		private static readonly RmsTemplatePresentationSchema schema = new RmsTemplatePresentationSchema();
	}
}
