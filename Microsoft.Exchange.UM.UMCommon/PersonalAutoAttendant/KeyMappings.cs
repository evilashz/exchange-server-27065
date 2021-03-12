using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x02000102 RID: 258
	internal class KeyMappings
	{
		// Token: 0x06000845 RID: 2117 RVA: 0x0001FBEC File Offset: 0x0001DDEC
		internal KeyMappings()
		{
			this.binding = new Dictionary<int, KeyMappingBase>();
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x0001FC00 File Offset: 0x0001DE00
		internal KeyMappingBase[] Menu
		{
			get
			{
				KeyMappingBase[] array = new KeyMappingBase[this.binding.Keys.Count];
				this.binding.Values.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x0001FC36 File Offset: 0x0001DE36
		internal int Count
		{
			get
			{
				return this.Menu.Length;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x0001FC50 File Offset: 0x0001DE50
		internal List<KeyMappingBase> SortedMenu
		{
			get
			{
				List<KeyMappingBase> list = new List<KeyMappingBase>();
				list.AddRange(this.Menu);
				list.Sort((KeyMappingBase left, KeyMappingBase right) => left.Key - right.Key);
				return list;
			}
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001FC94 File Offset: 0x0001DE94
		internal void AddTransferToADContactMailbox(int key, string context, string legacyExchangeDN)
		{
			if (key < 1 || key > 9)
			{
				throw new ArgumentOutOfRangeException("key");
			}
			if (legacyExchangeDN == null)
			{
				throw new ArgumentNullException("legacyExchangeDN");
			}
			KeyMappingBase value = null;
			if (this.binding.TryGetValue(key, out value))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Key {0} is already bound", new object[]
				{
					key
				}));
			}
			value = KeyMappingBase.CreateTransferToADContactMailbox(key, context, legacyExchangeDN);
			this.binding[key] = value;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001FD14 File Offset: 0x0001DF14
		internal void AddTransferToADContactPhone(int key, string context, string legacyExchangeDN)
		{
			if (key < 1 || key > 9)
			{
				throw new ArgumentOutOfRangeException("key");
			}
			if (legacyExchangeDN == null)
			{
				throw new ArgumentNullException("legacyExchangeDN");
			}
			KeyMappingBase value = null;
			if (this.binding.TryGetValue(key, out value))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Key {0} is already bound", new object[]
				{
					key
				}));
			}
			value = KeyMappingBase.CreateTransferToADContactPhone(key, context, legacyExchangeDN);
			this.binding[key] = value;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001FD94 File Offset: 0x0001DF94
		internal void AddTransferToNumber(int key, string context, string number)
		{
			if (key < 1 || key > 9)
			{
				throw new ArgumentOutOfRangeException("key");
			}
			if (number == null)
			{
				throw new ArgumentNullException("number");
			}
			KeyMappingBase value = null;
			if (this.binding.TryGetValue(key, out value))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Key {0} is already bound", new object[]
				{
					key
				}));
			}
			value = KeyMappingBase.CreateTransferToNumber(key, context, number);
			this.binding[key] = value;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0001FE14 File Offset: 0x0001E014
		internal void AddTransferToVoicemail(string context)
		{
			KeyMappingBase keyMappingBase = KeyMappingBase.CreateTransferToVoicemail(context);
			this.binding[keyMappingBase.Key] = keyMappingBase;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001FE3A File Offset: 0x0001E03A
		internal void AddFindMe(int key, string context, string number, int timeout)
		{
			this.AddFindMe(key, context, number, timeout, string.Empty);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0001FE4C File Offset: 0x0001E04C
		internal void AddFindMe(int key, string context, string number, int timeout, string label)
		{
			if (key < 1 || key > 9)
			{
				throw new ArgumentOutOfRangeException("key");
			}
			if (number == null)
			{
				throw new ArgumentNullException("number");
			}
			if (timeout < 0)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			KeyMappingBase keyMappingBase = null;
			if (!this.binding.TryGetValue(key, out keyMappingBase))
			{
				keyMappingBase = KeyMappingBase.CreateFindMe(key, context, number, timeout, label);
				this.binding[key] = keyMappingBase;
				return;
			}
			if (keyMappingBase.KeyMappingType != KeyMappingTypeEnum.FindMe)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Key {0} is already bound", new object[]
				{
					key
				}));
			}
			((TransferToFindMe)keyMappingBase).AddFindMe(number, timeout, label);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0001FEF8 File Offset: 0x0001E0F8
		internal void Remove(int key)
		{
			KeyMappingBase keyMappingBase = null;
			if (!this.binding.TryGetValue(key, out keyMappingBase))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Menu with key {0} does not exist", new object[]
				{
					key
				}));
			}
			this.binding.Remove(key);
		}

		// Token: 0x040004CD RID: 1229
		private Dictionary<int, KeyMappingBase> binding;
	}
}
