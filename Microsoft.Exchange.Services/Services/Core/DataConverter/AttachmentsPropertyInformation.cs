using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000207 RID: 519
	internal sealed class AttachmentsPropertyInformation : PropertyInformation
	{
		// Token: 0x06000D8F RID: 3471 RVA: 0x00043B85 File Offset: 0x00041D85
		public AttachmentsPropertyInformation() : base("Attachments", ExchangeVersion.Exchange2007, null, new PropertyUri(PropertyUriEnum.Attachments), new PropertyCommand.CreatePropertyCommand(AttachmentsProperty.CreateCommand))
		{
			this.exchange2010SP2PropertyInformationAttributes = base.PreparePropertyInformationAttributes(typeof(SetEnabledAttachmentsProperty), PropertyInformationAttributes.None);
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x00043BC2 File Offset: 0x00041DC2
		public override PropertyCommand.CreatePropertyCommand CreatePropertyCommand
		{
			get
			{
				if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010SP2))
				{
					return new PropertyCommand.CreatePropertyCommand(SetEnabledAttachmentsProperty.CreateCommand);
				}
				return base.CreatePropertyCommand;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x00043BE8 File Offset: 0x00041DE8
		public override PropertyInformationAttributes PropertyInformationAttributes
		{
			get
			{
				if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010SP2))
				{
					return this.exchange2010SP2PropertyInformationAttributes;
				}
				return base.PropertyInformationAttributes;
			}
		}

		// Token: 0x04000AA2 RID: 2722
		private readonly PropertyInformationAttributes exchange2010SP2PropertyInformationAttributes;
	}
}
