using System;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200009D RID: 157
	internal static class HoldCleanupExtensionMethods
	{
		// Token: 0x06000610 RID: 1552 RVA: 0x0002EC7B File Offset: 0x0002CE7B
		public static object GetValue(this object[] item, HoldCleanupEnforcer.PropertyIndex index)
		{
			return item[(int)index];
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0002EC80 File Offset: 0x0002CE80
		public static T GetValue<T>(this object[] item, HoldCleanupEnforcer.PropertyIndex index)
		{
			return (T)((object)item[(int)index]);
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0002EC8A File Offset: 0x0002CE8A
		public static object GetValue(this Item item, PropertyDefinition property)
		{
			return item.TryGetProperty(property);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0002EC94 File Offset: 0x0002CE94
		public static Stream GetStream(this Item item, PropertyDefinition property)
		{
			Stream result;
			try
			{
				result = item.OpenPropertyStream(property, PropertyOpenMode.ReadOnly);
			}
			catch (ObjectNotFoundException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0002ECC4 File Offset: 0x0002CEC4
		public static bool Compare(this Array array1, Array array2)
		{
			return array2 != null && array1.Compare(array2, Math.Max(array1.Length, array2.Length));
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0002ECE4 File Offset: 0x0002CEE4
		public static bool Compare(this Array array1, Array array2, int maxLength)
		{
			if (array2 == null)
			{
				return false;
			}
			if (array1.Length != array2.Length)
			{
				return false;
			}
			if (array1.Length < maxLength)
			{
				return false;
			}
			for (int i = 0; i < maxLength; i++)
			{
				if (!array1.GetValue(i).Equals(array2.GetValue(i)))
				{
					return false;
				}
			}
			return true;
		}
	}
}
