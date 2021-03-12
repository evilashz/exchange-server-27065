using System;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Configuration
{
	// Token: 0x02000462 RID: 1122
	[KnownType(typeof(UMSettingsData))]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract]
	[KnownType(typeof(OwaFlightConfigData))]
	[KnownType(typeof(OwaConfigurationBaseData))]
	[KnownType(typeof(SmimeSettingsData))]
	[KnownType(typeof(MobileDevicePolicyData))]
	[KnownType(typeof(PolicyTipsData))]
	[KnownType(typeof(OwaAttachmentPolicyData))]
	[KnownType(typeof(WacConfigData))]
	[KnownType(typeof(OwaHelpUrlData))]
	[KnownType(typeof(OwaOrgConfigData))]
	internal abstract class SerializableDataBase
	{
		// Token: 0x060031FA RID: 12794 RVA: 0x000CCE53 File Offset: 0x000CB053
		public override bool Equals(object other)
		{
			return this.InternalEquals(other);
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x000CCE5C File Offset: 0x000CB05C
		public override int GetHashCode()
		{
			return this.InternalGetHashCode();
		}

		// Token: 0x060031FC RID: 12796
		protected abstract bool InternalEquals(object other);

		// Token: 0x060031FD RID: 12797
		protected abstract int InternalGetHashCode();

		// Token: 0x060031FE RID: 12798 RVA: 0x000CCE64 File Offset: 0x000CB064
		protected static bool ArrayContentsEquals<T>(T[] s1, T[] s2)
		{
			return object.ReferenceEquals(s1, s2) || (s1 != null && s2 != null && s1.SequenceEqual(s2));
		}

		// Token: 0x060031FF RID: 12799 RVA: 0x000CCE80 File Offset: 0x000CB080
		protected static int ArrayContentsHash<T>(T[] a1)
		{
			int num = 17;
			if (a1 != null)
			{
				foreach (T t in a1)
				{
					if (t != null)
					{
						num = (num * 397 ^ t.GetHashCode());
					}
				}
			}
			return num;
		}
	}
}
