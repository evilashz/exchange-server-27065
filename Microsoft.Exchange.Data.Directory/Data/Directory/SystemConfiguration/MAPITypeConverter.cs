using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002EF RID: 751
	internal sealed class MAPITypeConverter : TypeConverter
	{
		// Token: 0x06002304 RID: 8964 RVA: 0x00098A05 File Offset: 0x00096C05
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x00098A08 File Offset: 0x00096C08
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			if (context != null)
			{
				DetailsTemplateTypeService detailsTemplateTypeService = (DetailsTemplateTypeService)context.GetService(typeof(DetailsTemplateTypeService));
				IDetailsTemplateControlBound detailsTemplateControlBound = context.Instance as IDetailsTemplateControlBound;
				if (detailsTemplateTypeService != null && detailsTemplateTypeService.TemplateType != null && detailsTemplateTypeService.MAPIPropertiesDictionary != null && detailsTemplateControlBound != null && detailsTemplateControlBound.DetailsTemplateControl != null && detailsTemplateTypeService.MAPIPropertiesDictionary.GetControlMAPIAttributes(detailsTemplateTypeService.TemplateType, detailsTemplateControlBound.DetailsTemplateControl.GetAttributeControlType()) != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x00098A7C File Offset: 0x00096C7C
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			DetailsTemplateTypeService detailsTemplateTypeService = (DetailsTemplateTypeService)context.GetService(typeof(DetailsTemplateTypeService));
			DetailsTemplateControl detailsTemplateControl = (context.Instance as IDetailsTemplateControlBound).DetailsTemplateControl;
			ICollection<string> controlMAPIAttributes = detailsTemplateTypeService.MAPIPropertiesDictionary.GetControlMAPIAttributes(detailsTemplateTypeService.TemplateType, detailsTemplateControl.GetAttributeControlType());
			return new TypeConverter.StandardValuesCollection(controlMAPIAttributes as ICollection);
		}
	}
}
