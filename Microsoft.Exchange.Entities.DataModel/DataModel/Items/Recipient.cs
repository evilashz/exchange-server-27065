using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Items
{
	// Token: 0x02000027 RID: 39
	public class Recipient<TSchema> : SchematizedObject<TSchema>, IRecipient where TSchema : RecipientSchema, new()
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003200 File Offset: 0x00001400
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00003228 File Offset: 0x00001428
		public string EmailAddress
		{
			get
			{
				TSchema schema = base.Schema;
				return base.GetPropertyValueOrDefault<string>(schema.EmailAddressProperty);
			}
			set
			{
				TSchema schema = base.Schema;
				base.SetPropertyValue<string>(schema.EmailAddressProperty, value);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003250 File Offset: 0x00001450
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00003278 File Offset: 0x00001478
		public string RoutingType
		{
			get
			{
				TSchema schema = base.Schema;
				return base.GetPropertyValueOrDefault<string>(schema.RoutingTypeProperty);
			}
			set
			{
				TSchema schema = base.Schema;
				base.SetPropertyValue<string>(schema.RoutingTypeProperty, value);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000032A0 File Offset: 0x000014A0
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x000032C8 File Offset: 0x000014C8
		public string Name
		{
			get
			{
				TSchema schema = base.Schema;
				return base.GetPropertyValueOrDefault<string>(schema.NameProperty);
			}
			set
			{
				TSchema schema = base.Schema;
				base.SetPropertyValue<string>(schema.NameProperty, value);
			}
		}
	}
}
