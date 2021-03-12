using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001C7 RID: 455
	internal struct CachedProperty
	{
		// Token: 0x060014D1 RID: 5329 RVA: 0x00053851 File Offset: 0x00051A51
		public CachedProperty(ADPropertyDefinition adProperty, string extendedProperty)
		{
			this.adProperty = adProperty;
			this.extendedProperty = extendedProperty;
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x00053861 File Offset: 0x00051A61
		public ADPropertyDefinition ADProperty
		{
			get
			{
				return this.adProperty;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x00053869 File Offset: 0x00051A69
		public string ExtendedProperty
		{
			get
			{
				return this.extendedProperty;
			}
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x00053871 File Offset: 0x00051A71
		public void Set(ADRawEntry entry, TransportMailItem mailItem)
		{
			this.Set(entry, mailItem.ExtendedProperties);
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x00053880 File Offset: 0x00051A80
		public void Set(ADRawEntry entry, MailRecipient recipient)
		{
			this.Set(entry, recipient.ExtendedProperties);
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x00053890 File Offset: 0x00051A90
		private static object ProxyAddressCollectionToStringList(object data)
		{
			List<string> list = new List<string>(((ProxyAddressCollection)data).ToStringArray());
			if (list.Count != 0)
			{
				return list;
			}
			return null;
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x000538BC File Offset: 0x00051ABC
		private static object MultiValuedPropertyToList<T>(object data)
		{
			List<T> list = new List<T>((MultiValuedProperty<T>)data);
			if (list.Count != 0)
			{
				return list;
			}
			return null;
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x000538E0 File Offset: 0x00051AE0
		private static object UnlimitedToInt32(object data)
		{
			Unlimited<int> unlimited = (Unlimited<int>)data;
			int? num = null;
			if (!unlimited.IsUnlimited)
			{
				num = new int?(unlimited.Value);
			}
			return num;
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x0005391C File Offset: 0x00051B1C
		private static object UnlimitedToUInt64(object data)
		{
			Unlimited<ByteQuantifiedSize> unlimited = (Unlimited<ByteQuantifiedSize>)data;
			ulong? num = null;
			if (!unlimited.IsUnlimited)
			{
				num = new ulong?(unlimited.Value.ToBytes());
			}
			return num;
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x00053960 File Offset: 0x00051B60
		private void Set(ADRawEntry entry, IExtendedPropertyCollection properties)
		{
			object obj = entry[this.adProperty];
			if (obj == null)
			{
				return;
			}
			object obj2;
			if (obj.GetType() == typeof(SmtpAddress))
			{
				obj2 = obj.ToString();
			}
			else if (obj.GetType() == typeof(DeliveryReportsReceiver) || obj.GetType() == typeof(ExternalOofOptions) || obj.GetType() == typeof(TransportModerationNotificationFlags))
			{
				obj2 = (int)obj;
			}
			else if (obj.GetType() == typeof(Unlimited<int>))
			{
				obj2 = CachedProperty.UnlimitedToInt32(obj);
			}
			else if (obj.GetType() == typeof(Unlimited<ByteQuantifiedSize>))
			{
				obj2 = CachedProperty.UnlimitedToUInt64(obj);
			}
			else if (obj.GetType() == typeof(ProxyAddressCollection))
			{
				obj2 = CachedProperty.ProxyAddressCollectionToStringList(obj);
			}
			else if (obj.GetType() == typeof(MultiValuedProperty<string>))
			{
				obj2 = CachedProperty.MultiValuedPropertyToList<string>(obj);
			}
			else if (typeof(MultiValuedProperty<ADObjectId>).IsInstanceOfType(obj))
			{
				obj2 = CachedProperty.MultiValuedPropertyToList<ADObjectId>(obj);
			}
			else if (obj is ProxyAddress)
			{
				obj2 = obj.ToString();
			}
			else
			{
				obj2 = obj;
			}
			if (obj2 != null)
			{
				properties.SetValue<object>(this.extendedProperty, obj2);
			}
		}

		// Token: 0x04000A80 RID: 2688
		private ADPropertyDefinition adProperty;

		// Token: 0x04000A81 RID: 2689
		private string extendedProperty;
	}
}
