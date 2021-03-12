using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200014F RID: 335
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class MrsAccessorBase : RunspaceAccessorBase
	{
		// Token: 0x060010A1 RID: 4257 RVA: 0x000459F5 File Offset: 0x00043BF5
		protected MrsAccessorBase(IMigrationDataProvider dataProvider, string batchName) : base(dataProvider)
		{
			this.BatchName = string.Format("MigrationService:{0}", batchName);
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060010A2 RID: 4258 RVA: 0x00045A0F File Offset: 0x00043C0F
		// (set) Token: 0x060010A3 RID: 4259 RVA: 0x00045A17 File Offset: 0x00043C17
		private protected string BatchName { protected get; private set; }

		// Token: 0x060010A4 RID: 4260 RVA: 0x00045A20 File Offset: 0x00043C20
		internal T Run<T>(MrsAccessorCommand command) where T : class
		{
			Type type;
			return this.Run<T>(command, out type);
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x00045A36 File Offset: 0x00043C36
		internal T Run<T>(MrsAccessorCommand command, out Type ignoredErrorType) where T : class
		{
			return base.RunCommand<T>(command.Command, command.IgnoreExceptions, command.TransientExceptions, out ignoredErrorType);
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x00045A54 File Offset: 0x00043C54
		protected static long HandleLongOverflow(ulong value, RequestStatisticsBase subscription)
		{
			if (value > 9223372036854775807UL)
			{
				MigrationLogger.Log(MigrationEventType.Warning, "Subscription {0}: Enumerated more items {1} than a signed long can store.", new object[]
				{
					subscription,
					value
				});
				return long.MaxValue;
			}
			return (long)value;
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x00045A98 File Offset: 0x00043C98
		protected MRSSubscriptionId GetMRSIdentity(ISubscriptionId subscriptionId, bool required = false)
		{
			if (required)
			{
				MigrationUtil.ThrowOnNullArgument(subscriptionId, "subscriptionId");
			}
			else if (subscriptionId == null)
			{
				return null;
			}
			MRSSubscriptionId mrssubscriptionId = subscriptionId as MRSSubscriptionId;
			MigrationUtil.AssertOrThrow(mrssubscriptionId != null, "SubscriptionId needs to be a MRSSubscriptionID", new object[0]);
			return mrssubscriptionId;
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00045ADC File Offset: 0x00043CDC
		protected override T HandleException<T>(string commandString, Exception ex, ICollection<Type> transientExceptions)
		{
			MigrationUtil.ThrowOnNullArgument(ex, "ex");
			this.HandleTransientException(ex as LocalizedException, transientExceptions);
			LocalizedException ex2 = ex as LocalizedException;
			if (ex2 != null)
			{
				throw new MigrationPermanentException(ex2.LocalizedString, commandString, ex);
			}
			throw new MigrationPermanentException(ServerStrings.MigrationRunspaceError(commandString, ex.Message), ex);
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00045B2C File Offset: 0x00043D2C
		private void HandleTransientException(LocalizedException ex, ICollection<Type> transientExceptions)
		{
			if (ex == null)
			{
				return;
			}
			if (ex is ManagementObjectNotFoundException)
			{
				throw new MigrationTransientException(ex.LocalizedString, ex);
			}
			if (CommonUtils.IsTransientException(ex))
			{
				throw new MigrationTransientException(ex.LocalizedString, ex);
			}
			Type type;
			if (RunspaceAccessorBase.ExceptionIsIn(ex, transientExceptions, out type))
			{
				throw new MigrationTransientException(ex.LocalizedString, ex);
			}
		}

		// Token: 0x040005DF RID: 1503
		private const string BatchNameFormat = "MigrationService:{0}";
	}
}
