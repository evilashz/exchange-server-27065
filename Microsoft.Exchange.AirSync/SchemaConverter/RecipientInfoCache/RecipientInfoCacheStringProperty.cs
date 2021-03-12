using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.RecipientInfoCache
{
	// Token: 0x020001EC RID: 492
	internal class RecipientInfoCacheStringProperty : RecipientInfoCacheProperty, IStringProperty, IProperty
	{
		// Token: 0x06001389 RID: 5001 RVA: 0x0007084C File Offset: 0x0006EA4C
		public RecipientInfoCacheStringProperty(RecipientInfoCacheEntryElements element)
		{
			switch (element)
			{
			case RecipientInfoCacheEntryElements.EmailAddress:
			case RecipientInfoCacheEntryElements.DisplayName:
			case RecipientInfoCacheEntryElements.Alias:
				base.State = PropertyState.Modified;
				this.element = element;
				return;
			default:
				throw new ArgumentException("The element " + element + " is not a string type!");
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x0600138A RID: 5002 RVA: 0x0007089C File Offset: 0x0006EA9C
		public string StringData
		{
			get
			{
				string result = null;
				switch (this.element)
				{
				case RecipientInfoCacheEntryElements.EmailAddress:
					result = this.entry.SmtpAddress;
					break;
				case RecipientInfoCacheEntryElements.DisplayName:
					result = this.entry.DisplayName;
					break;
				case RecipientInfoCacheEntryElements.Alias:
					result = this.entry.Alias;
					break;
				}
				return result;
			}
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x000708EF File Offset: 0x0006EAEF
		public override void Bind(RecipientInfoCacheEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("Entry is null!");
			}
			this.entry = entry;
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x00070908 File Offset: 0x0006EB08
		public override void CopyFrom(IProperty srcProperty)
		{
			IStringProperty stringProperty = srcProperty as IStringProperty;
			if (stringProperty == null)
			{
				throw new UnexpectedTypeException("IStringProperty", srcProperty);
			}
			if (this.entry == null)
			{
				throw new ConversionException("Haven't been bound to an item yet! Element is: " + this.element);
			}
			switch (this.element)
			{
			case RecipientInfoCacheEntryElements.EmailAddress:
				throw new NotImplementedException("Can't change the email address!");
			case RecipientInfoCacheEntryElements.DisplayName:
				this.entry.DisplayName = stringProperty.StringData;
				return;
			case RecipientInfoCacheEntryElements.Alias:
				this.entry.Alias = stringProperty.StringData;
				return;
			default:
				return;
			}
		}

		// Token: 0x04000C0C RID: 3084
		private RecipientInfoCacheEntryElements element;

		// Token: 0x04000C0D RID: 3085
		private RecipientInfoCacheEntry entry;
	}
}
