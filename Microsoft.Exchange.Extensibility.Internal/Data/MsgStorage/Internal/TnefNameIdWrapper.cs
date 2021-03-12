using System;
using Microsoft.Exchange.Data.ContentTypes.Tnef;

namespace Microsoft.Exchange.Data.MsgStorage.Internal
{
	// Token: 0x020000BC RID: 188
	internal class TnefNameIdWrapper
	{
		// Token: 0x06000629 RID: 1577 RVA: 0x0001B97A File Offset: 0x00019B7A
		public TnefNameIdWrapper(TnefNameId nameId)
		{
			this.nameId = nameId;
			this.ComputeHashCode();
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0001B990 File Offset: 0x00019B90
		public TnefNameIdWrapper(Guid guid, string name) : this(new TnefNameId(guid, name))
		{
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001B99F File Offset: 0x00019B9F
		public TnefNameIdWrapper(Guid guid, int id) : this(new TnefNameId(guid, id))
		{
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0001B9AE File Offset: 0x00019BAE
		public TnefNameId TnefNameId
		{
			get
			{
				return this.nameId;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x0001B9B6 File Offset: 0x00019BB6
		public Guid PropertySetGuid
		{
			get
			{
				return this.nameId.PropertySetGuid;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0001B9C3 File Offset: 0x00019BC3
		public TnefNameIdKind Kind
		{
			get
			{
				return this.nameId.Kind;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x0001B9D0 File Offset: 0x00019BD0
		public int Id
		{
			get
			{
				return this.nameId.Id;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x0001B9DD File Offset: 0x00019BDD
		public string Name
		{
			get
			{
				return this.nameId.Name;
			}
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0001B9EC File Offset: 0x00019BEC
		public override bool Equals(object obj)
		{
			TnefNameIdWrapper tnefNameIdWrapper = obj as TnefNameIdWrapper;
			return tnefNameIdWrapper != null && this.PropertySetGuid == tnefNameIdWrapper.PropertySetGuid && this.Id == tnefNameIdWrapper.Id && this.Name == tnefNameIdWrapper.Name;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001BA37 File Offset: 0x00019C37
		public override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0001BA40 File Offset: 0x00019C40
		private int ComputeHashCode()
		{
			this.hashCode = (this.nameId.PropertySetGuid.GetHashCode() ^ ((this.Kind == TnefNameIdKind.Id) ? this.Id.GetHashCode() : this.Name.GetHashCode()));
			return this.hashCode;
		}

		// Token: 0x040005CB RID: 1483
		private int hashCode;

		// Token: 0x040005CC RID: 1484
		private TnefNameId nameId;
	}
}
