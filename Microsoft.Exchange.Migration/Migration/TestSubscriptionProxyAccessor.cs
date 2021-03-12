using System;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Migration.DataAccessLayer;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000180 RID: 384
	internal sealed class TestSubscriptionProxyAccessor : SubscriptionAccessorBase
	{
		// Token: 0x060011F7 RID: 4599 RVA: 0x0004B494 File Offset: 0x00049694
		private TestSubscriptionProxyAccessor(string endpoint)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(endpoint, "endpoint");
			this.implementation = (ITestSubscriptionAccessor)Activator.GetObject(typeof(ITestSubscriptionAccessor), endpoint);
			if (this.implementation == null)
			{
				throw new InvalidOperationException("couldn't create remote instance at endpoint " + endpoint);
			}
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0004B4FC File Offset: 0x000496FC
		public static bool TryCreate(out SubscriptionAccessorBase accessor)
		{
			accessor = null;
			string migrationAccessorEndpoint = MigrationTestIntegration.Instance.MigrationAccessorEndpoint;
			if (string.IsNullOrEmpty(migrationAccessorEndpoint))
			{
				return false;
			}
			accessor = new TestSubscriptionProxyAccessor(migrationAccessorEndpoint);
			return true;
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x0004B550 File Offset: 0x00049750
		public override SubscriptionSnapshot CreateSubscription(MigrationJobItem jobItem)
		{
			MigrationEndpointBase endpoint = null;
			if (jobItem.MigrationJob.JobDirection == MigrationBatchDirection.Onboarding)
			{
				endpoint = jobItem.MigrationJob.SourceEndpoint;
			}
			else if (jobItem.MigrationJob.JobDirection == MigrationBatchDirection.Offboarding)
			{
				endpoint = jobItem.MigrationJob.TargetEndpoint;
			}
			TestSubscriptionAspect aspect = new TestSubscriptionAspect(endpoint, jobItem, false);
			SubscriptionSnapshot snapshot = null;
			this.RunProxyOperation(delegate
			{
				snapshot = this.implementation.CreateSubscription(aspect);
			});
			return snapshot;
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x0004B5F8 File Offset: 0x000497F8
		public override SubscriptionSnapshot TestCreateSubscription(MigrationJobItem jobItem)
		{
			MigrationEndpointBase endpoint = null;
			if (jobItem.MigrationJob.JobDirection == MigrationBatchDirection.Onboarding)
			{
				endpoint = jobItem.MigrationJob.SourceEndpoint;
			}
			else if (jobItem.MigrationJob.JobDirection == MigrationBatchDirection.Offboarding)
			{
				endpoint = jobItem.MigrationJob.TargetEndpoint;
			}
			TestSubscriptionAspect aspect = new TestSubscriptionAspect(endpoint, jobItem, false);
			SubscriptionSnapshot snapshot = null;
			this.RunProxyOperation(delegate
			{
				snapshot = this.implementation.TestCreateSubscription(aspect);
			});
			return snapshot;
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x0004B6AC File Offset: 0x000498AC
		public override SnapshotStatus RetrieveSubscriptionStatus(ISubscriptionId subscriptionId)
		{
			MRSSubscriptionId subId = subscriptionId as MRSSubscriptionId;
			SnapshotStatus? status = null;
			this.RunProxyOperation(delegate
			{
				status = new SnapshotStatus?(this.implementation.RetrieveSubscriptionStatus(subId.Id));
			});
			if (status == null)
			{
				throw new MigrationPermanentException(new LocalizedString("could not retrieve subscription status for " + subId.Id));
			}
			return status.Value;
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x0004B758 File Offset: 0x00049958
		public override SubscriptionSnapshot RetrieveSubscriptionSnapshot(ISubscriptionId subscriptionId)
		{
			MRSSubscriptionId subId = subscriptionId as MRSSubscriptionId;
			SubscriptionSnapshot snapshot = null;
			this.RunProxyOperation(delegate
			{
				snapshot = this.implementation.RetrieveSubscriptionSnapshot(subId.Id);
			});
			if (snapshot == null)
			{
				throw new MigrationPermanentException(new LocalizedString("could not retrieve subscription for " + subId.Id));
			}
			return snapshot;
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x0004B7CA File Offset: 0x000499CA
		public override bool ResumeSubscription(ISubscriptionId subscriptionId, bool finalize = false)
		{
			this.UpdateSubscriptionStatus(subscriptionId, SnapshotStatus.InProgress);
			return true;
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x0004B7D5 File Offset: 0x000499D5
		public override bool SuspendSubscription(ISubscriptionId subscriptionId)
		{
			this.UpdateSubscriptionStatus(subscriptionId, SnapshotStatus.Suspended);
			return true;
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0004B7E0 File Offset: 0x000499E0
		public override bool RemoveSubscription(ISubscriptionId subscriptionId)
		{
			this.UpdateSubscriptionStatus(subscriptionId, SnapshotStatus.Removed);
			return true;
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x0004B818 File Offset: 0x00049A18
		private void UpdateSubscriptionStatus(ISubscriptionId subscriptionId, SnapshotStatus status)
		{
			MRSSubscriptionId subId = subscriptionId as MRSSubscriptionId;
			this.RunProxyOperation(delegate
			{
				this.implementation.UpdateSubscriptionStatus(subId.Id, status);
			});
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x0004B884 File Offset: 0x00049A84
		public override bool UpdateSubscription(ISubscriptionId subscriptionId, MigrationEndpointBase endpoint, MigrationJobItem jobItem, bool adoptingSubscription)
		{
			MRSSubscriptionId subId = subscriptionId as MRSSubscriptionId;
			TestSubscriptionAspect aspect = new TestSubscriptionAspect(endpoint, jobItem, true);
			this.RunProxyOperation(delegate
			{
				this.implementation.UpdateSubscription(subId.Id, aspect);
			});
			return true;
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x0004B8CC File Offset: 0x00049ACC
		private void RunProxyOperation(TestSubscriptionProxyAccessor.ProxyOperation operation)
		{
			try
			{
				this.debugContextIndex = (this.debugContextIndex + 1) % 10;
				this.debugContext[this.debugContextIndex] = ExDateTime.UtcNow.ToString() + " " + this.implementation.GetDebuggingContext();
				operation();
			}
			catch (TransientException)
			{
				throw;
			}
			catch (MigrationPermanentException)
			{
				throw;
			}
			catch (StoragePermanentException)
			{
				throw;
			}
			catch (Exception ex)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 10; i > 0; i--)
				{
					int num = (i + this.debugContextIndex) % 10;
					if (string.IsNullOrEmpty(this.debugContext[num]))
					{
						break;
					}
					stringBuilder.Append(this.debugContext[num]);
				}
				MigrationLogger.Log(MigrationEventType.Error, ex, "TEST RunProxyOperation failed with unknown error: context ", new object[]
				{
					stringBuilder
				});
				throw new MigrationDataCorruptionException("Error running test proxy code:" + this.debugContext[this.debugContextIndex], ex);
			}
		}

		// Token: 0x0400064C RID: 1612
		private const int DebugContextSize = 10;

		// Token: 0x0400064D RID: 1613
		private readonly ITestSubscriptionAccessor implementation;

		// Token: 0x0400064E RID: 1614
		private string[] debugContext = new string[10];

		// Token: 0x0400064F RID: 1615
		private int debugContextIndex = -1;

		// Token: 0x02000181 RID: 385
		// (Invoke) Token: 0x06001204 RID: 4612
		internal delegate void ProxyOperation();
	}
}
