using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Collections
{
	// Token: 0x02000466 RID: 1126
	[ComVisible(true)]
	[Serializable]
	public sealed class Comparer : IComparer, ISerializable
	{
		// Token: 0x06003712 RID: 14098 RVA: 0x000D321F File Offset: 0x000D141F
		private Comparer()
		{
			this.m_compareInfo = null;
		}

		// Token: 0x06003713 RID: 14099 RVA: 0x000D322E File Offset: 0x000D142E
		public Comparer(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			this.m_compareInfo = culture.CompareInfo;
		}

		// Token: 0x06003714 RID: 14100 RVA: 0x000D3250 File Offset: 0x000D1450
		private Comparer(SerializationInfo info, StreamingContext context)
		{
			this.m_compareInfo = null;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (name == "CompareInfo")
				{
					this.m_compareInfo = (CompareInfo)info.GetValue("CompareInfo", typeof(CompareInfo));
				}
			}
		}

		// Token: 0x06003715 RID: 14101 RVA: 0x000D32B0 File Offset: 0x000D14B0
		public int Compare(object a, object b)
		{
			if (a == b)
			{
				return 0;
			}
			if (a == null)
			{
				return -1;
			}
			if (b == null)
			{
				return 1;
			}
			if (this.m_compareInfo != null)
			{
				string text = a as string;
				string text2 = b as string;
				if (text != null && text2 != null)
				{
					return this.m_compareInfo.Compare(text, text2);
				}
			}
			IComparable comparable = a as IComparable;
			if (comparable != null)
			{
				return comparable.CompareTo(b);
			}
			IComparable comparable2 = b as IComparable;
			if (comparable2 != null)
			{
				return -comparable2.CompareTo(a);
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_ImplementIComparable"));
		}

		// Token: 0x06003716 RID: 14102 RVA: 0x000D332B File Offset: 0x000D152B
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (this.m_compareInfo != null)
			{
				info.AddValue("CompareInfo", this.m_compareInfo);
			}
		}

		// Token: 0x0400181C RID: 6172
		private CompareInfo m_compareInfo;

		// Token: 0x0400181D RID: 6173
		public static readonly Comparer Default = new Comparer(CultureInfo.CurrentCulture);

		// Token: 0x0400181E RID: 6174
		public static readonly Comparer DefaultInvariant = new Comparer(CultureInfo.InvariantCulture);

		// Token: 0x0400181F RID: 6175
		private const string CompareInfoName = "CompareInfo";
	}
}
