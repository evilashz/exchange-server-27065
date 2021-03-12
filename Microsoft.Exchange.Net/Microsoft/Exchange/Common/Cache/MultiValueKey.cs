using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Common.Cache
{
	// Token: 0x02000686 RID: 1670
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MultiValueKey
	{
		// Token: 0x06001E3B RID: 7739 RVA: 0x00037838 File Offset: 0x00035A38
		public MultiValueKey(params object[] keys)
		{
			if (keys == null || keys.Length == 0)
			{
				throw new ArgumentNullException("keys");
			}
			for (int i = 0; i < keys.Length; i++)
			{
				if (keys[i] == null)
				{
					throw new ArgumentNullException("keys", "One or more keys are null!");
				}
			}
			this.keys = keys;
			this.KeyLength = keys.Length;
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x00037891 File Offset: 0x00035A91
		public object GetKey(int index)
		{
			if (index < 0 || index >= this.KeyLength)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return this.keys[index];
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x000378B4 File Offset: 0x00035AB4
		public override int GetHashCode()
		{
			int num = 23;
			foreach (object obj in this.keys)
			{
				num = num * 37 + obj.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x000378EC File Offset: 0x00035AEC
		public override bool Equals(object obj)
		{
			MultiValueKey multiValueKey = obj as MultiValueKey;
			bool flag = true;
			bool flag2 = multiValueKey != null && multiValueKey.keys.Length == this.keys.Length;
			if (flag2)
			{
				for (int i = 0; i < this.keys.Length; i++)
				{
					if (!multiValueKey.keys[i].Equals(this.keys[i]))
					{
						flag = false;
						break;
					}
				}
			}
			return flag2 && flag;
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x00037954 File Offset: 0x00035B54
		public override string ToString()
		{
			char value = ',';
			char value2 = ' ';
			StringBuilder stringBuilder = new StringBuilder();
			int num = this.keys.Length;
			for (int i = 0; i < num; i++)
			{
				object obj = this.keys[i];
				stringBuilder.Append(obj.ToString());
				if (i < num - 1)
				{
					stringBuilder.Append(value);
					stringBuilder.Append(value2);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001E3B RID: 7739
		public readonly int KeyLength;

		// Token: 0x04001E3C RID: 7740
		private readonly object[] keys;
	}
}
