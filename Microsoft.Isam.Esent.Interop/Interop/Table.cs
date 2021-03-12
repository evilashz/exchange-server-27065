using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002E8 RID: 744
	public class Table : EsentResource
	{
		// Token: 0x06000D92 RID: 3474 RVA: 0x0001B36B File Offset: 0x0001956B
		public Table(JET_SESID sesid, JET_DBID dbid, string name, OpenTableGrbit grbit)
		{
			this.sesid = sesid;
			this.name = name;
			Api.JetOpenTable(this.sesid, dbid, this.name, null, 0, grbit, out this.tableid);
			base.ResourceWasAllocated();
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x0001B3A4 File Offset: 0x000195A4
		public string Name
		{
			get
			{
				base.CheckObjectIsNotDisposed();
				return this.name;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x0001B3B2 File Offset: 0x000195B2
		public JET_TABLEID JetTableid
		{
			get
			{
				base.CheckObjectIsNotDisposed();
				return this.tableid;
			}
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0001B3C0 File Offset: 0x000195C0
		public static implicit operator JET_TABLEID(Table table)
		{
			return table.JetTableid;
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x0001B3C8 File Offset: 0x000195C8
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0001B3D0 File Offset: 0x000195D0
		public void Close()
		{
			base.CheckObjectIsNotDisposed();
			this.ReleaseResource();
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0001B3DE File Offset: 0x000195DE
		protected override void ReleaseResource()
		{
			Api.JetCloseTable(this.sesid, this.tableid);
			this.sesid = JET_SESID.Nil;
			this.tableid = JET_TABLEID.Nil;
			this.name = null;
			base.ResourceWasReleased();
		}

		// Token: 0x0400092C RID: 2348
		private JET_SESID sesid;

		// Token: 0x0400092D RID: 2349
		private JET_TABLEID tableid;

		// Token: 0x0400092E RID: 2350
		private string name;
	}
}
