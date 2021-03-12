using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Config;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Providers;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;

namespace Microsoft.Exchange.MailboxLoadBalance.Injector
{
	// Token: 0x02000090 RID: 144
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InjectorClient : VersionedClientBase<IInjectorService>, IInjectorService, IVersionedService, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000541 RID: 1345 RVA: 0x0000D979 File Offset: 0x0000BB79
		private InjectorClient(Binding binding, EndpointAddress remoteAddress, ILogger logger) : base(binding, remoteAddress, logger)
		{
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0000D990 File Offset: 0x0000BB90
		public static InjectorClient Create(string serverName, IDirectoryProvider directory, ILogger logger)
		{
			Func<Binding, EndpointAddress, ILogger, InjectorClient> constructor = (Binding binding, EndpointAddress endpointAddress, ILogger log) => new InjectorClient(binding, endpointAddress, log);
			return VersionedClientBase<IInjectorService>.CreateClient<InjectorClient>(serverName, InjectorService.EndpointAddress, constructor, logger);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0000DA0C File Offset: 0x0000BC0C
		public void InjectMoves(Guid targetDatabase, string batchName, IEnumerable<LoadEntity> mailboxes)
		{
			base.Logger.Log(MigrationEventType.Instrumentation, "Begin injecting '{0}' moves into database '{1}' with batch name '{2}'", new object[]
			{
				mailboxes.Count<LoadEntity>(),
				targetDatabase,
				batchName
			});
			int injectionBatchSize = LoadBalanceADSettings.Instance.Value.InjectionBatchSize;
			foreach (IEnumerable<LoadEntity> enumerable in mailboxes.Batch(injectionBatchSize))
			{
				base.Logger.Log(MigrationEventType.Instrumentation, "Set of {0} moves being injected into database '{1}' with batch name '{2}'", new object[]
				{
					enumerable.Count<LoadEntity>(),
					targetDatabase,
					batchName
				});
				IEnumerable<LoadEntity> batch = enumerable;
				base.CallService(delegate()
				{
					this.Channel.InjectMoves(targetDatabase, batchName, batch);
				});
			}
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0000DB5C File Offset: 0x0000BD5C
		public void InjectSingleMove(Guid targetDatabase, string batchName, LoadEntity mailbox)
		{
			base.CallService(delegate()
			{
				this.Channel.InjectSingleMove(targetDatabase, batchName, mailbox);
			});
		}
	}
}
