using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200076F RID: 1903
	internal sealed class NameInfo
	{
		// Token: 0x0600533B RID: 21307 RVA: 0x00124985 File Offset: 0x00122B85
		internal NameInfo()
		{
		}

		// Token: 0x0600533C RID: 21308 RVA: 0x00124990 File Offset: 0x00122B90
		internal void Init()
		{
			this.NIFullName = null;
			this.NIobjectId = 0L;
			this.NIassemId = 0L;
			this.NIprimitiveTypeEnum = InternalPrimitiveTypeE.Invalid;
			this.NItype = null;
			this.NIisSealed = false;
			this.NItransmitTypeOnObject = false;
			this.NItransmitTypeOnMember = false;
			this.NIisParentTypeOnObject = false;
			this.NIisArray = false;
			this.NIisArrayItem = false;
			this.NIarrayEnum = InternalArrayTypeE.Empty;
			this.NIsealedStatusChecked = false;
		}

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x0600533D RID: 21309 RVA: 0x001249FA File Offset: 0x00122BFA
		public bool IsSealed
		{
			get
			{
				if (!this.NIsealedStatusChecked)
				{
					this.NIisSealed = this.NItype.IsSealed;
					this.NIsealedStatusChecked = true;
				}
				return this.NIisSealed;
			}
		}

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x0600533E RID: 21310 RVA: 0x00124A22 File Offset: 0x00122C22
		// (set) Token: 0x0600533F RID: 21311 RVA: 0x00124A43 File Offset: 0x00122C43
		public string NIname
		{
			get
			{
				if (this.NIFullName == null)
				{
					this.NIFullName = this.NItype.FullName;
				}
				return this.NIFullName;
			}
			set
			{
				this.NIFullName = value;
			}
		}

		// Token: 0x040025A9 RID: 9641
		internal string NIFullName;

		// Token: 0x040025AA RID: 9642
		internal long NIobjectId;

		// Token: 0x040025AB RID: 9643
		internal long NIassemId;

		// Token: 0x040025AC RID: 9644
		internal InternalPrimitiveTypeE NIprimitiveTypeEnum;

		// Token: 0x040025AD RID: 9645
		internal Type NItype;

		// Token: 0x040025AE RID: 9646
		internal bool NIisSealed;

		// Token: 0x040025AF RID: 9647
		internal bool NIisArray;

		// Token: 0x040025B0 RID: 9648
		internal bool NIisArrayItem;

		// Token: 0x040025B1 RID: 9649
		internal bool NItransmitTypeOnObject;

		// Token: 0x040025B2 RID: 9650
		internal bool NItransmitTypeOnMember;

		// Token: 0x040025B3 RID: 9651
		internal bool NIisParentTypeOnObject;

		// Token: 0x040025B4 RID: 9652
		internal InternalArrayTypeE NIarrayEnum;

		// Token: 0x040025B5 RID: 9653
		private bool NIsealedStatusChecked;
	}
}
