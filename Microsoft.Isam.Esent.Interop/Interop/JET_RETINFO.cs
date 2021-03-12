using System;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002BB RID: 699
	public class JET_RETINFO : IContentEquatable<JET_RETINFO>, IDeepCloneable<JET_RETINFO>
	{
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x00019413 File Offset: 0x00017613
		// (set) Token: 0x06000C8F RID: 3215 RVA: 0x0001941B File Offset: 0x0001761B
		public int ibLongValue { get; set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x00019424 File Offset: 0x00017624
		// (set) Token: 0x06000C91 RID: 3217 RVA: 0x0001942C File Offset: 0x0001762C
		public int itagSequence { get; set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x00019435 File Offset: 0x00017635
		// (set) Token: 0x06000C93 RID: 3219 RVA: 0x0001943D File Offset: 0x0001763D
		public JET_COLUMNID columnidNextTagged { get; internal set; }

		// Token: 0x06000C94 RID: 3220 RVA: 0x00019448 File Offset: 0x00017648
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_RETINFO(ibLongValue={0},itagSequence={1})", new object[]
			{
				this.ibLongValue,
				this.itagSequence
			});
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00019488 File Offset: 0x00017688
		public bool ContentEquals(JET_RETINFO other)
		{
			return other != null && (this.ibLongValue == other.ibLongValue && this.itagSequence == other.itagSequence) && this.columnidNextTagged == other.columnidNextTagged;
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x000194BE File Offset: 0x000176BE
		public JET_RETINFO DeepClone()
		{
			return (JET_RETINFO)base.MemberwiseClone();
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x000194CC File Offset: 0x000176CC
		internal NATIVE_RETINFO GetNativeRetinfo()
		{
			return checked(new NATIVE_RETINFO
			{
				cbStruct = (uint)NATIVE_RETINFO.Size,
				ibLongValue = (uint)this.ibLongValue,
				itagSequence = (uint)this.itagSequence
			});
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0001950C File Offset: 0x0001770C
		internal void SetFromNativeRetinfo(NATIVE_RETINFO value)
		{
			checked
			{
				this.ibLongValue = (int)value.ibLongValue;
				this.itagSequence = (int)value.itagSequence;
				JET_COLUMNID columnidNextTagged = new JET_COLUMNID
				{
					Value = value.columnidNextTagged
				};
				this.columnidNextTagged = columnidNextTagged;
			}
		}
	}
}
