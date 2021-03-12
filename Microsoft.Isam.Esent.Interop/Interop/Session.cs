using System;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002E5 RID: 741
	public class Session : EsentResource
	{
		// Token: 0x06000D7C RID: 3452 RVA: 0x0001B0D0 File Offset: 0x000192D0
		public Session(JET_INSTANCE instance)
		{
			Api.JetBeginSession(instance, out this.sesid, null, null);
			base.ResourceWasAllocated();
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x0001B0EC File Offset: 0x000192EC
		public JET_SESID JetSesid
		{
			get
			{
				base.CheckObjectIsNotDisposed();
				return this.sesid;
			}
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0001B0FA File Offset: 0x000192FA
		public static implicit operator JET_SESID(Session session)
		{
			return session.JetSesid;
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0001B104 File Offset: 0x00019304
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Session (0x{0:x})", new object[]
			{
				this.sesid.Value
			});
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0001B13B File Offset: 0x0001933B
		public void End()
		{
			base.CheckObjectIsNotDisposed();
			this.ReleaseResource();
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x0001B149 File Offset: 0x00019349
		protected override void ReleaseResource()
		{
			if (!this.sesid.IsInvalid)
			{
				Api.JetEndSession(this.JetSesid, EndSessionGrbit.None);
			}
			this.sesid = JET_SESID.Nil;
			base.ResourceWasReleased();
		}

		// Token: 0x04000927 RID: 2343
		private JET_SESID sesid;
	}
}
