using System;
using System.Xml;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x02000974 RID: 2420
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class DisposableTenantLicensePair : IDisposeTrackable, IDisposable
	{
		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x06003463 RID: 13411 RVA: 0x00080FDC File Offset: 0x0007F1DC
		internal SafeRightsManagementHandle EnablingPrincipalRac
		{
			get
			{
				this.ThrowIfDisposed();
				return this.tenantLicenses.EnablingPrincipalRac;
			}
		}

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x06003464 RID: 13412 RVA: 0x00080FEF File Offset: 0x0007F1EF
		internal SafeRightsManagementHandle BoundLicenseClc
		{
			get
			{
				this.ThrowIfDisposed();
				return this.tenantLicenses.BoundLicenseClc;
			}
		}

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x06003465 RID: 13413 RVA: 0x00081002 File Offset: 0x0007F202
		internal XmlNode[] Rac
		{
			get
			{
				this.ThrowIfDisposed();
				return this.tenantLicenses.Rac;
			}
		}

		// Token: 0x06003466 RID: 13414 RVA: 0x00081015 File Offset: 0x0007F215
		private DisposableTenantLicensePair(TenantLicensePair licensePair)
		{
			this.tenantLicenses = licensePair;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06003467 RID: 13415 RVA: 0x00081030 File Offset: 0x0007F230
		private void ThrowIfDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("Object already disposed.");
			}
		}

		// Token: 0x06003468 RID: 13416 RVA: 0x00081045 File Offset: 0x0007F245
		public static DisposableTenantLicensePair CreateDisposableTenantLicenses(TenantLicensePair licensePair)
		{
			if (licensePair == null)
			{
				throw new ArgumentNullException("licensePair");
			}
			licensePair.AddRef();
			if (!licensePair.IsCleanedUp)
			{
				return new DisposableTenantLicensePair(licensePair);
			}
			licensePair.Release();
			return null;
		}

		// Token: 0x06003469 RID: 13417 RVA: 0x00081071 File Offset: 0x0007F271
		public DisposableTenantLicensePair Clone()
		{
			this.ThrowIfDisposed();
			return DisposableTenantLicensePair.CreateDisposableTenantLicenses(this.tenantLicenses);
		}

		// Token: 0x0600346A RID: 13418 RVA: 0x00081084 File Offset: 0x0007F284
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<DisposableTenantLicensePair>(this);
		}

		// Token: 0x0600346B RID: 13419 RVA: 0x0008108C File Offset: 0x0007F28C
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x0600346C RID: 13420 RVA: 0x000810A4 File Offset: 0x0007F2A4
		public void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
			if (this.tenantLicenses != null)
			{
				this.tenantLicenses.Release();
				this.tenantLicenses = null;
			}
			this.disposed = true;
		}

		// Token: 0x04002C66 RID: 11366
		private DisposeTracker disposeTracker;

		// Token: 0x04002C67 RID: 11367
		private bool disposed;

		// Token: 0x04002C68 RID: 11368
		private TenantLicensePair tenantLicenses;
	}
}
