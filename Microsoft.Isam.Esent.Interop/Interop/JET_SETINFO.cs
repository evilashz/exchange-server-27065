using System;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002C6 RID: 710
	[Serializable]
	public class JET_SETINFO : IContentEquatable<JET_SETINFO>, IDeepCloneable<JET_SETINFO>
	{
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x00019E5B File Offset: 0x0001805B
		// (set) Token: 0x06000CE6 RID: 3302 RVA: 0x00019E63 File Offset: 0x00018063
		public int ibLongValue
		{
			get
			{
				return this.longValueOffset;
			}
			set
			{
				this.longValueOffset = value;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x00019E6C File Offset: 0x0001806C
		// (set) Token: 0x06000CE8 RID: 3304 RVA: 0x00019E74 File Offset: 0x00018074
		public int itagSequence
		{
			get
			{
				return this.itag;
			}
			set
			{
				this.itag = value;
			}
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x00019E80 File Offset: 0x00018080
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_SETINFO(ibLongValue={0},itagSequence={1})", new object[]
			{
				this.ibLongValue,
				this.itagSequence
			});
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x00019EC0 File Offset: 0x000180C0
		public bool ContentEquals(JET_SETINFO other)
		{
			return other != null && this.ibLongValue == other.ibLongValue && this.itagSequence == other.itagSequence;
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00019EE5 File Offset: 0x000180E5
		public JET_SETINFO DeepClone()
		{
			return (JET_SETINFO)base.MemberwiseClone();
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x00019EF4 File Offset: 0x000180F4
		internal NATIVE_SETINFO GetNativeSetinfo()
		{
			return checked(new NATIVE_SETINFO
			{
				cbStruct = (uint)NATIVE_SETINFO.Size,
				ibLongValue = (uint)this.ibLongValue,
				itagSequence = (uint)this.itagSequence
			});
		}

		// Token: 0x0400084E RID: 2126
		private int longValueOffset;

		// Token: 0x0400084F RID: 2127
		private int itag;
	}
}
