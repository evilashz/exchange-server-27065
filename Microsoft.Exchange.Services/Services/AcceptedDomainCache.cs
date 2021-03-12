using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Services
{
	// Token: 0x02000013 RID: 19
	internal class AcceptedDomainCache : IDisposable
	{
		// Token: 0x06000091 RID: 145 RVA: 0x00003A6B File Offset: 0x00001C6B
		internal AcceptedDomainCache()
		{
			this.session = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 45, ".ctor", "f:\\15.00.1497\\sources\\dev\\services\\src\\Services\\Common\\AcceptedDomainCache.cs");
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003A95 File Offset: 0x00001C95
		private bool IsInitialized
		{
			get
			{
				return this.cookie != null;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003AA4 File Offset: 0x00001CA4
		public AcceptedDomain DefaultDomain
		{
			get
			{
				AcceptedDomain result = null;
				lock (this)
				{
					this.CheckDisposed();
					if (!this.IsInitialized)
					{
						this.RegisterWithAD();
						this.Reload(null);
					}
					result = this.defaultDomain;
				}
				return result;
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003B00 File Offset: 0x00001D00
		public void Dispose()
		{
			if (!this.isDisposed)
			{
				lock (this)
				{
					if (this.cookie != null)
					{
						this.ReleaseCookie();
						this.cookie = null;
					}
					this.isDisposed = true;
				}
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003B5C File Offset: 0x00001D5C
		private void CheckDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003B78 File Offset: 0x00001D78
		private void Reload(ADNotificationEventArgs args)
		{
			lock (this)
			{
				try
				{
					if (this.defaultDomain != null)
					{
						QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Guid, this.defaultDomain.Guid);
						AcceptedDomain[] array = this.session.Find<AcceptedDomain>(null, QueryScope.SubTree, filter, null, 1);
						if (array != null && array.Length == 1 && array[0].Default)
						{
							this.defaultDomain = array[0];
							ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>((long)this.GetHashCode(), "Default accepted domain still set to {0}", this.defaultDomain.DomainName.Address);
							return;
						}
						ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>((long)this.GetHashCode(), "Default accepted domain changed. It was previously set to {0}", this.defaultDomain.DomainName.Address);
					}
					ExTraceGlobals.CommonAlgorithmTracer.TraceDebug((long)this.GetHashCode(), "Attempting to find the current default accepted domain");
					BitMaskAndFilter filter2 = new BitMaskAndFilter(AcceptedDomainSchema.AcceptedDomainFlags, 4UL);
					AcceptedDomain[] array2 = this.session.Find<AcceptedDomain>(null, QueryScope.SubTree, filter2, null, 1);
					if (array2 != null && array2.Length == 1)
					{
						this.defaultDomain = array2[0];
						ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>((long)this.GetHashCode(), "Default accepted domain has been changed to {0}", this.defaultDomain.DomainName.Address);
					}
				}
				catch (ADTransientException arg)
				{
					ExTraceGlobals.ExceptionTracer.TraceError<ADTransientException>((long)this.GetHashCode(), "Failed to find default Accepted Domain: {0}", arg);
					if (args == null)
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003D0C File Offset: 0x00001F0C
		private void RegisterWithAD()
		{
			try
			{
				this.cookie = ADNotificationAdapter.RegisterChangeNotification<AcceptedDomain>(this.session.GetOrgContainerId().GetDescendantId(AcceptedDomain.AcceptedDomainContainer), new ADNotificationCallback(this.Reload), null);
			}
			catch (ADTransientException arg)
			{
				this.cookie = null;
				ExTraceGlobals.ExceptionTracer.TraceError<ADTransientException>((long)this.GetHashCode(), "Failed to register Accepted Domain Change notification: {0}", arg);
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003D7C File Offset: 0x00001F7C
		private void ReleaseCookie()
		{
			ADNotificationAdapter.UnregisterChangeNotification(this.cookie);
		}

		// Token: 0x0400002C RID: 44
		private AcceptedDomain defaultDomain;

		// Token: 0x0400002D RID: 45
		private IConfigurationSession session;

		// Token: 0x0400002E RID: 46
		private bool isDisposed;

		// Token: 0x0400002F RID: 47
		private ADNotificationRequestCookie cookie;
	}
}
