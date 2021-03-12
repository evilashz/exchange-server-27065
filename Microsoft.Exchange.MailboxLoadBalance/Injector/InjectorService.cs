using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Providers;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.MailboxLoadBalance.Injector
{
	// Token: 0x02000092 RID: 146
	[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class InjectorService : VersionedServiceBase, IInjectorService, IVersionedService, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000551 RID: 1361 RVA: 0x0000DF06 File Offset: 0x0000C106
		public InjectorService(IDirectoryProvider directory, ILogger logger, MoveInjector moveInjector) : base(logger)
		{
			this.directory = directory;
			this.moveInjector = moveInjector;
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0000DF1D File Offset: 0x0000C11D
		public static ServiceEndpointAddress EndpointAddress
		{
			get
			{
				return InjectorService.EndpointAddressHook.Value;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x0000DF29 File Offset: 0x0000C129
		protected override VersionInformation ServiceVersion
		{
			get
			{
				return LoadBalancerVersionInformation.InjectorVersion;
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0000DFDC File Offset: 0x0000C1DC
		void IInjectorService.InjectMoves(Guid targetDatabase, string batchName, IEnumerable<LoadEntity> mailboxes)
		{
			base.ForwardExceptions(delegate()
			{
				DirectoryReconnectionVisitor visitor = new DirectoryReconnectionVisitor(this.directory, this.Logger);
				IList<LoadEntity> list = (mailboxes as IList<LoadEntity>) ?? mailboxes.ToList<LoadEntity>();
				foreach (LoadEntity loadEntity in list)
				{
					loadEntity.Accept(visitor);
				}
				this.moveInjector.InjectMoves(targetDatabase, BatchName.FromString(batchName), list, false);
			});
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0000E090 File Offset: 0x0000C290
		void IInjectorService.InjectSingleMove(Guid targetDatabase, string batchName, LoadEntity mailbox)
		{
			base.ForwardExceptions(delegate()
			{
				DirectoryReconnectionVisitor visitor = new DirectoryReconnectionVisitor(this.directory, this.Logger);
				mailbox.Accept(visitor);
				this.moveInjector.InjectMoves(targetDatabase, BatchName.FromString(batchName), new LoadEntity[]
				{
					mailbox
				}, false);
			});
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0000E0D1 File Offset: 0x0000C2D1
		protected internal static IDisposable SetEndpointAddress(ServiceEndpointAddress endpointAddress)
		{
			return InjectorService.EndpointAddressHook.SetTestHook(endpointAddress);
		}

		// Token: 0x040001AE RID: 430
		private const string EndpointSuffix = "Microsoft.Exchange.MailboxLoadBalance.InjectorService";

		// Token: 0x040001AF RID: 431
		private static readonly Hookable<ServiceEndpointAddress> EndpointAddressHook = Hookable<ServiceEndpointAddress>.Create(true, new ServiceEndpointAddress("Microsoft.Exchange.MailboxLoadBalance.InjectorService"));

		// Token: 0x040001B0 RID: 432
		private readonly IDirectoryProvider directory;

		// Token: 0x040001B1 RID: 433
		private readonly MoveInjector moveInjector;
	}
}
