using System;
using System.Text;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200017C RID: 380
	[Serializable]
	internal class ReplicationNeighbor
	{
		// Token: 0x06001062 RID: 4194 RVA: 0x0004F58B File Offset: 0x0004D78B
		public ReplicationNeighbor()
		{
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0004F593 File Offset: 0x0004D793
		private ReplicationNeighbor(ADObjectId sourceDsa)
		{
			if (sourceDsa == null)
			{
				throw new ArgumentNullException("sourceDsa");
			}
			this.SourceDsa = sourceDsa;
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x0004F5B0 File Offset: 0x0004D7B0
		// (set) Token: 0x06001065 RID: 4197 RVA: 0x0004F5B8 File Offset: 0x0004D7B8
		public ADObjectId SourceDsa { get; private set; }

		// Token: 0x06001066 RID: 4198 RVA: 0x0004F5C4 File Offset: 0x0004D7C4
		public static ReplicationNeighbor Parse(byte[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Length < 36)
			{
				throw new ArgumentException("value");
			}
			byte[] array = new byte[4];
			Array.Copy(value, 4, array, 0, 4);
			int num = BitConverter.ToInt32(array, 0);
			ADObjectId adobjectId = null;
			if (num >= 120)
			{
				byte[] array2 = new byte[value.Length - num];
				Array.Copy(value, num, array2, 0, value.Length - num);
				string text = Encoding.Unicode.GetString(array2);
				string text2 = text;
				char[] separator = new char[1];
				text = text2.Split(separator)[0];
				adobjectId = new ADObjectId(text);
			}
			if (adobjectId != null)
			{
				return new ReplicationNeighbor(adobjectId);
			}
			return null;
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0004F65F File Offset: 0x0004D85F
		public override string ToString()
		{
			return this.SourceDsa.ToString();
		}
	}
}
