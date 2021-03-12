using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000184 RID: 388
	[Serializable]
	public class UMPrompt : ConfigurableObject
	{
		// Token: 0x06000C67 RID: 3175 RVA: 0x0002DA16 File Offset: 0x0002BC16
		public UMPrompt(ObjectId identity) : base(new SimpleProviderPropertyBag())
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.propertyBag.SetField(SimpleProviderObjectSchema.Identity, identity);
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x0002DA43 File Offset: 0x0002BC43
		// (set) Token: 0x06000C69 RID: 3177 RVA: 0x0002DA55 File Offset: 0x0002BC55
		public byte[] AudioData
		{
			get
			{
				return (byte[])this[UMPromptSchema.AudioData];
			}
			set
			{
				this[UMPromptSchema.AudioData] = value;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x0002DA63 File Offset: 0x0002BC63
		// (set) Token: 0x06000C6B RID: 3179 RVA: 0x0002DA75 File Offset: 0x0002BC75
		public string Name
		{
			get
			{
				return (string)this[UMPromptSchema.Name];
			}
			set
			{
				this[UMPromptSchema.Name] = value;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x0002DA83 File Offset: 0x0002BC83
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return UMPrompt.schema;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x0002DA8A File Offset: 0x0002BC8A
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x040006B4 RID: 1716
		private static UMPromptSchema schema = ObjectSchema.GetInstance<UMPromptSchema>();
	}
}
