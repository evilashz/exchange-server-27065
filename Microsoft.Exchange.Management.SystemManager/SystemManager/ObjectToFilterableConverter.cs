using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000027 RID: 39
	public class ObjectToFilterableConverter : IFilterableConverter
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x00007D50 File Offset: 0x00005F50
		public virtual bool ShouldUseStandardFiltering(Type type)
		{
			return typeof(bool).IsAssignableFrom(type) || typeof(byte).IsAssignableFrom(type) || typeof(char).IsAssignableFrom(type) || typeof(DateTime).IsAssignableFrom(type) || typeof(decimal).IsAssignableFrom(type) || typeof(double).IsAssignableFrom(type) || typeof(short).IsAssignableFrom(type) || typeof(int).IsAssignableFrom(type) || typeof(long).IsAssignableFrom(type) || typeof(sbyte).IsAssignableFrom(type) || typeof(float).IsAssignableFrom(type) || typeof(string).IsAssignableFrom(type) || typeof(ushort).IsAssignableFrom(type) || typeof(uint).IsAssignableFrom(type) || typeof(ulong).IsAssignableFrom(type) || typeof(Enum).IsAssignableFrom(type);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00007E98 File Offset: 0x00006098
		public virtual IConvertible ToFilterable(object item)
		{
			if (this.IsNullValue(item))
			{
				return null;
			}
			if (this.ShouldUseStandardFiltering(item.GetType()))
			{
				return item as IConvertible;
			}
			if (item is ADObjectId)
			{
				return (item as ADObjectId).ToDNString();
			}
			return item.ToUserFriendText();
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00007ED4 File Offset: 0x000060D4
		protected virtual bool IsNullValue(object item)
		{
			return item == null || DBNull.Value.Equals(item);
		}

		// Token: 0x0400007B RID: 123
		public static ObjectToFilterableConverter DefaultObjectToFilterableConverter = new ObjectToFilterableConverter();
	}
}
