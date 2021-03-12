using System;
using System.Globalization;
using Microsoft.Isam.Esent.Interop.Windows8;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002EE RID: 750
	public class Transaction : EsentResource
	{
		// Token: 0x06000DA7 RID: 3495 RVA: 0x0001B656 File Offset: 0x00019856
		public Transaction(JET_SESID sesid)
		{
			this.sesid = sesid;
			this.Begin();
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x0001B66B File Offset: 0x0001986B
		public bool IsInTransaction
		{
			get
			{
				base.CheckObjectIsNotDisposed();
				return base.HasResource;
			}
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0001B67C File Offset: 0x0001987C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Transaction (0x{0:x})", new object[]
			{
				this.sesid.Value
			});
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0001B6B3 File Offset: 0x000198B3
		public void Begin()
		{
			base.CheckObjectIsNotDisposed();
			if (this.IsInTransaction)
			{
				throw new InvalidOperationException("Already in a transaction");
			}
			Api.JetBeginTransaction(this.sesid);
			base.ResourceWasAllocated();
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x0001B6DF File Offset: 0x000198DF
		public void Commit(CommitTransactionGrbit grbit)
		{
			base.CheckObjectIsNotDisposed();
			if (!this.IsInTransaction)
			{
				throw new InvalidOperationException("Not in a transaction");
			}
			Api.JetCommitTransaction(this.sesid, grbit);
			base.ResourceWasReleased();
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0001B70C File Offset: 0x0001990C
		public void Commit(CommitTransactionGrbit grbit, TimeSpan durableCommit, out JET_COMMIT_ID commitId)
		{
			base.CheckObjectIsNotDisposed();
			if (!this.IsInTransaction)
			{
				throw new InvalidOperationException("Not in a transaction");
			}
			Windows8Api.JetCommitTransaction2(this.sesid, grbit, durableCommit, out commitId);
			base.ResourceWasReleased();
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0001B73B File Offset: 0x0001993B
		public void Rollback()
		{
			base.CheckObjectIsNotDisposed();
			if (!this.IsInTransaction)
			{
				throw new InvalidOperationException("Not in a transaction");
			}
			Api.JetRollback(this.sesid, RollbackTransactionGrbit.None);
			base.ResourceWasReleased();
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0001B768 File Offset: 0x00019968
		protected override void ReleaseResource()
		{
			this.Rollback();
		}

		// Token: 0x04000937 RID: 2359
		private readonly JET_SESID sesid;
	}
}
