using System;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002A3 RID: 675
	public class JET_OBJECTINFO
	{
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x000184ED File Offset: 0x000166ED
		// (set) Token: 0x06000C03 RID: 3075 RVA: 0x000184F5 File Offset: 0x000166F5
		public JET_objtyp objtyp { get; private set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x000184FE File Offset: 0x000166FE
		// (set) Token: 0x06000C05 RID: 3077 RVA: 0x00018506 File Offset: 0x00016706
		public ObjectInfoGrbit grbit { get; private set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x0001850F File Offset: 0x0001670F
		// (set) Token: 0x06000C07 RID: 3079 RVA: 0x00018517 File Offset: 0x00016717
		public ObjectInfoFlags flags { get; private set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x00018520 File Offset: 0x00016720
		// (set) Token: 0x06000C09 RID: 3081 RVA: 0x00018528 File Offset: 0x00016728
		public int cRecord { get; private set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x00018531 File Offset: 0x00016731
		// (set) Token: 0x06000C0B RID: 3083 RVA: 0x00018539 File Offset: 0x00016739
		public int cPage { get; private set; }

		// Token: 0x06000C0C RID: 3084 RVA: 0x00018544 File Offset: 0x00016744
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_OBJECTINFO({0})", new object[]
			{
				this.flags
			});
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00018576 File Offset: 0x00016776
		internal void SetFromNativeObjectinfo(ref NATIVE_OBJECTINFO value)
		{
			this.objtyp = (JET_objtyp)value.objtyp;
			this.grbit = (ObjectInfoGrbit)value.grbit;
			this.flags = (ObjectInfoFlags)value.flags;
			this.cRecord = (int)value.cRecord;
			this.cPage = (int)value.cPage;
		}
	}
}
