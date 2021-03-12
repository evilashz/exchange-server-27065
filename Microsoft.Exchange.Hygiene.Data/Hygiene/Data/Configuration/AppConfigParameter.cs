using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Configuration
{
	// Token: 0x02000005 RID: 5
	internal class AppConfigParameter : ConfigurablePropertyBag
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002520 File Offset: 0x00000720
		public override ObjectId Identity
		{
			get
			{
				if (this.id == null)
				{
					object obj = this[AppConfigSchema.ParamIdProp];
					if (obj != null)
					{
						this.id = new GenericObjectId((Guid)obj);
					}
				}
				return this.id;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000255B File Offset: 0x0000075B
		// (set) Token: 0x06000033 RID: 51 RVA: 0x0000256D File Offset: 0x0000076D
		public string Name
		{
			get
			{
				return (string)this[AppConfigSchema.ParamNameProp];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length > 255)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this[AppConfigSchema.ParamNameProp] = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000025A1 File Offset: 0x000007A1
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000025B3 File Offset: 0x000007B3
		public string Value
		{
			get
			{
				return (string)this[AppConfigSchema.ParamValueProp];
			}
			set
			{
				this[AppConfigSchema.ParamValueProp] = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000025C1 File Offset: 0x000007C1
		// (set) Token: 0x06000037 RID: 55 RVA: 0x000025D8 File Offset: 0x000007D8
		public AppConfigVersion Version
		{
			get
			{
				return new AppConfigVersion((long)this[AppConfigSchema.ParamVersionProp]);
			}
			set
			{
				this[AppConfigSchema.ParamVersionProp] = value.ToInt64();
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000025F1 File Offset: 0x000007F1
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002603 File Offset: 0x00000803
		public string Description
		{
			get
			{
				return (string)this[AppConfigSchema.DescriptionProp];
			}
			set
			{
				this[AppConfigSchema.DescriptionProp] = value;
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002611 File Offset: 0x00000811
		public override Type GetSchemaType()
		{
			return typeof(AppConfigSchema);
		}

		// Token: 0x04000009 RID: 9
		private ObjectId id;
	}
}
