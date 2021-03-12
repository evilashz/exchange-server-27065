using System;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AddressBook.Service;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Nspi.Client;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.AddressBook.Nspi.Client
{
	// Token: 0x0200002F RID: 47
	internal class NspiConnection : IDisposable
	{
		// Token: 0x060001AC RID: 428 RVA: 0x000092BA File Offset: 0x000074BA
		internal NspiConnection(NspiConnectionPool owningPool)
		{
			this.owningPool = owningPool;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001AD RID: 429 RVA: 0x000092C9 File Offset: 0x000074C9
		public NspiClient Client
		{
			get
			{
				return this.client;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001AE RID: 430 RVA: 0x000092D1 File Offset: 0x000074D1
		public string Server
		{
			get
			{
				return this.owningPool.Server;
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000092DE File Offset: 0x000074DE
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000092E8 File Offset: 0x000074E8
		public NspiStatus Connect()
		{
			NspiConnection.NspiConnectionTracer.TraceDebug<string>(0L, "NspiConnection.Connect: owningPool.Server={0}", this.owningPool.Server ?? "(null)");
			NspiClient nspiClient = null;
			for (int i = 0; i < 3; i++)
			{
				try
				{
					if (i != 0)
					{
						Thread.Sleep(NspiConnection.RetryInterval);
					}
					nspiClient = new NspiClient(this.owningPool.Server);
					NspiStatus nspiStatus = nspiClient.Bind(NspiBindFlags.None);
					if (nspiStatus == NspiStatus.Success)
					{
						NspiConnection.NspiConnectionTracer.TraceDebug<string>(0L, "Bind to {0} succeeded", this.owningPool.Server ?? "(null)");
						this.client = nspiClient;
						nspiClient = null;
						return NspiStatus.Success;
					}
					NspiConnection.NspiConnectionTracer.TraceDebug<string, NspiStatus>(0L, "Bind to {0} failed with status {1}", this.owningPool.Server ?? "(null)", nspiStatus);
				}
				catch (RpcException innerException)
				{
					throw new ADTransientException(DirectoryStrings.ExceptionServerUnavailable(this.owningPool.Server), innerException);
				}
				finally
				{
					if (nspiClient != null)
					{
						nspiClient.Dispose();
						nspiClient = null;
					}
				}
			}
			NspiConnection.NspiConnectionTracer.TraceDebug<int, string>(0L, "All {0} attempts to connect to {1} failed", 3, this.owningPool.Server ?? "(null)");
			return NspiStatus.GeneralFailure;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00009428 File Offset: 0x00007628
		public void ReturnToPool()
		{
			this.returningToPool = true;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00009434 File Offset: 0x00007634
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.returningToPool)
				{
					this.returningToPool = false;
					this.owningPool.ReturnToPool(this);
					return;
				}
				if (this.client != null)
				{
					this.client.Dispose();
					this.client = null;
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x040000FD RID: 253
		private const int BindRetries = 3;

		// Token: 0x040000FE RID: 254
		internal static readonly Trace NspiConnectionTracer = ExTraceGlobals.NspiConnectionTracer;

		// Token: 0x040000FF RID: 255
		private static readonly TimeSpan RetryInterval = TimeSpan.FromSeconds(1.0);

		// Token: 0x04000100 RID: 256
		private readonly NspiConnectionPool owningPool;

		// Token: 0x04000101 RID: 257
		private NspiClient client;

		// Token: 0x04000102 RID: 258
		private bool returningToPool;
	}
}
